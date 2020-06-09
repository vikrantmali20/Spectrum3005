Imports C1.Win.C1FlexGrid
Imports SpectrumBL
Imports System.Text
Imports C1.C1Pdf
Imports System.Drawing.Printing
Imports System.Drawing
Imports Microsoft.PointOfService
Imports System.Threading
Imports System.Resources
Imports System.Globalization
Imports System.IO
Imports C1.Win.C1BarCode
Imports C1.Win.C1Ribbon
Imports C1.Win.C1List
Imports C1
Imports System.Collections
Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Reflection
Imports System.ComponentModel
Imports System.Net
''' <summary>
''' This Module is Used Commonly Define function's,Methods's 
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Module modCommonFunc

#Region "Global Variable's"
    Public IsValidated As Boolean = False
    Public IsRecordInserted As Boolean = False
    Public IsNewCustomer As Boolean = False
    Public drItemsRow As DataRow
    Public dtAuthUserTran As DataTable 'Used to Store user Authorization of transation 
    Public dtPrinterInfo As DataTable 'Used to assign printer to terminal
    Public _SelectedCustmCode As String
    Public Const _TitleSize As Int16 = 200
    Public cResourcePath As String = Application.StartupPath.ToString()
    Public gResourceMgr As New Object
    Public gCI As New Object
    Public gActiveLangId As String
    Public LoginStatus As Boolean = False
    Public strTitle As String = ""
    Public gmdiclientheight As Integer
    Public gmdiclientwidth As Integer
    'Public ObjMdi As New MDISpectrum()
    Public resourceMgr As ResourceManager
    Public VarBarcode As C1BarCode
    Public IsOutboundCreated As Boolean = False
    Public EanType As String = ""
    Public objSO As New clsSalesOrder
    Public objPCSO As New clsSalesOrderPC
    Public objQuotation As New clsQuotation
    Public objSchCustm As New frmNSearchCustomer
    Public dtItemScanData As DataTable
    Public CLPRedemptionAllowed As Boolean
    Public _BarCodeType As String = 0 ' constant value of OPOS barcode type is used. 
    'Public strZero As String = FormatNumber(0, 2).ToString
    Public strZero As String = 0
    Public isPromotionsDetailClearRequired As Boolean = False
    'Public ci As CultureInfo
    Public dsPatientHcTrnDetails As New DataSet
#End Region
#Region "Global Variable For Class"


    Public Enum enumDineInProcess
        NewHold = 1
        EditHold = 2
        MergeHold = 3
        SwitchTable = 4
        EditAndLoad = 5
    End Enum

    Public Enum enumOperationOnBill
        EditBill = 1
        VoidBill = 2
        Promotion = 3
        HoldBill = 4
        GiftPrint = 5
        Reprint = 6
        '  EditAndLoad = 5
    End Enum
    'added by khusrao adil on 13-03-2018 for spectrum new developement 
    Public Enum enumProductNotificationTimerPopups
        CreditSalesPopup = 1
        NCreditSalesPopup = 2
        LowStockPoup = 3
        SalesOrderPopup = 4
        ExpiryProductPopup = 5
    End Enum
    Dim _Pdf As C1PdfDocument
    Private printPreviewDialog1 As New PrintPreviewDialog()
    Private WithEvents printDocument1 As New PrintDocument()

    'Public gPrinterName As String = My.Settings.OtherPrinter   '"HP LaserJet 2420 PCL 6_ "
    Public gPrinterName As String = ReadSpectrumParamFile("OtherPrinter")   '"HP LaserJet 2420 PCL 6_ "

    'Private gPrinterName As String = "TVSPrinter"
    '** change at Ireland
    'Private stringToPrint, documentContents As String
    Public stringToPrint, documentContents As String

    'There are 5 POS devices for use with this application 
    Public WithEvents explorer As New PosExplorer
    'WithEvents explorer As PosExplorer
    WithEvents gOposScanner As Scanner
    WithEvents gOposMSR As Msr
    Public gOposPolDisplay As LineDisplay
    Dim gOposPrinter As PosPrinter
    Dim gOposCashDrawer As CashDrawer
    Public strReturn2CustomerMsg As String = ""

    Public Property BarCodeType() As String
        Get
            Return _BarCodeType
        End Get
        Set(ByVal value As String)
            _BarCodeType = value
        End Set
    End Property


#End Region
#Region "POS Devices"

    Dim lblDeviceStatus As New Label
    '******************************************************************************************************************
    '
    'InitializeDevices
    'Here we initialize the different POS devices. Assuming that the logical names have been setup correctly,
    'a create isntance of the five device will be performed. If there is a device missing or not functional, the Status box
    'will list the missing devices. 
    'The line display and the SCanner are the only two devices that need to be opended and enabled. The others
    'will be opened, claimed, and enabled as there are needed.

    Public Sub InitializeDevices()

        Dim DeviceStr As String = ""
        Dim eFlag As Boolean = False

        'Poll Display setup
        'If the display is not attached it cannot be claimed. An exception will pop
        'if we don't have the try-catch in place. The Try Catch will be used for all
        '5 POS devices
        Try
            Dim device As DeviceInfo = explorer.GetDevice("LineDisplay", "TVSDISPLAY")
            If Not device Is Nothing Then
                gOposPolDisplay = explorer.CreateInstance(device)
                gOposPolDisplay.Open()
                gOposPolDisplay.Claim(1000)
                gOposPolDisplay.DeviceEnabled = True
                gOposPolDisplay.ClearText()
                gOposPolDisplay.DisplayText("Pol Display              WEPOS/POS for .NET")
            End If

        Catch ex As Exception
            DeviceStr = DeviceStr & " LineDisplay"
            eFlag = True

        End Try


        'Now lets get the Printer
        Try
            Dim device2 As DeviceInfo = explorer.GetDevice("PosPrinter", "TVSPRINTER")
            If Not device2 Is Nothing Then
                gOposPrinter = explorer.CreateInstance(device2)
            End If
        Catch ex As Exception
            DeviceStr = DeviceStr & " Printer"
            eFlag = True
        End Try

        'Get the Cash Drawer
        Try
            Dim device3 As DeviceInfo = explorer.GetDevice("CashDrawer", "WASPCD")
            If Not device3 Is Nothing Then
                gOposCashDrawer = explorer.CreateInstance(device3)
            End If

        Catch ex As Exception
            DeviceStr = DeviceStr & " Cash Drawer"
            eFlag = True

        End Try

        'Connect the MSR
        Try
            Dim device4 As DeviceInfo = explorer.GetDevice("Msr", "gOposMSR")
            If Not device4 Is Nothing Then
                gOposMSR = explorer.CreateInstance(device4)
            End If
        Catch ex As Exception
            DeviceStr = DeviceStr & " MSR"
            eFlag = True

        End Try

        'Finally, connect the Bar Code Scanner
        Try
            Dim device5 As DeviceInfo = explorer.GetDevice("Scanner", "gOposScanner")
            If Not device5 Is Nothing Then
                gOposScanner = explorer.CreateInstance(device5)
                gOposScanner.Open()
                gOposScanner.Claim(1000)
                gOposScanner.DeviceEnabled = True
                gOposScanner.DataEventEnabled = True
                gOposScanner.DecodeData = True
            End If

        Catch ex As Exception
            DeviceStr = DeviceStr & " Scanner"
            eFlag = True

        End Try

        ''If anything is missing then list it in the status box.
        ''If eFlag.Equals(True) Then
        ''    lblStatus.Text = "No" & DeviceStr & " Attached - Reconnect or Replace"
        ''Else
        ''    lblStatus.Text = "Ready"
        ''End If
        ''Update()

    End Sub


    '******************************************************************************************************************
    '
    'explorer_DeviceAddedEvent
    'explorer was defined WithEvents at the top of the listing. This enables two events DeviceAddedEvent and DeviceRemoveEvent
    'Here we will check to see what devices were added and display to the line display and status box
    '
    '
    Private Sub explorer_DeviceAddedEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.DeviceChangedEventArgs) Handles explorer.DeviceAddedEvent

        'Since the scanner is used from the onset, we imediately open the scanner.
        If (e.Device.Type = "Scanner") Then
            gOposScanner = explorer.CreateInstance(e.Device)
            gOposPolDisplay.ClearText()
            'gOposPolDisplay.DisplayText("New bar-code scanner attached")
            'lblDeviceStatus.Text = "New bar-code scanner attached: " & e.Device.ServiceObjectName
            gOposPolDisplay.DisplayText(getValueByKey("mod001"))
            lblDeviceStatus.Text = getValueByKey("mod001") & ": " & e.Device.ServiceObjectName
            gOposScanner.Open()
            gOposScanner.Claim(1000)
            gOposScanner.DeviceEnabled = True
            gOposScanner.DataEventEnabled = True
            gOposScanner.DecodeData = True
            'Update()
        End If

        'We only use the MSR when there is a credit card payment
        'We only need to create and instance of the device. The MSR will be opened later

        If (e.Device.Type = "Msr") Then
            gOposMSR = explorer.CreateInstance(e.Device)
            gOposPolDisplay.ClearText()
            'gOposPolDisplay.DisplayText("New MSR attached")
            'lblDeviceStatus.Text = "New Msr attached: " & e.Device.ServiceObjectName
            gOposPolDisplay.DisplayText(getValueByKey("mod002"))
            lblDeviceStatus.Text = getValueByKey("mod002") & ": " & e.Device.ServiceObjectName
            'Update()
        End If

    End Sub

    '******************************************************************************************************************
    '
    'explorer_DeviceRemoveEvent
    'explorer was defined WithEvents at the top of the listing. This enables two events DeviceAddedEvent and DeviceRemoveEvent
    'Here we will check to see what devices were removed added and display to the line display and status box
    '
    '
    Private Sub explorer_DeviceRemovedEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.DeviceChangedEventArgs) Handles explorer.DeviceRemovedEvent

        'Close out the scanner
        If (e.Device.Type = "Scanner") Then
            'gOposPolDisplay.DisplayText("Bar-code scanner removed")
            'lblDeviceStatus.Text = "Bar-code scanner removed"

            gOposPolDisplay.ClearText()
            gOposPolDisplay.DisplayText(getValueByKey("mod003"))
            lblDeviceStatus.Text = getValueByKey("mod003")
            gOposScanner.DecodeData = False
            gOposScanner.DataEventEnabled = False
            gOposScanner.DeviceEnabled = False
            gOposScanner.Release()
            gOposScanner.Close()
            'Update()
        End If
        'Close out the MSR
        If (e.Device.Type = "Msr") Then
            gOposPolDisplay.ClearText()

            'gOposPolDisplay.DisplayText("Msr removed")
            'lblDeviceStatus.Text = "Msr removed"

            gOposPolDisplay.DisplayText(getValueByKey("mod004"))
            lblDeviceStatus.Text = getValueByKey("mod004")
            gOposMSR.DataEventEnabled = False
            gOposMSR.DecodeData = False
            gOposMSR.Release()
            gOposMSR.Close()
            lblDeviceStatus.Text = getValueByKey("mod005")
            'Update()
        End If


    End Sub
    '******************************************************************************************************************
    '
    'gOposScanner_ErrorEvent
    'If there is an error scanning the item, then indicate this on the Status Box, and re-enable the 
    'scanner data event - don't leave the scanner disabled or someone will think the scanner
    'went bad.
    '

    Private Sub gOposScanner_ErrorEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.DeviceErrorEventArgs) Handles gOposScanner.ErrorEvent
        lblDeviceStatus.Text = getValueByKey("mod006")
        'Update()
        gOposScanner.DataEventEnabled = True
    End Sub


    '******************************************************************************************************************
    '
    'OpenCashDrawer
    'Seperate subroutine to open the WASP 5000 cash drawer. One could add a button to open the cash drawer
    'independent of the sales process.
    '

    Sub OpenCashDrawer()
        Try


            gOposCashDrawer.Open()
            gOposCashDrawer.Claim(1000)
            gOposCashDrawer.DeviceEnabled = True
            gOposCashDrawer.OpenDrawer()
            gOposCashDrawer.DeviceEnabled = False
            gOposCashDrawer.Release()
            gOposCashDrawer.Close()

            lblDeviceStatus.Text = getValueByKey("mod007")
        Catch ex As Exception
            gOposCashDrawer.DeviceEnabled = False
            gOposCashDrawer.Release()
            gOposCashDrawer.Close()
        End Try
        'Update()

    End Sub


    Private Sub gOposMSR_ErrorEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.DeviceErrorEventArgs) Handles gOposMSR.ErrorEvent
        lblDeviceStatus.Text = getValueByKey("mod008")
        'Update()
        gOposMSR.DataEventEnabled = True
        Dim cardno As String
        cardno = gOposMSR.AccountNumber

    End Sub

#End Region

