
Public Class frmPropmt
    Dim Source As Object
    Dim ColName As String
    Public SelectedVal As String
    Dim HideCol As String

    Public Sub New(ByVal source As Object, ByVal ColumnName As String, ByVal FormName As String, Optional ByVal hidecolumns As String = "")
        ' This call is required by the designer.
        InitializeComponent()
        Try
            Me.Source = source
            Me.ColName = ColumnName
            Me.Text = FormName
            Me.HideCol = hidecolumns
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = New Point((Screen.PrimaryScreen.Bounds.Right - Me.Width) / 2, (Screen.PrimaryScreen.Bounds.Bottom - (Me.Height + 15)))
        Catch ex As Exception
            LogException(ex)
        End Try
        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

        Me.btnCancel.Text = getValueByKey("frmncommonsearch.cmdcancel")
        Me.btnSelect.Text = getValueByKey("Crs020")
    End Sub
    Private Sub frmPropmt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If Not Source Is Nothing Then
                grdPopup.DataSource = Source

                If (Source IsNot Nothing AndAlso Not String.IsNullOrEmpty(HideCol)) Then

                    Dim properties = Source.GetType().GetGenericArguments().Single().GetProperties()

                    For index = 0 To properties.Count - 1
                        If Not (HideCol.Contains(properties(index).Name)) Then
                            grdPopup.Cols(properties(index).Name).Visible = False
                        End If
                    Next
                End If
            End If

            grdPopup.AutoSizeCols()
            grdPopup.ExtendLastCol = True

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdPopup_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles grdPopup.MouseDoubleClick
        Try
            If grdPopup.Rows.Count > 0 Then
                If Not grdPopup.RowSel = -1 Then
                    SelectedVal = grdPopup.Rows(grdPopup.RowSel)(ColName).ToString()
                    Me.Close()
                End If
            Else
                MessageBox.Show(getValueByKey("Crs021"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnSelect_Click(sender As System.Object, e As System.EventArgs) Handles btnSelect.Click
        Try
            If grdPopup.Rows.Count > 0 Then
                If Not grdPopup.RowSel = -1 Then
                    SelectedVal = grdPopup.Rows(grdPopup.RowSel)(ColName).ToString()
                    Me.Close()
                End If

            Else
                MessageBox.Show(getValueByKey("Crs021"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        SelectedVal = ""
        Me.Close()
    End Sub
End Class