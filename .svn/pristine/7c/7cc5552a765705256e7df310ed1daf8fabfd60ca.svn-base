Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Public Class frmTransfer
    Inherits CtrlRbnBaseForm
    Private Enum GridColumns

        ItemName
        ItemCode
        StoreName
        STR
        STRQuantity
        TransferQuantity
        UOM
        TransferStatus
    End Enum
     
     
    'Private Sub btnCloseSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseSetting.Click
    '    Me.Close()
    'End Sub
   
    Private Sub frmTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim objTransfer As New clsTransfer

            Dim _dsMain As New DataTable
            Dim strDetail As String = ""
            Dim strDetailCode As String = ""
            Dim strStores As String = ""
            _dsMain = objTransfer.GetArticlesBySite(clsAdmin.SiteCode)
            For Each row As DataRow In _dsMain.Rows
                strDetail &= row.Item("ArticleName") & "|"
                strDetailCode &= row.Item("ArticleCode") & "|"
            Next row
            strDetail = "|" & strDetail
            strDetail = Microsoft.VisualBasic.Left(strDetail, (strDetail.ToString().Length - 1))
            strDetailCode = "|" & strDetailCode
            strDetailCode = Microsoft.VisualBasic.Left(strDetailCode, (strDetailCode.ToString().Length - 1))
            Dim _dsSite As New DataTable
            _dsSite = objTransfer.GetStores()
            For Each row As DataRow In _dsSite.Rows

                strStores &= row.Item("SiteCode") & "|"
            Next row

            strStores = Microsoft.VisualBasic.Left(strStores, (strStores.ToString().Length - 1))
            dgMainGrid.Cols(1).ComboList = strDetail
            dgMainGrid.Cols(1).Width = 200
            dgMainGrid.Cols(2).ComboList = strDetailCode
            dgMainGrid.Cols(2).Width = 200
            dgMainGrid.Cols(3).ComboList = strStores
            dgMainGrid.Cols(3).Width = 120
        Catch ex As Exception

        End Try
    End Sub
    
    'Private Sub dgMainGrid_CellChanged(sender As Object, e As RowColEventArgs) Handles dgMainGrid.CellChanged
    '    Try
    '        Dim selrow As Integer = e.Row
    '        Dim cell As Integer = e.Col
    '        Dim articleName As String
    '        Dim articleCode As String
    '        'articleName=
    '    Catch ex As Exception

    '    End Try
    'End Sub

     

     
    Private Sub dgMainGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            Dim objTransfer As New clsTransfer
             


            Dim articleName As String = ""
            Dim articleCode As String = ""
            Dim storeName As String = ""
            Dim STRlist As String = ""
            Dim STRdetails As String = ""
            If e.Col = 1 Then
                articleName = Convert.ToString(dgMainGrid.Rows(e.Row)(CInt(GridColumns.ItemName) + 1))
                articleCode = objTransfer.GetArticleCodeByName(clsAdmin.SiteCode, articleName)
                dgMainGrid.Rows(e.Row).Item(e.Col + 1) = articleCode
            End If
            If e.Col = 3 Then
                storeName = Convert.ToString(dgMainGrid.Rows(e.Row)(CInt(GridColumns.StoreName) + 1))
                articleCode = Convert.ToString(dgMainGrid.Rows(e.Row)(CInt(GridColumns.ItemCode) + 1))
                If articleCode <> "" AndAlso storeName <> "" Then
                    Dim _dsSTR As New DataTable
                    _dsSTR = objTransfer.GetSTRbyArticles(storeName, articleCode)
                    For Each row As DataRow In _dsSTR.Rows

                        STRlist &= row.Item("DocumentNumber") & ", " & row.Item("DeliveryDt") & "|"
                    Next row

                    STRlist = Microsoft.VisualBasic.Left(STRlist, (STRlist.ToString().Length - 1))
                    dgMainGrid.Cols(4).ComboList = STRlist
                    dgMainGrid.Cols(4).Width = 200
                End If
            End If
            If e.Col = 4 Then
                Dim STRcodeArra As Array
                STRcodeArra = Convert.ToString(dgMainGrid.Rows(e.Row)(CInt(GridColumns.STR) + 1)).Split(",")
                Dim STRcode As String
                STRcode = STRcodeArra(0)
                If STRcode <> "" Then
                    Dim _dsSTRdetails As New DataTable
                    _dsSTRdetails = objTransfer.GetSTRDetails(clsAdmin.SiteCode, Convert.ToString(dgMainGrid.Rows(e.Row)(CInt(GridColumns.ItemCode) + 1)), STRcode)
                    For Each row As DataRow In _dsSTRdetails.Rows
                        dgMainGrid.Rows(e.Row).Item(e.Col + 1) = row.Item("Qty")
                        dgMainGrid.Rows(e.Row).Item(e.Col + 3) = "Not Transferred"
                    Next row

                End If
                
            End If
            articleName = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            Dim objTransferDetail As New TransferDetails
            For tansferCode As Integer = 1 To dgMainGrid.Rows.Count - 1
                objTransferDetail.ArticleCode = dgMainGrid.Rows(tansferCode)(CInt(GridColumns.ItemCode))
                objTransferDetail.SiteCode = dgMainGrid.Rows(tansferCode)(CInt(GridColumns.StoreName))
                Dim STRcodeArra As Array
                STRcodeArra = dgMainGrid.Rows(tansferCode)(CInt(GridColumns.STR))
                Dim STRcode As String
                STRcode = STRcodeArra(0)
                objTransferDetail.RefDocumentNo = STRcode
                objTransferDetail.Quantity = dgMainGrid.Rows(tansferCode)(CInt(GridColumns.STRQuantity))
                objTransferDetail.UOM = dgMainGrid.Rows(tansferCode)(CInt(GridColumns.UOM))
                If dgMainGrid.Rows(tansferCode)(CInt(GridColumns.UOM)) = "Not Transferred" Then
                    objTransferDetail.TransferStatus = 0
                Else
                    objTransferDetail.TransferStatus = 1
                End If

                objTransferDetail.CREATEDAT = clsAdmin.SiteCode
                objTransferDetail.CREATEDBY = clsAdmin.UserCode
                objTransferDetail.CREATEDON = DateTime.Now
                objTransferDetail.UPDATEDAT = clsAdmin.SiteCode
                objTransferDetail.UPDATEDBY = clsAdmin.UserCode
                objTransferDetail.UPDATEDON = DateTime.Now
                objTransferDetail.Status = 1
                'If((dgMainGrid.Rows(tansferCode)(CInt(enumArticles.cost)) Is Nothing), 0, Convert.ToInt64(dgMainGrid.Rows(rowBarCode)(CInt(enumArticles.cost))))
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class