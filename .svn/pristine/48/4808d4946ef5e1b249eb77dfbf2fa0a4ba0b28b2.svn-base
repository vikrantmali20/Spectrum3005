Imports SpectrumBL
Public Class frmDefaultSettings
    Dim dtMain As DataTable
    Dim obj As New clsCommon

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



    Private Sub frmDefaultSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtMain = obj.GetDefaultSetting(clsAdmin.SiteCode, "")
            If Not dtMain Is Nothing Then
                dgMain.DataSource = dtMain
            End If
            GridSettings()
            SetCulture(Me, Me.Name)


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub GridSettings()
        dgMain.Cols("DESCRIPTION").Caption = "Setting"
        dgMain.Cols("fldValue").Caption = "Value"
        dgMain.Cols("DocumentType").Caption = "Document"
        dgMain.Cols("fldLabel").Visible = False
        dgMain.Cols.Frozen = 2
        dgMain.ExtendLastCol = True
        dgMain.Cols("DESCRIPTION").AllowEditing = False
        dgMain.Cols("DocumentType").AllowEditing = False
        dgMain.AutoSizeCols()
        dgMain.ExtendLastCol = True
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim dt As DataTable = dtMain.Copy()
            If obj.SaveDefaultSetting(dt, clsAdmin.SiteCode) = True Then
                ShowMessage(getValueByKey("DS001"), "DS001 - " & getValueByKey("CLAE04"))
                Me.Close()
                'ShowMessage("Saved Successfully", "Information")
            Else
                ShowMessage(getValueByKey("DS002"), "DS002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Data not Saved", "Information")
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("DS002"), "DS002 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Data not Saved", "Information")
        End Try
    End Sub
End Class