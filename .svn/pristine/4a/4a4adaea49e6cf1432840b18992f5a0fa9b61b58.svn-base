Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.IO
Imports System.Linq

Public Class AdsrReport
    Inherits clsCommon
    Implements IReports
    Private DayCloseReportPath As String = "D:\AdsrReport"
    Public Function GenerateDayCloseReport(request As SpectrumCommon.DayCloseReportModel, reportPath As String) As Boolean Implements IReports.GenerateDayCloseReport
        Try
            If Not String.IsNullOrEmpty(reportPath) Then
                DayCloseReportPath = reportPath
            End If
            If Not System.IO.Directory.Exists(DayCloseReportPath) Then
                System.IO.Directory.CreateDirectory(DayCloseReportPath)
            End If
            Dim sequenceNo As String = GetSequenceNumber()
            Dim ds As DataSet = GetAdsrDataSet(request, sequenceNo)
            If ds IsNot Nothing AndAlso ds.Tables.Count >= 3 Then
                Dim path As String = GetDirectoryPath(ds, sequenceNo, request)
                Using sw As StreamWriter = File.CreateText(path)
                    For Each table In ds.Tables
                        For Each dr In table.Rows
                            sw.WriteLine(dr(0))
                        Next
                    Next
                End Using
                System.Diagnostics.Process.Start(path)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function GetAdsrDataSet(ByVal request As SpectrumCommon.DayCloseReportModel, ByVal sequence As String) As DataSet

        Dim query As String = String.Format("EXEC {0} '{1}', '{2}', '{3}'", request.AdsrReportProcedureName, request.ToDate.ToString("yyyy-MM-dd"), request.SiteCode, sequence)

        'If request.AdsrReportProcedureName.ToUpper() = "USP_ADSR_DlfDelhi".ToUpper() Then
        '    query = "exec USP_ADSR_DlfDelhi '" & request.ToDate.ToString("yyyy-MM-dd") & "','" & request.SiteCode & "', '" & sequence & "'"
        'ElseIf request.AdsrReportProcedureName.ToUpper() = "USP_ADSR_Korum".ToUpper() Then
        '    query = "exec USP_ADSR_Korum '" & request.ToDate.ToString("yyyy-MM-dd") & "','" & request.SiteCode & "', '" & sequence & "'"
        'End If
        Dim dataAdapter As SqlDataAdapter
        Dim reportSet As New DataSet
        'dataAdapter = New SqlDataAdapter(query, SpectrumCon)
        Dim cmd As New SqlCommand(query, SpectrumCon)
        cmd.CommandTimeout = 0
        dataAdapter = New SqlDataAdapter(cmd)
        dataAdapter.Fill(reportSet)
        Return reportSet
    End Function

    Private Function GetDirectoryPath(ByRef ds As DataSet, ByVal sequence As String, request As SpectrumCommon.DayCloseReportModel) As String
        Dim createdDate As String = ds.Tables(0).Rows(0)(0)
        Dim array As String() = createdDate.Split("|")
        Dim time As String = array(array.Count - 3)
        Dim timeArray As String() = time.Split(":")
        Dim terminalId As String = array(3)
        createdDate = array(array.Count - 1)
        createdDate = createdDate.Substring(2)
        If timeArray.Count >= 2 Then
            createdDate = createdDate & timeArray(0) & timeArray(1)
        End If

        Dim path As String = String.Format("{0}\{1}_{2}_{3}_{4}.txt", DayCloseReportPath, request.AdsrReportFileName, terminalId, sequence, createdDate)

        'If request.AdsrReportProcedureName.ToUpper() = "USP_ADSR_DlfDelhi".ToUpper() Then
        '    path = DayCloseReportPath & "\tH024840000557_" & terminalId & "_" & sequence & "_" & createdDate & ".txt"

        'ElseIf request.AdsrReportProcedureName.ToUpper() = "USP_ADSR_Korum".ToUpper() Then
        '    path = DayCloseReportPath & "\t113-K01TF04U13_" & terminalId & "_" & sequence & "_" & createdDate & ".txt"
        'End If     

        Return path
    End Function

    Private Function GetSequenceNumber() As String
        Dim files As FileInfo() = New DirectoryInfo(DayCloseReportPath).GetFiles("*.txt")
        Dim lastFile As FileInfo
        Dim nextNumber As Integer
        If files IsNot Nothing AndAlso files.Count > 0 Then
            lastFile = files.OrderByDescending(Function(F) F.CreationTime).First()
        Else
            nextNumber = 1
        End If
        If lastFile IsNot Nothing Then
            Dim fileNameParts As String() = lastFile.Name.Split("_")
            If fileNameParts.Length = 4 Then
                If Integer.TryParse(fileNameParts(2), nextNumber) Then
                    nextNumber = nextNumber + 1
                End If
            End If
        End If
        If nextNumber = 10000 Then
            nextNumber = 1
        End If
        Return String.Format("{0:D4}", nextNumber)
    End Function
End Class
