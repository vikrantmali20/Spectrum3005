Imports System.Windows.Forms
Imports SpectrumBL
Imports System.Drawing.Drawing2D
Imports TimesheetLib
Imports SpectrumPrint
Imports Spectrum.LicenseServices
Imports SpectrumCommon
Imports Spectrum.SpectrumUpdate
Imports System.Text
Imports System.IO
Imports System.ComponentModel
Imports System.Net
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
'Imports System.Threading

Public Class MDISpectrum
    Inherits C1.Win.C1Ribbon.C1RibbonForm
    Private m_ChildFormNumber As Integer
    Private objLogin As New clsLogin
    Private LastClosedDate As DateTime
    Private AnyTerminalOpen As Integer = 0
    Private WithEvents spectrumTimer As New Timer()
    Private clientLicense As New ClsLicense()
    Dim dtSite As New DataTable
    Dim dtTimer As New DataTable
    Public STRNotificationTimer As New Timer()
    Public NotificationTimer As New Timer()
    Public ScedularTimer As New Timer()
    Public ProcScedularTimer As New Timer()
    Public AutoCancelTimer As New Timer()
    Dim objclsCommon As New clsCommon
    Dim selectedPartner, SelectedTender As String
    Private directoryPath As String = Application.StartupPath
    Dim ObjClsCashMemoPrint As New clsCashMemoPrint("", False, False, False, False, False, False, Nothing, False, "") 'vipin 08.08.2018
    Dim objCM As New clsCashMemo
    Dim ObjKds As New clsKdsData
    Dim objAuth As New clsAuthorization
    Dim ForLoopComplete As Boolean = False


    Private Sub SalesOrderUpdation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderUpdation.Click
        Dim ChildForm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            ChildForm = New Spectrum.frmPCNSalesOrderUpdate
        Else
            ChildForm = New Spectrum.frmNSalesOrderUpdate
        End If

        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub SalesOrderCancelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderCancelation.Click
        ' Dim ChildForm As New Spectrum.frmNSalesOrderCancel

        Dim ChildForm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            ChildForm = New Spectrum.frmPCNSalesOrderUpdate
            ChildForm.SoCancel = True
        Else
            ChildForm = New Spectrum.frmNSalesOrderCancel
        End If
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub MDISpectrum_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If LoginStatus = False Then
            Try
                AppActivate(My.Application.Info.Title)
            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub MDISpectrum_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        AutoCancelTimer.Stop()
    End Sub
    Private Sub MDISpectrum_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        'If Me.MdiChildren.Count = 0 Then
        SetCulture(Me, Me.Name)
        Me.MenuStrip.Show()
        'Me.StatusStrip.Visible = False
        GrpHome.Show()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ToolsMenu.Text = ToolsMenu.Text.ToUpper
            WindowsMenu.Text = WindowsMenu.Text.ToUpper
            Transaction.Text = Transaction.Text.ToUpper
            PettyCash.Text = PettyCash.Text.ToUpper
            ToolsToolStripMenuItem.Text = ToolsToolStripMenuItem.Text.ToUpper
            ReportsToolsStripOuterMenuItem.Text = ReportsToolsStripOuterMenuItem.Text.ToUpper
            HelpMenu.Text = HelpMenu.Text.ToUpper
            LogoutToolStripMenuItem.Text = LogoutToolStripMenuItem.Text.ToUpper
            ExitToolStripMenuItem.Text = ExitToolStripMenuItem.Text.ToUpper
            TicketingSystemToolStripMenuItem.Text = TicketingSystemToolStripMenuItem.Text.ToUpper
        End If

    End Sub
    ' Added By Ketan For Ticketing System Notification 
    Public FirstNotificationTimer As New Timer()
    Private Sub FirstNotificationTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            FirstNotificationTimer.Stop()
            If clsDefaultConfiguration.ClientForMail = "JK" Then
                notification()
            Else
                HomeDeliveryNotification()
            End If
            FirstNotificationTimer.Start()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'added by khusrao adil on 13-03-2018 for spectrum new developement 
    Public CreditSalesPopupTimer As New Timer()
    Public dtCreditSalesPopupDtl As DataTable
    Public AutoKotGenerate As New Timer()

    Private Sub AutoKotGenerate_Tick(ByVal sender As Object, e As EventArgs)
        Try

            Dim dtFinal As New DataTable
            Dim DtBussyTable = objclsCommon.GetAllPendingKOT()
            If ForLoopComplete Then
                ForLoopComplete = False
                Exit Sub
            End If
            For Each DrBusyTable1 In DtBussyTable.DefaultView.ToTable(True, "Billno", "DineinNumber").Rows
                ForLoopComplete = True
                Dim dtDineInItemRemarks = objclsCommon.LoadRemarks(clsAdmin.SiteCode, DrBusyTable1("Billno"))
                ' DineInAutoSave = False
                objCM.DinInFlagForKOT = True
                Dim oldQty As Decimal
                Dim newQty As Decimal
                Dim MaxKotNo As Integer = 0
                Dim ItemCode, desc, remark As String
                Dim dtNew As New DataTable
                Dim ObjKDS As New clsKdsData
                '    Dim dtOld As DataTable = objclsCommon.GetOldKotData(DrBusyTable("DineinNumber"))
                'If Not dtOld Is Nothing AndAlso dtOld.Rows.Count = 0 Then
                '    If PaxOnCurrentTable = "" Then
                '        PaxOnCurrentTable = "0"
                '    End If
                '    objCM.UpdateTrackCustomers(currentDineInBillNo, currentDineInTable, clsAdmin.SiteCode, PaxOnCurrentTable)
                ''End If


                dtFinal = objCM.GetItemsInfoForGenerateBill()
                dtFinal.Rows.Clear()
                MaxKotNo = objclsCommon.GetMaxKotNo(DrBusyTable1("Billno"))
                MaxKotNo = MaxKotNo + 1
                'Dim dvItemDetail As New DataView(dtNew, "", "", DataViewRowState.CurrentRows)
                'Dim BillLineNO As String = String.Empty
                'For Each dr As DataRow In dtNew.Rows
                '    oldQty = 0
                '    ItemCode = dr("ArticleCode").ToString()
                '    desc = dr("Discription").ToString()
                '    BillLineNO = dr("BillLineNO").ToString()
                '    Dim result As DataRow() = dtOld.[Select]("ArticleCode='" + dr("ArticleCode") & "'")
                '    If result.Count > 0 Then
                '        oldQty = Convert.ToDecimal(result(0)("KotQty"))
                '    End If
                '    If oldQty > 0 Then
                '        newQty = Convert.ToDecimal(dr("Quantity")) - oldQty
                '    Else
                '        newQty = Convert.ToDecimal(dr("Quantity"))
                '    End If
                '    If clsDefaultConfiguration.AllowDecimalQty Then 'added by khusrao adil on 02-08-2018 for sharda restaurent
                '        newQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(newQty)), 3)
                '    Else
                '        newQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(newQty)), 1)
                '    End If
                '-----For Remark For item
                ' remark = ObjclsCommon.GetRemarkItemWise(clsAdmin.SiteCode, currentDineInBillNo, ItemCode, dr("EAN").ToString())

                Dim DtPrintKotBillWise As DataTable = DtBussyTable.Select("BillNo= '" & DrBusyTable1("BillNo") & "'").CopyToDataTable()

                '    Dim Drnew() As DataRow
                For Each DrBusyTable In DtPrintKotBillWise.Rows
                    Dim Drnew = dtDineInItemRemarks.Select("ArticleCode ='" & DrBusyTable("ArticleCode").ToString() & "' and EAN='" & DrBusyTable("EAN").ToString() & "'")
                    If Drnew.Count > 0 Then
                        remark = IIf(IsDBNull(Drnew(0)("Remark")), "", Drnew(0)("Remark"))
                    Else
                        remark = ""
                    End If
                    '---adding to datatable
                    'MaxKotNo = objclsCommon.GetMaxKotNo(DrBusyTable1("DineinNumber"))
                    'MaxKotNo = MaxKotNo + 1
                    If CDbl(DrBusyTable("KOTQuantity")) > 0 Then
                        Dim newRow As DataRow = dtFinal.NewRow()
                        newRow("BillNo") = DrBusyTable("BillNo")
                        newRow("EAN") = DrBusyTable("EAN").ToString()
                        newRow("DISCRIPTION") = DrBusyTable("Discription").ToString()
                        newRow("ArticleCode") = DrBusyTable("ArticleCode").ToString()
                        newRow("KotQuantity") = DrBusyTable("KOTQuantity").ToString()
                        newRow("KotNo") = MaxKotNo
                        newRow("Remark") = remark
                        dtFinal.Rows.Add(newRow)
                    End If
                Next

                Dim dtLoad = dtFinal.Copy()

                Dim objPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                objPrint.KOTCustomerSaleType = objCM.SalesType
                '  objPrint.DeliveryPersonName = CtrlSalesPersons.CtrllabelSalesPerson.Text & ":" & SalesPersonName
                Dim ErrorMsg As String = ""
                Dim vBillno As String = ""
                If Not dtFinal Is Nothing AndAlso dtFinal.Rows.Count > 0 Then 'vipin 27.07.2018 new Order KOT print Wrong
                    vBillno = dtFinal.Rows(0)("BillNo").ToString.Trim()
                End If
                'If clsDefaultConfiguration.KOTPrintRequired Then
                '    objPrint.DineInKOTPrint("KOT", ErrorMsg, Nothing, dtFinal, vBillno, DrBusyTable1("DineinNumber"))
                'End If
                Dim cmd As New System.Text.StringBuilder()

                ' Cmd.Length = 0

                For Each drfinal In dtFinal.Rows
                    cmd.Append(" INSERT INTO DineInKotQtyMap( ")
                    cmd.Append("  billno, EAN, ")
                    cmd.Append("  ArticleCode, KotQuantity, status,KotNO,Remark,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON)")
                    cmd.Append(" Values(")
                    cmd.Append("'" & drfinal("BillNo").ToString() & "','" & drfinal("EAN").ToString() & "',")
                    cmd.Append("'" & drfinal("ArticleCode").ToString() & "','" & drfinal("KOTQuantity").ToString() & "',")
                    cmd.Append(" 1,'" & MaxKotNo & "','" & remark & "','" & clsAdmin.SiteCode & "',")
                    cmd.Append("'" & clsAdmin.UserCode & "',getDate(),'" & clsAdmin.SiteCode & "',")
                    cmd.Append("'" & clsAdmin.UserCode & "',getDate() ")
                    cmd.Append(");")
                Next
                If Not dtFinal Is Nothing AndAlso dtFinal.Rows.Count > 0 Then
                    If objCM.InsertOrUpdateRecord(cmd.ToString()) = True Then
                        objCM.DinInFlagForKOT = False
                        TransferDatatoKDS(clsAdmin.SiteCode, DrBusyTable1("Billno"))
                    End If
                End If

                If clsDefaultConfiguration.KOTPrintRequired Then
                    Try
                        objPrint.TableNameForDineIn = clsDefaultConfiguration.TableNameForDineIn
                        objPrint.DineInKOTPrint("KOT", ErrorMsg, Nothing, dtFinal, vBillno, DrBusyTable1("DineinNumber"), clsAdmin.SiteCode)
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                End If
            Next
            ForLoopComplete = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub TransferDatatoKDS(ByVal Sitecode As String, ByVal BillNo As String)
        Try
            Dim DtKdsTran As DataTable = objAuth.GetSitedAllowedTran(Sitecode, "KDS")
            If Not DtKdsTran Is Nothing AndAlso DtKdsTran.Rows.Count > 0 Then
                If Not String.IsNullOrEmpty(BillNo) Then
                    objCM.TRANSFERDINEINDATATOKDS(BillNo, Sitecode)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub CreditSalesPopupTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            dtCreditSalesPopupDtl = objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode, clsDefaultConfiguration.CreditSalesPopupRecordsBeforeHours)
            If dtCreditSalesPopupDtl.Rows.Count > 0 Then
                CreditSalesPopupTimer.Stop()
                PopupCreditSale.TitleText = "Unsettled Credit Sales"
                PopupCreditSale.ContentText = "Please check unsettled credit sales"
                PopupCreditSale.Image = My.Resources.BellAlert
                PopupCreditSale.ShowCloseButton = True
                PopupCreditSale.ShowOptionsButton = False
                PopupCreditSale.ShowGrip = False
                PopupCreditSale.Delay = clsDefaultConfiguration.CreditSalesPopupInterval * 60 * 1000
                PopupCreditSale.AnimationInterval = 10
                PopupCreditSale.AnimationDuration = 1000
                PopupCreditSale.TitlePadding = New Padding(0)
                PopupCreditSale.ContentPadding = New Padding(0)
                PopupCreditSale.ImagePadding = New Padding(0)
                PopupCreditSale.Scroll = False
                PopupCreditSale.Popup()
                CreditSalesPopupTimer.Start()
                Me.Enabled = True
                ' Me.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub PopupCreditSale_Click(sender As System.Object, e As System.EventArgs) Handles PopupCreditSale.Click
        dtCreditSalesPopupDtl = objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode, clsDefaultConfiguration.CreditSalesPopupRecordsBeforeHours)
        Dim obj As New frmProductNotificationPopups()
        'PopupCreditSale.Dispose()
        obj.dtCreditSales = dtCreditSalesPopupDtl 'objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode)
        obj.PopupType = enumProductNotificationTimerPopups.CreditSalesPopup
        obj.ShowDialog()
        obj.BringToFront()
        PopupCreditSale.Hide()
    End Sub
    Dim ExpiryProductTimer As New Timer()
    Dim dtCreditSalesPopupDtl1 As New DataTable()
    Public Sub ExpiryProductTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            dtCreditSalesPopupDtl1 = objclsCommon.GetProductExpiryPopupDetails(clsAdmin.SiteCode)
            dtCreditSalesPopupDtl1 = dtCreditSalesPopupDtl1.DefaultView.ToTable(True, "BatchBarCode", "ArticleShortName", "QtyAllocated", "ExpiryDate")
            If dtCreditSalesPopupDtl1.Rows.Count > 0 Then
                CreditSalesPopupTimer.Stop()
                PopupExpiryProduct.TitleText = "Expiry Product"
                PopupExpiryProduct.ContentText = "Please check Near To Expiry Product."
                PopupExpiryProduct.Image = My.Resources.BellAlert
                PopupExpiryProduct.ShowCloseButton = True
                PopupExpiryProduct.ShowOptionsButton = False
                PopupExpiryProduct.ShowGrip = False
                PopupExpiryProduct.Delay = clsDefaultConfiguration.CreditSalesPopupInterval * 60 * 1000
                PopupExpiryProduct.AnimationInterval = 10
                PopupExpiryProduct.AnimationDuration = 1000
                PopupExpiryProduct.TitlePadding = New Padding(0)
                PopupExpiryProduct.ContentPadding = New Padding(0)
                PopupExpiryProduct.ImagePadding = New Padding(0)
                PopupExpiryProduct.Scroll = False
                PopupExpiryProduct.Popup()
                CreditSalesPopupTimer.Start()
                Me.Enabled = True
                ' Me.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub PopupExpiryProduct_Click(sender As Object, e As EventArgs) Handles PopupExpiryProduct.Click
        Try
            Dim obj As New frmProductExpiryDatePopUp()
            'PopupCreditSale.Dispose()
            obj.dtCreditSales = dtCreditSalesPopupDtl1        'objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode)
            obj.PopupType = enumProductNotificationTimerPopups.ExpiryProductPopup
            obj.ShowDialog()
            obj.BringToFront()
            PopupCreditSale.Hide()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    '
    '
    Public SalesOrderPopupTimer As New Timer()
    Public dtSalesOrderPopupDtl As DataTable
    Private Sub SalesOrderPopupTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            dtSalesOrderPopupDtl = objclsCommon.GetSalesOrderPopupDetails(clsAdmin.SiteCode)
            If dtSalesOrderPopupDtl.Rows.Count > 0 Then
                SalesOrderPopupTimer.Stop()
                PopupSalesOrder.TitleText = "Near to Delivery Orders"
                PopupSalesOrder.ContentText = "Please check Near to Delivery Orders"

                PopupSalesOrder.ShowCloseButton = True
                PopupSalesOrder.ShowOptionsButton = False
                PopupSalesOrder.Image = My.Resources.BellAlert
                PopupSalesOrder.ShowGrip = False
                PopupSalesOrder.Delay = clsDefaultConfiguration.CreditSalesPopupInterval * 60 * 1000
                PopupSalesOrder.AnimationInterval = 10
                PopupSalesOrder.AnimationDuration = 1000
                PopupSalesOrder.TitlePadding = New Padding(0)
                PopupSalesOrder.ContentPadding = New Padding(0)
                PopupSalesOrder.ImagePadding = New Padding(0)
                PopupSalesOrder.Scroll = False
                PopupSalesOrder.Popup()

                SalesOrderPopupTimer.Start()
                Me.Enabled = True
                'Me.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub PopupSalesOrder_Click(sender As Object, e As EventArgs) Handles PopupSalesOrder.Click
        dtSalesOrderPopupDtl = objclsCommon.GetSalesOrderPopupDetails(clsAdmin.SiteCode)
        Dim obj As New frmSalesOrderNotifiactionPopup()
        obj.dtSalesOrder = dtSalesOrderPopupDtl 'objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode)
        obj.PopupType = enumProductNotificationTimerPopups.SalesOrderPopup
        obj.ShowDialog()
        obj.BringToFront()

        PopupSalesOrder.Hide()
    End Sub
    ' added by vipin for mail resend 08.08.2018
    Public FailedMailResend As New Timer()
    Private Sub FailedMailResend_Click(ByVal sender As Object, e As EventArgs)
        Try
            Dim DSFailedMailData As DataSet = objclsCommon.GetFailedMailData(clsAdmin.SiteCode, clsAdmin.TerminalID)
            ObjClsCashMemoPrint.UserID = clsAdmin.UserCode
            ObjClsCashMemoPrint.Terminalid = clsAdmin.TerminalID
            ObjClsCashMemoPrint.EnableMailReSend = clsDefaultConfiguration.EnableMailReSend
            If Not DSFailedMailData Is Nothing Then
                If Not DSFailedMailData.Tables(0) Is Nothing AndAlso DSFailedMailData.Tables(0).Rows.Count > 0 Then
                    For Each DrFaildMail In DSFailedMailData.Tables(0).Rows
                        ObjClsCashMemoPrint.FailedMailReSend(clsAdmin.SiteCode, DrFaildMail("DocumentNumber"), DrFaildMail("CSVDocPath"), DrFaildMail("CSVTO"), DrFaildMail("NanoId"), DrFaildMail("DocumentType"), DrFaildMail("Subject"), DrFaildMail("MailBody"), DrFaildMail("CSVCC"), DrFaildMail("CSVBCC"))
                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'code Added By Roshan on 16-03-2018 for Spectrum New Developement
    Public LowStockNotificationTimer As New Timer()
    Public OnlineOrderNotificationTimer As New Timer()
    Public dtLowStockNotificationDtl As DataTable
    Public OnlineOrderRejectionTimer As New Timer()

    Private Sub LowStockNotificationTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            'roshab
            'Dim obj As New frmProductNotificationPopups
            'obj.dtCreditSales = objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode)
            dtLowStockNotificationDtl = objclsCommon.GetLowStockNotificationDetails(clsAdmin.SiteCode)


            If dtLowStockNotificationDtl.Rows.Count > 0 Then
                LowStockNotificationTimer.Stop()
                PopupLowStock.TitleText = "Low Stock Notifications"
                PopupLowStock.ContentText = "Please check articles that are low on stock"
                PopupLowStock.Image = My.Resources.BellAlert
                PopupLowStock.ShowCloseButton = True
                PopupLowStock.ShowOptionsButton = False
                PopupLowStock.ShowGrip = False
                PopupLowStock.Delay = clsDefaultConfiguration.LowStockNotificationInterval * 60 * 1000
                PopupLowStock.AnimationInterval = 10
                PopupLowStock.AnimationDuration = 1000
                PopupLowStock.TitlePadding = New Padding(0)
                PopupLowStock.ContentPadding = New Padding(0)
                PopupLowStock.ImagePadding = New Padding(0)
                PopupLowStock.Scroll = False
                PopupLowStock.Popup()
                LowStockNotificationTimer.Start()
                Me.Enabled = True
                'Me.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub OnlineOrderNotificationTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            'roshab
            'Dim obj As New frmProductNotificationPopups
            'obj.dtCreditSales = objclsCommon.GetCreditSalesPopupDetails(clsAdmin.SiteCode)
            'dtLowStockNotificationDtl = objclsCommon.GetLowStockNotificationDetails(clsAdmin.SiteCode)

            Dim DS = objclsCommon.GetSQlData("select * from DPACashMemoHdr where orderstatus ='New Order';select isnull(count(*),0) 'count' from DPACashMemoHdr where orderstatus ='New Order'")
            If Not DS Is Nothing Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Dim CMD As String = "UPDATE DPACashMemoHdr set OrderStatus ='NOTIFY' where OrderStatus ='New Order'"
                    If objCM.InsertOrUpdateRecord(CMD.ToString()) = True Then

                    End If
                    OnlineOrderNotificationTimer.Stop()
                    PopupOnlineOrders.TitleText = "online Orders"

                    If DS.Tables(1).Rows.Count Then
                        PopupOnlineOrders.ContentText = "Hi! You Have " & DS.Tables(1).Rows(0)("count") & " New Orders."
                    End If

                    PopupOnlineOrders.Image = My.Resources.BellAlert
                    PopupOnlineOrders.ShowCloseButton = True
                    PopupOnlineOrders.ShowOptionsButton = False
                    PopupOnlineOrders.ShowGrip = False
                    PopupOnlineOrders.Delay = clsDefaultConfiguration.LowStockNotificationInterval * 60 * 1000
                    PopupOnlineOrders.AnimationInterval = 10
                    PopupOnlineOrders.AnimationDuration = 1000
                    PopupOnlineOrders.TitlePadding = New Padding(0)
                    PopupOnlineOrders.ContentPadding = New Padding(0)
                    PopupOnlineOrders.ImagePadding = New Padding(0)
                    PopupOnlineOrders.Scroll = False
                    PopupOnlineOrders.Popup()
                    OnlineOrderNotificationTimer.Start()
                    Me.Enabled = True
                End If
            End If

            'Me.Focus()
            '   End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub OnlineOrderRejectionTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            Dim dtRejectOrder = objclsCommon.GetSQlData("select BillNo,ExternalOrderReferenceID,NetAmt,HDName 'CustomerName',DeliveryPartnerId 'DeliveryPartner' from cashmemohdr where BillIntermediateStatus <>'Deleted' and ExternalOrderReferenceID in (select ExternalOrderReferenceID  from dpacashmemohdr where orderstatus='Void')")
            If Not dtRejectOrder Is Nothing Then
                If dtRejectOrder.Tables(0).Rows.Count > 0 Then
                    Dim CMD As String = "UPDATE DPACashMemoHdr set OrderStatus ='VOIDNOTIFY' , Status =0 where OrderStatus ='VOID'"
                    If objCM.InsertOrUpdateRecord(CMD.ToString()) = True Then

                    End If
                    OnlineOrderRejectionTimer.Stop()
                    PopupRejectOrder.TitleText = "Rejected Orders"

                    If dtRejectOrder.Tables(0).Rows.Count Then
                        PopupRejectOrder.ContentText = "Hi! You Have  " & dtRejectOrder.Tables(0).Rows.Count & "  rejected order."
                    End If

                    PopupRejectOrder.Image = My.Resources.BellAlert
                    PopupRejectOrder.ShowCloseButton = True
                    PopupRejectOrder.ShowOptionsButton = False
                    PopupRejectOrder.ShowGrip = False
                    PopupRejectOrder.Delay = clsDefaultConfiguration.LowStockNotificationInterval * 60 * 1000
                    PopupRejectOrder.AnimationInterval = 10
                    PopupRejectOrder.AnimationDuration = 1000
                    PopupRejectOrder.TitlePadding = New Padding(0)
                    PopupRejectOrder.ContentPadding = New Padding(0)
                    PopupRejectOrder.ImagePadding = New Padding(0)
                    PopupRejectOrder.Scroll = False
                    PopupRejectOrder.Popup()
                    OnlineOrderRejectionTimer.Start()
                    Me.Enabled = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub PopupLowStock_Click(sender As Object, e As EventArgs) Handles PopupLowStock.Click
        dtLowStockNotificationDtl = objclsCommon.GetLowStockNotificationDetails(clsAdmin.SiteCode)
        Dim obj As New frmLowStockNotificationPopup()
        'PopupCreditSale.Dispose()
        obj.dtLowStock = dtLowStockNotificationDtl
        obj.PopupType = enumProductNotificationTimerPopups.LowStockPoup
        obj.ShowDialog()
        obj.BringToFront()
        PopupLowStock.Hide()
    End Sub
    Private Sub PopupOnlineOrders_Click(sender As Object, e As EventArgs) Handles PopupOnlineOrders.Click
        '  dtLowStockNotificationDtl = objclsCommon.GetLowStockNotificationDetails(clsAdmin.SiteCode)
        Dim objHD As New frmonlineOrders(False)
        objHD.ShowDialog()
        objHD.BringToFront()
        PopupOnlineOrders.Hide()
    End Sub
    Private Sub PopupRejectOrder_Click(sender As Object, e As EventArgs) Handles PopupRejectOrder.Click
        Try
            Dim dtRejectOrder = objclsCommon.GetSQlData("select BillNo,ExternalOrderReferenceID,NetAmt ,HDName 'CustomerName',DeliveryPartnerId 'DeliveryPartner' from cashmemohdr where BillIntermediateStatus <>'Deleted' and ExternalOrderReferenceID in (select ExternalOrderReferenceID  from dpacashmemohdr where orderstatus='VOIDNOTIFY')")
            If Not dtRejectOrder Is Nothing Then
                If dtRejectOrder.Tables(0).Rows.Count > 0 Then
                    Dim obj As New FrmDisplayRejectOrder
                    obj.dt = dtRejectOrder
                    obj.ShowDialog()
                    obj.BringToFront()
                    PopupRejectOrder.Hide()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub MDISpectrum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            DirectReportToolStripMenuItem.Visible = False
            DayCloseReportToolStripMenuItem.Visible = False
            ProcSchedulerToolStripMenuItem.Visible = False
            CashMemo.Visible = False
            BirthList.Visible = False
            SalesOrder.Visible = False
            ProductInfoMenuItem.Visible = True
            clsDefaultConfiguration.SpectrumLicenseRequired = True

            'Change
            'If TSEncryption.Instance.ValidateLicenseKey() Then
            clsLogin.AppStartUpPath = Application.StartupPath
            'End of change

            'If My.Settings.Server = String.Empty Then
            If ReadSpectrumParamFile("Server") = String.Empty Then
                Dim frmcon As New frmNDbConnectionBuild
                frmcon.ShowDialog()
            End If

            'If My.Settings.LocalConnectionString = String.Empty Then
            If ReadSpectrumParamFile("LocalConnectionString") = String.Empty Then
                Dim frmcon As New frmNDbLocalConnectionBuild
                frmcon.ShowDialog()
            End If



            'DataBaseConnection._OnlineConn = My.Settings.ServerConnectionString
            'DataBaseConnection._OfflineConn = My.Settings.LocalConnectionString

            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            DataBaseConnection._OfflineConn = ReadSpectrumParamFile("LocalConnectionString")
            DataBaseConnection._CurrentStatus = True
            Me.CreditSaleAdjToolStripMenuItem.Text = getValueByKey("Crs019")
            Me.UpdateQuotationToolStripMenuItem.Text = getValueByKey("frmnquotationupdate")
            'If OnlineConnect = True Then
            'SetConnection()
            'Else
            'SetLocalConnection()
            'End If

            Dim objclslogin As New clsLogin
            If objclslogin.pDbConnTest() = False Then
                ShowMessage(getValueByKey("MDI03"), "MDI03 - " & getValueByKey("CLAE04"))
                DataBaseConnection._CurrentStatus = False
                'DataBaseConnection._OnlineConn = My.Settings.LocalConnectionString
                DataBaseConnection._OnlineConn = ReadSpectrumParamFile("LocalConnectionString")
                Dim frmcon As New frmNDbConnectionBuild
                frmcon.ShowDialog()
            End If

            clsAdmin.TerminalID = ReadSpectrumParamFile("TerminalID")
            clsAdmin.PrepStationID = ReadSpectrumParamFile("mstPrepStationID")

            clsAdmin.SiteCode = clsCommon.GetDefaultConfigValue(String.Empty, "LocalSiteCode")
            clsAdmin.CashDrawerWithoutDriver = objclsCommon.GetCashDrawerName(clsAdmin.SiteCode, clsAdmin.TerminalID)
            'added by khusrao adil on 04-07-2018 for Dynamic License Duration setting

            If clsCommon.GetLicenseDurationDays() < 1 Then 'Jayesh 06/02/2019
                clsAdmin.OfflinelicenseActivationDuration = 0
            Else
                If clsCommon.GetLicenseDurationDays() >= 365 Then
                    clsAdmin.OfflinelicenseActivationDuration = 365
                Else
                    clsAdmin.OfflinelicenseActivationDuration = clsCommon.GetLicenseDurationDays()
                End If
            End If
            'clsAdmin.OfflinelicenseActivationDuration = clsCommon.GetLicenseDurationDays()
            'added by khusrao adil on 02-11-2017 for jk sprint 31
            '  clsAdmin.LegacySiteCode = objclsCommon.GetSiteByCode(clsAdmin.SiteCode, LagacySiteCodeRequired:=True)
            clsAdmin.SiteStdCode = objclsCommon.GetSiteByCode(clsAdmin.SiteCode, SiteStdCodeRequired:=True)

            Dim objCM As New clsCashMemo
            rbnlblSiteCodeValue.Text = clsAdmin.SiteCode
            rbnlblSiteNameValue.Text = objCM.GetSiteName(clsAdmin.SiteCode)
            ''sql server not up 
            If objclslogin.pDbConnTest() = False Then
                End
            End If
            Me.Visible = False
            'Validate client license of Spectrum Application
            If clsDefaultConfiguration.SpectrumLicenseRequired AndAlso (Not ValidateClientLicense()) Then
                Return
            End If
            clsDefaultConfiguration.AutoUpdate = objclslogin.GetAutoUpdateFLdValue(clsAdmin.SiteCode)
            Me.Visible = True

            'Dim waitPopupMsg As frmSpecialPrompt
            'waitPopupMsg = ShowLicense("Day Close and Synchronization in Progress. Please wait..", getValueByKey("CLAE04"))
            'Application.DoEvents()

            'Threading.Thread.Sleep(50000000)

            'waitPopupMsg.Close()

            Dim dtTheme As DataTable = objclsCommon.GetDefaultSetting(clsAdmin.SiteCode, "0000")
            For Each dr As DataRow In dtTheme.Rows
                If dr("FLDLABEL") = "ThemeSelect" Then
                    clsDefaultConfiguration.ThemeSelect = dr("FLDVALUE").ToString
                ElseIf dr("FLDLABEL") = "IsTablet" Then
                    clsDefaultConfiguration.IsTablet = dr("FLDVALUE").ToString
                ElseIf dr("FLDLABEL") = "HostTerminals" Then
                    clsDefaultConfiguration.HostTerminals = dr("FLDVALUE").ToString
                End If
            Next
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
                HotelReservationToolStripMenuItem.Visible = True
            Else
                HotelReservationToolStripMenuItem.Visible = False
                'Dim strHost As String = clsDefaultConfiguration.HostTerminals.ToString()
                'If strHost.Contains(clsAdmin.TerminalID) Then
                '    Me.GrpHome.BackgroundImage = My.Resources.Host_HomePage_bg
                '    Me.MenuStrip.BackColor = Color.FromArgb(0, 113, 188)
                '    Me.MenuStrip.ForeColor = Color.FromArgb(255, 255, 255)
                '    Me.MenuStrip.ForeColor = Color.FromArgb(255, 255, 255)
                '    Me.MenuStrip.Font = New Font("Trebuchet MS", 11, FontStyle.Regular)
                '    '   Me.GrpHome.BackColor = Color.FromArgb(72, 72, 72)
                '    '  Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
                'End If
            End If
            If clsDefaultConfiguration.IsTablet Then
                'code is added by irfan on 22/9/2017 for background image in login page and mdispectrum page
                'Me.GrpHome.BackgroundImage = My.Resources.BackgroundImage2
                Dim imagepath As String
                imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath") '= "C:\MyFolder\mainbg.png"
                If Not Directory.Exists(imagepath) Then
                    If Not (String.IsNullOrEmpty(imagepath)) Then
                        If File.Exists(imagepath) Then
                            Me.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
                        Else
                            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                                Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                            End If
                        End If

                    Else
                        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                            Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                            'Else
                            '    Me.GrpHome.BackgroundImage = Nothing
                            '    Me.GrpHome.BackColor = Color.FromArgb(72, 72, 72)
                            '    Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
                        End If

                    End If
                Else
                    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                        Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                    End If

                End If
                Me.MenuStrip.BackColor = Color.FromArgb(0, 107, 163)
                Me.MenuStrip.Font = New Font("Verdana", 11, FontStyle.Regular)
                ToolsMenu.ForeColor = Color.White
                WindowsMenu.ForeColor = Color.White
                Transaction.ForeColor = Color.White
                PettyCash.ForeColor = Color.White
                ToolsToolStripMenuItem.ForeColor = Color.White
                ReportsToolsStripOuterMenuItem.ForeColor = Color.White
                HelpMenu.ForeColor = Color.White
                LogoutToolStripMenuItem.ForeColor = Color.White
                ExitToolStripMenuItem.ForeColor = Color.White
                TicketingSystemToolStripMenuItem.ForeColor = Color.White
            End If

            Dim ChildForm As New frmNLogin
            If Not ChildForm Is Nothing Then
                ChildForm.StartPosition = FormStartPosition.CenterParent
                Me.MenuStrip.Hide()
                'mdiGUI()
                ' Me.StatusStrip.Visible = False
                'Me.pnlTop.Visible = False
                'Me.btnKEYBOARD.Visible = False
                ChildForm.ShowDialog()
            End If
            'Else
            '    Application.Exit()
            'End If
            'CheckAllTransactionRights()

            If (Not ChildForm.ActiveControl.Name.Equals("btnCancel")) Then

                If (String.IsNullOrEmpty(clsAdmin.Financialyear)) Then
                    ShowMessage(True, getValueByKey("LG023"), "LG023 - " & getValueByKey("CLAE04"))
                    DisableTransactionMainMenu(False)
                    Exit Sub
                End If

                Dim dtDayOpenStatus As DataTable = objLogin.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)

                If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then

                    If clsDefaultConfiguration.ShiftManagement Then
                        Dim dt As DataTable = clsCommon.GetShiftName(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
                        If dt.Rows.Count > 0 Then
                            lblShiftName.Text = dt.Rows(0)("ShiftName").ToString()
                            lblShiftStatus.Text = dt.Rows(0)("OpenCloseStatus").ToString()
                        Else
                            lblShiftName.Visible = False
                            lblShiftStatus.Visible = False
                            RibbonSeparator5.Visible = False
                        End If
                    Else
                        lblShiftName.Visible = False
                        lblShiftStatus.Visible = False
                        RibbonSeparator5.Visible = False
                    End If

                    LastClosedDate = IIf(dtDayOpenStatus.Rows(0)("LastClosedDate") Is DBNull.Value, DateTime.MinValue, dtDayOpenStatus.Rows(0)("LastClosedDate"))
                    AnyTerminalOpen = dtDayOpenStatus.Rows(0)("AnyTerminalOpen")
                    clsAdmin.DayOpenDate = IIf(dtDayOpenStatus.Rows(0)("DayOpenDate") Is DBNull.Value, DateTime.MinValue, dtDayOpenStatus.Rows(0)("DayOpenDate"))

                    'commented by vipin added thiscode below
                    'If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False AndAlso dtDayOpenStatus.Rows(0)("DayOpenDate") IsNot DBNull.Value Then
                    '    If clsDefaultConfiguration.ShiftManagement = False Then
                    '        ShowMessage(getValueByKey("TLC002"), "TLC002 - " & getValueByKey("CLAE04"))
                    '    End If
                    '    'After Till Closed, All transactiom menu will be disabled
                    '    DisableTransactionMainMenu(False)
                    '    DayOpenMenuItem.Enabled = True
                    '    KDSToolStripMenuItem.Enabled = True ' to enable the kitchen display system transaction for kds
                    '    DayCloseToolStripMenuItem.Enabled = True
                    '    If clsDefaultConfiguration.ShiftManagement = True Then
                    '        ShiftOpen.Enabled = True
                    '        ShiftClose.Enabled = True
                    '    Else
                    '        TillClose.Enabled = True
                    '        TillOpen.Enabled = True
                    '    End If
                    '    Exit Sub
                    'End If

                    'If (dtDayOpenStatus.Rows(0)("DayOpenDate") Is DBNull.Value AndAlso AnyTerminalOpen = 0) Then

                    '    DisableTransactionMainMenu(False)
                    '    DayOpenMenuItem.Enabled = True

                    '    'LG017=Day Open Operation not performed. Select Day Open from 'Start' menu
                    '    ShowMessage(False, getValueByKey("LG017"), "LG017 - " & getValueByKey("CLAE04"))
                    'End If


                    If (dtDayOpenStatus.Rows(0)("DayOpenDate") Is DBNull.Value AndAlso AnyTerminalOpen = 0) Then

                        DisableTransactionMainMenu(False)
                        DayOpenMenuItem.Enabled = True
                        DayOpenMenuItem_Click(sender, New EventArgs()) 'vipin
                        'LG017=Day Open Operation not performed. Select Day Open from 'Start' menu
                        '   ShowMessage(False, getValueByKey("LG017"), "LG017 - " & getValueByKey("CLAE04"))
                    Else
                        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False AndAlso dtDayOpenStatus.Rows(0)("DayOpenDate") IsNot DBNull.Value Then
                            If clsDefaultConfiguration.ShiftManagement = False Then
                                'ShowMessage(getValueByKey("TLC002"), "TLC002 - " & getValueByKey("CLAE04"))
                            End If
                            'After Till Closed, All transactiom menu will be disabled
                            DisableTransactionMainMenu(False)
                            DayOpenMenuItem.Enabled = True
                            KDSToolStripMenuItem.Enabled = True ' to enable the kitchen display system transaction for kds
                            DayCloseToolStripMenuItem.Enabled = True
                            If clsDefaultConfiguration.ShiftManagement = True Then
                                ShiftOpen.Enabled = True
                                ShiftClose.Enabled = True
                            Else
                                TillClose.Enabled = True
                                TillOpen.Enabled = True
                            End If
                            ' Exit Sub
                        End If
                        If Not clsDefaultConfiguration.TillOpenDone AndAlso clsDefaultConfiguration.ShiftManagement = False Then
                            TillOpen_Click(sender, New EventArgs())
                        End If
                    End If

                    lblTodayDate.Text = clsAdmin.DayOpenDate.ToShortDateString()
                    Dim releaseDate = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly.Location)
                    lblReleaseDate.Text = String.Format("Release Date :{0}", releaseDate.ToShortDateString)
                End If

            End If
            If clsDefaultConfiguration.NotificationPopUpRequired Then
                NotificationTimer.Interval = Convert.ToInt32(1000 * 60 * clsDefaultConfiguration.NotificationTiming)
                AddHandler NotificationTimer.Tick, AddressOf NotificationTimer_Tick
                NotificationTimer.Start()
            End If
            'If String.IsNullOrEmpty(My.Settings.SchedularInterval.Trim()) Then
            '    My.Settings.SchedularInterval = 1
            '    My.Settings.Save()
            'End If

            If Not String.IsNullOrEmpty(My.Settings.SchedularInterval.Trim()) Then
                ScedularTimer.Interval = Convert.ToInt32(1000 * 60 * Convert.ToDouble(My.Settings.SchedularInterval.Trim))

                My.Settings.SchedularNextRunTime = DateTime.Now().AddMinutes(Convert.ToDouble(My.Settings.SchedularInterval.Trim()))
                My.Settings.Save()

                AddHandler ScedularTimer.Tick, AddressOf ScedularTimer_Tick
                ScedularTimer.Start()
            End If

            If Not String.IsNullOrEmpty(My.Settings.ProcSchedularInterval.Trim()) Then
                ProcScedularTimer.Interval = 20000   ' in milisec

                ' My.Settings.ProcSchedularNextRunTime = DateTime.Now().AddMinutes(Convert.ToDouble(My.Settings.ProcSchedularInterval.Trim()))
                My.Settings.Save()

                AddHandler ProcScedularTimer.Tick, AddressOf ProcScedularTimer_Tick
                ProcScedularTimer.Start()
            End If
            'Getting The Data of STR Notification Site And Timer Details to Datatable
            Dim objcomm As New clsCommon
            dtSite = objcomm.GetSTRNotificationSiteDetails(clsAdmin.SiteCode, clsAdmin.TerminalID)
            dtTimer = objcomm.GetSTRNotificationTimerDetails()
            AddHandler STRNotificationTimer.Tick, AddressOf STRNotificationTimer_Tick
            '-- if not define will not start timer ...
            If Not dtSite Is Nothing AndAlso dtSite.Rows.Count > 0 AndAlso dtTimer.Rows.Count > 0 Then
                STRNotificationTimer.Start()
            End If
            If clsDefaultConfiguration.ISReservationCancelOnTimer Then
                AutoCancelTimer.Interval = Convert.ToInt32(1000 * 60 * clsDefaultConfiguration.ReservationCancelTiming)
                AddHandler AutoCancelTimer.Tick, AddressOf AutoCancelTimer_Tick
                AutoCancelTimer.Start()
            End If
            ''Added By Ketan Ticketing System Notification Changes 
            If clsDefaultConfiguration.ClientForMail = "JK" Then

                Dim FirstTimeInterval As Integer = clsDefaultConfiguration.TicketingSystemPopupInterval + 5
                FirstNotificationTimer.Start()
                AddHandler FirstNotificationTimer.Tick, AddressOf FirstNotificationTimer_Tick
                FirstNotificationTimer.Interval = Convert.ToInt32(1000 * FirstTimeInterval)
            End If
            '' added By Ketan For E- Commerce :- at time of issue in JK
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "DispchNDelivery") = True Then
                Dim FirstTimeInterval As Integer = clsDefaultConfiguration.TicketingSystemPopupInterval + 5
                FirstNotificationTimer.Start()
                AddHandler FirstNotificationTimer.Tick, AddressOf FirstNotificationTimer_Tick
                FirstNotificationTimer.Interval = Convert.ToInt32(1000 * FirstTimeInterval)
            End If
            If clsDefaultConfiguration.IsPoleDisply Then
                'DtArticlePoleDisplay = objclsCommon.GetArticleONPoledisplay(clsAdmin.SiteCode)
                'If DtArticlePoleDisplay.Rows.Count > 0 Then
                '    thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf ScrollTextBox1))
                '    If Not thread.IsAlive Then
                '        thread.Start()
                '    End If
                'End If
            End If
            If clsDefaultConfiguration.IsPoleDisply Then
                DtArticlePoleDisplay = objclsCommon.GetArticleONPoledisplay(clsAdmin.SiteCode)
                If DtArticlePoleDisplay.rows.count > 0 Then
                    PoleDisplayTimer.Start()
                    AddHandler PoleDisplayTimer.Tick, AddressOf PoleDisplayTimer_Tick
                    PoleDisplayTimer.Interval = 3500
                End If
            End If
            '---------------------------------------------------------
            'added by khusrao adil on 13-03-2018 for spectrum new developement 
            '#######
            If clsDefaultConfiguration.AutoKOTGenerateTimeInterval <> "0" Then
                Dim _AutoKOTGenerateTimeInterval As Integer = Convert.ToInt32(clsDefaultConfiguration.AutoKOTGenerateTimeInterval)
                AutoKotGenerate.Interval = Convert.ToInt32(1000 * _AutoKOTGenerateTimeInterval)
                AutoKotGenerate.Start()

                AddHandler AutoKotGenerate.Tick, AddressOf AutoKotGenerate_Tick
            End If

            If clsDefaultConfiguration.EnableCreditSalesPopup Then
                Dim _creditSalesPopupInterval As Integer = Convert.ToInt32(clsDefaultConfiguration.CreditSalesPopupInterval)
                CreditSalesPopupTimer.Interval = Convert.ToInt32(1000 * _creditSalesPopupInterval * 60)
                CreditSalesPopupTimer.Start()

                AddHandler CreditSalesPopupTimer.Tick, AddressOf CreditSalesPopupTimer_Tick
            End If
            If clsDefaultConfiguration.EnableExpiryProductPopup Then
                Dim _EnableExpiryProductPopup As Integer = Convert.ToInt32(clsDefaultConfiguration.EnableExpiryProductPopup)
                ExpiryProductTimer.Interval = Convert.ToInt32(1000 * _EnableExpiryProductPopup * 60)
                ExpiryProductTimer.Start()

                AddHandler ExpiryProductTimer.Tick, AddressOf ExpiryProductTimer_Tick                                'ExpiryProductTimer_Tick
            End If

            If clsDefaultConfiguration.EnableLowStockNotificatonPopup Then
                Dim _lowStockNotificatonInterval As Integer = Convert.ToInt32(clsDefaultConfiguration.LowStockNotificationInterval)
                LowStockNotificationTimer.Interval = Convert.ToInt32(1000 * _lowStockNotificatonInterval * 60)
                LowStockNotificationTimer.Start()
                AddHandler LowStockNotificationTimer.Tick, AddressOf LowStockNotificationTimer_Tick
            End If
            If clsDefaultConfiguration.EnableSalesOrderPopup Then
                Dim _SalesOrderPopupInterval As Integer = Convert.ToInt32(clsDefaultConfiguration.SalesOrderPopupInterval)
                SalesOrderPopupTimer.Interval = Convert.ToInt32(1000 * _SalesOrderPopupInterval * 60)
                SalesOrderPopupTimer.Start()
                AddHandler SalesOrderPopupTimer.Tick, AddressOf SalesOrderPopupTimer_Tick

            End If
            If clsDefaultConfiguration.EnableMailReSend Then
                Dim _MailResendInterval As Integer = Convert.ToInt32(clsDefaultConfiguration.FailedMailReSendInterval) 'vipin Mail Resend 08.08.2018
                FailedMailResend.Interval = Convert.ToInt32(1000 * _MailResendInterval * 60)
                FailedMailResend.Start()
                AddHandler FailedMailResend.Tick, AddressOf FailedMailResend_Click

            End If
            If clsDefaultConfiguration.ExternalOrdersTillNo = clsAdmin.TerminalID Then
                If clsDefaultConfiguration.EnableOnlineOrderNotification Then
                    Dim _EnableOnlineOrderNotification As Integer = Convert.ToInt32(clsDefaultConfiguration.OnlineOrderNotificationInterval)
                    OnlineOrderNotificationTimer.Interval = Convert.ToInt32(1000 * _EnableOnlineOrderNotification)
                    OnlineOrderNotificationTimer.Start()
                    AddHandler OnlineOrderNotificationTimer.Tick, AddressOf OnlineOrderNotificationTimer_Tick
                End If
            End If
            CallWebServicesToolStripMenuItem.Visible = False
            If CheckAuthorisation(clsAdmin.UserCode, "GetItemUpdates") = False Then
                CallWebServicesToolStripMenuItem.Visible = False
            Else
                If clsDefaultConfiguration.ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH.Contains(clsAdmin.TerminalID) Then
                    CallWebServicesToolStripMenuItem.Visible = True
                End If
            End If
            If clsDefaultConfiguration.ExternalOrdersTillNo = clsAdmin.TerminalID Then
                If clsDefaultConfiguration.EnableRejectOrderNotification Then
                    Dim _OnlineOrderRejectionTimer As Integer = Convert.ToInt32(clsDefaultConfiguration.OnlineOrderRejectionInterval)
                    OnlineOrderRejectionTimer.Interval = Convert.ToInt32(1000 * _OnlineOrderRejectionTimer)
                    OnlineOrderRejectionTimer.Start()
                    AddHandler OnlineOrderRejectionTimer.Tick, AddressOf OnlineOrderRejectionTimer_Tick
                End If

            End If
            If clsDefaultConfiguration.EnableHashTagIntegration = True Then
                Try
                    Dim tempId = Shell(Application.StartupPath & "\run_hasTag.bat", AppWinStyle.Hide)
                Catch ex As Exception
                    LogException(ex)
                End Try
                ' Dim tempId = Process.Start(Application.StartupPath & "\first_simple_batch.bat")
            End If

            MDISpectrum_Shown(sender, New EventArgs()) 'vipin
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Shared PoleDisplayTimer As New Timer()
    Dim dtrow As Integer = 1
    Dim Articlename As String
    Dim ArticlPrice As String
    Dim DtArticlePoleDisplay
    ' Dim ComPortName As String = clsDefaultConfiguration.SerialPort
    Private Sub PoleDisplayTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try

            If DtArticlePoleDisplay.rows.count > 0 Then
                ScrollTextBox11(dtrow, DtArticlePoleDisplay)

                If dtrow + 1 = DtArticlePoleDisplay.Rows.Count Then
                    dtrow = 0
                Else
                    dtrow = dtrow + 1
                End If

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub ScrollTextBox11(ByRef rowid As Integer, ByRef DtArticlePoleDisplay As DataTable)
        Try
            Dim ComPortName As String = clsDefaultConfiguration.SerialPort

            ' For i As Integer = 0 To DtArticlePoleDisplay.Rows.Count - 1
            Articlename = DtArticlePoleDisplay.Rows(rowid)("ArticleShortName").ToString()
            ArticlPrice = DtArticlePoleDisplay.Rows(rowid)("SellingPrice").ToString()
            Dim custStr1 As String = If(Articlename.Length <= 20, Articlename, Articlename.Substring(0, 20))
            Dim custStr2 As String = "      " + ArticlPrice + "      "
            Dim space As Integer = 0
            Dim space2 As Integer = 0
            space = 20 - custStr1.Length
            space2 = 20 - custStr2.Length
            custStr1 = custStr1.PadRight(20, " "c)
            custStr2 = custStr2.PadRight(20, " "c)
            Dim d As Integer = custStr1.Length
            Dim l As Integer = custStr2.Length
            Dim m_text As String = custStr1
            Dim m_text2 As String = custStr2

            'Next
            objclsCommon.Display20x2Line(m_text, m_text2, ComPortName)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub ShowChildForm(ByRef ChildForm As System.Windows.Forms.Form, Optional ByVal isdoctoparent As Boolean = False)
        'gmdiclientheight = Me.Height - 40
        'gmdiclientwidth = Me.Width - 10

        gmdiclientheight = ChildForm.Height
        gmdiclientwidth = ChildForm.Width


        Try
            'Dim ChildForm As New objChildForm
            'System.Windows.Forms.Form()
            ' Make it a child of this MDI form before showing it.
            For Each frmS As Form In MdiChildren
                If ChildForm.Name = frmS.Name Then
                    Exit Sub
                End If
            Next
            If LoginStatus = False And ChildForm.Name <> "frmNLogin" Then
                MsgBox(getValueByKey("MDI04"), "MDI04 - " & MsgBoxStyle.Critical, AcceptButton)
                ChildForm.Close()
                Exit Sub
            End If

            ChildForm.MdiParent = Me
            m_ChildFormNumber += 1
            ChildForm.Text = ChildForm.Text '& m_ChildFormNumber

            'lblTitle.Text = fnMakeTitle(, ChildForm.Text)

            If isdoctoparent = True Then
                ChildForm.Dock = DockStyle.Fill
                ChildForm.MainMenuStrip = MenuStrip

                'Rakesh-21.08.2013:Issue-7606-->Disappears title text
                ChildForm.MaximizeBox = False
            Else
                ChildForm.StartPosition = FormStartPosition.CenterScreen
            End If

            ChildForm.StartPosition = FormStartPosition.CenterScreen
            If ChildForm.Name <> "frmNLogin" Then
                'Close all child forms of the parent.
                For Each vChildForm As Form In Me.MdiChildren
                    If ChildForm.Name <> vChildForm.Name Then
                        vChildForm.Close()
                    End If
                Next
            Else
                ChildForm.StartPosition = FormStartPosition.CenterScreen
                Me.MenuStrip.Show()
            End If

            ChildForm.Select()
            Try
                ChildForm.Show()
            Catch ex As Exception
                LogException(ex)
                ChildForm.Close()
            End Try

        Catch ex As Exception
            LogException(ex)
            ChildForm.Close()
        End Try

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
             For Each frm As Form In MdiChildren
            ShowMessage(getValueByKey("MDI01"), "MDI01 - " & getValueByKey("CLAE04"))
            Exit Sub
        Next
        If Logout() = False Then
            Exit Sub
        End If
        LoginStatus = False
        Dim ChildForm As New frmNLogin
        If Not ChildForm Is Nothing Then
            ChildForm.StartPosition = FormStartPosition.CenterParent
            ChildForm.cboLanguage.SelectedValue = clsAdmin.LangCode
            ChildForm.txtusername.Focus()
            ChildForm.Focus()
            ' ShowChildForm(ChildForm, False)
            Me.MenuStrip.Hide() 'added by irfan
            ChildForm.ShowDialog() 'vipin

            ChildForm.Select()
            ChildForm.txtusername.Select()
            MDISpectrum_Shown(sender, New EventArgs()) 'vipin
            Me.MenuStrip.Show() ' added by ifan
        End If
    End Sub



    Public Function Logout() As Boolean
        Try
            Dim objlog As New clsLogin
            Dim IpAddress = objlog.getIPAddress()

            ''get Local  IP Address
            'Dim xEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
            'Dim ipAddr As Net.IPAddress() = xEntry.AddressList
            'Dim IpAddress As String = ipAddr(0).ToString()
            If objlog.InsertLoginHistory(clsAdmin.UserCode, clsAdmin.SiteCode, clsAdmin.TerminalID, False, IpAddress) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    Private Sub NewCashMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewCashMemo.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New Spectrum.frmCashMemo
        If ChildForm.Name <> String.Empty Then

            Try
                ShowChildForm(ChildForm, True)
                ChildForm.Select()
            Catch ex As Exception
                ChildForm.Close()

            End Try
        End If
    End Sub

    Private Sub BirthSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthSearchEdit.Click
        Dim ChildForm As New frmNBirthListUpdate
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub BirthListSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthListSales.Click
        Dim ChildForm As New frmNBirthListSales
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub BirthListNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthListNew.Click
        Dim ChildForm As New frmNBirthListCreate
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub SalesOrderCreation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderCreation.Click
        Dim ChildForm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            ChildForm = New Spectrum.frmPCSalesOrderCreation
        Else
            ChildForm = New Spectrum.frmNSalesOrderCreation
        End If

        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)

            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'Private Sub CashMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TillClose.Click
    '    ' Create a new instance of the child form.
    '    Dim ChildForm As New Spectrum.frmCashMemo
    '    Try
    '        If ChildForm.Name <> String.Empty Then
    '            ShowChildForm(ChildForm, True)
    '        End If
    '    Catch ex As Exception
    '        ChildForm.Close()
    '    End Try
    'End Sub

    Private Sub CashMemo_Click(sender As System.Object, e As System.EventArgs) Handles CashMemo.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New Spectrum.frmCashMemo
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    'Private Sub BirthList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftClose.Click
    '    BirthListNew_Click(sender, e)
    'End Sub

    'Private Sub SalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashMemo.Click
    '    SalesOrderCreation_Click(sender, e)
    'End Sub
    Private Sub BirthList_Click(sender As System.Object, e As System.EventArgs) Handles BirthList.Click
        BirthListNew_Click(sender, e)
    End Sub
    Private Sub SalesOrder_Click(sender As System.Object, e As System.EventArgs) Handles SalesOrder.Click
        SalesOrderCreation_Click(sender, e)
    End Sub
    Private Sub NewCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewCustomer.Click
        'Dim ChildForm As New Spectrum.frmNSOCustomer
        Dim ChildForm As New Spectrum.frmNLoyalityCustomer
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.BtnOk.Visible = False
                ChildForm.BtnExit.Visible = False
                'ADDSP
                ChildForm.Tag = "NEW"
                'ADDSP
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try


    End Sub

    Private Sub NewLoyaltyCustomer_Click(sender As System.Object, e As System.EventArgs) Handles NewLoyaltyCustomer.Click
        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
            Dim ChildForm As New Spectrum.frmNLoyalityCustomer
            Try
                If ChildForm.Name <> String.Empty Then
                    ChildForm.BtnOk.Visible = False
                    ChildForm.BtnExit.Visible = False
                    'ADDSP
                    ChildForm.Tag = "NEW"
                    'ADDSP
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Else
            Dim ChildForm As New Spectrum.frmNewCustomer
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        End If

    End Sub


    Private Sub SearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchEdit.Click
        Dim ChildForm As New Spectrum.frmNSOCustomer


        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.pSearchCust = "SEARCH"
                ChildForm.Tag = "NEW"
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try


    End Sub
    Private Sub SearchCLPCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchCLPCustomer.Click
        '  Dim ChildForm As New Spectrum.frmNSearchCLPLookUp("CLP", clsAdmin.SiteCode, "0")
        Dim ChildForm As New Spectrum.frmNLoyalityCustomer
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.pSearchCust = "SEARCH"
                ChildForm.Tag = "NEW"
                ShowChildForm(ChildForm, False)
                'ChildForm.MdiParent = Me
                'ChildForm.Dock = DockStyle.Fill

            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub SearchEditCashMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchEditCashMemo.Click
        Dim ChildForm As New Spectrum.frmCashMemo(True)

        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
                ChildForm.cmdOldCashMemo_Click(ChildForm, New System.EventArgs)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub SkinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim openfiledialog1 As New OpenFileDialog
        openfiledialog1.FileName = ""
        Dim path = System.Windows.Forms.Application.StartupPath
        openfiledialog1.InitialDirectory = path.Substring(0, path.Length - 3) + "skin"
        openfiledialog1.ShowDialog()
        If (openfiledialog1.FileName <> "") Then
            'SkinfeatureDotNet.LoadSkinFile(openfiledialog1.FileName, "")
        End If
    End Sub
    Private Sub NewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    'Private Sub EditMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditMenuItem.Click
    '    Dim ChildForm As New Spectrum.frmNTillClosing()
    '    If ChildForm.Name <> String.Empty Then
    '        ShowChildForm(ChildForm, False)
    '    End If
    'End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        For Each frm As Form In Me.MdiChildren
            If frm.IsDisposed = False Then
                ShowMessage(getValueByKey("MDI01"), "MDI01 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
        Next
        If Logout() = False Then
            Exit Sub
        End If
        Dim ComPortName As String = clsDefaultConfiguration.SerialPort
        objclsCommon.Display20x2Line("", "", ComPortName, True)
        Application.Exit()

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Try
            '-----------------------------------Flicker isssue resolution code
            'added by khusrao adil on 13-06-2018 
            Dim style As Integer = NativeWinAPI.GetWindowLong(Me.Handle, NativeWinAPI.GWL_EXSTYLE)
            style = style Or NativeWinAPI.WS_EX_COMPOSITED
            NativeWinAPI.SetWindowLong(GrpHome.Handle, NativeWinAPI.GWL_EXSTYLE, style)
            '-----------------------------------Flicker isssue resolution code end
            Dim releaseDate = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly.Location)
            lblReleaseDate.Text = String.Format("Release Date :{0}", releaseDate.ToShortDateString)
            lblVersion.Text = String.Format("Ver({0})", My.Application.Info.Version.ToString())

            spectrumTimer.Start()

            'My.Resources.main_bg.FromFile("C:\MyFolder\mainbg.png", True)
            Me.BackgroundImage = Nothing 'System.Drawing.Bitmap.FromFile("C:\MyFolder\mainbg.png")
            Dim imagepath As String = "C:\MyFolder\mainbg.png"
            imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath")

            Me.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
        Catch ex As Exception

        End Try

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    ''' <summary>
    ''' STR Notification Timer Tick Event for Showing Popup window
    ''' </summary>
    ''' <remarks></remarks>
    Dim i As Integer = 0
    Dim fnFirstCall As Boolean = True
    Private Sub STRNotificationTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            Dim Currenttime = DateTime.Now.ToString("HH:mm")
            Dim IntervalInMillisec = TimeSpan.FromMinutes(dtTimer.Rows(i)("Interval"))
            STRNotificationTimer.Interval = IntervalInMillisec.TotalMilliseconds
            If Currenttime > dtTimer.Rows(i)("ToTime") Then
                i = i + 1
                fnFirstCall = True
                ' STRNotificationTimer.Stop()
                If i = dtTimer.Rows.Count Then
                    STRNotificationTimer.Stop()
                    Exit Sub
                End If
            End If
            If dtTimer.Rows(i)("FromTime") = Currenttime Then
                fnFirstCall = False
            End If
            If fnFirstCall Then
                fnFirstCall = False
                Exit Sub
            End If
            If (dtTimer.Rows(i)("FromTime") = Currenttime) Or Currenttime > dtTimer.Rows(i)("FromTime") Then
                If Currenttime > dtTimer.Rows(i)("ToTime") Then
                    STRNotificationTimer.Stop()
                    Exit Sub
                End If
                STRNotificationTimer.Stop()
                Dim objSTRNotification As New frmPCSTRReminders
                Me.DialogResult = objSTRNotification.ShowDialog()

                If Me.DialogResult = Windows.Forms.DialogResult.OK Then
                    ' Me.Close()
                ElseIf Me.DialogResult = Windows.Forms.DialogResult.Yes Then
                    Dim objsop As New frmPCNSalesOrderUpdate
                    objsop.StrNotificationSONo = objSTRNotification.SONumber
                    ShowChildForm(objsop, True)
                End If
            End If

            STRNotificationTimer.Start()

        Catch Ex As Exception
            LogException(Ex)
        End Try
    End Sub

    Private Sub ScedularTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            ScedularTimer.Interval = Convert.ToInt32(1000 * 60 * Convert.ToDouble(My.Settings.SchedularInterval.Trim))
            ScedularTimer.Stop()
            If Not String.IsNullOrEmpty(My.Settings.SchedulerUrl.Trim()) AndAlso Not String.IsNullOrEmpty(My.Settings.SchedularInterval.Trim()) Then
                If String.IsNullOrEmpty(My.Settings.SchedularLastRunTime.Trim()) Then
                    Dim Proc As New System.Diagnostics.Process
                    Proc.StartInfo = New ProcessStartInfo(My.Settings.SchedulerUrl)
                    Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Proc.Start()
                    'System.Diagnostics.Process.Start((My.Settings.SchedulerUrl))
                ElseIf (Convert.ToDateTime(My.Settings.SchedularLastRunTime).AddMinutes(My.Settings.SchedularInterval) <= DateTime.Now()) Then
                    Dim Proc As New System.Diagnostics.Process
                    Proc.StartInfo = New ProcessStartInfo(My.Settings.SchedulerUrl)
                    Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Proc.Start()
                    'Proc.StartInfo.CreateNoWindow = True
                    'System.Diagnostics.Process.Start((My.Settings.SchedulerUrl))

                End If
                My.Settings.SchedularLastRunTime = DateTime.Now()
                My.Settings.SchedularNextRunTime = DateTime.Now().AddMinutes(Convert.ToDouble(My.Settings.SchedularInterval.Trim()))
                My.Settings.Save()
            End If
            ScedularTimer.Start()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ProcScedularTimer_Tick(sender As Object, e As EventArgs)
        Try
            ProcScedularTimer.Interval = 20000
            ProcScedularTimer.Stop()
            If Not String.IsNullOrEmpty(My.Settings.ProcSchedularInterval.Trim()) Then
                If String.IsNullOrEmpty(My.Settings.ProcSchedularLastRunTime.Trim()) Then
                    If (objclsCommon.ExecuteSchedulerProcedure()) Then
                        My.Settings.ProcSchedularLastRunTime = DateTime.Now()
                        My.Settings.ProcSchedularNextRunTime = DateTime.Now().AddMinutes(24 * 60 * (Convert.ToDouble(My.Settings.ProcSchedularInterval.Trim())))
                        My.Settings.Save()
                    End If
                ElseIf (Convert.ToDateTime(My.Settings.ProcSchedularLastRunTime).AddMinutes(24 * 60 * Convert.ToDouble(My.Settings.ProcSchedularInterval.Trim())) <= DateTime.Now()) Then
                    If (objclsCommon.ExecuteSchedulerProcedure()) Then
                        My.Settings.ProcSchedularLastRunTime = DateTime.Now()
                        My.Settings.ProcSchedularNextRunTime = DateTime.Now().AddMinutes(24 * 60 * (Convert.ToDouble(My.Settings.ProcSchedularInterval.Trim())))
                        My.Settings.Save()
                    End If
                End If
            End If
            ProcScedularTimer.Start()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub NotificationTimer_Tick(ByVal sender As Object, e As EventArgs)
        NotificationTimer.Stop()
        If clsDefaultConfiguration.ClientForMail = "JK" Then
            Dim obj As New frmTargetVSActualSaleDisplayPopup
            obj.ShowDialog()
            obj.BringToFront()
        Else
            ShowMessage(clsDefaultConfiguration.NotificationText, "Notification")
        End If
        NotificationTimer.Start()

    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ' Declare a variable of type Graphics named formGraphics.
        ' Assign the address (reference) of this forms Graphics object
        ' to the formGraphics variable.
        Dim formGraphics As Graphics = e.Graphics
        ' Declare a variable of type LinearGradientBrush named gradientBrush.
        ' Use a LinearGradientBrush constructor to create a new LinearGradientBrush object.
        ' Assign the address (reference) of the new object
        ' to the gradientBrush variable.
        Dim gradientBrush As New LinearGradientBrush(New Point(0, 0), New Point(Width, 0), Color.White, Color.DeepSkyBlue)

        ' Here are two more examples that create different gradients.
        ' Comment the Dim statement immedately above and uncomment one of these
        ' Dim statements to see how varying the two colors changes the gradient result.
        ' Dim gradientBrush As New LinearGradientBrush(New Point(0, 0), New Point(Width, 0), Color.Chartreuse, Color.SteelBlue)
        ' Dim gradientBrush As New LinearGradientBrush(New Point(0, 0), New Point(Width, 0), Color.White, Color.SteelBlue)

        formGraphics.FillRectangle(gradientBrush, ClientRectangle)
    End Sub

    'Private Sub lblMenuHideShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenuHideShow.Click
    '    If lblMenuHideShow.Text = ">>" Then
    '        MenuStrip.Hide()
    '        lblMenuHideShow.Text = "<<"

    '    Else
    '        MenuStrip.Show()
    '        pnlTop.Height = 20
    '        lblMenuHideShow.Text = ">>"
    '    End If
    'End Sub

    'Private Sub mdiGUI()
    '    lblVersion.Text = "[Spectrum Ver." & Application.ProductVersion & "]          ."

    '    'BitmapRegion.CreateControlRegion(this, bmpFrmBack)
    '    'Dim formGraphics As Graphics = Graphics
    '    'Dim gradientBrush As New LinearGradientBrush(New Point(0, 0), New Point(Width, 0), Color.White, Color.DeepSkyBlue)
    '    'formGraphics.FillRectangle(gradientBrush, ClientRectangle)


    'End Sub


    Private Sub EditOpenTill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim ChildForm As New Spectrum.frmNTillOpen(True)
        'If ChildForm.Name <> String.Empty Then
        '    ShowChildForm(ChildForm, False)
        'End If
    End Sub

    Private Sub btnKEYBOARD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKEYBOARD.Click
        Process.Start("osk.exe")
    End Sub


    Private Sub HardwareSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HardwareSetupToolStripMenuItem.Click
        'Dim ChildForm As New Spectrum.frmPOSDeviceProfile
        'If ChildForm.Name <> String.Empty Then
        '    ShowChildForm(ChildForm, False)
        'End If
    End Sub

    Private Sub PageSettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ChildForm As New Spectrum.frmNPrintingSettings
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub POSDeviceSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POSDeviceSettingsMenuItem.Click
        Dim ChildForm As New Spectrum.frmPOSDeviceProfile
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub


    Private Sub SyncReportMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncReportToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmSyncReport
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'Private Sub ManualPromotionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualPromotionsToolStripMenuItem.Click
    '    Dim ChildForm As New Spectrum.frmManualPromotions
    '    If ChildForm.Name <> String.Empty Then
    '        ShowChildForm(ChildForm)
    '    End If
    'End Sub

    Private Sub BirthlistItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthlistItems.Click
        Dim ChildForm As New Spectrum.frmNBirthListUpdate


        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.IsReprintView = True
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub BirthlistSalesInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthlistSalesInvoiceToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNBirthListSales
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'Private Sub DefaultSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ChildForm As New Spectrum.frmDefaultSettings
    '    Try
    '        If ChildForm.Name <> String.Empty Then
    '            ShowChildForm(ChildForm, False)
    '        End If
    '    Catch ex As Exception
    '        ChildForm.Close()
    '    End Try
    'End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.AboutBox
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub MDISpectrum_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        Me.MenuStrip.Hide()
        GrpHome.Hide()
        If MdiChildren.Count = 1 Then
            For Each frm As Form In MdiChildren()
                If frm.Name = "frmNLogin" Then
                    frm.Focus()
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub LocalConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocalConnectionToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNDbLocalConnectionBuild
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub OnlineConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineConnectionToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNDbConnectionBuild
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'Private Sub StockCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthList.Click
    '    Try
    '        Dim objfrmStockCheck As New frmNStockCheck
    '        objfrmStockCheck.ShowDialog()
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub
    Private Sub StockCheck_Click(sender As System.Object, e As System.EventArgs) Handles StockCheck.Click
        Try
            Dim ChildForm As New Spectrum.frmNStockCheck
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub KeyBoardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyBoardToolStripMenuItem.Click
        Process.Start("osk.exe")
    End Sub

    Private Sub BirthListMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthListMenuItem.Click
        Try
            Dim objReprint As New frmNReprint
            PrintTransType = "BirthList"
            objReprint.Text = getValueByKey("frmnreprintbirthlist")
            objReprint.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SalesOrderMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderMenuItem.Click
        Try
            Dim objReprint As New frmNReprint
            PrintTransType = "SalesOrder"
            objReprint.Text = getValueByKey("frmnreprintsalesorder")
            objReprint.ShowDialog()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CashMemoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashMemoMenuItem.Click
        Try
            Dim objReprint As New frmNReprint
            PrintTransType = "CashMemo"
            objReprint.Text = getValueByKey("frmnreprintcashmemo")
            objReprint.ShowDialog()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ReturnCashMemoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnCashMemoMenuItem.Click
        Try
            Dim objReprint As New frmNReprint
            PrintTransType = "ReturnCashMemo"
            objReprint.Text = getValueByKey("frmnreprintreturncashmemo")
            objReprint.ShowDialog()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub SyncSettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncSettingToolStripMenuItem.Click
        Try
            Dim ChildForm As New Spectrum.frmSyncSettings
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OutboundDeliveryMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutboundDeliveryMenuItem.Click
        Try
            Dim ChildForm As New Spectrum.frmNOutboundDelivery
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetTerminalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetTerminalToolStripMenuItem.Click
        Try
            Dim ChildForm As New frmNTerminal
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChangeTenderModeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeTenderModeMenuItem.Click
        Try
            Dim ChildForm As New frmNChangeTenderMode
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TillOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TillOpen.Click
        If IsMettlerPaymentTill() Then
            If clsDefaultConfiguration.TillOperationRequired = True AndAlso clsDefaultConfiguration.ShiftManagement = False Then
                Dim isAutoLogout As Integer = 0
                Dim ChildForm As New Spectrum.frmNTillOpen(isAutoLogout)
                Try
                    If isAutoLogout = 0 Then
                        If ChildForm.Name <> String.Empty Then
                            'ShowChildForm(ChildForm)
                            ChildForm.ShowDialog() 'vipin
                        End If
                    ElseIf isAutoLogout = 1 Then
                        LogoutToolStripMenuItem_Click(Me.LogoutToolStripMenuItem, New EventArgs())
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            Else
                If clsDefaultConfiguration.ShiftManagement = False Then
                    ShowMessage(getValueByKey("MDI02"), "MDI02 - " & getValueByKey("CLAE04"))
                End If
            End If
        End If


    End Sub

    Private Sub ShiftOpen_Click(sender As System.Object, e As System.EventArgs) Handles ShiftOpen.Click
        Try
            ''Added by ketan Check Day Open Or close (Shift Open issue)
            Dim Status As Boolean = objclsCommon.GetDayOpenOrClose(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
            If Status Then
                ShowMessage("Previous day has already been closed. Click OK to exit from the application. Please restart the application to open a terminal again.", "Information")
                ' MessageBox.Show("Previous day has already been closed. Click OK to exit from the application. Please restart the application to open a terminal again.")
                Application.Exit()
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Dim isAutoLogout As Integer = 0
        Dim ChildForm As New Spectrum.frmShiftOpen(isAutoLogout)
        Try
            If isAutoLogout = 0 Then
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm)
                End If
            ElseIf isAutoLogout = 1 Then
                LogoutToolStripMenuItem_Click(Me.LogoutToolStripMenuItem, New EventArgs())
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'Private Sub TillClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShiftOpen.Click
    '    Dim ChildForm As New Spectrum.frmTillFinancialReport
    '    Try
    '        If ChildForm.Name <> String.Empty Then
    '            ShowChildForm(ChildForm, False)
    '        End If
    '    Catch ex As Exception
    '        ChildForm.Close()
    '    End Try


    'End Sub

    Private Sub ShiftClose_Click(sender As System.Object, e As System.EventArgs) Handles ShiftClose.Click
        Dim ChildForm As New Spectrum.frmShiftFinancialReport
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub TillClose_Click(sender As System.Object, e As System.EventArgs) Handles TillClose.Click

        If IsMettlerPaymentTill() Then
            Dim ChildForm As New Spectrum.frmTillFinancialReport
            Try
                Dim objDayCLose As New clsDayClose
                If clsDefaultConfiguration.ClientForMail = "JK" Then
                    If Not CheckAuthorisation(clsAdmin.UserCode, "TillCloseWOSale") Then
                        If objDayCLose.CheckBillCount(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then
                            'MessageBox.Show("Day close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
                            ShowMessage("Till close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
                            Exit Sub
                        End If
                    End If
                End If
                If objDayCLose.IsBillSettleBoforeTillClose() Then
                    ShowMessage("Till close not allowed since DineIn Tables are Reserved.", "WARNING MESSAGE")
                    Exit Sub
                End If
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        End If
    End Sub
    Private Sub PrinterDocMapMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ChildForm As New Spectrum.frmNPrinterTillMapping
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub


    Private Sub DefaultConfigMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultConfigMenu.Click
        Dim ChildForm As New Spectrum.frmConfigurationSettings
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub PrinterDocumentMapToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrinterDocumentMapToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNPrinterTillMapping
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub PrintingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintingToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNPrintingSettings
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub DataArchiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataArchiveToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmDataArcive
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub SchedulerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SchedulerToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmScheduler
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub ChangeCheckDueDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCheckDueDateToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmUpdateCheckDueDate
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub FastCashMemoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FastCashMemoToolStripMenuItem.Click
        Try
            'added by khusrao adil on 18-04-2017 for natural 
            If clsDefaultConfiguration.EnableNewTouchCashMemo = True Then
                Dim ChildForm = New Spectrum.frmTouchCashMemo
                If ChildForm.Name <> String.Empty Then
                    lblUserId.Visible = False
                    lblLoggedIn.Visible = False
                    lblTodayDate.Visible = False
                    If clsDefaultConfiguration.RenderGrievance Then
                        lblReleaseDate.Text = "Day Open Date:" + lblTodayDate.Text
                        lblReleaseDate.Font = lblTodayDate.Font
                    End If

                    RibbonSeparator1.Visible = False
                    RibbonSeparator2.Visible = False
                    RibbonSeparator3.Visible = False
                    ShowChildForm(ChildForm, False)
                End If
            Else
                Dim ChildForm = New Spectrum.frmFastCashMemo
                If ChildForm.Name <> String.Empty Then
                    lblUserId.Visible = False
                    lblLoggedIn.Visible = False
                    lblTodayDate.Visible = False
                    RibbonSeparator1.Visible = False
                    RibbonSeparator2.Visible = False
                    RibbonSeparator3.Visible = False
                    ShowChildForm(ChildForm, False)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''added by khusrao adil on 10-05-2018 for spectrum lite transaction 
    Public Sub SpectrumLiteTransactions(ByVal value As Boolean)
        BarcodePrintToolStripMenuItem.Visible = value
        CharacteristicsToolStripMenuItem.Visible = value
        ItemToolStripMenuItem.Visible = value
        ManualPromotionToolStripMenuItem.Visible = value
        SiteDetailsToolStripMenuItem.Visible = value
        SupplierToolStripMenuItem.Visible = value
        StockInToolStripMenuItem.Visible = value
        StockOutToolStripMenuItem.Visible = value
        TaxToolStripMenuItem.Visible = value
        TenderToolStripMenuItem.Visible = value
        CreateEditUserToolStripMenuItem.Visible = value
        SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Visible = value
        ChangePasswordToolStripMenuItem.Visible = value
        UploadItemToolStripMenuItem.Visible = value
        ItemMasterToolStripMenuItem.Visible = value
        ItemHiToolStripMenuItem.Visible = value
        ImportExportToolStripMenuItem.Visible = value
        ExportArticleXLSReportToolStripMenuItem.Visible = value
        ExportArticleHierarchyToolStripMenuItem.Visible = value
        DefineComboToolStripMenuItem.Visible = value
        PosTabCreationToolStripMenuItem.Visible = value
        DefineKitToolStripMenuItem.Visible = value
        TableManagmentToolStripMenuItem.Visible = value
        'ashma - 15 may 2018 for ShiftManagement
        PrepStationToolStripMenuItem2.Visible = value
        ShiftManagementToolStripMenuItem.Visible = value

    End Sub
    Public Sub CheckAllTransactionRights(ByVal isHOinstance As Boolean)
        Dim cntBtn As Integer = 0
        'modified by khusrao adil on 10-05-2018
        ''-----------------------------------------------spectrum lite transaction start
        If clsDefaultConfiguration.SpectrumLiteAllowed = False Then
            SpectrumLiteTransactions(False)
        Else
            'SpectrumLiteTransactions(True)
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_BarcodeP") = False Then
                BarcodePrintToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_BarcodeP") = False Then
                    BarcodePrintToolStripMenuItem.Visible = False
                Else
                    BarcodePrintToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_Character") = False Then
                CharacteristicsToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_Character") = False Then
                    CharacteristicsToolStripMenuItem.Visible = False
                Else
                    CharacteristicsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ItemMaster") = False Then
                ItemToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ItemMaster") = False Then
                    ItemToolStripMenuItem.Visible = False
                Else
                    ItemToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If

            'If CheckAuthorisation(clsAdmin.UserCode, "GetItemUpdates") = False Then
            '    CallWebServicesToolStripMenuItem.Visible = False
            'Else
            '    If clsDefaultConfiguration.ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH.Contains(clsAdmin.TerminalID) Then
            '        CallWebServicesToolStripMenuItem.Visible = True
            '    End If

            'End If


            ''------------------------Under item tab
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ItemMNew") = False Then
                ItemMasterToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ItemMNew") = False Then
                    ItemMasterToolStripMenuItem.Visible = False
                Else
                    ItemMasterToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ItemHierar") = False Then
                ItemHiToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ItemHierar") = False Then
                    ItemHiToolStripMenuItem.Visible = False
                Else
                    ItemHiToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ItemExIm") = False Then
                ImportExportToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ItemExIm") = False Then
                    ImportExportToolStripMenuItem.Visible = False
                Else
                    ImportExportToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            ''=======
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ExImUpload") = False Then
                UploadItemToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ExImUpload") = False Then
                    UploadItemToolStripMenuItem.Visible = False
                Else
                    UploadItemToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ExImExXLS") = False Then
                ExportArticleXLSReportToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ExImExXLS") = False Then
                    ExportArticleXLSReportToolStripMenuItem.Visible = False
                Else
                    ExportArticleXLSReportToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ExImExHier") = False Then
                ExportArticleHierarchyToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ExImExHier") = False Then
                    ExportArticleHierarchyToolStripMenuItem.Visible = False
                Else
                    ExportArticleHierarchyToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            ''------------------------
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_MPromo") = False Then
                ManualPromotionToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_MPromo") = False Then
                    ManualPromotionToolStripMenuItem.Visible = False
                Else
                    ManualPromotionToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_SiteDetail") = False Then
                SiteDetailsToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_SiteDetail") = False Then
                    SiteDetailsToolStripMenuItem.Visible = False
                Else
                    SiteDetailsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_Supplier") = False Then
                SupplierToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_Supplier") = False Then
                    SupplierToolStripMenuItem.Visible = False
                Else
                    SupplierToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If

            'added by khusrao adil on 31-05-2018
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "StockInventory") = False Then
                InventoryToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "StockInventory") = False Then
                    InventoryToolStripMenuItem.Visible = False
                Else
                    InventoryToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_StockIn") = False Then
                StockInToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_StockIn") = False Then
                    StockInToolStripMenuItem.Visible = False
                Else
                    StockInToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If

            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_StockOut") = False Then
                StockOutToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_StockOut") = False Then
                    StockOutToolStripMenuItem.Visible = False
                Else
                    StockOutToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_Tax") = False Then
                TaxToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_Tax") = False Then
                    TaxToolStripMenuItem.Visible = False
                Else
                    TaxToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_Tender") = False Then
                TenderToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_Tender") = False Then
                    TenderToolStripMenuItem.Visible = False
                Else
                    TenderToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_User") = False Then
                CreateEditUserToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_User") = False Then
                    CreateEditUserToolStripMenuItem.Visible = False
                Else
                    CreateEditUserToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_ChangePasword") = False Then
                ChangePasswordToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_ChangePasword") = False Then
                    ChangePasswordToolStripMenuItem.Visible = False
                Else
                    ChangePasswordToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_PriceMapping") = False Then
                SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_PriceMapping") = False Then
                    SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Visible = False
                Else
                    SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            'If CheckAuthorisation(clsAdmin.UserCode, "GetItemUpdates") = False Then
            '    CallWebServicesToolStripMenuItem.Visible = False
            'Else
            '    If clsDefaultConfiguration.ALLOWED_CSV_TERMINALS_FOR_UPDATE_SYNCH.Contains(clsAdmin.TerminalID) Then
            '        CallWebServicesToolStripMenuItem.Visible = True
            '    End If

            'End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_PosTabCreate") = False Then
                PosTabCreationToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_PosTabCreate") = False Then
                    PosTabCreationToolStripMenuItem.Visible = False
                Else
                    PosTabCreationToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If

            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_DefineCombo") = False Then
                DefineComboToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_DefineCombo") = False Then 'vipin for new transaction '##
                    DefineComboToolStripMenuItem.Visible = False
                Else
                    DefineComboToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            'code added by vipul for define kit transaction
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_DefineKit") = False Then
                DefineKitToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_DefineKit") = False Then
                    DefineKitToolStripMenuItem.Visible = False
                Else
                    DefineKitToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_Table") = False Then
                TableManagmentToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_Table") = False Then
                    TableManagmentToolStripMenuItem.Visible = False
                Else
                    TableManagmentToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_PrepStation") = False Then
                PrepStationToolStripMenuItem2.Visible = False     'PrepStationToolStripMenuItem2
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_PrepStation") = False Then
                    PrepStationToolStripMenuItem2.Visible = False
                Else
                    PrepStationToolStripMenuItem2.Visible = True AndAlso Not isHOinstance
                End If
            End If
            'ashma - 15 may 2018 for ShiftManagement
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "L_SManagment") = False Then
                ShiftManagementToolStripMenuItem.Visible = False
            Else
                If CheckAuthorisation(clsAdmin.UserCode, "L_SManagment") = False Then
                    ShiftManagementToolStripMenuItem.Visible = False
                Else
                    ShiftManagementToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                End If
            End If
        End If
        ''-----------------------------------------------spectrum lite transaction end
        'code added for jk sprint 28

        'If CheckAuthorisation(clsAdmin.UserCode, "BackOfficeLink") = False Then
        '    '
        'Else
        '    cntBtn += 1
        'End If
        'If CheckAuthorisation(clsAdmin.UserCode, "Teamviewer") = False Then
        'Else
        '    cntBtn += 1
        'End If
        'If CheckAuthorisation(clsAdmin.UserCode, "Ammyy_Admin") = False Then
        'Else
        '    cntBtn += 1
        'End If
        '------------
        If CheckAuthorisation(clsAdmin.UserCode, "Billing Main") = False Then
            NewToolStripMenuItem.Visible = False
        Else
            NewToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SOMain") = False Then
            OpenToolStripMenuItem.Visible = False
        Else
            OpenToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "BLMain") = False Then
            BirthListToolStripMenuItem.Visible = False
        Else
            BirthListToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ReprintMain") = False Then
            ReprintMenuItem.Visible = False
        Else
            ReprintMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "POSSync") = False Then
            SyncReportMenuItem.Visible = False
        Else
            SyncReportMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "Database") = False Then
            DatabseConnection.Visible = False
        Else
            DatabseConnection.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "HWSetup") = False Then
            HardwareSetupToolStripMenuItem.Visible = False
        Else
            HardwareSetupToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If

        If CheckAuthorisation(clsAdmin.UserCode, "SLblBarcode") = False Then
            LabelPrintToolStripMenuItem.Visible = False
        Else
            LabelPrintToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "TillOpenNClose") = False Then
            TillOpen.Visible = False
            TillClose.Visible = False
        Else
            TillOpen.Visible = True AndAlso Not isHOinstance
            TillClose.Visible = True AndAlso Not isHOinstance
        End If
        If clsDefaultConfiguration.ShiftManagement = True Then
            TillOpen.Visible = False
            TillClose.Visible = False
            ShiftOpen.Visible = True
            ShiftClose.Visible = True
        Else
            ShiftOpen.Visible = False
            ShiftClose.Visible = False
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "Billing") = False Then
            CashMemo.Visible = False
            NewCashMemo.Visible = False
        Else
            CashMemo.Visible = True AndAlso Not isHOinstance
            NewCashMemo.Visible = True AndAlso Not isHOinstance
            cntBtn += 1
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "BirthListCreate") = False Then
            BirthList.Visible = False
            BirthListNew.Visible = False
        Else
            BirthList.Visible = True AndAlso Not isHOinstance
            BirthListNew.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SOCreation") = False Then
            SalesOrder.Visible = False
            SalesOrderCreation.Visible = False
        Else
            SalesOrder.Visible = True AndAlso Not isHOinstance
            SalesOrderCreation.Visible = True AndAlso Not isHOinstance
            cntBtn += 1
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "FOStockCheck") = False Then
            StockCheck.Visible = False
        Else
            StockCheck.Visible = True AndAlso Not isHOinstance
            cntBtn += 1
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "CashmemoSearch") = False Then
            SearchEditCashMemo.Visible = False
        Else
            SearchEditCashMemo.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "BirthListSales") = False Then
            BirthListSales.Visible = False
        Else
            BirthListSales.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "BirthListUpdate") = False Then
            BirthSearchEdit.Visible = False
        Else
            BirthSearchEdit.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SOUpdation") = False Then
            SalesOrderUpdation.Visible = False
        Else
            SalesOrderUpdation.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SOCancel") = False Then
            SalesOrderCancelation.Visible = False
        Else
            SalesOrderCancelation.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ReprintBL") = False Then
            BirthListMenuItem.Visible = False
        Else
            BirthListMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ReprintSO") = False Then
            SalesOrderMenuItem.Visible = False
        Else
            SalesOrderMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ReprintCM") = False Then
            CashMemoMenuItem.Visible = False
        Else
            CashMemoMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "RePrintReturnCM") = False Then
            ReturnCashMemoMenuItem.Visible = False
        Else
            ReturnCashMemoMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "FOOBD") = False Then
            OutboundDeliveryMenuItem.Visible = False
        Else
            OutboundDeliveryMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ChangeTender") = False Then
            ChangeTenderModeMenuItem.Visible = False
        Else
            ChangeTenderModeMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ChangeCheck") = False Then
            ChangeCheckDueDateToolStripMenuItem.Visible = False
        Else
            ChangeCheckDueDateToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "FastCashMemo") = False Then
            FastCashMemoToolStripMenuItem.Visible = False
        Else
            FastCashMemoToolStripMenuItem.Visible = True
            NewCashMemo.Visible = True
            cntBtn += 1
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "NewCustomer") = False Then
            NewCustomer.Visible = False
        Else
            NewCustomer.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SearchOCustomer") = False Then
            SearchEdit.Visible = False
        Else
            SearchEdit.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SearchCLP") = False Then
            SearchCLPCustomer.Visible = False
        Else
            SearchCLPCustomer.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "NewCLPCustomer") = False Then
            NewLoyaltyCustomer.Visible = False
        Else
            NewLoyaltyCustomer.Visible = True AndAlso Not isHOinstance
        End If

        If CheckAuthorisation(clsAdmin.UserCode, "CmOrderHistory") = False Then
            ViewOrderDetailsToolStripMenuItem.Visible = False
        Else
            ViewOrderDetailsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "PrintFormat") = False Then
            PrintingToolStripMenuItem.Visible = False
        Else
            PrintingToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SetTerminal") = False Then
            SetTerminalToolStripMenuItem.Visible = False
        Else
            SetTerminalToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "OP_SplitBill") = False Then 'vipin
            SplitBIllToolStripMenuItem.Visible = False
        Else
            SplitBIllToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        ' code commented by khusrao adil on 11-09-2017 for Jk sprint 29
        'If CheckAuthorisation(clsAdmin.UserCode, "Reports") = False Then
        '    ReportsToolStripMenuItem.Visible = False
        'Else
        '    If clsDefaultConfiguration.ClientForMail.ToUpper = "JK" Then
        '        ReportsToolStripMenuItem.Visible = False
        '    Else
        '        ReportsToolStripMenuItem.Visible = True
        '    End If
        'End If

        ' code commented by khusrao adil on 11-09-2017 for Jk sprint 29
        If CheckAuthorisation(clsAdmin.UserCode, "Reports") = False Then
            ReportsToolStripMenuItem.Visible = False
            ReportsToolsStripOuterMenuItem.Visible = False
        Else
            'ReportsToolStripMenuItem.Visible = True
            ReportsToolStripMenuItem.Visible = False
            If clsDefaultConfiguration.ReportTerminalId.Contains(clsAdmin.TerminalID) Then
                'added by khusrao adil on 08-09-2017 for jk sprint 29
                ' ReportsToolsStripOuterMenuItem.Visible = True
                ReportsToolsStripOuterMenuItem.DropDownItems.Clear()
                If clsDefaultConfiguration.ThemeSelect = "Themee 1" Then
                    ReportsToolsStripOuterMenuItem.Margin = New Padding(0, 3, 0, 3)
                End If
                'ReportsToolsStripOuterMenuItem.Margin = New Padding(0, 0, 0, 3)
                Dim objReportBase As New ReportBase
                Dim reportsDt As DataTable = objReportBase.GetAllValidReports(clsAdmin.SiteCode)
                If CheckAuthorisation(clsAdmin.UserCode, "DirectReport") = True Then
                    Dim drR As DataRow = reportsDt.NewRow
                    drR("SiteCode") = clsAdmin.SiteCode
                    drR("ReportName") = "Direct Report"
                    drR("CREATEDAT") = clsAdmin.SiteCode
                    drR("CREATEDBY") = clsAdmin.UserName
                    drR("CREATEDON") = clsAdmin.CurrentDate
                    drR("UPDATEDAT") = clsAdmin.SiteCode
                    drR("UPDATEDBY") = clsAdmin.UserName
                    drR("UPDATEDON") = clsAdmin.CurrentDate
                    drR("STATUS") = True
                    reportsDt.Rows.Add(drR)
                End If
                If reportsDt.Rows.Count > 0 Then
                    For Each dr As DataRow In reportsDt.Rows
                        Dim ReportName As String = dr("ReportName")
                        Dim ReportMenuItem As New ToolStripMenuItem(ReportName)
                        AddHandler ReportMenuItem.Click, AddressOf ReportMenuItemClicked
                        ReportsToolsStripOuterMenuItem.DropDownItems.Add(ReportMenuItem)
                        ReportMenuItem.Name = ReportName
                        ReportMenuItem.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
                        ReportMenuItem.TextAlign = ContentAlignment.MiddleCenter
                        ReportMenuItem.TextDirection = ToolStripTextDirection.Horizontal
                        ReportMenuItem.Font = New Font("Verdana", 9.75, FontStyle.Regular)
                        ReportMenuItem.ImageAlign = ContentAlignment.MiddleCenter
                        ReportMenuItem.Margin = New Padding(0, 3, 0, 3)
                        ReportMenuItem.Image = Nothing 'Global.Spectrum.My.Resources.payment_Normal
                        ReportMenuItem.Visible = True
                        ReportMenuItem.Enabled = True

                    Next
                    ReportsToolsStripOuterMenuItem.Visible = True
                Else
                    ReportsToolsStripOuterMenuItem.Visible = False
                End If
            Else
                ReportsToolsStripOuterMenuItem.Visible = False
            End If
        End If

        If CheckAuthorisation(clsAdmin.UserCode, "DataArchive") = False Then
            DataArchiveToolStripMenuItem.Visible = False
        Else
            DataArchiveToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "DEF.CONF") = False Then
            DefaultConfigMenu.Visible = False
        Else
            DefaultConfigMenu.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SHW_CSAdj") = False Then
            CreditSaleAdjToolStripMenuItem.Visible = False
        Else
            CreditSaleAdjToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            cntBtn += 1
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SHW_Quotation") = False Then
            QuotationToolStripMenuItem.Visible = False
        Else
            QuotationToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SHW_PETTYCASH") = False Then
            PettyCash.Visible = False
        ElseIf clsDefaultConfiguration.PettyCashTerminalId <> clsAdmin.TerminalID Then
            PettyCash.Visible = False
        Else
            PettyCash.Visible = True AndAlso Not isHOinstance
        End If

        If CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN") = False Then
            DayOpenMenuItem.Visible = False
        Else
            DayOpenMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "DAY_CLS_FO") = False Then
            DayCloseToolStripMenuItem.Visible = False
        Else
            DayCloseToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "Main_Grievance") = False Then
            'GrievanceToolStripMenuItem.Visible = False
            TicketingSystemToolStripMenuItem.Visible = False
        Else
            'GrievanceToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            TicketingSystemToolStripMenuItem.Visible = True
        End If
        'added by khusrao adil on 13-04-2014 on sprint 35
        If CheckAuthorisation(clsAdmin.UserCode, "Training_Videos") = False Then
            TrainingVideosToolStripMenuItem.Visible = False
        Else
            TrainingVideosToolStripMenuItem.Visible = True
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "Record_Product") = False Then
            RecordProductionToolStripMenuItem.Visible = False
        Else
            RecordProductionToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "KDS") = False Then
            KDSToolStripMenuItem.Visible = False
        Else
            KDSToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "PrepStation") = False Then
            SetPrepStationToolStripMenuItem.Visible = False
        Else
            SetPrepStationToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "Expeditor") = False Then
            ExpidiorToolStripMenuItem.Visible = False
        Else
            ExpidiorToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        '----05042016 check user authorization for so booking new and edit -sagar
        'If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBooking") = True AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBooking") = True AndAlso CheckAuthorisation(clsAdmin.UserCode, "SoBooking") AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
            SalesOrderBookingToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        Else
            SalesOrderBookingToolStripMenuItem.Visible = False
        End If
        'If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBookingEdit") = True AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "SoBookingEdit") = True AndAlso CheckAuthorisation(clsAdmin.UserCode, "SoBookingEdit") AndAlso clsDefaultConfiguration.ISAllowSOBooking = True AndAlso clsDefaultConfiguration.IsNewSalesOrder = True Then
            SaleOrderEditBookingToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        Else
            SaleOrderEditBookingToolStripMenuItem.Visible = False
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "KEYBOARD") = False Then
            KeyBoardToolStripMenuItem.Visible = False
        Else
            KeyBoardToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "SCHEDULAR") = False Then
            SchedulerToolStripMenuItem.Visible = False
        Else
            SchedulerToolStripMenuItem.Visible = True AndAlso Not isHOinstance
        End If
        '-----------------------------------------------------------------------------
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "StockTake") = False Then
            StockTakeToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "StockTake") = False Then
                StockTakeToolStripMenuItem.Visible = False
            Else
                StockTakeToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "ManualSync") = False Then
            ExecuteSyncToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "ManualSync") = False Then
                ExecuteSyncToolStripMenuItem.Visible = False
            Else
                ExecuteSyncToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "C_Form") = False Then
            CformsToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "C_Form") = False Then
                CformsToolStripMenuItem.Visible = False
            Else
                CformsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "CFormNew") = False Then
            PendingCformsToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "CFormNew") = False Then
                PendingCformsToolStripMenuItem.Visible = False
            Else
                PendingCformsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "CFormEdit") = False Then
            EditCformsToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "CFormEdit") = False Then
                EditCformsToolStripMenuItem.Visible = False
            Else
                EditCformsToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "Membership") = False Then
            MembershipToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "Membership") = False Then
                MembershipToolStripMenuItem.Visible = False
            Else
                MembershipToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "AutoDataBackUp") = False Then
            AutoDataBackUpToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "AutoDataBackUp") = False Then
                AutoDataBackUpToolStripMenuItem.Visible = False
            Else
                AutoDataBackUpToolStripMenuItem.Visible = True AndAlso Not isHOinstance
            End If
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "DineIn") = False Then
            DineInToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "DineIn") = False Then
                DineInToolStripMenuItem.Visible = False
            Else
                DineInToolStripMenuItem.Visible = True AndAlso Not isHOinstance
                cntBtn += 1
            End If
        End If
        ' added by ashma on 27 dec 2016
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "DispchNDelivery") = False Then
            HomeDeliveryToolStripMenuItem.Visible = False
        Else
            HomeDeliveryToolStripMenuItem.Visible = True
        End If
        'added by khusrao adil on 21-03-2017
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "HostReservation") = False Then
            HotelReservationToolStripMenuItem.Visible = False
        Else
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                HotelReservationToolStripMenuItem.Visible = True
            Else
                HotelReservationToolStripMenuItem.Visible = False
            End If
        End If



        'code added by vipul for membership
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "Enquiry_Dtl") = False Then
            ViewEnquiryToolStripMenuItem.Visible = False
        Else
            ViewEnquiryToolStripMenuItem.Visible = True
        End If

        If CheckAuthorisationForTran(clsAdmin.SiteCode, "View_MemberShip") = False Then
            ViewMemberShipToolStripMenuItem.Visible = False
        Else
            ViewMemberShipToolStripMenuItem.Visible = True
        End If
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "OP_Screen") = False Then
            OrderPackagingScreenToolStripMenuItem.Visible = False
        Else
            If CheckAuthorisation(clsAdmin.UserCode, "OP_Screen") = False Then
                OrderPackagingScreenToolStripMenuItem.Visible = False
            End If
        End If

        If clsDefaultConfiguration.IsTablet Then
            DashboardMenuButtons(cntBtn, isHOinstance)
            'commented by khusrao adil on 25-05-2018 for dynaimic display and dashboard button design
            'Dim bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9 As New Button
            'Dim btSize As Integer = 135
            'Dim btyLocation As Integer = 326
            'Dim btxLocation As Integer = 0
            'If cntBtn = 6 Or cntBtn = 7 Then
            '    btxLocation = 15
            'ElseIf cntBtn = 5 Then
            '    btxLocation = 99
            'ElseIf cntBtn = 4 Then
            '    btxLocation = 176
            'ElseIf cntBtn = 3 Then
            '    btxLocation = 260
            'ElseIf cntBtn = 2 Then
            '    btxLocation = 384
            'End If
            'If CheckAuthorisation(clsAdmin.UserCode, "Billing Main") = False Then
            '    bt1.Visible = False
            'Else
            '    bt1.Visible = True AndAlso Not isHOinstance
            '    bt1.Location = New Point(btxLocation, btyLocation)
            '    bt1.Size = New Size(btSize, btSize)
            '    bt1.Image = My.Resources.TabLPCashMemo
            '    bt1.FlatAppearance.BorderSize = 0
            '    AddHandler bt1.Click, AddressOf CashMemo_Click
            '    GrpHome.Controls.Add(bt1)
            'End If
            'If CheckAuthorisation(clsAdmin.UserCode, "FastCashMemo") = False Then
            '    bt2.Visible = False
            'Else
            '    bt2.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt2.Location = New Point(btxLocation, btyLocation)
            '    bt2.Size = New Size(btSize, btSize)
            '    bt2.Image = My.Resources.TabLPTouchCashMemo
            '    bt2.FlatAppearance.BorderSize = 0
            '    AddHandler bt2.Click, AddressOf FastCashMemoToolStripMenuItem_Click
            '    GrpHome.Controls.Add(bt2)
            'End If
            'If CheckAuthorisationForTran(clsAdmin.SiteCode, "DineIn") = False Then
            '    bt3.Visible = False
            'Else
            '    If CheckAuthorisation(clsAdmin.UserCode, "DineIn") = False Then
            '        bt3.Visible = False
            '    Else
            '        bt3.Visible = True AndAlso Not isHOinstance
            '        btxLocation = btxLocation + 168
            '        bt3.Location = New Point(btxLocation, btyLocation)
            '        bt3.Size = New Size(btSize, btSize)
            '        bt3.Image = My.Resources.TabLPDineIn
            '        bt3.FlatAppearance.BorderSize = 0
            '        AddHandler bt3.Click, AddressOf DineInToolStripMenuItem_Click
            '        GrpHome.Controls.Add(bt3)
            '    End If
            'End If
            'If CheckAuthorisation(clsAdmin.UserCode, "SOMain") = False Then
            '    bt4.Visible = False
            'Else
            '    bt4.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt4.Location = New Point(btxLocation, btyLocation)
            '    bt4.Size = New Size(btSize, btSize)
            '    bt4.Image = My.Resources.TabLPSalesOrder
            '    bt4.FlatAppearance.BorderSize = 0
            '    AddHandler bt4.Click, AddressOf SalesOrderCreation_Click
            '    GrpHome.Controls.Add(bt4)
            'End If

            'If CheckAuthorisation(clsAdmin.UserCode, "SHW_CSAdj") = False Then
            '    bt5.Visible = False
            'Else
            '    bt5.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt5.Location = New Point(btxLocation, btyLocation)
            '    bt5.Size = New Size(btSize, btSize)
            '    bt5.Image = My.Resources.TabLPCreditSalesAdjustment
            '    bt5.FlatAppearance.BorderSize = 0
            '    AddHandler bt5.Click, AddressOf CreditSaleAdjToolStripMenuItem_Click
            '    GrpHome.Controls.Add(bt5)
            'End If
            'If CheckAuthorisation(clsAdmin.UserCode, "FOStockCheck") = False Then
            '    bt6.Visible = False
            'Else
            '    bt6.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt6.Location = New Point(btxLocation, 326)
            '    bt6.Size = New Size(btSize, btSize)
            '    bt6.Image = My.Resources.TabLPStockCheck
            '    bt6.FlatAppearance.BorderSize = 0
            '    AddHandler bt6.Click, AddressOf StockCheck_Click
            '    GrpHome.Controls.Add(bt6)
            'End If
            ''code added for jk sprint 28
            'If CheckAuthorisation(clsAdmin.UserCode, "Ammyy_Admin") = False Then
            '    bt7.Visible = False
            'Else
            '    bt7.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt7.Location = New Point(btxLocation, 326)
            '    bt7.Size = New Size(btSize, btSize)

            '    bt7.Image = My.Resources.ammyy_admin
            '    bt7.FlatAppearance.BorderSize = 0
            '    AddHandler bt7.Click, AddressOf AmmyAdmin_Click
            '    bt7.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            '    ' bt7.Text = "Ammyy Admin"
            '    bt7.BackColor = Color.WhiteSmoke
            '    GrpHome.Controls.Add(bt7)
            'End If


            'If CheckAuthorisation(clsAdmin.UserCode, "Teamviewer") = False Then
            '    bt8.Visible = False
            'Else
            '    bt8.Visible = True AndAlso Not isHOinstance
            '    btxLocation = btxLocation + 168
            '    bt8.Location = New Point(btxLocation, 326)
            '    bt8.Size = New Size(btSize, btSize)
            '    bt8.Image = My.Resources.teamviewer
            '    bt8.FlatAppearance.BorderSize = 0
            '    AddHandler bt8.Click, AddressOf TeamViewer_Click
            '    bt8.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            '    bt8.BackColor = Color.WhiteSmoke
            '    '  bt8.Text = "Teamviewer"
            '    GrpHome.Controls.Add(bt8)
            'End If
            'If CheckAuthorisation(clsAdmin.UserCode, "BackOfficeLink") = False Then
            '    bt9.Visible = False
            'Else
            '    bt9.Visible = True AndAlso Not isHOinstance
            '    '  btxLocation = btxLocation + 168
            '    ' bt9.Location = New Point(btxLocation, 326)
            '    If cntBtn > 8 Then
            '        btxLocation = 0
            '        bt9.Location = New Point(0, 500)
            '    Else
            '        btxLocation = btxLocation + 168
            '        bt9.Location = New Point(btxLocation, 326)
            '    End If

            '    bt9.Size = New Size(btSize, btSize)
            '    bt9.Image = My.Resources.backoffice
            '    bt9.FlatAppearance.BorderSize = 0
            '    AddHandler bt9.Click, AddressOf BackOffice_Link
            '    bt9.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            '    bt9.BackColor = Color.WhiteSmoke
            '    ' bt9.Text = "Back Office Link"
            '    GrpHome.Controls.Add(bt9)
            'End If
            '-------------------dynamic design comments end
            ''added by khusrao adil on 08-09-2017 for jk sprint 29
            'If CheckAuthorisation(clsAdmin.UserCode, "Reports") = False Then
            '    ReportsToolsStripOuterMenuItem.Visible = False
            'Else
            '    ReportsToolsStripOuterMenuItem.Visible = True
            '    ReportsToolsStripOuterMenuItem.DropDownItems.Clear()
            '    'ReportsToolsStripOuterMenuItem.Margin = New Padding(0, 3, 0, 3)
            '    'code added by irfan on 28/9/2017 for report menu adjustable 
            '    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '        ReportsToolsStripOuterMenuItem.Margin = New Padding(0, 3, 0, 3)
            '    End If
            '    Dim objReportBase As New ReportBase
            '    Dim reportsDt As DataTable = objReportBase.GetAllValidReports(clsAdmin.SiteCode)
            '    If CheckAuthorisation(clsAdmin.UserCode, "DirectReport") = True Then
            '        Dim drR As DataRow = reportsDt.NewRow
            '        drR("SiteCode") = clsAdmin.SiteCode
            '        drR("ReportName") = "Direct Report"
            '        drR("CREATEDAT") = clsAdmin.SiteCode
            '        drR("CREATEDBY") = clsAdmin.UserName
            '        drR("CREATEDON") = clsAdmin.CurrentDate
            '        drR("UPDATEDAT") = clsAdmin.SiteCode
            '        drR("UPDATEDBY") = clsAdmin.UserName
            '        drR("UPDATEDON") = clsAdmin.CurrentDate
            '        drR("STATUS") = True
            '        reportsDt.Rows.Add(drR)
            '    End If
            '    For Each dr As DataRow In reportsDt.Rows
            '        Dim ReportName As String = dr("ReportName")
            '        Dim ReportMenuItem As New ToolStripMenuItem(ReportName)
            '        AddHandler ReportMenuItem.Click, AddressOf ReportMenuItemClicked
            '        ReportsToolsStripOuterMenuItem.DropDownItems.Add(ReportMenuItem)
            '        ReportMenuItem.Name = ReportName
            '        ReportMenuItem.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
            '        ReportMenuItem.TextAlign = ContentAlignment.MiddleCenter
            '        ReportMenuItem.TextDirection = ToolStripTextDirection.Horizontal
            '        ReportMenuItem.Font = New Font("Verdana", 9.75, FontStyle.Regular)
            '        ReportMenuItem.ImageAlign = ContentAlignment.MiddleCenter
            '        ReportMenuItem.Margin = New Padding(0, 3, 0, 3)
            '        ReportMenuItem.Image = Nothing 'Global.Spectrum.My.Resources.payment_Normal
            '    Next
            'End If
        End If
    End Sub
    'added by khusrao adil on 25-05-2018 for dashboard button desgin
    Dim tblMenu As New TableLayoutPanel
    Dim buttonSlidTimer As New Timer
    Public Sub DashboardMenuButtons(ByVal cntBtn As Integer, ByVal isHOinstance As Boolean)
        Dim dtDashboardMenu As New DataTable
        Dim drMenu As DataRow
        dtDashboardMenu.Columns.Add("TransactionCode", GetType(String))
        dtDashboardMenu.Columns.Add("Visible", GetType(Boolean))
        dtDashboardMenu.Columns.Add("rowNumber", GetType(Integer))
        dtDashboardMenu.Columns.Add("ColNumber", GetType(Integer))
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="Billing Main")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="FastCashMemo")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="DineIn")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="SOMain")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="SHW_CSAdj")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="FOStockCheck")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="Ammyy_Admin")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="Teamviewer")
        AddRowToDataTable(dtDashboardMenu, TransactionCode:="BackOfficeLink")
        Dim rowNumber As Integer = 0
        Dim colNumber As Integer = 0
        If GrpHome.Contains(tblMenu) Then
            '  GrpHome.Controls.Remove(tblMenu)
            tblMenu.Controls.Clear()
            tblMenu.ColumnCount = 0
            tblMenu.RowCount = 0
        End If
     
        ' tblMenu = New TableLayoutPanel
        tblMenu.BackColor = Color.Transparent
        ' tblMenu.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        Dim myScreen As System.Windows.Forms.Screen
        myScreen = Screen.FromControl(Me)
        tblMenu.Size = New Size(GrpHome.Width, 280)
        Dim tblColCount As Integer = 10
        tblMenu.ColumnCount = tblColCount
        Dim tblColSizePercent As Integer = 10
        If GrpHome.Width = 1024 Then
            tblMenu.Size = New Size(GrpHome.Width, 280)
            tblColCount = 6
            tblMenu.ColumnCount = tblColCount + 2
        End If
        If dtDashboardMenu.Rows.Count > 6 Then
            tblMenu.RowCount = 2
        Else
            tblMenu.RowCount = 1
        End If
        For Each drM As DataRow In dtDashboardMenu.Rows
            If CheckAuthorisation(clsAdmin.UserCode, drM("TransactionCode")) = False Then
            Else
                drM("Visible") = True
                If colNumber >= tblColCount Then
                    rowNumber = rowNumber + 1
                    colNumber = 1
                Else
                    colNumber = colNumber + 1
                End If
                drM("rowNumber") = rowNumber
                drM("ColNumber") = colNumber
                dtDashboardMenu.AcceptChanges()
            End If
        Next
        For index = 1 To tblMenu.ColumnCount
            tblMenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, tblColSizePercent))
        Next
        For index = 1 To tblMenu.RowCount
            tblMenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10))
        Next
        'tblMenu.ColumnCount = tblColCount
        'For Each drM As DataRow In dtDashboardMenu.Rows
        '    If CheckAuthorisation(clsAdmin.UserCode, drM("TransactionCode")) = False Then
        '    Else
        '        drM("Visible") = True
        '        drM("rowNumber") = rowNumber
        '        drM("ColNumber") = colNumber + 1
        '        If tblColCount = 10 Then
        '            If colNumber >= 7 Then
        '                rowNumber = rowNumber + 1
        '                colNumber = 0
        '            Else
        '                colNumber = colNumber + 1
        '            End If
        '        Else
        '            If colNumber >= 5 Then
        '                rowNumber = rowNumber + 1
        '                colNumber = 0
        '            Else
        '                colNumber = colNumber + 1
        '            End If
        '        End If
        '    End If
        'Next
        'For index = 1 To tblColCount
        '    If tblColCount = 6 And index = 1 Then
        '        tblMenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7))
        '    End If
        '    tblMenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, tblColSizePercent))
        '    tblMenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10))
        '    If tblColCount = 6 And index = 1 Then
        '        tblMenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7))
        '    End If
        'Next
        Dim BtnDashboard As CtrlDashboardBtn
        For index = 0 To dtDashboardMenu.Rows.Count - 1
            If dtDashboardMenu.Rows(index)("Visible") = True Then
                BtnDashboard = New CtrlDashboardBtn
                'If tblColCount = 10 Then
                '    BtnDashboard.RectangleShape1.Size = New Size(125, 125)
                'Else
                '    BtnDashboard.RectangleShape1.Size = New Size(130, 130)
                '    BtnDashboard.Margin = New Padding(0, 0, 2, 0)
                'End If
                BtnDashboard.RectangleShape1.Size = New Size(120, 120)
                BtnDashboard.Margin = New Padding(0, 0, 2, 0)
                BtnDashboard.Dock = DockStyle.Fill
                BtnDashboard.RectangleShape1.BackStyle = PowerPacks.BackStyle.Opaque
                BtnDashboard.RectangleShape1.BackColor = Color.White
                BtnDashboard.RectangleShape1.CornerRadius = 15
                BtnDashboard.RectangleShape1.BorderStyle = DashStyle.Solid
                BtnDashboard.RectangleShape1.BorderColor = Color.White
                BtnDashboard.picbtnPicture.Visible = False
                BtnDashboard.lblTrsactionName.Visible = False
                BtnDashboard.lblTrsactionName.Text = dtDashboardMenu.Rows(index)("TransactionCode").ToString()
                SetButtonImageAndEvent(dtDashboardMenu.Rows(index)("TransactionCode").ToString(), BtnDashboard)
                tblMenu.Controls.Add(BtnDashboard, dtDashboardMenu.Rows(index)("ColNumber"), dtDashboardMenu.Rows(index)("rowNumber"))
                BtnDashboard.Dock = DockStyle.Fill
            End If
        Next
        GrpHome.Controls.Add(tblMenu)
        tblMenu.Location = New Point(0, 320)
    End Sub
    'added by khusrao adil on 25-05-2018 for dashboard button desgin
    Public Sub AddRowToDataTable(ByRef dt As DataTable, ByVal TransactionCode As String)
        Dim drMenu As DataRow
        drMenu = dt.NewRow
        drMenu("TransactionCode") = TransactionCode
        drMenu("Visible") = False
        drMenu("rowNumber") = 0
        drMenu("ColNumber") = 0
        dt.Rows.Add(drMenu)
    End Sub
    'added by khusrao adil on 25-05-2018 for dashboard button desgin
    Public Function RoundTheButton(ByRef Button As CtrlBtn) As CtrlBtn
        Dim p As New Drawing2D.GraphicsPath
        p.FillMode = FillMode.Winding
        p.StartFigure()
        Dim Curvalue = 30
        p.AddArc(New Rectangle(0, 0, Curvalue, Curvalue), 180, 90)
        p.AddLine(Curvalue, 0, Button.Width - Curvalue, 0)
        p.AddArc(New Rectangle(Button.Width - Curvalue, 0, Curvalue, Curvalue), -90, 90)
        p.AddLine(Button.Width, Curvalue, Button.Width, Button.Height - Curvalue)
        p.AddArc(New Rectangle(Button.Width - Curvalue, Button.Height - Curvalue, Curvalue, Curvalue), 0, 90)
        p.AddLine(Button.Width - Curvalue, Button.Height, Curvalue, Button.Height)
        p.AddArc(New Rectangle(0, Button.Height - Curvalue, Curvalue, Curvalue), 90, 90)

        p.CloseFigure()
        Button.Region = New Region(p)
    End Function
    'added by khusrao adil on 25-05-2018 for dashboard button desgin
    Public Function SetButtonImageAndEvent(ByVal TransactionCode As String, ByRef Button As CtrlDashboardBtn)

        Button.RectangleShape1.BackgroundImageLayout = ImageLayout.Stretch
        Select Case TransactionCode
            Case "Billing Main"
                AddHandler Button.RectangleShape1.Click, AddressOf CashMemo_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPCashMemo
            Case "FastCashMemo"
                AddHandler Button.RectangleShape1.Click, AddressOf FastCashMemoToolStripMenuItem_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPTouchCashMemo
            Case "DineIn"
                AddHandler Button.RectangleShape1.Click, AddressOf DineInToolStripMenuItem_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPDineIn
            Case "SOMain"
                AddHandler Button.RectangleShape1.Click, AddressOf SalesOrderCreation_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPSalesOrder
            Case "SHW_CSAdj"
                AddHandler Button.RectangleShape1.Click, AddressOf CreditSaleAdjToolStripMenuItem_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPCreditSalesAdjustment
            Case "FOStockCheck"
                AddHandler Button.RectangleShape1.Click, AddressOf StockCheck_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.TabLPStockCheck
            Case "Ammyy_Admin"
                AddHandler Button.RectangleShape1.Click, AddressOf AmmyAdmin_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.ammyy_admin
            Case "Teamviewer"
                AddHandler Button.RectangleShape1.Click, AddressOf TeamViewer_Click
                Button.RectangleShape1.BackgroundImage = My.Resources.teamviewer
            Case "BackOfficeLink"
                AddHandler Button.RectangleShape1.Click, AddressOf BackOffice_Link
                Button.RectangleShape1.BackgroundImage = My.Resources.backoffice
            Case Else
                Return Nothing
        End Select
        'Billing Main ,FastCashMemo,DineIn ,SOMain, SHW_CSAdj ,FOStockCheck ,Ammyy_Admin ,Teamviewer ,BackOfficeLink  

    End Function
    'added by khusrao adil on 15-09-2017 for jk sprint 29
    Private Sub ReportMenuItemClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim DropDownName As String = DirectCast(sender, ToolStripItem).Name
        Try
            If DropDownName <> "Direct Report" Then
                Dim path As String = ""
                Dim _paraMonth As String
                Dim _paraYear As String
                Dim _PromotionIds As String
                Dim SelectedClass As String = ""
                Dim SelectedTerminals As String = ""
                Me.Cursor = Cursors.WaitCursor
                Dim obj As New frmReportFilter
                Dim obNetSale As New frmNHierarchyWiseNetSales
                obj.dtToDate.Value = DateTime.Now
                obj.dtFromDate.Value = DateTime.Now
                obj.dtExpiryDate.Value = DateTime.Now
                Dim objDefault As New clsDefaultConfiguration("DC")
                objDefault.GetDefaultSettings()
                Dim Client As String = clsDefaultConfiguration.ClientForMail
                If DropDownName = POSReports.KOTReport.ToString() OrElse DropDownName = POSReports.BillWiseReport.ToString() OrElse _
                    DropDownName = POSReports.CashierWiseSalesReport.ToString() OrElse DropDownName = POSReports.TaxDetailsReport.ToString() _
                    OrElse DropDownName = POSReports.PostingReport.ToString() OrElse DropDownName = POSReports.ProductMixReport.ToString() _
                    OrElse DropDownName = POSReports.FoodCostReport.ToString() Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                ElseIf DropDownName = POSReports.GiftVoucherReport.ToString() Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.lblExpryDate.Visible = True
                    obj.dtExpiryDate.Visible = True
                ElseIf DropDownName = POSReports.TimeScheduledSaleReport.ToString() Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.lblTimeSpan.Visible = True
                    obj.txtTimeSpan.Visible = True
                ElseIf DropDownName = POSReports.HcCustomerDetailsReport.ToString() Then
                    obj.pnlCustomerClass.Visible = True
                    obj.lblSelectClass.Visible = True
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.pnlPromotions.Visible = False
                ElseIf DropDownName = POSReports.SalesPersonwiseSalesReport.ToString() Then
                    obj.pnlCustomerClass.Visible = True
                    obj.lblSelectClass.Visible = True
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                ElseIf DropDownName = POSReports.XReadCategorywiseReport.ToString() Then
                    obj.dtFromDate.Visible = False
                    obj.lblFromDate.Visible = False
                    obj.lblTimeSpan.Visible = False
                    obj.txtTimeSpan.Visible = False
                ElseIf DropDownName = POSReports.ConversionReport.ToString() Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.lblTimeSpan.Visible = False
                    obj.txtTimeSpan.Visible = False
                    obj.dtToDate.Visible = True
                    obj.lblToDate.Visible = True
                ElseIf DropDownName = POSReports.TallySalesReport.ToString() Then 'vipin
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.dtToDate.Visible = True
                    obj.lblToDate.Visible = True
                    obj.lblTimeSpan.Visible = False
                    obj.txtTimeSpan.Visible = False
                ElseIf DropDownName = POSReports.HierarchyWiseSalesReport.ToString() Then  '' added by vipin
                    obNetSale.IsNetSale = False
                    obNetSale.ShowDialog()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                ElseIf DropDownName = POSReports.HierarchyWiseNetSalesDetailsReport.ToString() Then  '' added by vipin
                    obNetSale.IsNetSale = True
                    obNetSale.ShowDialog()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                ElseIf DropDownName = "X-Read" OrElse DropDownName = "XReadFlavourwiseReport" OrElse DropDownName = "XReadCatReport" OrElse DropDownName = "WriteOffDetailsReport" Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                ElseIf DropDownName = "CustomerWiseSalesReport" Then
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                ElseIf DropDownName = "SalesAndTranctionReport" Then 'Jayesh
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                ElseIf DropDownName = POSReports.ImprestCashAmountDetails.ToString() Then  '' added by vipin
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.dtToDate.Visible = True
                    obj.lblToDate.Visible = True
                    obj.lblTimeSpan.Visible = False
                    obj.txtTimeSpan.Visible = False
                ElseIf DropDownName = POSReports.OrderTypeWiseSalesReport.ToString() Then  '' added by vipin 06082018
                    obj.dtFromDate.Visible = True
                    obj.lblFromDate.Visible = True
                    obj.dtToDate.Visible = True
                    obj.lblToDate.Visible = True
                    obj.lblTimeSpan.Visible = False
                    obj.txtTimeSpan.Visible = False
                End If
                obj.selectedReport = DropDownName
                obj.CallFromDirectMenu = True
                Me.DialogResult = obj.ShowDialog()
                If Me.DialogResult = Windows.Forms.DialogResult.OK Then
                    _paraMonth = obj.selectedmonth
                    _paraYear = obj.selectedyear
                    _PromotionIds = obj.SelectedPromotions
                    SelectedClass = obj.SelectedClassify
                    SelectedTerminals = obj.SelectedTerminals  'added by khusrao adil on 29-11-2017  for jk sprint 29
                    selectedPartner = obj.SelectedPartner
                    SelectedTender = obj.SelectedTender
                    CallReport(ReportName:=DropDownName, obj:=obj, _paraMonth:=_paraMonth, _paraYear:=_paraYear, _PromotionIds:=_PromotionIds, SelectedClass:=SelectedClass, SelectedTerminals:=SelectedTerminals, SelectedPartner:=selectedPartner, SelectedTender:=SelectedTender)
                    'CallReport(ReportName:=DropDownName, obj:=obj, _paraMonth:=_paraMonth, _paraYear:=_paraYear, _PromotionIds:=_PromotionIds, SelectedClass:=SelectedClass)
                End If
                Me.Cursor = Cursors.Default
            Else
                Dim ChildForm As New Spectrum.frmReportList
                Try
                    If ChildForm.Name <> String.Empty Then
                        ShowChildForm(ChildForm, False)
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'added by khusrao adil on 15-09-2017 for jk sprint 29
    Public Sub CallReport(ByVal ReportName As String, ByRef obj As frmReportFilter, Optional _paraMonth As String = "", Optional _paraYear As String = "", Optional _PromotionIds As String = "", Optional SelectedClass As String = "", Optional SelectedTerminals As String = "", Optional SelectedPartner As String = "", Optional SelectedTender As String = "")
        'Public Sub CallReport(ByVal ReportName As String, ByRef obj As frmReportFilter, Optional _paraMonth As String = "", Optional _paraYear As String = "", Optional _PromotionIds As String = "", Optional SelectedClass As String = "")
        Try
            If Not IsDBNull(obj.dtToDate.Value) AndAlso obj.dtToDate.Value IsNot Nothing AndAlso Not IsDBNull(obj.dtFromDate.Value) AndAlso obj.dtFromDate.Value IsNot Nothing Then
                Dim Client As String = clsDefaultConfiguration.ClientForMail
                Dim adsrReportFileName As String = String.Empty
                Dim objReportBase As New ReportBase
                Dim adsrProcName As String = objReportBase.GetAdsrProcedureName(clsAdmin.SiteCode, adsrReportFileName)

                Dim objReport As IReports = ReportFactory.Instance.GetReportInstance(ReportName)
                Dim request As New DayCloseReportModel
                request.ToDate = DirectCast(obj.dtToDate.Value, Date).Date
                request.FromDate = DirectCast(obj.dtFromDate.Value, Date).Date
                request.ExpiryDate = DirectCast(obj.dtExpiryDate.Value, Date).Date
                request.TimeSpan = IIf(obj.txtTimeSpan.Text = "" OrElse obj.txtTimeSpan.Text = String.Empty, 1, obj.txtTimeSpan.Text)
                request.SiteCode = clsAdmin.SiteCode
                request.CreatedBy = clsAdmin.UserName
                request.CreatedOn = DateTime.Now

                request.AdsrReportProcedureName = adsrProcName
                request.AdsrReportFileName = adsrReportFileName

                Dim objClient As New clsCommon
                Dim clientname As String = objClient.GetFLdValue(clsAdmin.SiteCode)
                Dim objFrmPosReport As New frmPosReports
                If ReportName = "DayCloseReport" Then
                    If clientname = "JK" Then
                        objFrmPosReport.GenerateDSRReport(request)
                    ElseIf clientname = "PC" Then
                        objFrmPosReport.GenerateDayCloseReport(request)
                    Else
                        objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath)
                    End If
                ElseIf Client = "JK" And (ReportName = "KOTReport" Or ReportName = "ProductMixReport") Then
                    Dim objKOT As New kotprint
                    objKOT.GenerateKOT(request, clsDefaultConfiguration.DayCloseReportPath, dtPrinterInfo, ReportName)
                ElseIf ReportName = "X-Read" Then
                    objFrmPosReport.genrateXread(request)
                ElseIf ReportName = "XReadCategorywiseReport" Then
                    objFrmPosReport.genrateXreadCategoryWise(request)
                ElseIf ReportName = "XReadFlavourwiseReport" Then
                    objFrmPosReport.genrateXreadFlavourWise(request)
                ElseIf ReportName = "XReadCatReport" Then
                    objFrmPosReport.genrateXreadcat(request)
                ElseIf ReportName = "WriteOffDetailsReport" Then
                    objFrmPosReport.WriteOffDetailsReport(request)
                ElseIf ReportName = "CustomerWiseSalesReport" Then
                    objFrmPosReport.genrateCustomerWiseSalesReport(request)
                ElseIf ReportName = "SalesAndTranctionReport" Then
                    objFrmPosReport.DisplaySalesandTransactionsReport(request)
                ElseIf ReportName = "FoodCostReport" Then
                    objFrmPosReport.GenerateFoodCost(request)
                ElseIf ReportName = "TargetVsActualSales" Then
                    objFrmPosReport._paraMonth = _paraMonth
                    objFrmPosReport._paraYear = _paraYear
                    objFrmPosReport.TargetVsAcutalSalesReport(request)
                ElseIf ReportName = "JKOftheDayPayoutReport" Then
                    objFrmPosReport.JKDayOfReportPayout(request, _PromotionIds)
                ElseIf ReportName = "JKProductMixReport" Then
                    objFrmPosReport.JKProductMixReport(request, clsAdmin.UserName)
                ElseIf ReportName = "ConversionReport" Then 'added by khusrao adil on 29-11-2017 for jk sprint  32
                    ' report name modified by khusrao adil on 11-01-2018 for jk 
                    objFrmPosReport.JKProductMixReportTillWise(request, Terminals:=clsAdmin.TerminalID)
                ElseIf ReportName = "HcCustomerDetailsReport" Then
                    objFrmPosReport.HcCustomerDetailReport(request, SelectedClass)
                ElseIf ReportName = "SalesReconciliationReport" Then
                    objFrmPosReport.GenerateJKSalesReconciliation(request)
                ElseIf ReportName = "SalesPersonwiseSalesReport" Then
                    objFrmPosReport.GeneratePersonWiseSalesReport(request)
                ElseIf ReportName = "BillSummaryReport" Then
                    objFrmPosReport.GenerateBillSummaryReport(request)

                ElseIf ReportName = "BillWiseGSTReport" Then
                    objFrmPosReport.GenerateBillWiseGSTReport(request)
                ElseIf ReportName = "BillWiseTenderReport" Then
                    objFrmPosReport.GenerateBillWiseTenderReport(request, clsAdmin.UserName)
                ElseIf ReportName = "SpectrumKOTReport" Then
                    objFrmPosReport.GenerateKOTReport(request)
                ElseIf ReportName = "DeliveryPartnerWiseSalesReport" Then
                    objFrmPosReport.GenerateDeliveryPartnerWiseSalesReport(request, SelectedPartner)
                ElseIf ReportName = "TenderWiseCommisionReport" Then
                    objFrmPosReport.GenerateTenderWiseCommisionReport(request, clsAdmin.UserName, SelectedTender)
                ElseIf ReportName = "TallySalesReport" Then 'vipin
                    objFrmPosReport.GenerateTallySalesReport(request)
                ElseIf ReportName = "ImprestCashAmountDetails" Then 'vipin
                    objFrmPosReport.ImprestCashAmountDetailsReport(request)
                ElseIf ReportName = "OrderTypeWiseSalesReport" Then 'vipin
                    objFrmPosReport.OrderTypeWiseSalesReport(request)
                Else
                    objReport.GenerateDayCloseReport(request, clsDefaultConfiguration.DayCloseReportPath)
                End If
                'Me.Cursor = Cursors.Default
                objFrmPosReport.Close()
                objFrmPosReport.Dispose()

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ToolsMenu_DropDownClosed(sender As Object, e As System.EventArgs) Handles ToolsMenu.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            ToolsMenu.ForeColor = Color.White
        End If
    End Sub

    Private Sub ToolsMenu_DropDownOpened(sender As Object, e As System.EventArgs) Handles ToolsMenu.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            ToolsMenu.ForeColor = Color.Black
        End If
    End Sub


    Dim isClosed As Boolean = False
    Private Sub DayCloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayCloseToolStripMenuItem.Click
        'If clsDefaultConfiguration.SyncOnDayClose Then
        '    If True Then

        '    Else

        '    End If
        'End If

        For Each form In My.Application.OpenForms
            If (form.name = frmDayCloseMain.Name) Then
                Exit Sub
            End If
        Next

        Dim objDayCLose As New clsDayClose
        ''added By ketan JK Changes
        'If clsDefaultConfiguration.ClientForMail = "JK" Then
        '    If Not CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN_MODIFY") Then
        '        If objDayCLose.CheckBillCount(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then
        '            'MessageBox.Show("Day close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
        '            ShowMessage("Day close not allowed since no bills are punched for the day.", "WARNING MESSAGE")
        '            Exit Sub
        '        End If
        '    End If
        'End If
        If objDayCLose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then

            'Dim dtDayOpenStatus As DataTable = objLogin.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)

            'If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then
            '    AnyTerminalOpen = dtDayOpenStatus.Rows(0)("AnyTerminalOpen")

            '    If (AnyTerminalOpen > 0) Then
            '        ShowMessage(True, getValueByKey("LG021"), "LG021 - " & getValueByKey("CLAE04"))
            '        Exit Sub
            '    End If
            'End If
            Dim DisplayDayOpenDate As String = clsAdmin.DayOpenDate
            'If (MessageBox.Show(String.Format(getValueByKey("LG022"), DisplayDayOpenDate), "LG022 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel) Then
            '    Exit Sub
            'End If
            Dim eventType As Int32
            ShowMessage(String.Format(getValueByKey("LG022"), DisplayDayOpenDate), "CM014 - " & getValueByKey("CLAE04"), eventType, "Cancel", "Ok")
            If eventType = 2 Then
                Exit Sub
            End If

            Dim objDefault As New clsDefaultConfiguration("DC")
            objDefault.GetDefaultSettings()
            If clsDefaultConfiguration.DayCloseOtherScreens = False Then
                If objDayCLose.CheckIfAllTerminalAreClosed(clsAdmin.SiteCode) = False Then
                    ShowMessage(getValueByKey("mdispectrum.dayclosevalidationmsg"), getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            'auto-update on day close ..day close will happen after update automatically JK requested on 14-oct-2015

            If clsDefaultConfiguration.AutoUpdateonDayClose Then
                AutoUpdates()
                If isClosed Then
                    isClosed = False
                    Exit Sub
                End If
            End If

            ''added by nikhil for Hari OM

            If clsDefaultConfiguration.IsHariOM Then
                Dim tillClose As New frmNTillClosing
                ' tillClose.cmdFinsh_Click(sender, New EventArgs)
                'TillClose_Click(sender, New EventArgs)
                tillClose.AllTillCloseForHariOM()


            End If


            If clsDefaultConfiguration.IsHariOM Then

            Else
                Dim childForm As New Spectrum.frmDayCloseMain
                Try
                    'childForm.MdiParent = Me
                    childForm.Show()
                    childForm.GetNextScreen()

                Catch ex As Exception
                    childForm.Close()
                End Try
            End If
        Else
            ShowMessage(String.Format(getValueByKey("mdispectrum.daycloseperformedmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
        End If
    End Sub

    Public Sub AutoUpdates()
        Try
            Dim proxy As New FOAutomaticVersionUpgradeServiceImplClient
            Dim exs As New executeService
            Dim exser As New executeService



            Dim licence As New ClsLicense
            Dim HDDKey = licence.GetEncryptedHDDKey()
            Dim exsA As executeService = CreateExecuteServiceObject("EN", "isUpdateAvailable", HDDKey, clsAdmin.SiteCode, clsAdmin.TerminalID)

            Dim resp As executeServiceResponse = proxy.executeService(exsA)
            Dim resp1 As wsResponse = resp.[return]
            If resp1.responseCode = "200" Then
                Dim message As New StringBuilder
                'message.Append("Update for a new Spectrum version is available.")
                'message.Append("System will now install the new update and restart Spectrum.")
                'message.Append("Spectrum will restart and the login screen will appear once the update installation is done.")
                'message.Append(" Continue with Day Close operation once the update installation is completed.")
                message.Append("Spectrum is being updated to latest version.")
                message.Append("Spectrum application will now close while this operation is being performed.")
                message.Append("Please continuie with day close operation once Spectrum re-starts. ")
                message.Append("Kindly be patient during this process..")

                ShowBigMessagewithOK(message.ToString(), "DBCB004 - " & getValueByKey("CLAE04"))
                Application.DoEvents()
                Dim objCls As New clsCommon
                directoryPath = directoryPath & "\Update"
                If Directory.Exists(directoryPath) Then
                    'Directory.Delete(directoryPath)
                    DeleteDirectory(directoryPath)
                End If
                Directory.CreateDirectory(directoryPath)




                Dim req As New wsRequest
                req.languageCode = "EN-US"
                req.webMethod = "fetchExeFile"

                Dim soap As New soapWsHeader
                soap.userName = ""
                soap.password = ""
                req.soapWsHeader = soap

                exser.arg0 = req

                Dim exresp As executeServiceResponse = proxy.executeService(exser)

                Dim wsResonse As wsResponse = exresp.return

                Dim t As dynaTable = wsResonse.dynaTables.FirstOrDefault()
                Dim q As dynaRow = t.dynaRows.FirstOrDefault()
                Dim w As dynaColumn = q.dynaColumn.FirstOrDefault()
                Dim updatezip As String = w.columnValue

                Dim bytIn() As Byte = System.Convert.FromBase64String(updatezip)
                Dim wFile As FileStream = New FileStream(directoryPath & "\setup.zip", FileMode.Create)
                wFile.Write(bytIn, 0, bytIn.Length)
                wFile.Close()


                Using zip1 As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read(directoryPath & "\setup.zip")
                    zip1.ExtractAll(directoryPath,
                            Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                End Using

                Dim dataSource As String = ReadSpectrumParamFile("Server")
                Dim dbname As String = ReadSpectrumParamFile("DataSource")
                Dim username As String = ReadSpectrumParamFile("UserId")
                Dim password As String = ReadSpectrumParamFile("Password")
                Dim clientname As String = objCls.GetSiteName("CCE")
                Dim AutoUpdateDayClose As String = "Yes"
                'HDDKey = "c4:43:8f:43:e4:2f"
                Dim pHelp As New ProcessStartInfo
                pHelp.FileName = "SpectrumUpdate.exe"
                pHelp.Arguments = "" & dataSource & " " & dbname & " " & username & " " & password & " " & clientname.Replace(" ", "") & " " & HDDKey & " " & clsAdmin.SiteCode & " " & clsAdmin.TerminalID & " " & AutoUpdateDayClose & ""
                pHelp.WorkingDirectory = directoryPath
                'pHelp.WorkingDirectory = "C:\Users\sagar.borole\Desktop\ConsoleApplication1"
                Process.Start(pHelp)
                Application.Exit()
                'Else
                '    ShowMessage("New Version not available", "DBCB004 - " & getValueByKey("CLAE04"))

            End If
        Catch ex As Exception
            'MessageBox.Show("Error")
            LogException(ex)
            If (ex.ToString().Contains("no endpoint")) Then
                ShowMessage("Invalid CCE Connection", "DBCB004 - " & getValueByKey("CLAE04"))
            End If
            isClosed = True
        End Try
    End Sub

    Private Shared Function CreateExecuteServiceObject(languageCode As String, webMethod As String, hardwareKey As String, siteCode As String, terminalId As String) As executeService
        Try
            Dim exs As New executeService()

            Dim req As New wsRequest()
            req.languageCode = languageCode
            req.webMethod = webMethod

            Dim soap As New soapWsHeader()
            soap.userName = ""
            soap.password = ""
            req.soapWsHeader = soap

            Dim row As New dynaRow()

            Dim col As New dynaColumn()
            col.columnName = "hardwareKey"
            col.columnType = "STRING"
            col.columnValue = hardwareKey

            Dim col2 As New dynaColumn()
            col2.columnName = "siteCode"
            col2.columnType = "STRING"
            col2.columnValue = siteCode

            Dim col3 As New dynaColumn()
            col3.columnName = "terminalId"
            col3.columnType = "STRING"
            col3.columnValue = terminalId



            Dim cols As New List(Of dynaColumn)()
            cols.Add(col)
            cols.Add(col2)
            cols.Add(col3)


            row.dynaColumn = cols.ToArray()
            req.dynaColumns = row

            exs.arg0 = req


            Return exs
        Catch ex As Exception

            Return Nothing
        End Try
    End Function

    Private Sub DeleteDirectory(path As String)
        If Directory.Exists(path) Then
            'Delete all files from the Directory
            For Each filepath As String In Directory.GetFiles(path)
                File.Delete(filepath)
            Next
            'Delete all child Directories
            For Each dir As String In Directory.GetDirectories(path)
                DeleteDirectory(dir)
            Next
            'Delete a Directory
            Directory.Delete(path)
        End If
    End Sub

    Private Sub NewVoucherEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewVoucherEntryToolStripMenuItem.Click
        Dim objDayCLose As New clsDayClose
        If clsDefaultConfiguration.IsPettyCashApplicable Then
            If objDayCLose.CheckIfValidDayClose(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.DayOpenDate) Then
                Dim ChildForm As New Spectrum.frmPCVoucherEntry
                Try
                    If ChildForm.Name <> String.Empty Then
                        ShowChildForm(ChildForm, False)
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            Else
                ShowMessage(String.Format(getValueByKey("mdispectrum.pettycashvalidationmsg"), clsAdmin.DayOpenDate.ToShortDateString()), getValueByKey("CLAE04"))
            End If
        Else
            ShowMessage(getValueByKey("mdispectrum.pettycashnotallowedmsg"), getValueByKey("CLAE04"))
        End If
    End Sub

    Private Sub ViewVocuherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewVocuherToolStripMenuItem.Click
        If clsDefaultConfiguration.IsPettyCashApplicable Then
            Dim ChildForm As New Spectrum.frmViewVoucher
            Try
                If ChildForm.Name <> String.Empty Then
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Else
            ShowMessage(getValueByKey("mdispectrum.pettycashnotallowedmsg"), getValueByKey("CLAE04"))
        End If
    End Sub

    Private Sub QuotationCreaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles QuotationCreaToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNQuotationCreation
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub UpdateQuotationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UpdateQuotationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNQuotationUpdate
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub


    'Private Sub TransferToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TransferToolStripMenuItem.Click
    '    Try
    '        Dim ChildForm As New Spectrum.frmTransfer
    '        Try
    '            If ChildForm.Name <> String.Empty Then
    '                ShowChildForm(ChildForm, True)
    '            End If
    '        Catch ex As Exception
    '            ChildForm.Close()
    '        End Try

    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub


    Private Sub CreditSaleAdjToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreditSaleAdjToolStripMenuItem.Click
        Try
            'Dim childForm As New frmNCreditSales(True)
            'If childForm.Name <> String.Empty Then
            '    ShowChildForm(childForm, False)
            '    childForm.MdiParent = Me
            '    childForm.Dock = DockStyle.Fill
            'End If
            'vipin 21.05.2018
            If clsDefaultConfiguration.IsNewCreditSale Then
                Dim objCreditSales As New frmNCreditSalesNew(False)
                objCreditSales.ShowDialog()
            Else
                Dim objCreditSales As New frmNCreditSales(False)
                objCreditSales.ShowDialog()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub notification()
        Try
            Dim TimeInterval = clsDefaultConfiguration.TicketingSystemPopupInterval
            Dim objCommon As New clsCommon
            Dim GrievanceNotification = objCommon.GetGrievanceNotification()
            If GrievanceNotification.Rows.Count > 0 Then
                For row = 0 To GrievanceNotification.Rows.Count - 1
                    'MobNo = MobNo + "," + DeptMobNo.Rows(MobileRow)(0).ToString         
                    popupNotifier1.TitleText = "Notification Title"
                    popupNotifier1.ContentText = "Kindly check the ticketing system for new updates :-" + GrievanceNotification.Rows(row)(0)

                    popupNotifier1.ShowCloseButton = True
                    popupNotifier1.ShowOptionsButton = True
                    popupNotifier1.ShowGrip = True
                    popupNotifier1.Delay = TimeInterval * 1000
                    popupNotifier1.AnimationInterval = 10
                    popupNotifier1.AnimationDuration = 1000
                    popupNotifier1.TitlePadding = New Padding(0)
                    popupNotifier1.ContentPadding = New Padding(0)
                    popupNotifier1.ImagePadding = New Padding(0)
                    popupNotifier1.Scroll = True
                    'popupNotifier1.Image = My.Resources._157_GetPermission_48x48_72
                    popupNotifier1.Popup()
                    objCommon.UpdateGrievanceNotification(GrievanceNotification.Rows(row)(0).ToString)
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CancelQuotationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CancelQuotationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmNQuotationCancel
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub DayOpenMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DayOpenMenuItem.Click
        Dim objCommon As New clsCommon
        Dim objLoginData As New clsLogin
        Dim dayOpenMessage As String = String.Empty
        Dim IsDayOpen As Boolean = False
        Dim DayOpenDate As DateTime
        Dim checkDayAlreadyOpen As Boolean = False
        'ShiftOpen.Enabled = True
        Try
            Dim currentDate = objCommon.GetCurrentDate()
            Dim dtDayOpenStatus = objLogin.GetDayCloseStatus(clsAdmin.SiteCode, clsAdmin.Financialyear)

            If dtDayOpenStatus IsNot Nothing AndAlso dtDayOpenStatus.Rows.Count > 0 Then

                LastClosedDate = IIf(dtDayOpenStatus.Rows(0)("LastClosedDate") Is DBNull.Value, Date.MinValue, dtDayOpenStatus.Rows(0)("LastClosedDate"))
                AnyTerminalOpen = dtDayOpenStatus.Rows(0)("AnyTerminalOpen")
                DayOpenDate = IIf(dtDayOpenStatus.Rows(0)("DayOpenDate") Is DBNull.Value, Date.MinValue, dtDayOpenStatus.Rows(0)("DayOpenDate"))

                If (DayOpenDate <> Date.MinValue) Then
                    'Day Close for date 'Monday, Dec 15, 2013' not performed
                    'dayOpenMessage = String.Format(getValueByKey("LG018"), DirectCast(DayOpenDate, Date).ToString("ddd MMM dd HH:mm:ss IST yyyy"))
                    'ShowMessage(False, dayOpenMessage, "LG018 - " & getValueByKey("CLAE04"))

                    ShowMessage(False, getValueByKey("LG024"), "LG024 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If (LastClosedDate = Date.MinValue) Then
                'LastClosedDate = objLogin.GetFinancialYearStartDate(clsAdmin.SiteCode).AddDays(-1)
                ''
                If Not CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN_MODIFY") Then
                    objLogin.InsertDayOpenDetail(clsAdmin.SiteCode, clsAdmin.Financialyear, clsAdmin.CurrentDate, clsAdmin.UserCode)
                    clsAdmin.DayOpenDate = currentDate
                    IsDayOpen = True
                End If
                ' LastClosedDate = objLogin.GetFinancialYearStartDate(clsAdmin.SiteCode).AddDays(-1)
                'objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear,clsAdmin.CurrentDate, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
            End If
            If (LastClosedDate.AddDays(1).Date > currentDate.Date) Then
                ShowMessage(False, String.Format(getValueByKey("LG025"), LastClosedDate.ToString("ddd MMM dd HH:mm:ss IST yyyy"), currentDate.ToString("ddd MMM dd HH:mm:ss IST yyyy")), "LG025 - " & getValueByKey("CLAE04"))
                Exit Sub

            ElseIf (currentDate.Date = LastClosedDate.AddDays(1).Date) Then

                objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, LastClosedDate.AddDays(1).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                clsAdmin.DayOpenDate = LastClosedDate.AddDays(1).Date
                IsDayOpen = True

            ElseIf (currentDate.Date > LastClosedDate.AddDays(1).Date) AndAlso CheckAuthorisation(clsAdmin.UserCode, "DAY_OPEN_MODIFY") Then
                Dim dayOpenForm As New frmDayOpen
                dayOpenForm.dayOpenDate.Value = currentDate
                dayOpenForm.MinDate = LastClosedDate
                dayOpenForm.MaxDate = currentDate
                dayOpenForm.ShowDialog()
                Me.Cursor = Cursors.WaitCursor
                If dayOpenForm.dayOpenDate.Value Is Nothing Or IsDBNull(dayOpenForm.dayOpenDate.Value) Then
                    Return
                Else
                    ''added by nikhil Uplanchiwar for Hari Om to Update OpencloseStatus when Bill not Done
                    If clsDefaultConfiguration.IsHariOM Then

                        If dayOpenForm.DayUpdate Then
                            '  checkDayAlreadyOpen = dayOpenForm.DayUpdate
                            Dim Status As Boolean = objclsCommon.GetDayOpenOrCloseForHariOM(DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.SiteCode)
                            If Status = True Then
                                objLoginData.UpdateDayOpenDetailsForHariOm(clsAdmin.SiteCode, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date)
                            Else
                                objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                            End If

                            clsAdmin.DayOpenDate = dayOpenForm.dayOpenDate.Value
                            IsDayOpen = True
                        End If
                    Else
                        objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                        clsAdmin.DayOpenDate = dayOpenForm.dayOpenDate.Value
                        IsDayOpen = True
                    End If
                    '31-07-2017
                    'objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, DirectCast(dayOpenForm.dayOpenDate.Value, Date).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                    'clsAdmin.DayOpenDate = dayOpenForm.dayOpenDate.Value
                    'IsDayOpen = True

                End If
            Else
                If Not (IsDayOpen) Then
                    'Last day close on 'Monday, Dec 15, 2013'. To proceed to Day open for 'Tuesday, Dec 17, 2013' click Ok.
                    'Or click Cancel and contact the authorized user to select other date between last day close and current date.
                    dayOpenMessage = String.Format(getValueByKey("LG019"), LastClosedDate.ToString("ddd MMM dd HH:mm:ss IST yyyy"), LastClosedDate.AddDays(1).ToString("ddd MMM dd HH:mm:ss IST yyyy"))

                    If (MessageBox.Show(dayOpenMessage, "LG019 - " & getValueByKey("CLAE04"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK) Then
                        objLoginData.InsertDayOpenDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, LastClosedDate.AddDays(1).Date, clsAdmin.UserCode, clsDefaultConfiguration.IsPettyCashApplicable)
                        clsAdmin.DayOpenDate = LastClosedDate.AddDays(1).Date
                        IsDayOpen = True
                    End If
                End If
            End If

            If (IsDayOpen) Then
                DisableTransactionMainMenu(False)
                DayOpenMenuItem.Enabled = True
                DayCloseToolStripMenuItem.Enabled = True
                'TillOpen.Enabled = True
                If clsDefaultConfiguration.ShiftManagement = True Then
                    ShiftOpen.Enabled = True
                Else
                    TillOpen.Enabled = True
                End If
                lblTodayDate.Text = clsAdmin.DayOpenDate.ToShortDateString()
                ''performance query  It will take around 2-6 min & help to improve performance while billing 
                Dim Performance As Boolean = objCommon.GetPerformanceQuery()
                Me.Cursor = Cursors.Default
                'Day opened for the date: 'Monday, Dec 15, 2013'
                dayOpenMessage = String.Format(getValueByKey("LG020"), clsAdmin.DayOpenDate.ToString("ddd MMM dd HH:mm:ss IST yyyy"))
                ShowMessage(False, dayOpenMessage, "LG020 - " & getValueByKey("CLAE04"))

                ''added by nikhil uplanchiwar for Hari Om    on 06/04/2017
                If clsDefaultConfiguration.IsHariOM Then
                    finishDataForHariOm()
                End If

                If clsDefaultConfiguration.SpectrumLiteAllowed And clsDefaultConfiguration.ShiftManagement = False Then
                    TillOpen_Click(sender, New EventArgs())
                End If
                'If clsDefaultConfiguration.ShiftManagement = True Then
                '    ShiftOpen_Click(sender, New EventArgs())
                'Else
                '    TillOpen_Click(sender, New EventArgs())
                'End If

            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    '' added by nikhil For Hari OM
    Public Sub finishDataForHariOm()

        Try
            Dim objTill As New clsTill()
            Dim objLoginData As New clsLogin
            Dim OpeningTerminalBalance, CurrentTerminalBalance As Double
            Dim ServerDate = objclsCommon.GetCurrentDate()

            Dim EditMode As Boolean = False
            If EditMode = False Then
                OpenTerminalHariOm()
                DisableTransactionMainMenu(True)
                clsAdmin.Financialyear = objLoginData.GetFinancialYear(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
            End If
            If EditMode Then
                ShowMessage(String.Format("{0} Updated Successfully", clsAdmin.TerminalID), getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' added by nikhil Uplanchiwar for Hari OM for day Open without Till Close
    ''' 
    Public Sub OpenTerminalHariOm()
        Try
            Dim objTill As New clsTill()

            If clsDefaultConfiguration.IsHariOM Then
                Dim daAllTerminal As DataTable = objTill.GetAllTerminalForHariOm(clsAdmin.SiteCode)
                If daAllTerminal.Rows.Count > 0 Then
                    For i = 0 To daAllTerminal.Rows.Count - 1
                        'Dim _terminalLocation As String = ""
                        'If i = 0 Then
                        '    _terminalLocation = My.Computer.Name
                        'End If
                        If objTill.OpenCloseTerminalForHariOm(daAllTerminal.Rows(i)("TerminalID"), clsAdmin.SiteCode, clsAdmin.UserCode) = True Then
                            clsDefaultConfiguration.TillOpenDone = True
                        End If
                    Next
                    'MessageBox.Show(getValueByKey("TO05"), "TO05 - " & getValueByKey("CLAE04"))
                    ShowMessage("All Till Opened", "Information")
                End If
            Else
                If objTill.OpenCloseTerminal(clsAdmin.TerminalID, clsAdmin.SiteCode, True, clsAdmin.UserCode) = True Then
                    clsDefaultConfiguration.TillOpenDone = True
                    MessageBox.Show(getValueByKey("TO05"), "TO05 - " & getValueByKey("CLAE04"))
                End If
            End If
            '
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub ViewOrderDetailsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewOrderDetailsToolStripMenuItem.Click

        Try
            ' Dim ChildForm As New Spectrum.frmViewOrderDetails
            Dim ChildForm As New Spectrum.frmSearchCustomer
            Try
                If ChildForm.Name <> String.Empty Then
                    'ChildForm.pSearchCust = "SEARCH"
                    ChildForm.Tag = String.Empty
                    ShowChildForm(ChildForm, False)
                End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub spectrumTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spectrumTimer.Tick
        If clsAdmin.DisplayShift Then
            lblShiftName.Visible = True
            lblShiftStatus.Visible = True
            lblShiftName.Text = clsAdmin.ShiftName
            lblShiftStatus.Text = clsAdmin.ShiftStatus
            RibbonSeparator5.Visible = True
            clsAdmin.DisplayShift = False
        End If
        rbnSpectrumTime.Text = System.DateTime.Now.ToLongTimeString()
    End Sub

    Private Sub ProductInfoMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProductInfoMenuItem.Click
        Dim LicHelper As New ClsLicense()
        Dim HDDKey = LicHelper.GetEncryptedHDDKey()
        Dim CurrentLicense = LicHelper.FetchLicenseFromDB(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)
        Dim ChildForm As New Spectrum.TrailVersionProductInfo(CurrentLicense)
        'Dim ChildForm2 As New Spectrum.LicenseVersionProductInfo(CurrentLicense)
        Try
            If CurrentLicense IsNot Nothing Then
                If CurrentLicense.VersionType = SpectrumCommon.VersionType.Licensed Then
                    ChildForm.Text = "Product Information - Enterprise Version"
                    ChildForm.lblLicenseVersion.Text = "Spectrum Enterprise Version Activated"
                Else
                    ChildForm.Text = "Product Information - Trail Version"
                    ChildForm.lblLicenseVersion.Text = "Spectrum Trial Version Activated"
                End If

                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Function ValidateClientLicense() As Boolean

        Dim licence As New ClsLicense
        Dim HDDKey = licence.GetEncryptedHDDKey()


        Dim supportEmailID = licence.GetSupportEmailID(clsAdmin.SiteCode)
        '''Dim supportEmailID = licence.GetSupportEmailID(clsAdmin.SiteCode)
        If (clsDefaultConfiguration.SpectrumLicenseRequired) Then
            Dim licenseStatus As LicenseStatus
            'licenseStatus = clientLicense.ValidateCurrentLicense(clsAdmin.SiteCode, clsAdmin.TerminalID)
            licenseStatus = clientLicense.ValidateCurrentLicense(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)

            If licenseStatus = SpectrumCommon.LicenseStatus.None OrElse licenseStatus = SpectrumCommon.LicenseStatus.InvalidHardwareKey Then

                Dim selector As New VersionSelector()
                If (selector.ShowDialog() = Windows.Forms.DialogResult.Cancel) Then
                    selector.Close()
                    Application.Exit()
                    Return False
                End If
            Else
                'Dim daysleft = clientLicense.GetDaystoExpire(clsAdmin.SiteCode, clsAdmin.TerminalID)
                Dim daysleft = clientLicense.GetDaystoExpire(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)

                If licenseStatus = licenseStatus.Valid AndAlso daysleft <= 15 Then
                    If daysleft >= 0 Then
                        Dim warning As New LicenseWarning(String.Format("The evaluation license will expire in {0} day(s)." & vbCrLf & " Contact Spectrum support at {1} to renew your license", daysleft, supportEmailID))
                        warning.ShowDialog()
                        warning.Dispose()
                    Else
                        Dim warning As New LicenseWarning(String.Format("Evaluation License you are using is expired." & vbCrLf & " Contact Spectrum support at {0} to renew your license", supportEmailID))
                        warning.ShowDialog()
                        warning.Dispose()
                    End If

                ElseIf licenseStatus = SpectrumCommon.LicenseStatus.Invalid Then

                    Dim warning As New LicenseWarning(String.Format("Evaluation License you are using is expired." & vbCrLf & " Contact Spectrum support at {0} to renew your license", supportEmailID))
                    warning.ShowDialog()
                    warning.Dispose()
                    Application.Exit()
                    Return False
                End If
            End If

            If (licenseStatus <> SpectrumCommon.LicenseStatus.None) Then
                'Dim licenseModel = clientLicense.FetchLicenseFromDB(clsAdmin.SiteCode, clsAdmin.TerminalID)
                Dim licenseModel = clientLicense.FetchLicenseFromDB(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)
                Dim licenseKey = clientLicense.DecryptKey(licenseModel.LicenseKey)

                'Dim binding As New System.ServiceModel.BasicHttpBinding()

                ' Dim LicencseServiceUrl As String = ReadWebServiceParaFile("LicencseServiceUrl")
                'Specify the address to be used for the client.:2051/LicenseService.svc
                'Dim address As New System.ServiceModel.EndpointAddress()

                ''Create a client that is configured with this address and binding.
                'Dim client As New LicenseServiceClient()

                ' Dim client As New LicenseServiceClient()


                'Dim client As New LicenseServiceClient()
                'AddHandler client.GetLicenseByLicenseKeyCompleted, AddressOf ValidateClientLicenseCompletedHandler
                'client.GetLicenseByLicenseKeyAsync(licenseKey)
                'OLD START''
                'Dim serviceThread As New System.Threading.Thread(AddressOf ValidateClientLicenseFromService)
                'serviceThread.Start(licenseKey)
                'OLD END''
                ''NEW START''
                Dim serviceThread2 As New System.Threading.Thread(AddressOf ValidateClientLicenseIssuDateFromServiceByHardwareKey)
                serviceThread2.Start(HDDKey)
                ''NEW END''
                'Dim result = client.GetLicenseByLicenseKey(licenseKey)

                'clientService.BeginGetLicenseByLicenseKey(licenseKey, AddressOf ValidateClientLicenseResponse, Nothing)
            End If

            Return True
        End If

    End Function

    Public Sub ValidateClientLicenseFromService(licenseKey As String)
        Try
            Dim client As New LicenseServiceClient()
            AddHandler client.GetLicenseByLicenseKeyCompleted, AddressOf ValidateClientLicenseCompletedHandler
            '   client.GetLicenseByLicenseKeyAsync(licenseKey)
            client.GetLicenseByLicenseKey(licenseKey)
        Catch ex As Exception
            LogException(ex)
            ' Call AccessControl()
        End Try
    End Sub
    Public Sub ValidateClientLicenseIssuDateFromServiceByHardwareKey(hardwareKey As String)
        Try
            Dim client As New LicenseServiceClient()
            'AddHandler client.GetIssueDateByHardwareKeyCompleted, AddressOf ValidateClientLicenseIssueDateByHardwareKeyCompletedHandler
            AddHandler client.GetIssueDateByHardwareKeyNewCompleted, AddressOf ValidateClientLicenseIssueDateByHardwareKeyCompletedHandler
            client.GetIssueDateByHardwareKeyNewAsync(hardwareKey, clsAdmin.SiteCode, clsAdmin.TerminalID)

            'client.GetIssueDateByLicenseKeyCompletedEventArgs(licenseKey)
            'client.GetLicenseByLicenseKey(licenseKey)
        Catch ex As Exception
            LogException(ex)
            ' Call AccessControl()
        End Try
    End Sub
    Public Sub ValidateClientLicenseIssueDateByHardwareKeyCompletedHandler(ByVal sender As Object, ByVal e As Spectrum.LicenseServices.GetIssueDateByHardwareKeyNewCompletedEventArgs)
        Try
            Dim licence As New ClsLicense
            Dim HDDKey = licence.GetEncryptedHDDKey()
            Dim licenseModel = clientLicense.FetchLicenseFromDB(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)
            Dim adminlicenseKey = clientLicense.DecryptKey(licenseModel.LicenseKey)
            'Dim strary As Array = e.Result.Split("$")
            'Dim IssueDate As DateTime
            'Dim Licensekeys As String
            Dim onlineNewLicense = e.Result

            If onlineNewLicense.IssueDate.Date <> licenseModel.ActivationDate.Date Then
                If onlineNewLicense.IssueDate.Date.AddDays(onlineNewLicense.Validity) > DateTime.Now Then
                    Dim obj1 As New ClsLicense
                    licenseModel.LicenseKey = onlineNewLicense.LicenseKey
                    licenseModel.ActivationDate = onlineNewLicense.IssueDate
                    obj1.InstallBackDatedLicense(licenseModel)
                End If
            End If


            If onlineNewLicense.ExpireDate < DateTime.Now Then

                If (licenseModel.VersionType = VersionType.Licensed) Then
                    clientLicense.UpdateLicenseInDB(licenseModel)
                End If
            End If


        Catch ex As Exception
            LogException(ex)
            ' Call AccessControl()
        End Try
    End Sub

    Private Sub AccessControl()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf AccessControl))
            Else
                '----Dated 02 june Message is Commenting For vinit suggestion no need to show client 
                '     ShowMessage("Problem in Connecting to Internet", "Information")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub ValidateClientLicenseCompletedHandler(ByVal sender As Object, ByVal e As Spectrum.LicenseServices.GetLicenseByLicenseKeyCompletedEventArgs)
        Dim licence As New ClsLicense
        Dim HDDKey = licence.GetEncryptedHDDKey()
        If Not (e.Result) Then
            Dim licenseModel = clientLicense.FetchLicenseFromDB(clsAdmin.SiteCode, HDDKey, clsAdmin.TerminalID)

            If (licenseModel.VersionType = VersionType.Licensed) Then
                clientLicense.UpdateLicenseInDB(licenseModel)
            End If
        End If

    End Sub

    'Private clientService As New LicenseServiceClient()

    'Private Sub ValidateClientLicenseResponse(ByVal result As IAsyncResult)
    '    Dim isValidLicense = clientService.EndGetLicenseByLicenseKey(result)

    '    If Not (isValidLicense) Then
    '        Dim licenseModel = clientLicense.FetchLicenseFromDB(clsAdmin.SiteCode, clsAdmin.TerminalID)

    '        If (licenseModel.VersionType = VersionType.Licensed) Then
    '            clientLicense.UpdateLicenseInDB(licenseModel)
    '        End If
    '    End If
    'End Sub

    Private Sub DirectReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DirectReportToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmReportList
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub AllReportToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AllReportToolStripMenuItem1.Click
        Dim ChildForm As New Spectrum.frmPosReports
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub


    Private Sub SiteDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SiteDetailsToolStripMenuItem.Click

        Dim ChildForm As New Spectrum.BO.frmSiteDetails()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub BarcodePrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodePrintToolStripMenuItem.Click
        'Dim ChildForm As New Spectrum.BO.frmBarcode()
        'Try
        '    If ChildForm.Name <> String.Empty Then
        '        ShowChildForm(ChildForm, False)
        '    End If
        'Catch ex As Exception
        '    ChildForm.Close()
        'End Try

    End Sub

    Private Sub CharacteristicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CharacteristicsToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmCharacteristics()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub ItemMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemMasterToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmItemMaster()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub ItemHiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemHiToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmItemHierarchy()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub UploadItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadItemToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmImportExportItem(CInt(Spectrum.BO.CommonFunc.enumImportExportItem.UploadItem))
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub ExportArticleXLSReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportArticleXLSReportToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmImportExportItem(CInt(Spectrum.BO.CommonFunc.enumImportExportItem.ExportArticleXlsReport))
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub ExportArticleHierarchyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportArticleHierarchyToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmImportExportItem(CInt(Spectrum.BO.CommonFunc.enumImportExportItem.ExportArticleHierarchy))
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub ManualPromotionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManualPromotionToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmManualPromotion()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub StockInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockInToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmArticleStockIn()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub StockOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockOutToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmArticleStockOut()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmSupplier()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub TaxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaxToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmTax()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub TenderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TenderToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmTender()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub UserToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim ChildForm As New Spectrum.BO.frmUser()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try

    End Sub

    Private Sub DayCloseReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DayCloseReportToolStripMenuItem.Click
        Dim ChildForm As New frmTest()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ProcSchedulerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProcSchedulerToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmProcScheduler
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub GrievanceToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim ChildForm As New Spectrum.frmGrievance
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub


    Private Sub AddEditViewTickitsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddEditViewTickitsToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmGrievance
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub RecordProductionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordProductionToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmRecordProduction
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub KDSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KDSToolStripMenuItem.Click
        If clsDefaultConfiguration.KotWiseKds Then
            Using ChildForm As New Spectrum.frmPrepStationKotWise(False)
                Try
                    If ChildForm.Name <> String.Empty Then
                        ChildForm.ShowDialog()
                        'ShowChildForm(ChildForm, False)
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            End Using
        Else
            Using ChildForm As New Spectrum.frmPrepStation(False)
                Try
                    If ChildForm.Name <> String.Empty Then
                        ChildForm.ShowDialog()
                        'ShowChildForm(ChildForm, False)
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            End Using
        End If
    End Sub

    Private Sub SetPrepStationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetPrepStationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmSetPrepStation
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub SalesOrderBookingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesOrderBookingToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmPCSalesOrderCreation
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.IsBooking = True
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ExpidiorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpidiorToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmExpeditorScreen
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub SaleOrderEditBookingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaleOrderEditBookingToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmPCNSalesOrderUpdate
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.IsBookingEdit = True
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub StockTakeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StockTakeToolStripMenuItem.Click
        'Dim childForm As New Spectrum.frmStockTake
        'Try

        '    childForm.Show()
        '    childForm.GetNextScreen()

        'Catch ex As Exception
        '    childForm.Close()
        'End Try
        Dim childForm As New Spectrum.frmStockTake
        Try
            If childForm.Name <> String.Empty Then
                ShowChildForm(childForm, False)
                childForm.GetNextScreen()
            End If
            'childForm.Show()
            'childForm.GetNextScreen()

        Catch ex As Exception
            childForm.Close()
        End Try
    End Sub
    Private Sub PendingCformsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PendingCformsToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmPendingCForms
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub EditCformsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EditCformsToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmPendingCForms
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.IsEdit = True
                ShowChildForm(ChildForm, True)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ExecuteSyncToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExecuteSyncToolStripMenuItem.Click
        Try
            Dim proc1 As Integer
            Try
                proc1 = Process.GetProcessesByName(clsDefaultConfiguration.BatchFileProcessName).GetUpperBound(0) + 1
            Catch ex As Exception
                LogException(ex)
            End Try
            If System.IO.File.Exists(Application.StartupPath & "\run.bat") Then
                If proc1 < 1 Then
                    Dim Proc As New System.Diagnostics.Process
                    Proc.StartInfo = New ProcessStartInfo(Application.StartupPath & "\run.bat")
                    Proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Proc.Start()
                Else
                    ShowMessage("Sync is already running", getValueByKey("CLAE05"))
                End If
                'ShowMessage("Sync is running successfully", getValueByKey("CLAE05"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        'code is added by irfan on 28/9/2017 for background image in login page and spectrum page.
        Dim imagepath As String
        Dim fulldirectory As String '= "C:\images\mainbg.png"
        imagepath = clsFiscalprinter.ReadSpectrumParamFile("MDIBGImagePath")


        ' Directory.CreateDirectory("C:\images\")
        ' Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen

        If Not Directory.Exists(imagepath) Then
            If Not (String.IsNullOrEmpty(imagepath)) Then
                'Dim getfilename As String = Path.GetFileName(imagepath)
                If File.Exists(imagepath) Then
                    Me.GrpHome.BackgroundImage = System.Drawing.Bitmap.FromFile(imagepath)
                Else
                    Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen
                End If

            Else
                Me.GrpHome.BackgroundImage = Nothing
                Me.GrpHome.BackColor = Color.FromArgb(72, 72, 72)
                Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
            End If
        Else
            Me.GrpHome.BackgroundImage = My.Resources.LoadingScreen
        End If
        'Me.GrpHome.BackgroundImage = Nothing
        'Me.GrpHome.BackColor = Color.FromArgb(72, 72, 72)
        'Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        If clsDefaultConfiguration.IsTablet Then
            Me.MenuStrip.Font = New Font("Neo Sans", 11, FontStyle.Regular)
        Else
            Me.MenuStrip.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            Me.MenuStrip.BackColor = Color.FromArgb(212, 212, 212)
            ToolsMenu.ForeColor = Color.Black
            WindowsMenu.ForeColor = Color.Black
            Transaction.ForeColor = Color.Black
            PettyCash.ForeColor = Color.Black
            ReportsToolsStripOuterMenuItem.ForeColor = Color.Black
            ToolsToolStripMenuItem.ForeColor = Color.Black
            HelpMenu.ForeColor = Color.Black
            LogoutToolStripMenuItem.ForeColor = Color.Black
            ExitToolStripMenuItem.ForeColor = Color.Black
            TicketingSystemToolStripMenuItem.ForeColor = Color.Black
        End If
        ToolsMenu.Margin = New Padding(0, 3, 0, 0)
        TicketingSystemToolStripMenuItem.Margin = New Padding(0, 3, 0, 0)
        ReportsToolsStripOuterMenuItem.Margin = New Padding(0, 3, 0, 3)
        ' ToolsMenu.Text = ToolsMenu.Text.ToUpper
        ReportsToolsStripOuterMenuItem.Text = ReportsToolsStripOuterMenuItem.Text.ToUpper
        WindowsMenu.Text = WindowsMenu.Text.ToUpper
        Transaction.Text = Transaction.Text.ToUpper
        PettyCash.Text = PettyCash.Text.ToUpper
        ToolsToolStripMenuItem.Text = ToolsToolStripMenuItem.Text.ToUpper
        HelpMenu.Text = HelpMenu.Text.ToUpper
        LogoutToolStripMenuItem.Text = LogoutToolStripMenuItem.Text.ToUpper
        ExitToolStripMenuItem.Text = ExitToolStripMenuItem.Text.ToUpper

        TicketingSystemToolStripMenuItem.Text = TicketingSystemToolStripMenuItem.Text.ToUpper
        ToolsMenu.TextAlign = ContentAlignment.MiddleCenter
        ReportsToolsStripOuterMenuItem.TextAlign = ContentAlignment.MiddleCenter
        TicketingSystemToolStripMenuItem.TextAlign = ContentAlignment.BottomCenter
        MenuStrip.MinimumSize = New Size(0, 28)                 'code is added by irfan on 22/9/2017 for icon visiblity in theme 1 selection flag
        'Me.DayOpenMenuItem.Image = Nothing
        'Me.DayCloseToolStripMenuItem.Image = Nothing
        'Me.TillOpen.Image = Nothing
        'Me.ShiftOpen.Image = Nothing
        'Me.TillClose.Image = Nothing
        'Me.ShiftClose.Image = Nothing
        'Me.CashMemo.Image = Nothing
        'Me.BirthList.Image = Nothing
        'Me.SalesOrder.Image = Nothing
        'Me.StockCheck.Image = Nothing
        'Me.WindowsMenu.Image = Nothing
        'Me.BarcodePrintToolStripMenuItem.Image = Nothing
        'Me.CharacteristicsToolStripMenuItem.Image = Nothing
        'Me.CustomerToolStripMenuItem.Image = Nothing
        'Me.NewCustomer.Image = Nothing
        'Me.NewLoyaltyCustomer.Image = Nothing
        'Me.SearchEdit.Image = Nothing
        'Me.SearchCLPCustomer.Image = Nothing
        'Me.ViewOrderDetailsToolStripMenuItem.Image = Nothing
        'Me.ItemToolStripMenuItem.Image = Nothing
        'Me.ItemMasterToolStripMenuItem.Image = Nothing
        'Me.ItemHiToolStripMenuItem.Image = Nothing
        'Me.ImportExportToolStripMenuItem.Image = Nothing
        'Me.UploadItemToolStripMenuItem.Image = Nothing
        'Me.ExportArticleXLSReportToolStripMenuItem.Image = Nothing
        'Me.ExportArticleHierarchyToolStripMenuItem.Image = Nothing
        'Me.ManualPromotionToolStripMenuItem.Image = Nothing
        'Me.SiteDetailsToolStripMenuItem.Image = Nothing
        'Me.StockInToolStripMenuItem.Image = Nothing
        'Me.StockOutToolStripMenuItem.Image = Nothing
        'Me.SupplierToolStripMenuItem.Image = Nothing
        'Me.TaxToolStripMenuItem.Image = Nothing
        'Me.TenderToolStripMenuItem.Image = Nothing
        'Me.UserToolStripMenuItem.Image = Nothing
        'Me.Transaction.Image = Nothing
        'Me.NewToolStripMenuItem.Image = Nothing
        'Me.NewCashMemo.Image = Nothing
        'Me.SearchEditCashMemo.Image = Nothing
        'Me.BirthListToolStripMenuItem.Image = Nothing
        'Me.BirthListNew.Image = Nothing
        'Me.BirthListSales.Image = Nothing
        'Me.BirthSearchEdit.Image = Nothing
        'Me.OpenToolStripMenuItem.Image = Nothing
        'Me.SalesOrderCreation.Image = Nothing
        'Me.SalesOrderUpdation.Image = Nothing
        'Me.SalesOrderCancelation.Image = Nothing
        'Me.SalesOrderBookingToolStripMenuItem.Image = Nothing
        'Me.SaleOrderEditBookingToolStripMenuItem.Image = Nothing
        'Me.QuotationToolStripMenuItem.Image = Nothing
        'Me.QuotationCreaToolStripMenuItem.Image = Nothing
        'Me.UpdateQuotationToolStripMenuItem.Image = Nothing
        'Me.CancelQuotationToolStripMenuItem.Image = Nothing
        'Me.Reprint.Image = Nothing
        'Me.BirthlistItems.Image = Nothing
        'Me.BirthlistsummaryToolStripMenuItem.Image = Nothing
        'Me.BirthlistSalesInvoiceToolStripMenuItem.Image = Nothing
        'Me.ReprintMenuItem.Image = Nothing
        'Me.BirthListMenuItem.Image = Nothing
        'Me.SalesOrderMenuItem.Image = Nothing
        'Me.CashMemoMenuItem.Image = Nothing
        'Me.ReturnCashMemoMenuItem.Image = Nothing
        'Me.OutboundDeliveryMenuItem.Image = Nothing
        'Me.ChangeTenderModeMenuItem.Image = Nothing
        'Me.ChangeCheckDueDateToolStripMenuItem.Image = Nothing
        'Me.FastCashMemoToolStripMenuItem.Image = Nothing
        'Me.CreditSaleAdjToolStripMenuItem.Image = Nothing
        'Me.RecordProductionToolStripMenuItem.Image = Nothing
        'Me.PettyCash.Image = Nothing
        'Me.NewVoucherEntryToolStripMenuItem.Image = Nothing
        'Me.ViewVocuherToolStripMenuItem.Image = Nothing
        'Me.ToolsToolStripMenuItem.Image = Nothing
        'Me.DatabseConnection.Image = Nothing
        'Me.LocalConnectionToolStripMenuItem.Image = Nothing
        'Me.OnlineConnectionToolStripMenuItem.Image = Nothing
        'Me.DefaultConfigMenu.Image = Nothing
        'Me.KeyBoardToolStripMenuItem.Image = Nothing
        'Me.HardwareSetupToolStripMenuItem.Image = Nothing
        'Me.POSDeviceSettingsMenuItem.Image = Nothing
        'Me.PrinterDocumentMapToolStripMenuItem.Image = Nothing
        'Me.PrintingToolStripMenuItem.Image = Nothing
        'Me.SetTerminalToolStripMenuItem.Image = Nothing
        'Me.SyncReportMenuItem.Image = Nothing
        'Me.SyncReportToolStripMenuItem.Image = Nothing
        'Me.SyncSettingToolStripMenuItem.Image = Nothing
        'Me.ReportsToolStripMenuItem.Image = Nothing
        'Me.AllReportToolStripMenuItem1.Image = Nothing
        'Me.DirectReportToolStripMenuItem.Image = Nothing
        'Me.DataArchiveToolStripMenuItem.Image = Nothing
        'Me.SchedulerToolStripMenuItem.Image = Nothing
        'Me.DayCloseReportToolStripMenuItem.Image = Nothing
        'Me.ProcSchedulerToolStripMenuItem.Image = Nothing
        'Me.HelpMenu.Image = Nothing
        'Me.ContentsToolStripMenuItem.Image = Nothing
        'Me.IndexToolStripMenuItem.Image = Nothing
        'Me.SearchToolStripMenuItem.Image = Nothing
        'Me.ToolStripSeparator8.Image = Nothing
        'Me.ProductInfoMenuItem.Image = Nothing
        'Me.AboutToolStripMenuItem.Image = Nothing
        'Me.LogoutToolStripMenuItem.Image = Nothing
        'Me.ExitToolStripMenuItem.Image = Nothing
        'Me.ToolStripMenuItem1.Image = Nothing
        'KDSToolStripMenuItem.Image = Nothing
        'ExpidiorToolStripMenuItem.Image = Nothing
        'AddEditViewTickitsToolStripMenuItem.Image = Nothing
        'SetPrepStationToolStripMenuItem.Image = Nothing
        'StockTakeToolStripMenuItem.Image = Nothing
        'MembershipToolStripMenuItem.Image = Nothing
        'CformsToolStripMenuItem.Image = Nothing
        'PendingCformsToolStripMenuItem.Image = Nothing
        'EditCformsToolStripMenuItem.Image = Nothing
        'ExecuteSyncToolStripMenuItem.Image = Nothing
        'DineInToolStripMenuItem.Image = Nothing
        'HomeDeliveryToolStripMenuItem.Image = Nothing
        Me.DayOpenMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.DayCloseToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.TillOpen.Margin = New Padding(0, 3, 0, 3)
        Me.ShiftOpen.Margin = New Padding(0, 3, 0, 3)
        Me.TillClose.Margin = New Padding(0, 3, 0, 3)
        Me.ShiftClose.Margin = New Padding(0, 3, 0, 3)
        Me.CashMemo.Margin = New Padding(0, 3, 0, 3)
        Me.BirthList.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrder.Margin = New Padding(0, 3, 0, 3)
        Me.StockCheck.Margin = New Padding(0, 3, 0, 3)
        Me.WindowsMenu.Margin = New Padding(0, 3, 0, 3)
        Me.BarcodePrintToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.BarcodePrintChildToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.LabelPrintToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CharacteristicsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CustomerToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.NewCustomer.Margin = New Padding(0, 3, 0, 3)
        Me.NewLoyaltyCustomer.Margin = New Padding(0, 3, 0, 3)
        Me.SearchEdit.Margin = New Padding(0, 3, 0, 3)
        Me.SearchCLPCustomer.Margin = New Padding(0, 3, 0, 3)
        Me.ViewOrderDetailsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ItemToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ItemMasterToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ItemHiToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ImportExportToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.UploadItemToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ExportArticleXLSReportToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ExportArticleHierarchyToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ManualPromotionToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SiteDetailsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.StockInToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.StockOutToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SupplierToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.TaxToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.TenderToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CreateEditUserToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.Transaction.Margin = New Padding(0, 3, 0, 3)
        Me.NewToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.NewCashMemo.Margin = New Padding(0, 3, 0, 3)
        Me.SearchEditCashMemo.Margin = New Padding(0, 3, 0, 3)
        Me.BirthListToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.BirthListNew.Margin = New Padding(0, 3, 0, 3)
        Me.BirthListSales.Margin = New Padding(0, 3, 0, 3)
        Me.BirthSearchEdit.Margin = New Padding(0, 3, 0, 3)
        Me.OpenToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrderCreation.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrderUpdation.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrderCancelation.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrderBookingToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SaleOrderEditBookingToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.QuotationToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.QuotationCreaToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.UpdateQuotationToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CancelQuotationToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.Reprint.Margin = New Padding(0, 3, 0, 3)
        Me.BirthlistItems.Margin = New Padding(0, 3, 0, 3)
        Me.BirthlistsummaryToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.BirthlistSalesInvoiceToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ReprintMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.BirthListMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SalesOrderMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CashMemoMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ReturnCashMemoMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.OutboundDeliveryMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ChangeTenderModeMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ChangeCheckDueDateToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.FastCashMemoToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.CreditSaleAdjToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.RecordProductionToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.PettyCash.Margin = New Padding(0, 3, 0, 3)
        Me.NewVoucherEntryToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ViewVocuherToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ToolsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.DatabseConnection.Margin = New Padding(0, 3, 0, 3)
        Me.LocalConnectionToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.OnlineConnectionToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.DefaultConfigMenu.Margin = New Padding(0, 3, 0, 3)
        Me.KeyBoardToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.HardwareSetupToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.POSDeviceSettingsMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.PrinterDocumentMapToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.PrintingToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SetTerminalToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SyncReportMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SyncReportToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SyncSettingToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ReportsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.AllReportToolStripMenuItem1.Margin = New Padding(0, 3, 0, 3)
        Me.DirectReportToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.DataArchiveToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SchedulerToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.DayCloseReportToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ProcSchedulerToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.HelpMenu.Margin = New Padding(0, 3, 0, 3)
        Me.ContentsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.IndexToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.SearchToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ToolStripSeparator8.Margin = New Padding(0, 3, 0, 3)
        Me.ProductInfoMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.AboutToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.LogoutToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ExitToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        Me.ToolStripMenuItem1.Margin = New Padding(0, 3, 0, 3)
        KDSToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        ExpidiorToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        AddEditViewTickitsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        SetPrepStationToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        StockTakeToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        MembershipToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        CformsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        PendingCformsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        EditCformsToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        ExecuteSyncToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        DineInToolStripMenuItem.Margin = New Padding(0, 3, 0, 3)
        rbnlblSiteCodeText.ForeColorOuter = Color.FromArgb(177, 227, 253)
        rbnlblSiteCodeText.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        rbnlblSiteCodeValue.ForeColorOuter = Color.FromArgb(177, 227, 253)
        rbnlblSiteCodeValue.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        rbnlblSiteNameText.ForeColorOuter = Color.FromArgb(177, 227, 253)
        rbnlblSiteNameText.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        rbnlblSiteNameValue.ForeColorOuter = Color.FromArgb(177, 227, 253)
        rbnlblSiteNameValue.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    End Function


    Private Sub MembershipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MembershipToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmMembershipEnrollment
        Try
            If ChildForm.Name <> String.Empty Then
                ChildForm.pSearchCust = "SEARCH"
                ChildForm.Tag = "NEW"
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Dim bw As BackgroundWorker
    Public Delegate Sub PictureVisibilityDelegate(ByVal visibility As Integer)
    Dim ChangePictureVisibility As PictureVisibilityDelegate
    'Dim isClosed As Boolean = False
    Dim waitPopupMsg As frmSpecialPrompt
    Private Sub bw_DoWork(sender As Object, e As DoWorkEventArgs)
        Me.Invoke(ChangePictureVisibility, 0)
        If DataBaseAutoBackup() = True Then
            Me.Invoke(ChangePictureVisibility, 1)
            ShowMessage("DB Backup successfull on location  " + clsDefaultConfiguration.DatabaseBackupPath, "Information")
        Else
            Me.Invoke(ChangePictureVisibility, 1)
            ShowMessage("DB Backup failed try again..", "Information")
        End If
    End Sub
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        'Me.Invoke(ChangePictureVisibility, False)
    End Sub
    Public Sub ChangeVisibility(ByVal steps As Integer)
        If steps = 0 Then
            waitPopupMsg = DayCloseSyncProgress("Please wait while system is backing up the data...", getValueByKey("CLAE04"))
            Application.DoEvents()
        ElseIf steps = 1 Then
            waitPopupMsg.Close()
        End If
    End Sub

    Private Sub AutoDataBackUpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoDataBackUpToolStripMenuItem.Click
        bw = New BackgroundWorker
        bw.WorkerSupportsCancellation = True
        AddHandler bw.DoWork, AddressOf bw_DoWork
        ChangePictureVisibility = AddressOf ChangeVisibility
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
        ChangePictureVisibility = AddressOf ChangeVisibility
        If Not bw.IsBusy = True Then
            bw.RunWorkerAsync()
        End If
    End Sub

    Private Sub DineInToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DineInToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmTabCashMemo

        Try
            If ChildForm.Name <> String.Empty Then
                lblUserId.Visible = False
                lblLoggedIn.Visible = False
                lblTodayDate.Visible = False
                RibbonSeparator1.Visible = False
                RibbonSeparator2.Visible = False
                RibbonSeparator3.Visible = False
                AutoCancelTimer.Stop()
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub WindowsMenu_DropDownClosed(sender As Object, e As System.EventArgs) Handles WindowsMenu.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            WindowsMenu.ForeColor = Color.White
        End If

    End Sub

    Private Sub Transaction_DropDownClosed(sender As Object, e As System.EventArgs) Handles Transaction.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            Transaction.ForeColor = Color.White
        End If

    End Sub

    Private Sub PettyCash_DropDownClosed(sender As Object, e As System.EventArgs) Handles PettyCash.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            PettyCash.ForeColor = Color.White
        End If

    End Sub

    Private Sub TillClose_DropDownClosed(sender As Object, e As System.EventArgs) Handles TillClose.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            TillClose.ForeColor = Color.White
        End If

    End Sub

    Private Sub HelpMenu_DropDownClosed(sender As Object, e As System.EventArgs) Handles HelpMenu.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            HelpMenu.ForeColor = Color.White
        End If

    End Sub

    Private Sub LogoutToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles LogoutToolStripMenuItem.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            LogoutToolStripMenuItem.ForeColor = Color.White
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles ExitToolStripMenuItem.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            ExitToolStripMenuItem.ForeColor = Color.White
        End If

    End Sub

    Private Sub TicketingSystemToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles TicketingSystemToolStripMenuItem.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            TicketingSystemToolStripMenuItem.ForeColor = Color.White
        End If

    End Sub

    Private Sub WindowsMenu_DropDownOpened(sender As Object, e As System.EventArgs) Handles WindowsMenu.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            WindowsMenu.ForeColor = Color.Black
        End If

    End Sub

    Private Sub Transaction_DropDownOpened(sender As Object, e As System.EventArgs) Handles Transaction.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            Transaction.ForeColor = Color.Black
        End If

    End Sub

    Private Sub TicketingSystemToolStripMenuItem_DropDownOpened(sender As Object, e As System.EventArgs) Handles TicketingSystemToolStripMenuItem.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            TicketingSystemToolStripMenuItem.ForeColor = Color.Black
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_DropDownOpened(sender As Object, e As System.EventArgs) Handles ExitToolStripMenuItem.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            ExitToolStripMenuItem.ForeColor = Color.Black
        End If

    End Sub

    Private Sub LogoutToolStripMenuItem_DropDownOpened(sender As Object, e As System.EventArgs) Handles LogoutToolStripMenuItem.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            LogoutToolStripMenuItem.ForeColor = Color.Black
        End If

    End Sub

    Private Sub HelpMenu_DropDownOpened(sender As Object, e As System.EventArgs) Handles HelpMenu.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            HelpMenu.ForeColor = Color.Black
        End If

    End Sub

    Private Sub PettyCash_DropDownOpened(sender As Object, e As System.EventArgs) Handles PettyCash.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            PettyCash.ForeColor = Color.Black
        End If

    End Sub


    Private Sub ToolsToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles ToolsToolStripMenuItem.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            ToolsToolStripMenuItem.ForeColor = Color.White
        End If

    End Sub

    Private Sub ToolsToolStripMenuItem_DropDownOpened(sender As Object, e As System.EventArgs) Handles ToolsToolStripMenuItem.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            ToolsToolStripMenuItem.ForeColor = Color.Black
        End If

    End Sub
    Private Sub AutoCancelTimer_Tick(ByVal sender As Object, e As EventArgs)
        Dim dtReservDetails As New DataTable
        Dim objRes As New clsReservation
        dtReservDetails = objRes.GetReservedFullData(clsAdmin.SiteCode)
        If Not dtReservDetails Is Nothing AndAlso dtReservDetails.Rows.Count > 0 Then
            Dim CurrentDate As Date = DateTime.Now.ToShortDateString
            Dim CurrentTime As DateTime = DateTime.Now.ToShortTimeString
            For Each drRes As DataRow In dtReservDetails.Rows
                Dim ReservedTime As DateTime = drRes("FromTime")
                Dim ReservedDate As DateTime = drRes("Date").Date
                Dim ReservedExpiryTime As DateTime
                Dim ExpiryTime As Integer = clsDefaultConfiguration.ExpiryTime
                ReservedExpiryTime = ReservedTime.AddMinutes(ExpiryTime).ToShortTimeString

                If ReservedDate = CurrentDate Then
                    If ReservedExpiryTime = CurrentTime Then
                        AutoCancelTimer.Stop()
                        Dim objAutoCancel As New frmAutoCancel
                        objAutoCancel.FromTime = DateTime.Now.Date & " " & ReservedTime
                        objAutoCancel.CustNo = drRes("CustomerNo")
                        objAutoCancel.PhoneNo = drRes("MobileNo")
                        objAutoCancel.ReservationId = drRes("ReservationId")
                        objAutoCancel.ListOfTables = drRes("TableList")
                        Me.DialogResult = objAutoCancel.ShowDialog()
                        If Me.DialogResult = Windows.Forms.DialogResult.OK OrElse Me.DialogResult = Windows.Forms.DialogResult.Retry OrElse Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
                            AutoCancelTimer.Start()
                            Exit For
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub HomeDeliveryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HomeDeliveryToolStripMenuItem.Click
        Try
            Dim objHD As New frmHD(False)
            objHD.ShowDialog()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub HomeDeliveryNotification()
        Dim TimeInterval = clsDefaultConfiguration.TicketingSystemPopupInterval
        Dim objCommon As New clsCommon
        '-- added on 21 dec 2016 - ashma
        Dim HDNotification = objCommon.GetHomeDeliveryNotification()
        If HDNotification.Rows.Count > 0 Then
            For rowHD = 0 To HDNotification.Rows.Count - 1
                'MobNo = MobNo + "," + DeptMobNo.Rows(MobileRow)(0).ToString         
                popupNotifier1.TitleText = "Notification Title"
                popupNotifier1.ContentText = "Kindly check the order for new updates :-" + HDNotification.Rows(rowHD)(1).ToString

                popupNotifier1.ShowCloseButton = True
                popupNotifier1.ShowOptionsButton = True
                popupNotifier1.ShowGrip = True
                popupNotifier1.Delay = TimeInterval * 1000
                popupNotifier1.AnimationInterval = 10
                popupNotifier1.AnimationDuration = 1000
                popupNotifier1.TitlePadding = New Padding(0)
                popupNotifier1.ContentPadding = New Padding(0)
                popupNotifier1.ImagePadding = New Padding(0)
                popupNotifier1.Scroll = True
                ' popupNotifier1.Image = My.Resources._157_GetPermission_48x48_72
                popupNotifier1.Popup()
                objCommon.UpdateHomeDeliveryNotification(clsAdmin.SiteCode, HDNotification.Rows(rowHD)(1).ToString)
            Next
        End If
    End Sub

    Private Sub popupNotifier1_Click(sender As System.Object, e As System.EventArgs) Handles popupNotifier1.Click
        '' added By Ketan For E- Commerce add transection Condition :- at time of issue in JK  
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "DispchNDelivery") = True Then
            Dim objHD As New frmHD(False)
            objHD.ShowDialog()
        End If
    End Sub

    Private Sub HotelReservationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HotelReservationToolStripMenuItem.Click
        'Dim ChildForm As New Spectrum.frmHostCreateReservation
        'Try
        '    If ChildForm.Name <> String.Empty Then
        '        ShowChildForm(ChildForm, False)
        '    End If
        'Catch ex As Exception
        '    ChildForm.Close()
        'End Try
    End Sub

    Private Sub CreateReservationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateReservationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmHostCreateReservation
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ViewReservationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewReservationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmHostViewReservation
        Try
            'ChildForm.ShowDialog()
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'code added for jk sprint 28

    'code added on 14-08-2017
    Private Sub TeamViewer_Click(sender As System.Object, e As System.EventArgs)

        Try
            Dim dt As New DataTable
            Dim TeamViewerPath As String
            TeamViewerPath = ""
            dt.Columns.Add("Name")
            dt.Columns.Add("InstallLocation")
            dt.Columns.Add("publisher")
            dt.Columns.Add("uninstallString")

            Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
            Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
                For Each skName As String In rk.GetSubKeyNames()
                    Using sk As RegistryKey = rk.OpenSubKey(skName)
                        ' we have many attributes other than these which are useful. 
                        '  Console.WriteLine(sk.GetValue("DisplayName") + " " + sk.GetValue("DisplayVersion"))
                        If Not String.IsNullOrEmpty(sk.GetValue("Publisher")) Then

                            If sk.GetValue("Publisher").Equals("TeamViewer") Then
                                dt.Rows.Add(sk.GetValue("DisplayName"), sk.GetValue("InstallLocation"), sk.GetValue("Publisher"), sk.GetValue("InstallSource"))
                            End If
                        End If


                    End Using
                Next
            End Using

            If dt IsNot Nothing And dt.Rows.Count = 1 Then
                ' TeamViewerPath  = table.Rows[1]["mycolumn1"].ToString();
                TeamViewerPath = dt.Rows(0)("InstallLocation").ToString
                TeamViewerPath = TeamViewerPath.Concat(TeamViewerPath, "\Teamviewer.exe")
            End If

            If Not String.IsNullOrEmpty(TeamViewerPath) Then
                Process.Start(TeamViewerPath)
            Else
                ShowMessage(True, "Please Install TeamViewer", "Info message")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AmmyAdmin_Click(sender As System.Object, e As System.EventArgs)

        'Try
        '    Dim AmmyyAdminPath As String

        '    AmmyyAdminPath = clsDefaultConfiguration.EnableAmmyyAdmin
        '    AmmyyAdminPath = AmmyyAdminPath.Concat(AmmyyAdminPath, "\AmmyyAdmin.exe")
        '    Try
        '        Process.Start(AmmyyAdminPath)
        '    Catch ex As Exception
        '        ShowMessage(True, "Setup File not found at given location or setup file name not matching AmmyyAdmin.exe ", "Message")
        '    End Try


        'Catch ex As Exception

        'End Try
        Try
            Dim AmmyyAdminPath As String
            AmmyyAdminPath = clsDefaultConfiguration.EnableAmmyyAdmin
            Dim szFiles() As String, i As UInteger
            szFiles = Directory.GetFiles(AmmyyAdminPath, "*.exe", SearchOption.AllDirectories)

            If szFiles.Length > 0 Then
                AmmyyAdminPath = szFiles(0).ToString

            Else
                AmmyyAdminPath = ""
            End If

            Try
                If Not String.IsNullOrEmpty(AmmyyAdminPath) Then
                    Process.Start(AmmyyAdminPath)
                Else
                    ShowMessage(True, "Setup File not found at given location", "Info message")
                End If

            Catch ex As Exception
                LogException(ex)
                ShowMessage(True, "Setup File not found at given location", "Info message")
            End Try

        Catch ex As Exception
            ShowMessage(True, "Setup File not found at given location", "Info message")
            LogException(ex)
        End Try
    End Sub

    Private Sub BackOffice_Link(sender As System.Object, e As System.EventArgs)
        Try
            Dim BackOfficePath As String
            BackOfficePath = clsDefaultConfiguration.OpenBackOfficeFromFO
            Try
                If BackOfficePath IsNot Nothing AndAlso Not String.IsNullOrEmpty(BackOfficePath.ToString()) Then
                    Process.Start(BackOfficePath)
                Else
                    ShowMessage(True, "Back Office Link is Not Available", "Info message")

                End If
            Catch ex As Exception
                ShowMessage(True, "Provided Link is Incorrect", "Info message")
            End Try

        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    Private Sub ReportsToolsStripOuterMenuItem_DropDownClosed(sender As System.Object, e As System.EventArgs) Handles ReportsToolsStripOuterMenuItem.DropDownClosed
        If clsDefaultConfiguration.IsTablet Then
            ReportsToolsStripOuterMenuItem.ForeColor = Color.White
        End If
    End Sub

    Private Sub ReportsToolsStripOuterMenuItem_DropDownOpened(sender As System.Object, e As System.EventArgs) Handles ReportsToolsStripOuterMenuItem.DropDownOpened
        If clsDefaultConfiguration.IsTablet Then
            ReportsToolsStripOuterMenuItem.ForeColor = Color.Black
        End If
    End Sub


    Private Sub TableManagmentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TableManagmentToolStripMenuItem.Click '##
        Dim ChildForm As New Spectrum.frmTableManagment
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    ''code added by vipul restrict billing from spectrum mettler till
    Private Function IsMettlerPaymentTill() As Boolean
        Try
            If clsDefaultConfiguration.SpectrumAsMettler Then
                If Not clsDefaultConfiguration.SpectrumMettlerPaymentTill.Contains(clsAdmin.TerminalID) Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 14-03-2018 for spectrum New development -spectrumLite
    Private Sub PosTabCreationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PosTabCreationToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmPosTabCreation
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    'Private Sub DefineComboToolStripMenuItem_Click(sender As Object, e As EventArgs)  '##
    '    Dim ChildForm As New Spectrum.FrmDefineCombo
    '    Try
    '        If ChildForm.Name <> String.Empty Then
    '            ShowChildForm(ChildForm, False)
    '        End If
    '    Catch ex As Exception
    '        ChildForm.Close()
    '    End Try
    'End Sub

    'Private Sub DefineComboToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles DefineComboToolStripMenuItem.Click

    'End Sub

    Private Sub DefineComboToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DefineComboToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.FrmDefineCombo
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ViewEnquiryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewEnquiryToolStripMenuItem.Click, ViewEnquiryToolStripMenuItem.Click

        Try
            Dim objmembership As New frmViewMembership(False)
            objmembership.IsEnquiry = 1
            objmembership.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ViewMemberShipToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewMemberShipToolStripMenuItem.Click
        Try
            Dim objmembership As New frmViewMembership(False)
            objmembership.IsEnquiry = 0
            objmembership.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LabelPrintToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LabelPrintToolStripMenuItem.Click
        'code added by vipul for printing spectrum mettler Kot


        Dim ChildForm As New frmGenerateBarcode()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub BarcodePrintChildToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BarcodePrintChildToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmBarcode()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub TrainingVideosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TrainingVideosToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmDepartmentsVideosAndAttachements
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    'code added by ashma for Material Import Export Functionality under Item Master - 25 april 2018
    Private Sub ArticleMaterialToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ArticleMaterialToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmImportExportItem(CInt(Spectrum.BO.CommonFunc.enumImportExportItem.ImportExportMaterial))
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub DefineKitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DefineKitToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.FrmDefineKit
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    'ashma 10 may 2018 for Shift Management Functionality
    Private Sub ShiftManagementToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShiftManagementToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmShiftManagement
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub OrderPackagingScreenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OrderPackagingScreenToolStripMenuItem.Click
        Dim ChildForm As New frmOrderPackageScreen
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub PrepStationToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles PrepStationToolStripMenuItem2.Click
        Dim ChildForm As New Spectrum.frmPrepStationTemplate
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub MDISpectrum_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Added by vipin 
        If clsDefaultConfiguration.TillOpenDone Then
            If clsDefaultConfiguration.DefaultModuleAfterLogin = "CM" AndAlso CheckAuthorisation(clsAdmin.UserCode, "Billing") = True Then
                CashMemo_Click(sender, New EventArgs())
            ElseIf clsDefaultConfiguration.DefaultModuleAfterLogin = "TCM" AndAlso CheckAuthorisation(clsAdmin.UserCode, "Billing") = True Then
                FastCashMemoToolStripMenuItem_Click(sender, New EventArgs())
            ElseIf clsDefaultConfiguration.DefaultModuleAfterLogin = "Dine In" AndAlso CheckAuthorisationForTran(clsAdmin.SiteCode, "DineIn") = True Then
                DineInToolStripMenuItem_Click(sender, New EventArgs())
            ElseIf clsDefaultConfiguration.DefaultModuleAfterLogin = "SO" AndAlso CheckAuthorisation(clsAdmin.UserCode, "SOCreation") = True Then
                SalesOrder_Click(sender, New EventArgs())
            End If
        End If
    End Sub

    Private Sub SplitBIllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SplitBIllToolStripMenuItem.Click
        PrintTransType = "CashMemo"
        Dim ChildForm As New FrmSplitBill
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmChangeUserPassword()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub CreateEditUserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreateEditUserToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.BO.frmUser()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub

    Private Sub SeatingAreaWiseArticlePriceMappingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SeatingAreaWiseArticlePriceMappingToolStripMenuItem.Click
        Dim ChildForm As New Spectrum.frmSeatingAreaWiseArticlePriceMapping()
        Try
            If ChildForm.Name <> String.Empty Then
                ShowChildForm(ChildForm, False)
            End If
        Catch ex As Exception
            ChildForm.Close()
        End Try
    End Sub
    Private Sub MenuOnlineOrders_Click(sender As Object, e As EventArgs) Handles MenuOnlineOrders.Click 'vipin
        Dim objHD As New frmonlineOrders(False)
        Try
            objHD.ShowDialog()
            objHD.BringToFront()
            PopupOnlineOrders.Hide()
        Catch ex As Exception
            objHD.Close()
        End Try
    End Sub

    Private Sub CallWebServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CallWebServicesToolStripMenuItem.Click
        Try
            Dim ChildForm As New Spectrum.frmCallWS
            Try
                ChildForm.ShowDialog()
                'If ChildForm.Name <> String.Empty Then
                '    ShowChildForm(ChildForm, False)
                'End If
            Catch ex As Exception
                ChildForm.Close()
            End Try
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
'added by khusrao adil on 13-06-2018 for Flicker isssue resolution 
Friend Module NativeWinAPI
    Friend ReadOnly GWL_EXSTYLE As Integer = -20
    Friend ReadOnly WS_EX_COMPOSITED As Integer = &H2000000
    <DllImport("user32")>
    Friend Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer

    End Function
    <DllImport("user32")>
    Friend Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

    End Function
End Module