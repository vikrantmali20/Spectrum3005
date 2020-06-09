Imports SpectrumBL
''' <summary>
''' This Class is Used for Advance search oF Item objects
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNAdvanceSearch
#Region "Global Variable For Class"
    Dim FormNormalHeight, SpliterDistance As Integer
    Dim ObjectId As String
    Dim dtObject As DataTable
    Dim obj As New clsAdvanceSearch
    Dim dtWhere, dtAttribute, dtAttributeList, dtResultSet As DataTable
    Dim _ean As String
#End Region
#Region "Class Events"
    Public ReadOnly Property Ean() As String
        Get
            Return _ean
        End Get
    End Property
    Private Sub frmAdvanceSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FormNormalHeight = Me.Height
            SpliterDistance = SplitContainer1.SplitterDistance
            GetObjects()
            CreateGridDataSour()
            fillOperators()
            SetLayout(1)
            cbObjects.SelectedIndex = 0

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR10"), "ADSR10 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub cmdFNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFNext.Click
        Try
            SetLayout(4)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cbObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbObjects.SelectedValueChanged
        Try
            If cbObjects.SelectedValue Is Nothing Then Exit Sub
            If cbObjects.SelectedValue.ToString() <> "System.Data.DataRowView" Then
                ObjectId = cbObjects.SelectedValue
                dtAttribute = obj.GetAttributeList(ObjectId)
                dtAttributeList = dtAttribute.Copy()
                dtResultSet = dtAttribute.Clone()
                If Not dtAttribute Is Nothing Then
                    cbAttributes.DataSource = dtAttribute
                    cbAttributes.ValueMember = "RELFIELD_NAME"
                    cbAttributes.DisplayMember = "ATTRIBUTE_NAME"
                    cbAttributes.ExtendRightColumn = True
                    For Each r As C1.Win.C1List.Split In cbAttributes.Splits
                        Dim i As Integer
                        For i = 0 To r.DisplayColumns.Count - 1
                            If r.DisplayColumns(i).Name <> cbAttributes.DisplayMember Then
                                r.DisplayColumns(i).Visible = False
                            End If
                        Next
                    Next

                    lstAttribute.DataSource = dtAttributeList
                    lstAttribute.DisplayMember = "ATTRIBUTE_NAME"
                    lstAttribute.ValueMember = "RELFIELD_NAME"
                    lstAttribute.ExtendRightColumn = True
                    For Each r As C1.Win.C1List.Split In lstAttribute.Splits
                        Dim i As Integer
                        For i = 0 To r.DisplayColumns.Count - 1
                            If r.DisplayColumns(i).Name <> lstAttribute.DisplayMember Then
                                r.DisplayColumns(i).Visible = False
                            End If
                        Next
                    Next
                    lstResultSet.DataSource = dtResultSet
                    lstResultSet.DisplayMember = "ATTRIBUTE_NAME"
                    lstResultSet.ValueMember = "RELFIELD_NAME"
                    lstResultSet.ExtendRightColumn = True
                    For Each r As C1.Win.C1List.Split In lstResultSet.Splits
                        Dim i As Integer
                        For i = 0 To r.DisplayColumns.Count - 1
                            If r.DisplayColumns(i).Name <> lstResultSet.DisplayMember Then
                                r.DisplayColumns(i).Visible = False
                            End If
                        Next
                    Next
                    cbAttributes.SelectedIndex = -1
                    SetLayout(2)
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR02"), "ADSR02 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdFCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgWhere_AfterDataRefresh(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles dgWhere.AfterDataRefresh
        Try
            If dgWhere.Rows.Count - 1 > 0 Then
                cbLogicalOperator.Visible = True
                lblLogicalOperator.Visible = True
                cbLogicalOperator.SelectedIndex = 0
            Else
                cbLogicalOperator.Visible = False
                lblLogicalOperator.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            If txtInput1.Text = String.Empty Then
                ShowMessage(getValueByKey("ADSR03"), "ADSR03 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If cbAttributes.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ADSR04"), "ADSR04 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If cbOperators.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ADSR05"), "ADSR05 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If cbLogicalOperator.Visible = True AndAlso cbLogicalOperator.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ADSR06"), "ADSR06 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim dr As DataRow = dtWhere.NewRow()
            If cbLogicalOperator.Visible = True And cbLogicalOperator.SelectedIndex <> -1 Then
                dr("Loper") = cbLogicalOperator.SelectedValue
            End If
            dr("Attribute") = cbAttributes.SelectedText
            dr("AttributeCode") = cbAttributes.SelectedValue
            dr("OperCode") = cbOperators.SelectedValue
            dr("Oper") = cbOperators.SelectedText
            dr("Value1") = txtInput1.Text.Trim
            If txtInput2.Text.Trim <> String.Empty Then
                dr("Value2") = txtInput2.Text.Trim
            End If
            If ClearWhere(False) Then
                dtWhere.Rows.Add(dr)
                SetLayout(3)
                lblInput2.Visible = False
                txtInput2.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrevious.Click
        Try
            SetLayout(2)
            SetLayout(3)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdForward_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdForward.Click
        Try
            Dim Str As String = ""
            For Each Item As Int32 In lstAttribute.SelectedIndices
                Str = Str & "'" & lstAttribute.GetItemText(Item, "ATTRIBUTE_NAME") & "',"
            Next
            Str = Str.Substring(0, Str.Length - 1)
            For Each row As DataRow In dtAttributeList.Select("ATTRIBUTE_NAME in (" & Str & ")", "", DataViewRowState.CurrentRows)
                dtResultSet.ImportRow(row)
                row.Delete()
            Next
            dtAttributeList.AcceptChanges()
            dtResultSet.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdBackward_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBackward.Click
        Try
            Dim Str As String = ""
            For Each Item As Int32 In lstResultSet.SelectedIndices
                Str = Str & "'" & lstResultSet.GetItemText(Item, "ATTRIBUTE_NAME") & "',"
            Next
            Str = Str.Substring(0, Str.Length - 1)
            For Each row As DataRow In dtResultSet.Select("ATTRIBUTE_NAME in (" & Str & ")", "", DataViewRowState.CurrentRows)
                dtAttributeList.ImportRow(row)
                row.Delete()
            Next
            dtAttributeList.AcceptChanges()
            dtResultSet.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdMCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdMNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMNext.Click
        Try
            lstSort.DataSource = dtResultSet.Copy()
            lstSort.DisplayMember = "ATTRIBUTE_NAME"
            lstSort.ValueMember = "RELFIELD_NAME"
            lstSort.ExtendRightColumn = True
            For Each r As C1.Win.C1List.Split In lstSort.Splits
                Dim i As Integer
                For i = 0 To r.DisplayColumns.Count - 1
                    If r.DisplayColumns(i).Name <> lstSort.DisplayMember Then
                        r.DisplayColumns(i).Visible = False
                    End If
                Next
            Next
            SetLayout(5)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdLPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLPrevious.Click
        Try
            SetLayout(4)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cbOperators_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOperators.SelectedValueChanged
        Try
            If cbOperators.SelectedValue.ToString.ToUpper() = "BetWeen".ToUpper() Then
                txtInput2.Visible = True
                lblInput2.Visible = True
            Else
                txtInput2.Visible = False
                lblInput2.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdLFinish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLFinish.Click
        Try
            Dim StrQuery As String = ""
            If MakeQuery(StrQuery) = True Then
                getData(StrQuery)
                If dgResult.Cols.Count > 0 Then
                    dgResult.Cols(0).Visible = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdFFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFFinish.Click
        Try
            Dim StrQuery As String = ""
            If MakeQuery(StrQuery) = True Then
                getData(StrQuery)
                If dgResult.Cols.Count > 0 Then
                    dgResult.Cols(0).Visible = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdMFinish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMFinish.Click
        Try
            Dim StrQuery As String = ""
            If MakeQuery(StrQuery) = True Then
                getData(StrQuery)
                If dgResult.Cols.Count > 0 Then
                    dgResult.Cols(0).Visible = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Private Functions & Methods"
    ''' <summary>
    ''' Get the Result of the Query
    ''' </summary>
    ''' <param name="strQuery">Query</param>
    ''' <remarks></remarks>
    Private Sub getData(ByVal strQuery As String)
        Try
            Dim dt As DataTable = obj.GetResultedData(strQuery)
            If Not dt Is Nothing Then
                dgResult.DataSource = dt
                Me.Height = FormNormalHeight
                SplitContainer1.Panel2Collapsed = False
                SplitContainer1.SplitterDistance = SpliterDistance
                dgResult.Visible = True
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR07"), "ADSR07 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get the diffrent Objects 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetObjects()
        Try
            dtObject = obj.GetObjects()
            If Not dtObject Is Nothing And dtObject.Rows.Count > 0 Then
                cbObjects.DataSource = dtObject
                cbObjects.ValueMember = dtObject.Columns("OBJECTSID").ColumnName
                cbObjects.DisplayMember = dtObject.Columns("OBJECTSNAME").ColumnName
                cbObjects.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cbObjects.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cbObjects.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cbObjects.SelectedIndex = -1
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR02"), "ADSR02 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Form Layouts 
    ''' </summary>
    ''' <param name="Level">Level no</param>
    ''' <remarks></remarks>
    Private Sub SetLayout(ByVal Level As Int32)
        Try
            If Level = 1 Then
                Dim i As Integer
                i = SplitContainer1.Panel2.Height
                i = FormNormalHeight - i
                Me.Height = i
                SplitContainer1.Panel2Collapsed = True
                cbObjects.Enabled = True
                sizHeader.Visible = False
                sizMidlle.Visible = False
                sizLast.Visible = False
            ElseIf Level = 2 Then
                'SplitContainer1.Panel1Collapsed = True
                cbObjects.Enabled = False
                dgWhere.Visible = False
                sizHeader.Visible = True
            ElseIf Level = 3 Then
                dgWhere.Visible = True
            ElseIf Level = 4 Then
                sizHeader.Visible = False
                sizMidlle.Visible = True
            ElseIf Level = 5 Then
                sizLast.Visible = True
                sizMidlle.Visible = False
                sizHeader.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get the Structure of Where condition
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateGridDataSour()
        Try
            dtWhere = obj.GetWhereStru()
            If Not dtWhere Is Nothing Then
                dgWhere.DataSource = dtWhere
                dgWhere.Cols("Loper").Caption = "Logical Operator"
                dgWhere.Cols("Attribute").Caption = "Attributes"
                dgWhere.Cols("oper").Caption = "Operator"
                dgWhere.Cols("Value1").Caption = "First Input"
                dgWhere.Cols("Value2").Caption = "Second Input"
                dgWhere.Cols("AttributeCode").Visible = False
                dgWhere.Cols("operCode").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Fill Logical Operators And Normal Operators 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fillOperators()
        Try
            Dim dtLogical, dtOper As DataTable
            dtLogical = obj.GetLogicalOperators()
            If Not dtLogical Is Nothing Then
                cbLogicalOperator.DataSource = dtLogical
                cbLogicalOperator.DisplayMember = "Value"
                cbLogicalOperator.ValueMember = "Code"
                cbLogicalOperator.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cbLogicalOperator.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name.ToUpper() <> "Value".ToUpper() Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cbLogicalOperator.SelectedIndex = -1
            End If
            dtOper = obj.GetOperators()
            If Not dtOper Is Nothing Then
                cbOperators.DataSource = dtOper
                cbOperators.DisplayMember = "OPERENGLAN"
                cbOperators.ValueMember = "OPER"
                cbOperators.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cbOperators.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cbOperators.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cbOperators.SelectedIndex = -1
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR08"), "ADSR08 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Clear the Screen
    ''' </summary>
    ''' <param name="GridClear">True/False </param>
    ''' <returns>True/False </returns>
    ''' <remarks></remarks>
    Private Function ClearWhere(ByVal GridClear As Boolean) As Boolean
        Try
            cbAttributes.SelectedIndex = -1
            cbOperators.SelectedIndex = -1
            txtInput1.Text = String.Empty
            txtInput2.Text = String.Empty
            If GridClear = True Then
                dtWhere.Clear()
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Make Query Based on Selections
    ''' </summary>
    ''' <param name="StrQuery">Return Query</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function MakeQuery(ByRef StrQuery As String) As Boolean
        Try
            Dim TableName, WhereCondition, SelectList, SortList As String
            For Each row As DataRow In dtObject.Select("ObjectsId='" & ObjectId & "'", "ObjectSName", DataViewRowState.CurrentRows)
                TableName = row("ViewNames").ToString()
            Next
            WhereCondition = "Where "
            For Each row As DataRow In dtWhere.Rows
                Dim str, Oper As String
                If row("Loper").ToString() <> String.Empty Then
                    str = row("Loper").ToString()
                End If
                Oper = row("OperCode").ToString
                If Oper.Contains("%") Then
                    If Oper = "%_" Then
                        Oper = "LIKE '%" & row("Value1").ToString() & "'"
                    ElseIf Oper = "_%" Then
                        Oper = "LIKE '" & row("Value1").ToString() & "%'"
                    ElseIf Oper = "%_%" Then
                        Oper = "LIKE '%" & row("Value1") & "%'"
                    End If
                Else
                    Oper = row("OperCode").ToString() & " '" & row("Value1").ToString() & "'"
                End If
                str = str & " " & row("AttributeCode").ToString() & " " & Oper
                If row("Value2").ToString() <> String.Empty Then
                    str = str & " AND '" & row("Value2").ToString() & "'"
                End If
                WhereCondition = WhereCondition & str & " "
            Next
            For Each row As DataRow In dtResultSet.Rows
                SelectList = SelectList & row("RELFIELD_NAME") & ","
            Next
            If SelectList = String.Empty Then
                SelectList = "*"
            Else
                SelectList = SelectList.Substring(0, SelectList.Length - 1)
            End If

            For Each item As Int32 In lstSort.SelectedIndices
                SortList = SortList & lstSort.GetItemText(item, "ATTRIBUTE_NAME") & ","
            Next
            StrQuery = "SELECT DISTINCT ArticleCode AS ArticleCodeS, " & SelectList & " FROM " & TableName & " " & WhereCondition
            If Not SortList Is Nothing AndAlso SortList.Length > 2 Then
                SortList = SortList.Substring(0, SortList.Length - 1)
                If chkGroupBy.Checked = True Then
                    StrQuery = StrQuery & " Group By " & SelectList
                End If
                If rbAsc.Checked = True Then
                    StrQuery = StrQuery & " Order By " & SortList & " ASC"
                ElseIf rbDesc.Checked = True Then
                    StrQuery = StrQuery & " Order By " & SortList & " DESC"
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage(getValueByKey("ADSR09"), "ADSR09 - " & getValueByKey("CLAE04"))
            LogException(ex)
        End Try
    End Function
