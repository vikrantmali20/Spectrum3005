Imports System.Windows.Forms

Public Class CustomPrintPreviewDialog
    Inherits System.Windows.Forms.PrintPreviewDialog

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Me.Close()
            ' Me.Dispose()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class
