Imports SpectrumBL

Public Class frmConfigurationSettings
    Dim obj As New clsCommon

    Dim dtMain, dtDefault, dtCashMemo, dtBirthList, dtSalesOrder, dtTillOpenClose, dtPCM, dtDC, DtIntegration As New DataTable
    Dim dvDefault, dvCashMemo, dvBirthList, dvSalesOrder, dvTillOpenClose, dvPCM, dvDC, dvIntegration As DataView
    Dim dtSite, dtSupplier As New DataTable

    Private Sub frmConfigurationSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            dtMain = obj.GetDefaultSetting(clsAdmin.SiteCode, String.Empty)

            dtDefault = obj.GetDefaultSetting(clsAdmin.SiteCode, "0000")
            dtCashMemo = obj.GetDefaultSetting(clsAdmin.SiteCode, "CMS")
            dtBirthList = obj.GetDefaultSetting(clsAdmin.SiteCode, "BLS")
            dtSalesOrder = obj.GetDefaultSetting(clsAdmin.SiteCode, "SalesOrder")
            dtTillOpenClose = obj.GetDefaultSetting(clsAdmin.SiteCode, "TillOpenNClose")
            dtPCM = obj.GetDefaultSetting(clsAdmin.SiteCode, "PCM")
            dtDC = obj.GetDefaultSetting(clsAdmin.SiteCode, "DC")
            DtIntegration = obj.GetDefaultSetting(clsAdmin.SiteCode, "Integration")

            gridDefaults.RefreshGridData(dtDefault)
            gridDefaults.DefaultFlexGrid = gridDefaults.gridConfigSettings

            gridCashMemo.RefreshGridData(dtCashMemo)
            gridBirthList.RefreshGridData(dtBirthList)
            gridSalesOrder.RefreshGridData(dtSalesOrder)
            gridTillOpenClose.RefreshGridData(dtTillOpenClose)
            gridPCM.RefreshGridData(dtPCM)
            gridDC.RefreshGridData(dtDC)
            gridintegration.RefreshGridData(DtIntegration)

            CtrlTabMain.SelectedIndex = 0

            TabPageDefaults.Text = getValueByKey("DC0003")
            TabPageCashMemo.Text = getValueByKey("DC0004")
            TabPageSalesOrder.Text = getValueByKey("DC0005")
            TabPageBirthList.Text = getValueByKey("DC0006")
            TabPageTillOpenClose.Text = getValueByKey("DC0007")
            TabPagePCM.Text = getValueByKey("DC0012")
            TabPageDC.Text = getValueByKey("DC0013")
            btnSaveSetting.Text = getValueByKey("DC0008")
            btnCloseSetting.Text = getValueByKey("DC0009")
            Me.Text = getValueByKey("DC0010")

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSaveSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSetting.Click
        Dim dtSaveSetting As New DataTable
        Try

            dvDefault = New DataView(dtDefault.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvDefault.ToTable

            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                        If (drSetting("FldType").ToString.ToUpper = "Images".ToUpper) Then
                            CreateSpectrumParamFile(drSetting("FldLabel"), drSetting("FldValue").ToString.Trim)

                        ElseIf (drSetting("FldLabel").ToString.ToUpper = "LocalSiteCode".ToUpper) Then
                            Dim clsComn As New clsCommon
                            Dim dtDefaultSupplier As New DataTable
                            dtDefaultSupplier = clsComn.GetSiteAndSupplierInfo("Site")

                            Dim drSite As DataRow() = dtDefaultSupplier.Select("SiteShortName='" & drSetting("FldValue") & "'")
                            drChange("FldValue") = drSite(0)("SiteCode")
                            drChange("UPDATEDBY") = clsAdmin.UserCode
                            drChange("UPDATEDON") = DateTime.Now
                        ElseIf (drSetting("FldLabel").ToString.ToUpper = "SOSupplier".ToUpper) Then
                            Dim clsComn As New clsCommon
                            Dim dtDefaultSupplier As New DataTable
                            dtDefaultSupplier = clsComn.GetSiteAndSupplierInfo("Supplier")

                            Dim drSupplier As DataRow() = dtDefaultSupplier.Select("SupplierName='" & drSetting("FldValue") & "'")
                            drChange("FldValue") = drSupplier(0)("SupplierCode")
                            drChange("UPDATEDBY") = clsAdmin.UserCode
                            drChange("UPDATEDON") = DateTime.Now
                        ElseIf (drSetting("FldLabel").ToString.ToUpper = "CustSearchCharpos".ToUpper) Then
                            If drSetting("FldValue") > 10 Then
                                ShowMessage("Value canot be greater than 10", getValueByKey("CLAE05"))
                                Exit Sub
                            End If

                        End If
                    Next
                Next
            End If


            dvCashMemo = New DataView(dtCashMemo.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvCashMemo.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If


            dvBirthList = New DataView(dtBirthList.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvBirthList.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If


            dvSalesOrder = New DataView(dtSalesOrder.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvSalesOrder.ToTable

            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If


            dvTillOpenClose = New DataView(dtTillOpenClose.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvTillOpenClose.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If

            dvPCM = New DataView(dtPCM.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvPCM.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If

            dvDC = New DataView(dtDC.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvDC.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If


            dvIntegration = New DataView(DtIntegration.Copy, String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            dtSaveSetting = dvIntegration.ToTable
            If (dtSaveSetting.Rows.Count > 0) Then
                For Each drSetting As DataRow In dtSaveSetting.Rows
                    For Each drChange As DataRow In dtMain.Select("FldLabel='" & drSetting("FldLabel") & "'")
                        drChange("FldValue") = drSetting("FldValue")
                        drChange("UPDATEDBY") = clsAdmin.UserCode
                        drChange("UPDATEDON") = DateTime.Now
                    Next
                Next
            End If


            If obj.SaveDefaultSetting(dtMain, clsAdmin.SiteCode) = True Then
                ShowMessage(getValueByKey("DS001"), "DS001 - " & getValueByKey("CLAE04"))
                Me.Close()
            Else
                ShowMessage(getValueByKey("DS002"), "DS002 - " & getValueByKey("CLAE04"))
            End If
            If clsDefaultConfiguration.EnableImprestCash Then
                Dim obj1 As New clsCommon
                Dim dvPayment As New DataView(dtMain, "FLDLABEL='ImprestCashAmount'", "", DataViewRowState.CurrentRows)

                Dim Till As New DataView(dtMain, "FLDLABEL='ImprestCashTill'", "", DataViewRowState.CurrentRows)

                Dim Amount As String = ""
                Dim Terminal As String = ""
                Amount = dvPayment(0)("FLDVALUE")
                Terminal = Till(0)("FLDVALUE")
                Dim opendate As Date

                Dim objReportBase As New ReportBase
                Dim currentshiftid As Integer
                Dim createdon As DateTime
                If clsDefaultConfiguration.ShiftManagement Then
                    Dim dt = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
                    If dt.Rows.Count > 0 Then
                        currentshiftid = dt.Rows(0)("ShiftId").ToString()
                        createdon = dt.Rows(0)("CREATEDON")
                    End If
                Else
                    currentshiftid = "0"
                End If
                obj1.SaveImprestAmount(Amount, clsAdmin.SiteCode, Terminal, currentshiftid, clsAdmin.DayOpenDate, clsAdmin.UserCode, "Imprest")

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("DS002"), "DS002 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnCloseSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSetting.Click
        Me.Close()
    End Sub

    Public Sub New()
        Try

            If CheckAuthorisation(clsAdmin.UserCode, "DEF.CONF") = False Then
                ShowMessage(getValueByKey("SPCM001"), "SPCM001 - " & getValueByKey("CLAE04"))
                'ShowMessage("You have not Sufficent Rights", "Information")
                Me.Dispose()
                Me.Close()
                Exit Sub
            End If

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlTabMain_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CtrlTabMain.SelectedIndexChanged

        If (CtrlTabMain.SelectedTab IsNot Nothing) Then
            If (CtrlTabMain.SelectedTab.Name.Equals("TabPageDefaults")) Then
                If (gridDefaults.gridConfigSettings.Rows.Count > 1) Then
                    gridDefaults.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPageCashMemo")) Then
                If (gridCashMemo.gridConfigSettings.Rows.Count > 1) Then
                    gridCashMemo.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPageSalesOrder")) Then
                If (gridSalesOrder.gridConfigSettings.Rows.Count > 1) Then
                    gridSalesOrder.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPageBirthList")) Then
                If (gridBirthList.gridConfigSettings.Rows.Count > 1) Then
                    gridBirthList.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPageTillOpenClose")) Then
                If (gridTillOpenClose.gridConfigSettings.Rows.Count > 1) Then
                    gridTillOpenClose.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPagePCM")) Then
                If (gridPCM.gridConfigSettings.Rows.Count > 1) Then
                    gridPCM.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If

            ElseIf (CtrlTabMain.SelectedTab.Name.Equals("TabPageDC")) Then
                If (gridDC.gridConfigSettings.Rows.Count > 1) Then
                    gridDC.gridConfigSettings.Select(1, 1, 1, 1, True)
                End If
            End If
        End If

    End Sub

    Private Sub frmConfigurationSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "default-config.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        gridDefaults.BackColor = Color.FromArgb(212, 212, 212)
        TabPageBirthList.TabForeColor = Color.White
        TabPageCashMemo.TabForeColor = Color.White
        TabPageDC.TabForeColor = Color.White
        TabPageDefaults.TabForeColor = Color.White
        TabPagePCM.TabForeColor = Color.White
        TabPageSalesOrder.TabForeColor = Color.White
        TabPageTillOpenClose.TabForeColor = Color.White
        Integration.TabForeColor = Color.White
        TabPageBirthList.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPageCashMemo.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPageDC.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPageDefaults.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPagePCM.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPageSalesOrder.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        TabPageTillOpenClose.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        Integration.TabForeColorSelected = Color.FromArgb(255, 255, 255)

        TabPageBirthList.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageCashMemo.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageDC.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageDefaults.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPagePCM.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageSalesOrder.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        TabPageTillOpenClose.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        Integration.TabBackColorSelected = Color.FromArgb(0, 107, 163)

        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnSaveSetting.Location = New Point(335, 537)
        btnSaveSetting.Size = New Size(111, 28)
        btnSaveSetting.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveSetting.BackColor = Color.Transparent
        btnSaveSetting.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveSetting.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveSetting.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveSetting.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveSetting.FlatStyle = FlatStyle.Flat
        btnSaveSetting.FlatAppearance.BorderSize = 0
        btnSaveSetting.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnSaveSetting.TextAlign = ContentAlignment.MiddleCenter

        btnCloseSetting.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCloseSetting.Location = New Point(452, 537)
        btnCloseSetting.Size = New Size(111, 28)
        btnCloseSetting.BackColor = Color.Transparent
        btnCloseSetting.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseSetting.ForeColor = Color.FromArgb(255, 255, 255)
        btnCloseSetting.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCloseSetting.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseSetting.FlatStyle = FlatStyle.Flat
        btnCloseSetting.FlatAppearance.BorderSize = 0
        btnCloseSetting.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnCloseSetting.TextAlign = ContentAlignment.MiddleCenter

    End Function
End Class