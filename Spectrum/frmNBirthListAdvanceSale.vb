
Imports SpectrumBL
Public Class frmNBirthListAdvanceSale



    Private objDataRowItem As DataRow

    Public Property DTableVoucher() As DataTable
        Get
            Return dtVoucherTable
        End Get
        Set(ByVal value As DataTable)
            dtVoucherTable = value
        End Set
    End Property



    Private dtVoucherTable As DataTable

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click, txtVoucherAmount.DoubleClick

        'If (rbGv.CanFocus) Then
        '    lblVoucherAmount.Visible = True
        '    txtVoucherAmount.Visible = True
        '    rbGv.Visible = False
        '    rbCLp.Visible = False
        'Else
     
        Save()


    End Sub

    Private Function Save() As Boolean
        Try
            Dim decValue As Decimal
            If Not txtVoucherAmount.Text = String.Empty Then
                Try
                    decValue = CDbl(txtVoucherAmount.Text)
                Catch ex As Exception
                    ShowMessage(getValueByKey("BLAS01"), "BLAS01 - " & getValueByKey("CLAE04"))
                    Exit Function
                End Try
                Dim obBirthListGlobal As New clsBirthListGobal
                Dim strMsg As String = String.Empty
                Dim dtEAN As DataTable = obBirthListGlobal.RetrieveQuery("SELECT Top 1 B.EAN,B.ARTICLECODE FROM MSTVOUCHER A LEFT OUTER JOIN MSTEAN B ON A.ARTICLECODE=B.ARTICLECODE WHERE A.VourcherType='GiftVoucher(I)'", strMsg)

                Dim EAN As String = String.Empty
                Dim ArticleCode As String = String.Empty
                If Not dtEAN Is Nothing Then
                    EAN = dtEAN.Rows(0)("EAN")
                    ArticleCode = dtEAN.Rows(0)("ARTICLECODE")
                End If
                If Not EAN Is String.Empty Then
                    If Not DTableVoucher.Rows.Count > 0 Then
                        objDataRowItem = DTableVoucher.NewRow()
                        objDataRowItem.Item("SiteCode") = clsAdmin.SiteCode
                        objDataRowItem.Item("EAN") = EAN
                        objDataRowItem.Item("ArticleCode") = ArticleCode
                        objDataRowItem.Item("RequstedQty") = 1
                        objDataRowItem.Item("BookedQty") = 1
                        objDataRowItem.Item("PurchasedQty") = 1
                        objDataRowItem.Item("SellingPrice") = decValue
                        objDataRowItem.Item("Discription") = "Gift Voucher"
                        objDataRowItem("NetAmount") = decValue
                        DTableVoucher.Rows.Add(objDataRowItem)
                    Else
                        DTableVoucher.Rows(0).Delete()
                        DTableVoucher.AcceptChanges()
                        objDataRowItem = DTableVoucher.NewRow()
                        objDataRowItem.Item("SiteCode") = clsAdmin.SiteCode
                        objDataRowItem.Item("EAN") = EAN
                        objDataRowItem.Item("ArticleCode") = ArticleCode
                        objDataRowItem.Item("RequstedQty") = 1
                        objDataRowItem.Item("BookedQty") = 1
                        objDataRowItem.Item("PurchasedQty") = 1
                        objDataRowItem.Item("SellingPrice") = decValue
                        objDataRowItem.Item("Discription") = "Gift Voucher"
                        objDataRowItem("NetAmount") = decValue
                        DTableVoucher.Rows.Add(objDataRowItem)
                    End If
                Else
                    ShowMessage(strMsg, getValueByKey("CLAE04"))
                End If
                Me.Close()
            Else
                ShowMessage(getValueByKey("BLAS02"), "BLAS02 - " & getValueByKey("CLAE04"))
            End If
            'End If
            Return True
        Catch ex As Exception
            LogException(ex) 
        End Try
      
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmNBirthListAdvanceSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtVoucherAmount.Focus()
            lblVoucherAmount.Visible = True
            txtVoucherAmount.Visible = True
            If Not dtVoucherTable Is Nothing AndAlso dtVoucherTable.Rows.Count > 0 Then
                txtVoucherAmount.Text = dtVoucherTable.Rows(0)("NetAmount")
            End If
        Catch ex As Exception

        End Try
        SetCulture(Me, Me.Name)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub txtVoucherAmount_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtVoucherAmount.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            Save()
        End If
    End Sub
End Class