#End Region
    Private Sub dgResult_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgResult.DoubleClick
        Try
            _ean = dgResult.Rows(dgResult.RowSel)(0).ToString()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgResult_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgResult.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                _ean = dgResult.Rows(dgResult.RowSel)(0).ToString()
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ThemeChange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        cbLogicalOperator.Location = New System.Drawing.Point(10, 35)
        Me.lblLogicalOperator.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblLogicalOperator.BorderStyle = BorderStyle.None
        Me.lblLogicalOperator.Location = New System.Drawing.Point(10, 12)
        Me.lblLogicalOperator.MinimumSize = New Size(112, 21)
        Me.lblLogicalOperator.MaximumSize = New Size(112, 21)
        Me.lblLogicalOperator.Size = New Size(112, 21)

        Me.lblAttribute.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblAttribute.BorderStyle = BorderStyle.None
        Me.lblAttribute.Location = New System.Drawing.Point(127, 12)
        Me.lblAttribute.MinimumSize = New Size(128, 21)
        Me.lblAttribute.Size = New Size(128, 21)

        Me.lblOperators.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblOperators.BorderStyle = BorderStyle.None
        Me.lblOperators.Location = New System.Drawing.Point(261, 12)
        Me.lblOperators.MinimumSize = New Size(112, 21)
        Me.lblOperators.Size = New Size(112, 21)

        Me.lblInput1.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblInput1.BorderStyle = BorderStyle.None
        Me.lblInput1.Location = New System.Drawing.Point(378, 12)
        Me.lblInput1.MinimumSize = New Size(95, 21)
        Me.lblInput1.Size = New Size(95, 21)

        Me.txtInput2.Location = New System.Drawing.Point(478, 35)
        Me.lblInput2.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblInput2.BorderStyle = BorderStyle.None
        Me.lblInput2.Location = New System.Drawing.Point(478, 12)
        Me.lblInput2.MinimumSize = New Size(95, 21)
        Me.lblInput2.Size = New Size(95, 21)

        Me.lblResultSet.BackColor = Color.Transparent
        Me.lblResultSet.BorderStyle = BorderStyle.None
        Me.lblResultSet.ForeColor = Color.White

        Me.lblAttributList.BackColor = Color.Transparent
        Me.lblAttributList.BorderStyle = BorderStyle.None
        Me.lblAttributList.ForeColor = Color.White
        'Me.lblAttributList.Location = New System.Drawing.Point(10, 12)
        'Me.lblAttributList.MinimumSize = New Size(112, 17)
        'Me.lblAttributList.MaximumSize = New Size(112, 17)
        'Me.lblAttributList.Size = New Size(112, 17)

        Me.lblSortBy.BackColor = Color.Transparent
        Me.lblSortBy.BorderStyle = BorderStyle.None
        Me.lblSortBy.ForeColor = Color.White

        Me.lblObject.BackColor = Color.Transparent
        Me.lblObject.BorderStyle = BorderStyle.None
        Me.lblObject.ForeColor = Color.White

        Me.cmdAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdAdd.BackColor = Color.Transparent
        Me.cmdAdd.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdAdd.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdAdd.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdAdd.FlatStyle = FlatStyle.Flat
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdFNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdFNext.BackColor = Color.Transparent
        Me.cmdFNext.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdFNext.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdFNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdFNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdFNext.FlatStyle = FlatStyle.Flat
        Me.cmdFNext.FlatAppearance.BorderSize = 0
        Me.cmdFNext.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdFFinish.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdFFinish.BackColor = Color.Transparent
        Me.cmdFFinish.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdFFinish.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdFFinish.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdFFinish.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdFFinish.FlatStyle = FlatStyle.Flat
        Me.cmdFFinish.FlatAppearance.BorderSize = 0
        Me.cmdFFinish.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdFCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdFCancel.BackColor = Color.Transparent
        Me.cmdFCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdFCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdFCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdFCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdFCancel.FlatStyle = FlatStyle.Flat
        Me.cmdFCancel.FlatAppearance.BorderSize = 0
        Me.cmdFCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdMCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdMCancel.BackColor = Color.Transparent
        Me.cmdMCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdMCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdMCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdMCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdMCancel.FlatStyle = FlatStyle.Flat
        Me.cmdMCancel.FlatAppearance.BorderSize = 0
        Me.cmdMCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdMNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdMNext.BackColor = Color.Transparent
        Me.cmdMNext.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdMNext.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdMNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdMNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdMNext.FlatStyle = FlatStyle.Flat
        Me.cmdMNext.FlatAppearance.BorderSize = 0
        Me.cmdMNext.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdBackward.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdBackward.BackColor = Color.Transparent
        Me.cmdBackward.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdBackward.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdBackward.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdBackward.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdBackward.FlatStyle = FlatStyle.Flat
        Me.cmdBackward.FlatAppearance.BorderSize = 0
        Me.cmdBackward.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdForward.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdForward.BackColor = Color.Transparent
        Me.cmdForward.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdForward.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdForward.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdForward.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdForward.FlatStyle = FlatStyle.Flat
        Me.cmdForward.FlatAppearance.BorderSize = 0
        Me.cmdForward.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdMFinish.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdMFinish.BackColor = Color.Transparent
        Me.cmdMFinish.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdMFinish.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdMFinish.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdMFinish.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdMFinish.FlatStyle = FlatStyle.Flat
        Me.cmdMFinish.FlatAppearance.BorderSize = 0
        Me.cmdMFinish.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdPrevious.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrevious.BackColor = Color.Transparent
        Me.cmdPrevious.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdPrevious.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdPrevious.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdPrevious.FlatStyle = FlatStyle.Flat
        Me.cmdPrevious.FlatAppearance.BorderSize = 0
        Me.cmdPrevious.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdLFinish.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdLFinish.BackColor = Color.Transparent
        Me.cmdLFinish.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdLFinish.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdLFinish.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdLFinish.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdLFinish.FlatStyle = FlatStyle.Flat
        Me.cmdLFinish.FlatAppearance.BorderSize = 0
        Me.cmdLFinish.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.cmdLPrevious.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdLPrevious.BackColor = Color.Transparent
        Me.cmdLPrevious.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdLPrevious.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdLPrevious.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.cmdLPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdLPrevious.FlatStyle = FlatStyle.Flat
        Me.cmdLPrevious.FlatAppearance.BorderSize = 0
        Me.cmdLPrevious.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.dgWhere.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgWhere.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgWhere.Rows.MinSize = 25
        Me.dgWhere.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgWhere.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgWhere.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgWhere.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgWhere.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgWhere.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgWhere.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        Me.dgResult.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgResult.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgResult.Rows.MinSize = 25
        Me.dgResult.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgResult.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgResult.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgResult.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgResult.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgResult.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgResult.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

    End Function
End Class
