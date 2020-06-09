Imports SpectrumCommon
Imports iTextSharp.text
Public Interface IDayCloseReport
    Sub CreateReport(ByRef request As DayCloseReportModel, ByRef doc As Document)
End Interface
