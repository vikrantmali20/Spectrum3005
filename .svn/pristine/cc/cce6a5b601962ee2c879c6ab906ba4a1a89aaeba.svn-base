Imports Microsoft.PointOfService
Imports System.IO
Imports System.Drawing.Printing
Imports System.Management
Imports SpectrumBL
Imports System.Windows.Forms
Imports System.Text
Imports SpectrumPrint
Imports System.Data
Imports System.Data.DataTable


Public Class frmPOSDeviceProfile
    'Private WithEvents POSDeviceExplorer As New PosExplorer
    'Dim POSDeviceExplorer As New PosExplorer
    Dim DevInfo As DeviceInfo
    Dim MyPrinter As PosPrinter
    Dim MyDrawer As CashDrawer
    Dim MyScanner As Scanner
    Dim MyDisplay As LineDisplay
    Dim DAPOSDevice As New POSDBDataSetTableAdapters.PosDeviceProfileTableAdapter
    Dim DTPOSDevice As New POSDBDataSet.PosDeviceProfileDataTable
    Dim RecFound As Boolean
    Dim currTab As Integer = 0
    Dim MyMsr As Msr
    Private WithEvents printDocument1 As New PrintDocument()

#Region "Printer Functions"

    Private Sub cmbPrinter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrinter.SelectedIndexChanged
        Try
            cmbPrinterName.Text = String.Empty

            If cmbPrinter.Text = "OPOS" Then
                GetDevice(tcPOSDevice.SelectedTab.Tag)
                btnOpenDevicePrinter.Enabled = True
                btnCloseDevicePrinter.Enabled = True
            Else
                GetSystemDevice("Printer")
                btnOpenDevicePrinter.Enabled = False
                btnCloseDevicePrinter.Enabled = False
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbPrinterName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrinterName.SelectedIndexChanged
        If cmbPrinter.Text = "OPOS" Then
            If cmbPrinterName.Text <> "" Then
                Try
                    DevInfo = explorer.GetDevice(DeviceType.PosPrinter, "POSPRINTER")
                    If Not DevInfo Is Nothing Then
                        MyPrinter = explorer.CreateInstance(DevInfo)
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try
            End If
        End If
    End Sub

    Private Sub btnOpenDevicePrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDevicePrinter.Click
        Try
            If cmbPrinterName.Text <> "" Then
                If cmbPrinter.Text = "OPOS" Then
                    If lblStatePrinter.Tag <> "Opened" Then
                        Try
                            'ShowMessage(cmbPrinterName.Text)
                            Dim device2 As DeviceInfo = explorer.GetDevice("PosPrinter", cmbPrinterName.Text)
                            'ShowMessage(device2.LogicalNames(0).ToString)
                            If Not device2 Is Nothing Then
                                MyPrinter = explorer.CreateInstance(device2)
                                MyPrinter.Open()
                                MyPrinter.Claim(100)
                                MyPrinter.DeviceEnabled = True
                                MyPrinter.AsyncMode = False
                                lblStatePrinter.Text = getValueByKey("posdeviceopened")
                                lblStatePrinter.Tag = "Opened"
                            End If
                        Catch ex As Exception
                            ShowMessage(ex.Message, getValueByKey("CLAE05"))
                            LogException(ex)
                        End Try

                        DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")

                        If DTPOSDevice.Rows.Count > 0 Then
                            AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, False)
                            btnSavePrinter.Enabled = True
                        Else
                            AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, False)
                            btnSavePrinter.Enabled = True
                        End If
                    End If
                End If
            Else
                ShowMessage(getValueByKey("PDP004"), "PDP004 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please select a Printer device name")
            End If

        Catch ex As Exception
            lblStatePrinter.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnPrintSlip_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSlip.Click
        Try
            If cmbPrinter.Text = "OPOS" Then
                If lblStatePrinter.Tag = "Opened" Then
                    MyPrinter.PrintNormal(PrinterStation.Receipt, "This is a test page" + vbCrLf + "This is a test page" + vbCrLf + "This is a test page" + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf)

                    'MyPrinter.PrintNormal(PrinterStation.Receipt, getvaluebykey("PDP007") + vbCrLf + getvaluebykey("PDP007") + vbCrLf + getvaluebykey("PDP007") + vbCrLf)
                    Try
                        MyPrinter.CutPaper(100)
                    Catch ex As Exception

                    End Try


                    If DTPOSDevice.Select("DriverType='OPOS'").Length > 0 Then
                        AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                        btnSavePrinter.Enabled = True
                    Else
                        AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                        btnSavePrinter.Enabled = True
                    End If

                    'If DTPOSDevice.Rows.Count > 0 Then
                    '    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    '    btnSavePrinter.Enabled = True
                    'End If

                End If
            ElseIf cmbPrinter.Text = "Other" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()

                ' printDocument1.Print()

                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")

                If DTPOSDevice.Select("DriverType='Other'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Other1" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()

                ' printDocument1.Print()

                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Other1'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If
                'code added for adding extra printer by vipul
            ElseIf cmbPrinter.Text = "Printer2" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer2'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer3" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer3'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer4" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer4'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer5" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer5'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer6" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer6'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer7" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer7'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer8" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer8'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer9" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer9'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer10" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer10'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer11" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer11'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer12" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer12'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer13" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()


                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer13'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer14" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer14'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            ElseIf cmbPrinter.Text = "Printer15" Then
                printDocument1.PrinterSettings.PrinterName = cmbPrinterName.Text
                Dim dlgPrintPreview As New PrintPreviewDialog
                dlgPrintPreview.Document = printDocument1
                dlgPrintPreview.WindowState = FormWindowState.Maximized
                dlgPrintPreview.ShowDialog()



                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                If DTPOSDevice.Select("DriverType='Printer15'").Length > 0 Then
                    AddRecords("Update", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                Else
                    AddRecords("New", "Printer", "Printer", cmbPrinterName.Text, cmbPrinter.Text, True)
                    btnSavePrinter.Enabled = True
                End If

            End If
            SetPrinterInSetting()
        Catch ex As Exception
            ShowMessage(getValueByKey("BL015"), "BL015 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub SetPrinterInSetting()
        Try
            If cmbPrinter.Text = "OPOS" Then
                'My.Settings.OPOSPrinter = cmbPrinterName.Text.Trim
                CreateSpectrumParamFile("OPOSPrinter", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Other" Then
                'My.Settings.OtherPrinter = cmbPrinterName.Text.Trim
                CreateSpectrumParamFile("OtherPrinter", cmbPrinterName.Text.Trim)
                gPrinterName = cmbPrinterName.Text.Trim
            ElseIf cmbPrinter.Text = "Other1" Then
                'My.Settings.OtherPrinter1 = cmbPrinterName.Text.Trim
                CreateSpectrumParamFile("OtherPrinter1", cmbPrinterName.Text.Trim)
                'code added for adding extra printer by vipul
            ElseIf cmbPrinter.Text = "Printer2" Then

                CreateSpectrumParamFile("OtherPrinter2", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer3" Then

                CreateSpectrumParamFile("OtherPrinter3", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer4" Then

                CreateSpectrumParamFile("OtherPrinter4", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer5" Then

                CreateSpectrumParamFile("OtherPrinter5", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer6" Then

                CreateSpectrumParamFile("OtherPrinter6", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer7" Then

                CreateSpectrumParamFile("OtherPrinter7", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer8" Then

                CreateSpectrumParamFile("OtherPrinter8", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer9" Then

                CreateSpectrumParamFile("OtherPrinter9", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer10" Then

                CreateSpectrumParamFile("OtherPrinter10", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer11" Then

                CreateSpectrumParamFile("OtherPrinter11", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer12" Then

                CreateSpectrumParamFile("OtherPrinter12", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer13" Then

                CreateSpectrumParamFile("OtherPrinter13", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer14" Then

                CreateSpectrumParamFile("OtherPrinter14", cmbPrinterName.Text.Trim)
            ElseIf cmbPrinter.Text = "Printer15" Then

                CreateSpectrumParamFile("OtherPrinter15", cmbPrinterName.Text.Trim)

            End If
            My.Settings.Save()

            'lblCurrWinPrnName.Text = My.Settings.OtherPrinter.ToString
            'lblOtherWinPrnName.Text = My.Settings.OtherPrinter1.ToString
            'lblCurrOPOSPrnName.Text = My.Settings.OPOSPrinter.ToString

            lblCurrWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter1")
            lblCurrOPOSPrnName.Text = ReadSpectrumParamFile("OPOSPrinter")
            'code added for adding extra printer by vipul
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter2")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter3")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter4")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter5")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter6")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter7")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter8")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter9")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter10")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter11")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter12")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter13")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter14")
            lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter15")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        
    End Sub

    Private Sub btnCloseDevicePrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDevicePrinter.Click
        Try
            If cmbPrinterName.Text <> "" Then
                If cmbPrinter.Text = "OPOS" Then
                    If lblStatePrinter.Tag <> "Closed" Then
                        MyPrinter.DeviceEnabled = False
                        MyPrinter.Release()
                        MyPrinter.Close()
                        lblStatePrinter.Text = lblstateDisplay.Text = getValueByKey("posdeviceclosed")
                        lblStatePrinter.Tag = "Closed"
                    End If
                End If
            Else
                ShowMessage(getValueByKey("PDP002"), "PDP002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Device not open.")
            End If

        Catch ex As Exception
            lblStatePrinter.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnSavePrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePrinter.Click
        Try
            'code added for KOT printer mapping by vipul
            Printermapping(DTPOSDevice)
            SetPrinterInSetting()

            ShowMessage(getValueByKey("PDP003"), "PDP003 - " & getValueByKey("CLAE04"))

            Dim sp As New SpectrumPrint.My.MySettings
            sp.POSPrinter = cmbPrinterName.Text.Trim
            sp.Save()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        

        'ShowMessage(" Printer Setting Saved Successfully", ShowMessageStyle.Information)
    End Sub

#End Region

#Region "Drawer Functions"

    Private Sub cmbDrawer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDrawer.SelectedIndexChanged
        GetDevice(tcPOSDevice.SelectedTab.Tag)
    End Sub

    Private Sub cmbDrawerName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDrawerName.SelectedIndexChanged
        If cmbDrawerName.Text <> "" Then
            Try
                'DevInfo = explorer.GetDevice(DeviceType.CashDrawer, cmbDrawerName.Text)
                'If Not DevInfo Is Nothing Then
                '    MyDrawer = explorer.CreateInstance(DevInfo)
                '    lblDisplCashDrawar.Text = cmbDrawerName.Text
                'End If

                For Each Device As Object In explorer.GetDevices()
                    Dim POSDType = Device.Type.ToString
                    Dim POSSoName = Device.SoName.ToString
                    If cmbDrawerName.Text.Equals(POSSoName) Then
                        MyDrawer = explorer.CreateInstance(Device)
                        lblDisplCashDrawar.Text = cmbDrawerName.Text
                        Exit For
                    End If
                Next
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If
    End Sub

    Private Sub btnOpenDeviceDrawer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDeviceDrawer.Click
        Try
            If cmbDrawerName.Text <> "" Then
                If lblStateDrawer.Tag <> "Opened" Then
                    MyDrawer.Open()
                    MyDrawer.Claim(1000)
                    MyDrawer.DeviceEnabled = True

                    lblStateDrawer.Tag = "Opened"
                    lblStateDrawer.Text = getValueByKey("posdeviceclosed")

                    DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Drawer")
                    If DTPOSDevice.Rows.Count > 0 Then
                        AddRecords("Update", "Drawer", "Drawer", cmbDrawerName.Text, cmbDrawer.Text, False)
                        btnSaveDrawer.Enabled = True
                    Else
                        AddRecords("New", "Drawer", "Drawer", cmbDrawerName.Text, cmbDrawer.Text, False)
                        btnSaveDrawer.Enabled = True
                    End If
                End If
            Else
                ShowMessage(getValueByKey("PDP001"), "PDP001 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please select the Drawer device name.")
            End If

        Catch ex As Exception
            lblStateDrawer.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnOpenDrawer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDrawer.Click
        Try
            If lblStateDrawer.Tag = "Opened" Then
                MyDrawer.OpenDrawer()
                MyDrawer.Release()
                MyDrawer.Close()

                If DTPOSDevice.Rows.Count > 0 Then
                    AddRecords("Update", "Drawer", "Drawer", cmbDrawerName.Text, cmbDrawer.Text, True)
                    btnSaveDrawer.Enabled = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSaveDrawer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDrawer.Click
        If cmbDrawer.Text = "OPOS" Then
            'My.Settings.Drawer = cmbDrawerName.Text.Trim
            CreateSpectrumParamFile("Drawer", cmbDrawerName.Text.Trim)
            My.Settings.Save()
            Dim sp As New SpectrumPrint.My.MySettings
            sp.Drawer = cmbDrawerName.Text.Trim
            sp.Save()
            clsAdmin.CashDrawerWithoutDriver = cmbDrawerName.Text.Trim
        End If
    End Sub

    Private Sub btnCloseDeviceDrawer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDeviceDrawer.Click
        Try
            If cmbDrawerName.Text <> "" Then
                If lblStateDrawer.Tag <> "Closed" Then
                    'MyDrawer.Close()
                    lblStateDrawer.Tag = "Closed"
                    lblStateDrawer.Text = getValueByKey("posdeviceclosed")
                End If
            Else
                ShowMessage(getValueByKey("PDP002"), "PDP002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Device not open.")
            End If

        Catch ex As Exception
            lblStateDrawer.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region

#Region "Line Display Functions"

    Private Sub cmbDisplayDevice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisplay.SelectedIndexChanged
        GetDevice(tcPOSDevice.SelectedTab.Tag)
    End Sub

    Private Sub cmbDisplayDeviceName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisplayName.SelectedIndexChanged
        If cmbDisplayName.Text <> "" Then
            Try
                DevInfo = explorer.GetDevice(DeviceType.LineDisplay, cmbDisplayName.Text)
                If Not DevInfo Is Nothing Then
                    MyDisplay = explorer.CreateInstance(DevInfo)
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If
    End Sub

    Private Sub btnOpenDeviceDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDeviceDisplay.Click
        Try
            If cmbDisplayName.Text <> "" Then
                If lblstateDisplay.Tag <> "Opened" Then
                    MyDisplay.Open()
                    MyDisplay.Claim(1000)
                    MyDisplay.DeviceEnabled = True

                    lblstateDisplay.Tag = "Opened"
                    lblstateDisplay.Text = getValueByKey("posdeviceopened")

                    DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Line Display")
                    If DTPOSDevice.Rows.Count > 0 Then
                        AddRecords("Update", "Line Display", "Line Display", cmbDisplayName.Text, cmbDisplay.Text, False)
                        btnSaveDisplay.Enabled = True
                    Else
                        AddRecords("New", "Line Display", "Line Display", cmbDisplayName.Text, cmbDisplay.Text, False)
                        btnSaveDisplay.Enabled = True
                    End If
                End If
            Else
                ShowMessage(getValueByKey("PDP005"), "PDP005 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            lblstateDisplay.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnShowMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowMessage.Click
        Try

            If lblstateDisplay.Tag = "Opened" Then
                MyDisplay.ClearText()
                MyDisplay.DisplayText(txtMessage.Text)
                If DTPOSDevice.Rows.Count > 0 Then
                    AddRecords("Update", "Line Display", "Line Display", cmbDisplayName.Text, cmbDisplay.Text, True)
                    btnSaveDisplay.Enabled = True
                End If
            Else
                ShowMessage(getValueByKey("PDP006"), "PDP006 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnCloseDeviceDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDeviceDisplay.Click
        Try
            If cmbDisplayName.Text <> "" Then
                If lblstateDisplay.Tag <> "Closed" Then
                    MyDisplay.Close()
                    lblstateDisplay.Tag = "Closed"
                    lblstateDisplay.Text = getValueByKey("posdeviceclosed")
                End If
            Else
                ShowMessage(getValueByKey("PDP002"), "PDP002 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            lblstateDisplay.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSaveDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDisplay.Click
        Try
            If cmbDisplay.Text = "OPOS" Then
                'My.Settings.LineDisplay = cmbDisplayName.Text.Trim
                CreateSpectrumParamFile("LineDisplay", cmbDisplayName.Text.Trim)
                Dim sp As New SpectrumPrint.My.MySettings
                sp.LineDisplay = cmbDisplayName.Text.Trim
                sp.Save()
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
       
    End Sub

#End Region

#Region "Scanner Functions"

    Private Sub cmbscanner_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbscanner.SelectedIndexChanged
        GetDevice(tcPOSDevice.SelectedTab.Tag)
    End Sub

    Private Sub cmbScannerName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbScannerName.SelectedIndexChanged
        If cmbScannerName.Text <> "" Then
            Try
                DevInfo = explorer.GetDevice(DeviceType.Scanner, cmbScannerName.Text)
                If Not DevInfo Is Nothing Then
                    MyScanner = explorer.CreateInstance(DevInfo)
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If
    End Sub

    Private Sub btnOpenDeviceScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDeviceScanner.Click
        Try
            If cmbScannerName.Text <> "" Then
                If lblStateScanner.Tag <> "Opened" Then
                    MyScanner.Open()
                    MyScanner.Claim(1000)
                    MyScanner.DeviceEnabled = True
                    lblStateScanner.Tag = "Opened"
                    lblstateDisplay.Text = getValueByKey("posdeviceopened")

                    DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Scanner")
                    If DTPOSDevice.Rows.Count > 0 Then
                        AddRecords("Update", "Scanner", "Scanner", cmbScannerName.Text, cmbscanner.Text, False)
                        btnSaveScanner.Enabled = True
                    Else
                        AddRecords("New", "Scanner", "Scanner", cmbScannerName.Text, cmbscanner.Text, False)
                        btnSaveScanner.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblStateScanner.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCLoseDeviceScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLoseDeviceScanner.Click
        Try
            If cmbScannerName.Text <> "" Then
                If lblStateScanner.Tag <> "Closed" Then
                    MyScanner.Close()
                    lblStateScanner.Tag = "Closed"
                    lblstateDisplay.Text = getValueByKey("posdeviceclosed")
                End If
            End If
        Catch ex As Exception
            lblStateScanner.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtScannerOutput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtScannerOutput.TextChanged
        Try
            If lblStateScanner.Tag = "Opened" Then
                If DTPOSDevice.Rows.Count > 0 Then
                    AddRecords("Update", "Scanner", "Scanner", cmbScannerName.Text, cmbscanner.Text, True)
                    btnSaveScanner.Enabled = True
                End If
            Else
                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Scanner")
                If DTPOSDevice.Rows.Count > 0 Then
                    AddRecords("Update", "Scanner", "Scanner", cmbScannerName.Text, cmbscanner.Text, True)
                Else
                    AddRecords("New", "Scanner", "Scanner", "Scanner", "OPOS", True)
                End If

                btnSaveScanner.Enabled = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnSaveScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveScanner.Click
        If cmbscanner.Text = "OPOS" Then
            'My.Settings.Scanner = cmbScannerName.Text.Trim
            CreateSpectrumParamFile("Scanner", cmbScannerName.Text.Trim)
            Dim sp As New SpectrumPrint.My.MySettings
            sp.Scanner = cmbScannerName.Text.Trim
            sp.Save()
        End If
    End Sub

#End Region

#Region "Private Functions"

    Private Sub GetDevice(ByVal POSDType As String)
        explorer.GetDevices()
        Dim POSSoName As String
        Try
            cmbPrinterName.Items.Clear()
            If (explorer.GetDevices().Count > 0) Then
                For Each Device As Object In explorer.GetDevices()
                    POSDType = Device.Type.ToString
                    POSSoName = Device.SoName.ToString
                    If Device.Type.ToString = "PosPrinter" Then
                        If Device.logicaldevices.count() > 0 Then
                            cmbPrinterName.Items.Clear()
                            cmbPrinterName.Items.Add(Device.LogicalNames(0).ToString)
                        End If
                    ElseIf Device.type.ToString = "CashDrawer" Then
                        ' If Device.logicaldevices.count() > 0 Then
                        cmbDrawerName.Items.Clear()
                        cmbDrawerName.Items.Add(Device.ServiceObjectName.ToString)
                        'End If
                    ElseIf Device.type.ToString = "Scanner" Then
                        If Device.logicaldevices.count() > 0 Then
                            cmbScannerName.Items.Clear()
                            cmbScannerName.Items.Add(Device.LogicalNames(0).ToString)
                        End If
                    ElseIf Device.type.ToString = "LineDisplay" Then
                        If Device.logicaldevices.count() > 0 Then
                            cmbDisplayName.Items.Clear()
                            cmbDisplayName.Items.Add(Device.LogicalNames(0).ToString)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
        
    End Sub

    Private Sub GetSystemDevice(ByVal POSDType As String)
        Try
            ' Use the ObjectQuery to get the list of configured printers
            Dim oquery As System.Management.ObjectQuery = New System.Management.ObjectQuery("SELECT * FROM Win32_Printer")
            Dim mosearcher As System.Management.ManagementObjectSearcher = New System.Management.ManagementObjectSearcher(oquery)

            Dim moc As System.Management.ManagementObjectCollection = mosearcher.Get()
            cmbPrinterName.Items.Clear()
            For Each mo As ManagementObject In moc
                If CBool(mo("Network")) Or CBool(mo("Local")) Then
                    cmbPrinterName.Items.Add(mo("Name"))
                End If
            Next mo
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
       ByVal e As PrintPageEventArgs) Handles printDocument1.PrintPage

        Try
            Dim MyFont As New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            Dim charactersOnPage As Integer = 0
            Dim linesPerPage As Integer = 0
            ' Sets the value of charactersOnPage to the number of characters 
            ' of stringToPrint that will fit within the bounds of the page.
            'e.Graphics.MeasureString("THIS IS A TEST PAGE", MyFont, e.MarginBounds.Size, _
            e.Graphics.MeasureString(getValueByKey("PDP007"), MyFont, e.MarginBounds.Size, _
                StringFormat.GenericTypographic, charactersOnPage, linesPerPage)

            ' Draws the string within the bounds of the page.
            'e.Graphics.DrawString("THIS IS A TEST PAGE", MyFont, Brushes.Black, _
            e.Graphics.DrawString(getValueByKey("PDP007"), MyFont, Brushes.Black, _
                e.MarginBounds, StringFormat.GenericTypographic)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub AddRecords(ByVal TransType As String, ByVal DeviceType As String, ByVal DeviceName As String, ByVal LogicalName As String, ByVal DriverType As String, ByVal IsConfigured As Boolean)
        Try
            If TransType = "Update" Then
                For Each drOther1 As DataRow In DTPOSDevice.Select("DriverType='" & DriverType & "'")
                    drOther1("DeviceName") = DeviceName
                    drOther1("LogicalName") = LogicalName
                    drOther1("isconfigured") = IsConfigured
                    drOther1("UPDATEDBY") = clsAdmin.UserCode
                    drOther1("UPDATEDAT") = clsAdmin.SiteCode
                    drOther1("UPDATEDON") = Now
                    drOther1("Status") = 1
                Next
            ElseIf TransType = "New" Then
                Dim DRow As DataRow = DTPOSDevice.NewRow
                DRow.Item("SiteCode") = clsAdmin.SiteCode
                DRow.Item("TerminalId") = clsAdmin.TerminalID
                DRow.Item("DeviceType") = DeviceType
                DRow.Item("DeviceName") = DeviceName
                DRow.Item("DriverType") = DriverType
                DRow.Item("LogicalName") = LogicalName
                DRow.Item("CREATEDBY") = clsAdmin.UserCode
                DRow.Item("CREATEDAT") = clsAdmin.SiteCode
                DRow.Item("CREATEDON") = Now
                DRow.Item("UPDATEDBY") = clsAdmin.UserCode
                DRow.Item("UPDATEDAT") = clsAdmin.SiteCode
                DRow.Item("UPDATEDON") = Now
                DRow.Item("isconfigured") = IsConfigured
                DRow.Item("Status") = 1
                DTPOSDevice.Rows.Add(DRow)
            End If
            DAPOSDevice.Update(DTPOSDevice)
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("PTM002"), "PTM002 - " & getValueByKey("CLAE05"))
        End Try
       
    End Sub

    Private Sub frmPOSDeviceProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Themechange()
        End If
        'code added for adding extra printer by vipul
        cmbPrinter.Items.Add("OPOS")
        cmbPrinter.Items.Add("Other")
        cmbPrinter.Items.Add("Other1")
        cmbPrinter.Items.Add("Printer2")
        cmbPrinter.Items.Add("Printer3")
        cmbPrinter.Items.Add("Printer4")
        cmbPrinter.Items.Add("Printer5")
        cmbPrinter.Items.Add("Printer6")
        cmbPrinter.Items.Add("Printer7")
        cmbPrinter.Items.Add("Printer8")
        cmbPrinter.Items.Add("Printer9")
        cmbPrinter.Items.Add("Printer10")
        cmbPrinter.Items.Add("Printer11")
        cmbPrinter.Items.Add("Printer12")
        cmbPrinter.Items.Add("Printer13")
        cmbPrinter.Items.Add("Printer14")
        cmbPrinter.Items.Add("Printer15")
        tbMSR.Visible = False
        lblTerminalId.Text = clsAdmin.TerminalID
        cmbPrinter_SelectedIndexChanged(sender, e)
        DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
        For Each drrow As DataRow In DTPOSDevice
            cmbPrinter.SelectedItem = drrow("DriverType")
            cmbPrinterName.SelectedItem = drrow("Logicalname")
        Next
        tcPOSDevice_SelectedIndexChanged(tbPrinter, New System.EventArgs)
        SetCulture(Me, Me.Name)

        'If cmbPrinterName.Items.Contains(My.Settings.OPOSPrinter) = True Then
        If cmbPrinterName.Items.Contains(ReadSpectrumParamFile("OPOSPrinter")) = True Then
            cmbPrinter.SelectedIndex = 0
            'cmbPrinterName.SelectedItem = My.Settings.OPOSPrinter.ToString
            cmbPrinterName.SelectedItem = ReadSpectrumParamFile("OPOSPrinter")
            'ElseIf cmbPrinterName.Items.Contains(My.Settings.OtherPrinter) = True Then
        ElseIf cmbPrinterName.Items.Contains(ReadSpectrumParamFile("OtherPrinter")) = True Then
            cmbPrinter.SelectedIndex = 1
            'cmbPrinterName.SelectedItem = My.Settings.OtherPrinter.ToString
            cmbPrinterName.SelectedItem = ReadSpectrumParamFile("OtherPrinter")
        End If

        'lblCurrWinPrnName.Text = My.Settings.OtherPrinter.ToString
        'lblOtherWinPrnName.Text = My.Settings.OtherPrinter1.ToString
        'lblCurrOPOSPrnName.Text = My.Settings.OPOSPrinter.ToString

        lblCurrWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter")
        lblOtherWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter1")
        lblCurrOPOSPrnName.Text = ReadSpectrumParamFile("OPOSPrinter")

        'cmbPrinterName.Items.Contains(My.Settings.OtherPrinter)
        'My.Settings.OtherPrinter
        'My.Settings.OPOSPrinter
        'cmbPrinter.Items(0)

    End Sub

    Private Sub tcPOSDevice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcPOSDevice.SelectedIndexChanged
        Try
            currTab = tcPOSDevice.SelectedIndex
            DTPOSDevice.Clear()
            If currTab = 0 Then
                cmbPrinter_SelectedIndexChanged(sender, e)
                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Printer")
                'code added for multiple KOT printer mapping by vipul
                Printermapping(DTPOSDevice)

                For Each drrow As DataRow In DTPOSDevice
                    'cmbPrinter.SelectedItem = drrow("DriverType")
                    'cmbPrinterName.SelectedItem = drrow("Logicalname")

                    'cmbPrinter.SelectedItem = My.Settings.OtherPrinter
                    'cmbPrinterName.SelectedItem = My.Settings.OtherPrinter
                    cmbPrinter.SelectedItem = ReadSpectrumParamFile("OtherPrinter")
                    cmbPrinterName.SelectedItem = ReadSpectrumParamFile("OtherPrinter")
                Next

                'lblCurrWinPrnName.Text = My.Settings.OtherPrinter.ToString
                'lblCurrOPOSPrnName.Text = My.Settings.OPOSPrinter.ToString

                lblCurrWinPrnName.Text = ReadSpectrumParamFile("OtherPrinter")
                lblCurrOPOSPrnName.Text = ReadSpectrumParamFile("OPOSPrinter")

            ElseIf currTab = 1 Then
                cmbDrawer_SelectedIndexChanged(sender, e)
                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Drawer")
                For Each drrow As DataRow In DTPOSDevice
                    cmbDrawer.SelectedItem = drrow("DriverType")
                    cmbDrawerName.SelectedItem = drrow("Logicalname")
                Next
                'If Not (My.Settings.Drawer.ToString = String.Empty) Then
                If Not (ReadSpectrumParamFile("Drawer") = String.Empty) Then
                    Try
                        'cmbDrawerName.SelectedItem = My.Settings.Drawer.ToString
                        cmbDrawerName.SelectedItem = ReadSpectrumParamFile("Drawer")
                    Catch ex As Exception

                    End Try
                End If
            ElseIf currTab = 2 Then
                cmbDisplayDevice_SelectedIndexChanged(sender, e)
                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Scanner")
                For Each drrow As DataRow In DTPOSDevice
                    cmbscanner.SelectedItem = drrow("DriverType")
                    cmbScannerName.SelectedItem = drrow("Logicalname")
                Next
                If Not (ReadSpectrumParamFile("LineDisplay") = String.Empty) Then
                    Try
                        'cmbScannerName.SelectedItem = My.Settings.LineDisplay.ToString
                        cmbScannerName.SelectedItem = ReadSpectrumParamFile("LineDisplay")
                    Catch ex As Exception

                    End Try
                End If


            ElseIf currTab = 3 Then
                cmbscanner_SelectedIndexChanged(sender, e)
                DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Line Display")
                For Each drrow As DataRow In DTPOSDevice
                    cmbDisplay.SelectedItem = drrow("DriverType")
                    cmbDisplayName.SelectedItem = drrow("Logicalname")
                Next
                If Not (ReadSpectrumParamFile("Scanner") = String.Empty) Then
                    Try
                        'cmbScannerName.SelectedItem = My.Settings.Scanner.ToString
                        cmbScannerName.SelectedItem = ReadSpectrumParamFile("Scanner")
                    Catch ex As Exception

                    End Try
                End If

                'Added code for fetching Miscellaneous tab details
            ElseIf currTab = 4 Then
                txtBackground.Text = ReadSpectrumParamFile("MDIBGIMAGEPATH")
                txtProduct.Text = ReadSpectrumParamFile("ArticleImageFolder")
            End If

            'ShowMessage(currTab)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        
    End Sub

#End Region

#Region "MSR"

    Private Sub cmbMsr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMsrName.SelectedIndexChanged
        If cmbMsrName.Text <> "" Then
            Try
                DevInfo = explorer.GetDevice(DeviceType.Msr, cmbMsrName.Text)
                If Not DevInfo Is Nothing Then
                    MyMsr = explorer.CreateInstance(DevInfo)
                End If
            Catch ex As Exception
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
                LogException(ex)
            End Try
        End If
    End Sub

    Private Sub cmbMsr_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMsr.SelectedIndexChanged
        GetDevice(tcPOSDevice.SelectedTab.Tag)
    End Sub

    Private Sub btnOpenMsr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenMsr.Click
        Try
            If cmbMsrName.Text <> "" Then
                If lblstateMsr.Tag <> "Opened" Then
                    MyMsr.Open()
                    MyMsr.Claim(1000)
                    MyMsr.DeviceEnabled = True

                    lblstateMsr.Tag = "Opened"
                    lblstateMsr.Text = getValueByKey("posdeviceopened")

                    DTPOSDevice = DAPOSDevice.GetPOSDeviceData(clsAdmin.SiteCode, clsAdmin.TerminalID, "Msr")
                    If DTPOSDevice.Rows.Count > 0 Then
                        AddRecords("Update", "Msr", "Msr", cmbMsrName.Text, cmbMsr.Text, False)
                        btnSaveDisplay.Enabled = True
                    Else
                        AddRecords("New", "Msr", "Msr", cmbMsrName.Text, cmbMsr.Text, False)
                        btnSaveDisplay.Enabled = True
                    End If
                End If
            Else
                ShowMessage(getValueByKey("PDP005"), "PDP005 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            lblstateMsr.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCloseMsr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseMsr.Click
        Try
            If cmbMsrName.Text <> "" Then
                If lblstateMsr.Text <> "Closed" Then
                    MyMsr.Release()
                    MyMsr.Close()
                    lblstateMsr.Tag = "Closed"
                    lblstateMsr.Text = getValueByKey("posdeviceclosed")
                End If
            Else
                ShowMessage(getValueByKey("PDP002"), "PDP002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Device not open.")
            End If

        Catch ex As Exception
            lblstateMsr.Text = ""
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSwipeCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSwipeCard.Click
        Try
            If lblstateMsr.Tag = "Opened" Then
                txtMsrHealthCheck.Text = ""
                MyMsr.CheckHealth(HealthCheckLevel.Interactive)
                txtMsrHealthCheck.Text = MyMsr.CheckHealth(HealthCheckLevel.Interactive)
                If DTPOSDevice.Rows.Count > 0 Then
                    AddRecords("Update", "Msr", "Msr", cmbMsrName.Text, cmbMsr.Text, True)
                    btnSaveMsr.Enabled = True
                Else
                    AddRecords("New", "Msr", "Msr", cmbMsrName.Text, cmbMsr.Text, False)
                    btnSaveMsr.Enabled = True
                End If
            Else
                lblstateMsr.Text = " Device not Claimed"
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSaveMsr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMsr.Click
        If cmbMsr.Text = "OPOS" Then
            My.Settings.MsrLogicalName = cmbMsrName.Text.Trim
            My.Settings.Save()
            lblCurrentMsr.Text = cmbMsrName.Text.Trim

        End If
    End Sub

    Private Sub ChkEnabledMsr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkEnabledMsr.Click
        If ChkEnabledMsr.CheckState = CheckState.Checked Then
            My.Settings.MSREnabled = 1
        Else
            My.Settings.MSREnabled = 0
        End If
        My.Settings.Save()
    End Sub

    Private Sub ChkpwayEnabled_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkpwayEnabled.Click
        If ChkpwayEnabled.CheckState = CheckState.Checked Then
            My.Settings.PayGateWayEnabled = 1
        Else
            My.Settings.PayGateWayEnabled = 0
        End If
        My.Settings.Save()
    End Sub

#End Region

#Region "Miscellaneous"
    Private Sub btnBackgroundButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackgroundButton.Click
        BackgroundFileDialog.ShowDialog()
    End Sub

    Private Sub btnProductButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductButton.Click
        If ProductFolderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtProduct.Text = ProductFolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            CreateSpectrumParamFile("MDIBGIMAGEPATH", txtBackground.Text)
            CreateSpectrumParamFile("ArticleImageFolder", txtProduct.Text)
            ShowMessage(getValueByKey("PRNS001"), "PRNS001 - " & getValueByKey("CLAE04"))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BackgroundFileDialog_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BackgroundFileDialog.FileOk
        If Not String.IsNullOrEmpty(BackgroundFileDialog.FileName) Then
            txtBackground.Text = BackgroundFileDialog.FileName
        End If
    End Sub

    Private Sub btnBackgroundReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackgroundReset.Click
        txtBackground.Text = String.Empty
    End Sub

    Private Sub btnProductReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductReset.Click
        txtProduct.Text = String.Empty
    End Sub

#End Region

   
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmPOSDeviceProfile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "pos-device-settings.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()

        Label7.ForeColor = Color.White
        lblTerminalId.ForeColor = Color.White

        btnOpenDevicePrinter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenDevicePrinter.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenDevicePrinter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenDevicePrinter.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDevicePrinter.FlatStyle = FlatStyle.Flat
        btnOpenDevicePrinter.FlatAppearance.BorderSize = 0
        btnOpenDevicePrinter.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenDevicePrinter.Size = New Size(85, 25)
        btnOpenDevicePrinter.ForeColor = Color.FromArgb(255, 255, 255)

        btnPrintSlip.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnPrintSlip.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnPrintSlip.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnPrintSlip.BackColor = Color.Transparent
        btnPrintSlip.BackColor = Color.FromArgb(0, 107, 163)
        btnPrintSlip.BackColor = Color.FromArgb(0, 107, 163)
        btnPrintSlip.ForeColor = Color.FromArgb(255, 255, 255)
        btnPrintSlip.FlatStyle = FlatStyle.Flat
        btnPrintSlip.FlatAppearance.BorderSize = 0
        btnPrintSlip.Size = New Size(85, 25)

        btnCloseDevicePrinter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseDevicePrinter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCloseDevicePrinter.BackColor = Color.Transparent
        btnCloseDevicePrinter.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseDevicePrinter.ForeColor = Color.FromArgb(255, 255, 255)
        btnCloseDevicePrinter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCloseDevicePrinter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseDevicePrinter.FlatStyle = FlatStyle.Flat
        btnCloseDevicePrinter.FlatAppearance.BorderSize = 0
        btnCloseDevicePrinter.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCloseDevicePrinter.Size = New Size(85, 25)

        btnSavePrinter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSavePrinter.BackColor = Color.Transparent
        btnSavePrinter.BackColor = Color.FromArgb(0, 107, 163)
        btnSavePrinter.ForeColor = Color.FromArgb(255, 255, 255)
        btnSavePrinter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSavePrinter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSavePrinter.FlatStyle = FlatStyle.Flat
        btnSavePrinter.FlatAppearance.BorderSize = 0
        btnSavePrinter.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSavePrinter.Size = New Size(85, 25)

        btnOpenDeviceDrawer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenDeviceDrawer.BackColor = Color.Transparent
        btnOpenDeviceDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenDeviceDrawer.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenDeviceDrawer.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOpenDeviceDrawer.FlatStyle = FlatStyle.Flat
        btnOpenDeviceDrawer.FlatAppearance.BorderSize = 0
        btnOpenDeviceDrawer.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDrawer.Size = New Size(85, 25)

        btnOpenDrawer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenDrawer.BackColor = Color.Transparent
        btnOpenDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenDrawer.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenDrawer.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOpenDrawer.FlatStyle = FlatStyle.Flat
        btnOpenDrawer.FlatAppearance.BorderSize = 0
        btnOpenDrawer.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDrawer.Size = New Size(85, 25)

        btnCloseDeviceDrawer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCloseDeviceDrawer.BackColor = Color.Transparent
        btnCloseDeviceDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        btnCloseDeviceDrawer.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCloseDeviceDrawer.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseDeviceDrawer.FlatStyle = FlatStyle.Flat
        btnCloseDeviceDrawer.FlatAppearance.BorderSize = 0
        btnCloseDeviceDrawer.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDrawer.Size = New Size(85, 25)

        btnSaveDrawer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveDrawer.BackColor = Color.Transparent
        btnSaveDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveDrawer.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveDrawer.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveDrawer.FlatStyle = FlatStyle.Flat
        btnSaveDrawer.FlatAppearance.BorderSize = 0
        btnSaveDrawer.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSaveDrawer.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveDrawer.Size = New Size(85, 25)

        btnOpenDeviceScanner.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenDeviceScanner.BackColor = Color.Transparent
        btnOpenDeviceScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceScanner.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenDeviceScanner.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenDeviceScanner.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOpenDeviceScanner.FlatStyle = FlatStyle.Flat
        btnOpenDeviceScanner.FlatAppearance.BorderSize = 0
        btnOpenDeviceScanner.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceScanner.Size = New Size(85, 25)

        btnCLoseDeviceScanner.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCLoseDeviceScanner.BackColor = Color.Transparent
        btnCLoseDeviceScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnCLoseDeviceScanner.ForeColor = Color.FromArgb(255, 255, 255)
        btnCLoseDeviceScanner.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCLoseDeviceScanner.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCLoseDeviceScanner.FlatStyle = FlatStyle.Flat
        btnCLoseDeviceScanner.FlatAppearance.BorderSize = 0
        btnCLoseDeviceScanner.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCLoseDeviceScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnCLoseDeviceScanner.Size = New Size(85, 25)

        btnSaveScanner.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveScanner.BackColor = Color.Transparent
        btnSaveScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveScanner.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveScanner.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveScanner.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveScanner.FlatStyle = FlatStyle.Flat
        btnSaveScanner.FlatAppearance.BorderSize = 0
        btnSaveScanner.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSaveScanner.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveScanner.Size = New Size(85, 25)

        btnOpenDeviceDisplay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenDeviceDisplay.BackColor = Color.Transparent
        btnOpenDeviceDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDisplay.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenDeviceDisplay.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenDeviceDisplay.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOpenDeviceDisplay.FlatStyle = FlatStyle.Flat
        btnOpenDeviceDisplay.FlatAppearance.BorderSize = 0
        btnOpenDeviceDisplay.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenDeviceDisplay.Size = New Size(85, 25)

        btnShowMessage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnShowMessage.BackColor = Color.Transparent
        btnShowMessage.BackColor = Color.FromArgb(0, 107, 163)
        btnShowMessage.ForeColor = Color.FromArgb(255, 255, 255)
        btnShowMessage.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnShowMessage.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnShowMessage.FlatStyle = FlatStyle.Flat
        btnShowMessage.FlatAppearance.BorderSize = 0
        btnShowMessage.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnShowMessage.BackColor = Color.FromArgb(0, 107, 163)
        btnShowMessage.Size = New Size(85, 25)

        btnCloseDeviceDisplay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCloseDeviceDisplay.BackColor = Color.Transparent
        btnCloseDeviceDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDisplay.ForeColor = Color.FromArgb(255, 255, 255)
        btnCloseDeviceDisplay.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCloseDeviceDisplay.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseDeviceDisplay.FlatStyle = FlatStyle.Flat
        btnCloseDeviceDisplay.FlatAppearance.BorderSize = 0
        btnCloseDeviceDisplay.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseDeviceDisplay.Size = New Size(85, 25)


        btnSaveDisplay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveDisplay.BackColor = Color.Transparent
        btnSaveDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveDisplay.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveDisplay.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveDisplay.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveDisplay.FlatStyle = FlatStyle.Flat
        btnSaveDisplay.FlatAppearance.BorderSize = 0
        btnSaveDisplay.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSaveDisplay.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveDisplay.Size = New Size(85, 25)

        btnOpenMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOpenMsr.BackColor = Color.Transparent
        btnOpenMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenMsr.ForeColor = Color.FromArgb(255, 255, 255)
        btnOpenMsr.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnOpenMsr.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOpenMsr.FlatStyle = FlatStyle.Flat
        btnOpenMsr.FlatAppearance.BorderSize = 0
        btnOpenMsr.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnOpenMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnOpenMsr.Size = New Size(85, 25)

        btnSwipeCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSwipeCard.BackColor = Color.Transparent
        btnSwipeCard.BackColor = Color.FromArgb(0, 107, 163)
        btnSwipeCard.ForeColor = Color.FromArgb(255, 255, 255)
        btnSwipeCard.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSwipeCard.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSwipeCard.FlatStyle = FlatStyle.Flat
        btnSwipeCard.FlatAppearance.BorderSize = 0
        btnSwipeCard.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSwipeCard.BackColor = Color.FromArgb(0, 107, 163)
        btnSwipeCard.Size = New Size(85, 25)

        btnCloseMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCloseMsr.BackColor = Color.Transparent
        btnCloseMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseMsr.ForeColor = Color.FromArgb(255, 255, 255)
        btnCloseMsr.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCloseMsr.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCloseMsr.FlatStyle = FlatStyle.Flat
        btnCloseMsr.FlatAppearance.BorderSize = 0
        btnCloseMsr.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCloseMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnCloseMsr.Size = New Size(85, 25)

        btnSaveMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSaveMsr.BackColor = Color.Transparent
        btnSaveMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveMsr.ForeColor = Color.FromArgb(255, 255, 255)
        btnSaveMsr.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSaveMsr.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSaveMsr.FlatStyle = FlatStyle.Flat
        btnSaveMsr.FlatAppearance.BorderSize = 0
        btnSaveMsr.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSaveMsr.BackColor = Color.FromArgb(0, 107, 163)
        btnSaveMsr.Size = New Size(85, 25)

        btnBackgroundButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBackgroundButton.BackColor = Color.Transparent
        btnBackgroundButton.BackColor = Color.FromArgb(0, 107, 163)
        btnBackgroundButton.ForeColor = Color.FromArgb(255, 255, 255)
        btnBackgroundButton.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBackgroundButton.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBackgroundButton.FlatStyle = FlatStyle.Flat
        btnBackgroundButton.FlatAppearance.BorderSize = 0
        btnBackgroundButton.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnBackgroundButton.BackColor = Color.FromArgb(0, 107, 163)
        btnBackgroundButton.Size = New Size(76, 28)

        btnBackgroundReset.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBackgroundReset.BackColor = Color.Transparent
        btnBackgroundReset.BackColor = Color.FromArgb(0, 107, 163)
        btnBackgroundReset.ForeColor = Color.FromArgb(255, 255, 255)
        btnBackgroundReset.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBackgroundReset.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBackgroundReset.FlatStyle = FlatStyle.Flat
        btnBackgroundReset.FlatAppearance.BorderSize = 0
        btnBackgroundReset.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnBackgroundReset.BackColor = Color.FromArgb(0, 107, 163)
        btnBackgroundReset.Size = New Size(85, 28)

        btnProductButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnProductButton.BackColor = Color.Transparent
        btnProductButton.BackColor = Color.FromArgb(0, 107, 163)
        btnProductButton.ForeColor = Color.FromArgb(255, 255, 255)
        btnProductButton.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnProductButton.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnProductButton.FlatStyle = FlatStyle.Flat
        btnProductButton.FlatAppearance.BorderSize = 0
        btnProductButton.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnProductButton.BackColor = Color.FromArgb(0, 107, 163)
        btnProductButton.Size = New Size(76, 28)

        btnProductReset.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnProductReset.BackColor = Color.Transparent
        btnProductReset.BackColor = Color.FromArgb(0, 107, 163)
        btnProductReset.ForeColor = Color.FromArgb(255, 255, 255)
        btnProductReset.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnProductReset.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnProductReset.FlatStyle = FlatStyle.Flat
        btnProductReset.FlatAppearance.BorderSize = 0
        btnProductReset.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnProductReset.BackColor = Color.FromArgb(0, 107, 163)
        btnProductReset.Size = New Size(85, 28)

        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.Size = New Size(85, 28)

        tbDrawer.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tbLineDisplay.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tbMiscellaneous.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tbMSR.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tbPrinter.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        tbScanner.TabBackColorSelected = Color.FromArgb(0, 107, 163)

        tbDrawer.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbLineDisplay.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbMiscellaneous.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbMSR.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbPrinter.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbScanner.TabForeColorSelected = Color.FromArgb(255, 255, 255)
        tbDrawer.BackColor = Color.FromArgb(134, 134, 134)
        tbLineDisplay.BackColor = Color.FromArgb(134, 134, 134)
        tbMiscellaneous.BackColor = Color.FromArgb(134, 134, 134)
        tbMSR.BackColor = Color.FromArgb(134, 134, 134)
        tbPrinter.BackColor = Color.FromArgb(134, 134, 134)
        tbScanner.BackColor = Color.FromArgb(134, 134, 134)
        tbDrawer.BackColor = Color.FromArgb(134, 134, 134)

        Label1.ForeColor = Color.FromArgb(255, 255, 255)
        Label10.ForeColor = Color.FromArgb(255, 255, 255)
        Label11.ForeColor = Color.FromArgb(255, 255, 255)
        Label12.ForeColor = Color.FromArgb(255, 255, 255)
        Label13.ForeColor = Color.FromArgb(255, 255, 255)
        Label15.ForeColor = Color.FromArgb(255, 255, 255)
        Label2.ForeColor = Color.FromArgb(255, 255, 255)
        Label3.ForeColor = Color.FromArgb(255, 255, 255)
        Label4.ForeColor = Color.FromArgb(255, 255, 255)
        Label5.ForeColor = Color.FromArgb(255, 255, 255)
        Label6.ForeColor = Color.FromArgb(255, 255, 255)
        Label7.ForeColor = Color.FromArgb(255, 255, 255)
        Label8.ForeColor = Color.FromArgb(255, 255, 255)
        Label9.ForeColor = Color.FromArgb(255, 255, 255)
        lblCardDtls.ForeColor = Color.FromArgb(255, 255, 255)
        lblCurrentMsr.ForeColor = Color.FromArgb(255, 255, 255)
        lblCurrentMSRlogicalName.ForeColor = Color.FromArgb(255, 255, 255)
        lblCurrentState.ForeColor = Color.FromArgb(255, 255, 255)
        lblCurrOPOSPrnName.ForeColor = Color.FromArgb(255, 255, 255)
        lblCurrWinPrn.ForeColor = Color.FromArgb(255, 255, 255)

        lblCurrWinPrnName.ForeColor = Color.FromArgb(255, 255, 255)
        lblDisplay.ForeColor = Color.FromArgb(255, 255, 255)
        lblDisplayDeviceName.ForeColor = Color.FromArgb(255, 255, 255)
        lblDisplCashDrawar.ForeColor = Color.FromArgb(255, 255, 255)
        lblDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        lblMsr.ForeColor = Color.FromArgb(255, 255, 255)
        lblOPOSWinPrn.ForeColor = Color.FromArgb(255, 255, 255)
        lblOtherWinPrnName.ForeColor = Color.FromArgb(255, 255, 255)
        lblScanner.ForeColor = Color.FromArgb(255, 255, 255)
        lblScannerName.ForeColor = Color.FromArgb(255, 255, 255)
        lblstateDisplay.ForeColor = Color.FromArgb(255, 255, 255)
        lblStateDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        lblstateMsr.ForeColor = Color.FromArgb(255, 255, 255)



        lblStatePrinter.ForeColor = Color.FromArgb(255, 255, 255)
        lblStateScanner.ForeColor = Color.FromArgb(255, 255, 255)
        lblStatusPrinter.ForeColor = Color.FromArgb(255, 255, 255)
        lblTerminalId.ForeColor = Color.FromArgb(255, 255, 255)
        lblTrackNo.ForeColor = Color.FromArgb(255, 255, 255)
        MsrName.ForeColor = Color.FromArgb(255, 255, 255)
        GrpDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        GrpAssignedPrn.ForeColor = Color.FromArgb(255, 255, 255)
        GroupBox1.ForeColor = Color.FromArgb(255, 255, 255)
        GroupBox2.ForeColor = Color.FromArgb(255, 255, 255)

        gbTestDrawer.ForeColor = Color.FromArgb(255, 255, 255)
        gbTestPrinter.ForeColor = Color.FromArgb(255, 255, 255)
        gbTestScanner.ForeColor = Color.FromArgb(255, 255, 255)
        GrpMsrCurrAssigned.ForeColor = Color.FromArgb(255, 255, 255)

    End Function
    ''code added for KOT printer mapping by vipul

    Private Function Printermapping(ByVal DTPOSDevice As DataTable)
        Try
            Dim dtprintx = DTPOSDevice.DefaultView.ToTable(True, "DriverType", "LogicalName")
            grdprinter.DataSource = dtprintx


            Dim aa = grdprinter.Columns(0).Width
            Dim bb = grdprinter.Width

            grdprinter.Columns(1).Width = bb - aa
            ' grdprinter.ScrollBars = ScrollBars.None

            grdprinter.AllowUserToResizeColumns = False
            grdprinter.AllowUserToResizeRows = False


            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                grdprinter.CellBorderStyle = DataGridViewCellBorderStyle.None
                grdprinter.RowHeadersVisible = False
                grdprinter.BorderStyle = BorderStyle.None

                grdprinter.ColumnHeadersVisible = False
                grdprinter.BackgroundColor = Color.Gray
                grdprinter.DefaultCellStyle.BackColor = Color.Gray
                ' grdprinter.BackColor = Color.FromArgb(255, 255, 255)

                grdprinter.DefaultCellStyle.ForeColor = Color.White

                grdprinter.DefaultCellStyle.SelectionBackColor = Color.Gray
                grdprinter.DefaultCellStyle.SelectionForeColor = Color.White






            Else

                grdprinter.CellBorderStyle = DataGridViewCellBorderStyle.None
                grdprinter.RowHeadersVisible = False
                grdprinter.BorderStyle = BorderStyle.None
                grdprinter.BackgroundColor = Color.White
                grdprinter.ColumnHeadersVisible = False
                grdprinter.DefaultCellStyle.SelectionBackColor = Color.White
                grdprinter.DefaultCellStyle.SelectionForeColor = Color.Black
            End If

        Catch ex As Exception

        End Try
    End Function
End Class
