Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class FrmDisplayRejectOrder
    Dim objcls As New clsCommon
    Dim objCM As New clsCashMemo

    Dim dsMain As New DataSet
    Public Property _dt As DataSet
    Public Property dt As DataSet
        Get
            Return _dt
        End Get
        Set(value As DataSet)
            _dt = value
        End Set
    End Property

#Region "Events"
    Private Sub frmProductNotificationPopups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            getBinding()
            BindAddArticle(dt)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        Me.BackColor = Color.FromArgb(76, 76, 76)

    End Function

#End Region
#Region "Function"
    Private Sub BindAddArticle(ByVal dt As DataSet)
        Try
         
            dgRejectOrder.DataSource = dt.Tables(0)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
    
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

  
    Private Sub dgRejectOrder_CellButtonClick(sender As Object, e As RowColEventArgs) Handles dgRejectOrder.CellButtonClick
        Try
            Dim VoidBillNo = dgRejectOrder.Item(dgRejectOrder.Row, "BillNo")
            voidBill(VoidBillNo, clsAdmin.SiteCode)
            ShowMessage("Bill void successfully " + VoidBillNo + " ", "Information")
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
       
    End Sub

    Private Sub voidBill(ByVal voidbillno As String, ByVal sitecode As String)
        Try
            GetCashMemoDetails(voidbillno, sitecode)
            Dim billNo, StrReason As String
            billNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString()
            Dim DeletTime As DateTime = objCM.GetCurrentDate()
            Dim AuthUser As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId").ToString()
            Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
            dsMain.Tables("CASHMEMOHDR").Rows(0)("BILLINTERMEDIATESTATUS") = "Deleted"
            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionDate") = DeletTime
            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionTime") = DeletTime
            dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedon") = DeletTime
            dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedby") = clsAdmin.UserCode
            dsMain.Tables("CASHMEMOHDR").Rows(0)("UpdatedAt") = clsAdmin.SiteCode
            dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId") = clsAdmin.UserCode
            dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserRemarks") = "online order reject from zomato "
            Dim ds As DataSet = dsMain.Copy()
            If objCM.DeleteBill(ds, clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation) = True Then
                If dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString <> "" Then
                    Dim totalPoints As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPPoints").ToString()
                    If String.IsNullOrEmpty(totalPoints) Then
                        totalPoints = "0"
                    End If
                    If totalPoints = "" Or CDbl(totalPoints) <= 0 Then
                        totalPoints = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPDiscount").ToString()
                    End If
                    Dim RedemptionPoints As Double = 0
                    For Each dr As DataRow In dsMain.Tables("CashMemoReceipt").Select("TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
                        RedemptionPoints = IIf(dr("AmountTendered") Is DBNull.Value, 0, dr("AmountTendered"))
                    Next
                    If (totalPoints <> "" AndAlso CDbl(totalPoints) > 0) Or RedemptionPoints > 0 Then
                        If objCM.ReversedCLPPoints(clsAdmin.CLPProgram, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString(), totalPoints, RedemptionPoints, clsAdmin.SiteCode, clsAdmin.UserCode, dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), dsMain.Tables("CASHMEMOHDR").Rows(0)("Billdate").ToString()) = False Then
                            ShowMessage(getValueByKey("CM053"), "CM053 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ' ShowMessage(getValueByKey("CM040"), "CM040 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub GetCashMemoDetails(ByVal strCashMemo As String, ByVal strSiteCode As String)
        Try
            Dim dsTemp As DataSet
            dsTemp = objCM.GetStruc(strCashMemo, strSiteCode)
            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("CASHMEMOHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMODTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMORECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Dim ResponsefromUser As Int32
            ShowMessage("Are you sure ,you want to void all Online Rejected Order bills?", "Information", ResponsefromUser, "NO", "YES")

            If ResponsefromUser = 1 Then
                Dim strVoidBill As String = ""
                For Each dr As DataRow In dt.Tables(0).Rows
                    Dim str = dr("BillNo").ToString
                    If String.IsNullOrEmpty(strVoidBill) Then
                        strVoidBill = str
                    Else
                        strVoidBill = strVoidBill + "," + str
                    End If
                Next
                For Each dr As DataRow In dt.Tables(0).Rows
                    Dim strVoidBillno = dr("BillNo").ToString
                    voidBill(strVoidBillno, clsAdmin.SiteCode)
                Next
                ShowMessage("Bill void successfully " + strVoidBill + " ", "Information")
                Me.Close()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub getBinding()
        Try
            dsMain = objCM.GetStruc("0", "0")    
        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
End Class