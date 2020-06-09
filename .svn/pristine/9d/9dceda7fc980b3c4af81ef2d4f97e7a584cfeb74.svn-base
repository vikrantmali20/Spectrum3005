Public Class frmProcScheduler
    Inherits CtrlRbnBaseForm

   
    Private Sub frmProcScheduler_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If Not String.IsNullOrEmpty(My.Settings.ProcSchedularInterval.Trim()) Then
                txtDuration.Text = My.Settings.ProcSchedularInterval
            End If

            If Not String.IsNullOrEmpty(My.Settings.ProcSchedularLastRunTime.Trim()) Then
                lblDisplayLRT.Text = Convert.ToDateTime(My.Settings.ProcSchedularLastRunTime)

            End If

            If Not String.IsNullOrEmpty(My.Settings.ProcSchedularNextRunTime.Trim()) AndAlso Not String.IsNullOrEmpty(My.Settings.ProcSchedularInterval.Trim()) Then
                lblDisplayNRT.Text = Convert.ToDateTime(My.Settings.ProcSchedularNextRunTime)
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        
    End Sub


    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            If (IsFormValid()) Then
                My.Settings.ProcSchedularInterval = txtDuration.Text
                'My.Settings.SchedularLastRunTime = "#9/24/2014 4:05:04 PM#"
                My.Settings.Save()
                ShowMessage("Setting Saved Successfully", getValueByKey("CLAE04"))
                Me.Close()
            End If

            ' System.Diagnostics.Process.Start(txtUrl.Text)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Function IsFormValid() As Boolean
        Try
            If String.IsNullOrEmpty(txtDuration.Text.Trim()) Then
                ShowMessage("Time Interval is Mandatory", getValueByKey("CLAE04"))
                txtDuration.Focus()
                Return False
            ElseIf Val(txtDuration.Text) = 0 Then
                ShowMessage("Please Enter Valid Time Interval", getValueByKey("CLAE04"))
                txtDuration.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
End Class
