Imports System.Text
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports Microsoft.PointOfService
Imports System.Drawing
Imports System.IO

Public Class clsFiscalPrinting
    Shared _FiscalPrinterName As String
    Shared Property FiscalPrinterName() As String
        Get
            Return _FiscalPrinterName
        End Get
        Set(ByVal value As String)
            _FiscalPrinterName = value
        End Set
    End Property
    Public Shared Function ReadSpectrumParamFile(ByVal strLabel As String) As String
        Dim strValue As String = ""
        Try
            If File.Exists(Application.StartupPath & "\SpectrumParam") Then
                Dim dtData As New DataSet
                dtData.ReadXml("SpectrumParam")
                For i As Integer = 0 To dtData.Tables(0).Rows.Count - 1
                    If UCase(dtData.Tables(0).Rows(i)("Label")) = UCase(strLabel) Then
                        strValue = Encrypt(dtData.Tables(0).Rows(i)("Value"))
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strValue = ""
        End Try
        Return strValue
    End Function
    Public Shared Sub fnFiscalPrint(ByVal StrToPrint As String)
        'Add for fiscal printer

        Try
            Dim cA4Print As New clsA4Print
            cA4Print.OperateDevice("FiscalPrinter", StrToPrint)
        Catch ex As Exception

        End Try
        'Add for fiscal printer
    End Sub

End Class
