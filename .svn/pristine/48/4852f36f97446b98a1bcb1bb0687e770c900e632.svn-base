Imports System.Data.SqlClient
Imports SpectrumBL

Public Class frmSetPrepStation
    Dim dt As DataTable
    Dim obj As New clsCommon

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            'If My.Settings.TerminalID <> String.Empty AndAlso My.Settings.TerminalID <> cmbTerminal.SelectedValue Then
            If ReadSpectrumParamFile("mstPrepStationID") <> String.Empty AndAlso ReadSpectrumParamFile("mstPrepStationID") <> cmbPrepStation.SelectedValue Then
                ShowMessage(getValueByKey("T04"), "T04 - " & getValueByKey("CLAE04"))
                'Exit Sub
            End If

            If obj.UpdatePrepStation(cmbPrepStation.SelectedValue, My.Computer.Name, clsAdmin.SiteCode, clsAdmin.UserName, clsAdmin.CurrentDate) Then
                Dim str As String = cmbPrepStation.SelectedValue
                'My.Settings.TerminalID = str
                CreateSpectrumParamFile("mstPrepStationID", str)
                clsAdmin.TerminalID = str
                Me.Close()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmSetPrepStation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'CtrlLabel1.Text = My.Settings.TerminalID
            'CtrlLabel1.Text = ReadSpectrumParamFile("mstPrepStationID")
            Dim Site As String = ""
            If String.IsNullOrEmpty(clsAdmin.SiteCode) Then
                Dim dt As DataTable = obj.GetDefaultSetting("0000", "")
                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Select("fldLabel='LocalSiteCode'", "", DataViewRowState.CurrentRows)
                        Site = dr("fldvalue").ToString()
                    Next
                End If
            Else
                Site = clsAdmin.SiteCode
            End If
            dt = obj.GetPrepStations(Site, True)
            dt.TableName = "PerpStation"
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbPrepStation.DataSource = dt
                cmbPrepStation.DisplayMember = "mstPrepStationID"
                cmbPrepStation.ValueMember = "mstPrepStationID"
                cmbPrepStation.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmbPrepStation.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbPrepStation.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
            ElseIf Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                ShowMessage(getValueByKey("T03") & Site, "T03 - " & getValueByKey("CLAE04"))
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))

            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlLabel2.ForeColor = Color.White
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.AutoSize = False
        CtrlLabel2.BackColor = Color.Transparent
        CtrlLabel2.Size = New Size(400, 16)
        CtrlLabel2.SendToBack()
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft

        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Size = New Size(400, 16)
        CtrlLabel1.SendToBack()
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.TextAlign = ContentAlignment.MiddleCenter
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSave.Size = New Size(80, 30)

    End Function

End Class