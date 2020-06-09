Imports SpectrumBL
Imports System.Data.SqlClient

Public Class frmCardSummary

#Region " Global Varibale for class"
    Dim objSite As New clsCommon
    Dim objShift As New clsShift

#End Region

    Public Shared _SummaryAmt As String
    Public Shared Property SummaryAmt() As String
        Get
            Return _SummaryAmt
        End Get
        Set(value As String)
            _SummaryAmt = value
        End Set
    End Property
    ''added by nikhil
    Public Shared _frmCancel As Boolean = False
    Public Shared Property frmCancel() As Boolean
        Get
            Return _frmCancel
        End Get
        Set(value As Boolean)
            _frmCancel = value
        End Set
    End Property

    Public Shared _ShiftId As Integer
    Public Shared Property ShiftId() As Integer
        Get
            Return _ShiftId
        End Get
        Set(value As Integer)
            _ShiftId = value
        End Set
    End Property

    Public Shared _DtTender As DataTable
    Public Shared Property DtTender() As DataTable
        Get
            Return _DtTender
        End Get
        Set(value As DataTable)
            _DtTender = value
        End Set
    End Property

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        Try
            txtSummaryAmt.Text = "0"
        Catch ex As Exception
            LogException(ex)

        End Try
    End Sub


    Public Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Try

            Me.Close()
            frmCancel = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        Try
            Dim objShiftClosingPC As New frmShiftClosingPC
            Dim DbtAmt As Double = SummaryAmt
            Dim amtPos As Double
            Dim amtShow As Double
            Dim Incon As Boolean = False
            Dim tran As SqlTransaction
            Dim eventType As Int32
            objShiftClosingPC.CardAmt = txtSummaryAmt.Text.ToString().Trim() 'vipin

            If txtSummaryAmt.Text < DbtAmt Then
                amtShow = Convert.ToDouble(txtSummaryAmt.Text - SummaryAmt) * (-1)
                Incon = True
                If objShift.InsertIncosDetail(Incon, clsAdmin.SiteCode, clsAdmin.TerminalID, "TillClose", clsAdmin.DayOpenDate, amtShow, ShiftId, clsAdmin.UserName) = True Then  '', tran

                    'MsgBox(vbCrLf & "Card Summary Inconsistency of Rs." & amtShow & "", MsgBoxStyle.OkOnly, " ")
                    ShowMessage(vbCrLf & vbCrLf & "Card Summary Inconsistency of Rs." & amtShow & " ", "Information")
                End If
            ElseIf txtSummaryAmt.Text > DbtAmt Then
                amtPos = Convert.ToDouble(txtSummaryAmt.Text - SummaryAmt) * (-1)
                Incon = True
                If objShift.InsertIncosDetail(Incon, clsAdmin.SiteCode, clsAdmin.TerminalID, "TillClose", clsAdmin.DayOpenDate, amtPos, ShiftId, clsAdmin.UserCode) = True Then   '' , tran
                    'ShowMessage(vbCrLf & vbCrLf & "Card Summary Inconsistency of Rs." & amtPos & "                        ", "")
                    'MsgBox(vbCrLf & "Card Summary Inconsistency of Rs." & amtPos & "", MsgBoxStyle.OkOnly, " ")
                    ShowMessage(vbCrLf & vbCrLf & "Card Summary Inconsistency of Rs." & amtPos & " ", "Information")

                End If
            ElseIf txtSummaryAmt.Text = DbtAmt Then
                amtPos = 0
                Incon = False
                objShift.InsertIncosDetail(Incon, clsAdmin.SiteCode, clsAdmin.TerminalID, "TillClose", clsAdmin.DayOpenDate, amtPos, ShiftId, clsAdmin.UserCode)
            End If
            Me.Close()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmCardSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        txtSummaryAmt.Text = "0"
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Try

            '  dTender.Columns.Add("Amt", GetType(Integer))
            Dim ObjPrint As New frmNReprint
            Dim dtTenderCopy As DataTable = DtTender.Copy()
            If txtSummaryAmt.Text.ToString().Trim() <> "" Then
                dtTenderCopy.Rows.Add("Card Summary as Per EDC", IIf((txtSummaryAmt.Text.ToString().Trim() <> ""), txtSummaryAmt.Text.ToString().Trim(), 0))
            End If

            Dim dsSummary As DataSet = objSite.GetSummaryCardData(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, ShiftId)
            dsSummary.Tables.Add(dtTenderCopy)

            Dim dr As DataRow() = dtTenderCopy.Select("TENDERTYPE='Card Summary as Per EDC'")
            Dim newRow As DataRow = dtTenderCopy.NewRow()
            ' We "clone" the row
            newRow.ItemArray = dr(0).ItemArray
            ' We remove the old and insert the new
            dsSummary.Tables(1).Rows.Remove(dr(0))
            dsSummary.Tables(1).Rows.InsertAt(newRow, 0)

            ' DtTender.TableName("CardSummay")

            Dim daAmount As DataTable = DtTender.Select("TENDERTYPE = 'CreditCard'").CopyToDataTable()
            Dim TnderAmount As Double = Convert.ToInt32(DtTender.Compute("Sum(AMOUNTTENDERED)", "TENDERTYPE = 'CreditCard'"))
            Dim DiffAmt As String = txtSummaryAmt.Text - TnderAmount
           
            ObjPrint.GenerateCardSummaryReportPrint(dsSummary, clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, ShiftId)
            dtTenderCopy.Clear()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdNext.BackColor = Color.Transparent
        cmdNext.BackColor = Color.FromArgb(0, 107, 163)
        cmdNext.ForeColor = Color.FromArgb(255, 255, 255)
        cmdNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdNext.FlatStyle = FlatStyle.Flat
        cmdNext.FlatAppearance.BorderSize = 0
        cmdNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPrint.BackColor = Color.Transparent
        cmdPrint.BackColor = Color.FromArgb(0, 107, 163)
        cmdPrint.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPrint.FlatStyle = FlatStyle.Flat
        cmdPrint.FlatAppearance.BorderSize = 0
        cmdPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        cmdClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdClear.BackColor = Color.Transparent
        cmdClear.BackColor = Color.FromArgb(0, 107, 163)
        cmdClear.ForeColor = Color.FromArgb(255, 255, 255)
        cmdClear.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdClear.FlatStyle = FlatStyle.Flat
        cmdClear.FlatAppearance.BorderSize = 0
        cmdClear.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdPrint.BackColor = Color.Transparent
        cmdPrint.BackColor = Color.FromArgb(0, 107, 163)
        cmdPrint.ForeColor = Color.FromArgb(255, 255, 255)
        cmdPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdPrint.FlatStyle = FlatStyle.Flat
        cmdPrint.FlatAppearance.BorderSize = 0
        cmdPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        txtSummaryAmt.AutoSize = False
        txtSummaryAmt.BackColor = Color.FromArgb(212, 212, 212)
        txtSummaryAmt.Size = New Size(270, 23)
        txtSummaryAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.BackColor = Color.Transparent


    End Function

    Private Sub txtSummaryAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSummaryAmt.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtSummaryAmt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSummaryAmt.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdNext_Click(sender, e)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class