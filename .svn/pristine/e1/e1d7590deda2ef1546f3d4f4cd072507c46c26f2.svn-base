Imports Spectrum
Imports SpectrumBL
Imports System.Text
Imports System.IO

Public Class frmNGrievance
    Dim objCls As New clsCommon
    Dim objGrievance As New clsGrievance
    Dim status As String = String.Empty
    Dim OldStatus As String
    Dim NewStatus As String
    Dim OldDept As String = String.Empty
    Dim RaisedFromDeptSite As String = String.Empty
    Dim NewDept As String = String.Empty
    Dim OldDeptName As String = String.Empty
    Dim NewDeptName As String = String.Empty
    Dim Orderby As String = ""
    Dim OldTicketType As String = String.Empty
    Dim IsTicketUpdated As Boolean = False
    Dim IsTexBoxRemarkAdded As Boolean = False
    Public _IsEdit As Boolean = False
    Public strFinyear As String = String.Empty
    Public GrievanceHistoryText As String = String.Empty
    Dim _resultGrievanceHistoryDetail As New DataTable
    Dim _isGrievanceHistoryChange As Boolean = False
    Dim toolTip As ToolTip
    Dim IsFileUpload As Boolean = False
    Dim fileLocation As String
    Dim folder As String
    Dim TargetFolderPath
    Dim RecentUploadFilePath As String
    Dim FullNameWithExtension As String
    Dim TxtRemarks As String = ""
    Private OFileDialog As OpenFileDialog
    Public filterImage As String = "All files (*.*)|*.*"
    Dim strLastRemarkIdForGrvHistory As String = ""
    Dim TempStrLastRemarkIdForGrvHistory As String = ""
    Dim MobileNumberListSaveWithHistory As String = ""
    Dim objComn As New clsGrievance
    Dim tooltip1 As New ToolTip
    Dim userid, userid1, userid2 As String
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property

    Public _GrievanceId As String
    Public Property GrievanceId() As String
        Get
            Return _GrievanceId
        End Get
        Set(ByVal value As String)
            _GrievanceId = value
        End Set
    End Property

    Public _RaisedBy As String
    Public Property RaisedBy() As String
        Get
            Return _RaisedBy
        End Get
        Set(ByVal value As String)
            _RaisedBy = value
        End Set
    End Property
    Public _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property

    Public _IsRead As Boolean
    Public Property IsRead() As Boolean
        Get
            Return _IsRead
        End Get
        Set(ByVal value As Boolean)
            _IsRead = value
        End Set
    End Property
    Public Function PopulateChkListRaisedDept(ByVal dtCombo As DataTable)
        ChkListRaisedDept.DataSource = dtCombo
        ChkListRaisedDept.ValueMember = dtCombo.Columns(0).ColumnName
        ChkListRaisedDept.DisplayMember = dtCombo.Columns(1).ColumnName
        ChkListRaisedDept.SelectedIndex = -1
        Return ""
    End Function
    Public Function PopulateChkListCCDept(ByVal dtCombo As DataTable)
        ChkListCCDept.DataSource = dtCombo
        ChkListCCDept.ValueMember = dtCombo.Columns(0).ColumnName
        ChkListCCDept.DisplayMember = dtCombo.Columns(1).ColumnName
        ChkListCCDept.SelectedIndex = -1
        Return ""
    End Function
    Dim deptBox As DataTable
    Dim deptBox1 As DataTable
    Dim deptBox2 As DataTable
    Dim SiteBox As DataTable
    Dim SiteBox1 As DataTable
    Dim ListViewShowCCDeptItemCount As Integer = 0
    Dim loadtimelistview As New ListView
    Dim submittimelistview As New ListView
    Dim SavedCCDept As String
    Private Sub frmNGrievance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CtrlLblSMSNo.MaximumSize = New Size(150, 15)
            RbCCSite.Visible = False
            'Added by sagar
            'Update Read status for a given ticket
            If IsRead Then
                If UpdatedBy <> clsAdmin.UserCode Then
                    objGrievance.UpdateReadStatusForTicket(clsAdmin.SiteCode, clsAdmin.UserCode, GrievanceId, False)
                End If
            End If
            c1Sizer1.Size = Me.Size
            deptBox = objCls.GetDepartment(clsAdmin.UserCode)
            deptBox1 = objCls.GetDepartment(clsAdmin.UserCode)
            deptBox2 = objCls.GetDepartment(clsAdmin.UserCode)
            SiteBox = objCls.GetSite(clsAdmin.UserCode, clsAdmin.SiteCode)
            SiteBox1 = objCls.GetSite(clsAdmin.UserCode, clsAdmin.SiteCode)
            '' New Changes Sprint = 23
            RbRaisedDept.Checked = True
            RbCCDept.Checked = True
            PopulateChkListRaisedDept(deptBox2)
            PopulateChkListCCDept(deptBox1)
            cbDepartment.Visible = False
            PopulateComboBox(deptBox, cbDepartment)
            pC1ComboSetDisplayMember(cbDepartment)
            'Button1.Visible = False
            'Button2.Visible = False
            'Dim griTypeBox = objCls.GetGrievanceType()
            'PopulateComboBox(griTypeBox, cbGrievanceType)
            'pC1ComboSetDisplayMember(cbGrievanceType)
            'code added for jk sprint 24 by vipul

            'If ListViewShowCCDept.Items.Count > 0 Then
            '    ListViewShowCCDeptItemCount = ListViewShowCCDept.Items.Count
            'End If

            dgGrievanceHistoryGrid.Visible = False
            Dim dt As DataTable
            strFinyear = objCls.GetFinancialYear(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
            If IsEdit = True Then

                dt = objCls.GetGrievanceDetailIdWise(clsAdmin.SiteCode, GrievanceId)

                SetValueEditTime(dt)
                lblRaisedFromSiteOrdept.Text = RaisedFromDeptSite
                lblId.Text = GrievanceId
                OldStatus = dt.Rows(0)("GrievanceStatus")
                'OldDept = dt.Rows(0)("DeptId")
                OldTicketType = dt.Rows(0)("GrievanceTypeId")
                txtGrievanceTitle.Text = dt.Rows(0)("Title").ToString()
                txtGrievanceDetail.Text = dt.Rows(0)("GrievanceDesc1").ToString()
                If RbRaisedDept.Checked Then
                    cbDepartment.SelectedValue = dt.Rows(0)("RaisedToDepartment")
                Else
                    cbDepartment.SelectedValue = dt.Rows(0)("AssignedSiteCode")
                End If


                cbGrievanceType.SelectedValue = dt.Rows(0)("GrievanceTypeId")
                cbStatus.SelectedText = dt.Rows(0)("GrievanceStatus")
                status = dt.Rows(0)("GrievanceStatus")
                If (status = "Broadcast") Then
                    cbDepartment.Enabled = False
                    cbStatus.Enabled = False
                    txtGrievanceDetail.ReadOnly = True
                    dgGrievanceHistoryGrid.Enabled = False
                    cmdSubmit.Visible = False

                End If
                CtrlLabelCreatedBy.Text = dt.Rows(0)("Createdby")
                CtrlLabelCreatedOn.Text = Convert.ToDateTime(dt.Rows(0)("Createdon")).ToString("MM/dd/yyyy HH:mm:ss")
                CtrlLabelUpdatedBy.Text = dt.Rows(0)("UpdatedBy")
                CtrlLabelUpdatedOn.Text = Convert.ToDateTime(dt.Rows(0)("UpdatedOn")).ToString("MM/dd/yyyy HH:mm:ss")
                txtGrievanceTitle.ReadOnly = True
                txtGrievanceDetail.ReadOnly = True
                Dim TicketSiteCode = dt.Rows(0)("SiteCode").ToString()
                If RaisedBy = Nothing Then
                    If TicketSiteCode = "CCE" Then
                        RaisedBy = "Bo"
                    Else
                        RaisedBy = "Fo"
                    End If
                End If
                'If Not status = "New" Then
                '    RdAsc.Visible = True
                '    RdDesc.Visible = True
                'Else
                '    RdAsc.Visible = False
                '    RdDesc.Visible = False
                'End If

                _resultGrievanceHistoryDetail = objGrievance.GetGrievanceHistory(GrievanceId)
                If (_resultGrievanceHistoryDetail.Rows.Count > 0) Then
                    dgGrievanceHistoryGrid.Visible = True
                    dgGrievanceHistoryGrid.DataSource = _resultGrievanceHistoryDetail
                    If dgGrievanceHistoryGrid.Rows.Count <= 5 = True Then
                        GHistory()
                    End If

                End If

                Dim statusBox = objCls.GetStatus()
                If clsDefaultConfiguration.JkTicketingSystem Then
                    statusBox.Rows.RemoveAt(3)
                End If

                Dim statusMsg As String = status
                Select Case statusMsg
                    Case "New" 'New
                        statusBox.Rows.RemoveAt(2)
                        If RaisedBy = "Fo" Then
                            statusBox.Rows.RemoveAt(1)
                        ElseIf RaisedBy = "Bo" Then
                            statusBox.Rows.Clear()
                            statusBox.Rows.Add("New", "New")
                            statusBox.Rows.Add("Resolved", "Resolved")
                        End If
                    Case "Open" 'Open
                        statusBox.Rows.Clear()
                        statusBox.Rows.Add("Open", "Open")
                    Case "Resolved"
                        'statusBox.Rows.RemoveAt(0)
                        'If RaisedBy = "Fo" Then
                        '    statusBox.Rows.RemoveAt(0)
                        'ElseIf RaisedBy = "Bo" Then
                        '    statusBox.Rows.RemoveAt(1)
                        'End If
                        statusBox.Rows.Clear()
                        If RaisedBy = "Fo" Then
                            statusBox.Rows.Add("Resolved", "Resolved")
                            statusBox.Rows.Add("Re-opened", "Re-opened")
                        ElseIf RaisedBy = "Bo" Then
                            statusBox.Rows.Add("Resolved", "Resolved")
                        End If
                    Case "Re-opened"
                        statusBox.Rows.Clear()
                        If RaisedBy = "Fo" Then
                            statusBox.Rows.Add("Re-opened", "Re-opened")
                        ElseIf RaisedBy = "Bo" Then
                            statusBox.Rows.Add("Resolved", "Resolved")
                            statusBox.Rows.Add("Re-opened", "Re-opened")
                        End If
                    Case "Closed"
                        statusBox.Rows.RemoveAt(1)
                        statusBox.Rows.RemoveAt(1)
                End Select

                PopulateComboBox(statusBox, cbStatus)
                pC1ComboSetDisplayMember(cbStatus)



                cmdSubmit.Text = "Save"
                cmdSubmit.Tag = "Save"

                For Each item In ListViewShowCCDept.Items
                    loadtimelistview.Items.Add(item)
                Next
                AddHandler btnEditTicketAttachment.Click, AddressOf Edit_show_document
            Else
                RaisedFromDeptSite = objCls.GetSiteByCode(clsAdmin.SiteCode).ToString()
                lblRaisedFromSiteOrdept.Text = RaisedFromDeptSite
                CtrlLabel6.Text = "Raised From Site :"
                Dim Month, day, Quarter As Int32
                Month = clsAdmin.DayOpenDate.Month
                day = clsAdmin.DayOpenDate.Day
                Dim docno As String = objCls.getDocumentNo("Grievance", clsAdmin.SiteCode)
                'clsAdmin.DayOpenDate

                ' docno = GenDocNo("GCF" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 14, docno)
                'docno = GenDocNo("GC" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3), 15, docno)
                'commented by khusrao adil on 02-11-2017 for jk sprint 31
                ' docno = GenDocNo("GCF" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 14, docno)
                'modified by khusrao adil on 2-11-2017 for jk sprint 31
                ' docno = GenDocNo("GCF" & clsAdmin.LegacySiteCode.Substring(clsAdmin.LegacySiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 14, docno)
                docno = GenDocNo("GC" & clsAdmin.SiteStdCode & strFinyear.Substring(strFinyear.Length - 2, 2), 13, docno)
                lblId.Text = docno
                cbStatus.DisplayMember = "New"
                cbStatus.SelectedText = "New"
                cbStatus.Enabled = False
                status = "New"

                CtrlCreatedBy.Visible = False
                CtrlCreatedOn.Visible = False
                CtrlUpdatedBy.Visible = False
                CtrlUpdatedOn.Visible = False

                'Call AddRemark(, "", "(F)")

                OldStatus = cbStatus.SelectedText
                RdAsc.Visible = False
                RdDesc.Visible = False

            End If

            RdAsc.Checked = True
            lblGrievanceTitle.Visible = Not clsDefaultConfiguration.JkTicketingSystem
            cbGrievanceType.Visible = Not clsDefaultConfiguration.JkTicketingSystem
            lblGrievanceType.Visible = Not clsDefaultConfiguration.JkTicketingSystem
            txtGrievanceTitle.Visible = Not clsDefaultConfiguration.JkTicketingSystem
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'added for jk issue
            'BindMobileNo()
            BindMobileNoList()


            'code added for jk sprint 25

            If ListViewShowCCDept.Items.Count > 0 Then
                ListViewShowCCDeptItemCount = ListViewShowCCDept.Items.Count
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GHistory()
        Dim rowsCount = dgGrievanceHistoryGrid.Rows.Count
        dgGrievanceHistoryGrid.Rows.MinSize = 19
        dgGrievanceHistoryGrid.Rows.MaxSize = 19
        dgGrievanceHistoryGrid.Rows.DefaultSize = 19
        dgGrievanceHistoryGrid.MinimumSize = New Size(863, 20 * rowsCount - 1)
        dgGrievanceHistoryGrid.MaximumSize = New Size(863, 20 * rowsCount - 1)
        dgGrievanceHistoryGrid.Size = New Size(863, 20 * rowsCount - 1)
        dgGrievanceHistoryGrid.ScrollBars = ScrollBars.None
        Return 0
    End Function
    'code added for jk sprint 24 by vipul 
    Dim dtCMFForRaiesdFromOnEdit As DataTable
    Dim RaisedFromSite As String = clsAdmin.SiteCode
    Dim IsRaisedFromSite As Boolean = True
    Dim RaisedToDepartment As Integer
    Dim AssignedSiteCode As String = ""
    Dim IsRaisedToSite As Boolean = False
    Dim IsCCSite As Boolean = False
    Dim CCSite As String = ""
    Dim CCDepartment As String = ""
    Dim DepId As Integer
    Private Function SetValue(Optional ByVal RaisedDepSite As String = "", Optional ByVal CCDepSite As String = "")
        Try

            If RbRaisedDept.Checked Then
                RaisedToDepartment = RaisedDepSite
            Else
                AssignedSiteCode = RaisedDepSite
            End If
            If RbRaisedSite.Checked Then
                IsRaisedToSite = True
            Else
                IsRaisedToSite = False
            End If
            If RbCCSite.Checked Then
                CCSite = CCDepSite
                IsCCSite = True
            Else
                CCDepartment = CCDepSite
                IsCCSite = False
            End If
            DepId = Nothing
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function BindToListBox(ByVal Departmentname As String)
        Dim i As Integer
        Dim aryTextFile() As String
        aryTextFile = Departmentname.Split(",")
        For Each part In aryTextFile
            If part <> "" Then
                For j As Integer = 0 To ChkListCCDept.Items.Count - 1
                    If DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(0) = part Then
                        ChkListCCDept.SetItemChecked(j, True)
                        Dim ItemNme = DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(1)
                        ListViewShowCCDept.Items.Add(ItemNme)
                        Exit For
                        'ChkListRaisedDept.SetItemCheckState(j, CheckState.Checked)
                    End If
                Next

            End If
        Next
    End Function
    'code added for jk sprint 24 by vipul
    'method added for unchecked checklist
    Private Function UnBindToListBox(ByVal Departmentname As String)
        Dim i As Integer
        Dim aryTextFile() As String
        aryTextFile = Departmentname.Split(",")
        For Each part In aryTextFile
            If part <> "" Then
                For j As Integer = 0 To ChkListCCDept.Items.Count - 1
                    If DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(0) = part Then
                        ChkListCCDept.SetItemChecked(j, False)
                        'Dim ItemNme = DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(1)
                        'ListViewShowCCDept.Items.Add(ItemNme)
                        Exit For
                        'ChkListRaisedDept.SetItemCheckState(j, CheckState.Checked)
                    End If
                Next

            End If
        Next
    End Function
    'method added for unchecked checklist if client remove from raised  ticket CMF
    Private Function BindToListBoxOnCMF(ByVal Departmentname As String)
        Dim i As Integer
        Dim aryTextFile() As String
        aryTextFile = Departmentname.Split(",")
        For Each part In aryTextFile
            If part <> "" Then
                For j As Integer = 0 To ChkListCCDept.Items.Count - 1
                    If DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(0) = part Then
                        ChkListCCDept.SetItemChecked(j, True)
                        'Dim ItemNme = DirectCast(ChkListCCDept.Items(j), System.Data.DataRowView).Row.ItemArray(1)
                        'ListViewShowCCDept.Items.Add(ItemNme)
                        Exit For
                        'ChkListRaisedDept.SetItemCheckState(j, CheckState.Checked)
                    End If
                Next

            End If
        Next
    End Function
    Private Function SetValueEditTime(ByVal dt As DataTable)
        Try

            ChkListRaisedDept.Visible = False
            ListViewShowRaisedDept.Visible = False
            cbDepartment.Visible = True
            If dt.Rows(0)("IsRaisedToSite") Then
                RbRaisedSite.Checked = True
            Else
                RbRaisedDept.Checked = True
                'Dim theItemRaisedToDept = dt.Rows(0)("RaisedToDepartment").ToString
                'For j As Integer = 0 To ChkListRaisedDept.Items.Count - 1
                '    If DirectCast(ChkListRaisedDept.Items(j), System.Data.DataRowView).Row.ItemArray(0) = theItemRaisedToDept Then
                '        ChkListRaisedDept.SetItemChecked(j, True)
                '        Dim ItemNme = DirectCast(ChkListRaisedDept.Items(j), System.Data.DataRowView).Row.ItemArray(1)
                '        ListViewShowRaisedDept.Items.Add(ItemNme)
                '        Exit For
                '        'ChkListRaisedDept.SetItemCheckState(j, CheckState.Checked)
                '    End If
                'Next
            End If
            If dt.Rows(0)("IsCCSite") Then
                RbCCSite.Checked = True
                Dim CCSite = dt.Rows(0)("CCSite").ToString
                BindToListBox(CCSite)
            Else
                Dim CCDepartment = dt.Rows(0)("CCDepartment").ToString
                BindToListBox(CCDepartment)

            End If
            If Not IsDBNull(dt.Rows(0)("RaisedToDepartment")) AndAlso dt.Rows(0)("RaisedToDepartment") <> "0" Then
                OldDept = dt.Rows(0)("RaisedToDepartment")
                OldDeptName = objCls.GetDepartmentById(OldDept).ToString()
            Else
                OldDept = dt.Rows(0)("AssignedSiteCode")
                OldDeptName = objCls.GetSiteByCode(OldDept).ToString()
            End If
            If dt.Rows(0)("IsRaisedFromSite") Then
                Dim RaisedFromSite = dt.Rows(0)("RaisedFromSite")
                RaisedFromDeptSite = objCls.GetSiteByCode(RaisedFromSite).ToString()
                dtCMFForRaiesdFromOnEdit = objGrievance.Togetsite_cmfid(RaisedFromSite)
            Else
                Dim RaisedFromDept = dt.Rows(0)("DeptId")
                RaisedFromDeptSite = objCls.GetDepartmentById(RaisedFromDept).ToString()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function ValidationForRaisedAndCC() As Boolean
        Try
            Dim RaisedFrom = lblRaisedFromSiteOrdept.Text
            Dim ShowDisplayMessage = "Raised From ,Raised To or CC cannot be same"
            If Not IsEdit Then
                For i As Integer = 0 To ListViewShowRaisedDept.Items.Count - 1
                    Dim Raised = ListViewShowRaisedDept.Items(i).ToString
                    If Raised = RaisedFrom Then
                        ShowMessage(ShowDisplayMessage, "GRV004 - " & getValueByKey("GRV010"))
                        Exit Function
                        Return False
                    End If
                    For j As Integer = 0 To ListViewShowCCDept.Items.Count - 1
                        Dim CC = ListViewShowCCDept.Items(j).ToString
                        If Raised = CC Then
                            ShowMessage(ShowDisplayMessage, "GRV004 - " & getValueByKey("GRV010"))
                            Exit Function
                            Return False
                        End If
                    Next
                Next
                Return True
            Else
                Dim EditRaised = cbDepartment.SelectedText
                If EditRaised = RaisedFrom Then
                    ShowMessage(ShowDisplayMessage, "GRV004 - " & getValueByKey("GRV010"))
                    Exit Function
                    Return False
                End If
                If EditRaised = "" Then
                    ShowMessage(getValueByKey("GRV003") & " /Site", "GRV003 - " & getValueByKey("GRV010"))
                    ' ShowMessage("Please Select Department", "" & getValueByKey("CLAE04"))
                    Exit Function
                    Return False
                End If
                For j As Integer = 0 To ListViewShowCCDept.Items.Count - 1
                    Dim CC = ListViewShowCCDept.Items(j).ToString

                    If EditRaised = CC Then
                        ShowMessage(ShowDisplayMessage, "GRV004 - " & getValueByKey("GRV010"))
                        Exit Function
                        Return False
                    End If
                Next
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        Try
            If Not ValidationForRaisedAndCC() Then
                Exit Sub
            End If
            If IsEdit Then
                'jk sprint 25
                ''''new added code 12-05-2017
                Dim dtcccdept As New DataTable
                dtcccdept.Columns.Add("CCDepartment")
                dtcccdept = DeptMapTOTicket(GrievanceId)
                Dim ccdeptonly As String = ""
                ccdeptonly = dtcccdept.Rows(0)("CCDepartment").ToString()

                '-------------------------------------------
                Dim userid, userid1, userid2 As String
                Dim removeHimselfDept As Boolean
                removeHimselfDept = False
                For Each item In ListViewShowCCDept.Items
                    submittimelistview.Items.Add(item)
                Next
                Dim TxtRemarks As String = ""
                Dim IsBoldApplicable As Boolean = False
                'code moved by khusrao adil on 15-11-2017 for remark wiouth changes save issues
                'If Not NewStatus = "New" Then
                TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.Trim()
                ' End If
                '--------------------
                ' comparing two listview
                Dim oldnewCCmatchCount As Integer = 0
                For Each oldCC As ListViewItem In loadtimelistview.Items
                    For Each newCC As ListViewItem In submittimelistview.Items
                        If oldCC.Text = newCC.Text Then
                            oldnewCCmatchCount = oldnewCCmatchCount + 1
                        End If
                    Next
                Next

                If ListViewShowCCDept.Items.Count <> ListViewShowCCDeptItemCount Then
                    'add some new cc or delete some cc
                    If oldnewCCmatchCount = ListViewShowCCDept.Items.Count - 1 Then
                        removeHimselfDept = True
                        'user remove only one CC
                        'need to check is remove himself or any other ONE
                        Dim AA = clsAdmin.UserCode
                        Dim bb = clsAdmin.SiteCode
                        For Each oldCC As ListViewItem In loadtimelistview.Items
                            For Each newCC As ListViewItem In submittimelistview.Items
                                If oldCC.Text <> newCC.Text Then
                                    'is map to CC
                                    Dim removeHimselfDeptName = oldCC.Text
                                End If
                            Next
                        Next
                        'Else

                        '    If oldnewCCmatchCount <> ListViewShowCCDept.Items.Count Then
                        '    End If
                    End If
                End If
                '-------------------LIST OF ALL MAPPING
                ' Dim userid, userid1, userid2 As String
                Dim isMaptoFrom, isMaptoTO, isMaptoCC As Boolean
                Dim ticketraisedfromsite, ticketraisedfromCCE As Boolean
                Dim mapflagcount As Integer = 0
                ' userid = clsAdmin.UserName
                userid1 = clsAdmin.UserCode
                userid2 = clsAdmin.SiteCode
                Dim dtUserMapFordept, newdtUserMapFordept As New DataTable
                Dim dtUserMapForSite, newdtUserMapForSite As New DataTable
                Dim dtUserMapForFromSite, newdtUserMapForFromSite As DataTable
                Dim dtTicketInfo, newdtTicketInfo As New DataTable
                Dim dtRaisedfromMobileNumbers As New DataTable
                dtUserMapFordept = GetDepartmentMapForUser(userid1)
                Dim dtview As New DataView(dtUserMapFordept)
                newdtUserMapFordept = dtview.ToTable(True, "DeptId")   ''' dept user map
                dtUserMapForSite = GetSiteMapForUser(userid1)  '' site user map
                Dim dtview1 As New DataView(dtUserMapForSite)
                newdtUserMapForSite = dtview1.ToTable(True, "SiteCode")
                dtTicketInfo = GetGrievanceTicketInfo(GrievanceId)  '' tickted info
                Dim dtview2 As New DataView(dtTicketInfo)
                newdtTicketInfo = dtview2.ToTable(True, "RaisedFromSite", "DeptId", "CREATEDAT", "CREATEDBY")
                Dim isRaisedFromSite1 As String = ""
                Dim iscreatedby As String = ""
                Dim isDeptId As String = ""
                ticketraisedfromsite = False
                ticketraisedfromCCE = False
                isRaisedFromSite1 = newdtTicketInfo.Rows(0)("RaisedFromSite").ToString()
                iscreatedby = newdtTicketInfo.Rows(0)("CREATEDBY").ToString()
                ' iscreatedby()
                isDeptId = newdtTicketInfo.Rows(0)("DeptId").ToString()
                If isRaisedFromSite1.Equals("") Then '''grab FROM where Ticket is Raised
                    ticketraisedfromCCE = True
                Else
                    ticketraisedfromsite = True
                End If
                If ticketraisedfromsite = True Then
                    isRaisedFromSite1 = newdtTicketInfo.Rows(0)("RaisedFromSite").ToString()
                    If iscreatedby = userid1 Then
                        'For Each dr As DataRow In dtUserMapForSite.Rows
                        '    If dr("SiteCode") = isRaisedFromSite1 Then
                        '        isMaptoFrom = True
                        '        mapflagcount = mapflagcount + 1
                        '        Exit For
                        '    End If
                        'Next
                        isMaptoFrom = True
                        mapflagcount = mapflagcount + 1
                    End If
                Else
                    'isDeptId = newdtTicketInfo.Rows(0)("DeptId").ToString()
                    'For Each dr As DataRow In newdtUserMapFordept.Rows
                    '    If dr("DeptId") = isDeptId Then
                    '        isMaptoFrom = True
                    '        mapflagcount = mapflagcount + 1
                    '        Exit For
                    '    End If
                    'Next
                End If
                If ticketraisedfromCCE = True Then
                    dtRaisedfromMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(isDeptId)   'from ticket dept number
                Else
                    dtRaisedfromMobileNumbers = objCls.GetMobileNumberBySiteCode(isRaisedFromSite1) '' from Site Mobile number
                End If
                '  If newdtTicketInfo.Select("RaisedFromSite <> Null").Count Then
                Dim x As String = ""
                If RbRaisedSite.Checked Then
                    x = cbDepartment.SelectedValue
                    For Each dr As DataRow In dtUserMapForSite.Rows
                        If dr("SiteCode") = x Then
                            isMaptoTO = True
                            mapflagcount = mapflagcount + 1
                            Exit For
                        End If
                    Next
                End If

                If RbRaisedDept.Checked Then
                    x = cbDepartment.SelectedValue
                    For Each dr As DataRow In newdtUserMapFordept.Rows
                        If dr("DeptId") = x Then
                            isMaptoTO = True
                            mapflagcount = mapflagcount + 1
                            Exit For
                        End If
                    Next
                End If
                Dim removeccdeptmobilenumber As String = ""
                Dim ccmapcount As Integer = 0
                Dim dtccuser As New DataTable
                ''decalration purpose of  dtccuser if there are more than 1 dept present in CC and only CC is map to user AND only 1 Dept from CC is map to User
                dtccuser.Columns.Add("DeptId")
                Dim cccount = ChkListCCDept.CheckedItems.Count
                Dim maptoccflag As Boolean
                maptoccflag = False
                'code for adding CCE DEPTID number
                If ChkListCCDept.CheckedItems.Count > 0 Then
                    For J As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                        Dim selectedCCDeparmentId = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                        Dim selectedCCDepSite1 = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                        For Each dr As DataRow In newdtUserMapFordept.Rows
                            If dr("DeptId") = selectedCCDeparmentId Then
                                ccmapcount = ccmapcount + 1
                                isMaptoCC = True
                                maptoccflag = True
                                Dim newdr As DataRow
                                newdr = dtccuser.NewRow()
                                newdr("DeptId") = selectedCCDeparmentId
                                dtccuser.Rows.Add(newdr)
                                removeccdeptmobilenumber = selectedCCDeparmentId
                            End If
                        Next
                    Next
                    '---------------------
                    '  isMaptoFrom, isMaptoTO, isMaptoCC,ccmapcount
                End If
                If maptoccflag = True Then
                    mapflagcount = mapflagcount + 1
                End If
                '------------------------
                'newadded for comparing old raisedTo name and Current Raised To NAME
                Dim oldRaisedTOname = OldDeptName
                Dim currentRaisedTOname = cbDepartment.Text.ToString
                '--------------
                Dim selectedCCDepSite As String = ""
                Dim dtRaisedMobileNumbers As New DataTable
                Dim dtCCMobileNumbers As New DataTable

                If ccmapcount >= 2 Then
                    GoTo line2
                End If

line2:          If RbRaisedDept.Checked Then
                    dtRaisedMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                Else
                    dtRaisedMobileNumbers = objCls.GetMobileNumberBySiteCode(cbDepartment.SelectedValue) '' Site MobileNo
                End If

                'code added for jk sprint 24 by vipul
                ADDCMFValueAtEditTime()
                If ChkListCCDept.CheckedItems.Count > 0 Then
                    For J As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                        Dim selectedCCDeparmentId = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                        Dim selectedCCDepSite1 = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                        selectedCCDepSite = selectedCCDepSite.ToString + "," + selectedCCDepSite1.ToString
                        If RbCCDept.Checked Then
                            dtCCMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(selectedCCDeparmentId)
                        Else
                            dtCCMobileNumbers = objCls.GetMobileNumberBySiteCode(selectedCCDeparmentId) '' CC Site MobileNo
                        End If

                        dtRaisedMobileNumbers.Merge(dtCCMobileNumbers)
                    Next
                End If
                'we adding from mobile number if more than two dept map which are present in CC or MORE than Two are Two mapping are Found 
                If ccmapcount > 1 Or mapflagcount >= 2 Then

                    If dtRaisedfromMobileNumbers IsNot Nothing Then
                        dtRaisedMobileNumbers.Merge(dtRaisedfromMobileNumbers)
                    End If
                    GoTo line3
                End If

                '''''''''''''''''''''''''''''''if user map to ONLY 1 from OR To Or CC
                If mapflagcount = 1 Then
                    'if user map to FROM Ticket 
                    If isMaptoFrom = True Then
                        'no need to write code allready working senario
                    End If
                    If isMaptoTO = True Then
                        dtRaisedMobileNumbers.Clear()
                        'Grabbing the number of from 
                        If dtRaisedfromMobileNumbers IsNot Nothing Then
                            dtRaisedMobileNumbers.Merge(dtRaisedfromMobileNumbers)
                        End If
                        dtCCMobileNumbers.Clear()
                        'GARBBING the number of CC
                        If ChkListCCDept.CheckedItems.Count > 0 Then
                            For J As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                                Dim selectedCCDeparmentId = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                Dim selectedCCDepSite1 = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                selectedCCDepSite = selectedCCDepSite.ToString + "," + selectedCCDepSite1.ToString
                                If RbCCDept.Checked Then
                                    dtCCMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(selectedCCDeparmentId)
                                Else
                                    dtCCMobileNumbers = objCls.GetMobileNumberBySiteCode(selectedCCDeparmentId) '' CC Site MobileNo
                                End If
                                dtRaisedMobileNumbers.Merge(dtCCMobileNumbers)
                            Next
                        End If
                    End If

                    If isMaptoCC = True Then
                        '''grabbingb the no. of RaisedTo
                        If RbRaisedDept.Checked Then
                            dtRaisedMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                        Else

                            dtRaisedMobileNumbers = objCls.GetMobileNumberBySiteCode(cbDepartment.SelectedValue) '' Site MobileNo
                        End If
                        'garbbing the number of FROM
                        If dtRaisedfromMobileNumbers IsNot Nothing Then
                            dtRaisedMobileNumbers.Merge(dtRaisedfromMobileNumbers)
                        End If
                        If ccmapcount = 1 And cccount > 1 Then
                            'appart from map Single CC dept Sms should be send to all other CC dept
                            dtCCMobileNumbers.Clear()
                            If ChkListCCDept.CheckedItems.Count > 0 Then
                                For J As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                                    Dim selectedCCDeparmentId = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                    Dim selectedCCDepSite1 = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                    ' selectedCCDepSite = selectedCCDepSite.ToString + "," + selectedCCDepSite1.ToString
                                    If RbCCDept.Checked Then
                                        dtCCMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(selectedCCDeparmentId)
                                    Else
                                        dtCCMobileNumbers = objCls.GetMobileNumberBySiteCode(selectedCCDeparmentId) '' CC Site MobileNo
                                    End If

                                    dtRaisedMobileNumbers.Merge(dtCCMobileNumbers)
                                Next
                            End If
                            Dim dtforremoveSingleDeptMobileNO As New DataTable
                            Dim removedeptnumber As String = ""
                            dtforremoveSingleDeptMobileNO.Columns.Add("MobileNo")
                            'now only remove that single CC dept mobile number from dtraisedMo 
                            dtforremoveSingleDeptMobileNO = objCls.GetDepartmentMobileNumberByDepartmentId(removeccdeptmobilenumber)

                            removedeptnumber = newdtTicketInfo.Rows(0)("MobileNo").ToString()

                            'dtforremoveSingleDeptMobileNO()
                            For Each dr As DataRow In dtRaisedMobileNumbers.Rows
                                If dr("MobileNo") = removedeptnumber Then
                                    ' dtRaisedMobileNumbers()
                                    'ccmapcount = ccmapcount + 1
                                    'isMaptoCC = True
                                    'maptoccflag = True
                                    'Dim newdr As DataRow
                                    'newdr = dtccuser.NewRow()
                                    'newdr("DeptId") = selectedCCDeparmentId
                                    'dtccuser.Rows.Add(newdr)
                                    'removeccdeptmobilenumber = selectedCCDeparmentId
                                End If
                            Next
                        End If
                    End If
                End If
                '----------------------------------------------------------------------------------------------------------
                'code added if we change raised TO 

                '''''''''if login user change the RaisedTo Then sms should be send too
                '1--if site is in RaisedTO then That site and it's CMF if present
                '2--if dept is in RaisedTo then only that departtment
                'NOTE--NO need to send CC if he add some cc then also no need to send to CC
                If removeHimselfDept = True And isMaptoCC = True And mapflagcount = 1 Then
                    dtRaisedMobileNumbers.Clear()

                End If

line3:          If oldRaisedTOname <> currentRaisedTOname Then
                    dtRaisedMobileNumbers.Clear()
                    If RbRaisedDept.Checked Then
                        dtRaisedMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                        GoTo line1

                    Else
                        dtRaisedMobileNumbers = objCls.GetMobileNumberBySiteCode(cbDepartment.SelectedValue) '' Site MobileNo
                    End If

                    Dim CMFIDRaiesdTo As String = ""
                    Dim dtCMFIdForRaiesdToSite As New DataTable
                    Dim siteCMF = cbDepartment.SelectedValue
                    'grabbing the CMF 
                    dtCMFIdForRaiesdToSite = objGrievance.Togetsite_cmfid(siteCMF)
                    If dtCMFIdForRaiesdToSite IsNot Nothing Then
                        dtCCMobileNumbers.Clear()
                        CMFIDRaiesdTo = dtCMFIdForRaiesdToSite.Rows(0)("SQFTArea")
                        dtCCMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(CMFIDRaiesdTo)
                        BindToListBoxOnCMF(CMFIDRaiesdTo)
                        dtRaisedMobileNumbers.Merge(dtCCMobileNumbers)
                    End If
                End If
                ' End If
                'Dim TxtRemarks As String = ""
                'Dim IsBoldApplicable As Boolean = False
                'If IsEdit Then
                '    'If Not NewStatus = "New" Then
                '    TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.Trim()
                '    ' End If
                'End If

                '--------------------------------------------------------------------------------------
                If removeHimselfDept = True And TxtRemarks <> "" Then
                    'dtRaisedMobileNumbers.Clear()
                End If
                '-----------------------------
                'Dim ccsavednumberpresent As New Boolean
                'ccsavednumberpresent = False
                'Dim words As String() = ccdeptonly.Split(New Char() {","c})
                'Dim word As String
                'For Each word In words
                '    'Console.WriteLine(word)
                '    Dim dtsavedDepMobNo As DataTable
                '    If word <> "" Then
                '        dtsavedDepMobNo = objCls.GetDepartmentMobileNumberByDepartmentId(word)
                '    End If
                '    If dtsavedDepMobNo IsNot Nothing Then
                '        If dtRaisedMobileNumbers IsNot Nothing Then
                '            For i = 0 To dtRaisedMobileNumbers.Rows.Count - 1
                '                Dim mob1 As String = dtRaisedMobileNumbers.Rows(i)("MobileNo")
                '                If mob1.Equals(dtsavedDepMobNo.Rows(0)("MobileNo")) Then
                '                    ccsavednumberpresent = True

                '                End If


                '            Next
                '        End If
                '    End If
                '    If ccsavednumberpresent = True Then

                '    Else

                '        If dtsavedDepMobNo IsNot Nothing Then
                '            dtRaisedMobileNumbers.Merge(dtsavedDepMobNo)
                '        End If

                '    End If
                'Next
line1:          Dim dtMobileNumbers = dtRaisedMobileNumbers.Copy
                Dim DD = cbDepartment.SelectedValue

                SetValue(DD, selectedCCDepSite)
                NewStatus = cbStatus.Text
                '' Dim TxtRemarks As String = ""
                '' Dim IsBoldApplicable As Boolean = False
                'If IsEdit Then
                '    'If Not NewStatus = "New" Then
                '    TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.Trim()
                '    ' End If
                'End If
                'Added by sagar
                Dim doEntryForSms As Boolean = False
                If TxtRemarks <> "" Then
                    doEntryForSms = True
                    Dim objremarkstatus As New frmArticlesRemark
                    objremarkstatus.TicketStatus = OldStatus
                    objremarkstatus.isTicket = True
                    objremarkstatus.RaisedBy = RaisedBy
                    objremarkstatus.ShowDialog()
                    If objremarkstatus.DialogResult = Windows.Forms.DialogResult.OK Then
                        NewStatus = objremarkstatus.cbStatus.Text
                        cbStatus.Text = objremarkstatus.cbStatus.Text
                        status = objremarkstatus.cbStatus.Text
                    End If
                End If
                ' Dim doEntryForSms As Boolean = False
                NewStatus = cbStatus.Text
                'added by khusrao adil to check if status is resolved then took site mobile number else deparment number
                Dim dtMobileNumber As DataTable
                'If NewStatus = "Resolved" AndAlso OldStatus <> "Resolved" Then
                '    dtMobileNumber = objCls.GetMobileNumberBySiteCode(clsAdmin.SiteCode)
                'Else
                '    dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                'End If
                ' code change by khusrao adil on 02-09-2016
                ' sms should send to respective deparment
                ' dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                If RbRaisedDept.Checked Then
                    dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                Else
                    dtMobileNumber = objCls.GetMobileNumberBySiteCode(cbDepartment.SelectedValue) '' Site MobileNo
                End If

                If dtMobileNumbers.Rows.Count > 0 Then
                    Dim MobNo As String = ""
                    For MobileRow = 0 To dtMobileNumbers.Rows.Count - 1
                        MobNo = MobNo + "," + dtMobileNumbers.Rows(MobileRow)(0).ToString
                    Next
                    MobileNumberListSaveWithHistory = MobNo.Remove(0, 1)
                End If
                '' added by ketan check mobile no if mobile no are not available then set doEntryForSms is false
                'Dim dtMobileNumber As DataTable = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                If Not dtMobileNumbers Is Nothing AndAlso dtMobileNumbers.Rows.Count = 0 Then
                    doEntryForSms = False
                Else
                    If IsEdit = True AndAlso NewStatus = "Resolved" AndAlso OldStatus <> "Resolved" Then
                        doEntryForSms = True
                    ElseIf IsEdit = True AndAlso NewStatus = "Re-opened" AndAlso OldStatus <> "Re-opened" Then
                        doEntryForSms = True
                    ElseIf IsEdit = False Then
                        doEntryForSms = True
                    ElseIf IsEdit = True AndAlso ListViewShowCCDept.Items.Count <> ListViewShowCCDeptItemCount Then
                        doEntryForSms = True
                    End If
                End If
                'Dim TxtRemarks As String = ""

                'If IsEdit Then
                '    'If Not NewStatus = "New" Then
                '    TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.trim()
                '    ' End If
                'End If
                'added by sagar as discussed with yogesh no need for below code
                'If clsDefaultConfiguration.JkTicketingSystem Then
                '    If RaisedBy = "Fo" Then
                '        'If OldStatus = "Resolved" Then
                '        '    If cbStatus.Text = OldStatus Or NewStatus Is Nothing AndAlso TxtRemarks <> "" Then
                '        '        'ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '        '        'Exit Sub
                '        '        status = "Re-opened"
                '        '    End If
                '        'End If
                '        If OldStatus = "Resolved" Then
                '            If cbStatus.Text = OldStatus Or NewStatus Is Nothing Then
                '                If clsDefaultConfiguration.RenderGrievance = True Then
                '                    If TxtRemarks <> "" Then
                '                        status = "Re-opened"
                '                        doEntryForSms = True
                '                    Else
                '                        ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '                        Exit Sub
                '                    End If
                '                Else
                '                    ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '                    Exit Sub
                '                End If
                '            End If
                '        End If
                '    Else
                '        If RaisedBy = "Bo" Then
                '            'If clsDefaultConfiguration.RenderGrievance = True Then
                '            '    If OldStatus = "Resolved" Then
                '            '        If cbStatus.Text = OldStatus Or NewStatus Is Nothing AndAlso TxtRemarks <> "" Then
                '            '            'ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '            '            'Exit Sub
                '            '            status = "Re-opened"
                '            '        End If
                '            '    End If
                '            'End If
                '            If OldStatus = "Resolved" Then
                '                If cbStatus.Text = OldStatus Or NewStatus Is Nothing Then
                '                    If clsDefaultConfiguration.RenderGrievance = True Then
                '                        If TxtRemarks <> "" Then
                '                            status = "Re-opened"
                '                            doEntryForSms = True
                '                        Else
                '                            ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '                            Exit Sub
                '                        End If
                '                    Else
                '                        ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                '                        Exit Sub
                '                    End If
                '                End If
                '            End If
                '        End If

                '    End If
                'End If
                If IsEdit = False Then
                    If cbDepartment.SelectedIndex = -1 Then
                        ShowMessage(getValueByKey("GRV003"), "GRV003 - " & getValueByKey("GRV010"))
                        ' ShowMessage("Please Select Department", "" & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    If clsDefaultConfiguration.JkTicketingSystem Then
                        'cbGrievanceType.SelectedValue = 5
                        cbGrievanceType.Text = "Others"
                    Else
                        If cbGrievanceType.SelectedIndex = -1 Then
                            ShowMessage(getValueByKey("GRV004"), "GRV004 - " & getValueByKey("GRV010"))
                            'ShowMessage("Please Select Grievance Type", "" & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                    If clsDefaultConfiguration.JkTicketingSystem Then
                    Else
                        If txtGrievanceTitle.Text.Trim() = String.Empty Then
                            ShowMessage(getValueByKey("GRV005"), "GRV005 - " & getValueByKey("GRV010"))
                            'ShowMessage("Please Enter Grievance Title", "" & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                    If txtGrievanceDetail.Text.Trim().Replace("'", "") = String.Empty Then
                        ShowMessage(getValueByKey("GRV006"), "GRV006 - " & getValueByKey("GRV010"))
                        'ShowMessage("Please Enter Grievance Details", "" & getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                Else
                    If clsDefaultConfiguration.JkTicketingSystem Then
                        cbGrievanceType.Text = "Others"
                    Else
                        If cbGrievanceType.SelectedIndex = -1 Then
                            ShowMessage(getValueByKey("GRV007"), "GRV007 - " & getValueByKey("GRV010"))
                            'ShowMessage("Please Select Grievance Type", "" & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                End If
                If cbStatus.SelectedText = "" Then
                Else
                    status = cbStatus.SelectedText
                End If
                'TxtRemarks.ToString().Replace("'", "''")

                If TxtRemarks <> Nothing Then
                    If TxtRemarks.ToString.Length > 1000 Then
                        ShowMessage("Maximum character length exceed", "")
                        Exit Sub
                    End If
                Else
                    TxtRemarks = ""
                End If
                '--- Ticker issue in case of updatedOn :i.e. will not update ticket if no changes done by user .
                If IsEdit Then
                    If OldDept <> NewDept Then
                        _isGrievanceHistoryChange = True
                        'Dim OldDeptName As String = objCls.GetDepartmentById(OldDept).ToString()
                        Dim NewDeptName As String
                        If RbRaisedDept.Checked Then
                            NewDeptName = objCls.GetDepartmentById(NewDept).ToString()
                        Else
                            NewDeptName = objCls.GetSiteByCode(NewDept).ToString()
                        End If


                        If TxtRemarks <> "" Then
                            Dim newRemarkValue As String = ""
                            If strLastRemarkIdForGrvHistory <> "" Then
                                newRemarkValue = strLastRemarkIdForGrvHistory
                                Dim newRemarkValueArray() As String
                                newRemarkValueArray = newRemarkValue.Split(" "c)
                                Dim adjust As String = (newRemarkValueArray(1) + newRemarkValueArray(2) + newRemarkValueArray(3) + newRemarkValueArray(4)) + 1
                                Dim bb As String = adjust.Trim()
                                Dim OBJECTForNewRemark As String = "Remark   " + bb + "  (F- " + clsAdmin.UserName + ") " + newRemarkValueArray(7) + " " + newRemarkValueArray(8) + " :" + clsAdmin.CurrentDate.ToString("MM/dd/yyyy HH:mm:ss")
                                GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & OBJECTForNewRemark & " - " & MobileNumberListSaveWithHistory & ""    ''SMS on no.
                            Else
                                Dim OBJECTForNewRemark As String = "Remark   1  (F- " + clsAdmin.UserName + ") Changed on :" + clsAdmin.CurrentDate.ToString("MM/dd/yyyy HH:mm:ss")
                                GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & OBJECTForNewRemark & " - " & MobileNumberListSaveWithHistory & ""
                            End If
                            'Remark   4  (F- admin) 18-07-2016 04:37 PM
                        Else
                            If strLastRemarkIdForGrvHistory = "" Then
                                GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " by : " & clsAdmin.UserCode & " - " & MobileNumberListSaveWithHistory & ""
                            Else
                                GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & strLastRemarkIdForGrvHistory & " - " & MobileNumberListSaveWithHistory & ""
                            End If
                        End If
                        'GrievanceHistoryText = "Department change from : " & OldDeptName & " to " & NewDeptName & " by : " & clsAdmin.UserCode & ""
                        'Department change from : Business Development & Legal to Accounts by : Admin
                        If CtrlLblSMSNo.Text <> "" Then
                            doEntryForSms = True
                        End If
                    End If
                End If
                'code commented for jk sprint 24 by vipul
                If OldDept = NewDept And NewStatus = OldStatus And String.IsNullOrEmpty(TxtRemarks.Trim()) And loadtimelistview.Items.Count = oldnewCCmatchCount And loadtimelistview.Items.Count = ChkListCCDept.CheckedItems.Count Then
                    If clsDefaultConfiguration.JkTicketingSystem Then
                        If RaisedBy = "Bo" Then
                            ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                        Else
                            ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                        End If
                        ' ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                        'ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
                    Else
                        ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                        'ShowMessage(False, String.Format(getValueByKey("GRV008"), lblId.Text), getValueByKey("GRV010"))
                        'ShowMessage(getValueByKey("GRV008"), "GRV008 - " & getValueByKey("GRV010"))
                    End If
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                    Exit Sub
                End If
                If cbGrievanceType.SelectedValue = Nothing Then
                    ShowMessage((String.Format("{0} department not mapped. Please Contact to System Admin", cbDepartment.SelectedText)), getValueByKey("GRV010"))
                    Exit Sub
                End If
                Dim IsReopenHistoryEntery As Boolean = False
                If IsEdit = True AndAlso OldStatus <> NewStatus AndAlso NewStatus = "Re-opened" Then
                    IsReopenHistoryEntery = True
                End If
                'added by sagar
                If OldDept <> NewDept Or NewStatus <> OldStatus Or Not String.IsNullOrEmpty(TxtRemarks.Trim()) Then
                    IsBoldApplicable = True
                End If
                'Dim docno As String = objCls.getDocumentNo("Grievance", clsAdmin.SiteCode)
                'docno = GenDocNo("GC" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 13, docno)

                If objGrievance.SaveGrivanceDetail(Sitecode:=clsAdmin.SiteCode, SiteStdCode:=clsAdmin.SiteStdCode, Grievanceid:=lblId.Text, status:=status,
                    grievanceTypeid:=cbGrievanceType.SelectedValue, departmentid:=DepId, title:=txtGrievanceTitle.Text.Replace("'", ""), details:=txtGrievanceDetail.Text.Replace("'", ""),
                   userid:=clsAdmin.UserCode, EditMode:=IsEdit, Remarks:=TxtRemarks.ToString.Replace("'", "").Trim, strFinyear:=strFinyear, isGrievanceHistoryChange:=_isGrievanceHistoryChange,
                  GrievanceHistoryText:=GrievanceHistoryText, RaisedFromSite:=RaisedFromSite, IsRaisedFromSite:=IsRaisedFromSite, RaisedToDepartment:=RaisedToDepartment,
                  IsRaisedToSite:=IsRaisedToSite, CCSite:=CCSite, CCDepartment:=CCDepartment, IsCCSite:=IsCCSite, AssignedSiteCode:=AssignedSiteCode,
                  dayopendate:=clsAdmin.DayOpenDate, RaisedBy:=RaisedBy, doEntryForSms:=doEntryForSms, fileName:=FullNameWithExtension, MoblileList:=CtrlLblSMSNo.Text,
                  IsReopenHistoryEntery:=IsReopenHistoryEntery, IsBoldApplicable:=IsBoldApplicable, dtMobileNumbers:=dtMobileNumbers) Then
                    'If objGrievance.SaveGrivanceDetail(clsAdmin.SiteCode, lblId.Text, status, cbGrievanceType.SelectedValue, DepId, txtGrievanceTitle.Text.Replace("'", ""), txtGrievanceDetail.Text.Replace("'", ""),
                    '                                 clsAdmin.UserCode, IsEdit, TxtRemarks.ToString.Replace("'", "").Trim, strFinyear, _isGrievanceHistoryChange, GrievanceHistoryText,
                    '                                RaisedFromSite, IsRaisedFromSite, RaisedToDepartment, IsRaisedToSite, CCSite, CCDepartment, IsCCSite, AssignedSiteCode,
                    '                                clsAdmin.DayOpenDate, RaisedBy, doEntryForSms, FullNameWithExtension, CtrlLblSMSNo.Text, IsReopenHistoryEntery, IsBoldApplicable, dtMobileNumbers:=dtMobileNumbers) Then
                    '    ' If objGrievance.SaveGrivanceDetail(clsAdmin.SiteCode, lblId.Text, status, cbGrievanceType.SelectedValue, cbDepartment.SelectedValue, txtGrievanceTitle.Text.Replace("'", ""), txtGrievanceDetail.Text.Replace("'", ""), clsAdmin.UserCode, IsEdit, TxtRemarks.ToString.Replace("'", "").Trim, strFinyear, _isGrievanceHistoryChange, GrievanceHistoryText, clsAdmin.DayOpenDate, RaisedBy, doEntryForSms, FullNameWithExtension, CtrlLblSMSNo.Text, IsReopenHistoryEntery, IsBoldApplicable) Then
                    If TxtRemarks <> "" AndAlso IsFileUpload = True Then
                        CreateDirectoryandCopyFile(objGrievance.GrievanceRemarkIdForAttachement)
                        objGrievance.GrievanceRemarkIdForAttachement = ""
                    End If
                Else
                    ShowMessage(getValueByKey("GRV011"), "GRV011 - " & getValueByKey("GRV010"))
                End If

                'ShowMessage("Error In Saving Grievance Details", " " & getValueByKey("CLAE04"))

                If IsEdit = True Then
                    If clsDefaultConfiguration.JkTicketingSystem Then
                        If RaisedBy = "Bo" Then
                            ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                        Else
                            ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                        End If

                        'ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
                    Else
                        ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                        ' ShowMessage(False, String.Format(getValueByKey("GRV008"), lblId.Text, "GRV008 - " & getValueByKey("GRV010")))
                        'ShowMessage(getValueByKey("GRV008"), "GRV008 - " & getValueByKey("GRV010"))
                    End If

                    ' ShowMessage("Grievance Updated Successfully ", " " & getValueByKey("CLAE04"))
                Else
                    If clsDefaultConfiguration.JkTicketingSystem Then
                        ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                        ' ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
                    Else
                        ShowMessage((String.Format(getValueByKey("GRV009"), lblId.Text)), getValueByKey("GRV010"))
                        'ShowMessage(getValueByKey("GRV009"), "GRV009 - " & getValueByKey("GRV010"))
                    End If

                    'ShowMessage("Grievance Saved Successfully", " " & getValueByKey("CLAE04"))
                    'Reset()
                End If
            Else
                Dim CheckSequence = objCls.GetSiteByCode(clsAdmin.SiteCode, True) 'aded by vipin if SiteStdCode not present then block the ticket generATION 26032018
                If String.IsNullOrEmpty(CheckSequence) Then
                    ShowMessage("Ticketing Sequence not map for this site.Contact to system administrator", getValueByKey("GRV010"))
                    Exit Sub
                End If
                If ChkListRaisedDept.CheckedItems.Count > 0 Then
                    For i As Integer = 0 To ChkListRaisedDept.CheckedItems.Count - 1
                        Dim selectedDeparmentId = DirectCast(ChkListRaisedDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(0)
                        Dim selectedDeparmentName = DirectCast(ChkListRaisedDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(1)
                        Dim selectedCCDepSite As String = ""
                        'commented for jk issue
                        'Dim MobList = BindMobileNo(selectedDeparmentId)
                        Dim dtRaisedMobileNumbers As DataTable
                        If RbRaisedDept.Checked Then
                            dtRaisedMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(selectedDeparmentId)
                        Else
                            dtRaisedMobileNumbers = objCls.GetMobileNumberBySiteCode(selectedDeparmentId) '' Site MobileNo
                        End If

                        'code added for jk sprint 24 by vipul
                        '' New Funcationality of By Defauylt ADD CMF Value In CC By Vipul For JKSprint-24
                        If txtGrievanceDetail.Text.Trim().Replace("'", "") = String.Empty Then
                            ShowMessage(getValueByKey("GRV006"), "GRV006 - " & getValueByKey("GRV010"))
                            Exit Sub
                        End If
                        ' ADDCMFValueAtCreateTime(selectedDeparmentId)
                        Dim CMFIDRaiesdTo As String
                        Dim dtCCMobileNumbersCMF As DataTable
                        Dim dtCMFIdForRaiesdTo As DataTable
                        Dim dtCMFIdForRaisedFrom As DataTable
                        If Not RbRaisedDept.Checked Then
                            Dim SiteCodeforCMF = selectedDeparmentId

                            dtCMFIdForRaiesdTo = objGrievance.Togetsite_cmfid(SiteCodeforCMF)
                            dtCMFIdForRaisedFrom = objGrievance.Togetsite_cmfid(clsAdmin.SiteCode)
                            If dtCMFIdForRaiesdTo.Rows.Count > 0 Then
                                'Dim CMFIDRaiesdTo As String = dtCMFIdForRaiesdTo.Rows(0)("SQFTArea")
                                CMFIDRaiesdTo = dtCMFIdForRaiesdTo.Rows(0)("SQFTArea")
                                BindToListBox(CMFIDRaiesdTo)
                            End If
                            If dtCMFIdForRaisedFrom.Rows.Count > 0 Then
                                Dim CMFIDRaisedFrom As String = dtCMFIdForRaisedFrom.Rows(0)("SQFTArea")
                                BindToListBox(CMFIDRaisedFrom)
                            End If
                        Else
                            dtCMFIdForRaisedFrom = objGrievance.Togetsite_cmfid(clsAdmin.SiteCode)

                            If dtCMFIdForRaisedFrom.Rows.Count > 0 Then
                                If selectedDeparmentId.ToString <> dtCMFIdForRaisedFrom.Rows(0)("SQFTArea").ToString Then
                                    Dim CMFIDRaisedFrom As String = dtCMFIdForRaisedFrom.Rows(0)("SQFTArea")
                                    BindToListBox(CMFIDRaisedFrom)
                                End If
                            End If
                        End If
                        If ChkListCCDept.CheckedItems.Count > 0 Then
                            For J As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                                Dim selectedCCDeparmentId = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                Dim selectedCCDepSite1 = DirectCast(ChkListCCDept.CheckedItems(J), System.Data.DataRowView).Row.ItemArray(0)
                                selectedCCDepSite = selectedCCDepSite.ToString + "," + selectedCCDepSite1.ToString
                                Dim dtCCMobileNumbers As DataTable
                                If RbCCDept.Checked Then
                                    dtCCMobileNumbers = objCls.GetDepartmentMobileNumberByDepartmentId(selectedCCDeparmentId)
                                Else
                                    dtCCMobileNumbers = objCls.GetMobileNumberBySiteCode(selectedCCDeparmentId) '' Site MobileNo
                                End If

                                dtRaisedMobileNumbers.Merge(dtCCMobileNumbers)
                            Next
                        End If
                        Dim dtMobileNumbers = dtRaisedMobileNumbers.Copy
                        SetValue(selectedDeparmentId, selectedCCDepSite)

                        'code added for jk sprint 24 by vipul
                        If CMFIDRaiesdTo <> "" Then
                            UnBindToListBox(CMFIDRaiesdTo)
                        End If
                        NewStatus = cbStatus.Text

                        Dim TxtRemarks As String = ""
                        If IsFileUpload Then
                            TxtRemarks = "Find Attachment for Reference "
                        End If
                        Dim IsBoldApplicable As Boolean = False
                        If IsEdit Then
                            'If Not NewStatus = "New" Then
                            TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.Trim()
                            ' End If
                        End If
                        'Added by sagar
                        'If TxtRemarks <> "" Then
                        '    Dim objremarkstatus As New frmArticlesRemark
                        '    objremarkstatus.TicketStatus = OldStatus
                        '    objremarkstatus.isTicket = True
                        '    objremarkstatus.RaisedBy = RaisedBy
                        '    objremarkstatus.ShowDialog()
                        '    If objremarkstatus.DialogResult = Windows.Forms.DialogResult.OK Then
                        '        NewStatus = objremarkstatus.cbStatus.Text
                        '        cbStatus.Text = objremarkstatus.cbStatus.Text
                        '        status = objremarkstatus.cbStatus.Text
                        '    End If
                        'End If


                        Dim doEntryForSms As Boolean = False
                        NewStatus = cbStatus.Text
                        'added by khusrao adil to check if status is resolved then took site mobile number else deparment number
                        Dim dtMobileNumber As DataTable
                        'If NewStatus = "Resolved" AndAlso OldStatus <> "Resolved" Then
                        '    dtMobileNumber = objCls.GetMobileNumberBySiteCode(clsAdmin.SiteCode)
                        'Else
                        '    dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                        'End If
                        ' code change by khusrao adil on 02-09-2016
                        ' sms should send to respective deparment
                        ' dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                        If RbRaisedDept.Checked Then
                            dtMobileNumber = objCls.GetDepartmentMobileNumberByDepartmentId(selectedDeparmentId)
                        Else
                            dtMobileNumber = objCls.GetMobileNumberBySiteCode(selectedDeparmentId) '' Site MobileNo
                        End If

                        If dtMobileNumbers.Rows.Count > 0 Then
                            Dim MobNo As String = ""
                            For MobileRow = 0 To dtMobileNumbers.Rows.Count - 1
                                MobNo = MobNo + "," + dtMobileNumbers.Rows(MobileRow)(0).ToString
                            Next
                            MobileNumberListSaveWithHistory = MobNo.Remove(0, 1)
                        End If
                        '' added by ketan check mobile no if mobile no are not available then set doEntryForSms is false
                        'Dim dtMobileNumber As DataTable = objCls.GetDepartmentMobileNumberByDepartmentId(cbDepartment.SelectedValue)
                        If Not dtMobileNumbers Is Nothing AndAlso dtMobileNumbers.Rows.Count = 0 Then
                            doEntryForSms = False
                        Else
                            If IsEdit = True AndAlso NewStatus = "Resolved" AndAlso OldStatus <> "Resolved" Then
                                doEntryForSms = True
                            ElseIf IsEdit = True AndAlso NewStatus = "Re-opened" AndAlso OldStatus <> "Re-opened" Then
                                doEntryForSms = True
                            ElseIf IsEdit = False Then
                                doEntryForSms = True
                            End If
                        End If
                        'Dim TxtRemarks As String = ""

                        'If IsEdit Then
                        '    'If Not NewStatus = "New" Then
                        '    TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.trim()
                        '    ' End If
                        'End If
                        'added by sagar as discussed with yogesh no need for below code
                        'If clsDefaultConfiguration.JkTicketingSystem Then
                        '    If RaisedBy = "Fo" Then
                        '        'If OldStatus = "Resolved" Then
                        '        '    If cbStatus.Text = OldStatus Or NewStatus Is Nothing AndAlso TxtRemarks <> "" Then
                        '        '        'ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '        '        'Exit Sub
                        '        '        status = "Re-opened"
                        '        '    End If
                        '        'End If
                        '        If OldStatus = "Resolved" Then
                        '            If cbStatus.Text = OldStatus Or NewStatus Is Nothing Then
                        '                If clsDefaultConfiguration.RenderGrievance = True Then
                        '                    If TxtRemarks <> "" Then
                        '                        status = "Re-opened"
                        '                        doEntryForSms = True
                        '                    Else
                        '                        ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '                        Exit Sub
                        '                    End If
                        '                Else
                        '                    ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '                    Exit Sub
                        '                End If
                        '            End If
                        '        End If
                        '    Else
                        '        If RaisedBy = "Bo" Then
                        '            'If clsDefaultConfiguration.RenderGrievance = True Then
                        '            '    If OldStatus = "Resolved" Then
                        '            '        If cbStatus.Text = OldStatus Or NewStatus Is Nothing AndAlso TxtRemarks <> "" Then
                        '            '            'ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '            '            'Exit Sub
                        '            '            status = "Re-opened"
                        '            '        End If
                        '            '    End If
                        '            'End If
                        '            If OldStatus = "Resolved" Then
                        '                If cbStatus.Text = OldStatus Or NewStatus Is Nothing Then
                        '                    If clsDefaultConfiguration.RenderGrievance = True Then
                        '                        If TxtRemarks <> "" Then
                        '                            status = "Re-opened"
                        '                            doEntryForSms = True
                        '                        Else
                        '                            ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '                            Exit Sub
                        '                        End If
                        '                    Else
                        '                        ShowMessage(getValueByKey("GRV015"), "GRV015 - " & getValueByKey("GRV010"))
                        '                        Exit Sub
                        '                    End If
                        '                End If
                        '            End If
                        '        End If

                        '    End If
                        'End If
                        If IsEdit = False Then
                            'If cbDepartment.SelectedIndex = -1 Then
                            If ListViewShowRaisedDept.Items.Count = 0 Then
                                ShowMessage(getValueByKey("GRV003"), "GRV003 - " & getValueByKey("GRV010"))
                                ' ShowMessage("Please Select Department", "" & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                            If clsDefaultConfiguration.JkTicketingSystem Then
                                'cbGrievanceType.SelectedValue = 5
                                cbGrievanceType.Text = "Others"
                            Else
                                If cbGrievanceType.SelectedIndex = -1 Then
                                    ShowMessage(getValueByKey("GRV004"), "GRV004 - " & getValueByKey("GRV010"))
                                    'ShowMessage("Please Select Grievance Type", "" & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If
                            End If
                            If clsDefaultConfiguration.JkTicketingSystem Then
                            Else
                                If txtGrievanceTitle.Text.Trim() = String.Empty Then
                                    ShowMessage(getValueByKey("GRV005"), "GRV005 - " & getValueByKey("GRV010"))
                                    'ShowMessage("Please Enter Grievance Title", "" & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If
                            End If
                            If txtGrievanceDetail.Text.Trim().Replace("'", "") = String.Empty Then
                                ShowMessage(getValueByKey("GRV006"), "GRV006 - " & getValueByKey("GRV010"))
                                'ShowMessage("Please Enter Grievance Details", "" & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        Else
                            If clsDefaultConfiguration.JkTicketingSystem Then
                                cbGrievanceType.Text = "Others"
                            Else
                                If cbGrievanceType.SelectedIndex = -1 Then
                                    ShowMessage(getValueByKey("GRV007"), "GRV007 - " & getValueByKey("GRV010"))
                                    'ShowMessage("Please Select Grievance Type", "" & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If
                            End If
                        End If
                        If cbStatus.SelectedText = "" Then
                        Else
                            status = cbStatus.SelectedText
                        End If
                        'TxtRemarks.ToString().Replace("'", "''")

                        If TxtRemarks <> Nothing Then
                            If TxtRemarks.ToString.Length > 1000 Then
                                ShowMessage("Maximum character length exceed", "")
                                Exit Sub
                            End If
                        Else
                            TxtRemarks = ""
                        End If
                        '--- Ticker issue in case of updatedOn :i.e. will not update ticket if no changes done by user .
                        If IsEdit Then
                            If OldDept <> NewDept Then
                                _isGrievanceHistoryChange = True
                                Dim OldDeptName As String = objCls.GetDepartmentById(OldDept).ToString()
                                Dim NewDeptName As String = objCls.GetDepartmentById(NewDept).ToString()
                                If TxtRemarks <> "" Then
                                    Dim newRemarkValue As String = ""
                                    If strLastRemarkIdForGrvHistory <> "" Then
                                        newRemarkValue = strLastRemarkIdForGrvHistory
                                        Dim newRemarkValueArray() As String
                                        newRemarkValueArray = newRemarkValue.Split(" "c)
                                        Dim adjust As String = (newRemarkValueArray(1) + newRemarkValueArray(2) + newRemarkValueArray(3) + newRemarkValueArray(4)) + 1
                                        Dim bb As String = adjust.Trim()
                                        Dim OBJECTForNewRemark As String = "Remark   " + bb + "  (F- " + clsAdmin.UserName + ") " + newRemarkValueArray(7) + " " + newRemarkValueArray(8) + " :" + clsAdmin.CurrentDate.ToString("MM/dd/yyyy HH:mm:ss")
                                        GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & OBJECTForNewRemark & " - " & MobileNumberListSaveWithHistory & ""    ''SMS on no.
                                    Else
                                        Dim OBJECTForNewRemark As String = "Remark   1  (F- " + clsAdmin.UserName + ") Changed on :" + clsAdmin.CurrentDate.ToString("MM/dd/yyyy HH:mm:ss")
                                        GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & OBJECTForNewRemark & " - " & MobileNumberListSaveWithHistory & ""
                                    End If
                                    'Remark   4  (F- admin) 18-07-2016 04:37 PM
                                Else
                                    If strLastRemarkIdForGrvHistory = "" Then
                                        GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " by : " & clsAdmin.UserCode & " - " & MobileNumberListSaveWithHistory & ""
                                    Else
                                        GrievanceHistoryText = "Raised To change from : " & OldDeptName & " to " & NewDeptName & " After: " & strLastRemarkIdForGrvHistory & " - " & MobileNumberListSaveWithHistory & ""
                                    End If
                                End If
                                'GrievanceHistoryText = "Department change from : " & OldDeptName & " to " & NewDeptName & " by : " & clsAdmin.UserCode & ""
                                'Department change from : Business Development & Legal to Accounts by : Admin
                                If CtrlLblSMSNo.Text <> "" Then
                                    doEntryForSms = True
                                End If
                            End If
                        End If
                        If OldDept = NewDept And NewStatus = OldStatus And String.IsNullOrEmpty(TxtRemarks.Trim()) Then
                            If clsDefaultConfiguration.JkTicketingSystem Then
                                If RaisedBy = "Bo" Then
                                    ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                                Else
                                    ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                                End If
                                ' ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                                'ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
                            Else
                                ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                                'ShowMessage(False, String.Format(getValueByKey("GRV008"), lblId.Text), getValueByKey("GRV010"))
                                'ShowMessage(getValueByKey("GRV008"), "GRV008 - " & getValueByKey("GRV010"))
                            End If
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                            Exit Sub
                        End If
                        If cbGrievanceType.SelectedValue = Nothing Then
                            ShowMessage((String.Format("{0} department not mapped. Please Contact to System Admin", cbDepartment.SelectedText)), getValueByKey("GRV010"))
                            Exit Sub
                        End If
                        Dim IsReopenHistoryEntery As Boolean = False
                        If IsEdit = True AndAlso OldStatus <> NewStatus AndAlso NewStatus = "Re-opened" Then
                            IsReopenHistoryEntery = True
                        End If
                        'added by sagar
                        If OldDept <> NewDept Or NewStatus <> OldStatus Or Not String.IsNullOrEmpty(TxtRemarks.Trim()) Then
                            IsBoldApplicable = True
                        End If
                        Dim docno As String = objCls.getDocumentNo("Grievance", clsAdmin.SiteCode)
                        'commented by khusrao adil on 02-11-2017 for jk sprint 31
                        'docno = GenDocNo("GCF" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 14, docno)
                        'added by khusrao adil on 02-11-2017 for jk sprint 31
                        ' docno = GenDocNo("GCF" & clsAdmin.LegacySiteCode.Substring(clsAdmin.LegacySiteCode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 3, 3), 14, docno)
                        docno = GenDocNo("GC" & clsAdmin.SiteStdCode & strFinyear.Substring(strFinyear.Length - 2, 2), 13, docno)

                        If objGrievance.SaveGrivanceDetail(Sitecode:=clsAdmin.SiteCode, SiteStdCode:=clsAdmin.SiteStdCode, Grievanceid:=docno, status:=status,
                                grievanceTypeid:=cbGrievanceType.SelectedValue, departmentid:=DepId,
                                title:=txtGrievanceTitle.Text.Replace("'", ""), details:=txtGrievanceDetail.Text.Replace("'", ""),
                                userid:=clsAdmin.UserCode, EditMode:=IsEdit, Remarks:=TxtRemarks.ToString.Replace("'", "").Trim, strFinyear:=strFinyear,
                                isGrievanceHistoryChange:=_isGrievanceHistoryChange, GrievanceHistoryText:=GrievanceHistoryText,
                                RaisedFromSite:=RaisedFromSite, IsRaisedFromSite:=IsRaisedFromSite, RaisedToDepartment:=RaisedToDepartment,
                                IsRaisedToSite:=IsRaisedToSite, CCSite:=CCSite, CCDepartment:=CCDepartment, IsCCSite:=IsCCSite, AssignedSiteCode:=AssignedSiteCode,
                                dayopendate:=clsAdmin.DayOpenDate, RaisedBy:=RaisedBy, doEntryForSms:=doEntryForSms, fileName:=FullNameWithExtension,
                                MoblileList:=CtrlLblSMSNo.Text, IsReopenHistoryEntery:=IsReopenHistoryEntery, IsBoldApplicable:=IsBoldApplicable,
                                dtMobileNumbers:=dtMobileNumbers) Then

                            'If objGrievance.SaveGrivanceDetail(clsAdmin.SiteCode, docno, status, cbGrievanceType.SelectedValue, DepId, txtGrievanceTitle.Text.Replace("'", ""), txtGrievanceDetail.Text.Replace("'", ""),
                            '                                 clsAdmin.UserCode, IsEdit, TxtRemarks.ToString.Replace("'", "").Trim, strFinyear, _isGrievanceHistoryChange, GrievanceHistoryText,
                            '                                RaisedFromSite, IsRaisedFromSite, RaisedToDepartment, IsRaisedToSite, CCSite, CCDepartment, IsCCSite, AssignedSiteCode,
                            '                                clsAdmin.DayOpenDate, RaisedBy, doEntryForSms, FullNameWithExtension, CtrlLblSMSNo.Text, IsReopenHistoryEntery, IsBoldApplicable, dtMobileNumbers:=dtMobileNumbers) Then
                            '    ' If objGrievance.SaveGrivanceDetail(clsAdmin.SiteCode, lblId.Text, status, cbGrievanceType.SelectedValue, cbDepartment.SelectedValue, txtGrievanceTitle.Text.Replace("'", ""), txtGrievanceDetail.Text.Replace("'", ""), clsAdmin.UserCode, IsEdit, TxtRemarks.ToString.Replace("'", "").Trim, strFinyear, _isGrievanceHistoryChange, GrievanceHistoryText, clsAdmin.DayOpenDate, RaisedBy, doEntryForSms, FullNameWithExtension, CtrlLblSMSNo.Text, IsReopenHistoryEntery, IsBoldApplicable) Then
                            If TxtRemarks <> "" AndAlso IsFileUpload = True Then
                                CreateDirectoryandCopyFile(objGrievance.GrievanceRemarkIdForAttachement)
                                objGrievance.GrievanceRemarkIdForAttachement = ""
                            End If
                        Else
                            ShowMessage(getValueByKey("GRV011"), "GRV011 - " & getValueByKey("GRV010"))
                        End If

                        'ShowMessage("Error In Saving Grievance Details", " " & getValueByKey("CLAE04"))
                    Next
                    showSaveMessage()
                End If

                If ListViewShowRaisedDept.Items.Count = 0 Then
                    ShowMessage(getValueByKey("GRV003") & " /Site", "GRV003 - " & getValueByKey("GRV010"))
                    ' ShowMessage("Please Select Department", "" & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'code added for jk sprint 24 by vipul
    Private Function ADDCMFValueAtEditTime()
        Try

            Dim CMFIDRaiesdTo As String
            Dim dtCMFIdForRaiesdTo As DataTable

            If Not RbRaisedDept.Checked Then

                Dim SiteCodeforCMF = cbDepartment.SelectedValue
                'grabbing the CMF 
                dtCMFIdForRaiesdTo = objGrievance.Togetsite_cmfid(SiteCodeforCMF)

                Dim oldsitename = OldDeptName
                Dim currentsitename = cbDepartment.Text.ToString
                If oldsitename = currentsitename Then

                Else

                    If dtCMFIdForRaiesdTo.Rows.Count > 0 Then
                        CMFIDRaiesdTo = dtCMFIdForRaiesdTo.Rows(0)("SQFTArea")
                        BindToListBoxOnCMF(CMFIDRaiesdTo)
                    End If
                    If dtCMFForRaiesdFromOnEdit IsNot Nothing Then
                        If dtCMFForRaiesdFromOnEdit.Rows.Count > 0 Then
                            CMFIDRaiesdTo = dtCMFForRaiesdFromOnEdit.Rows(0)("SQFTArea")
                            ' BindToListBox(CMFIDRaiesdTo)
                            BindToListBoxOnCMF(CMFIDRaiesdTo)
                            ' CMFIDRaisedFrom
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub showSaveMessage()
        If IsEdit = True Then
            If clsDefaultConfiguration.JkTicketingSystem Then
                If RaisedBy = "Bo" Then
                    ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                Else
                    ShowMessage((String.Format(getValueByKey("GRV014"), lblId.Text)), getValueByKey("GRV010"))
                End If

                'ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
            Else
                ShowMessage((String.Format(getValueByKey("GRV008"), lblId.Text)), getValueByKey("GRV010"))
                ' ShowMessage(False, String.Format(getValueByKey("GRV008"), lblId.Text, "GRV008 - " & getValueByKey("GRV010")))
                'ShowMessage(getValueByKey("GRV008"), "GRV008 - " & getValueByKey("GRV010"))
            End If

            ' ShowMessage("Grievance Updated Successfully ", " " & getValueByKey("CLAE04"))
        Else
            If clsDefaultConfiguration.JkTicketingSystem Then
                ShowMessage((String.Format(getValueByKey("GRV014"), "")), getValueByKey("GRV010"))
                ' ShowMessage(getValueByKey("GRV014"), "GRV014 - " & getValueByKey("GRV010"))
            Else
                ShowMessage((String.Format(getValueByKey("GRV009"), lblId.Text)), getValueByKey("GRV010"))
                'ShowMessage(getValueByKey("GRV009"), "GRV009 - " & getValueByKey("GRV010"))
            End If

            'ShowMessage("Grievance Saved Successfully", " " & getValueByKey("CLAE04"))
            'Reset()
        End If
    End Sub
    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Dim eventType As Int32
        ' Dim result = MessageBox.Show(getValueByKey("GRV013"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.None)
        If IsTicketUpdated Then
            ShowMessage(getValueByKey("GRV013"), "CM014 - " & getValueByKey("CLAE04"), eventType, "Cancel", "OK")
        Else
            Me.Close()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        If eventType = 1 Then
            Me.Close()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
        End If

    End Sub
    Private Function Reset() As Boolean
        cbStatus.SelectedIndex = -1
        cbDepartment.SelectedIndex = -1
        cbGrievanceType.SelectedIndex = -1
        txtGrievanceTitle.Text = String.Empty
        txtGrievanceDetail.Text = String.Empty
        'lblId.Text = objCls.GetNextGrievanceId()
    End Function

    Private Sub cbDepartment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbDepartment.SelectedValueChanged
        NewDept = cbDepartment.SelectedValue
        Dim deptid As String = ""
        If Not cbDepartment.SelectedValue Is Nothing Then
            deptid = cbDepartment.SelectedValue.ToString()
        End If
        If Not RbRaisedDept.Checked Then
            deptid = 22
        End If
        Dim deptBox = objCls.GetSelectedDepartment(deptid)
        PopulateComboBox(deptBox, cbGrievanceType)
        pC1ComboSetDisplayMember(cbGrievanceType)
        If OldDept <> NewDept Then
            IsTicketUpdated = True
        End If
        '' Added By ketan display Mobile No Departmet wise JK Sprint 19
        If Not IsEdit Then
            'BindMobileNo(deptid)
        End If
    End Sub

    Private Function BindMobileNo(Optional ByVal deptid As String = "")
        Try
            Dim MobNo As String = ""
            If IsEdit Then
                Dim DeptMobNo = objCls.GetDepartmentMobileList(lblId.Text, clsAdmin.SiteCode)
                If DeptMobNo.Rows.Count > 0 Then

                    MobNo = DeptMobNo.Rows(0)(0).ToString
                    CtrlLblSMSNo.Text = MobNo
                    tooltip1.SetToolTip(CtrlLblSMSNo, CtrlLblSMSNo.Text)
                Else
                    CtrlLblSMSNo.Text = ""
                End If
            Else
                Dim DeptMobNo As DataTable
                If RbRaisedDept.Checked Then
                    DeptMobNo = objCls.GetDepartmentMobileNumberByDepartmentId(deptid)
                Else
                    DeptMobNo = objCls.GetMobileNumberBySiteCode(deptid) '' Site MobileNo
                End If

                If DeptMobNo.Rows.Count > 0 Then
                    For MobileRow = 0 To DeptMobNo.Rows.Count - 1
                        MobNo = MobNo + "," + DeptMobNo.Rows(MobileRow)(0).ToString
                    Next
                    MobNo = MobNo.Remove(0, 1)
                    CtrlLblSMSNo.Text = MobNo
                Else
                    CtrlLblSMSNo.Text = ""
                End If
            End If
            Return MobNo
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Dim btnFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim btnLoc As New System.Drawing.Point(5, 3)
    Dim btnPadding As New Padding(0, 0, 0, 0)
    Dim lblFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim lb As Label
    Dim lb1 As Label
    Dim pn As TableLayoutPanel
    Dim txt As Spectrum.CtrlTextBox
    Dim btnNewTicketAttachment As New LinkLabel
    Dim btnEditTicketAttachment As New LinkLabel
    Dim isEditAttachementOpenOnce As Boolean = False
    Dim LastCounterForRemarkIdForGrvHistory As Integer = 0
    Private Sub AddattachedLabel(Optional ByVal count As Integer = 0, Optional Remark As String = "", Optional ByVal SiteAndUser As String = "", Optional ByVal CreatedTime As DateTime = Nothing, Optional ByVal RemarkId As String = "")
        Try
            Dim btn As New LinkLabel
            btn.Size = New Size(150, 21)
            Dim GriRemarkFileName As String
            If RemarkId <> "" Then
                GriRemarkFileName = objComn.getGrievanceFileName(RemarkId)
            End If
            If count = 0 Then
                btn.Text = "Attach File"
                btnNewTicketAttachment.Name = "btnNewTicketAttachment"
                AddHandler btnNewTicketAttachment.Click, AddressOf show_document 'show_document
                btnNewTicketAttachment.Size = New Size(200, 21)
                btnNewTicketAttachment.Visible = False
                AddHandler btn.Click, AddressOf UploadFile_Click
            ElseIf (count >= 1 AndAlso GriRemarkFileName <> "") Then
                btn.Text = "Att'd File" + "(" + "Remark" + count.ToString() + ")"
                btn.Tag = RemarkId
                AddHandler btn.Click, AddressOf Download_Click
            End If
            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            pn.AutoScroll = False
            pn.MaximumSize = New System.Drawing.Size(900, 900)
            pn.RowCount = 2
            pn.ColumnCount = 2
            pn.Controls.Add(btn, 1, 1)
            pn.Controls.Add(btnNewTicketAttachment, 1, 2)
            fpRemarks.Controls.Add(pn)
            fpRemarks.MinimumSize = New Size(900, 225)
            fpRemarks.MaximumSize = New Size(900, 225)
            fpRemarks.Size = New Size(900, 225)
            pn.ResumeLayout()
            IsTexBoxRemarkAdded = True
            If (status = "Broadcast") Then
                If count = 0 Then
                    txt.ReadOnly = True
                    btn.Enabled = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub AddRemark(Optional ByVal count As Integer = 0, Optional Remark As String = "", Optional ByVal SiteAndUser As String = "", Optional ByVal CreatedTime As DateTime = Nothing, Optional ByVal RemarkId As String = "")
        Try
            lb = New Label()
            lb.AutoSize = True
            lb.MaximumSize = New Size(500, 221)
            lb.MinimumSize = New Size(106, 15)
            'lb.Size = New Size(100, 21)

            'lb.Margin = New Padding(1)
            Dim tip As ToolTip = New ToolTip()
            'If count = 0 Then
            '    lb.Text = "Remarks   " & count & " " + Site + "  " + CreatedTime
            '    lb.Padding = New Padding(0, 0, 3, 10)
            'Else
            '    'lb.Text = "Remarks '& " & count + Site + ":" + CreatedTime  '& " " & count
            '    lb.Text = "Remarks   " & count & " " + Site + "  " + CreatedTime
            '    lb.Padding = New Padding(0, 0, 3, 10)
            'End If
            'lb.Name = "Remarks " + count + Site + ":" + CreatedTime '& count

            Dim _rrmktemp As String = ""
            If count = 0 Then
                lb.Text = "Remark "
            ElseIf String.IsNullOrEmpty(SiteAndUser) Then
                lb.Text = "Remark   " & count & ""
            Else

                lb.Text = "Remark   " & count & "  " + SiteAndUser + " " + CreatedTime.ToString("MM/dd/yyyy HH:mm:ss")
                _rrmktemp = "Remark   " & count & "  " + SiteAndUser + " Changed on :" + CreatedTime.ToString("MM/dd/yyyy HH:mm:ss")
            End If

            TempStrLastRemarkIdForGrvHistory = _rrmktemp
            lb.Padding = New Padding(0, 10, 0, 4)
            lb.TextAlign = ContentAlignment.TopLeft
            'lb.Dock = DockStyle.Fill
            'lb.Font = New Font("Arial",10)

            txt = New Spectrum.CtrlTextBox
            txt.Multiline = True

            'If count = 0 Then
            '    txt.Margin = New Padding(0, 4, 0, 2)
            'End If



            'txt.MaximumSize = New System.Drawing.Size(800, 21)
            'txt.MinimumSize = New System.Drawing.Size(800, 21)
            txt.Name = "txtAlternate" & count
            'txt.Size = New System.Drawing.Size(830, 21)
            txt.Size = New System.Drawing.Size(760, 21)

            txt.Dock = DockStyle.Fill
            txt.Text = Remark
            txt.MaxLength = 1000

            Dim size As Size = TextRenderer.MeasureText(Remark, txt.Font)
            Dim factor As Integer = Math.Ceiling(size.Width / txt.Width)
            ''txt.Width = size.Width
            'txt.Height = txt.Height + ((factor - 1) * size.Height)

            If 5 + size.Height > txt.Height + ((factor - 1) * size.Height) Then
                txt.Height = 6 + size.Height

            Else
                txt.Height = txt.Height + ((factor - 1) * size.Height)

            End If

            txt.Margin = New Padding(5, 4, 0, 0)
            Dim btn As New LinkLabel

            btn.Size = New Size(150, 21)
            'code added by irfan for jk sprint 28
            btnEditTicketAttachment.Size = New Size(300, 30)
            Dim GriRemarkFileName As String
            If RemarkId <> "" Then
                GriRemarkFileName = objComn.getGrievanceFileName(RemarkId)
            End If

            If count = 0 Then
                btn.Text = "Attach File"
                AddHandler btn.Click, AddressOf UploadFile_Click


            ElseIf (count >= 1 AndAlso GriRemarkFileName <> "") Then
                'btn.Text = "AttachRemark" + count.ToString()

                btn.Text = "Att'd File" + "(" + "Remark" + count.ToString() + ")"

                btn.Tag = RemarkId


                AddHandler btn.Click, AddressOf Download_Click
                'Else

                '    btn.Text = "Upload File"
                '    AddHandler btn.Click, AddressOf UploadFile_Click
            End If
            'code added by irfan for jk sprint 28
            If count = 0 Then

                btnEditTicketAttachment.ForeColor = Color.Black
            End If
            txt.TextDetached = True

            AddHandler txt.TextChanged, AddressOf txt_TextChanged
            AddHandler txt.KeyPress, AddressOf txt_KeyPress
            If Remark = "" Then
                txt.Enabled = True
                btn.Enabled = True
            Else
                toolTip = New ToolTip
                txt.Enabled = True
                txt.ReadOnly = True

                'toolTip.InitialDelay = 1000
                'toolTip.ReshowDelay = 500
                'toolTip.SetToolTip(txt, txt.Text)
                'toolTip.Active = True
                'toolTip.AutoPopDelay = 90000
                'toolTip.ShowAlways = True
                'txt.Visible = False
            End If

            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            pn.AutoScroll = False
            'pn.MaximumSize = New System.Drawing.Size(840, 900)
            pn.MaximumSize = New System.Drawing.Size(900, 900)
            pn.RowCount = 2
            ' pn.ColumnCount = 1
            pn.ColumnCount = 2
            'pn.SetColumnSpan(lb, 2)
            pn.Controls.Add(lb, 0, 0)
            pn.Controls.Add(txt, 0, 1)
            pn.Controls.Add(txt, 0, 1)
            pn.Controls.Add(btn, 1, 1)
            'code added by irfan for jk sprint 28
            pn.Controls.Add(btnEditTicketAttachment, 1, 2)



            fpRemarks.Controls.Add(pn)
            fpRemarks.MinimumSize = New Size(900, 225)
            fpRemarks.MaximumSize = New Size(900, 225)
            fpRemarks.Size = New Size(900, 225)
            pn.ResumeLayout()
            IsTexBoxRemarkAdded = True
            If (status = "Broadcast") Then
                If count = 0 Then
                    txt.ReadOnly = True
                    btn.Enabled = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'code added by irfan for jk sprint 28
    Public Sub show_document(sender As Object, e As EventArgs)
        If sender.Text <> "" Then
            If RecentUploadFilePath <> "" Then
                Dim Proc As New System.Diagnostics.Process
                Proc.StartInfo.FileName = RecentUploadFilePath
                Proc.Start()
            End If

        End If
    End Sub
    Public Sub Edit_show_document(sender As Object, e As EventArgs)
        If sender.Text <> "" Then
            If RecentUploadFilePath <> "" Then
                Dim Proc As New System.Diagnostics.Process
                Proc.StartInfo.FileName = RecentUploadFilePath
                Proc.Start()
            End If
        End If
    End Sub
    Private Sub UploadFile_Click(sender As Object, e As EventArgs)
        OFileDialog = New OpenFileDialog()

        OFileDialog.Filter = filterImage '"All files (*.*)|*.*"
        OFileDialog.FilterIndex = 1
        'Dim created As String = lblStatus.
        Dim GrievanceId As String = lblId.Text

        If (OFileDialog.ShowDialog() = DialogResult.OK) Then
            '  If (iFileSize > OFileDialog.FileName.Length) Then  
            fileLocation = OFileDialog.FileName
            Dim file() As String
            'If file.Length > 0 Then
            file = fileLocation.Split(".")
            'End If
            Dim FileInfo As New FileInfo(fileLocation)
            Dim fileinkb = CInt(FileInfo.Length / 1024)  '1MB approx (actually less though)
            ' If clsDefaultConfiguration.TicketingAttachment Then
            Dim filelength = clsDefaultConfiguration.TicketingAttachment
            If fileLocation.ToLower().Contains(".exe") Or fileLocation.ToLower().Contains(".bat") Or fileLocation.ToLower().Contains(".dll") Then
                ShowMessage("File format cannot be supported.", "GRV018 - " & "File Information")
                'ShowMessage("The file  cannot be uploaded. Files with an content Type" + "  " + "." + file(1) + " are not allowed.", "GRV018 - " & "File Information")
            Else

                If fileinkb <= filelength Then
                    Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(fileLocation)
                    Dim extension As String = System.IO.Path.GetExtension(fileLocation)
                    FullNameWithExtension = fileName + extension
                    'code added by irfan for jk sprint 28

                    RecentUploadFilePath = fileLocation
                 
                    btnNewTicketAttachment.Text = FullNameWithExtension
                    btnNewTicketAttachment.Visible = True
                    btnEditTicketAttachment.Visible = False

                    btnEditTicketAttachment.Text = FullNameWithExtension
                    btnEditTicketAttachment.Visible = True
                 
                  
                    IsFileUpload = True
                Else
                    ShowMessage("File size should be less than " & clsDefaultConfiguration.TicketingAttachment & "KB ", "GRV018 - " & "File Information")
                End If
            End If
            'ElseIf (OFileDialog.ShowDialog() = DialogResult.Cancel) Then

            OFileDialog.Dispose()
            ' End If
        End If
    End Sub


    '' added by nikhil
    Sub CreateDirectoryandCopyFile(ByVal GrievanceRemarkId As String)
        Try
            Dim Targetpath As String = clsDefaultConfiguration.GrievanceRemarkAttachment
            If Targetpath = "" Or Targetpath = Nothing Then
                If Directory.Exists(Targetpath) = False Then
                    Directory.CreateDirectory(Targetpath)
                End If
            End If
            folder = GrievanceRemarkId
            TargetFolderPath = Targetpath + "\" + folder
            If Directory.Exists(TargetFolderPath) = False Then
                Directory.CreateDirectory(TargetFolderPath)
            End If
            File.Copy(fileLocation, Path.Combine(TargetFolderPath, Path.GetFileName(FullNameWithExtension)), True)

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub Download_Click(sender As Object, e As EventArgs)
        Try


            Dim GrievRemarkId As String = sender.tag

            'GrievanceId = sender.tag
            Dim AttachmentFileName As String = objComn.getGrievanceFileName(GrievRemarkId)

            ' Dim fullPath As String = clsDefaultConfiguration.GrievanceRemarkAttachment + "\" + GrievanceId + "_" + GrievRemarkId + "\" + AttachmentFileName
            Dim fullPath As String = clsDefaultConfiguration.GrievanceRemarkAttachment + "\" + GrievRemarkId + "\" + AttachmentFileName
            If Not fullPath Is Nothing AndAlso fullPath.Contains(AttachmentFileName) Then
                If AttachmentFileName <> "" Then
                    Process.Start(fullPath)
                Else
                    ShowMessage("File is Not Available", "GRV018 - " & "File Information")
                End If
            Else
                ShowMessage("File is Not Available", "GRV018 - " & "File Information")
                'btn.Text = "Upload File"
            End If

            ' Else
            'ShowMessage("File is Not Available", "GRV018 - " & "File Information")

            ' End If

            'If filePath Then
            'Using fs As FileStream = File.Create(filePath)
            '    '' fs.Write(mybytes, 0, mybytes.Length)
            'End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        OpenFileDialog1.ShowDialog()
        'CtrllblFileName.Visible = True
        lblFile.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub lblFile_DoubleClick(sender As Object, e As EventArgs) Handles lblFile.DoubleClick
        'MessageBox.Show(OpenFileDialog1.FileName.ToString())
        Process.Start(OpenFileDialog1.FileName.ToString())
        'Dim chkExtention() As String = OpenFileDialog1.FileName.ToString().Split(".")

        'If chkExtention.Length > 0 Then
        '    For t As Integer = 0 To CInt(chkExtention.Length) - 1
        '        If chkExtention(t) = "jpg" Or chkExtention(t) = "png" Then
        '            Process.Start(OpenFileDialog1.FileName.ToString())
        '            'ShowImage()
        '        End If
        '    Next
        'End If

    End Sub
    Dim height As Integer = 21
    Dim pretxthight As Integer = 0

    Private Sub txt_TextChanged(sender As Object, e As EventArgs)
        Try
            sender.suspendlayout()
            Dim size As Size = TextRenderer.MeasureText(sender.Text, sender.Font)
            Dim factor As Integer = Math.Ceiling(size.Width / sender.Width)
            ' sender.Width = size.Width

            Dim currentTxtHight As Integer = 0
            If height + ((factor - 1) * size.Height) > 6 + size.Height Then
                currentTxtHight = height + ((factor - 1) * size.Height)
            Else
                currentTxtHight = 1 + size.Height + 10
            End If
            ' sender.Height = height + ((factor - 1) * size.Height)
            If currentTxtHight <> pretxthight Then
                sender.Height = currentTxtHight
                pretxthight = currentTxtHight
            End If

            sender.Multiline = True
            sender.resumelayout()
            IsTicketUpdated = True
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub txt_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
        'code added by vipul for jk sprint 28
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txt.Focus()
            txt.SelectionStart = txt.Text.Length
        End If
    End Sub
    Private Sub txtGrievanceDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGrievanceDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtGrievanceDetail.Focus()
            txtGrievanceDetail.SelectionStart = txtGrievanceDetail.Text.Length
        End If
    End Sub

    Private Sub txtGrievanceDetail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrievanceDetail.KeyPress
        IsTicketUpdated = True
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub cbStatus_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbStatus.SelectedValueChanged
        NewStatus = cbStatus.SelectedText
        If Not cbStatus.SelectedText = "New" Then
            If Not IsTexBoxRemarkAdded Then
                AddRemark(, "", "(F)")
            End If
            If fpRemarks.Controls.Count > 2 Then
                RdAsc.Visible = True
                RdDesc.Visible = True
            Else
                RdAsc.Visible = False
                RdDesc.Visible = False
            End If

            ' IsTexBoxRemarkAdded = True
            'Else
            '    IsTexBoxRemarkAdded = False
            '    fpRemarks.Controls.Clear()
            '    RdAsc.Visible = False
            '    RdDesc.Visible = False
            'End If
            'If cbStatus.SelectedValue <> NewStatus Then
            '    IsTicketUpdated = True
        End If
    End Sub

    Private Sub RdAsc_CheckedChanged(sender As Object, e As EventArgs) Handles RdAsc.CheckedChanged
        If IsEdit Then
            Orderby = "Asc"
            AddRemarkPanel()
            'If Not status = "New" Then
            strLastRemarkIdForGrvHistory = TempStrLastRemarkIdForGrvHistory
            TempStrLastRemarkIdForGrvHistory = ""
            AddRemark(, "", "(F)")
            If fpRemarks.Controls.Count <> 1 Then
                RdAsc.Visible = True
                RdDesc.Visible = True
            Else
                RdAsc.Visible = False
                RdDesc.Visible = False
            End If
            'Else
            'RdAsc.Visible = False
            'RdDesc.Visible = False
            ' End If
            'If Not status = "New" AndAlso fpRemarks.Controls.Count > 1 Then

            'Else
        Else
            AddattachedLabel(, "", "(F)")
        End If

    End Sub

    Private Sub RdDesc_CheckedChanged(sender As Object, e As EventArgs) Handles RdDesc.CheckedChanged
        Orderby = "Desc"
        AddRemarkPanel()
    End Sub


    Private Function AddRemarkPanel()

        fpRemarks.Controls.Clear()
        Dim dt As DataTable
        Dim OldRemarks() As String
        Dim DtRemarks As New DataTable



        dt = objCls.GetGrievanceDetailIdWise(clsAdmin.SiteCode, GrievanceId)
        If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then

            Dim Remarks As String = dt.Rows(0)("Remark").ToString
            If Not Remarks Is Nothing Then
                OldRemarks = Remarks.ToString.Split(New String() {"@~&"}, StringSplitOptions.None)
            End If
            Dim count As Integer = 1
            'If Not dt.Rows(0)("GrievanceStatus") = "New" Then
            If RdDesc.Checked = True Then
                AddRemark(, "", "(F)")
                IsTexBoxRemarkAdded = True
            End If
            'End If



            DtRemarks = objGrievance.GetGrievanceRemarks(GrievanceId, OrderBy:=Orderby)
            Dim siteAndUser As String = ""
            If Not DtRemarks Is Nothing AndAlso DtRemarks.Rows.Count > 0 Then

                If OldRemarks.Length > 0 Then
                    For Each item As String In OldRemarks
                        Dim dr As DataRow = DtRemarks.NewRow
                        dr("Remark") = item
                        dr("RemarksSiteCode") = ""
                        If Not item = String.Empty Then
                            DtRemarks.Rows.InsertAt(dr, DtRemarks.Rows.Count)
                        End If


                    Next

                End If
                Dim counter As Integer
                If Orderby = "Asc" Then
                    counter = 1
                ElseIf Orderby = "Desc" Then
                    counter = CInt(DtRemarks.Rows.Count)
                End If


                For T As Integer = 0 To CInt(DtRemarks.Rows.Count - 1)
                    If clsAdmin.SiteCode = DtRemarks.Rows(T)("RemarksSiteCode") Then
                        siteAndUser = "(F- " & DtRemarks.Rows(T)("UserName") & ")"
                    Else
                        siteAndUser = "(HO- " & DtRemarks.Rows(T)("UserName") & ")"
                    End If


                    'If Not dt.Rows(0)("GrievanceStatus") = "New" Then
                    If String.IsNullOrEmpty(DtRemarks.Rows(T)("RemarksSiteCode").ToString) = True Then
                        'AddRemark(counter, DtRemarks.Rows(T)("Remark"), "", Nothing)
                        AddRemark(counter, DtRemarks.Rows(T)("Remark"), "", DtRemarks.Rows(T)("RemarkId"))
                    Else
                        'AddRemark(counter, DtRemarks.Rows(T)("Remark"), siteAndUser, DtRemarks.Rows(T)("CreateTime"))
                        AddRemark(counter, DtRemarks.Rows(T)("Remark"), siteAndUser, DtRemarks.Rows(T)("CreateTime"), DtRemarks.Rows(T)("RemarkId"))
                    End If
                    'End If
                    If Orderby = "Asc" Then
                        counter = counter + 1
                    ElseIf Orderby = "Desc" Then
                        counter = counter - 1
                    End If

                Next
            Else
                If OldRemarks.Length > 0 Then

                    For Each item As String In OldRemarks

                        Dim dr As DataRow = DtRemarks.NewRow
                        dr("Remark") = item
                        dr("RemarksSiteCode") = ""

                        If Not item = String.Empty Then
                            DtRemarks.Rows.InsertAt(dr, DtRemarks.Rows.Count)
                        End If
                    Next


                    For T As Integer = 0 To CInt(DtRemarks.Rows.Count - 1)
                        ' If Not dt.Rows(0)("GrievanceStatus") = "New" Then
                        If String.IsNullOrEmpty(DtRemarks.Rows(T)("RemarksSiteCode").ToString()) = True Then
                            ' AddRemark("0", DtRemarks.Rows(T)("Remark").ToString(), "", Nothing)
                            AddRemark("0", DtRemarks.Rows(T)("Remark").ToString(), "", DtRemarks.Rows(T)("RemarkId"))
                        End If
                        'End If
                    Next

                End If
            End If
        End If
    End Function

    Private Sub txtGrievanceTitle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrievanceTitle.KeyPress
        IsTicketUpdated = True
    End Sub

    Private Sub cbGrievanceType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbGrievanceType.SelectedValueChanged
        Dim newTicketType As String = cbGrievanceType.SelectedValue
        If OldTicketType <> newTicketType Then
            IsTicketUpdated = True
        End If
    End Sub

    Private Function Themechange()


        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        lblGrievanceId.ForeColor = Color.Black
        lblGrievanceId.AutoSize = False
        CtrlCreatedBy.Size = New Size(75, 18)
        lblGrievanceId.BorderStyle = BorderStyle.None
        lblGrievanceId.SendToBack()
        lblGrievanceId.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrievanceId.TextAlign = ContentAlignment.MiddleLeft
        lblGrievanceId.BackColor = Color.FromArgb(212, 212, 212)

        lblId.ForeColor = Color.Black
        lblId.AutoSize = False
        lblId.BorderStyle = BorderStyle.None
        lblId.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblId.TextAlign = ContentAlignment.MiddleLeft
        lblId.BackColor = Color.White
        'lblGrievanceType.Visible = True
        'cbGrievanceType.Visible = True
        'lblGrievanceTitle.Visible = True
        'txtGrievanceTitle.Visible = True


        lblGrievanceType.ForeColor = Color.Black
        lblGrievanceType.AutoSize = False
        lblGrievanceType.Size = New Size(75, 18)
        lblGrievanceType.SendToBack()
        lblGrievanceType.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrievanceType.TextAlign = ContentAlignment.MiddleLeft
        lblGrievanceType.BackColor = Color.FromArgb(212, 212, 212)





        lblGrievanceDesc.ForeColor = Color.Black
        lblGrievanceDesc.AutoSize = False
        lblGrievanceDesc.Size = New Size(65, 16)
        lblGrievanceDesc.SendToBack()
        lblGrievanceDesc.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrievanceDesc.TextAlign = ContentAlignment.MiddleLeft
        lblGrievanceDesc.BackColor = Color.FromArgb(212, 212, 212)

        'lblDepartment.ForeColor = Color.Black
        'lblDepartment.AutoSize = False
        'lblDepartment.MaximumSize = New Size(250, 21)
        'lblDepartment.Size = New Size(250, 21)
        'lblDepartment.SendToBack()
        'lblDepartment.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'lblDepartment.TextAlign = ContentAlignment.MiddleLeft
        'lblDepartment.BackColor = Color.FromArgb(212, 212, 212)

        lblGrievanceTitle.ForeColor = Color.Black
        lblGrievanceTitle.AutoSize = False
        lblGrievanceTitle.Size = New Size(65, 16)
        lblGrievanceTitle.SendToBack()
        lblGrievanceTitle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrievanceTitle.TextAlign = ContentAlignment.MiddleLeft
        lblGrievanceTitle.BackColor = Color.FromArgb(212, 212, 212)
        CtrlCreatedOn.BorderStyle = BorderStyle.None
        CtrlLabelCreatedOn.BorderStyle = BorderStyle.None
        CtrlLabelUpdatedOn.BorderStyle = BorderStyle.None
        CtrlUpdatedOn.BorderStyle = BorderStyle.None
        CtrlLabelUpdatedOn.BorderStyle = BorderStyle.None
        CtrlLabelUpdatedBy.BorderStyle = BorderStyle.None
        CtrlUpdatedBy.BorderStyle = BorderStyle.None
        CtrlLabelCreatedBy.BorderStyle = BorderStyle.None
        CtrlCreatedBy.BorderStyle = BorderStyle.None
        If IsEdit Then
            CtrlCreatedBy.ForeColor = Color.Black
            CtrlCreatedBy.AutoSize = False
            CtrlCreatedBy.Size = New Size(75, 18)

            'CtrlCreatedBy.SendToBack()
            CtrlCreatedBy.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlCreatedBy.TextAlign = ContentAlignment.MiddleLeft
            CtrlCreatedBy.BackColor = Color.FromArgb(212, 212, 212)

            CtrlLabelCreatedBy.ForeColor = Color.Black
            CtrlLabelCreatedBy.AutoSize = False

            CtrlLabelCreatedBy.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlLabelCreatedBy.TextAlign = ContentAlignment.MiddleLeft
            CtrlLabelCreatedBy.BackColor = Color.White

            CtrlUpdatedBy.ForeColor = Color.Black
            CtrlUpdatedBy.AutoSize = False
            CtrlUpdatedBy.Size = New Size(65, 16)

            CtrlUpdatedBy.SendToBack()
            CtrlUpdatedBy.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlUpdatedBy.TextAlign = ContentAlignment.MiddleLeft
            CtrlUpdatedBy.BackColor = Color.FromArgb(212, 212, 212)

            CtrlLabelUpdatedBy.ForeColor = Color.Black
            CtrlLabelUpdatedBy.AutoSize = False

            CtrlLabelUpdatedBy.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlLabelUpdatedBy.TextAlign = ContentAlignment.MiddleLeft
            CtrlLabelUpdatedBy.BackColor = Color.White

            CtrlCreatedOn.ForeColor = Color.Black
            CtrlCreatedOn.AutoSize = False
            CtrlCreatedOn.Size = New Size(65, 16)
            CtrlCreatedOn.SendToBack()

            CtrlCreatedOn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlCreatedOn.TextAlign = ContentAlignment.MiddleLeft
            CtrlCreatedOn.BackColor = Color.FromArgb(212, 212, 212)

            CtrlLabelCreatedOn.ForeColor = Color.Black
            CtrlLabelCreatedOn.AutoSize = False
            CtrlLabelCreatedOn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlLabelCreatedOn.TextAlign = ContentAlignment.MiddleLeft
            CtrlLabelCreatedOn.BackColor = Color.White

            CtrlUpdatedOn.ForeColor = Color.Black
            CtrlUpdatedOn.AutoSize = False
            CtrlUpdatedOn.Size = New Size(65, 16)
            CtrlUpdatedOn.SendToBack()
            CtrlUpdatedOn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlUpdatedOn.TextAlign = ContentAlignment.MiddleLeft
            CtrlUpdatedOn.BackColor = Color.FromArgb(212, 212, 212)

            CtrlLabelUpdatedOn.ForeColor = Color.Black
            CtrlLabelUpdatedOn.AutoSize = False
            CtrlLabelUpdatedOn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            CtrlLabelUpdatedOn.TextAlign = ContentAlignment.MiddleLeft
            CtrlLabelUpdatedOn.BackColor = Color.White

            RdAsc.ForeColor = Color.FromArgb(212, 212, 212)
            RdAsc.AutoSize = False
            RdAsc.Size = New Size(65, 16)
            RdAsc.SendToBack()
            RdAsc.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            RdAsc.TextAlign = ContentAlignment.MiddleLeft
            'RdAsc.BackColor = Color.FromArgb(212, 212, 212)

            RdDesc.ForeColor = Color.FromArgb(212, 212, 212)
            RdDesc.AutoSize = False
            RdDesc.Size = New Size(65, 16)
            RdDesc.SendToBack()
            RdDesc.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            RdDesc.TextAlign = ContentAlignment.MiddleLeft
            ' RdDesc.BackColor = Color.FromArgb(212, 212, 212)

        End If


        lblStatus.ForeColor = Color.Black
        lblStatus.AutoSize = False
        lblStatus.MaximumSize = New Size(180, 21)
        lblStatus.Size = New Size(180, 21)
        lblStatus.SendToBack()
        lblStatus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblStatus.TextAlign = ContentAlignment.MiddleLeft
        lblStatus.BackColor = Color.FromArgb(212, 212, 212)
        'lblStatus.BackColor = Color.Green

        CtrlLabel6.ForeColor = Color.Black
        CtrlLabel6.AutoSize = False
        CtrlLabel6.MaximumSize = New Size(125, 22)
        CtrlLabel6.Size = New Size(125, 22)
        CtrlLabel6.BorderStyle = BorderStyle.None
        CtrlLabel6.SendToBack()
        CtrlLabel6.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel6.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)

        lblRaisedFromSiteOrdept.ForeColor = Color.Black
        lblRaisedFromSiteOrdept.AutoSize = False
        lblRaisedFromSiteOrdept.MaximumSize = New Size(200, 22)
        lblRaisedFromSiteOrdept.Size = New Size(200, 22)
        lblRaisedFromSiteOrdept.BorderStyle = BorderStyle.None
        lblRaisedFromSiteOrdept.SendToBack()
        lblRaisedFromSiteOrdept.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblRaisedFromSiteOrdept.TextAlign = ContentAlignment.MiddleLeft
        lblRaisedFromSiteOrdept.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel1.ForeColor = Color.Black
        CtrlLabel1.AutoSize = False
        CtrlLabel1.MaximumSize = New Size(111, 22)
        CtrlLabel1.Size = New Size(111, 22)
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.SendToBack()
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel3.ForeColor = Color.Black
        CtrlLabel3.AutoSize = False
        CtrlLabel3.BorderStyle = BorderStyle.None
        CtrlLabel3.SendToBack()
        CtrlLabel3.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel2.ForeColor = Color.Black
        CtrlLabel2.AutoSize = False
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.SendToBack()
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel4.ForeColor = Color.Black
        CtrlLabel4.AutoSize = False
        CtrlLabel4.BorderStyle = BorderStyle.None
        CtrlLabel4.SendToBack()
        CtrlLabel4.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLabel5.ForeColor = Color.Black
        CtrlLabel5.AutoSize = False
        CtrlLabel5.BorderStyle = BorderStyle.None
        CtrlLabel5.SendToBack()
        CtrlLabel5.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel5.TextAlign = ContentAlignment.MiddleLeft
        CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)

        CtrlLblSMSNo.ForeColor = Color.Black
        CtrlLblSMSNo.AutoSize = False
        CtrlLblSMSNo.MaximumSize = New Size(155, 22)
        CtrlLblSMSNo.Size = New Size(155, 22)
        CtrlLblSMSNo.BorderStyle = BorderStyle.None
        CtrlLblSMSNo.SendToBack()
        CtrlLblSMSNo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLblSMSNo.TextAlign = ContentAlignment.MiddleLeft
        CtrlLblSMSNo.BackColor = Color.FromArgb(212, 212, 212)




        '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        '                                                                                                                                                                                                   '           \||/
        ' c1Sizer1.GridDefinition = <data name="c1Sizer1.GridDefinition" xml:space="preserve">
        '                             <value>2.09003215434084:False:True;3.53697749196142:False:False;2.89389067524116:False:False;3.37620578778135:False:False;3.05466237942122:False:False;3.53697749196142:False:False;10.9003215434084:False:False;20.21543408360129:False:False;3.85852090032154:False:False;27.3311897106109:False:False;18.4887459807074:False:True;	0.600961538461538:False:True;9.49519230769231:False:False;2.04326923076923:False:True;13.1009615384615:False:False;1.20192307692308:False:True;9.85576923076923:False:False;1.20192307692308:False:True;15.3846153846154:False:False;1.80288461538462:False:True;7.57211538461539:False:True;15.9855769230769:False:True;15.5048076923077:False:True;</value>
        '                          </data>
        'c1Sizer1.GridDefinition = <data name="c1Sizer1.GridDefinition" xml:space="preserve">
        '                              <value>2.09003215434084:False:True;3.53697749196142:False:False;2.89389067524116:False:False;3.37620578778135:False:False;3.05466237942122:False:False;3.53697749196142:False:False;20.9003215434084:False:False;3.21543408360129:False:False;3.85852090032154:False:False;27.3311897106109:False:False;18.4887459807074:False:True;	0.600961538461538:False:True;9.49519230769231:False:False;2.04326923076923:False:True;13.1009615384615:False:False;1.20192307692308:False:True;9.85576923076923:False:False;1.20192307692308:False:True;15.3846153846154:False:False;1.80288461538462:False:True;7.57211538461539:False:True;15.1442307692308:False:True;16.3461538461538:False:True;</value>
        '                          </data>
        'Me.TableLayoutPanel1.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.88889!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(3, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        'Me.TableLayoutPanel1.ColumnStyles.Insert(4, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63.0!))

        'Me.TableLayoutPanel1.RowStyles.Insert(0, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        'Me.TableLayoutPanel1.RowStyles.Insert(1, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        'Me.TableLayoutPanel1.RowStyles.Insert(2, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))





        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdCancel.Size = New Size(70, 30)
        cmdCancel.Location = New Point(58, 85)

        cmdSubmit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSubmit.BackColor = Color.Transparent
        cmdSubmit.BackColor = Color.FromArgb(0, 107, 163)
        cmdSubmit.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSubmit.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        cmdSubmit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSubmit.FlatStyle = FlatStyle.Flat
        cmdSubmit.FlatAppearance.BorderSize = 0
        cmdSubmit.TextAlign = ContentAlignment.MiddleCenter
        cmdSubmit.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSubmit.Size = New Size(70, 30)
        cmdSubmit.Location = New Point(-5, 85)
        dgGrievanceHistoryGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.dgGrievanceHistoryGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgGrievanceHistoryGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgGrievanceHistoryGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        Me.dgGrievanceHistoryGrid.Styles.Highlight.ForeColor = Color.Black
        Me.dgGrievanceHistoryGrid.Styles.Normal.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.dgGrievanceHistoryGrid.Styles.Fixed.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.dgGrievanceHistoryGrid.Styles.Highlight.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.dgGrievanceHistoryGrid.Styles.Highlight.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.dgGrievanceHistoryGrid.Styles.Focus.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.dgGrievanceHistoryGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgGrievanceHistoryGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
        '    For i = 0 To dgGrievanceHistoryGrid.Cols.Count - 1
        '        dgGrievanceHistoryGrid.Cols(i).Caption = dgGrievanceHistoryGrid.Cols(i).Caption.ToUpper
        '    Next
        'End If
        'If dgGrievanceHistoryGrid.Visible Then
        '    If dgGrievanceHistoryGrid.Rows.Count <= 6 = True Then
        '        Dim rowsCount = dgGrievanceHistoryGrid.Rows.Count
        '        dgGrievanceHistoryGrid.Rows.MinSize = 19
        '        dgGrievanceHistoryGrid.Rows.MaxSize = 19clsgr
        '        dgGrievanceHistoryGrid.Rows.DefaultSize = 19
        '        dgGrievanceHistoryGrid.MinimumSize = New Size(810, 20 * rowsCount - 1)
        '        dgGrievanceHistoryGrid.MaximumSize = New Size(8103, 20 * rowsCount - 1)
        '        dgGrievanceHistoryGrid.Size = New Size(810, 20 * rowsCount - 1)
        '        dgGrievanceHistoryGrid.ScrollBars = ScrollBars.None
        '    Else
        '        dgGrievanceHistoryGrid.RowSel = 2
        '        dgGrievanceHistoryGrid.MinimumSize = New Size(810, 88)
        '        dgGrievanceHistoryGrid.MaximumSize = New Size(8103, 88)
        '        dgGrievanceHistoryGrid.Size = New Size(810, 88)
        '        dgGrievanceHistoryGrid.ScrollBars = ScrollBars.Horizontal
        '    End If
        'End If

    End Function


    'Private Sub ChkListRaisedDept_ItemCheck(sender As Object, e As ItemCheckEventArgs)
    '    Dim deptids As String = ""


    'For i As Integer = 0 To ChkListRaisedDept.Items.Count - 1

    '    '  ChkListRaisedDept.Items
    'Next
    'Dim Data
    'For Each itemChecked In ChkListRaisedDept.CheckedItems


    ''  Data = ChkListRaisedDept.SelectedItem

    ''Dim DD = ChkListRaisedDept.GetItemCheckState(ChkListRaisedDept.Items.IndexOf(itemChecked)).ToString() + "."
    'Next
    ' deptids = ChkListRaisedDept.SelectedItem


    ''  ListViewShowRaisedDept.Columns[0].DisplayMember[0] = "Name";
    'For i As Integer = 0 To ChkListRaisedDept.Items.Count - 1
    '    If ChkListRaisedDept.Items(i).Selected Then
    '        deptids += ChkListRaisedDept.Items(i).Value + ","
    '    End If
    'Next

    'End Sub  
    Private Sub ChkListRaisedDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkListRaisedDept.SelectedIndexChanged
        ListViewShowRaisedDept.Items.Clear()
        Dim DptId As String = ""
        If Not IsEdit Then
            'commented for jk issue
            ' CtrlLblSMSNo.Text = ""
        End If
        If ChkListRaisedDept.CheckedItems.Count > 0 Then
            For i As Integer = 0 To ChkListRaisedDept.CheckedItems.Count - 1
                '  ListViewShowRaisedDept.Items.Add(ChkListRaisedDept.CheckedItems(i).ToString())
                Dim a = DirectCast(ChkListRaisedDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(0)
                Dim b = DirectCast(ChkListRaisedDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(1)
                ListViewShowRaisedDept.Items.Add(b)
                'If GVBasedAricleReturnList.Count = 0 Then
                '    GVBasedAricleReturnList.Add(a, b)
                'End If
                If Not RbRaisedDept.Checked Then
                    a = "'" + a.ToString + "'"
                End If
                DptId = DptId + "," + a.ToString
                cbDepartment.Text = b
            Next
            'commented for jk issue
            ' BindMobileNoList(DptId)
        End If

    End Sub
    Private Function BindMobileNoList(Optional ByVal deptid As String = "")
        Try
            Dim MobNo As String = ""
            If IsEdit Then
                Dim DeptMobNo = objCls.GetDepartmentMobileList(lblId.Text, clsAdmin.SiteCode)
                If DeptMobNo.Rows.Count > 0 Then

                    MobNo = DeptMobNo.Rows(0)(0).ToString
                    CtrlLblSMSNo.Text = MobNo
                Else
                    CtrlLblSMSNo.Text = ""
                End If
            Else
                Dim DeptMobNo As DataTable
                'code commented & added for jk issue
                'If RbRaisedDept.Checked Then
                '    deptid = deptid.Remove(0, 1)
                '    DeptMobNo = objCls.GetDepartmentMobileNumberByDepartment(deptid)
                'Else
                '    deptid = deptid.Remove(0, 1)
                '    DeptMobNo = objCls.GetMobileNumberBySiteCodeForList(deptid) '' Site MobileNo
                'End If
                DeptMobNo = objCls.GetMobileNumberBySiteCodeForList(clsAdmin.SiteCode) '' Site MobileNo

                If DeptMobNo.Rows.Count > 0 Then
                    For MobileRow = 0 To DeptMobNo.Rows.Count - 1
                        MobNo = MobNo + "," + DeptMobNo.Rows(MobileRow)(0).ToString
                    Next
                    MobNo = MobNo.Remove(0, 1)
                    CtrlLblSMSNo.Text = MobNo
                    tooltip1.SetToolTip(CtrlLblSMSNo, CtrlLblSMSNo.Text)
                Else
                    CtrlLblSMSNo.Text = ""
                End If
            End If
            '    Return MobNo
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub ChkListCCDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkListCCDept.SelectedIndexChanged
        ListViewShowCCDept.Items.Clear()
        If ChkListCCDept.CheckedItems.Count > 0 Then
            For i As Integer = 0 To ChkListCCDept.CheckedItems.Count - 1
                Dim a = DirectCast(ChkListCCDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(1)
                ListViewShowCCDept.Items.Add(a)
            Next
        End If
    End Sub

    Private Sub ChkListRaisedDept_ItemCheck(sender As Object, e As ItemCheckEventArgs) ' Handles ChkListRaisedDept.ItemCheck
        'ListViewShowRaisedDept.Items.Clear()
        'If ChkListRaisedDept.CheckedItems.Count > 0 Then
        '    For i As Integer = 0 To ChkListRaisedDept.CheckedItems.Count - 1

        '        'Dim asas = ChkListRaisedDept.CheckedItems(i)

        '        Dim a = DirectCast(ChkListRaisedDept.CheckedItems(i), System.Data.DataRowView).Row.ItemArray(1)

        '        ListViewShowRaisedDept.Items.Add(a)
        '        'Dim vv = (ChkListRaisedDept.)
        '        'DirectCast(vv, System.Data.DataRowView).Row.ItemArray(1)
        '        'ListViewShowRaisedDept.Items.Add(vv)
        '        ' Dim dd = ChkListRaisedDept.SelectedItems
        '        'ListViewShowRaisedDept.Items.Add("DAta ")
        '        ' ChkListRaisedDept.SelectedItems.ToString()

        '        'Dim dd = ChkListRaisedDept.SelectedItems
        '        '  ListViewShowRaisedDept.Items.Add(ChkListRaisedDept.SelectedItem)
        '    Next
        'End If
    End Sub

    Private Sub RbRaisedDept_CheckedChanged(sender As Object, e As EventArgs) Handles RbRaisedDept.CheckedChanged, RbRaisedSite.CheckedChanged

        If Not IsEdit Then
            If RbRaisedDept.Checked Then
                'deptBox = objCls.GetDepartment(clsAdmin.UserCode)
                'SiteBox = objCls.GetSite(clsAdmin.UserCode)
                PopulateChkListRaisedDept(deptBox2)

            Else
                PopulateChkListRaisedDept(SiteBox)
                PopulateComboBox(SiteBox, cbDepartment)
                pC1ComboSetDisplayMember(cbDepartment)
            End If
            'commented for jk issue
            'CtrlLblSMSNo.Text = ""
        Else
            If RbRaisedDept.Checked Then
                PopulateComboBox(deptBox, cbDepartment)
                pC1ComboSetDisplayMember(cbDepartment)
            Else
                PopulateComboBox(SiteBox, cbDepartment)
                pC1ComboSetDisplayMember(cbDepartment)
            End If

        End If
    End Sub

    Private Sub RbCCDept_CheckedChanged(sender As Object, e As EventArgs) Handles RbCCDept.CheckedChanged
        If RbCCDept.Checked Then
            PopulateChkListCCDept(deptBox1)
        Else
            PopulateChkListCCDept(SiteBox1)
        End If

    End Sub
End Class