#Region "Public Property's"
    Public Property SelectedCustmCode() As String
        Get
            Return _SelectedCustmCode
        End Get
        Set(ByVal value As String)
            _SelectedCustmCode = value
        End Set
    End Property
    ''' <summary>
    ''' Used by "TransactionType" proprty 
    ''' </summary>
    ''' <remarks></remarks>
    Private _TransactionType As TransactionTypes
    ''' <summary>
    ''' Stores BirthList Transaction names
    ''' <UsedBy>frmBirthListSales.vb,frmBirthListUpdation.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TransactionTypes
        BirthListCreate
        BirthListUpdate
        BirthListSales
        BL_CLOSE
        BL_ChngDisc
    End Enum
    ''' <summary>
    '''  Need to set in frmBirthListUpdation.vb ,when we call "apply discount" 
    ''' </summary>
    ''' <UsedBy> frmBirthListUpdation.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransactionType() As TransactionTypes
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As TransactionTypes)
            _TransactionType = value
        End Set
    End Property

    Private _PrintTransType As String
    Public Property PrintTransType() As String
        Get
            Return _PrintTransType
        End Get
        Set(ByVal value As String)
            _PrintTransType = value
        End Set
    End Property

#End Region
#Region "Public Method's & Function's"

    Public ReadOnly Property OnlineConnect() As Boolean
        Get
            Return DataBaseConnection._CurrentStatus
        End Get
    End Property
    Dim _NewTotalDiscount As Decimal 'vipin PC SO Merge 03-05-2018
    Public ReadOnly Property NewDiscountAmount() As Decimal
        Get
            Return _NewTotalDiscount
        End Get
    End Property
#Region "Language Translation"
    'START RS

    Public Sub SetCulture(ByRef ctrls As Control, Optional ByVal formName As String = "", Optional ByRef rbCntrl As C1.Win.C1Ribbon.C1Ribbon = Nothing)
        Try
            If Not resourceMgr Is Nothing Then
                If TypeOf ctrls Is Form Then
                    If getValueByKey(formName.ToLower) <> String.Empty Then
                        ctrls.Text = getValueByKey(formName.ToLower)
                    End If
                End If

                For Each ctrl As Control In ctrls.Controls
                    'Dim MyFont As New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    'Dim ctrlFont As Font = ctrl.Font
                    'Dim MyFont As New System.Drawing.Font("Courier New", ctrlFont.Size)
                    'ctrl.Font = MyFont
                    If TypeOf ctrl Is Button Or TypeOf ctrl Is Label Or TypeOf ctrl Is RadioButton Or TypeOf ctrl Is CheckBox Then
                        Dim cntrlKey As String = String.Empty
                        constructKey(ctrl, cntrlKey, formName)
                        If getValueByKey(cntrlKey.ToLower) <> String.Empty Then
                            ctrl.Text = getValueByKey(cntrlKey.ToLower)
                        End If
                    End If

                    If TypeOf ctrl Is C1FlexGrid Then
                        For i As Integer = 0 To CType(ctrl, C1FlexGrid).Cols.Count - 1
                            Dim cntrlKey As String = String.Empty
                            constructKey(ctrl, cntrlKey, formName, CType(ctrl, C1FlexGrid).Cols(i).Name)
                            If getValueByKey(cntrlKey.ToLower) <> String.Empty Then
                                CType(ctrl, C1FlexGrid).Cols(i).Caption = getValueByKey(cntrlKey.ToLower)
                            End If
                        Next
                    End If

                    If TypeOf ctrl Is C1.Win.C1TrueDBGrid.C1TrueDBGrid Then
                        For i As Integer = 0 To CType(ctrl, C1.Win.C1TrueDBGrid.C1TrueDBGrid).Columns.Count - 1
                            Dim cntrlKey As String = String.Empty
                            constructKey(ctrl, cntrlKey, formName, CType(ctrl, C1.Win.C1TrueDBGrid.C1TrueDBGrid).Columns(i).ToString)
                            If getValueByKey(cntrlKey.ToLower) <> String.Empty Then
                                CType(ctrl, C1.Win.C1TrueDBGrid.C1TrueDBGrid).Columns(i).Caption = getValueByKey(cntrlKey.ToLower)
                            End If
                        Next
                    End If

                    If TypeOf ctrl Is C1.Win.C1List.C1List Then
                        For Each splCol As C1.Win.C1List.Split In CType(ctrl, C1.Win.C1List.C1List).Splits
                            For i As Integer = 0 To splCol.DisplayColumns.Count - 1
                                Dim cntrlKey As String = String.Empty
                                constructKey(ctrl, cntrlKey, formName, splCol.DisplayColumns(i).Name)
                                If getValueByKey(cntrlKey.ToLower) <> String.Empty Then
                                    splCol.DisplayColumns(i).DataColumn.Caption = getValueByKey(cntrlKey.ToLower)
                                End If
                            Next
                        Next
                    End If

                    'Menu
                    If TypeOf ctrl Is MenuStrip Then
                        For Each tsmitem As ToolStripMenuItem In CType(ctrl, MenuStrip).Items
                            If getValueByKey((formName & "." & tsmitem.Name).ToLower) <> String.Empty Then
                                tsmitem.Text = getValueByKey((formName & "." & tsmitem.Name).ToLower)
                            End If
                            For Each mitem As Object In tsmitem.DropDownItems
                                If TypeOf mitem Is ToolStripMenuItem Then
                                    If getValueByKey((formName & "." & CType(mitem, ToolStripMenuItem).Name).ToLower) <> String.Empty Then
                                        CType(mitem, ToolStripMenuItem).Text = getValueByKey((formName & "." & CType(mitem, ToolStripMenuItem).Name).ToLower)
                                    End If
                                    For Each submitem As Object In mitem.DropDownItems
                                        If TypeOf submitem Is ToolStripMenuItem Then
                                            If getValueByKey((formName & "." & CType(submitem, ToolStripMenuItem).Name).ToLower) <> String.Empty Then
                                                CType(submitem, ToolStripMenuItem).Text = getValueByKey((formName & "." & CType(submitem, ToolStripMenuItem).Name).ToLower)
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        Next
                    End If

                    If TypeOf ctrl Is TabPage Or TypeOf ctrl Is GroupBox Or TypeOf ctrl Is C1.Win.C1Command.C1DockingTabPage Then
                        Dim cntrlKey As String = String.Empty
                        constructKey(ctrl, cntrlKey, formName)
                        If getValueByKey(cntrlKey.ToLower) <> String.Empty Then
                            ctrl.Text = getValueByKey(cntrlKey.ToLower)
                        End If
                    End If

                    If ctrl.Controls.Count > 0 Then
                        SetCulture(ctrl, formName, rbCntrl)
                    End If
                Next
            End If

            'Ribbon
            If Not resourceMgr Is Nothing Then
                If Not rbCntrl Is Nothing Then
                    For Each rbctrl As C1.Win.C1Ribbon.RibbonTab In rbCntrl.Tabs
                        If getValueByKey((formName & "." & rbctrl.Name).ToLower) <> String.Empty Then
                            rbctrl.Text = getValueByKey((formName & "." & rbctrl.Name).ToLower)
                        End If
                        For Each rbgrp As C1.Win.C1Ribbon.RibbonGroup In rbctrl.Groups
                            If getValueByKey((formName & "." & rbgrp.Name).ToLower) <> String.Empty Then
                                rbgrp.Text = getValueByKey((formName & "." & rbgrp.Name).ToLower)
                            End If
                            For Each rbitm As C1.Win.C1Ribbon.RibbonItem In rbgrp.Items
                                If rbitm.GetType.ToString = "C1.Win.C1Ribbon.RibbonButton" Then
                                    If getValueByKey((formName & "." & rbitm.Name).ToLower) <> String.Empty Then
                                        CType(rbitm, C1.Win.C1Ribbon.RibbonButton).Text = getValueByKey((formName & "." & rbitm.Name).ToLower)
                                    End If
                                ElseIf rbitm.GetType.ToString = "C1.Win.C1Ribbon.RibbonLabel" Then
                                    If getValueByKey((formName & "." & rbitm.Name).ToLower) <> String.Empty Then
                                        CType(rbitm, C1.Win.C1Ribbon.RibbonLabel).Text = getValueByKey((formName & "." & rbitm.Name).ToLower)
                                    End If
                                End If
                            Next
                        Next
                    Next

                    For Each rbitm As C1.Win.C1Ribbon.RibbonItem In rbCntrl.ConfigToolBar.Items
                        If rbitm.GetType.ToString = "C1.Win.C1Ribbon.RibbonLabel" Then
                            If getValueByKey((formName & "." & rbitm.Name).ToLower) <> String.Empty Then
                                CType(rbitm, C1.Win.C1Ribbon.RibbonLabel).Text = getValueByKey((formName & "." & rbitm.Name).ToLower)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Public Function GetAllControls(ByRef container As Control, ByVal controlList As List(Of Control)) As List(Of Control)

        Try
            For Each control As Control In container.Controls

                If (TypeOf control Is C1.Win.C1Input.C1TextBox OrElse TypeOf control Is C1.Win.C1Input.C1Button) Then
                    control.TabStop = True
                Else
                    control.TabStop = False
                End If

                control.TabIndex = 0
                controlList.Add(control)

                If (control.Controls.Count > 0) Then
                    controlList = GetAllControls(control, controlList)
                End If
            Next

            Return controlList
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

    Public Function GetAllControls(ByVal container As Control) As List(Of Control)
        Return GetAllControls(container, New List(Of Control))
    End Function

    Public Sub constructKey(ByRef cntrlControl As Control, ByRef strKey As String, ByVal strFrmName As String, Optional ByVal strColName As String = "")
        If strKey = String.Empty Then
            strKey = cntrlControl.Name
        End If

        If TypeOf cntrlControl Is C1FlexGrid Or TypeOf cntrlControl Is C1.Win.C1TrueDBGrid.C1TrueDBGrid Or TypeOf cntrlControl Is C1.Win.C1List.C1List Then
            strKey = strKey & "." & strColName
        End If

        If Not cntrlControl.Parent Is Nothing Then
            If cntrlControl.Parent.GetType.Name <> "C1Sizer" AndAlso cntrlControl.Parent.GetType.Name <> "GroupBox" AndAlso cntrlControl.Parent.GetType.Name <> "TableLayoutPanel" AndAlso cntrlControl.Parent.GetType.Name <> "CtrlTab" AndAlso cntrlControl.Parent.GetType.Name <> "C1DockingTabPage" AndAlso cntrlControl.Parent.GetType.Name <> "SplitContainer" AndAlso cntrlControl.Parent.GetType.Name <> "SplitterPanel" AndAlso cntrlControl.Parent.GetType.Name <> "TabPage" AndAlso cntrlControl.Parent.GetType.Name <> "TabControl" AndAlso cntrlControl.Parent.GetType.Name <> "Panel" AndAlso cntrlControl.Parent.GetType.Name <> "C1DockingTab" Then
                strKey = cntrlControl.Parent.Name & "." & strKey
            End If
        End If

        If Not cntrlControl.Parent.Parent Is Nothing AndAlso cntrlControl.Parent.Name <> strFrmName Then
            constructKey(cntrlControl.Parent, strKey, strFrmName, strColName)
        End If

    End Sub

    'Function returns the value form a resource file, when supplied with a key.
    Public Function getValueByKey(ByVal strKey As String) As String
        Try
            If Not resourceMgr Is Nothing Then
                Return resourceMgr.GetString(strKey)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return "Error"
        End Try


    End Function

    ''' <summary>
    ''' Generates Document No.
    ''' </summary>
    ''' <param name="strDocNo">Incomplete document no.</param>
    ''' <param name="iMaxLength">Max length of document no.</param>
    ''' <param name="strSuffix">Current incremented value to be suffixed</param>        
    ''' <remarks></remarks>
    ''' 
    Public Function GenDocNo(ByVal strDocNo As String, ByVal iMaxLength As Integer, ByVal strSuffix As String) As String
        If strDocNo.Length < iMaxLength Then
            Dim iTempLength As Integer = iMaxLength - strDocNo.Length
            For i As Integer = 0 To (iTempLength - strSuffix.Length) - 1
                strDocNo = strDocNo & "0"
            Next
            strDocNo = strDocNo & strSuffix
            Return strDocNo
        End If
        Return strDocNo
    End Function

    Public Sub AutoLogout(Optional ByVal strTranType As String = Nothing, Optional ByRef frmTrans As Object = Nothing, Optional ByRef rbLblUserName As Win.C1Ribbon.RibbonLabel = Nothing)
        If clsDefaultConfiguration.SignOnRequired Then
            LoginStatus = False
            Dim bAuth As Boolean = False
            Dim ChildForm As New frmNLogin
            If Not ChildForm Is Nothing Then
                ChildForm.StartPosition = FormStartPosition.CenterParent
                ChildForm.cboLanguage.SelectedValue = clsAdmin.LangCode
                ChildForm.txtusername.Focus()
                ChildForm.ShowDialog()
            End If
            If Not strTranType Is Nothing Then
                While bAuth = False
                    bAuth = CheckAuthorisation(clsAdmin.UserCode, strTranType)
                    If bAuth = False Then
                        If LoginStatus = False Then
                            bAuth = True
                        Else
                            ShowMessage(getValueByKey("SPCM001"), "SPCM001")
                            If TypeOf (frmTrans) Is Form Then
                                CType(frmTrans, Form).Close()
                            End If
                            bAuth = True
                            'ShowMessage("You have not Sufficent Rights", "Information")
                            'LoginStatus = False
                            'ChildForm = New frmNLogin
                            'If Not ChildForm Is Nothing Then
                            '    ChildForm.StartPosition = FormStartPosition.CenterParent
                            '    ChildForm.cboLanguage.SelectedValue = clsAdmin.LangCode
                            '    ChildForm.txtusername.Focus()
                            '    ChildForm.ShowDialog()
                            'End If

                        End If
                    End If
                End While
            End If
            If LoginStatus = False Then
                Application.Exit()
            End If
            If Not rbLblUserName Is Nothing Then
                rbLblUserName.Text = clsAdmin.UserName
            End If


        End If

    End Sub


    Public Function CheckIfBlank(ByVal strValue As String) As String
        Try
            If strValue <> Nothing AndAlso strValue <> String.Empty Then
                Return strValue
            Else
                Return "0"
            End If

        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try
    End Function

    Public Function ConvertToEnglish(ByVal dblvalue As Object) As String
        Try
            If Not dblvalue Is Nothing AndAlso Not dblvalue Is DBNull.Value Then
                Return CDbl(dblvalue).ToString("F2", CultureInfo.CreateSpecificCulture("en-US"))
            Else
                Return "0"
            End If
        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try
    End Function

    Public Function GetSystemDateFormat() As String
        Dim installed As CultureInfo = My.Computer.Info.InstalledUICulture
        Dim strShortDateFormat As String = installed.DateTimeFormat.ShortDatePattern.ToString
        Return strShortDateFormat

    End Function

    Public Function GetEnglishMonthNames(ByVal iMonth As Integer) As String
        Dim arrMonthNames() As String
        arrMonthNames = New String() {"JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "Oct", "NOV", "DEC"}
        Return arrMonthNames(iMonth - 1)

    End Function
    'End RS

#End Region

    ''' <summary>
    ''' Hides the Column's From Grid
    ''' </summary>
    ''' <param name="dgGrid">Name of the Grid</param>
    ''' <param name="show">True/False</param>
    ''' <usedin>
    ''' CashMemo,AddOtherChargeForSO,CashMemoReturn,CommonView
    ''' </usedin>
    '''<Updatedby></Updatedby>
    ''' <Updatedon></Updatedon>
    ''' <param name="colName">Column Name's</param>
    ''' <remarks></remarks>


    Public Sub HideColumns(ByVal dgGrid As C1FlexGrid, ByVal show As Boolean, ByVal ParamArray colName() As String)
        Try
            Dim i As Integer = 0
            For i = 0 To colName.Count - 1
                If Not dgGrid.Cols(colName.ElementAt(i)) Is Nothing Then
                    dgGrid.Cols(colName.ElementAt(i)).Visible = show
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Has been Created for License Popups
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="Caption"></param>
    ''' <remarks></remarks>
    Public Function ShowLicense(ByVal msg As String, ByVal Caption As String) As frmSpecialPrompt
        Try
            Dim objMess As New frmSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.LicenseMessage = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = False
            objMess.PictureBox1.Visible = False
            objMess.AcceptButton = objMess.cmdOk
            objMess.Show()
            objMess.TopMost = True
            objMess.Select()
            Return objMess
        Catch ex As Exception

        End Try

    End Function
    Public Function DayCloseSyncProgress(ByVal msg As String, ByVal Caption As String) As frmSpecialPrompt
        Try
            Dim objMess As New frmSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.LicenseMessage = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = False
            objMess.ShowHoverBox = True
            'objMess.PictureBox1.Visible = True
            objMess.AcceptButton = objMess.cmdOk
            objMess.pnlMessage.AutoSize = True
            objMess.TableLayoutPanel1.Size = New System.Drawing.Size(460, 225)
            objMess.gbDialog.Size = New System.Drawing.Size(454, 180)
            objMess.lblMessage.Size = New System.Drawing.Size(299, 137)
            objMess.PictureBox1.Location = New System.Drawing.Point(358, 46)
            objMess.ClientSize = New System.Drawing.Size(463, 225)
            objMess.Show()
            objMess.TopMost = True
            objMess.Select()
            Return objMess
        Catch ex As Exception

        End Try

    End Function
    ''' <summary>
    ''' Show Special Prompt Messages
    ''' </summary>
    '''<Updatedby></Updatedby>
    ''' <Updatedon></Updatedon>
    ''' <param name="msg">Message</param>
    ''' <param name="Caption">Caption</param>
    ''' <remarks></remarks>
    Public Sub ShowMessage(ByVal msg As String, ByVal Caption As String, Optional ByVal Continu As Boolean = False)
        Try
            Dim objMess = New frmSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.ShowMessage = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = False
            objMess.AcceptButton = objMess.cmdOk
            objMess.PictureBox1.Visible = False
            If Continu = False Then
                objMess.ShowDialog()
            Else
                objMess.Show()
                objMess.TopMost = True
                objMess.Select()
            End If

        Catch ex As Exception


        End Try




    End Sub
    Public Sub ShowFullScreenMessage(ByVal msg As String, ByVal Caption As String, Optional ByVal IsChangeFormSize As Boolean = False, Optional ByVal ChangedFormSize As Size = Nothing)
        Try
            Dim objMess = New frmFullScreenMessageBox()
            objMess.TopMost = False
            objMess.Text = Caption
            objMess.MessageLabel.Text = msg
            If IsChangeFormSize = True Then
                objMess.Size = ChangedFormSize
            End If
            objMess.ShowDialog()
        Catch ex As Exception
        End Try

    End Sub

    '' added by nikhil
    Public Sub ShowMessagePC(ByVal msg As String, ByVal Caption As String, ByRef EventType As Int32, ByVal EditName As String, ByVal okText As String)

        Dim objMess As New frmSpecialPromptPC(Caption)
        objMess.ShowTextBox = False
        objMess.ShowMessage = True
        objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
        objMess.lblMessage.TextAlign = ContentAlignment.MiddleLeft

        objMess.lblMessage.Text = msg

        objMess.PictureBox1.Visible = False
        objMess.CmdCancel.Visible = True
        objMess.CmdCancel.Text = EditName
        objMess.cmdOk.Text = okText

        objMess.TopMost = False
        objMess.ShowDialog()
        EventType = objMess.getEventType
    End Sub
    Public Sub ShowFailedLicenseMessage(ByVal msg As String, ByVal Caption As String, Optional ByVal Continu As Boolean = False)
        Try
            Dim objMess = New frmSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.ShowMessage = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = True
            objMess.AcceptButton = objMess.cmdOk
            objMess.PictureBox1.Visible = False
            If Continu = False Then
                objMess.ShowDialog()
            Else
                objMess.Show()
                objMess.TopMost = True
                objMess.Select()
            End If

        Catch ex As Exception

        End Try



    End Sub
    Public Sub ShowBigMessage(ByVal msg As String, ByVal Caption As String, Optional ByVal Continu As Boolean = False)
        Try
            Dim objMess = New frmBigSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.ShowMessage = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = False
            objMess.AcceptButton = objMess.cmdOk
            If Continu = False Then
                objMess.ShowDialog()
            Else
                objMess.Show()
                objMess.TopMost = True
                objMess.Select()
            End If

        Catch ex As Exception

        End Try

    End Sub
    Public Sub ShowBigMessagewithOK(ByVal msg As String, ByVal Caption As String, Optional ByVal Continu As Boolean = False, Optional ByVal isEsc As Boolean = False)
        Try
            Dim objMess = New frmBigSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.ShowMessage = True
            objMess.cmdOk.Visible = True
            objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = True
            objMess.AcceptButton = objMess.cmdOk
            If Continu = False Then
                objMess.ShowDialog()
            Else
                objMess.Show()
                objMess.TopMost = True
                objMess.Select()
            End If

        Catch ex As Exception

        End Try

    End Sub
    Public Sub ShowMessage(ByVal isBiggerFont As Boolean, ByVal msg As String, ByVal Caption As String)
        Try
            Dim objMess As New frmSpecialPrompt(Caption)
            objMess.ShowTextBox = False
            objMess.ShowMessage = True
            If isBiggerFont Then
                objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
            End If
            objMess.lblMessage.Text = msg
            objMess.cmdHold.Visible = False
            objMess.TopMost = False
            objMess.AcceptButton = objMess.cmdOk
            objMess.PictureBox1.Visible = False
            'If Continu = False Then
            objMess.ShowDialog()
            'Else
            '    objMess.Show()
            '    objMess.TopMost = True
            '    objMess.Select()
            'End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function validateEmailId(ByVal emailid As String) As Boolean
        Try
            Dim AthIndex, DotIndex, TotalLength As Int16
            AthIndex = emailid.IndexOf("@")
            DotIndex = emailid.LastIndexOf(".")
            TotalLength = emailid.Length
            If AthIndex <= 0 Or DotIndex <= 0 Or AthIndex = TotalLength Or DotIndex = TotalLength Or AthIndex > DotIndex Or DotIndex = (AthIndex + 1) Then
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ShowMessage(ByVal msg As String, ByVal Caption As String, ByRef EventType As Int32, Optional ByVal Edit As Boolean = False, Optional ByVal EditName As String = "", Optional ByVal OKButtonText As String = "")
        Dim objMess As New frmSpecialPrompt(Caption)
        objMess.ShowTextBox = False
        objMess.ShowMessage = True
        objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
        objMess.lblMessage.Text = msg
        objMess.PictureBox1.Visible = False
        If Edit = True Then
            objMess.CmdCancel.Visible = True
            objMess.CmdCancel.Text = EditName
        Else
            objMess.cmdHold.Visible = True
            objMess.CmdCancel.Visible = True
            objMess.cmdOk.Text = getValueByKey("mod009")
        End If

        If (Not String.IsNullOrEmpty(OKButtonText)) Then
            objMess.cmdOk.Text = OKButtonText
        End If

        objMess.TopMost = False
        objMess.ShowDialog()
        EventType = objMess.getEventType
    End Sub

    Public Sub ShowMessage(ByVal msg As String, ByVal Caption As String, ByRef EventType As Int32, ByVal EditName As String, ByVal okText As String)
        Dim objMess As New frmSpecialPrompt(Caption)
        objMess.ShowTextBox = False
        objMess.ShowMessage = True
        objMess.lblMessage.Font = New Font("Arial", 12.5, FontStyle.Bold)
        objMess.lblMessage.Text = msg
        objMess.PictureBox1.Visible = False
        objMess.CmdCancel.Visible = True
        objMess.CmdCancel.Text = EditName
        objMess.cmdOk.Text = okText

        objMess.TopMost = False
        objMess.ShowDialog()
        EventType = objMess.getEventType
    End Sub

    ''' <summary>
    ''' Data attach to combo box
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="dtCombo">Data table</param>
    ''' <param name="ObjComboBox">Name of ComboBox</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PopulateComboBox(ByVal dtCombo As DataTable, ByRef ObjComboBox As ctrlCombo)

        ObjComboBox.DataSource = dtCombo
        ObjComboBox.ValueMember = dtCombo.Columns(0).ColumnName
        ObjComboBox.DisplayMember = dtCombo.Columns(1).ColumnName
        'ObjComboBox.SelectedIndex = -1

        Return ""
    End Function
    ''' <summary>
    ''' Populate a column in grid as a combobox 
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="FlexGrid">Name of the grid</param>
    ''' <param name="dtGrid">Data Table</param>
    ''' <param name="vColName">Name of the Column</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PopulateGridComboBox(ByRef FlexGrid As C1FlexGrid, ByRef dtGrid As DataTable, ByVal vColName As String)

        Dim hashTable As New Hashtable()
        Dim dataRow As DataRow
        For Each dataRow In dtGrid.Rows
            hashTable.Add(dataRow(0), dataRow(1))
        Next
        FlexGrid.Cols(vColName).DataMap = hashTable
        Return ""
    End Function
    ''' <summary>
    ''' Creating New Records for the Binding Datatable
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="frm">form</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="Tablename">Name of the table's</param>
    ''' <remarks></remarks>
    Public Sub CreateNewRecord(ByRef frm As Form, ByRef ds As DataSet, ByVal ParamArray Tablename() As String)
        Dim i As Integer
        If Tablename.Length > 0 Then
            For i = 0 To Tablename.Length - 1
                frm.BindingContext(ds.Tables(Tablename(i).ToString())).AddNew()
            Next
        End If
    End Sub
    ''' <summary>
    ''' Creating New Records for the Binding Datatable
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="frm">form</param>
    ''' <param name="dt">DataTable</param>
    ''' <param name="Tablename">Name of the table's</param>
    ''' <remarks></remarks>
    Public Sub CreateNewRecord(ByRef frm As Form, ByRef dt As DataTable, ByVal Tablename As String)
        dt.TableName = Tablename
        frm.BindingContext(dt).AddNew()
    End Sub
    ''' <summary>
    ''' Get the details of User authorisation of Transaction
    ''' </summary>
    ''' <usedin>Login</usedin>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <param name="UserId">UserID</param>
    ''' <remarks></remarks>
    Public Sub GetUserAuthDetails(ByVal SiteCode As String, ByVal UserId As String, Optional ByRef dtOtherUserAuth As DataTable = Nothing)
        Try
            Dim dtUserRoles As DataTable
            'Dim dtAuthSpecialTransaction As DataTable
            Dim objAuth As New clsAuthorization()
            dtUserRoles = objAuth.GetuserRoles(SiteCode, UserId)
            If Not dtUserRoles Is Nothing AndAlso dtUserRoles.Rows.Count > 0 Then
                Dim StrDistnctRoles As String = String.Empty
                For Each dr As DataRow In dtUserRoles.Rows
                    If dr("Groleid").ToString().Contains(",") Then
                        For Each Str As String In dr("Groleid").ToString().Split(",")
                            StrDistnctRoles = StrDistnctRoles & "'" & Str & "',"
                        Next
                    Else
                        StrDistnctRoles = StrDistnctRoles & "'" & dr("Groleid").ToString() & "',"
                    End If
                Next
                StrDistnctRoles = StrDistnctRoles.Substring(0, StrDistnctRoles.Length - 1)

                If StrDistnctRoles.Length > 0 Then
                    If dtOtherUserAuth Is Nothing Then
                        dtAuthUserTran = objAuth.GetSiteAllowedTransaction(SiteCode, StrDistnctRoles, UserId)
                    Else
                        dtOtherUserAuth = objAuth.GetSiteAllowedTransaction(SiteCode, StrDistnctRoles, UserId)
                    End If
                End If
            End If
            'dtAuthSpecialTransaction = objAuth.GetSpecialAuthorization(SiteCode, UserId)
            'If Not dtAuthSpecialTransaction Is Nothing AndAlso dtAuthSpecialTransaction.Rows.Count > 0 Then
            '    If Not dtOtherUserAuth Is Nothing Then
            '        If dtOtherUserAuth.Rows.Count > 0 Then
            '            dtOtherUserAuth.Merge(dtAuthSpecialTransaction, False, MissingSchemaAction.Ignore)
            '        Else
            '            dtOtherUserAuth = dtAuthSpecialTransaction
            '        End If

            '    Else
            '        If dtAuthUserTran Is Nothing Then
            '            dtAuthUserTran = dtAuthSpecialTransaction
            '        Else
            '            dtAuthUserTran.Merge(dtAuthSpecialTransaction, False, MissingSchemaAction.Ignore)
            '        End If
            '    End If

            'End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CLMCF01"), "CLMCF01 - " & getValueByKey("CLAE05"))
        End Try
    End Sub
    ''' <summary>
    ''' Check Authorisation for User against transactions
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="Userid">UserId</param>
    ''' <param name="Transaction">Transactions</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckAuthorisation(ByVal Userid As String, ByVal Transaction As String) As Boolean
        Try
            CheckAuthorisation = False
            If Userid.ToUpper = clsAdmin.UserCode.ToUpper Then
                If Not dtAuthUserTran Is Nothing AndAlso dtAuthUserTran.Rows.Count > 0 Then
                    Dim dv As New DataView(dtAuthUserTran, "AUTHTRANSACTIONCODE='" & Transaction & "' AND Rights = 1", "", DataViewRowState.CurrentRows)
                    If (dv.Count > 0 AndAlso dv(0)("Rights")) Then
                        CheckAuthorisation = True
                    Else
                        CheckAuthorisation = False
                    End If
                Else
                    CheckAuthorisation = False
                End If
            ElseIf Userid <> clsAdmin.UserCode Then
                Dim dtAuth As New DataTable
                GetUserAuthDetails(clsAdmin.SiteCode, Userid, dtAuth)
                If Not dtAuth Is Nothing AndAlso dtAuth.Rows.Count > 0 Then
                    Dim dv As New DataView(dtAuth, "AUTHTRANSACTIONCODE='" & Transaction & "' AND Rights = 1", "", DataViewRowState.CurrentRows)
                    If (dv.Count > 0 AndAlso dv(0)("Rights")) Then
                        CheckAuthorisation = True
                    Else
                        CheckAuthorisation = False
                    End If
                Else
                    CheckAuthorisation = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function CheckAuthorisationForTran(ByVal sitecode As String, ByVal TransactionCode As String) As Boolean
        Try
            Dim dtAuth As New DataTable
            Dim clsAuth As New clsAuthorization
            Dim KdsDetails As New DataTable
            KdsDetails = clsAuth.GetSitedAllowedTran(sitecode, TransactionCode)
            If Not KdsDetails Is Nothing AndAlso KdsDetails.Rows.Count > 0 Then
                CheckAuthorisationForTran = True
            Else
                CheckAuthorisationForTran = False
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Creating Pdf 
    ''' </summary>
    '''<usedin>CashMemo</usedin>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="Title">Page Title</param>
    ''' <param name="StrPrint">String For Print</param>
    ''' <remarks></remarks>
    Public Sub CreatePdf(ByVal Title As String, ByVal StrPrint As String)
        Try
            Dim str() As String = StrPrint.Split("\t")
            _Pdf = New C1PdfDocument()
            _Pdf.Clear()
            _Pdf.DocumentInfo.Title = Title
            Dim titleFont As Font = New Font("Verdana", 10, FontStyle.Bold)
            Dim bodyFont As Font = New Font("Verdana", 9)
            Dim rcPage As RectangleF = GetPageRect()
            Dim rc As RectangleF = RenderParagraph(_Pdf.DocumentInfo.Title, titleFont, rcPage, rcPage, False, False)
            rc.Y += titleFont.Size + 6
            rc.Height = rcPage.Height '- rc.Y
            ' render string spanning columns and pages 
            For Each StrString As String In str
                'While True
                Dim X As Double = 102
                Dim nextChar As Integer = _Pdf.DrawString(StrString, bodyFont, Brushes.Black, rc)
                'If nextChar <= X Then
                '    rc.Y = rc.Y + bodyFont.Size   '+ 10
                'Else
                '    Dim i As Integer
                '    For i = 0 To nextChar Step X
                '        rc.Y = rc.Y + (bodyFont.Size)  '+ 10
                '    Next
                '    rc.Y = rc.Y + bodyFont.Size
                'End If

                'If rc.Y >= rc.Height Then
                '    _Pdf.NewPage()
                '    rc.Y = 72
                'End If
            Next
            Dim fileName As String = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\invoice.pdf"
            _Pdf.Save(fileName)
            Process.Start(fileName)

        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' check User Authorisation of a transaction
    ''' </summary>
    ''' <param name="Transaction">Transaction Name</param>
    ''' <param name="dt">DataTable</param>
    ''' <param name="isFormCancel">Check Discount is apply to birthlist or not
    '''  True:Discount apply 
    ''' False :Discount canceled
    ''' </param>
    ''' <returns></returns>
    ''' <remarks> if current user have no authorisation goes for other user check</remarks>
    Public Function CheckInterTransactionAuth(ByVal Transaction As String, Optional ByRef dt As DataTable = Nothing, Optional ByVal decTotalAmount As Decimal = Decimal.Zero, Optional ByVal decTotalOpenAmount As Decimal = Decimal.Zero, Optional ByRef decTotalDiscount As Decimal = Decimal.Zero, Optional ByVal promotionid As String = "", Optional ByRef isFormCancel As Boolean = False, Optional ByRef strIssueType As String = "", Optional ByVal FilterCond As String = "", Optional ByRef decTotalDiscountPercentage As Decimal = 0.0, Optional ByRef remarks As String = "", Optional ByRef isPromoSelected As Boolean = False, Optional ByRef authUserID As String = "", Optional ByRef authRemarks As String = "", Optional ByVal baltoPay As Decimal = Decimal.Zero, Optional ByVal TotalPickUpDisc As DataTable = Nothing, Optional ByVal isNewSO As Boolean = False, Optional ByVal DiscPer As Double = 0.0, Optional ByVal DiscVal As Double = 0.0, Optional ByVal IsCalledFromCashMemo As Boolean = False) As Boolean
        Try
            If CheckAuthorisation(clsAdmin.UserCode, Transaction) = False Then
                Dim objAuth As New frmNUserAuthorisation(Transaction, dt)

                If (Transaction = TransactionTypes.BirthListUpdate.ToString()) Then
                    TransactionType = TransactionTypes.BirthListUpdate
                    objAuth.TotalAmount = decTotalAmount
                    objAuth.TotalOpenAmount = decTotalOpenAmount
                End If
                objAuth.PromotionId = promotionid
                objAuth.TotalPickUpDisc = TotalPickUpDisc
                objAuth.IsNewSalaesOrder = isNewSO
                objAuth.DiscountInPer = DiscPer
                objAuth.DiscountInVal = DiscVal
                If baltoPay <> Decimal.Zero Then
                    objAuth.balancetoPay = baltoPay
                End If
                If FilterCond <> String.Empty Then
                    objAuth.Extrafilter = FilterCond
                End If
                objAuth.TotalAmount = decTotalAmount
                objAuth.TotalOpenAmount = decTotalOpenAmount
                objAuth.remarks.Text = remarks
                objAuth.ShowDialog()
                isPromoSelected = objAuth.IsPromoSelected
                If isPromoSelected Then
                    remarks = objAuth.remarks.Text
                End If
                authUserID = objAuth.txtUserId.Text
                authRemarks = objAuth.txtRemarks.Text

                decTotalDiscount = objAuth.TotalDiscountAmount
                strIssueType = objAuth.IssueType '******** Added By Rahul . To Check Issue Document Type  
                isFormCancel = objAuth.IsFormCancel  '******** Added By Rahul . To Check discount is apply to birthlist   

                decTotalDiscountPercentage = objAuth.DiscountPercentage
                If objAuth.Authorized = False Then
                    Return False
                Else
                    Return True
                End If
            Else
                Dim objAuth As New frmNUserAuthorisation(Transaction, dt)
                objAuth.txtUserId.Text = clsAdmin.UserCode
                objAuth.SetAuthorization = True
                objAuth.PromotionId = promotionid
                objAuth.TotalPickUpDisc = TotalPickUpDisc
                objAuth.IsNewSalaesOrder = isNewSO
                objAuth.DiscountInPer = DiscPer
                objAuth.DiscountInVal = DiscVal
                objAuth.TotalAmount = decTotalAmount
                objAuth.TotalOpenAmount = decTotalOpenAmount
                If FilterCond <> String.Empty Then
                    objAuth.Extrafilter = FilterCond
                End If
                If baltoPay <> Decimal.Zero Then
                    objAuth.balancetoPay = baltoPay
                End If
                objAuth.remarks.Text = remarks
                objAuth.isPromotionsDetailClearRequired = isPromotionsDetailClearRequired
                '--- 05042016 changed by sagar for flickering issue (MOD)
                'objAuth.ShowDialog()
                Dim objRemarks As New frmArticlesRemark

                If Transaction = "DeleteBill" Then
                    If IsCalledFromCashMemo = True Then
                        objRemarks.IsKOTReason = True

                        Dim dialogresult = objRemarks.ShowDialog()
                        If dialogresult = Windows.Forms.DialogResult.OK Then
                            authRemarks = objRemarks.KOTReasonDetails
                        Else
                            Return False
                        End If
                    End If
                   


                ElseIf Transaction <> "ReprintCM" Then
                    objAuth.ShowDialog()
            End If
            isPromoSelected = objAuth.IsPromoSelected
            If isPromoSelected Then
                remarks = objAuth.remarks.Text
            End If
            decTotalDiscount = objAuth.TotalDiscountAmount
                _NewTotalDiscount = objAuth.NewDiscountAmount 'vipin
            strIssueType = objAuth.IssueType '******** Add By Rahul . To Check discount is apply to birthlist   
            isFormCancel = objAuth.IsFormCancel  '******** Add By Rahul . To Check discount is apply to birthlist  
            decTotalDiscountPercentage = objAuth.DiscountPercentage
            If objAuth.Authorized = True Then
                Return True
            End If
            Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub SetCurrentculture(ByVal Culture As String)
        Try
            Dim ci = New CultureInfo(clsAdmin.CultureInfo)
            ''add by ram this take care of calendar to specific culture.
            'Thread.CurrentThread.CurrentCulture = ci
            ''end for add by ram 
            Thread.CurrentThread.CurrentUICulture = ci

            Dim ResourceFilePath As String = Application.StartupPath
            ResourceFilePath = ResourceFilePath & "\MyResource"
            resourceMgr = ResourceManager.CreateFileBasedResourceManager("MyResource", ResourceFilePath, Nothing)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    ''' <summary>
    ''' Function for Printing 
    ''' </summary>
    ''' <param name="StrToPrint">String to print</param>
    ''' <param name="strPrintOrPreview">PRN/PRV</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fnPrint(ByVal StrToPrint As String, ByVal strPrintOrPreview As String) As String
        ' strPrintOrPreview = 'PRN' that means Print
        ' strPrintOrPreview = 'PRV' that means Preview
        stringToPrint = StrToPrint
        printDocument1.DefaultPageSettings.Margins.Left = 0
        printDocument1.DefaultPageSettings.Margins.Right = 1
        Try
            printDocument1.PrinterSettings.PrinterName = gPrinterName
            If strPrintOrPreview = "PRN" Then
                printDocument1.Print()
            Else
                printPreviewDialog1.Document = printDocument1
                printPreviewDialog1.Select()
                printPreviewDialog1.ShowDialog()
            End If
            fnPrint = "Success"

            If [String].IsNullOrEmpty(gOposPrinter.ErrorString) Then
                InitializeDevices()
                gOposPrinter.Open()
                gOposPrinter.Claim(1000)
                gOposPrinter.AsyncMode = False
                gOposPrinter.DeviceEnabled = True
                gOposPrinter.PrintNormal(PrinterStation.Receipt, StrToPrint)
                gOposPrinter.PrintNormal(PrinterStation.Receipt, Chr(13).ToString + Chr(10).ToString)
                gOposPrinter.PrintNormal(PrinterStation.Receipt, Chr(13).ToString + Chr(10).ToString)
                'gOposPrinter.PrintNormal(PrinterStation.Receipt, Chr(13).ToString + Chr(10).ToString)
                'gOposPrinter.PrintNormal(PrinterStation.Receipt, Chr(13).ToString + Chr(10).ToString)
                'gOposPrinter.PrintNormal(PrinterStation.Receipt, Chr(13).ToString + Chr(10).ToString)
                gOposPrinter.CutPaper(90)
                gOposPrinter.DeviceEnabled = False
                gOposPrinter.Release()
                gOposPrinter.Close()
            Else
                ShowMessage(gOposPrinter.ErrorString, getValueByKey("CLMCF05"))
            End If

        Catch ex As Exception

            gOposPrinter.DeviceEnabled = False
            gOposPrinter.Release()
            gOposPrinter.Close()

            If printDocument1.PrinterSettings.IsValid = False Then
                ShowMessage(getValueByKey("CLMCF06"), "CLMCF06 - " & getValueByKey("CLAE04"))
            End If
            fnPrint = "Failed"
        End Try
    End Function

    'Public Function fnMakeTitle(Optional ByVal strTitleLeft As String = "", Optional ByVal strTitleMiddle As String = "", Optional ByVal strTitleRight As String = "") As String

    '    If strTitleLeft IsNot Nothing Then
    '        If strTitleLeft.Trim <> "" Then
    '            strTitleLeft = strTitleLeft.Trim
    '        Else
    '            strTitleLeft = "SPECTRUM"
    '        End If
    '        strTitle = strTitleLeft
    '    End If

    '    If strTitleMiddle IsNot Nothing Then
    '        If strTitleMiddle.Trim <> "" Then
    '            strTitleMiddle = strTitleMiddle.Trim
    '            strTitle = strTitle & "/" & strTitleMiddle
    '        End If
    '    End If

    '    If strTitleRight IsNot Nothing Then
    '        If strTitleMiddle.Trim <> "" Then
    '            strTitleRight = strTitleRight.Trim
    '            strTitle = strTitle & "/" & strTitleRight
    '        End If
    '    End If

    '    'strTitle = Space((_TitleSize - strTitle.Length) / 2) & strTitle & Space((_TitleSize - strTitle.Length) / 2)
    '    Return strTitle
    'End Function

    'Public Function Encrypt(ByVal sPasswordEnc As String) As String
    '    ''This Function gets the string as input and returns the encrypted value of the string
    '    Dim AscEncrypt As String
    '    Dim nCount As Integer
    '    Dim sChar As String
    '    Dim strEncrypt As String = ""
    '    For nCount = 1 To Len(sPasswordEnc)
    '        sChar = Mid(sPasswordEnc, nCount, 1)
    '        AscEncrypt = Chr(Asc(sChar) - Asc(30))
    '        strEncrypt = strEncrypt & AscEncrypt
    '    Next
    '    Return strEncrypt
    'End Function

    'Public Function Decrypt(ByVal sPasswordDec As String) As String
    '    ''This Function gets the encrypted string as input and returns the decrypted value of the string
    '    Dim AscDecrypt As String
    '    Dim nCount As Integer
    '    Dim sChar As String
    '    Dim strDecrypt As String = ""
    '    sPasswordDec = Trim(sPasswordDec)
    '    For nCount = 1 To Len(sPasswordDec)
    '        sChar = Mid(sPasswordDec, nCount, 1)
    '        AscDecrypt = Chr(Asc(sChar) + Asc(30))
    '        strDecrypt = strDecrypt & AscDecrypt
    '    Next
    '    Return strDecrypt
    'End Function
    Public Function Encrypt(ByVal sPasswordEnc As String) As String
        ''This Function gets the string as input and returns the encrypted value of the string
        Dim AscEncrypt As String
        Dim nCount As Integer
        Dim sChar As String
        Dim strEncrypt As String = ""
        For nCount = 1 To Len(sPasswordEnc)
            sChar = Mid(sPasswordEnc, nCount, 1)
            AscEncrypt = ChrW(AscW(sChar) - AscW(30))
            strEncrypt = strEncrypt & AscEncrypt
        Next
        Return strEncrypt
    End Function

    Public Function Decrypt(ByVal sPasswordDec As String) As String
        ''This Function gets the encrypted string as input and returns the decrypted value of the string
        Dim AscDecrypt As String
        Dim nCount As Integer
        Dim sChar As String
        Dim strDecrypt As String = ""
        sPasswordDec = Trim(sPasswordDec)
        For nCount = 1 To Len(sPasswordDec)
            sChar = Mid(sPasswordDec, nCount, 1)
            AscDecrypt = ChrW(AscW(sChar) + AscW(30))
            strDecrypt = strDecrypt & AscDecrypt
        Next
        Return strDecrypt
    End Function
    Public Sub DisplayText(ByVal Text As String)
        Try
            If Not gOposPolDisplay Is Nothing Then
                gOposPolDisplay.ClearText()
                gOposPolDisplay.DisplayText(Text)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Function COMPortRead(ByVal PortName) As String
        Dim values = ""
        Dim port As New SerialPort()
        Try
            port.PortName = PortName
            port.BaudRate = 9600
            port.Parity = Parity.None
            port.DataBits = 8
            port.StopBits = StopBits.One
            port.Handshake = Handshake.None
            port.ReadTimeout = 5000
            port.WriteTimeout = 5000

            If port.IsOpen Then
                port.Close()
            End If
            If Not port.IsOpen Then
                port.Open()
            End If
            values = port.ReadLine()
            port.Dispose()

        Catch ex As Exception
            port.Dispose()
            LogException(ex)
        End Try
        Return values
    End Function


    Dim _TitleLeft As String
    Public Property TitleLeft() As String
        Get
            Return _TitleLeft
        End Get
        Set(ByVal value As String)
            _TitleLeft = value
        End Set
    End Property

    Dim _TitleMiddle As String
    Public Property TitleMiddle() As String
        Get
            Return _TitleMiddle
        End Get
        Set(ByVal value As String)
            _TitleMiddle = value
        End Set
    End Property
    Dim _TitleRight As String
    Public Property TitleRight() As String
        Get
            Return _TitleRight
        End Get
        Set(ByVal value As String)
            _TitleRight = value
        End Set
    End Property

    Public Structure gStrucLng
        Private lngName As String
        Private lngID As String

        Public Sub New(ByVal name As String, ByVal id As String)
            lngName = name
            lngID = id
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return lngName
            End Get
        End Property

        Public ReadOnly Property Id() As String
            Get
                Return lngID
            End Get
        End Property
    End Structure

    Public Sub SetConnection()
        Try
            Dim strcon, pass As String
            'pass = My.Settings.Password
            pass = ReadSpectrumParamFile("Password")
            'pass = Decrypt(pass)
            strcon = "Server= " & ReadSpectrumParamFile("Server") & ";DataBase=" & ReadSpectrumParamFile("DataSource") & ";Uid=" & ReadSpectrumParamFile("UserId") & ";pwd=" & pass & ";Connection Timeout=5;"
            DataBaseConnection._OnlineConn = strcon
            'My.Settings.ServerConnectionString = strcon
            My.Settings.Save()
            'Change
            CreateSpectrumParamFile("ServerConnectionString", strcon)
            'End of change
            Dim objlog As New clsLogin

            Dim objPrintDbConnectionBuild As New SpectrumPrint.DbConnectionBuild
            If objlog.SetConnection(strcon) = False Then
                ShowMessage(getValueByKey("CLMCF02"), "CLMCF02 - " & getValueByKey("CLAE04"))
            Else
                If objPrintDbConnectionBuild.SetConnection(strcon) = False Then
                    ShowMessage(getValueByKey("CLMCF03"), "CLMCF03 - " & getValueByKey("CLAE04"))
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SetConnection(ByVal Restablising As Boolean)
        Try
            Dim strcon, pass As String
            'pass = My.Settings.Password
            pass = ReadSpectrumParamFile("Password")
            pass = Decrypt(pass)
            strcon = "Server= " & ReadSpectrumParamFile("Server") & ";DataBase=" & ReadSpectrumParamFile("DataSource") & ";Uid=" & ReadSpectrumParamFile("UserId") & ";pwd=" & pass & ";Connection Timeout=5;"
            DataBaseConnection._OnlineConn = strcon
            'My.Settings.ServerConnectionString = strcon
            'Change
            CreateSpectrumParamFile("ServerConnectionString", strcon)
            'End of change

            Dim objlog As New clsLogin
            If objlog.SetConnection(strcon) = False Then
                ShowMessage(getValueByKey("CLMCF02"), "CLMCF02 - " & getValueByKey("CLAE04"))
            Else
                'ClearallClients()
                'ShowMessage("Connection with server set Properly", "Information")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub SetLocalConnection()
        Try
            Dim strcon, pass As String
            DataBaseConnection._OfflineConn = strcon
            'strcon = My.Settings.LocalConnectionString
            strcon = ReadSpectrumParamFile("LocalConnectionString")
            'pass = My.Settings.Password
            'pass = Decrypt(pass)
            'strcon = "Server= " & My.Settings.Server & ";DataBase=" & My.Settings.DataSource & ";Uid=" & My.Settings.UserId & ";pwd=" & pass
            Dim objlog As New clsLogin
            If objlog.SetConnection(strcon) = False Then
                ShowMessage(getValueByKey("CLMCF02"), "CLMCF02 - " & getValueByKey("CLAE04"))
            Else

                'ClearallClients()
                'ShowMessage("Local Connection Set Properly", "Information")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ClearallClients()
        Try
            For Each frm As Form In MDISpectrum.MdiChildren
                frm.Close()
            Next
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Private Method's & Function's"

    ''' <summary>
    ''' Printing the Doucment
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
       ByVal e As PrintPageEventArgs) Handles printDocument1.PrintPage

        Dim MyFont As New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Dim charactersOnPage As Integer = 0
        Dim linesPerPage As Integer = 0
        If Not VarBarcode Is Nothing AndAlso VarBarcode.Text <> String.Empty Then
            e.Graphics.DrawImage(VarBarcode.Image, 0, 0)
        End If
        ' Sets the value of charactersOnPage to the number of characters 
        ' of stringToPrint that will fit within the bounds of the page.
        e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, _
            StringFormat.GenericTypographic, charactersOnPage, linesPerPage)

        ' Draws the string within the bounds of the page.
        e.Graphics.DrawString(stringToPrint, MyFont, Brushes.Black, _
            e.MarginBounds, StringFormat.GenericTypographic)

        ' Remove the portion of the string that has been printed.
        stringToPrint = stringToPrint.Substring(charactersOnPage)

        ' Check to see if more pages are to be printed.
        e.HasMorePages = stringToPrint.Length > 0

        ' If there are no more pages, reset the string to be printed.
        If Not e.HasMorePages Then
            stringToPrint = documentContents
        End If

    End Sub
    Friend Function GetPageRect() As RectangleF
        Dim rcPage As RectangleF = _Pdf.PageRectangle
        rcPage.Inflate(-72, -72)
        Return rcPage
    End Function
    Friend Function RenderParagraph(ByVal text As String, ByVal font As Font, ByVal rcPage As RectangleF, ByVal rc As RectangleF, ByVal outline As Boolean, ByVal linkTarget As Boolean) As RectangleF
        ' if it won't fit this page, do a page break 
        rc.Height = _Pdf.MeasureString(text, font, rc.Width).Height
        If rc.Bottom > rcPage.Bottom Then
            _Pdf.NewPage()
            rc.Y = rcPage.Top
        End If

        ' draw the string 
        _Pdf.DrawString(text, font, Brushes.Black, rc)

        ' show bounds (mainly to check word wrapping) 
        '_c1pdf.DrawRectangle(Pens.Sienna, rc); 

        ' add headings to outline 
        If outline Then
            _Pdf.DrawLine(Pens.Black, rc.X, rc.Y, rc.Right, rc.Y)
            _Pdf.AddBookmark(text, 0, rc.Y)
        End If

        ' add link target 
        If linkTarget Then
            _Pdf.AddTarget(text, rc)
        End If

        ' update rectangle for next time 
        rc.Offset(0, rc.Height)
        Return rc
    End Function
#End Region
    ''' <summary>
    ''' Default currency  format 
    ''' </summary>
    ''' <param name="decTotalAmount"></param>
    ''' <UsedBy>BirthList all forms</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CurrencyFormat(ByVal decTotalAmount As Decimal) As String
        Try
            Dim decAMount As Double = 0
            decAMount = MyRound(CDbl(decTotalAmount), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
            Return CStr(decAMount)
        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try

    End Function
    Public Function MyRound(ByVal Amount As Double, ByVal RoundedAt As Int32, Optional ByVal IsRoundOffRequired As Boolean = True) As Double
        Try
            '--added by sagar new flag for round off issue at om ganesh
            If Not clsDefaultConfiguration.BillRoundOffNotRequired Then
                If IsRoundOffRequired Then
                    Dim Amt As Double = Amount
                    Amount = Math.Floor(Amount)
                    Amt = Amt - Amount
                    Amt = Math.Round(Amt, 2)
                    Amt = MyOwnRound(RoundedAt, Amt * 100)
                    Amount = Amount + (Amt * 0.01)
                    Return Amount
                Else
                    Return Amount
                End If
            End If
            Return Amount
        Catch ex As Exception
            Return Amount
        End Try
    End Function


    Private Function MyOwnRound(ByVal NumberToRound As Integer, _
                                ByVal ValueToRound As Integer) As Integer
        ' changed the datatype of "ValueToRound" from integer to double .
        'Changed by rahul katkar . for roundoff correction in cashmemo screen.

        Dim HalfRound As Integer = NumberToRound \ 2
        If NumberToRound < 0 Then
            NumberToRound = Math.Abs(NumberToRound)
        End If
        If NumberToRound = 0 Then Exit Function
        If ValueToRound Mod NumberToRound > HalfRound Then
            MyOwnRound = ValueToRound + (NumberToRound - (ValueToRound Mod NumberToRound))
        Else
            MyOwnRound = ValueToRound - (ValueToRound Mod NumberToRound)
        End If
    End Function
    Public Sub RemoveDeletedRow(ByRef dt As DataTable)
        Try
            Dim i As Int32
            For i = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i).RowState = DataRowState.Deleted Then
                    dt.Rows.RemoveAt(i)
                End If
            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    Public Function GridAmountColumnCustomeFormat() As String
        Return "0.00"
    End Function
    Public Function CalCulateCLP(ByVal CardType As String, ByRef dtItem As DataTable, ByVal ItemFilter As String) As Boolean
        Try
            Dim AccumType As String = ""
            Dim redType As String = ""
            Dim ds As DataSet
            Dim objCM As New clsCashMemo
            ds = objCM.GetCLPAccumlationDetails(clsAdmin.SiteCode, AccumType, redType)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim ConvAmt, ConvPoints, totalAmt, DiscountPer As Double
                Dim dv As New DataView(dtItem, ItemFilter, "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    Dim AllConvAmT, DiscPer, AllConvPoint As Double
                    Dim Items As String
                    Dim HFilter As String = ""
                    If AccumType.Contains("Fixed") Then
                        HFilter = "ISNULL(CARDTYPE,'')='" & CardType & "'"
                    ElseIf AccumType.Contains("Variable") Then
                        HFilter = "ISNULL(CARDTYPE,'')='" & CardType & "'"
                    End If
                    For Each headerPoint As DataRow In ds.Tables("Header").Select(HFilter, "", DataViewRowState.CurrentRows)
                        AllConvAmT = IIf(headerPoint("AMTVALUE") Is DBNull.Value, 0, headerPoint("AMTVALUE"))
                        AllConvPoint = IIf(headerPoint("Points") Is DBNull.Value, 0, headerPoint("Points"))
                        DiscPer = IIf(headerPoint("DiscountPer") Is DBNull.Value, 0, headerPoint("DiscountPer"))
                    Next
                    Dim dtItems As DataTable = dv.ToTable(True, "ArticleCode")
                    For Each ItemRow As DataRow In dtItems.Rows
                        Items = Items & "'" & ItemRow("ArticleCode") & "',"
                    Next
                    Items = Items.Substring(0, Items.Length - 1)
                    For Each ItemCLP As DataRow In ds.Tables("detail").Select(HFilter & " And ArticleCode in (" & Items & ")", "", DataViewRowState.CurrentRows)
                        Dim filter As String = dv.RowFilter
                        dv.RowFilter = filter & " And ArticleCode='" & ItemCLP("ArticleCode").ToString() & "'"
                        If Not ItemCLP("Flat") Is DBNull.Value AndAlso ItemCLP("Flat") = True Then
                            ConvPoints = IIf(ItemCLP("Points") Is DBNull.Value, 0, ItemCLP("Points"))
                            DiscountPer = IIf(ItemCLP("DiscountPer") Is DBNull.Value, 0, ItemCLP("DiscountPer"))
                            For Each Item As DataRowView In dv
                                totalAmt = Item("Quantity")
                                If ConvPoints > 0 Then
                                    Item("CLPPoints") = totalAmt * ConvPoints
                                ElseIf DiscountPer > 0 Then
                                    Item("CLPDiscount") = DiscountPer
                                End If
                            Next
                        Else
                            ConvAmt = IIf(ItemCLP("AMTVALUE") Is DBNull.Value, 0, ItemCLP("AMTVALUE"))
                            ConvPoints = IIf(ItemCLP("Points") Is DBNull.Value, 0, ItemCLP("Points"))
                            DiscountPer = IIf(ItemCLP("DiscountPer") Is DBNull.Value, 0, ItemCLP("DiscountPer"))
                            For Each Item As DataRowView In dv
                                totalAmt = Item("NetAmount") / ConvAmt
                                totalAmt = Math.Round(totalAmt, 2)
                                If ConvPoints > 0 Then
                                    Item("CLPPoints") = totalAmt * ConvPoints
                                ElseIf DiscountPer > 0 Then
                                    Item("CLPDiscount") = DiscountPer
                                End If
                            Next
                        End If
                        dv.RowFilter = filter
                    Next
                    If dv.RowFilter = " " Then
                        dv.RowFilter = " isnull(CLPPOINTS,0)=0 AND isnull(CLPDiscount,0)=0"
                    Else
                        dv.RowFilter = dv.RowFilter & " AND isnull(CLPPOINTS,0)=0 AND isnull(CLPDiscount,0)=0"
                    End If

                    dv.AllowEdit = True
                    For Each Row As DataRowView In dv
                        totalAmt = Row("NetAmount") / AllConvAmT
                        totalAmt = Math.Round(totalAmt, 2)
                        If AllConvPoint > 0 Then
                            Row("CLPPoints") = Convert.ToDouble(totalAmt * AllConvPoint)
                        ElseIf DiscPer > 0 Then
                            Row("CLPDiscount") = DiscPer
                        End If
                    Next
                End If
            Else
                ShowMessage(getValueByKey("CLMCF04"), "CLMCF04 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, "CLMCF04 - " & getValueByKey("CLAE04"))
        End Try
    End Function

    Public Function SearchSalesOrderCustomer(ByVal siteCode As String, Optional ByVal ShowStatus As Boolean = False) As String
        Try
            Dim dsSearch As DataSet
            If ShowStatus = True Then
                dsSearch = objPCSO.GetSearchSalesOrder(siteCode, True)
            Else
                dsSearch = objPCSO.GetSearchSalesOrder(siteCode, Nothing, Nothing, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
            End If
            Dim objSearch As New frmNCommonSearch
            objSearch.SetData = dsSearch.Tables(0)
            objSearch.ShowDialog()
            If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                Return objSearch.search(0)
            End If
            Return ""
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function SearchQuotation(Optional ByVal ShowStatus As Boolean = False) As String
        Try
            Dim dsSearch As DataSet
            If ShowStatus = True Then
                dsSearch = objQuotation.GetSearchQuotation(True)
            Else
                dsSearch = objQuotation.GetSearchQuotation
            End If
            Dim objSearch As New frmNCommonSearch
            objSearch.SetData = dsSearch.Tables(0)
            objSearch.ShowDialog()
            If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                Return objSearch.search(0)
            End If
            Return ""
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Sub pC1ComboSetDisplayMember(ByRef c1cbo As Object)
        c1cbo.ExtendRightColumn = True
        c1cbo.captionVisible = False
        For Each r As C1.Win.C1List.Split In c1cbo.Splits
            Dim i As Integer
            For i = 0 To r.DisplayColumns.Count - 1
                If r.DisplayColumns(i).Name.ToUpper() <> c1cbo.DisplayMember.ToString.ToUpper() Then
                    r.DisplayColumns(i).Visible = False
                End If
            Next
        Next
    End Sub

    Public Sub PSetDefaultCurrencyOfCashMemoSummary(ByRef objSummary As Object)
        'For Each ctrl In objSummary
        '    ctrl.text = ctrl.text & " (" & clsAdmin.CurrencyCode & ")"
        'Next
        If UCase(objSummary.parentform.Name) = "FRMNSALESORDERCREATION" Or UCase(objSummary.parentform.Name) = "FRMNSALESORDERUPDATE" Or UCase(objSummary.parentform.Name) = "FRMNSALESORDERCANCEL" Or UCase(objSummary.parentform.Name) = "FRMCASHMEMO" Then
            objSummary.lbl1 = objSummary.lbl1 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(1)
            objSummary.lbl2 = objSummary.lbl2 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(2)
            objSummary.lbl3 = objSummary.lbl3 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(3)
            objSummary.lbl4 = objSummary.lbl4 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(4)
            objSummary.lbl5 = objSummary.lbl5 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(5)
            objSummary.lbl6 = objSummary.lbl6 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(6)
            objSummary.lbl7 = objSummary.lbl7 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(7)
            objSummary.lbl8 = objSummary.lbl7 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(7)
            objSummary.lbl9 = objSummary.lbl7 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(7)
            objSummary.pSetFontSize(10)
        ElseIf UCase(objSummary.parentform.Name) = "FRMNBIRTHLISTCREATE" Then
            objSummary.lbl10 = objSummary.lbl10 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(10)
        ElseIf UCase(objSummary.parentform.Name) = "FRMNBIRTHLISTSALES" Then
            objSummary.lbl1 = objSummary.lbl1 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(1)
            objSummary.lbl2 = objSummary.lbl2 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(2)
            objSummary.lbl5 = objSummary.lbl5 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(5)
        ElseIf UCase(objSummary.parentform.Name) = "FRMNBIRTHLISTUPDATE" Then
            objSummary.lbl1 = objSummary.lbl1 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(1)
            objSummary.lbl4 = objSummary.lbl4 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(4)
            objSummary.lbl5 = objSummary.lbl5 & " (" & clsAdmin.CurrencyCode & ")"
            objSummary.pSetFontSize(5)
        End If


    End Sub

    ''' <summary>
    '''  Store Log file path and its name 
    ''' </summary>
    ''' <remarks></remarks>
    Private _strLogFilePath As String
    Private Property LogFilePath() As String
        Get
            Return _strLogFilePath
        End Get
        Set(ByVal value As String)
            _strLogFilePath = value
        End Set
    End Property

    Private _strShiftLogFilePath As String
    Private Property ShiftLogFilePath() As String
        Get
            Return _strShiftLogFilePath
        End Get
        Set(ByVal value As String)
            _strShiftLogFilePath = value
        End Set
    End Property


    ''' <summary>
    '''  Log Exception messages into txt files 
    ''' </summary>
    ''' <returns>On success, return true otherwise false </returns>
    ''' <remarks></remarks> 

    Public Function LogException(ByVal objException As Exception) As Boolean
        Try
            'Dim strDate As DateTime = DateAndTime.Now
            'Dim strFormatDate As String = strDate.ToLongDateString()
            'Dim sbParaText As New StringBuilder
            'strFormatDate = strFormatDate.Replace("/", "-")
            'strFormatDate = strFormatDate.Replace(":", "-")
            'strFormatDate = strFormatDate.Replace(" ", "") 
            'Dim objStreamWriter As StreamWriter
            'If Not LogFilePath = String.Empty Then
            '    If (File.Exists(LogFilePath)) Then
            '        objStreamWriter = File.AppendText(LogFilePath)
            '    Else
            '        objStreamWriter = File.CreateText(LogFilePath)
            '        sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
            '        sbParaText.AppendLine("------------------ Spectrum Log  -------------------------" & vbCrLf)
            '        sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
            '        sbParaText.AppendLine(String.Format("SiteCode: {0} ", clsAdmin.SiteCode) & vbCrLf)
            '        sbParaText.AppendLine(String.Format("Terminal ID: {0} ", clsAdmin.TerminalID) & vbCrLf)
            '        sbParaText.AppendLine(String.Format("User Name : {0} ", clsAdmin.UserName) & vbCrLf)
            '    End If
            '    sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
            '    sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)

            '    sbParaText.AppendLine(objException.ToString() & vbCrLf)
            '    objStreamWriter.Write(sbParaText.ToString)
            '    objStreamWriter.Flush()
            '    objStreamWriter.Close()
            'Else
            '    LogFileCreate()
            '    objStreamWriter = File.AppendText(LogFilePath)
            '    sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
            '    sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
            '    sbParaText.AppendLine(objException.ToString() & vbCrLf)
            '    objStreamWriter.Write(sbParaText.ToString)
            '    objStreamWriter.Flush()
            '    objStreamWriter.Close()
            'End If 
            'Return True



            SpectrumBL.CommanFuntion.LogException(objException, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, Application.StartupPath, LogFilePath)

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    '''  File Create at login time 
    ''' </summary>
    ''' <returns>On Success return true,otherwise return false  </returns>
    ''' <remarks></remarks>
    Public Function LogFileCreate() As Boolean
        Try
            'Dim strDate As DateTime = DateAndTime.Now

            'Dim strDateS As String = strDate.Date.ToString()
            'Dim strTime As String = strDate.TimeOfDay.ToString()
            'Dim strFormatDate As String = String.Concat(strDateS, strTime)
            'Dim sbParaText As New StringBuilder
            'strFormatDate = strFormatDate.Replace(".", "")
            'strFormatDate = strFormatDate.Replace("/", "-")
            'strFormatDate = strFormatDate.Replace(":", "_")
            'strFormatDate = strFormatDate.Replace(" ", "")
            'Dim strFilePath As String = String.Format("{0}\SpectrumLog_{1}.txt", Application.StartupPath, strFormatDate)


            'LogFilePath = strFilePath
            'Dim objStreamWriter As StreamWriter
            'If Not (File.Exists(LogFilePath)) Then
            '    objStreamWriter = File.CreateText(LogFilePath)
            '    sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
            '    sbParaText.AppendLine("------------------ Spectrum Log  -------------------------" & vbCrLf)
            '    sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
            '    sbParaText.AppendLine(String.Format("SiteCode: {0} ", clsAdmin.SiteCode) & vbCrLf)
            '    sbParaText.AppendLine(String.Format("Terminal ID: {0} ", clsAdmin.TerminalID) & vbCrLf)
            '    sbParaText.AppendLine(String.Format("User Name : {0} ", clsAdmin.UserName) & vbCrLf)
            '    objStreamWriter.Write(sbParaText.ToString)
            '    objStreamWriter.Flush()
            '    objStreamWriter.Close()



            'End If



            Dim strLogFileName As String = ""

            SpectrumBL.CommanFuntion.LogFileCreate(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, Application.StartupPath, strLogFileName)
            LogFilePath = strLogFileName
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    '''  ShiftAcitivityFileCreate
    ''' </summary>
    ''' <returns>On Success return true,otherwise return false  </returns>
    ''' <remarks></remarks>
    ''' 
    Public Function ActivityLogForShift(ByVal dt As DataTable, ByVal message As String, ByVal Shiftid As String) As Boolean
        Try
            SpectrumBL.CommanFuntion.ActivityLogForShift(dt, message, Shiftid, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, Application.StartupPath, ShiftLogFilePath)

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function LogFileForShiftActivityCreate() As Boolean
        Try

            Dim strShiftLogFileName As String = ""

            SpectrumBL.CommanFuntion.LogFileForShiftActivityCreate(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, Application.StartupPath, strShiftLogFileName)
            ShiftLogFilePath = strShiftLogFileName
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Checke for entered collect amount 
    ''' </summary>
    ''' <param name="objAmount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInteger(ByVal objAmount As Object) As Boolean
        Try
            Dim decCheck As Decimal = CDbl(objAmount)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    '''  Capture data of payment into table
    ''' </summary>
    ''' <param name="decAmount"></param>
    ''' <param name="strReceipttype"></param>
    ''' <param name="strReceiptTypeCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PaymentTransactionByShortCutForms(ByVal decAmount As Decimal, ByVal strReceipttype As String, ByVal strReceiptTypeCode As String, ByRef dsPaymentReceipt As DataSet, _
                                                Optional ByVal ExpiryDate As DateTime? = Nothing, Optional ByVal CardNo As String = "", Optional ByVal BankAccNo As String = "", Optional chequeNo As String = "", Optional ByVal Remark As String = "") As Boolean
        Try

            'Dim objClsAcceptPayment As New clsAcceptPayment

            'Dim dtPaymentReceipt As DataTable
            'If Not dsPaymentReceipt.Tables.Contains("MSTRecieptType") Then
            '    dsPaymentReceipt = objClsAcceptPayment.GetDataset()
            'Else

            'End If
            ''Dim rowCount As Integer
            'Dim objclscommaon As New clsCommon
            'Dim currentDate As Date = objclscommaon.GetCurrentDate()
            ''rowCount = dtPaymentReceipt.Rows.Count
            ''Dim srno As Integer = dtPaymentReceipt.Compute("max(SRNO)", "")

            ''dtPaymentReceipt.Rows(rowCount)("SRNO") = srno
            'Dim drNewOpenAmountPayment As DataRow = dsPaymentReceipt.Tables("MSTRecieptType").NewRow()
            'drNewOpenAmountPayment("RecieptType") = strReceipttype
            'drNewOpenAmountPayment("RecieptTypeCode") = strReceiptTypeCode
            'drNewOpenAmountPayment("Amount") = decAmount
            'drNewOpenAmountPayment("Date") = currentDate
            ''------ Start Code added By Mahesh adding new fields at form ...
            'If strReceiptTypeCode = "CreditCard" Then
            '    If Not IsDBNull(ExpiryDate) AndAlso ExpiryDate <> Nothing Then
            '        drNewOpenAmountPayment("Date") = ExpiryDate
            '    End If
            '    drNewOpenAmountPayment("Number") = CardNo
            '    drNewOpenAmountPayment("BankAccNo") = BankAccNo
            'ElseIf strReceiptTypeCode = "Cheque" Then
            '    drNewOpenAmountPayment("Number") = chequeNo
            'End If
            ''-------End  Code added By Mahesh adding new fields at form  ...

            'Dim decExchangeRate As Decimal
            'Dim decTotalAmountInCurrency As Decimal
            'decTotalAmountInCurrency = objClsAcceptPayment.CalculateTotalBillAmount_InCurrency(decAmount, clsAdmin.CurrencyCode, decExchangeRate, clsAdmin.CurrencyCode)
            'drNewOpenAmountPayment("ExchangeRate") = decExchangeRate
            'drNewOpenAmountPayment("CurrencyCode") = clsAdmin.CurrencyCode
            'drNewOpenAmountPayment("AmountInCurrency") = decTotalAmountInCurrency
            'dsPaymentReceipt.Tables("MSTRecieptType").Rows.Add(drNewOpenAmountPayment)
            'Return True
            'vipin SO merge
            Dim objClsAcceptPayment As New clsAcceptPayment

            Dim dtPaymentReceipt As DataTable
            If Not dsPaymentReceipt.Tables.Contains("MSTRecieptType") Then
                dsPaymentReceipt = objClsAcceptPayment.GetDataset()
            Else

            End If
            'Dim rowCount As Integer
            Dim objclscommaon As New clsCommon
            Dim currentDate As Date = objclscommaon.GetCurrentDate()
            'rowCount = dtPaymentReceipt.Rows.Count
            'Dim srno As Integer = dtPaymentReceipt.Compute("max(SRNO)", "")

            'dtPaymentReceipt.Rows(rowCount)("SRNO") = srno
            Dim drNewOpenAmountPayment As DataRow = dsPaymentReceipt.Tables("MSTRecieptType").NewRow()
            drNewOpenAmountPayment("RecieptType") = strReceipttype
            drNewOpenAmountPayment("RecieptTypeCode") = strReceiptTypeCode
            drNewOpenAmountPayment("Amount") = decAmount
            drNewOpenAmountPayment("Date") = currentDate
            '------ Start Code added By Mahesh adding new fields at form ...
            If strReceiptTypeCode = "CreditCard" Then
                If Not IsDBNull(ExpiryDate) AndAlso ExpiryDate <> Nothing Then
                    drNewOpenAmountPayment("Date") = ExpiryDate
                End If
                drNewOpenAmountPayment("Number") = CardNo
                drNewOpenAmountPayment("BankAccNo") = BankAccNo
            ElseIf strReceiptTypeCode = "Cheque" Then
                drNewOpenAmountPayment("Number") = chequeNo
            ElseIf strReceiptTypeCode = "Neft" Then      'vipin
                drNewOpenAmountPayment("Number") = CardNo
            ElseIf strReceiptTypeCode = "Rtgs" Then
                drNewOpenAmountPayment("Number") = CardNo
            End If
            '-------End  Code added By Mahesh adding new fields at form  ...

            Dim decExchangeRate As Decimal
            Dim decTotalAmountInCurrency As Decimal
            decTotalAmountInCurrency = objClsAcceptPayment.CalculateTotalBillAmount_InCurrency(decAmount, clsAdmin.CurrencyCode, decExchangeRate, clsAdmin.CurrencyCode)
            drNewOpenAmountPayment("ExchangeRate") = decExchangeRate
            drNewOpenAmountPayment("CurrencyCode") = clsAdmin.CurrencyCode
            drNewOpenAmountPayment("AmountInCurrency") = decTotalAmountInCurrency
            drNewOpenAmountPayment("RefNo_4") = Remark  'vipin
            dsPaymentReceipt.Tables("MSTRecieptType").Rows.Add(drNewOpenAmountPayment)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function AddBlankColumn(ByRef dtSource As DataTable) As Boolean
        Try
            If Not dtSource Is Nothing Then
                If Not dtSource.Columns.Contains("Blankclm") Then
                    Dim dc As DataColumn = New DataColumn("Blankclm")
                    dc.Caption = ""
                    dtSource.Columns.Add(dc)
                    Return True
                Else
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function BirthListCommanLoad() As Boolean
        Try
            If clsDefaultConfiguration.BLCLPIntimation Then
                ShowMessage(getValueByKey("CLMCF05"), "CLMCF05 - " & getValueByKey("CLAE04"))
            End If

            PrintSetProperty()
        Catch ex As Exception

        End Try
    End Function
    Public Function ShowReason(ByVal dtReason As DataTable) As String
        Try
            Dim StrReasons As String = ""
            Dim objReturn As New frmNCommonView
            objReturn.Text = getValueByKey("mod010")
            objReturn.SetData = dtReason

            Array.Resize(objReturn.ColumnName, dtReason.Columns.Count)
            Dim i As Integer = 0
            For Each col As DataColumn In dtReason.Columns
                If col.ColumnName.ToUpper() <> "Select".ToUpper() And col.ColumnName.ToUpper() <> "REASONCODE".ToUpper() And col.ColumnName.ToUpper() <> "REASONNAME".ToUpper() Then
                    objReturn.ColumnName(i) = col.ColumnName
                End If
                i = i + 1
            Next

            If dtReason.Rows.Count > 0 Then
                dtReason.Rows(0)(0) = True
            End If

            Dim result = objReturn.ShowDialog()
            If result = DialogResult.OK Then
                If Not objReturn.search Is Nothing Then
                    dtReason = objReturn.GetData
                    StrReasons = ""
                    For Each dr As DataRow In dtReason.Select("Select=True", "REASONCODE,TRNSEQUENCENAME", DataViewRowState.CurrentRows)
                        StrReasons = StrReasons & dr("REASONNAME").ToString & ","
                    Next
                    If StrReasons.Length > 0 AndAlso StrReasons.Substring(StrReasons.Length - 1, 1) = "," Then
                        StrReasons = StrReasons.Substring(0, StrReasons.Length - 1)
                    End If
                    Return StrReasons
                End If
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function PrintSetProperty() As Boolean
        Try
            'SpectrumPrint.My.MySettings.Default.POSPrinter = Spectrum.My.Settings.OPOSPrinter
            'SpectrumPrint.My.MySettings.Default.OtherPrinter = Spectrum.My.Settings.OtherPrinter
            SpectrumPrint.My.MySettings.Default.POSPrinter = ReadSpectrumParamFile("OPOSPrinter")
            SpectrumPrint.My.MySettings.Default.OtherPrinter = ReadSpectrumParamFile("OtherPrinter")
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetGiftMessage() As String
        Try
            Dim obj As New frmSpecialPrompt(getValueByKey("SP004"))
            obj.ShowTextBox = True
            obj.AllowText = True
            obj.callFromGiftMsg = "GiftMsg"
            obj.ShowDialog()
            Return obj.GetResult
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub GridTooltip(ByRef tlpSuper As C1.Win.C1SuperTooltip.C1SuperTooltip, ByRef fgData As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strTooltip As String)
        tlpSuper.AutomaticDelay = 0
        tlpSuper.AutoPopDelay = 1000
        Dim flex As C1.Win.C1FlexGrid.C1FlexGrid
        flex = CType(fgData, C1.Win.C1FlexGrid.C1FlexGrid)
        Dim tip As String
        tip = strTooltip
        tlpSuper.BackColor = Color.LightYellow
        tlpSuper.SetToolTip(flex, tip)
    End Sub

    ''' <summary>
    ''' Birthlist Item search 
    ''' </summary>
    ''' <param name="dt">Collection of Ean against Article code</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchBirthListItem(ByVal dt As DataTable) As DataRow
        Try
            If dt.Rows.Count <= 0 Then
                ShowMessage(getValueByKey("CMR12"), "CMR12 - " & getValueByKey("CLAE04"))
                Return Nothing
            End If
            If dt.Rows.Count >= 1 Then
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Return dt.Rows(0)
                End If
            End If
            'If dt.Rows.Count > 1 Then
            '    Dim objEan As New frmNCommonView
            '    objEan.SetData = dt
            '    Array.Resize(objEan.ColumnName, dt.Columns.Count)
            '    Dim i As Integer = 0
            '    For Each col As DataColumn In dt.Columns
            '        If col.ColumnName <> "EAN" And col.ColumnName <> "ARTICLECODE" And col.ColumnName <> "SELLINGPRICE" Then
            '            objEan.ColumnName(i) = col.ColumnName
            '        End If
            '        i = i + 1
            '    Next
            '    objEan.ShowDialog()
            '    Dim dtTemp As DataTable = dt.Clone()
            '    dtTemp.ImportRow(objEan.GetResultRow)
            '    dt.Clear()
            '    dt = dtTemp
            '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            '        Return dt.Rows(0)
            '    End If
            'End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function F2_ChangeSalesQuantity(ByRef grid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strEditColumnName As String, ByVal strFormTitle As String, Optional ByRef outIRowIndex As Integer = 0, Optional ByRef outIColumnIndex As Integer = 0, Optional ByVal BirthlistItemTable As DataTable = Nothing) As Boolean
        Try
            If GetSelectedRowsInGrid(grid) Then
                Dim frm As New frmSpecialPrompt(strFormTitle)
                frm.ShowTextBox = True
                frm.AcceptButton = frm.cmdOk
                frm.txtValue.MaxLength = 9
                If strEditColumnName = "SellingPrice" Or strEditColumnName = "Rate" Then
                    frm.AllowDecimal = True
                    frm.txtValue.MaxLength = 14
                End If
                frm.ShowDialog()
                Dim objValue As Object

                Try
                    objValue = frm.GetResult
                    If Not objValue = Nothing Then
                        If CDbl(objValue) <= Decimal.Zero Then
                            ShowMessage(getValueByKey("CLMCF07"), "CLMCF07 - " & getValueByKey("CLAE05"))
                        End If
                    End If
                Catch ex As Exception
                    'ShowMessage("Enter valid qunatity ", "F2 Click")
                    ShowMessage(getValueByKey("CLMCF07"), "CLMCF07 - " & getValueByKey("CLAE05"))
                End Try
                CurrencyFormat(objValue)

                GetSelectedRowsInGrid(grid, "", True, strEditColumnName, objValue, outIRowIndex, outIColumnIndex, BirthlistItemTable)

            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function GetSelectedRowsInGrid(ByRef grid As C1.Win.C1FlexGrid.C1FlexGrid, Optional ByVal strErrorMsg As String = "", Optional ByVal isEditSelectedClm As Boolean = False, Optional ByVal strEditColumnName As String = "", Optional ByVal objColumnValue As Object = Nothing, Optional ByRef outIRowIndex As Integer = 0, Optional ByRef outIColumnIndex As Integer = 0, Optional ByVal BirthlistItemTable As DataTable = Nothing) As Boolean
        Try
            Dim selectedItems As C1.Win.C1FlexGrid.RowCollection
            selectedItems = grid.Rows
            If Not selectedItems Is Nothing AndAlso selectedItems.Count > 1 Then
                If isEditSelectedClm And Not strEditColumnName = String.Empty AndAlso Not objColumnValue Is Nothing Then
                    If grid.RowSel <= 0 Then
                        Dim row As C1.Win.C1FlexGrid.Row = selectedItems.Item(1)
                        If Not BirthlistItemTable Is Nothing Then
                            BirthlistItemTable.Rows(row.Index - 1)("ActualSellingPrice") = row(strEditColumnName)
                        End If

                        row(strEditColumnName) = objColumnValue
                        outIRowIndex = row.Index
                        outIColumnIndex = grid.Cols(strEditColumnName).Index
                    Else
                        Dim row As C1.Win.C1FlexGrid.Row = selectedItems.Item(grid.RowSel)
                        If Not BirthlistItemTable Is Nothing Then
                            BirthlistItemTable.Rows(row.Index - 1)("ActualSellingPrice") = row(strEditColumnName)
                        End If

                        row.Item(strEditColumnName) = objColumnValue
                        outIRowIndex = row.Index
                        outIColumnIndex = grid.Cols(strEditColumnName).Index
                    End If
                    Return True
                Else
                    Return True
                End If
            Else
                strErrorMsg = getValueByKey("SO012")
                ShowMessage(strErrorMsg, "SO012 - " & getValueByKey("CLAE04"))
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function fnGridColAutoSize(ByRef grd As C1FlexGrid)
        For Each col As C1.Win.C1FlexGrid.Column In grd.Cols
            If col.Visible = True Then
                grd.AutoSizeCol(col.Index)
            End If
        Next
    End Function

    ''' <summary>
    ''' Validate Purchase ,Pickup ,Reserved etc. all the quantity fields in side the Birthlist 
    ''' </summary>
    ''' <param name="gridFlex">Grid </param>
    ''' <param name="iRowIndex">Modified Column Index</param>
    ''' <param name="strColumnName">Column Name</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateQuantity(ByVal gridFlex As C1FlexGrid, ByVal iRowIndex As Integer, ByVal strColumnName As String) As Boolean
        Try
            If gridFlex.Cols(iRowIndex).Name.ToUpper() = strColumnName.ToUpper() Then
                If CDbl(gridFlex.Editor.Text) > clsDefaultConfiguration.MaxQuantity Then
                    'CM059() 
                    ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE05"))
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function
    Public Sub CreateSpectrumParamFile(ByVal strLabel As String, ByVal strValue As String)
        Try
            Dim isFound As Boolean = False
            If File.Exists(Application.StartupPath & "\SpectrumParam") Then
                '        File.Decrypt(Application.StartupPath & "\SpectrumParam")
                Dim dtData As New DataSet
                dtData.ReadXml(Application.StartupPath & "\SpectrumParam")
                For i As Integer = 1 To dtData.Tables(0).Rows.Count
                    If UCase(dtData.Tables(0).Rows(i - 1)("Label")) = UCase(strLabel) Then
                        isFound = True
                        dtData.Tables(0).Rows(i - 1)("Value") = Decrypt(strValue)
                        '---commented By Mahesh Don't need to put in diffgram Mode ...
                        'dtData.AcceptChanges()
                        'dtData.WriteXml(Application.StartupPath & "\SpectrumParam", XmlWriteMode.DiffGram)
                        dtData.AcceptChanges()
                        dtData.WriteXml(Application.StartupPath & "\SpectrumParam", XmlWriteMode.IgnoreSchema)
                        Exit For
                    End If
                Next
                If isFound = False Then
                    Dim dr As DataRow = dtData.Tables(0).NewRow
                    dr("Label") = strLabel
                    dr("Value") = Decrypt(strValue)
                    dtData.Tables(0).Rows.Add(dr)
                    'dtData.AcceptChanges()
                    'dtData.WriteXml(Application.StartupPath & "\SpectrumParam", XmlWriteMode.DiffGram)
                    dtData.AcceptChanges()
                    dtData.WriteXml(Application.StartupPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
                End If

            Else
                Dim dtTable As New DataTable("SpectrumParam")
                dtTable.Columns.Add("Label", Type.GetType("System.String"))
                dtTable.Columns.Add("Value", Type.GetType("System.String"))
                Dim dr As DataRow = dtTable.NewRow
                dr("Label") = strLabel
                dr("Value") = Decrypt(strValue)
                dtTable.Rows.Add(dr)
                dtTable.WriteXml(Application.StartupPath & "\SpectrumParam", XmlWriteMode.WriteSchema)
            End If
        Catch ex As Exception
            LogException(ex)
            Dim p As New ApplicationException("Check For Label =" & strLabel & " AND Value =" & strValue)
            LogException(p)
        End Try
        '----- Create CreateLocal para file too 
        Try
            Call CreateLocalParaFile(strLabel, strValue)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub CreateLocalParaFile(ByVal strLabel As String, ByVal strValue As String)
        Try
            Dim isFound As Boolean = False
            If File.Exists(Application.StartupPath & "\LocalPara") Then
                '        File.Decrypt(Application.StartupPath & "\SpectrumParam")
                Dim dtData As New DataSet
                dtData.ReadXml(Application.StartupPath & "\LocalPara")
                For i As Integer = 1 To dtData.Tables(0).Rows.Count
                    If UCase(dtData.Tables(0).Rows(i - 1)("Label")) = UCase(strLabel) Then
                        isFound = True
                        dtData.Tables(0).Rows(i - 1)("Value") = Decrypt(strValue)
                        dtData.AcceptChanges()
                        dtData.WriteXml(Application.StartupPath & "\LocalPara", XmlWriteMode.DiffGram)
                        dtData.AcceptChanges()
                        dtData.WriteXml(Application.StartupPath & "\LocalPara", XmlWriteMode.IgnoreSchema)
                        Exit For
                    End If
                Next
                If isFound = False Then
                    Dim dr As DataRow = dtData.Tables(0).NewRow
                    dr("Label") = strLabel
                    dr("Value") = Decrypt(strValue)
                    dtData.Tables(0).Rows.Add(dr)
                    dtData.AcceptChanges()
                    dtData.WriteXml(Application.StartupPath & "\LocalPara", XmlWriteMode.DiffGram)
                    dtData.AcceptChanges()
                    dtData.WriteXml(Application.StartupPath & "\LocalPara", XmlWriteMode.WriteSchema)
                End If

            Else
                Dim dtTable As New DataTable("LocalPara")
                dtTable.Columns.Add("Label", Type.GetType("System.String"))
                dtTable.Columns.Add("Value", Type.GetType("System.String"))
                Dim dr As DataRow = dtTable.NewRow
                dr("Label") = strLabel
                dr("Value") = Decrypt(strValue)
                dtTable.Rows.Add(dr)
                dtTable.WriteXml(Application.StartupPath & "\LocalPara", XmlWriteMode.WriteSchema)
            End If
        Catch ex As Exception

        End Try
        ' File.Encrypt(Application.StartupPath & "\SpectrumParam")
    End Sub

    Public Function ReadSpectrumParamFile(ByVal strLabel As String) As String
        '  File.Decrypt(Application.StartupPath & "\SpectrumParam")
        Dim strValue As String = ""
        Try
            If File.Exists(Application.StartupPath & "\SpectrumParam") Then
                Dim dtData As New DataSet
                dtData.ReadXml(Application.StartupPath & "\SpectrumParam")
                For i As Integer = 0 To dtData.Tables(0).Rows.Count - 1
                    If UCase(dtData.Tables(0).Rows(i)("Label")) = UCase(strLabel) Then
                        strValue = Encrypt(dtData.Tables(0).Rows(i)("Value"))
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strValue = ""
        End Try
        'File.Encrypt(Application.StartupPath & "\SpectrumParam")
        Return strValue
    End Function


    Public Function ReadLocalParaFile(ByVal strLabel As String) As String
        '  File.Decrypt(Application.StartupPath & "\SpectrumParam")
        Dim strValue As String = ""
        Try
            If File.Exists(Application.StartupPath & "\SpectrumParam") Then
                Dim dtData As New DataSet
                dtData.ReadXml(Application.StartupPath & "\SpectrumParam")
                For i As Integer = 0 To dtData.Tables(0).Rows.Count - 1
                    If UCase(dtData.Tables(0).Rows(i)("Label")) = UCase(strLabel) Then
                        strValue = Encrypt(dtData.Tables(0).Rows(i)("Value"))
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strValue = ""
        End Try
        'File.Encrypt(Application.StartupPath & "\SpectrumParam")
        Return strValue
    End Function
    Public Function ReadWebServiceParaFile(ByVal strLabel As String) As String
        Dim strValue As String = ""
        Try
            If File.Exists(Application.StartupPath & "\WebServicePara") Then
                Dim dtData As New DataSet
                dtData.ReadXml(Application.StartupPath & "\WebServicePara")
                For i As Integer = 0 To dtData.Tables(0).Rows.Count - 1
                    If UCase(dtData.Tables(0).Rows(i)("Label")) = UCase(strLabel) Then
                        strValue = dtData.Tables(0).Rows(i)("Value")
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            strValue = ""
        End Try
        Return strValue
    End Function
    'Function added for CR 5679
    Public Function F12_ChangeSalesQuantity(ByRef grid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strEditColumnName As String, ByVal strFormTitle As String, Optional ByRef outIRowIndex As Integer = 0, Optional ByRef outIColumnIndex As Integer = 0) As Decimal
        Dim val As Decimal
        Try
            val = 0.0
            If GetSelectedRowsInGrid(grid) Then
                Dim frm As New frmSpecialPrompt(strFormTitle)
                frm.ShowTextBox = True
                frm.txtValue.MaxLength = 9
                frm.AcceptButton = frm.cmdOk
                frm.ShowDialog()
                Dim objValue As Object
                Try
                    objValue = frm.GetResult
                    If CDbl(objValue) <= Decimal.Zero Then
                        ShowMessage(getValueByKey("CLMCF07"), "CLMCF07 - " & getValueByKey("CLAE05"))
                    Else
                        val = CDbl(objValue)
                    End If
                Catch ex As Exception
                    'ShowMessage("Enter valid qunatity ", "F2 Click")
                    ShowMessage(getValueByKey("CLMCF07"), "CLMCF07 - " & getValueByKey("CLAE05"))
                End Try
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Return val
    End Function
    Public Sub ShowArticleImage(ByVal StrArticle As String, ByRef btn As Button)

        Try
            Dim url As String
            Dim objComm As New clsCommon
            url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            If url <> String.Empty Then
                Dim img As Image
                Dim ratio As Double
                Dim s As Size

                btn.Image = Nothing
                'btn.imageLayout = Nothing

                If btn.Width > 0 And btn.Height > 0 Then
                    Try
                        img = Image.FromFile(url)
                        ratio = img.Height / img.Width
                        s.Width = btn.Width
                        s.Height = s.Width * ratio
                        If s.Height > btn.Height Then
                            ratio = btn.Height / s.Height
                            s.Width = s.Width * ratio
                            s.Height = s.Height * ratio
                        End If
                        If s.Width > 0 And s.Height > 0 Then
                            btn.Image = New Bitmap(img, s)
                            'btn.imageLayout = ImageLayout.Center
                        End If
                        img.Dispose()
                    Catch ex As Exception

                    End Try
                End If
            Else
                btn.Image = My.Resources.NA
                'btn.imageLayout = ImageLayout.Center
                btn.Image = Nothing

            End If
            'btn.image = Image.FromFile(url)

            'btn.imageLayout = ImageLayout.Center
            'btn.SizeMode = PictureBoxSizeMode.AutoSize


        Catch ex As Exception
            btn.Image = My.Resources.NA
            btn.Image = Nothing
        End Try
    End Sub
    Public Function ReturnOnlyNumbersWhenCardSwipe(ByVal cardNo As String) As String
        Dim numCardNo As String = String.Empty
        For Each s As String In cardNo
            If Char.IsDigit(s) = True Then
                numCardNo &= s
            End If
        Next
        Return numCardNo
    End Function
    Public Function SendSMSForPC(ByVal strUrl As String, ByVal Mobileno As String, ByVal msgStr As String) As Boolean
        Try
            If Trim(Mobileno) <> String.Empty AndAlso Trim(msgStr) <> String.Empty Then
                Dim strResp As String
                strUrl = strUrl.Replace("$number", Mobileno)
                strUrl = strUrl.Replace("$msg", msgStr)
                strResp = DoWebRequest(strUrl)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Public Function SendSMSForPCSO(ByVal strUrl As String, ByVal Mobileno As String, ByVal msgStr As String) As Boolean
        Try
            If Trim(Mobileno) <> String.Empty AndAlso Trim(msgStr) <> String.Empty Then
                Dim strResp As String
                strUrl = strUrl.Replace("$number", Mobileno)
                strUrl = strUrl.Replace("$msg", msgStr)
                request = DirectCast(WebRequest.Create(strUrl), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function SendSMS(ByVal Mobileno As String, ByVal msgStr As String) As Boolean
        If Trim(Mobileno) <> String.Empty AndAlso Trim(msgStr) <> String.Empty Then
            Dim strUrl As String
            Dim strResp As String
            strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=parimalpchauhan@gmail.com:parimal&senderID=TEST SMS&receipientno=" & Mobileno & "&msgtxt=" & msgStr & "&state=4"
            'strUrl = "http://dndsms.dotnetbrothers.in/Api.aspx?smstype=TextSMS&to=" & Mobileno & "&msg=" & msgStr & "&usr=MOD&pwd=mod123"
            'strUrl = "http://www.unicel.in/SendSMS/sendmsg.php?uname=MODDemo&pass=k@T(o7Y6&send=Alerts&dest=919886640263&msg=" & msgStr
            strResp = DoWebRequest(strUrl)
        End If
    End Function
    'Do web Request Function
    Public Function DoWebRequest(ByVal url) As String
        'On Error GoTo err_DoWebRequest
        DoWebRequest = False
        Try
            Dim objXML As Object
            objXML = CreateObject("Microsoft.XMLHTTP")  'Msxml2.ServerXMLHTTP 'Msxml2.XMLHTTP.4.0
            objXML.Open("GET", url, True)
            objXML.Send()
            If (objXML.Status = 404) Then
                DoWebRequest = False
            ElseIf (objXML.Status = 200) Then
                DoWebRequest = True
            Else
                DoWebRequest = False 'objXML.ResponseText
            End If
            objXML = Nothing
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function DataBaseAutoBackup() As Boolean
        Try
            DataBaseAutoBackup = False
            Dim objclscommaon As New clsCommon
            Dim dbName As String = ReadSpectrumParamFile("DataSource")
            Dim folderPath As String = clsDefaultConfiguration.DatabaseBackupPath
            Dim bakFileName As String = dbName + DateTime.Now.ToString().Replace("/"c, "-"c).Replace(":"c, "-"c) + ".bak"
            DataBaseAutoBackup = objclscommaon.AutoDatabaseBackup(dbName, folderPath, bakFileName)
            Return DataBaseAutoBackup
        Catch ex As Exception
            LogException(ex)
            Return DataBaseAutoBackup
        End Try
    End Function
    'Public Function GetMultilinedString(ByRef stringCollection As IEnumerable(Of String)) As String
    '    Try
    '        Dim multilinedString As String = String.Empty
    '        For Each description In stringCollection
    '            multilinedString += description & vbCrLf
    '        Next
    '        If multilinedString(multilinedString.Count - 1) = vbCrLf Then
    '            multilinedString.Remove(multilinedString.Count - 1)
    '        End If
    '        Return multilinedString
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return String.Empty
    '    End Try
    'End Function

    Public Function GetMultilinedString(ByRef descriptionDictionary As Dictionary(Of String, Integer)) As String
        Try
            Dim multilinedString As String = String.Empty
            For Each keyValue In descriptionDictionary
                If keyValue.Value <> 0 Then
                    multilinedString += "   *" & keyValue.Key & " - " & keyValue.Value & vbCrLf
                Else
                    multilinedString += keyValue.Key & vbCrLf
                End If
            Next
            multilinedString = multilinedString.Remove(multilinedString.Count - 2, 1)
            Return multilinedString
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function ChangeComboItemsQuantity(ByRef stringArray As String(), ByVal prevQuantity As Integer, ByVal newQuantity As Integer) As String
        Try
            Dim outputString As String = String.Empty

            For Each desc In stringArray
                Dim newString As String = String.Empty
                If desc.Contains("-") Then
                    Dim quantitystring As String = desc.Substring(desc.LastIndexOf("-") + 1)
                    Dim quantity As Integer = 0
                    If Integer.TryParse(quantitystring, quantity) Then
                        'quantity = quantity + ((quantity / prevQuantity) * (newQuantity - prevQuantity))
                        quantity = quantitystring
                        newString = desc.Remove(desc.LastIndexOf("-") + 2) & quantity.ToString()
                        newString = newString.Replace(vbLf, "")
                    Else
                        newString = desc
                    End If
                Else
                    newString = desc
                End If
                outputString += newString & vbCrLf
            Next
            outputString = outputString.Remove(outputString.Count - 2, 1)
            Return outputString
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function IsNumeric(ByRef value As String) As Boolean
        For i As Integer = value.Length - 1 To 0 Step -1
            If Not Char.IsDigit(value(i)) Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function HasAlphaNumericChar(ByRef value As String) As Boolean
        For i As Integer = value.Length - 1 To 0 Step -1
            If Char.IsLetterOrDigit(value(i)) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CalCulateCLPSlabwise(ByVal CardType As String, ByRef dtItem As DataTable, ByVal ItemFilter As String, ByVal strCustomerID As String, Optional ByRef dtpayment As DataTable = Nothing) As Boolean
        Try
            Dim accumType As String = ""
            Dim redType As String = ""
            Dim ds As DataSet
            Dim objCM As New clsCashMemo

            ds = objCM.GetCLPAccumlationDetails(clsAdmin.SiteCode, accumType, redType, strCustomerID)

            'Verifies whether Program Assigned to site
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dblMaxAccLimit As Double = 0
                Dim dblCustAccPoints As Double = 0
                Dim bOnlyAtCreationSite As Boolean = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("onlyAtCreatedSite") Is DBNull.Value, 0, ds.Tables("MSTCLPPROGRAM").Rows(0)("onlyAtCreatedSite"))
                Dim bMaxLimitExists As Boolean = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("maxLimitOnAcc") Is DBNull.Value, False, ds.Tables("MSTCLPPROGRAM").Rows(0)("maxLimitOnAcc"))
                Dim bApplicableOnPromotion As Boolean = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("applicableOnPromotion") Is DBNull.Value, True, ds.Tables("MSTCLPPROGRAM").Rows(0)("applicableOnPromotion"))
                Dim bAccWhileRed As Boolean = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("accWhenRed") Is DBNull.Value, False, ds.Tables("MSTCLPPROGRAM").Rows(0)("accWhenRed"))
                Dim bConvertinMultiples As Boolean = IIf(ds.Tables("Header").Rows(0)("ConvertMultiples") Is DBNull.Value, False, ds.Tables("Header").Rows(0)("ConvertMultiples"))
                Dim strCLPProgramId As String = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("CLPProgramId") Is DBNull.Value, String.Empty, ds.Tables("MSTCLPPROGRAM").Rows(0)("CLPProgramId"))
                dblMaxAccLimit = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("maxAccumulationLimit") Is DBNull.Value, 0, ds.Tables("MSTCLPPROGRAM").Rows(0)("maxAccumulationLimit"))
                dblCustAccPoints = IIf(ds.Tables("CLPCUSTOMERS").Rows(0)("TotalBalancePoint") Is DBNull.Value, 0, ds.Tables("CLPCUSTOMERS").Rows(0)("TotalBalancePoint"))
                'Verifies whether Program Assigned to site Assigned to site
                If Not strCLPProgramId = String.Empty Then


                    'Verifies whthere CLP is redeeming & accumulation against redeemption allowed


                    Dim count As Integer

                    If dtpayment Is Nothing Then
                        count = 1
                    Else
                        Dim dataview As New DataView(dtpayment, "Reciepttypecode = 'CLPPoint'", "", DataViewRowState.CurrentRows)
                        count = dataview.Count
                        dataview.Dispose()
                    End If

                    If (bAccWhileRed = True Or count = 0) Then

                        'Verifies Max Accumulation Limit
                        If (bOnlyAtCreationSite = False Or ds.Tables("CLPCUSTOMERS").Rows.Count > 0) AndAlso (dblCustAccPoints < dblMaxAccLimit Or bMaxLimitExists = False) Then

                            Dim dv As New DataView(dtItem, ItemFilter, "", DataViewRowState.CurrentRows)
                            'Verifies that any article does have CLP applicablity
                            If dv.Count > 0 Then
                                Dim dblCurBilPts As Double = 0
                                If dblCustAccPoints < dblMaxAccLimit Or bMaxLimitExists = False Then
                                    Dim totalAmt As Double
                                    Dim AllConvAmT, AllConvPoint As Double
                                    Dim Items As String
                                    Dim HFilter As String = ""
                                    If accumType.Contains("Fixed") Then
                                        HFilter = "ISNULL(CARDTYPE,'')='" & CardType & "'"
                                    ElseIf accumType.Contains("Variable") Then
                                        HFilter = "ISNULL(CARDTYPE,'')='" & CardType & "'"
                                    ElseIf accumType.Contains("ItemFlat") Then
                                        HFilter = "ISNULL(CARDTYPE,'')='" & CardType & "'"
                                    End If
                                    For Each headerPoint As DataRow In ds.Tables("Header").Select(HFilter, "", DataViewRowState.CurrentRows)
                                        AllConvAmT = IIf(headerPoint("AMTVALUE") Is DBNull.Value, 0, headerPoint("AMTVALUE"))
                                        AllConvPoint = IIf(headerPoint("Points") Is DBNull.Value, 0, headerPoint("Points"))
                                        'DiscPer = IIf(headerPoint("DiscountPer") Is DBNull.Value, 0, headerPoint("DiscountPer"))
                                    Next

                                    'Takes distinct articles from cashmemodtl
                                    Dim dtItems As DataTable = dv.ToTable(True, "ArticleCode")
                                    For Each ItemRow As DataRow In dtItems.Rows
                                        Items = Items & "'" & ItemRow("ArticleCode") & "',"
                                    Next
                                    Items = Items.Substring(0, Items.Length - 1)
                                    Dim dblClpPoints As Double = 0
                                    Dim strPromotionId As String
                                    'Applies points to article if they are article veriable
                                    For Each ItemCLP As DataRow In ds.Tables("detail").Select(HFilter & " And ArticleCode in (" & Items & ")", "", DataViewRowState.CurrentRows)

                                        Dim drProm = dtItem.Select("ArticleCode='" & ItemCLP("ArticleCode").ToString() & "' And FirstLevel Is Not Null")
                                        If (drProm.Count > 0) Then
                                            strPromotionId = IIf(drProm(0)("FirstLevel") Is DBNull.Value, String.Empty, drProm(0)("FirstLevel").ToString())
                                        End If

                                        If bApplicableOnPromotion = True Or strPromotionId = String.Empty Then
                                            Dim filter As String = dv.RowFilter
                                            dv.RowFilter = filter & " And ArticleCode='" & ItemCLP("ArticleCode").ToString() & "'"
                                            Dim ConvAmt, ConvPoints As Double
                                            If Not ItemCLP("Flat") Is DBNull.Value AndAlso ItemCLP("Flat") = True Then
                                                ConvPoints = IIf(ItemCLP("Points") Is DBNull.Value, 0, ItemCLP("Points"))
                                                'DiscountPer = IIf(ItemCLP("DiscountPer") Is DBNull.Value, 0, ItemCLP("DiscountPer"))
                                                For Each Item As DataRowView In dv
                                                    'For Price changed check
                                                    If Item("IsPriceChanged") = False Or clsDefaultConfiguration.CLPOnPriceChange = True Then
                                                        'If CheckNode(ds.Tables("CLPARTICLEHIERARCHYMAP"), Item("ArticleCode"), strCLPProgramId) = True Then 
                                                        If Item("clprequire") Then
                                                            totalAmt = Item("Quantity")
                                                            If ConvPoints > 0 Then
                                                                dblClpPoints = totalAmt * ConvPoints
                                                                dblCurBilPts = dv.ToTable.Compute("Sum(IsNull(CLPPoints,0))", "")
                                                                If Item("CLPPoints") Is DBNull.Value Or Item("CLPPoints") = 0 Then
                                                                    If ((dblClpPoints + dblCurBilPts + dblCustAccPoints) <= dblMaxAccLimit Or bMaxLimitExists = False) Then

                                                                        'checks for flag converts in multilples
                                                                        If bConvertinMultiples Then
                                                                            Item("CLPPoints") = Math.Floor(dblClpPoints)
                                                                        Else

                                                                            Item("CLPPoints") = dblClpPoints
                                                                        End If

                                                                    ElseIf (dblClpPoints + dblCurBilPts + dblCustAccPoints) > dblMaxAccLimit Or bMaxLimitExists = False Then

                                                                        'checks for flag converts in multilples
                                                                        If bConvertinMultiples Then
                                                                            Item("CLPPoints") = Math.Floor(dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints))
                                                                        Else

                                                                            Item("CLPPoints") = dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints)
                                                                        End If

                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            Else
                                                ConvAmt = IIf(ItemCLP("AMTVALUE") Is DBNull.Value, 0, ItemCLP("AMTVALUE"))
                                                ConvPoints = IIf(ItemCLP("Points") Is DBNull.Value, 0, ItemCLP("Points"))
                                                'DiscountPer = IIf(ItemCLP("DiscountPer") Is DBNull.Value, 0, ItemCLP("DiscountPer"))



                                                For Each Item As DataRowView In dv

                                                    If Item("IsPriceChanged") = False Or clsDefaultConfiguration.CLPOnPriceChange = True Then
                                                        ' If CheckNode(ds.Tables("CLPARTICLEHIERARCHYMAP"), Item("ArticleCode"), strCLPProgramId) = True Then 
                                                        If Item("clprequire") Then
                                                            totalAmt = Item("NetAmount") / ConvAmt
                                                            totalAmt = Math.Round(totalAmt, 2)
                                                            If ConvPoints > 0 Then
                                                                dblClpPoints = totalAmt * ConvPoints
                                                                dblCurBilPts = IIf(dv.ToTable.Compute("Sum(CLPPoints)", "") Is DBNull.Value, 0, dv.ToTable.Compute("Sum(CLPPoints)", ""))
                                                                If Item("CLPPoints") Is DBNull.Value OrElse Item("CLPPoints") = 0 Then
                                                                    If (dblClpPoints + dblCurBilPts + dblCustAccPoints) <= dblMaxAccLimit Or bMaxLimitExists = False Then

                                                                        If bConvertinMultiples Then
                                                                            Item("CLPPoints") = Math.Floor(dblClpPoints)
                                                                        Else
                                                                            If (totalAmt.ToString().Equals("Infinity")) Then
                                                                                Item("CLPPoints") = ConvPoints
                                                                            Else
                                                                                Item("CLPPoints") = dblClpPoints
                                                                            End If
                                                                        End If

                                                                    ElseIf (dblClpPoints + dblCurBilPts + dblCustAccPoints) > dblMaxAccLimit Or bMaxLimitExists = False Then

                                                                        If bConvertinMultiples Then
                                                                            Item("CLPPoints") = Math.Floor(dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints))
                                                                        Else

                                                                            Item("CLPPoints") = dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints)
                                                                        End If

                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                            dv.RowFilter = filter
                                        End If
                                    Next

                                    'If dv.RowFilter = " " Then
                                    '    dv.RowFilter = " isnull(CLPPOINTS,0)=0"
                                    'Else
                                    '    dv.RowFilter = dv.RowFilter & " AND isnull(CLPPOINTS,0)=0"
                                    'End If

                                    dv.AllowEdit = True
                                    dv.RowFilter = dv.RowFilter & " AND  Isnull(CLPPoints,0)='0'"
                                    For Each Row As DataRowView In dv
                                        If IIf(Row("IsPriceChanged") Is DBNull.Value, False, Row("IsPriceChanged")) = False Or clsDefaultConfiguration.CLPOnPriceChange = True Then
                                            strPromotionId = IIf(Row("FirstLevel") Is DBNull.Value, String.Empty, Row("FirstLevel"))
                                            If bApplicableOnPromotion = True Or strPromotionId = String.Empty Then


                                                If Row("clprequire") Then


                                                    'This check is done at article level
                                                    'If CheckNode(ds.Tables("CLPARTICLEHIERARCHYMAP"), Row("ArticleCode"), strCLPProgramId) = True Then
                                                    totalAmt = Row("NetAmount") / AllConvAmT
                                                    totalAmt = Math.Round(totalAmt, 2)
                                                    If AllConvPoint > 0 Then
                                                        dblClpPoints = Convert.ToDouble(totalAmt * AllConvPoint)
                                                        '---Changed By Mahesh dblCurBilPts need to calculate Proper Value
                                                        'dblCurBilPts = IIf(dv.ToTable.Compute("Sum(CLPPoints)", "") Is DBNull.Value, 0, dv.ToTable.Compute("Sum(CLPPoints)", ""))
                                                        dblCurBilPts = IIf(dv.Table.Compute("Sum(CLPPoints)", "") Is DBNull.Value, 0, dv.Table.Compute("Sum(CLPPoints)", ""))
                                                        If Row("CLPPoints") Is DBNull.Value Or IsDBNull(Row("CLPPoints")) = 0 Then
                                                            If (dblClpPoints + dblCurBilPts + dblCustAccPoints) <= dblMaxAccLimit Or bMaxLimitExists = False Then

                                                                'checks for flag converts in multilples
                                                                If bConvertinMultiples Then
                                                                    Row("CLPPoints") = Math.Floor(dblClpPoints)

                                                                Else

                                                                    Row("CLPPoints") = dblClpPoints
                                                                End If


                                                            ElseIf (dblClpPoints + dblCurBilPts + dblCustAccPoints) > dblMaxAccLimit Or bMaxLimitExists = False Then
                                                                'checks for flag converts in multilples
                                                                If bConvertinMultiples Then
                                                                    Row("CLPPoints") = Math.Floor(dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints))
                                                                Else

                                                                    Row("CLPPoints") = dblMaxAccLimit - (dblCurBilPts + dblCustAccPoints)
                                                                End If

                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If

                    End If
                End If
                If clsDefaultConfiguration.CLP_Point_On_redeemption = False Then
                    If Not dtpayment Is Nothing Then
                        If bAccWhileRed = False Then
                            Dim filterstr As String
                            If (bOnlyAtCreationSite = False Or ds.Tables("CLPCUSTOMERS").Rows.Count > 0) AndAlso (dblCustAccPoints < dblMaxAccLimit Or bMaxLimitExists = False) Then
                                If dtpayment.Select("NOCLP='True'").Count > 0 Then
                                    If Not clsDefaultConfiguration.CLPOnPriceChange Then
                                        filterstr = ItemFilter
                                        filterstr += " And isnull(Ispricechanged,0)<>'1'"
                                    Else
                                        filterstr = ItemFilter
                                    End If
                                    Dim dv As New DataView(dtItem, "FIRSTLEVEL<>''", "", DataViewRowState.CurrentRows)
                                    If bApplicableOnPromotion = False And dv.Count > 0 Then
                                        filterstr += " And FIRSTLEVEL=''"
                                    End If

                                    For Each dr As DataRow In dtItem.Rows
                                        dr("CLPPoints") = 0
                                    Next

                                    Dim clp_Logic As New CLP_Logic
                                    clp_Logic.NON_CLP_Tender_Calc(dtItem, dtpayment, ds.Tables("Header").Rows(0)("AMTVALUE"), ds.Tables("Header").Rows(0)("Points"), bConvertinMultiples, filterstr)
                                End If
                            End If

                        End If
                    End If
                End If
            End If
        Catch ax As ApplicationException
            ShowMessage(ax.Message, getValueByKey("CLAE04"))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Public Function emailaddresscheck(ByVal emailaddress As String) As Boolean
        Dim pattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim test = Regex.Match(emailaddress, pattern)
        Return test.Success
    End Function

    Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String

        Dim fi As FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim attr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

        If attr.Length > 0 Then
            Return attr(0).Description
        Else
            Return EnumConstant.ToString()
        End If
    End Function

    Public Sub DisableTransactionMainMenu(ByVal IsEnable As Boolean)

        For Each menuItem As ToolStripItem In MDISpectrum.MenuStrip.Items
            If Not ((menuItem.Name.Equals("HelpMenu")) Or (menuItem.Name.Equals("ReportsToolsStripOuterMenuItem")) Or (menuItem.Name.Equals("ToolsToolStripMenuItem"))) Then
                For Each subMenuItem As ToolStripItem In DirectCast(menuItem, ToolStripMenuItem).DropDown.Items
                    subMenuItem.Enabled = IsEnable
                Next
            End If
        Next

    End Sub

    ''' <summary>
    ''' On Screen Keyboard pop up on Text Click
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OnTouchKeyBoard()
        Try
            Dim procc = Process.GetProcessesByName("osk")
            If procc.Length = 0 Then
                Process.Start("osk")
            Else
                For Each proccShow As Process In Process.GetProcessesByName("osk")
                    If proccShow.ProcessName = "osk" Then
                        proccShow.Kill()
                        Process.Start("osk")

                    End If
                Next

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#Region "AndroidSearch"

    Public Sub SetWildSearchTextBox(ByRef dtBind As DataTable, ByRef WildSearchTextBox As AndroidSearchTextBox, key As String, Value As String, searchData As String)
        Try
            If dtBind.Rows.Count > 0 Then
                dtBind.Columns(key).ColumnName = "key"
                dtBind.Columns(Value).ColumnName = "Value"
                dtBind.Columns(searchData).ColumnName = "searchData"
                Dim view As New DataView(dtBind, "", "", DataViewRowState.CurrentRows)
                WildSearchTextBox.DtSearchData = view.ToTable
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region

#Region "CForms"
    ''' <summary>
    ''' Data attach to combo box
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="dtCombo">Data table</param>
    ''' <param name="ObjComboBox">Name of ComboBox</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PopulateCustomerCombo(ByVal dtCombo As DataTable, ByRef ObjComboBox As ctrlCombo)

        ObjComboBox.DataSource = dtCombo
        ObjComboBox.ValueMember = dtCombo.Columns(0).ColumnName
        ObjComboBox.DisplayMember = dtCombo.Columns(1).ColumnName
        ObjComboBox.SelectedIndex = -1

        Return ""
    End Function
#End Region

    '--------------------------------------------------------------------------
    'added on 12 may - ashma - for Innoviti
    Dim objclscm As New clsCommon
    Public Function GetCashBillno(ByVal terminalid As String, ByVal DayOpenDate As Date, ByVal Fyear As String) As String
        Dim Month, day, year, Quarter As Int32
        Month = clsAdmin.DayOpenDate.Month
        day = clsAdmin.DayOpenDate.Day
        year = clsAdmin.DayOpenDate.Year
        Dim DocNo As String = objclscm.getDocumentNo("CM", clsAdmin.SiteCode)
        If OnlineConnect = False Then

            Try
                If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                    DocNo = GenDocNo("OCM" & terminalid & Fyear.Substring(Fyear.Length - 2, 2), 15, DocNo)
                Else
                    DocNo = GenDocNo("OCM" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, DocNo)
                End If
            Catch ex As Exception
                DocNo = "OCM" & terminalid & DocNo
            End Try

        Else

            Try

                If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                    DocNo = GenDocNo("CM" & terminalid & Fyear.Substring(Fyear.Length - 2, 2), 15, DocNo)
                Else
                    DocNo = GenDocNo("CM" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, DocNo)
                End If
            Catch ex As Exception
                DocNo = "CM" & terminalid & DocNo
            End Try
            Return DocNo
        End If
    End Function

    Private Sub writeDaycloseLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
    '--------------------------------------------------------------------------
#Region "health care (Vaidya notes)"
    Public Sub fnSetComboAfterSel(ByRef dtltb As DataTable, ByRef combo As ctrlCombo, ByVal strFilter As String)

        Dim dv As DataView = dtltb.DefaultView
        dv.RowFilter = strFilter

        combo.DataSource = dv.ToTable
        combo.ValueMember = dv.ToTable.Columns(0).ColumnName
        combo.DisplayMember = dv.ToTable.Columns(1).ColumnName
        pC1ComboSetDisplayMember(combo)
    End Sub
#End Region
End Module
