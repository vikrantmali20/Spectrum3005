Imports SpectrumBL

Public Class CtrlPatientImage
    Dim objComm As New clsCommon
    Public Sub ShowCustomerImage(ByVal StrCustomerNo As String)
        Try
            Dim url As String
            url = objComm.GetCustomerImage(StrCustomerNo, clsAdmin.PatientImagePath)
            PicBoxImages.BackgroundImage = Image.FromFile(url)
            PicBoxImages.BackgroundImageLayout = ImageLayout.Center
        Catch ex As Exception
            PicBoxImages.BackgroundImage = My.Resources.NA
            PicBoxImages.Image = Nothing
            Dim strAge As String = "0"
            Dim strGender As String = "0"
            objComm.GetPatientAge(StrCustomerNo, strAge, strGender)
            If Val(strAge) > 15 Then
                If strGender = "1" Then
                    ' PicBoxImages.BackgroundImage = My.Resources.user_male
                Else
                    ' PicBoxImages.BackgroundImage = My.Resources.user_female
                End If
            ElseIf Val(strAge) > 1 And Val(strAge) <= 15 Then
                'PicBoxImages.BackgroundImage = My.Resources.toddler_256
            Else
                PicBoxImages.BackgroundImage = My.Resources.NA
                PicBoxImages.Image = Nothing
            End If
        End Try
    End Sub

    Public Sub ClearImage()
        PicBoxImages.BackgroundImage = Nothing
        PicBoxImages.Image = Nothing
    End Sub

End Class
