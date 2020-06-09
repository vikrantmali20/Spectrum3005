Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Globalization
Imports System.Windows.Forms
Imports SpectrumBL
Imports C1.Win.C1FlexGrid

Public Class frmFeedbackForm
    Dim objCls As New clsCommon
    Dim dtFeedBackTickets As DataTable
    Dim RatingList As String

    Private Sub frmFeedbackForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CtrlFeedbackGrid.DataSource = dtFeedBackTickets
            TicketGridSeting()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub TicketGridSeting()


        CtrlFeedbackGrid.Cols("TicketId").Width = 130
        CtrlFeedbackGrid.Cols("TicketId").AllowEditing = False
        CtrlFeedbackGrid.Cols("TicketId").Caption = "TicketId"


        CtrlFeedbackGrid.Cols("Department").Width = 92
        CtrlFeedbackGrid.Cols("Department").AllowEditing = False

        CtrlFeedbackGrid.Cols("TicketDescription").Width = 350
        CtrlFeedbackGrid.Cols("TicketDescription").AllowEditing = False
        CtrlFeedbackGrid.Cols("TicketDescription").Caption = "TicketDescription"


        CtrlFeedbackGrid.Cols("UpdatedBy").Width = 80
        CtrlFeedbackGrid.Cols("UpdatedBy").AllowEditing = False
        CtrlFeedbackGrid.Cols("UpdatedBy").Caption = "UpdatedBy"
        CtrlFeedbackGrid.Cols("UpdatedBy").Format = "g"

        'RatingList = "1|2|3|4|5|
        RatingList = "1- Satisfactory|2- Unsatisfactory|"
        If RatingList.Length > 0 Then
            RatingList = RatingList.Substring(0, RatingList.Length - 1)
        End If

        CtrlFeedbackGrid.Cols("Rating").Width = 130
        CtrlFeedbackGrid.Cols("Rating").AllowEditing = True
        CtrlFeedbackGrid.Cols("Rating").Caption = "Rating"
        CtrlFeedbackGrid.Cols("Rating").Selected = True
        CtrlFeedbackGrid.Cols("Rating").ComboList = RatingList


        CtrlFeedbackGrid.Cols("Comment").Width = 100
        CtrlFeedbackGrid.Cols("Comment").AllowEditing = True
        CtrlFeedbackGrid.Cols("Comment").Caption = "Comment"


        CtrlFeedbackGrid.Width = Me.Size.Width

        TblFeedBackLayout.Location = New Point(10, 10)
        TblFeedBackLayout.Size = New Size(Me.Size.Width - 40, Me.Size.Height - 40)
        TblFeedBackLayout.Margin = New Padding(20)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try

            Dim grv As New clsGrievance
            Dim dt As DataTable
            Dim TicketId As New List(Of String)
            dt = dtFeedBackTickets
            Dim isSuccess As Boolean = True
            Dim isCommentEnterd As Boolean = True
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each rows In dt.Rows
                    If String.IsNullOrEmpty(rows("rating")) Then
                        TicketId.Add(rows("TicketId"))
                        isSuccess = False
                    Else
                        If rows("rating") = "2- Unsatisfactory" Then
                            If String.IsNullOrEmpty(rows("comment")) Then
                                isCommentEnterd = False
                            End If
                        End If
                    End If
                Next
            End If

            If isSuccess = False Then
                Dim Result As String = ""
                For Each Ticket As String In TicketId
                    Result = Ticket + "," + Result
                Next
                Result = Result.Substring(0, Result.Length - 1)
                ShowMessage(getValueByKey("GRV016") + Result, "GRV016 - " & "Feedback Information")
            Else
                If isCommentEnterd = False Then
                    ShowMessage("Kindly provide comment for tickets rated as Unsatisfactory", "GRV016 - " & "Feedback Information")
                    Exit Sub
                Else
                    If Not grv.SaveFeedBack(clsAdmin.SiteCode, dt, clsAdmin.UserCode) = True Then
                        Exit Sub
                    End If
                    'code added for jk sprint 24 by vipul
                    Dim dtUnsetisfacory As DataTable

                    If dt.Select("Rating='2- Unsatisfactory'").Count > 0 Then
                        dtUnsetisfacory = dt.Select("Rating='2- Unsatisfactory'").CopyToDataTable()
                        If dtUnsetisfacory.Rows.Count > 0 Then
                            For Each dr As DataRow In dtUnsetisfacory.Rows

                                Dim UpdateGrievanceDetails As DataTable = objCls.GetGrievanceDetailIdWise("", dr("TicketId").ToString())
                                'code added by vipul for issue id 0002904
                                If UpdateGrievanceDetails.Rows(0)("DeptId").ToString.Equals("") Then
                                    Continue For
                                End If
                                Dim RaisedToDepartment As Integer = 43
                                Dim _siteCode As String = UpdateGrievanceDetails.Rows(0)("SiteCode").ToString()
                                Dim _grevinceId As String = UpdateGrievanceDetails.Rows(0)("GrievanceId").ToString()
                                Dim _status As String = UpdateGrievanceDetails.Rows(0)("GrievanceStatus").ToString()
                                Dim _grievanceTypeid As Integer = Convert.ToInt64(UpdateGrievanceDetails.Rows(0)("GrievanceTypeId"))
                                Dim _departmentid As String = Convert.ToInt64(UpdateGrievanceDetails.Rows(0)("DeptId"))
                                Dim _updatedBy As String = clsAdmin.UserName

                                Dim obj As New clsGrievance
                                Dim doEntryForSms As Boolean = False
                                Dim dtMobileNumbers As DataTable = objCls.GetDepartmentMobileNumberByDepartmentId(RaisedToDepartment)
                                If dtMobileNumbers.Rows.Count > 0 Then
                                    doEntryForSms = True
                                End If
                                If obj.UpdateGrievanceDetailsONFeedBack(_siteCode, _grevinceId, _status, _grievanceTypeid, _departmentid, remark:="remark", RaisedToDepartment:=RaisedToDepartment, UpdatedBy:=_updatedBy, userId:=clsAdmin.UserCode, dtMobileNumbers:=dtMobileNumbers, RaisedBy:="", FinYear:=clsAdmin.Financialyear, doEntryForSms:=doEntryForSms) = True Then

                                End If
                            Next
                        End If
                    End If

                End If

                ShowMessage(getValueByKey("GRV017"), "GRV017 - " & getValueByKey("GRV010"))
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Dim eventType As Int32
        ShowMessage(getValueByKey("GRV013"), "CM014 - " & getValueByKey("CLAE04"), eventType, "Cancel", "OK")
        If eventType = 1 Then
            Me.Close()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
        End If
    End Sub

    'Private Sub CtrlFeedbackGrid_MouseMove _
    '(Button As Integer, Shift As Integer, X As Single, Y As Single)

    '    On Error Resume Next

    '    With CtrlFeedbackGrid
    '        .ToolTipText = .TextMatrix(.MouseRow, .MouseCol)
    '    End With

    'End Sub

    Private Sub CtrlFeedbackGrid_MouseMove(sender As Object, e As MouseEventArgs) Handles CtrlFeedbackGrid.MouseMove
        'Dim row = CtrlFeedbackGrid.Row()
        'Dim col = CtrlFeedbackGrid.Col()
        'If col = 2 Then
        '    Dim tl As New ToolTip()
        '    tl.SetToolTip(CtrlFeedbackGrid, " Private Sub CtrlFeedbackGrid_MouseMove (Button As Integer, Shift As Integer, X As Single, Y As Single)" _
        '                  & "On Error Resume Next ")
        '    'End SubI Am Here")
        '    'MsgBox("I Am Here")
        '    With CtrlFeedbackGrid
        '        '.ToolTipText = .TextMatrix(.MouseRow, .MouseCol)
        '    End With
        'End If

      
    End Sub

    Public Sub New(ByRef dtFeedbackData As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dtFeedBackTickets = dtFeedbackData
    End Sub

   
    Private Sub frmFeedbackForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TblFeedBackLayout.Location = New Point(10, 10)
        TblFeedBackLayout.Size = New Size(Me.Size.Width - 40, Me.Size.Height - 40)
        TblFeedBackLayout.Margin = New Padding(30)
    End Sub

    Private Sub CtrlFeedbackGrid_SetupEditor(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlFeedbackGrid.SetupEditor
        If e.Col = 6 Then
            Dim textBox As TextBox = CType(Me.CtrlFeedbackGrid.Editor, TextBox)
            textBox.MaxLength = 250
        End If
    End Sub

    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        CtrlFeedbackGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        CtrlFeedbackGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        CtrlFeedbackGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        CtrlFeedbackGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        CtrlFeedbackGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlFeedbackGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlFeedbackGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlFeedbackGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlFeedbackGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)


        ctrlbl.ForeColor = Color.White
        ctrlbl.AutoSize = False
        ctrlbl.Size = New Size(600, 16)
        ctrlbl.SendToBack()
        ctrlbl.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ctrlbl.TextAlign = ContentAlignment.MiddleLeft
        ctrlbl.BackColor = Color.Transparent
        'ctrlbl.BackColor = Color.FromArgb(212, 212, 212)

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        cmdCancel.Size = New Size(80, 30)

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdSave.TextAlign = ContentAlignment.MiddleCenter
        cmdSave.Size = New Size(80, 30)



    End Function

    Private Sub CtrlFeedbackGrid_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles CtrlFeedbackGrid.AfterEdit
        Try
            If CtrlFeedbackGrid.Cols(e.Col).Name = "Rating" Then
                If CtrlFeedbackGrid.Rows(e.Row)("Rating") = "2- Unsatisfactory" Then
                    ShowMessage("What could we have done better ? ", "GRV016 - " & "Feedback Information")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class