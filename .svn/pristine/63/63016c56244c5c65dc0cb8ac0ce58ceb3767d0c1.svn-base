Imports SpectrumCommon
Imports SpectrumBL
Imports System.Globalization
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports SpectrumPrint
Imports Spire.Pdf
Public Class frmCreditSaleRportPopUp

    Private _DtSale As DataTable

    Public Property DtSale() As DataTable
        Get
            Return _DtSale
        End Get
        Set(ByVal value As DataTable)
            _DtSale = value
        End Set
    End Property

    Private Sub cmdDownload_Click(sender As Object, e As EventArgs) Handles cmdDownload.Click
        Try
            CreditSalesDetailsReport(clsAdmin.SiteCode)
            'cmdPDF.Checked = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub FilterCredisaleDetails()
        Try
            If Not DtSale Is Nothing And DtSale.Rows.Count > 0 Then
                For i = 0 To DtSale.Rows.Count - 1
                    DtSale.Rows(i)("Till") = DtSale.Rows(i)("Till").ToString().Substring(DtSale.Rows(i)("Till").ToString.Length - 1)

                Next
                DtSale = DtSale.AsEnumerable().GroupBy(Function(r) New With {Key .cole1 = r("DocumentNo")}).[Select](Function(g) g.OrderBy(Function(r) r("SrNo.")).First()).Distinct.CopyToDataTable()

                ' Dim dvSalesDetails As New DataView(DtSale)
                'Dim groupedRows = dvSalesDetails.Cast(Of DataRowView).GroupBy(Function(r) r("DocumentNo"))
            End If
           
        Catch ex As Exception
            LogException(ex)
        End Try
        

    End Sub

    Private Sub CreditSalesDetailsReport(ByVal SiteCode As String)
        Try
            Dim dsCredit As DataSet
            Call FilterCredisaleDetails()
            Dim path As String = ""
            Dim clsObj As New ReportBase
            Dim Time = DateTime.Now.ToString("dd.MM.yyyy - hh:mm tt")
            dsCredit = clsObj.GetCreditSaleReportData(clsAdmin.SiteCode)
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\CreditSalesReportPC.rdl")
            reportViewer2.LocalReport.ReportPath = appPath
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", SiteCode)
            Dim GTime As New ReportParameter("V_GetDate", Time)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, GTime})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dscreditsalesreportheader", dsCredit.Tables(0))
            Dim datasource1 As New ReportDataSource("dscreditsalesreportdata", DtSale)
            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(datasource1)
            Dim mybytes As [Byte]()
            If cmdPDF.Checked = True Then
                mybytes = reportViewer2.LocalReport.Render("Pdf")
                path = clsDefaultConfiguration.DayCloseReportPath & "\ " & clsAdmin.DayOpenDate.ToString("dd-MM-yyyy") & "_CreditSalesReport" & ".pdf" 'vipin
            Else
                mybytes = reportViewer2.LocalReport.Render("Excel")
                path = clsDefaultConfiguration.DayCloseReportPath & "\" & clsAdmin.DayOpenDate.ToString("dd-MM-yyyy") & "_CreditSalesReport" & ".xls"
            End If
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

End Class