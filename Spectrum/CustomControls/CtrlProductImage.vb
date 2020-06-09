Imports SpectrumBL
Public Class CtrlProductImage
    Dim objComm As New clsCommon
    Public Sub ShowArticleImage(ByVal StrArticle As String)
        Try
            Dim url As String
            url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            If url <> String.Empty Then
                Dim img As Image
                Dim ratio As Double
                Dim s As Size

                CtrlProductImages.BackgroundImage = Nothing
                CtrlProductImages.BackgroundImageLayout = Nothing

                If CtrlProductImages.Width > 0 And CtrlProductImages.Height > 0 Then
                    Try
                        img = Image.FromFile(url)
                        ratio = img.Height / img.Width
                        s.Width = CtrlProductImages.Width
                        s.Height = s.Width * ratio
                        If s.Height > CtrlProductImages.Height Then
                            ratio = CtrlProductImages.Height / s.Height
                            s.Width = s.Width * ratio
                            s.Height = s.Height * ratio
                        End If
                        If s.Width > 0 And s.Height > 0 Then
                            CtrlProductImages.BackgroundImage = New Bitmap(img, s)
                            CtrlProductImages.BackgroundImageLayout = ImageLayout.Center
                        End If
                        img.Dispose()
                    Catch ex As Exception

                    End Try
                End If
            Else
                CtrlProductImages.BackgroundImage = My.Resources.NA
                CtrlProductImages.BackgroundImageLayout = ImageLayout.Center
                CtrlProductImages.Image = Nothing

            End If
            'CtrlProductImages.BackgroundImage = Image.FromFile(url)

            'CtrlProductImages.BackgroundImageLayout = ImageLayout.Center
            'CtrlProductImages.SizeMode = PictureBoxSizeMode.AutoSize


        Catch ex As Exception
            CtrlProductImages.BackgroundImage = My.Resources.NA
            CtrlProductImages.Image = Nothing
        End Try
    End Sub
    Public Sub ClearImage()
        CtrlProductImages.BackgroundImage = Nothing
        CtrlProductImages.Image = Nothing
    End Sub
   
End Class
