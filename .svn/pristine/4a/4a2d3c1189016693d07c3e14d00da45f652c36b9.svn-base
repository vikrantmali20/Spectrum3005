Imports Microsoft.PointOfService
Imports System.Management
Imports SpectrumBL

Public Class frmNPrinterTillMapping
    Private dsCombo As New DataSet
    Private dsMain As New DataSet
    Private dvPrinter As DataView
    Private dtPrinter As DataTable
    Private drPrinter As DataRow

    Private fKeyPrinter(1) As Object
    Private clsPMap As New clsPrinterMapping

    Private Sub frmNPrinterTillMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            btnAdvance.Visible = False
            SetCulture(Me, Me.Name)
            dsCombo = clsPMap.GetPrinterComboInfo(clsAdmin.SiteCode)
            LoadPrinterMappingData()
            CboDocType.DataSource = dsCombo.Tables("PrinterDoc")
            CboDocType.ValueMember = dsCombo.Tables("PrinterDoc").Columns("PrinterDocument").ColumnName
            CboDocType.DisplayMember = dsCombo.Tables("PrinterDoc").Columns("DocumentDesc").ColumnName
            CboDocType.SelectedIndex = 0

            CboTerminal.DataSource = dsCombo.Tables("TerminalInfo")
            CboTerminal.ValueMember = dsCombo.Tables("TerminalInfo").Columns("TerminalID").ColumnName
            CboTerminal.DisplayMember = dsCombo.Tables("TerminalInfo").Columns("TerminalName").ColumnName
            SetCulture(Me, Me.Name)
            grdPrinterMapping.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            'GetSystemDevice(Nothing)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub CboTerminal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTerminal.SelectedIndexChanged
        Try
            If Not (CboTerminal.SelectedValue.ToString = "System.Data.DataRowView") Then
                dvPrinter = New DataView(dsCombo.Tables("DeviceProfile"), "TerminalID='" & CboTerminal.SelectedValue & "'", String.Empty, DataViewRowState.CurrentRows)
                dtPrinter = dvPrinter.ToTable(False, "DriverType", "LogicalName")

                If dtPrinter.Rows.Count > 0 Then
                    CboPrinterInfo.DataSource = dtPrinter
                    CboPrinterInfo.ValueMember = dtPrinter.Columns("DriverType").ColumnName
                    CboPrinterInfo.DisplayMember = dtPrinter.Columns("LogicalName").ColumnName
                    CboPrinterInfo.SelectedIndex = 0
                Else
                    CboPrinterInfo.DataSource = Nothing

                End If

                RefreshPrinterGrid()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub GetSystemDevice(ByVal POSDType As String)
        Try
            ' Use the ObjectQuery to get the list of configured printers
            Dim oquery As System.Management.ObjectQuery = New System.Management.ObjectQuery("SELECT * FROM Win32_Printer")
            Dim mosearcher As System.Management.ManagementObjectSearcher = New System.Management.ManagementObjectSearcher(oquery)

            Dim moc As System.Management.ManagementObjectCollection = mosearcher.Get()
            CboPrinterInfo.Items.Clear()
            For Each mo As ManagementObject In moc
                If CBool(mo("Network")) Or CBool(mo("Local")) Then
                    CboPrinterInfo.Items.Add(mo("Name"))
                End If
            Next mo
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub RefreshPrinterGrid()
        Dim dvPrinterN As DataView
        dvPrinterN = New DataView(dsMain.Tables("PrinterTillMap"), "TerminalID='" & CboTerminal.SelectedValue & "'", Nothing, DataViewRowState.CurrentRows)
        dtPrinter = New DataTable
        dtPrinter = dvPrinterN.ToTable(False, "TerminalID", "DocDesc", "PrinterName", "PrinterDocument", "PrintFormat")

        grdPrinterMapping.DataSource = dtPrinter
        grdPrinterMapping.Cols("Delete").Caption = ""
        grdPrinterMapping.Cols("Delete").Width = 20
        grdPrinterMapping.Cols("Delete").ComboList = "..."
        grdPrinterMapping.Cols("PrinterDocument").Visible = False

        grdPrinterMapping.AutoSizeCols()
    End Sub
    Private Sub grdPrinterMapping_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdPrinterMapping.CellButtonClick
        Try
            If MsgBox(getValueByKey("SO011"), MsgBoxStyle.YesNo, "SO011 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Dim filterDelete As String = String.Empty

                grdPrinterMapping.Cols("PrinterDocument").Visible = True
                filterDelete = "TerminalID='" & CboTerminal.SelectedValue & "' And PrinterDocument='" & grdPrinterMapping.Item(grdPrinterMapping.Row, "PrinterDocument") & "'"

                dvPrinter = New DataView(dsMain.Tables("PrinterTillMap"), filterDelete, "", DataViewRowState.CurrentRows)
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


    Private Sub BtnSetPrinterMapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSetPrinterMapping.Click
        Try
            fKeyPrinter(0) = CboTerminal.SelectedValue
            fKeyPrinter(1) = CboDocType.SelectedValue
            drPrinter = dsMain.Tables("PrinterTillMap").Rows.Find(fKeyPrinter)
            Dim str As String = String.Empty
            If drPrinter Is Nothing Then
                drPrinter = dsMain.Tables("PrinterTillMap").NewRow

                drPrinter("TerminalID") = CboTerminal.SelectedValue
                drPrinter("PrinterDocument") = CboDocType.SelectedValue
                drPrinter("DocDesc") = CboDocType.Text
                drPrinter("PrinterType") = CboPrinterInfo.SelectedValue
                drPrinter("PrinterName") = CboPrinterInfo.Text

                drPrinter("CREATEDAT") = clsAdmin.SiteCode
                drPrinter("CREATEDBY") = clsAdmin.UserName
                drPrinter("CREATEDON") = clsAdmin.CurrentDate
                drPrinter("UPDATEDAT") = clsAdmin.SiteCode
                drPrinter("UPDATEDBY") = clsAdmin.UserName
                drPrinter("UPDATEDON") = clsAdmin.CurrentDate
                If rbA4.Checked Then
                    str = "A4"
                End If
                If rbL40.Checked Then
                    str = "L40"
                End If
                drPrinter("PrintFormat") = str

                dsMain.Tables("PrinterTillMap").Rows.Add(drPrinter)
            Else
                drPrinter("PrinterDocument") = CboDocType.SelectedValue
                drPrinter("DocDesc") = CboDocType.Text
                drPrinter("PrinterType") = CboPrinterInfo.SelectedValue
                drPrinter("PrinterName") = CboPrinterInfo.Text
                If rbA4.Checked Then
                    str = "A4"
                End If
                If rbL40.Checked Then
                    str = "L40"
                End If
                drPrinter("PrintFormat") = str

                drPrinter("UPDATEDAT") = clsAdmin.SiteCode
                drPrinter("UPDATEDBY") = clsAdmin.UserName
                drPrinter("UPDATEDON") = clsAdmin.CurrentDate
            End If

            RefreshPrinterGrid()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSavePrinterMapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSavePrinterMapping.Click
        Try
            If clsPMap.SavePrinterTillMapping(dsMain, clsAdmin.SiteCode) = True Then
                'MsgBox("Update", , getValueByKey("CLAE05"))
                ShowMessage(getValueByKey("PTM001"), "PTM001 - " & getValueByKey("CLAE04"))
                LoadPrinterMappingData()
            Else
                'MsgBox("Fail", , getValueByKey("CLAE05"))
                ShowMessage(getValueByKey("PTM002"), "PTM002 - " & getValueByKey("CLAE05"))
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub LoadPrinterMappingData()
        dsMain = clsPMap.GetPrnMappingStruc(clsAdmin.SiteCode)

        If dsMain.Tables("PrinterTillMap").Columns.Contains("DocDesc") = False Then
            dsMain.Tables("PrinterTillMap").Columns.Add("DocDesc", System.Type.GetType("System.String"))
        End If

        Dim filterDesc As String = String.Empty

        For Each drUpdate As DataRow In dsMain.Tables("PrinterTillMap").Rows
            filterDesc = "PrinterDocument='" & drUpdate("PrinterDocument") & "'"
            If dsCombo.Tables("PrinterDoc").Select(filterDesc).Length > 0 Then
                drUpdate("DocDesc") = dsCombo.Tables("PrinterDoc").Select(filterDesc)(0)("DocumentDesc").ToString
            End If
        Next

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

    Private Sub btnAdvance_Click(sender As System.Object, e As System.EventArgs) Handles btnAdvance.Click
        Try
            Dim objHierarchy As New frmPrinterHirearchyMap
            objHierarchy.TerminalId = CboTerminal.SelectedValue
            objHierarchy.DocTypeVal = CboDocType.SelectedValue
            objHierarchy.DocTypeText = CboDocType.SelectedValue
            objHierarchy.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CboDocType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CboDocType.SelectedIndexChanged
        Try
            If CboDocType.SelectedValue IsNot Nothing Then
                If CboDocType.SelectedValue = "KOT" Then
                    btnAdvance.Visible = True
                Else
                    btnAdvance.Visible = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNPrinterTillMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, " printer-document-mapping.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        grdPrinterMapping.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdPrinterMapping.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdPrinterMapping.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdPrinterMapping.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdPrinterMapping.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        BtnSavePrinterMapping.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSavePrinterMapping.BackColor = Color.Transparent
        BtnSavePrinterMapping.BackColor = Color.FromArgb(0, 107, 163)
        BtnSavePrinterMapping.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSavePrinterMapping.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSavePrinterMapping.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSavePrinterMapping.FlatStyle = FlatStyle.Flat
        BtnSavePrinterMapping.FlatAppearance.BorderSize = 0
        BtnSavePrinterMapping.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        BtnSavePrinterMapping.TextAlign = ContentAlignment.MiddleCenter
        BtnSavePrinterMapping.Size = New Size(85, 30)


        BtnSetPrinterMapping.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSetPrinterMapping.BackColor = Color.Transparent
        BtnSetPrinterMapping.BackColor = Color.FromArgb(0, 107, 163)
        BtnSetPrinterMapping.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSetPrinterMapping.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSetPrinterMapping.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSetPrinterMapping.FlatStyle = FlatStyle.Flat
        BtnSetPrinterMapping.FlatAppearance.BorderSize = 0
        BtnSetPrinterMapping.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        BtnSetPrinterMapping.TextAlign = ContentAlignment.MiddleCenter
        BtnSetPrinterMapping.Size = New Size(85, 30)

        btnAdvance.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdvance.BackColor = Color.Transparent
        btnAdvance.BackColor = Color.FromArgb(0, 107, 163)
        btnAdvance.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdvance.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnAdvance.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdvance.FlatStyle = FlatStyle.Flat
        btnAdvance.FlatAppearance.BorderSize = 0
        btnAdvance.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnAdvance.TextAlign = ContentAlignment.MiddleCenter
        btnAdvance.Size = New Size(85, 30)



    End Function
End Class