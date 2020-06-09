Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Public Class frmViewVoucher
    Public Sub New()
        BLInstance = New PettyCashVoucher()       
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _Response As ViewVoucherResponse
    Public Property Response As ViewVoucherResponse
        Get
            Return _Response
        End Get
        Set(ByVal value As ViewVoucherResponse)
            _Response = value
        End Set
    End Property

    Private _BLInstance As IPettyCashVoucher
    Public Property BLInstance As IPettyCashVoucher
        Get
            Return _BLInstance
        End Get
        Set(ByVal value As IPettyCashVoucher)
            _BLInstance = value
        End Set
    End Property

    Private _GridSource As DataView
    Public Property GridSource As DataView
        Get
            Return _GridSource
        End Get
        Set(ByVal value As DataView)
            _GridSource = value
        End Set
    End Property

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            If dgItemSearch.Row = -1 Then Exit Sub
            Dim objFrmVocuherEntry As New frmPCVoucherEntry()
            Dim voucherId As String = GridSource(dgItemSearch.Row)("VoucherID")
            objFrmVocuherEntry.VoucherId = voucherId
            objFrmVocuherEntry.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgItemSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgItemSearch.DoubleClick
        Try
            If dgItemSearch.Row = -1 Then Exit Sub
            Dim objFrmVocuherEntry As New frmPCVoucherEntry()
            Dim voucherId As String = GridSource(dgItemSearch.Row)("VoucherID")
            objFrmVocuherEntry.VoucherId = voucherId
            objFrmVocuherEntry.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAll.Click
        Try
            Response = BLInstance.GetAllVoucher()
            If Response IsNot Nothing Then
                Dim dv As New DataView(Response.VoucherTable, "", "", DataViewRowState.CurrentRows)
                GridSource = dv              
                dgItemSearch.DataSource = Nothing
                dgItemSearch.SetDataBinding(GridSource, "", True)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmViewVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try            
            For Each col In dgItemSearch.Columns
                col.FilterEscape = ""
                dgItemSearch.HeadingStyle.WrapText = False
            Next
            Response = BLInstance.GetAllVoucher("25")
            If Response IsNot Nothing Then                     
                Dim dv As New DataView(Response.VoucherTable, "", "", DataViewRowState.CurrentRows)               
                GridSource = dv
                dgItemSearch.DataSource = Nothing
                dgItemSearch.SetDataBinding(GridSource, "", True)
            End If
            SetCulture(Me)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "VOID_PETTYCASH") = False Then
                Me.dgItemSearch.AllowUpdate = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub frmViewVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "view-voucher.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(134, 134, 134)
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        'btnShowAll
        '
        Me.btnShowAll.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnShowAll.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnShowAll.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnShowAll.MaximumSize = New Size(0, 30)
        'Me.btnShowAll.MinimumSize = New Size(0, 30)
        Me.btnShowAll.Size = New System.Drawing.Size(68, 30)
        Me.btnShowAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        'Me.btnShowAll.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.btnShowAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnShowAll.FlatStyle = FlatStyle.Flat
        Me.btnShowAll.FlatAppearance.BorderSize = 0
        Me.btnShowAll.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnCancel
        '
        Me.btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnCancel.MaximumSize = New Size(0, 30)
        'Me.btnCancel.MinimumSize = New Size(0, 30)
        Me.btnCancel.Size = New System.Drawing.Size(68, 30)
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        'Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancel.FlatStyle = FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnOk
        '
        Me.btnOk.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Me.btnOk.MaximumSize = New Size(0, 30)
        'Me.btnOk.MinimumSize = New Size(0, 30)
        Me.btnOk.Size = New System.Drawing.Size(68, 30)
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        'Me.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnOk.FlatStyle = FlatStyle.Flat
        Me.btnOk.FlatAppearance.BorderSize = 0
        Me.btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'dgItemSearch
        '
        dgItemSearch.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        dgItemSearch.Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgItemSearch.Styles(1).Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgItemSearch.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        dgItemSearch.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        dgItemSearch.Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgItemSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        dgItemSearch.HighLightRowStyle.BackColor = Color.LightBlue
        dgItemSearch.HighLightRowStyle.ForeColor = Color.WhiteSmoke
        Return ""
    End Function
    Private Sub dgItemSearch_AfterColEdit(sender As Object, e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgItemSearch.AfterColEdit 'vipul 21/08/18 for delete petty cash voucher

        Try
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "VOID_PETTYCASH") = True Then
                Dim DeleteRemakrk As String
                Dim obj As New frmArticlesRemark
                Dim objcom As New clsCommon
                Dim voucherId As String = GridSource(dgItemSearch.Row)("VoucherID")

                If objcom.IsTodaysPettyCashVoucher(voucherId, clsAdmin.SiteCode, clsAdmin.Financialyear) = False Then
                    ShowMessage("System not allow to delete past Voucher", getValueByKey("CLAE04"))
                    ShowVoucher()
                    Exit Sub
                End If

                Dim DialogResult = obj.ShowDialog()
                If DialogResult = Windows.Forms.DialogResult.OK Then
                    DeleteRemakrk = obj.AuthUserRemarks

                    objcom.DeletePettyCashVoucher(clsAdmin.UserCode, voucherId, clsAdmin.SiteCode, DeleteRemakrk)
                    ShowVoucher()
                Else
                    ShowVoucher()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub ShowVoucher() 'vipul 21/08/18 
        Try
            Response = BLInstance.GetAllVoucher("25")
            Dim dv As New DataView(Response.VoucherTable, "", "", DataViewRowState.CurrentRows)
            GridSource = dv
            dgItemSearch.DataSource = Nothing
            dgItemSearch.SetDataBinding(GridSource, "", True)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
End Class