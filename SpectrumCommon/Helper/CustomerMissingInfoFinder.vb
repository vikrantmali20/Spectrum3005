Public Class CustomerMissingInfoFinder
    Private Sub New()

    End Sub

    Private ReadOnly CustomerInfoList As Dictionary(Of String, String) = New Dictionary(Of String, String)() From {{"RESPHONE", "Residential Number"}, {"OFFICENO", "Office Number"}, {"STATE", "State"}, {"BIRTHDATE", "Birth Date"}}

    Private Shared _Instance As CustomerMissingInfoFinder
    Public Shared ReadOnly Property Instance As CustomerMissingInfoFinder
        Get
            If _Instance Is Nothing Then
                _Instance = New CustomerMissingInfoFinder()
            End If
            Return _Instance
        End Get
    End Property



    Public Function FindMissingParameter(ByVal dtCustomerInfo As DataTable) As String
        Try            
            Dim returnString As String = String.Empty
            If dtCustomerInfo Is Nothing OrElse dtCustomerInfo.Rows.Count = 0 Then
                Return String.Empty
            End If
            For i As Integer = 0 To CustomerInfoList.Count - 1 Step 1
                If TypeOf dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) Is DateTime OrElse TypeOf dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) Is System.DBNull Then
                    If IsDBNull(dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i))) OrElse dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) = DateTime.MinValue OrElse dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) = DateTime.MaxValue Then
                        returnString += CustomerInfoList(CustomerInfoList.Keys(i)) & ", "
                        'Exit For
                    End If
                ElseIf TypeOf dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) Is String OrElse TypeOf dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i)) Is System.DBNull Then
                    If IsDBNull(dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i))) OrElse String.IsNullOrEmpty(dtCustomerInfo.Rows(0)(CustomerInfoList.Keys(i))) Then
                        returnString += CustomerInfoList(CustomerInfoList.Keys(i)) & ", "
                        'Exit For
                    End If
                End If
            Next
            If returnString.Length > 0 Then
                returnString = returnString.Remove(returnString.Count - 2)
            End If
            Return returnString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
End Class
