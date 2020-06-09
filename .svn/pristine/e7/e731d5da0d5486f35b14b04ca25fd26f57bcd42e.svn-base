

Public Class ComboDiscountCalculator    
    Public Sub New()

    End Sub

    Public Function GetNetPrice(ByVal quantity As Double, ByVal comboStepCost As Double) As Double
        Try
            If quantity <> 0 Then
                Return comboStepCost / quantity
            End If
            Return 0.0
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

End Class
