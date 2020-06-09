Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
''' <summary>
''' Summary description for BitmapRegion.
''' </summary>
Public Class BitmapRegion
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Create and apply the region on the supplied control
    ''' </summary>
    ''' <param name="control">The Control object to apply the region to</param>
    ''' <param name="bitmap">The Bitmap object to create the region from</param>
    Public Shared Sub CreateControlRegion(ByVal control As Control, ByVal bitmap As Bitmap)
        ' Return if control and bitmap are null
        If control Is Nothing OrElse bitmap Is Nothing Then
            Exit Sub
        End If

        ' Set our control's size to be the same as the bitmap
        control.Width = bitmap.Width
        control.Height = bitmap.Height
    

        ' Check if we are dealing with Form here
        If TypeOf control Is System.Windows.Forms.Form Then
            ' Cast to a Form object
            Dim form As Form = DirectCast(control, Form)

            ' Set our form's size to be a little larger that the bitmap just 
            ' in case the form's border style is not set to none in the first place
            form.Width += 15
            form.Height += 35

            ' No border
            form.FormBorderStyle = FormBorderStyle.None

            ' Set bitmap as the background image
            form.BackgroundImage = bitmap

            ' Calculate the graphics path based on the bitmap supplied
            Dim graphicsPath As GraphicsPath = CalculateControlGraphicsPath(bitmap)

            ' Apply new region
            form.Region = New Region(graphicsPath)

            ' Check if we are dealing with Button here
        ElseIf TypeOf control Is Spectrum.CtrlBtn Then
            ' Cast to a button object
            Dim button As Button = DirectCast(control, Button)

            ' Do not show button text
            button.Text = ""

            ' Change cursor to hand when over button
            button.Cursor = Cursors.Hand

            ' Set background image of button
            button.BackgroundImage = bitmap

            ' Calculate the graphics path based on the bitmap supplied
            Dim graphicsPath As GraphicsPath = CalculateControlGraphicsPath(bitmap)

            ' Apply new region
            button.Region = New Region(graphicsPath)
        End If
    End Sub

    ''' <summary>
    ''' Calculate the graphics path that representing the figure in the bitmap 
    ''' excluding the transparent color which is the top left pixel.
    ''' </summary>
    ''' <param name="bitmap">The Bitmap object to calculate our graphics path from</param>
    ''' <returns>Calculated graphics path</returns>
    Public Shared Function CalculateControlGraphicsPath(ByVal bitmap As Bitmap) As GraphicsPath
        ' Create GraphicsPath for our bitmap calculation
        Dim graphicsPath As New GraphicsPath()

        ' Use the top left pixel as our transparent color
        Dim colorTransparent As Color = bitmap.GetPixel(0, 0)

        ' This is to store the column value where an opaque pixel is first found.
        ' This value will determine where we start scanning for trailing opaque pixels.
        Dim colOpaquePixel As Integer = 0

        ' Go through all rows (Y axis)
        For row As Integer = 0 To bitmap.Height - 1
            ' Reset value
            colOpaquePixel = 0

            ' Go through all columns (X axis)
            For col As Integer = 0 To bitmap.Width - 1
                ' If this is an opaque pixel, mark it and search for anymore trailing behind
                If bitmap.GetPixel(col, row) <> colorTransparent Then
                    ' Opaque pixel found, mark current position
                    colOpaquePixel = col

                    ' Create another variable to set the current pixel position
                    Dim colNext As Integer = col

                    ' Starting from current found opaque pixel, search for anymore opaque pixels 
                    ' trailing behind, until a transparent pixel is found or minimum width is reached
                    For colNext = colOpaquePixel To bitmap.Width - 1
                        If bitmap.GetPixel(colNext, row) = colorTransparent Then
                            Exit For
                        End If
                    Next

                    ' Form a rectangle for line of opaque pixels found and add it to our graphics path
                    graphicsPath.AddRectangle(New Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1))

                    ' No need to scan the line of opaque pixels just found
                    col = colNext
                End If
            Next
        Next

        ' Return calculated graphics path
        Return graphicsPath
    End Function
End Class
