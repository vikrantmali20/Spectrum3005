Public Class CtrlCMbtnBottom

  
    Private Sub CtrlBtnSaleGV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnSaleGV.Click

    End Sub

    Private Sub CtrlCMbtnBottom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CtrlBtnSaleGV.Text = getValueByKey("salegv")
            CtrlBtnSaleCLPPoint.Text = getValueByKey("saleclppoints")
            CtrlBtnHomeDelivery.Text = getValueByKey("homedelivery")
            CtrlBtnStockCheck.Text = getValueByKey("stockcheck")
            CtrlBtnAddExtraCost.Text = getValueByKey("additionalcost")
            CtrlBtnReturn.Text = getValueByKey("returns")
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

        Catch ex As Exception
            CtrlBtnSaleGV.Text = "SaleGV"
            CtrlBtnSaleCLPPoint.Text = "Sale LP "
            CtrlBtnHomeDelivery.Text = "HomeDelivery"
            CtrlBtnStockCheck.Text = "Stock Check"
            CtrlBtnAddExtraCost.Text = "Addition Cost"
            CtrlBtnReturn.Text = "Return"
        End Try

    End Sub

    Private Function Themechange()
        CtrlBtnHomeDelivery.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnHomeDelivery.Image = My.Resources.HomeDelivery_Normal
        CtrlBtnHomeDelivery.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnHomeDelivery.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnHomeDelivery.BackColor = Color.White
        CtrlBtnHomeDelivery.Text = CtrlBtnHomeDelivery.Text.ToUpper()

        CtrlBtnSaleGV.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnSaleGV.Image = My.Resources.SellGiftVoucher
        CtrlBtnSaleGV.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnSaleGV.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnSaleGV.BackColor = Color.White
        CtrlBtnSaleGV.Text = CtrlBtnSaleGV.Text.ToUpper()

        CtrlBtnSaleCLPPoint.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnSaleCLPPoint.Image = My.Resources.LoyaltyPoints
        CtrlBtnSaleCLPPoint.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnSaleCLPPoint.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnSaleCLPPoint.BackColor = Color.White
        CtrlBtnSaleCLPPoint.Text = CtrlBtnSaleCLPPoint.Text.ToUpper()

        CtrlBtnStockCheck.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnStockCheck.Image = My.Resources.StockCheck
        CtrlBtnStockCheck.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnStockCheck.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnStockCheck.BackColor = Color.White
        CtrlBtnStockCheck.Text = CtrlBtnStockCheck.Text.ToUpper()

        CtrlBtnAddExtraCost.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnAddExtraCost.Image = My.Resources.AdditionalCost
        CtrlBtnAddExtraCost.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnAddExtraCost.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnAddExtraCost.BackColor = Color.White
        CtrlBtnAddExtraCost.Text = CtrlBtnAddExtraCost.Text.ToUpper()

        CtrlBtnReturn.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnReturn.Image = My.Resources.ReturnsNew
        CtrlBtnReturn.ImageAlign = ContentAlignment.MiddleCenter
        CtrlBtnReturn.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlBtnReturn.BackColor = Color.White
        CtrlBtnReturn.Text = CtrlBtnReturn.Text.ToUpper()
    End Function
End Class
