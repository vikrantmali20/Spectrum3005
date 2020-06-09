Imports SpectrumBL
Imports SpectrumCommon

Public Class frmTest

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim objReport As New DayCloseReportController
        objReport.GenerateDayCloseReport(New DayCloseReportModel With {.SiteCode = TextBox1.Text, .ToDate = DateTimePicker1.Value}, clsDefaultConfiguration.DayCloseReportPath)
    End Sub

End Class