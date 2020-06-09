Imports SpectrumBL
Public Class frmNAdjustment
    Inherits CtrlPopupForm
    Dim dtAdj, dtService As DataTable
    Public ReadOnly Property GetCharges() As DataTable
        Get
            Return dtAdj
        End Get
    End Property
    Private Sub addrow()
        Try
            If cmbAdjustmentType.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ACHR001"), "ACHR001 - " & getValueByKey("CLAE04"))
                cmbAdjustmentType.Select()
                'ShowMessage("Please select the Charges first", "Information")
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtAmount.Text.Trim()) Then
                ShowMessage(getValueByKey("ACHR002"), "ACHR002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Charges amount should be greater than zero ", "Information")
                txtAmount.Select()
                Exit Sub
            End If
            Dim dv As New DataView(dtAdj, "Ean='" & cmbAdjustmentType.SelectedValue & "'", "Article", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                dv.AllowEdit = True
                dv.Item(0)("AdjAmount") = txtAmount.Text.Trim
            Else
                Dim strArticleCode As String = ""
                For Each drService As DataRow In dtService.Select("EAN='" & cmbAdjustmentType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
                    strArticleCode = drService("ArticleCode").ToString()
                Next
                Dim dr As DataRow = dtAdj.NewRow()
                dr("AdjType") = cmbAdjustmentType.Text
                dr("AdjAmount") = txtAmount.Text.Trim()
                dr("Article") = strArticleCode
                dr("EAN") = cmbAdjustmentType.SelectedValue
                dtAdj.Rows.Add(dr)
            End If
            txtAmount.Text = String.Empty
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CreateStructure()
        Try
            dtAdj = New DataTable
            Dim ColId As New DataColumn("AdjType")
            ColId.DataType = Type.GetType("System.String")
            Dim ColAmt As New DataColumn("AdjAmount")
            ColAmt.DataType = Type.GetType("System.Double")
            Dim ColArticle As New DataColumn("Article")
            ColArticle.DataType = Type.GetType("System.String")
            Dim ColEan As New DataColumn("EAN")
            ColEan.DataType = Type.GetType("System.String")
            dtAdj.Columns.Add(ColId)
            dtAdj.Columns.Add(ColAmt)
            dtAdj.Columns.Add(ColArticle)
            dtAdj.Columns.Add(ColEan)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridSetting()
        dgAdjustment.Cols("AdjType").Width = 250
        dgAdjustment.Cols("AdjType").Caption = "Charges"

        dgAdjustment.Cols("AdjAmount").Width = 130
        dgAdjustment.Cols("AdjAmount").Caption = "Amount"
        dgAdjustment.Cols("AdjAmount").Format = "0.00"

        dgAdjustment.Cols("Article").Visible = False
        dgAdjustment.Cols("EAN").Visible = False
        'dgAdjustment.pResizeCol()
    End Sub
    Private Sub frmAdjustment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            Dim obj As New clsCashMemo
            dtService = obj.GetServiceArticle(clsAdmin.SiteCode)
            If Not dtService Is Nothing Then
                cmbAdjustmentType.DataSource = dtService
                cmbAdjustmentType.DisplayMember = "ARTICLESHORTNAME"
                cmbAdjustmentType.ValueMember = "EAN"
                cmbAdjustmentType.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmbAdjustmentType.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbAdjustmentType.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmbAdjustmentType.SelectedIndex = -1
            End If
            'cmbAdjustmentType_SelChange(cmbAdjustmentType, e)
            CreateStructure()
            dgAdjustment.DataSource = dtAdj
            GridSetting()
            If clsDefaultConfiguration.OtherChargesEditable = False Then
                txtAmount.ReadOnly = True
            Else
                txtAmount.ReadOnly = False
            End If
            cmbAdjustmentType.SelectedIndex = 0
            txtAmount.Select()
            'cmbAdjustmentType.Select()

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        txtAmount.Select()
        'cmbAdjustmentType.Select()
    End Sub
    Private Sub cmdOkCharg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOkCharg.Click
        addrow()
    End Sub
    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            If (txtAmount.Focused) Then
                addrow()
            ElseIf (cmdOk.Focused AndAlso cmbAdjustmentType.SelectedIndex <> -1 AndAlso txtAmount.Text <> String.Empty) Then
                addrow()
                Me.Close()
            Else
                Me.Close()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Try
            Dim i As Int32 = dgAdjustment.RowSel
            dtAdj.Rows.RemoveAt(i - 1)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
     
    Private Sub cmbAdjustmentType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAdjustmentType.SelectedValueChanged
        Try
            Dim strAmount As String = ""
            For Each drService As DataRow In dtService.Select("EAN='" & cmbAdjustmentType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
                strAmount = drService("SellingPrice").ToString()
                txtAmount.Text = strAmount
            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.Size = New Size(465, 270)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblAmount.BackColor = Color.FromArgb(212, 212, 212)
        lblAmount.AutoSize = False
        lblAmount.Location = New Point(226, 12)
        lblAmount.Size = New Size(59, 20)

        lblAmount.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        dgAdjustment.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgAdjustment.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgAdjustment.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgAdjustment.Rows.MinSize = 25
        dgAdjustment.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgAdjustment.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgAdjustment.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgAdjustment.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgAdjustment.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgAdjustment.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgAdjustment.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.TextAlign = ContentAlignment.MiddleCenter
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdOk.MinimumSize = New Size(60, 22)
        cmdOk.MaximumSize = New Size(60, 22)

        cmdDelete.Image = Nothing
        cmdDelete.BackgroundImage = My.Resources.Cancelnew
        cmdDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdDelete.BackgroundImageLayout = ImageLayout.Stretch
        cmdDelete.FlatAppearance.BorderSize = 0
        cmdDelete.FlatStyle = FlatStyle.Flat
        cmdDelete.Height = 25
        cmdDelete.MinimumSize = New Size(24, 24)
        cmdDelete.MaximumSize = New Size(24, 24)

        cmdOkCharg.Image = Nothing
        cmdOkCharg.BackgroundImage = My.Resources.Oknew
        cmdOkCharg.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdOkCharg.BackgroundImageLayout = ImageLayout.Stretch
        cmdOkCharg.FlatAppearance.BorderSize = 0
        cmdOkCharg.FlatStyle = FlatStyle.Flat
        cmdOkCharg.MinimumSize = New Size(24, 24)
        cmdOkCharg.MaximumSize = New Size(24, 24)

    End Function
End Class
