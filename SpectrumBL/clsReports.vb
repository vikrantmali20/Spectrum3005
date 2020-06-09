Imports System.Data.SqlClient
Public Class clsReports
    Public Function LoadModules(byval LangCode As String ) As DataTable
        Try
            Dim dt As DataTable
            dt = FillDataTable(" select A.Code,Case When isnull(B.Description,'')='' Then A.Description ELSE B.Description END AS Description " & _
                    "from mstreportmodule A Left outer join ReportLangDtl B on A.CODE=B.ReportCode  AND B.LangCode='" & LangCode & "' AND B.Seq='1001' Where A.Status=1 ")
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function LoadReportsName(ByVal LangCode As String) As DataTable
        Try

            Dim dt As DataTable
            dt = FillDataTable("SELECT A.Code,A.ModCode,Case When isnull(B.Description,'')='' Then A.ReportName ELSE  B.Description END AS ReportName, " & _
           " A.DateApplicable, A.ViewName, A.Status, A.DirectReport, A.ColapsedTreeLevel, B.LangCode AS ReportLangCode " & _
           " FROM ReportExcelDtl A Left Outer join ReportLangDtl B on A.Code=B.ReportCode AND  B.LangCode='" & LangCode & "' AND B.SEQ='1000' " & _
           " WHERE A.STATUS = 1 ")
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function FillReportSettings(ByVal ModuleId As String, ByVal ReportId As String, ByVal LangCode As String) As DataTable
        Try
            Dim dt As DataTable
            dt = FillDataTable("SELECT A.ModuleCode,A.ReportCode,Case When isnull(B.Description,'')='' Then A.ConfigName ELSE  B.Description END AS ConfigName,A.TableName,A.CodeField,A.DescriptionField,A.FilterText,A.LinkedFieldName,A.Seq,A.Status " & _
                    " FROM REPORTEXCELCONFIG A Left Outer join ReportLangDtl B on A.ReportCode=B.ReportCode AND B.LangCode='" & LangCode & "' AND A.SEQ =B.SEQ " & _
                    " WHERE A.MODULECODE='" & ModuleId & "' AND A.REPORTCODE='" & ReportId & "' Order by A.Seq")
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function FillDirectReportDetail(ByVal ReportId As String, ByVal LangCode As String) As DataTable
        Try
            Dim dt As DataTable
            dt = FillDataTable("SELECT A.ReportCode,A.ColumnNames,A.SEQ,A.SEL,A.Total,A.GroupOn,A.AVGON, " & _
                " Case When isnull(B.Description,'')='' Then A.ColumnNames ELSE  B.Description END AS Description " & _
                " FROM DirectReportDtl A Left Outer join ReportLangDtl B on A.ReportCode=B.ReportCode AND A.SEQ=B.SEQ AND B.LangCode='" & LangCode & "' " & _
                " WHERE   A.REPORTCODE='" & ReportId & "' AND A.SEL=1 Order by A.seq")
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function FillDataTable(ByVal StrQuery As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(StrQuery, DBConnection.ConString)
            daDefault.SelectCommand.CommandTimeout = 0
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
