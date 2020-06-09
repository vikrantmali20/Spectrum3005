Imports SpectrumCommon
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class FloatingDetails
    Inherits ReportBase
    Implements IDayCloseReport

    Private _DtOpenFloatDtls As DataTable
    Public Property DtOpenFloatDtls() As DataTable
        Get
            Return _DtOpenFloatDtls
        End Get
        Set(ByVal value As DataTable)
            _DtOpenFloatDtls = value
        End Set
    End Property

    Private _DtCloseFloatDtls As DataTable
    Public Property DtcloseFloatDtls() As DataTable
        Get
            Return _DtCloseFloatDtls
        End Get
        Set(ByVal value As DataTable)
            _DtCloseFloatDtls = value
        End Set
    End Property

    Private _DtNextDayFloatDtls As DataTable
    Public Property DtNextDayFloatDtls() As DataTable
        Get
            Return _DtNextDayFloatDtls
        End Get
        Set(ByVal value As DataTable)
            _DtNextDayFloatDtls = value
        End Set
    End Property

    Private _DtBankDetails As DataTable
    Public Property DtBankDetails() As DataTable
        Get
            Return _DtBankDetails
        End Get
        Set(ByVal value As DataTable)
            _DtBankDetails = value
        End Set
    End Property

    Public Sub CreateReport(ByRef request As SpectrumCommon.DayCloseReportModel, ByRef doc As iTextSharp.text.Document) Implements IDayCloseReport.CreateReport
        Try
            doc.Add(New Paragraph("Till Open Float Details", GetContentFontBold()) With {.Alignment = 1})
            Call DisplayFloatDetails(DtOpenFloatDtls, doc)

            doc.Add(New Paragraph("Till Close Float Details", GetContentFontBold()) With {.Alignment = 1})
            Call DisplayFloatDetails(DtcloseFloatDtls, doc)

            doc.Add(New Paragraph("Next Day Float Details", GetContentFontBold()) With {.Alignment = 1})
            Call DisplayFloatDetails(DtNextDayFloatDtls, doc)

            doc.Add(New Paragraph("Bank Float Details", GetContentFontBold()) With {.Alignment = 1})
            'tblSumValue = DtBankDetails.Compute("SUM([Total Amount])", "")
            'modified by vipul to avoid null values error
            tblSumValue = IIf(DtBankDetails.Compute("SUM([Total Amount])", "") Is DBNull.Value, 0, DtBankDetails.Compute("SUM([Total Amount])", ""))
            Call PrintDataTable(DtBankDetails, doc)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Sub DisplayFloatDetails(dt As DataTable, ByRef doc As iTextSharp.text.Document)
        Try
            If dt.Rows.Count = 0 Then
                PrintDataTable(dt, doc)
            Else
                Dim dv As New DataView(dt, "", "", DataViewRowState.CurrentRows)
                Dim dtUnique As DataTable = dv.ToTable(True, "Terminal No.")
                Dim isHidetableCols As Boolean = False
                isDrawStroke = False
                Dim rowcounter As Integer = 0
                Dim RunningTotal As Double = 0
                For Each dr As DataRow In dtUnique.Rows
                    rowcounter = rowcounter + 1
                    Dim dvfloatDtls As New DataView(dt, "[Terminal No.]='" & dr("Terminal No.") & "'", "", DataViewRowState.CurrentRows)

                    If dt.Rows.Count > 0 Then
                        tblSumValue = dvfloatDtls.ToTable.Compute("SUM([Total Amount])", "")
                        RunningTotal += tblSumValue
                    End If
                    If dtUnique.Rows.Count = rowcounter Then
                        tblRunningGrandTotal = RunningTotal
                    End If

                    HideTableColumn = isHidetableCols
                    isHidetableCols = True
                    PrintDataTable(dvfloatDtls.ToTable, doc)
                Next dr
                tblSumValue = 0 : tblRunningGrandTotal = 0
                DrawStrokedLine(doc)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function GetTillOpenFloatingDetaills(ByRef request As DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select MTI.TerminalName AS Till , FloatingDetail.CurrencyCode As [Currency Code] ,FloatingDetail.DenominationAmt As [Denomination Amount] ,FloatingDetail.Qty As [Quantity],FloatingDetail.TotalAmount As [Total Amount] " & _
               " from FloatingDetail Inner Join MstTerminalID AS MTI on FloatingDetail.TerminalID = MTI.TerminalID  " & _
               " where FloatingDetail.SiteCode = '" & request.SiteCode & "' and FloatingDetail.Action = '" & DayCloseAction.ExtraOpen.ToString() & "' and FloatingDetail.flotDatetime ='" & request.ToDate.ToString("yyyy-MM-dd") & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetTillCloseFloatingDetaills(ByRef request As DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select CurrencyCode As [Currency Code] ,DenominationAmt As [Denomination Amount] ,Sum(Qty) As [Quantity], Sum(TotalAmount) As [Total Amount] " & _
               "from FloatingDetail where SiteCode = '" & request.SiteCode & "' and Action = '" & DayCloseAction.Close.ToString() & "' and flotDatetime ='" & request.ToDate.ToString("yyyy-MM-dd") & "'  group by CurrencyCode,DenominationAmt"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetNextDayFloatingDetaills(ByRef request As DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select MTI.TerminalName AS Till , FloatingDetail.CurrencyCode As [Currency Code] ,FloatingDetail.DenominationAmt As [Denomination Amount] ,FloatingDetail.Qty As [Quantity],FloatingDetail.TotalAmount As [Total Amount] " & _
             " from FloatingDetail Inner Join MstTerminalID AS MTI on FloatingDetail.TerminalID = MTI.TerminalID  " & _
             " Where FloatingDetail.SiteCode = '" & request.SiteCode & "' and FloatingDetail.Action = '" & DayCloseAction.Open.ToString() & "' and FloatingDetail.flotDatetime ='" & request.ToDate.ToString("yyyy-MM-dd") & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetBankDetailsFloatingDetaills(ByRef request As DayCloseReportModel) As DataTable
        Try
            Dim query As String = "select CurrencyCode As [Currency Code] ,DenominationAmt As [Denomination Amount] ,Qty As [Quantity],TotalAmount As [Total Amount] " & _
               "from DayCloseBankDetails where SiteCode = '" & request.SiteCode & "' and CloseDate = '" & request.ToDate.ToString("yyyy-MM-dd") & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
