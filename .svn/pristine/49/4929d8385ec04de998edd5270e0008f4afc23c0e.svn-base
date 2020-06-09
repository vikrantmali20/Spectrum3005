Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class PdfHelper
    Private Sub New()

    End Sub

    Private Shared _Instance As PdfHelper
    Public Shared ReadOnly Property Instance As PdfHelper
        Get
            If _Instance Is Nothing Then
                _Instance = New PdfHelper()
            End If
            Return _Instance
        End Get
    End Property

    Public Function GetPdfDocument() As Document
        Try
            Return New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreatePdfWriter(ByVal doc As Document, ByVal path As String) As PdfWriter
        Try
            Return PdfWriter.GetInstance(doc, New FileStream(path, FileMode.Create))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
