Imports SpectrumCommon
Imports System.Linq
Public Class CtrlPagination  
    Public Delegate Function PageChangedHandler(ByVal paginationData As PaginationData) As Boolean
    Public PageChanged As PageChangedHandler
    'Public PageChanged As Action(Of PaginationData)
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _TotalRecords As Integer
    Public Property TotalRecords As Integer
        Get
            Return _TotalRecords
        End Get
        Set(ByVal value As Integer)
            _TotalRecords = value
        End Set
    End Property

    Private _LastActivePageNo As Integer = 1
    Public Property LastActivePageNo As Integer
        Get
            Return _LastActivePageNo
        End Get
        Set(ByVal value As Integer)
            _LastActivePageNo = value
        End Set
    End Property

    Private Sub CtrlPagination_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub CalculateTotalPages(ByVal noOfRecords As Integer)
        Try
            TotalRecords = noOfRecords
            For i As Integer = 1 To Math.Ceiling(noOfRecords / DayCloseConstants.RecordsPerPage) Step 1
                Dim lblNumber As New Label
                lblNumber.TextAlign = ContentAlignment.MiddleCenter
                lblNumber.BackColor = ColorTranslator.FromOle(RGB(164, 195, 235))
                lblNumber.Anchor = AnchorStyles.Left
                lblNumber.ForeColor = Color.White
                lblNumber.Text = i.ToString()
                lblNumber.Tag = i
                lblNumber.AutoSize = True                
                lblNumber.Font = New Font("Microsoft Sans Serif", 10.25, FontStyle.Underline Or FontStyle.Bold)
                AddHandler lblNumber.Click, AddressOf PageNumberClicked
                flpPageNumbers.Controls.Add(lblNumber)
            Next
            If flpPageNumbers.Controls.Count > 0 Then
                PageNumberClicked(flpPageNumbers.Controls(0), New EventArgs())
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub RecalulateTotalPages(ByVal noOfRecords As Integer)
        TotalRecords = noOfRecords
        For Each Label As Label In flpPageNumbers.Controls
            RemoveHandler Label.Click, AddressOf PageNumberClicked
        Next
        flpPageNumbers.Controls.Clear()
        Try
            For i As Integer = 1 To Math.Ceiling(noOfRecords / DayCloseConstants.RecordsPerPage) Step 1
                Dim lblNumber As New Label
                lblNumber.TextAlign = ContentAlignment.MiddleCenter
                lblNumber.BackColor = ColorTranslator.FromOle(RGB(164, 195, 235))
                lblNumber.Anchor = AnchorStyles.Left
                lblNumber.ForeColor = Color.White
                lblNumber.Text = i.ToString()
                lblNumber.Tag = i
                lblNumber.AutoSize = True
                lblNumber.Font = New Font("Microsoft Sans Serif", 10.25, FontStyle.Underline Or FontStyle.Bold)
                AddHandler lblNumber.Click, AddressOf PageNumberClicked
                flpPageNumbers.Controls.Add(lblNumber)
            Next
            If LastActivePageNo > flpPageNumbers.Controls.Count Then
                LastActivePageNo = flpPageNumbers.Controls.Count
            End If
            Dim lastPage As Label = (From lbl As Label In flpPageNumbers.Controls Where lbl.Tag = LastActivePageNo Select lbl).FirstOrDefault()
            If lastPage IsNot Nothing Then
                lastPage.ForeColor = Color.Black
            Else               
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PageNumberClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If PageChanged IsNot Nothing Then
                Dim pageNumberData As New PaginationData
                Dim label As Label = DirectCast(sender, Label)
                pageNumberData.EndNumber = label.Tag * DayCloseConstants.RecordsPerPage
                If pageNumberData.EndNumber > TotalRecords Then
                    pageNumberData.EndNumber = TotalRecords
                End If
                pageNumberData.StartNumber = (label.Tag * DayCloseConstants.RecordsPerPage) - (DayCloseConstants.RecordsPerPage - 1)
                If (PageChanged(pageNumberData)) Then
                    Dim lastPage As Label = (From lbl As Label In flpPageNumbers.Controls Where lbl.Tag = LastActivePageNo Select lbl).FirstOrDefault()
                    If lastPage IsNot Nothing Then
                        'lastPage.ForeColor = Color.Green
                        lastPage.ForeColor = Color.White
                    End If
                    label.ForeColor = Color.Black
                    LastActivePageNo = label.Tag
                Else

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class PaginationData
    Private _StartNumber As Integer
    Public Property StartNumber As Integer
        Get
            Return _StartNumber
        End Get
        Set(ByVal value As Integer)
            _StartNumber = value
        End Set
    End Property

    Private _EndNumber As Integer
    Public Property EndNumber As Integer
        Get
            Return _EndNumber
        End Get
        Set(ByVal value As Integer)
            _EndNumber = value
        End Set
    End Property
End Class
