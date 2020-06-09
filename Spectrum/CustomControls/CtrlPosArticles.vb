Imports SpectrumBL
Imports System.Drawing.Drawing2D
Imports System
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

Public Class CtrlPosArticles

    Dim objComm As New clsCommon
    Public Sub New(_ArticleCode)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetArticleCode = _ArticleCode

        ' Me.Location = New Point(-2, -2)
        ' Me.Size = New Size(156, 60)
        ' Me.MinimumSize = New Size(259, 150)
        Me.BackColor = Color.White

        Me.picBxArticlePictrue.BringToFront()
        ' Me.picBxArticlePictrue.Location = New Point(3, 3)
        ' Me.picBxArticlePictrue.Size = New Size(50, 50)
        Me.picBxArticlePictrue.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        Me.picBxArticlePictrue.SizeMode = PictureBoxSizeMode.StretchImage
        ShowArticleImage(SetArticleCode, picBxArticlePictrue)

        ' Me.lblArticleName.Location = New Point(55, 15)
        'Me.lblArticleCode.Location = New Point(55, 25)
        ' Me.lblArticleAdditionalInfo.Location = New Point(55, 35)
        'CtrlbtnDeleteArticle.Location = New Point(135, -3) 'final size
        'CtrlbtnDeleteArticle.Location = New Point(128, -2)
        ' CtrlbtnDeleteArticle.Location = New Point(128, -2)
        'CtrlbtnDeleteArticle.Location = New Point(70, 3)
        '  Me.CtrlbtnDeleteArticle.Size = New System.Drawing.Size(25, 25)
        'Me.CtrlbtnDeleteArticle.ImageAlign = ContentAlignment.MiddleCenter
        'Me.CtrlbtnDeleteArticle.TextImageRelation = TextImageRelation.Overlay

        'CtrlbtnDeleteArticle.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        ''CtrlbtnDeleteArticle.BackColor = Color.FromArgb(228, 37, 44)
        'CtrlbtnDeleteArticle.BackColor = Color.White
        'CtrlbtnDeleteArticle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'CtrlbtnDeleteArticle.FlatStyle = FlatStyle.Flat
        'CtrlbtnDeleteArticle.FlatAppearance.BorderSize = 0
        'CtrlbtnDeleteArticle.FlatAppearance.BorderColor = Color.Black

        Dim gp As New Drawing.Drawing2D.GraphicsPath
        Dim rect As New Rectangle
        rect.Location = New Point(6, 6)
        rect.Size = New Size(26, 26)

        rect.Inflate(-2, -2)
        gp.AddEllipse(rect)
        '  gp.AddEllipse(rect(New Point(3, 3), New Size(25, 26)).Inflate(-5, -5))
        'CtrlbtnDeleteArticle.Region = New Region(gp)
        'Me.CtrlbtnDeleteArticle.Text = ""
        'Me.CtrlbtnDeleteArticle.Image = Global.Spectrum.My.Resources.Exit_Hover

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private _SetArticleCode As String
    Public Property SetArticleCode() As String
        Get
            Return _SetArticleCode
        End Get
        Set(ByVal value As String)
            _SetArticleCode = value
        End Set
    End Property
    Public Sub ShowArticleImage(ByVal StrArticle As String, ByRef picBxArticle As PictureBox)
        Try
            Dim isImageNotAvailable = False
            Dim url As String
            Dim objComm As New clsCommon
            url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            If url <> String.Empty AndAlso url <> "c:\images\" Then
                Dim img As Image
                Dim ratio As Double
                Dim s As Size
                picBxArticle.Image = Nothing
CallByNoImage:  If picBxArticle.Width > 0 And picBxArticle.Height > 0 Then
                    Try
                        If isImageNotAvailable = True Then
                            'url = "c:\images\NoImage.jpg"
                            img = Global.Spectrum.My.Resources.NoImage
                        Else
                            img = Image.FromFile(url)
                        End If
                        ratio = img.Height / img.Width
                        s.Width = picBxArticle.Width
                        s.Height = s.Width * ratio
                        If s.Height > picBxArticle.Height Then
                            ratio = picBxArticle.Height / s.Height
                            s.Width = s.Width * ratio
                            s.Height = s.Height * ratio + 5
                        End If
                        If s.Width > 0 And s.Height > 0 Then
                            picBxArticle.Image = New Bitmap(img, s)
                        End If
                        img.Dispose()
                    Catch ex As Exception

                    End Try
                End If
            Else
                picBxArticle.Image = Nothing
                isImageNotAvailable = True
                GoTo CallByNoImage
            End If
        Catch ex As Exception

        End Try
    End Sub








End Class

