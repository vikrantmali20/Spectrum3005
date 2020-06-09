Imports SpectrumCommon
Public Class ReportFactory
    Private Sub New()

    End Sub

    Private Shared _Instance As ReportFactory
    Public Shared ReadOnly Property Instance As ReportFactory
        Get
            If _Instance Is Nothing Then
                _Instance = New ReportFactory()
            End If
            Return _Instance
        End Get
    End Property

    Public Function GetReportInstance(ByVal posReport As String) As IReports
        Try
            Select Case posReport
                Case POSReports.DayCloseReport.ToString()
                    Return New DayCloseReportController()
                Case POSReports.KOTReport.ToString()
                    Return New KOTReport()
                Case POSReports.BankReport.ToString()
                    Return New BankReport()
                Case POSReports.BillWiseReport.ToString()
                    Return New BillWiseReport()
                Case POSReports.GiftVoucherReport.ToString()
                    Return New GiftVoucherReport()
                Case POSReports.CashierWiseSalesReport.ToString()
                    Return New CashierWiseSalesReport()
                Case POSReports.TaxDetailsReport.ToString()
                    Return New SalesTaxDetailsReport()
                Case POSReports.PostingReport.ToString()
                    Return New PostingReport()
                Case POSReports.TimeScheduledSaleReport.ToString()
                    Return New TimeScheduleReport()
                Case POSReports.AdsrReport.ToString()
                    Return New AdsrReport()
                Case POSReports.JKDayOfReport.ToString()
                    Return New DayCloseReportController()
                Case Else
                    Return Nothing
            End Select           
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
