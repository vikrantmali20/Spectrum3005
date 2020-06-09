Imports SpectrumBL

Public Class frmNSearchOrderSOBL
    Dim dtDocInfo As New DataTable
    Dim clsTender As New clsChangeTender
    Dim _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property
    Dim _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property

    Private Sub frmNSearchOrderSOBL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            clsTender.ShiftManagementForSO = clsDefaultConfiguration.ShiftManagement
            Dim dt As New DataTable
            dt = clsCommon.GetNextShiftID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            dtDocInfo = clsTender.GetDocumentInfo(clsAdmin.SiteCode, clsAdmin.Financialyear, DocumentType, clsAdmin.DayOpenDate, clsAdmin.TerminalID, clsAdmin.UserCode, clsCommon._PrevShiftId)
            'dtDocInfo = clsTender.GetDocumentInfo(clsAdmin.SiteCode, clsAdmin.Financialyear, DocumentType, clsAdmin.DayOpenDate.ToString("yyyyMMdd"), clsAdmin.TerminalID)
            grdDocInfo1.DataSource = dtDocInfo

            If dtDocInfo.Rows.Count = 0 Then
                ShowMessage(getValueByKey("SO086"), "SO086 - " & getValueByKey("CLAE04"))
                Me.BtnSearchCancel_Click(sender, e)
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub grdDocInfo1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocInfo1.MouseClick
        Try
            If grdDocInfo1.RowCount > 0 Then
                _DocumentNo = grdDocInfo1.Item(grdDocInfo1.Row, 0).ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDocInfo1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocInfo1.MouseDoubleClick
        Try
            If grdDocInfo1.RowCount > 0 Then
                _DocumentNo = grdDocInfo1.Item(grdDocInfo1.Row, 0).ToString
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSearchOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnSearchCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class