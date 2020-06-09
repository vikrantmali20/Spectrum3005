
Imports System.Text
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Threading
Imports System.Resources
Imports System.Globalization
Imports System.IO
Imports Microsoft.PointOfService
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports C1.Win.C1BarCode



Public Module modCommanFunction
    ''' <summary>
    '''  Birthlist transaction for printing  
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PrintTransactionSet
        CreateBirthList
        SaleBirthListItem
        EditBirthListItem
        ReturnBirthListItem
    End Enum

    
#Region "HardCoded Values "
    ''' <summary>
    ''' Default values for Credit Voucher Receipt .
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CreditVoucher_R() As String
        Get
            Return "CreditVouc(R)".ToUpper()
        End Get
    End Property

    ''' <summary>
    ''' Default values for Credit Voucher Issue .
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CreditVoucher_I() As String
        Get
            Return "CreditVouc(Iss)".ToUpper()
        End Get
    End Property
    ''' <summary>
    ''' Default values for Gift Voucher Receipt .
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GIFTVOUCHE_R() As String
        Get
            Return "GIFTVOUCHER(R)"
        End Get
    End Property
    ''' <summary>
    ''' Default values for Gift Voucher Issue .
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GIFTVOUCHE_I() As String
        Get
            Return "GIFTVOUCHER(I)"
        End Get
    End Property
#End Region

    Public resourceMgrPrint As ResourceManager = ResourceManager.CreateFileBasedResourceManager("MyResource", Application.StartupPath & "\MyResource", Nothing)
    Public VarBarcode As C1BarCode
    Public IsPrintVoucher As Boolean = False
    Public PBarCodeType As String = 0
    Private _PrintTransaction As PrintTransactionSet
    Public strZero As String = FormatNumber(0, 2).ToString

    ''' <summary>
    ''' Customer Print for sales,retrun or create BL items 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PrintTransaction() As PrintTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintTransactionSet)
            _PrintTransaction = value
        End Set
    End Property

    Private _IsPrintingWithDefaultFontReq = False
    Public Property IsPrintingWithDefaultFontReq() As Boolean
        Get
            Return _IsPrintingWithDefaultFontReq
        End Get
        Set(ByVal value As Boolean)
            _IsPrintingWithDefaultFontReq = value
        End Set
    End Property

    Private _BillFont As String
    Public Property BillFont() As String
        Get
            Return _BillFont
        End Get
        Set(ByVal value As String)
            _BillFont = value
        End Set
    End Property

    Private _dtPrinterInfo1 As DataTable
    Public Property dtPrinterInfo1() As DataTable
        Get
            Return _dtPrinterInfo1
        End Get
        Set(ByVal value As DataTable)
            _dtPrinterInfo1 = value
        End Set
    End Property
    Private _InnovitiResponseError As String
    'added by khusrao adil on 26-07-2017 for innoviti error log
    Public Property InnovitiResponseError() As String
        Get
            Return _InnovitiResponseError
        End Get
        Set(ByVal value As String)
            _InnovitiResponseError = value
        End Set
    End Property
    Private SalesItemDetails As DataTable
    Private _dtBLVoucherSales As DataTable
    ' ''' <summary>
    ' ''' Printing Document header
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _IsHeaderPrinting As Boolean
    'Public Property IsHeaderPrinting() As Boolean
    '    Get
    '        Return _IsHeaderPrinting
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsHeaderPrinting = value
    '    End Set
    'End Property
    ' ''' <summary>
    ' ''' Printing Document Bottom
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _IsFooterPrinting As Boolean
    'Public Property IsFooterPrinting() As Boolean
    '    Get
    '        Return _IsFooterPrinting
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsFooterPrinting = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Welcome Message print
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _IsWelcomeMsgPrint As Boolean
    'Public Property IsWelComeMessagePrint() As Boolean
    '    Get
    '        Return _IsWelcomeMsgPrint
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsWelcomeMsgPrint = value
    '    End Set
    'End Property
    ' ''' <summary>
    ' ''' Promotion message priting 
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _IsPromotionalMessage As Boolean
    'Public Property IsPromotionMessagePrint() As Boolean
    '    Get
    '        Return _IsPromotionalMessage
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsPromotionalMessage = value
    '    End Set
    'End Property
    ' ''' <summary>
    ' ''' Tax information printing on Document
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _IsTaxinformation As Boolean
    'Public Property IsTaxInformation() As Boolean
    '    Get
    '        Return _IsTaxinformation
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsTaxinformation = value
    '    End Set
    'End Property
    ''' <summary>
    ''' Printing Document Type
    ''' i.e  SO,BL,CM
    ''' </summary>
    ''' <remarks></remarks>
    Private _strDocumentType As String
    Public Property DocumentType() As String
        Get
            Return _strDocumentType
        End Get
        Set(ByVal value As String)
            _strDocumentType = value
        End Set
    End Property
    ''' <summary>
    ''' Printing Site Code
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSiteCode As String
    Public Property SiteCode() As String
        Get
            Return _strSiteCode
        End Get
        Set(ByVal value As String)
            _strSiteCode = value
        End Set
    End Property


    Private _CustomerDetail As DataTable
    Private _dtBirthListItemDetail As DataTable
    ''' <summary>
    ''' Customer details who ordered or sold or comes to return items 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomerDetail() As DataTable
        Get
            Return _CustomerDetail
        End Get
        Set(ByVal value As DataTable)
            _CustomerDetail = value
        End Set
    End Property
    '''' <summary>
    ''''  Customer ordered,sold ,return item 
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Property BirthListItemDetail() As DataTable
    '    Get
    '        Return _dtBirthListItemDetail
    '    End Get
    '    Set(ByVal value As DataTable)
    '        _dtBirthListItemDetail = value
    '    End Set
    'End Property
    ''' <summary>
    ''' Voucher Information 
    ''' </summary>
    ''' <remarks></remarks>

    Private _dtVoucherSales As DataTable
    Private Property VoucherDetail() As DataTable
        Get
            Return _dtVoucherSales
        End Get
        Set(ByVal value As DataTable)
            _dtVoucherSales = value
        End Set
    End Property
    ''' <summary>
    '''  Payment History  
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtPaymentHistory As DataTable
    Private Property PaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property
    Public Property _IsBookingPrint() As Boolean = False
    Public Property IsBookingPrint() As Boolean
        Get
            Return _IsBookingPrint
        End Get
        Set(ByVal value As Boolean)
            _IsBookingPrint = value
        End Set
    End Property
    Public Property _IsEvassChanges() As Boolean = False
    Public Property IsEvassChanges() As Boolean
        Get
            Return _IsEvassChanges
        End Get
        Set(ByVal value As Boolean)
            _IsEvassChanges = value
        End Set
    End Property
    Public Property _SpectrumAsMettlerBarcode() As Boolean = False
    Public Property SpectrumAsMettlerBarcode() As Boolean
        Get
            Return _SpectrumAsMettlerBarcode
        End Get
        Set(ByVal value As Boolean)
            _SpectrumAsMettlerBarcode = value
        End Set
    End Property
    Private _dtBirthListCustomerInfo As DataRow
    ''' <summary>
    '''  Sales and Edit BirthList : Birthlist owner information 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BirthListCustomerInfo() As DataRow
        Get
            Return _dtBirthListCustomerInfo
        End Get
        Set(ByVal value As DataRow)
            _dtBirthListCustomerInfo = value
        End Set
    End Property

    Private _decimalDigits As Integer = 2
    Public Property DecimalDigits() As Integer
        Get
            Return _decimalDigits
        End Get
        Set(ByVal value As Integer)
            _decimalDigits = value
        End Set
    End Property

    Public _printMarginTop As Integer = 35
    Public Property PrintMarginTop() As Integer
        Get
            Return _printMarginTop
        End Get
        Set(ByVal value As Integer)
            _printMarginTop = value
        End Set
    End Property

    Public _printMarginBottom As Integer = 35
    Public Property PrintMarginBottom() As Integer
        Get
            Return _printMarginBottom
        End Get
        Set(ByVal value As Integer)
            _printMarginBottom = value
        End Set
    End Property

    Public _barcodeMarginTop As Integer
    Public Property BarcodeMarginTop() As Integer
        Get
            Return _barcodeMarginTop
        End Get
        Set(ByVal value As Integer)
            _barcodeMarginTop = value
        End Set
    End Property

    Private _PrintFormatNo As Integer
    Public Property PrintFormatNo() As Integer
        Get
            Return _PrintFormatNo
        End Get
        Set(ByVal value As Integer)
            _PrintFormatNo = value
        End Set
    End Property
    'Function returns the value form a resource file, when supplied with a key.
    Public Function getValueByKey(ByVal strKey As String) As String
        If Not resourceMgrPrint Is Nothing Then
            If Not resourceMgrPrint.GetString(strKey) Is Nothing Then
                Return resourceMgrPrint.GetString(strKey)
            Else
                Return " "
            End If

        Else
            Return "Error"
        End If

    End Function



    ''' <summary>
    ''' Get DefaultSeting Details
    ''' </summary>
    ''' <param name="strSiteCode">SiteCode</param>
    ''' <param name="Doctype">DocType</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' 
    Public VarBarcode1 As C1BarCode
    Dim lastAppliedFont As String = String.Empty
    Public strLineL40 As String = "---------------------------------------"
    Public strLineA4 As String = "_________________________________________________________________________________________________________________________"

    Public Function GetDefaultSetting(ByVal strSiteCode As String, ByVal Doctype As String) As DataTable
        Try
            Dim strString As String = "SELECT DOCUMENTTYPE,DESCRIPTION,FLDLABEL,FLDVALUE FROM DEFAULTCONFIG WHERE DOCUMENTGROUP='FO' AND Status=1 AND "
            If strSiteCode <> "0000" Then
                strString = strString & " SITECODE='" & strSiteCode & "' AND "
            End If
            If Doctype <> String.Empty Then
                strString = strString & "  DOCUMENTTYPE='" & Doctype & "'"
            End If
            If Right(strString, 4) = "AND " Then
                strString = strString.Substring(0, strString.Length - 4)
            End If
            strString = strString & " Order By DOCUMENTTYPE,FLDLABEL"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            'LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    '''  Print setting for birthlist for all birthlist printing 
    ''' </summary>
    ''' <UsedBy>frmBirthListCreate.vb,frmBirthListSale.vb,frmBirthListUpdate.vb</UsedBy>
    ''' <param name="StrSubHeader"></param>
    ''' <param name="StrFooter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Function PrinttHeaderAndFooter(ByRef StrSubHeader As StringBuilder, ByRef StrFooter As StringBuilder, ByRef strWelComeMessage As StringBuilder, ByRef strPromotionmsg As StringBuilder, ByRef strTaxinformation As StringBuilder, ByVal strDocumentType As String, ByVal strPrintPageSetup As String) As Boolean
        Try
            Dim dtPrint As DataTable
            Dim objComm As New SpectrumBL.clsCommon
            dtPrint = objComm.GetPrintingDetails(strDocumentType)
            StrSubHeader.Length = 0
            If Not dtPrint Is Nothing Then
                Dim filter As String = ""
                Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
                'If IsHeaderPrinting = True Then
                filter = "TOPBOTTOM='Top'"
                StrSubHeader = GetInformationMsgs(dtPrint, strPrintPageSetup, filter)
                'End If
                'If IsFooterPrinting = True Then
                filter = "TOPBOTTOM='BOTTOM' "
                StrFooter = GetInformationMsgs(dtPrint, strPrintPageSetup, filter)
                'End If
                'If IsWelComeMessagePrint = True Then
                filter = "TOPBOTTOM='Welcome' "
                strWelComeMessage = GetInformationMsgs(dtPrint, strPrintPageSetup, filter)
                'End If
                'If IsTaxInformation = True Then
                filter = "TOPBOTTOM='Tax' "
                strTaxinformation = GetInformationMsgs(dtPrint, strPrintPageSetup, filter)
                'End If
                'If IsPromotionMessagePrint = True Then
                filter = "TOPBOTTOM='Promo'"
                strPromotionmsg = GetInformationMsgs(dtPrint, strPrintPageSetup, filter)
                'End If
            End If
            Return True
        Catch ex As Exception

        End Try

    End Function

    Private Function GetInformationMsgs(ByVal dtPrint As DataTable, ByVal strPrintPageSetup As String, ByVal strFilter As String) As StringBuilder
        Try
            Dim StrInformationMsg As New StringBuilder
            Dim dv As New DataView(dtPrint, "", "Sequenceno", DataViewRowState.CurrentRows)
            If Not dv Is Nothing Then
                Dim StrPrintLine As String
                dv.RowFilter = strFilter

                For Each drview As DataRowView In dv
                    StrPrintLine = drview("ReceiptText").ToString()
                    If Not drview("Align") Is Nothing AndAlso drview("Align").ToString() <> String.Empty Then
                        'StrPrintHeaderLine
                    End If
                    If strPrintPageSetup = "L40" Then
                        StrInformationMsg = StrInformationMsg.AppendLine(SplitString(StrPrintLine, 39).ToString().Trim())
                    Else
                        StrInformationMsg = StrInformationMsg.AppendLine(StrPrintLine)
                    End If
                Next
            End If
            Return StrInformationMsg
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function A4GetStringTermsCondition(ByVal strDocumentType As String, ByVal strSiteCode As String, ByVal strPrintPageSetup As String) As String
        Try
            Dim dtPrint As DataTable
            Dim StrSubTnC As New StringBuilder
            Dim StrPrintTermsConditionsline As String
            Dim objcomm As New SpectrumBL.clsCommon
            StrSubTnC.Length = 0
            dtPrint = objcomm.GetTermsNCondition(strSiteCode, strDocumentType)
            If Not dtPrint Is Nothing Then
                Dim filter As String = ""
                Dim dv As New DataView(dtPrint, filter, "SiteCode", DataViewRowState.CurrentRows)
                If (dtPrint.Rows.Count > 0) Then
                    If Not strPrintPageSetup = "L40" Then
                        'StrSubTnC.Append("Terms and Conditions" + vbCrLf + strLineA4 + vbCrLf)
                        StrSubTnC.Append(getValueByKey("MDCMFN001"))
                    Else
                        'StrSubTnC.Append(vbCrLf + "Terms and Conditions" + vbCrLf)
                        StrSubTnC.Append(getValueByKey("MDCMFN001"))
                    End If
                End If

                For i As Integer = 1 To dtPrint.Rows.Count
                    filter = "TnCsrno='" & i & "'"
                    dv.RowFilter = filter
                    For Each drview As DataRowView In dv
                        StrPrintTermsConditionsline = drview("Description").ToString()

                        If strPrintPageSetup = "L40" Then
                            StrSubTnC = StrSubTnC.AppendLine(vbCrLf + SplitString(StrPrintTermsConditionsline).ToString())
                        Else
                            StrSubTnC = StrSubTnC.AppendLine(vbCrLf + StrPrintTermsConditionsline)
                        End If
                    Next
                Next
            End If

            Return StrSubTnC.ToString


        Catch ex As Exception
            Return ""
        End Try
    End Function


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
            'Dim device3 As DeviceInfo = explorer.GetDevice("CashDrawer", My.Settings.Drawer.ToString)
            Dim device3 As DeviceInfo = explorer.GetDevice("CashDrawer", SpectrumBL.clsLogin.ReadSpectrumParamFile("Drawer"))

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
            gOposPolDisplay.DisplayText(getValueByKey("mod001"))
            lblDeviceStatus.Text = getValueByKey("mod001") & e.Device.ServiceObjectName
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
            gOposPolDisplay.DisplayText(getValueByKey("mod002"))
            lblDeviceStatus.Text = getValueByKey("mod002") & e.Device.ServiceObjectName
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
            'Dim device3 As DeviceInfo = explorer.GetDevice("CashDrawer", My.Settings.Drawer.ToString)
            Dim device3 As DeviceInfo = explorer.GetDevice("CashDrawer", SpectrumBL.clsLogin.ReadSpectrumParamFile("Drawer"))
            If Not device3 Is Nothing Then
                gOposCashDrawer = explorer.CreateInstance(device3)
            End If
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
        Catch ex As Exception

        End Try

        'Update()

    End Sub


    Private Sub gOposMSR_ErrorEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.DeviceErrorEventArgs) Handles gOposMSR.ErrorEvent
        lblDeviceStatus.Text = getValueByKey("mod008")
        'Update()
        gOposMSR.DataEventEnabled = True

    End Sub

#End Region

    'Public gPrinterName As String = ""
    Private _PrinterType As String
    Public Property PrinterType() As String
        Get
            Return _PrinterType
        End Get
        Set(ByVal value As String)
            _PrinterType = value
        End Set
    End Property
    Private _PrinterName As String
    Private _DrawerName As String
    Public Property PrinterName() As String
        Get
            'If My.Settings.OtherPrinter.ToString = String.Empty Then
            '    Return My.Settings.POSPrinter
            'Else
            '    Return My.Settings.OtherPrinter
            'End If
            Return _PrinterName
        End Get
        Set(ByVal value As String)
            'If My.Settings.POSPrinter.ToString = String.Empty Then
            '    My.Settings.POSPrinter = value
            'Else
            '    My.Settings.OtherPrinter = value
            'End If
            _PrinterName = value
        End Set
    End Property

    Public Property DrawerName() As String
        Get
            Return _DrawerName
        End Get
        Set(ByVal value As String)
            _DrawerName = value
        End Set
    End Property
    
    Private stringToPrint, documentContents, stringPrintForPreview As String
    Public WithEvents printPreviewDialog1 As New CustomPrintPreviewDialog  'PrintPreviewDialog()
    Private WithEvents printDocument1 As New PrintDocument()

    'Public gPrinterName As String = My.Settings.OtherPrinter   '"HP LaserJet 2420 PCL 6_ "

    'Private gPrinterName As String = "TVSPrinter"
    'There are 5 POS devices for use with this application 
    Private WithEvents explorer As New PosExplorer
    'WithEvents explorer As PosExplorer
    WithEvents gOposScanner As Scanner
    WithEvents gOposMSR As Msr

    Public gOposPolDisplay As LineDisplay
    Dim gOposPrinter As PosPrinter
    Dim gOposCashDrawer As CashDrawer
    WithEvents gOposFiscalPrinter As FiscalPrinter

   
     
    ''' <summary>
    ''' Function for Printing 
    ''' </summary>
    ''' <param name="StrToPrint">String to print</param>
    ''' <param name="strPrintOrPreview">PRN/PRV</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  

    '    Public Function fnPrint(ByVal StrToPrint As String, ByVal strPrintOrPreview As String) As String
    '        ' strPrintOrPreview = 'PRN' that means Print
    '        ' strPrintOrPreview = 'PRV' that means Preview
    '        Dim retryCount As Integer = 0

    'RetryPrint:
    '        printDocument1.DefaultPageSettings.Margins.Left = 0
    '        printDocument1.DefaultPageSettings.Margins.Top = 40
    '        printDocument1.DefaultPageSettings.Margins.Right = 0
    '        'Rohit Start
    '        If PrinterType = "OPOS" Then

    '            Try

    '                Dim strSmallfont As String = Chr(27) + Chr(77) + Chr(49)

    '                Dim cA4Print As New clsA4Print
    '                cA4Print.OperateDevice("PosPrinter", strSmallfont & StrToPrint)

    '                'Rohit End
    '                fnPrint = "Success"

    '            Catch ex As Exception
    '                fnPrint = "Failed"
    '                If printDocument1.PrinterSettings.IsValid = False Then
    '                    MessageBox.Show(String.Format(getValueByKey("CLBL03"), _PrinterName), "CLBL03 - " & getValueByKey("CLAE04"))
    '                End If
    '            Finally
    '                VarBarcode1 = Nothing
    '                VarBarcode = Nothing
    '            End Try

    '        Else ' to print the document from windows drivers
    '            Try
    '                stringToPrint = StrToPrint
    '                stringPrintForPreview = StrToPrint
    '                printDocument1.DefaultPageSettings.Margins.Left = 0
    '                printDocument1.DefaultPageSettings.Margins.Right = 0
    '                printDocument1.DefaultPageSettings.Margins.Top = 10
    '                printDocument1.PrinterSettings.PrinterName = PrinterName

    '                If strPrintOrPreview = "PRN" Then
    '                    printPreviewDialog1.Document = printDocument1
    '                    printDocument1.PrinterSettings.PrintToFile = True
    '                    printDocument1.Print()
    '                Else
    '                    printPreviewDialog1.Document = printDocument1
    '                    printPreviewDialog1.WindowState = FormWindowState.Maximized
    '                    printDocument1.PrinterSettings.PrintToFile = True
    '                    stringToPrint = StrToPrint
    '                    stringPrintForPreview = StrToPrint
    '                    printPreviewDialog1.ShowDialog()
    '                    printPreviewDialog1.Select()
    '                End If
    '            Catch ex As Exception
    '                fnPrint = "Failed"
    '                If printDocument1.PrinterSettings.IsValid = False Then
    '                    'If MsgBox("Invalid Printer found ", MsgBoxStyle.AbortRetryIgnore, "Error") = MsgBoxResult.Retry Then
    '                    If MsgBox(getValueByKey("MFC001"), MsgBoxStyle.AbortRetryIgnore, getValueByKey("CLAE05")) = MsgBoxResult.Retry Then
    '                        retryCount = retryCount + 1
    '                        If retryCount < 3 Then
    '                            GoTo RetryPrint
    '                        End If
    '                    End If
    '                End If
    '            Finally

    '                VarBarcode1 = Nothing
    '                VarBarcode = Nothing
    '            End Try
    '        End If

    '    End Function

    Public Function fnPrint(ByVal StrToPrint As String, ByVal strPrintOrPreview As String, Optional ByVal topmargin As Int32 = 0, Optional ByVal NoOfCopies As Integer = 1) As String

        ' strPrintOrPreview = 'PRN' that means Print
        ' strPrintOrPreview = 'PRV' that means Preview

        Dim autoCutCommond As String = Convert.ToChar(27) + "@" + Convert.ToChar(29) + "V" + Convert.ToChar(1)
        Dim feedCommond As String = Convert.ToChar(29) + "V" + Convert.ToChar(65) + Convert.ToChar(0)

        Dim retryCount As Integer = 0

RetryPrint:

        If topmargin = 0 Then
            printDocument1.DefaultPageSettings.Margins.Top = PrintMarginTop
            printDocument1.DefaultPageSettings.Margins.Bottom = PrintMarginBottom
        Else
            printDocument1.DefaultPageSettings.Margins.Top = topmargin
        End If

        printDocument1.DefaultPageSettings.Margins.Left = 0
        printDocument1.DefaultPageSettings.Margins.Right = 1

        'Rohit Start
        If PrinterType = "OPOS" Then

            Try

                Dim strSmallfont As String = Chr(27) + Chr(77) + Chr(49)

                'Dim cA4Print As New clsA4Print
                'cA4Print.OperateDevice("PosPrinter", strSmallfont & StrToPrint)
                Dim cA4Print As New clsA4PrintNew
                cA4Print.OperateDevice("PosPrinter", strSmallfont & StrToPrint)

                'Rohit End
                fnPrint = "Success"

            Catch ex As Exception
                fnPrint = "Failed"
                If printDocument1.PrinterSettings.IsValid = False Then
                    MessageBox.Show(String.Format(getValueByKey("CLBL03"), _PrinterName), "CLBL03 - " & getValueByKey("CLAE04"))
                End If
            Finally
                VarBarcode1 = Nothing
                VarBarcode = Nothing
            End Try

        Else ' to print the document from windows drivers
            Try
                stringToPrint = StrToPrint
                stringPrintForPreview = StrToPrint
                printDocument1.DefaultPageSettings.Margins.Left = 0
                printDocument1.DefaultPageSettings.Margins.Right = 1
                printDocument1.PrinterSettings.PrinterName = PrinterName


                If strPrintOrPreview = "PRN" Then
                    If IsPrintingWithDefaultFontReq Then
                        RawDataPrint.RawPrinterHelper.PrintInvoiceWithAutocutAndFeedPaper(PrinterName, StrToPrint, True)
                        'RawDataPrint.RawPrinterHelper.SendStringToPrinter(PrinterName, StrToPrint)
                    Else
                        stringToPrint = StrToPrint
                        stringPrintForPreview = StrToPrint
                        printDocument1.DefaultPageSettings.Margins.Left = 0
                        printDocument1.DefaultPageSettings.Margins.Right = 0
                        printDocument1.DefaultPageSettings.Margins.Top = 0
                        printDocument1.PrinterSettings.PrinterName = PrinterName
                        printPreviewDialog1.Document = printDocument1
                        printDocument1.PrinterSettings.PrintToFile = True
                        '  printDocument1.Print()
                        'code added for number of print copies
                        If NoOfCopies = 0 Then
                            printDocument1.Print()
                        Else
                            For index = 1 To NoOfCopies
                                printDocument1.Print()
                            Next
                        End If
                    End If
                Else
                    printPreviewDialog1.Document = printDocument1
                    printPreviewDialog1.WindowState = FormWindowState.Maximized
                    printDocument1.PrinterSettings.PrintToFile = True

                    stringToPrint = StrToPrint
                    stringPrintForPreview = StrToPrint
                    printPreviewDialog1.Select()
                    printPreviewDialog1.ShowDialog()
                End If
            Catch ex As Exception
                fnPrint = "Failed"
                If printDocument1.PrinterSettings.IsValid = False Then
                    'If MsgBox("Invalid Printer found ", MsgBoxStyle.AbortRetryIgnore, "Error") = MsgBoxResult.Retry Then
                    If MsgBox(getValueByKey("MFC001"), MsgBoxStyle.AbortRetryIgnore, getValueByKey("CLAE05")) = MsgBoxResult.Retry Then
                        retryCount = retryCount + 1
                        If retryCount < 3 Then
                            GoTo RetryPrint
                        End If
                    End If
                End If
            Finally

                VarBarcode1 = Nothing
                VarBarcode = Nothing
            End Try
        End If

    End Function

    '    Public Function fnPrint(ByVal StrToPrint As String, ByVal strPrintOrPreview As String) As String
    '        ' strPrintOrPreview = 'PRN' that means Print
    '        ' strPrintOrPreview = 'PRV' that means Preview
    '        Dim retryCount As Integer = 0

    'RetryPrint:
    '        Try
    '            If strPrintOrPreview = "PRN" Then
    '                If IsPrintingWithDefaultFontReq Then
    '                    RawDataPrint.RawPrinterHelper.SendStringToPrinter(PrinterName, StrToPrint)
    '                Else
    '                    stringToPrint = StrToPrint
    '                    stringPrintForPreview = StrToPrint
    '                    printDocument1.DefaultPageSettings.Margins.Left = 0
    '                    printDocument1.DefaultPageSettings.Margins.Right = 0
    '                    printDocument1.DefaultPageSettings.Margins.Top = 0
    '                    printDocument1.PrinterSettings.PrinterName = PrinterName
    '                    printPreviewDialog1.Document = printDocument1
    '                    printDocument1.PrinterSettings.PrintToFile = True
    '                    printDocument1.Print()
    '                End If
    '            Else
    '                stringToPrint = StrToPrint
    '                stringPrintForPreview = StrToPrint
    '                printDocument1.DefaultPageSettings.Margins.Left = 0
    '                printDocument1.DefaultPageSettings.Margins.Right = 0
    '                printDocument1.DefaultPageSettings.Margins.Top = 0
    '                printDocument1.PrinterSettings.PrinterName = PrinterName
    '                printPreviewDialog1.Document = printDocument1
    '                printPreviewDialog1.WindowState = FormWindowState.Maximized
    '                printDocument1.PrinterSettings.PrintToFile = True
    '                stringToPrint = StrToPrint
    '                stringPrintForPreview = StrToPrint
    '                printPreviewDialog1.ShowDialog()
    '                printPreviewDialog1.Select()
    '            End If

    '            fnPrint = String.Empty
    '        Catch ex As Exception
    '            fnPrint = "Failed"
    '            'If printDocument1.PrinterSettings.IsValid = False Then
    '            '    'If MsgBox("Invalid Printer found ", MsgBoxStyle.AbortRetryIgnore, "Error") = MsgBoxResult.Retry Then
    '            '    If MsgBox(getValueByKey("MFC001"), MsgBoxStyle.AbortRetryIgnore, getValueByKey("CLAE05")) = MsgBoxResult.Retry Then
    '            '        retryCount = retryCount + 1
    '            '        If retryCount < 3 Then
    '            '            GoTo RetryPrint
    '            '        End If
    '            '    End If
    '            'End If
    '        Finally

    '            VarBarcode1 = Nothing
    '            VarBarcode = Nothing
    '        End Try
    '    End Function

    Public Function A4GetCustomerDetails(ByVal _dtCustomerDetails As DataTable) As String
        Try
            Dim sbCustomerdetails As New StringBuilder

            'Dim strCustomerName As String = "Customer Name : "
            Dim strCustomerName As String

            strCustomerName = getValueByKey("MDCMFN002")
            strCustomerName = strCustomerName + _dtCustomerDetails.Rows(0)("CustomerName").ToString().Trim()
            sbCustomerdetails.Append(strCustomerName + vbCrLf)

            A4GetCustomerAddress(_dtCustomerDetails, sbCustomerdetails, 1)
            'Dim strPhone As String = "Phone:"
            Dim strPhone As String = getValueByKey("MDCMFN003")
            Dim strdbPhone As Object = _dtCustomerDetails.Rows(0)("MobileNo").ToString().Trim()
            If Not strdbPhone Is DBNull.Value Then
                strPhone = strPhone.PadRight(10) + strdbPhone.ToString()
                sbCustomerdetails.Append(strPhone + vbCrLf)
            End If


            'Dim strEmailId As String = "e-mail:"
            Dim strEmailId As String = getValueByKey("MDCMFN004")
            Dim strdbEmailid As Object = _dtCustomerDetails.Rows(0)("EmailId")
            If Not strdbEmailid Is DBNull.Value Then
                strEmailId = strEmailId.PadRight(10) + strdbEmailid.ToString()
                sbCustomerdetails.Append(strEmailId + vbCrLf)
            End If

            Return sbCustomerdetails.ToString
        Catch ex As Exception

        End Try

    End Function

    Public Function A4GetCustomerDeliveryDetails(ByVal _dtCustomerDetails As DataTable) As String
        Dim sbCustomerDeliveryDetails As New StringBuilder

        A4GetCustomerAddress(_dtCustomerDetails, sbCustomerDeliveryDetails, 2)
        Return sbCustomerDeliveryDetails.ToString
    End Function

    Public Function A4GetCustomerAddress(ByVal dtCustomerAddress As DataTable, ByRef sbCustomerdetails As StringBuilder, ByVal iAddressType As Integer) As Boolean
        Try


            Dim sbAddressMain, strAddressMain As String
            Dim strAddressLn1, strAddressLn2, strAddressLn3, strAddressLn4, strPincode As String

            If iAddressType = "1" Then
                'strAddressMain = "Customer Address:  "
                strAddressMain = getValueByKey("MDCMFN005")
            Else
                'strAddressMain = "Delivery Address:  "
                strAddressMain = getValueByKey("MDCMFN006")
            End If
            sbAddressMain = String.Empty

            Dim drView As New DataView(dtCustomerAddress, "ADDRESSTYPE=" + iAddressType.ToString(), "", DataViewRowState.CurrentRows)
            For Each drRowView As DataRowView In drView

                strAddressLn1 = IIf(drRowView("AddressLn1") IsNot DBNull.Value, drRowView("AddressLn1").ToString.Trim, String.Empty)
                strAddressLn2 = IIf(drRowView("AddressLn2") IsNot DBNull.Value, drRowView("AddressLn2").ToString.Trim, String.Empty)
                strAddressLn3 = IIf(drRowView("AddressLn3") IsNot DBNull.Value, drRowView("AddressLn3").ToString.Trim, String.Empty)
                strAddressLn4 = IIf(drRowView("AddressLn4") IsNot DBNull.Value, drRowView("AddressLn4").ToString.Trim, String.Empty)
                strPincode = IIf(drRowView("Pincode") IsNot DBNull.Value, drRowView("Pincode").ToString.Trim, String.Empty)

                If Not strAddressLn1 = String.Empty Then
                    sbAddressMain += strAddressLn1
                End If
                If Not strAddressLn2 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn2, ", " + strAddressLn2)
                End If
                If Not strAddressLn3 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn3, ", " + strAddressLn3)
                End If
                If Not strAddressLn4 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn4, ", " + strAddressLn4)
                End If
                If Not strPincode = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strPincode, ", " + strPincode)
                End If

                sbAddressMain = SplitString(sbAddressMain, 80, 20).ToString()
            Next

            sbCustomerdetails.Append(strAddressMain + sbAddressMain + vbCrLf)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function L40GetCustomerAddress(ByVal dtCustomerAddress As DataTable, ByRef sbCustomerdetails As StringBuilder, ByVal iAddressType As Integer) As String
        Try
            Dim sbAddressMain As String = String.Empty
            Dim strAddressLn1, strAddressLn2, strAddressLn3, strAddressLn4, strCity, strPincode As String

            Dim drView As New DataView(dtCustomerAddress, "ADDRESSTYPE='" & iAddressType & "'", "", DataViewRowState.CurrentRows)
            For Each drRowView As DataRowView In drView

                strAddressLn1 = IIf(drRowView("AddressLn1") IsNot DBNull.Value, drRowView("AddressLn1").ToString.Trim, String.Empty)
                strAddressLn2 = IIf(drRowView("AddressLn2") IsNot DBNull.Value, drRowView("AddressLn2").ToString.Trim, String.Empty)
                strAddressLn3 = IIf(drRowView("AddressLn3") IsNot DBNull.Value, drRowView("AddressLn3").ToString.Trim, String.Empty)
                strAddressLn4 = IIf(drRowView("AddressLn4") IsNot DBNull.Value, drRowView("AddressLn4").ToString.Trim, String.Empty)
                strCity = IIf(drRowView("City") IsNot DBNull.Value, drRowView("City").ToString.Trim, String.Empty)
                strPincode = IIf(drRowView("Pincode") IsNot DBNull.Value, drRowView("Pincode").ToString.Trim, String.Empty)

                If Not strAddressLn1 = String.Empty Then
                    sbAddressMain += strAddressLn1
                End If
                If Not strAddressLn2 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn2, ", " + strAddressLn2)
                End If
                If Not strAddressLn3 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn3, ", " + strAddressLn3)
                End If
                If Not strAddressLn4 = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strAddressLn4, ", " + strAddressLn4)
                End If
                If Not strCity = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strCity, ", " + strCity)
                End If

                If Not strPincode = String.Empty Then
                    sbAddressMain += IIf(String.IsNullOrEmpty(sbAddressMain), strPincode, ", " + strPincode)
                End If

                'sbCustomerdetails.Append(sbAddressMain.ToString())
            Next
            Return sbAddressMain

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function L40GetCustomerDetails(ByVal _dtCustomerDetails As DataTable) As String
        Dim sbCustomerdetails As New StringBuilder
        Try
            Dim strCustomer, strAddress, strPhone, strEmailID As String
            Dim CustomerName, Address, Phone, EmailID As String

            Dim drCustomer As DataRow = _dtCustomerDetails.Rows(0)
            strCustomer = IIf(Not drCustomer("CustomerName") Is DBNull.Value, drCustomer("CustomerName"), String.Empty)
            strPhone = IIf(Not drCustomer("MobileNo") Is DBNull.Value, drCustomer("MobileNo"), String.Empty)
            strEmailID = IIf(Not drCustomer("EmailID") Is DBNull.Value, drCustomer("EmailID"), String.Empty)

            CustomerName = String.Format(getValueByKey("CLSPS4003").PadRight(10), strCustomer.Trim)  'Customer :
            sbCustomerdetails.Append(SplitString(CustomerName, 39).ToString().TrimEnd() + vbCrLf)

            strAddress = (L40GetCustomerAddress(_dtCustomerDetails, sbCustomerdetails, 1))
            Address = String.Format(getValueByKey("CLSPS4006").PadRight(10), strAddress.Trim)  'Address :
            sbCustomerdetails.Append(SplitString(Address, 39, 10).ToString().TrimEnd() + vbCrLf)

            Phone = String.Format(getValueByKey("CLSPS4004").PadRight(10), strPhone.Trim)  'Phone    :
            sbCustomerdetails.Append(SplitString(Phone, 39).ToString().TrimEnd() + vbCrLf)

            EmailID = String.Format(getValueByKey("CLSPS4005").PadRight(10), strEmailID.Trim)  'E-mail   :
            sbCustomerdetails.Append(SplitString(EmailID, 39).ToString().TrimEnd())

            Return sbCustomerdetails.ToString
        Catch ex As Exception
            Return sbCustomerdetails.ToString
        End Try

    End Function

    Public Function L40GetCustomerDeliveryDetails(ByVal _dtCustomerDetails As DataTable) As String
        Try
            Dim sbCustomerDeliveryDetails As New StringBuilder
            'Dim strDeliveryAddress As String = "Delivery adress: "
            Dim strDeliveryAddress As String = getValueByKey("MDCMFN006") & " "
            strDeliveryAddress = SplitString(strDeliveryAddress).ToString()
            Dim sbFinalDeliveryAddress As New StringBuilder
            L40GetCustomerAddress(_dtCustomerDetails, sbCustomerDeliveryDetails, 2)

            If sbCustomerDeliveryDetails.Length > 0 Then
                sbFinalDeliveryAddress.Append(strDeliveryAddress + vbCrLf)
                sbFinalDeliveryAddress.Append(sbCustomerDeliveryDetails.ToString())
            End If
            Return sbFinalDeliveryAddress.ToString()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function L40OrA4GetSiteDetails(ByVal siteCode As String, ByVal siteHeaderType As String) As String
        Try
            Dim daSite As New SqlDataAdapter("Select * from MstSite Where SiteCode='" & siteCode & "'", SpectrumCon)
            Dim dtSite, dtCity As New DataTable
            daSite.Fill(dtSite)

            Dim drSite As DataRow
            Dim siteName, site, address, TelNo As String
            Dim siteInfo As New StringBuilder

            If (Not dtSite Is Nothing AndAlso dtSite.Rows.Count > 0) Then
                drSite = dtSite.Rows(0)

                address = getValueByKey("CLSCMP007") & " " & drSite("SiteAddressLn1").ToString()
                If (drSite("SiteAddressLn2") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("SiteAddressLn2").ToString)) Then
                    address = address & ", " & drSite("SiteAddressLn2").ToString()
                End If
                If (drSite("SiteAddressLn3") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("SiteAddressLn3").ToString)) Then
                    address = address & ", " & drSite("SiteAddressLn3").ToString()
                End If
                If (drSite("CityCode") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("CityCode").ToString)) Then

                    daSite.SelectCommand.CommandText = "Select * from MstAreaCode Where AreaCode='" + drSite("CityCode") + "';"
                    dtCity = New DataTable
                    daSite.Fill(dtCity)

                    If (Not (dtCity Is Nothing) AndAlso dtCity.Rows.Count > 0) Then
                        address = address & ", " & dtCity.Rows(0)("Description").ToString()
                    End If
                End If
                If (drSite("SitePincode") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("SitePincode").ToString)) Then
                    address = address & ", " & drSite("SitePincode").ToString()
                End If

                TelNo = String.Empty
                If (drSite("SiteTelephone1") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("SiteTelephone1").ToString)) Then
                    TelNo = drSite("SiteTelephone1").ToString()
                End If
                If (drSite("SiteTelephone2") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(drSite("SiteTelephone2").ToString)) Then
                    TelNo = IIf(String.IsNullOrEmpty(TelNo), drSite("SiteTelephone2").ToString(), ", " & drSite("SiteTelephone2").ToString())
                End If

                If (siteHeaderType = "L40") Then
                    'SiteName = getValueByKey("CLSCMP004") & " " & drSite("SiteOfficialName").ToString()
                    siteName = SplitString(drSite("SiteOfficialName").ToString()).ToString()
                    site = getValueByKey("CLSCMP005") & " " & siteCode

                    siteInfo.Append(siteName & vbCrLf & site & vbCrLf)

                    address = SplitString(address, 39, 5).ToString()
                    siteInfo.Append(address.Trim() & vbCrLf)

                    siteInfo.Append(getValueByKey("CLSCMP006") & " " & TelNo & vbCrLf)

                ElseIf (siteHeaderType = "A4") Then
                    siteName = SplitString(drSite("SiteOfficialName").ToString(), 80).ToString()
                    site = getValueByKey("CLSCMP005") & " " & siteCode

                    siteInfo.Append(siteName & vbCrLf & site & vbCrLf)

                    address = SplitString(address, 80, 5).ToString()
                    siteInfo.Append(address & vbCrLf)

                    siteInfo.Append(getValueByKey("CLSCMP006") & " " & TelNo & vbCrLf)
                End If

                Return siteInfo.ToString()
            End If

            Return String.Empty
        Catch ex As Exception
            Return String.Empty
        End Try

    End Function

    ''' <summary>
    ''' Only need to pass string, to check its lenght and split into parts
    ''' </summary>
    ''' <param name="strCheckString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function SplitString(ByVal strCheckString As String) As StringBuilder
        Try
            Dim iLineLength As Integer = 40
            Dim sbCheckString As New StringBuilder
            Dim strResultString As String = strCheckString
            If Not (strCheckString.Length > iLineLength) Then
                sbCheckString.Append(strResultString)
            Else
                If strResultString.Length > iLineLength Then
                    Dim istartIndex As Integer = 0
                    While (Not strResultString.Length = 0)
                        Dim iLenght As Integer = strResultString.Length
                        Dim strCheckStringLine As String
                        If iLenght < istartIndex Then
                            strCheckStringLine = strResultString.Substring(istartIndex, iLineLength)
                        Else
                            strCheckStringLine = strResultString.Substring(istartIndex, iLenght - istartIndex)
                        End If
                        If Not strCheckStringLine.Length > iLineLength Then
                            sbCheckString.Append(strCheckStringLine + vbCrLf)
                            strResultString = String.Empty
                        Else
                            sbCheckString.Append(strCheckStringLine.Substring(0, iLineLength - 1) + vbCrLf)
                            istartIndex = istartIndex + iLineLength - 1
                        End If
                    End While
                End If
            End If
            Return sbCheckString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SplitString(ByVal strCheckString As String, ByVal stringLength As Int32, Optional ByVal blankSpace As Int32 = 0) As StringBuilder
        Try
            Dim outputValue As New StringBuilder
            'stringLength = 39 - 23
            Dim currentLength As Integer = stringLength
            Dim IsAddedBlankSpace As Boolean
            Dim currentString As String = strCheckString
            Dim strLineValueOld As String = String.Empty, strLineValueNew As String = String.Empty

            If (strCheckString.Trim().Length < stringLength) Then
                outputValue.Append(strCheckString + vbCrLf)
            Else
                While (Not currentString.Trim().Length = 0)

                    For Each strValue As String In currentString.Split(Space(1))
                        strLineValueNew += strValue + Space(1)

                        If (strLineValueNew.Length <= currentLength) Then
                            strLineValueOld = strLineValueNew
                        Else
                            Exit For
                        End If
                    Next

                    If (Not String.IsNullOrEmpty(strLineValueOld.ToString().Trim())) Then
                        outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.TrimEnd() + vbCrLf, strLineValueOld.Trim + vbCrLf + vbCrLf))
                        currentString = currentString.Replace(strLineValueOld.TrimEnd(), String.Empty)
                    Else
                        If (strLineValueNew.Length > currentLength) Then
                            Dim newString1 As String = String.Empty
                            Dim newString2 As String = String.Empty

                            For Each charValue As Char In strLineValueNew.ToArray()
                                If (newString1.Length < currentLength) Then
                                    newString1 += charValue
                                Else
                                    newString2 += newString1 + vbCrLf
                                    newString1 = String.Empty
                                End If
                            Next

                            strLineValueOld = newString2 + newString1

                            outputValue.Append(strLineValueOld + Space(1) + vbCrLf)
                            currentString = currentString.Replace(strLineValueNew.TrimEnd(), String.Empty)
                        End If
                    End If

                    strLineValueNew = String.Empty
                    strLineValueOld = String.Empty

                    If (Not IsAddedBlankSpace AndAlso blankSpace > 0) Then
                        IsAddedBlankSpace = True
                        currentLength = stringLength - blankSpace
                    End If

                End While

            End If

            ' Dim output As String = outputValue.ToString().Substring(0, outputValue.ToString().Length - 1).Replace(vbCr, vbCrLf)
            Dim output As String = outputValue.ToString()

            outputValue = New StringBuilder
            outputValue.Append(output)

            Return outputValue

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function SplitStringCenterAlign(ByVal strCheckString As String, ByVal stringLength As Int32, Optional ByVal blankSpace As Int32 = 0) As StringBuilder
        Try
            Dim outputValue As New StringBuilder
            'stringLength = 39 - 23
            Dim currentLength As Integer = stringLength
            Dim IsAddedBlankSpace As Boolean
            Dim currentString As String = strCheckString
            Dim strLineValueOld As String = String.Empty, strLineValueNew As String = String.Empty

            If (strCheckString.Trim().Length < stringLength) Then
                outputValue.Append(strCheckString + vbCrLf)
            Else
                While (Not currentString.Trim().Length = 0)

                    For Each strValue As String In currentString.Split(Space(1))
                        strLineValueNew += strValue + Space(1)

                        If (strLineValueNew.Length <= currentLength) Then
                            strLineValueOld = strLineValueNew

                        Else
                            Exit For
                        End If
                    Next
                    'strLineValueOld = strLineValueOld.PadLeft(stringLength / 2)
                    If (Not String.IsNullOrEmpty(strLineValueOld.ToString().Trim())) Then
                        'outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.TrimEnd() + vbCrLf, strLineValueOld.Trim + vbCrLf))
                        outputValue.Append(strLineValueOld.PadLeft(currentLength / 2 + (strLineValueOld.Length / 2)) + vbCrLf)
                        currentString = currentString.Replace(strLineValueOld.TrimEnd(), String.Empty)
                    Else
                        If (strLineValueNew.Length > currentLength) Then
                            Dim newString1 As String = String.Empty
                            Dim newString2 As String = String.Empty

                            For Each charValue As Char In strLineValueNew.ToArray()
                                If (newString1.Length < currentLength) Then
                                    newString1 += charValue
                                Else
                                    newString2 += newString1 + vbCrLf
                                    newString1 = String.Empty
                                End If
                            Next

                            strLineValueOld = newString2 + newString1

                            outputValue.Append(strLineValueOld + Space(1) + vbCrLf)
                            currentString = currentString.Replace(strLineValueNew.TrimEnd(), String.Empty)
                        End If
                    End If

                    ' strLineValueOld = strLineValueOld.PadLeft(40)
                    ' currentString = currentString.PadLeft(40)
                    strLineValueNew = String.Empty
                    strLineValueOld = String.Empty
                    If (Not IsAddedBlankSpace AndAlso blankSpace > 0) Then
                        IsAddedBlankSpace = True
                        currentLength = stringLength - blankSpace
                    End If
                End While

            End If
            'Dim output As String = (outputValue.ToString().Substring(0, outputValue.ToString().Length - 1).ToString()).Replace(vbCr, vbCrLf)
            Dim output As String = outputValue.ToString()
            outputValue = New StringBuilder
            outputValue.Append(output)

            Return outputValue

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        End Try
    End Function


    Public Function GetCurrentDate() As DateTime
        Dim currentDate As Date
        Dim drCurrentDate As SqlDataReader
        Try
            SpectrumCon = SpectrumBL.DataBaseConnection.SpectrumCon
            OpenConnection()
            Dim cmdCurrentDate As New SqlCommand("select getdate()", SpectrumCon)
            drCurrentDate = cmdCurrentDate.ExecuteReader()
            If (drCurrentDate.Read()) Then
                If Not (drCurrentDate.IsDBNull(0)) Then
                    currentDate = drCurrentDate.GetDateTime(0)
                End If
            End If
            Return currentDate
        Catch ex As Exception
            Return Nothing
        Finally
            drCurrentDate.Close()
            CloseConnection()
        End Try

    End Function

    Public Function MyRound(ByVal Amount As Double, ByVal RoundedAt As Int32, Optional ByVal IsRoundOffRequired As Boolean = True) As Double
        Try
            If (IsRoundOffRequired) Then
                Dim Amt As Double = Amount
                Amount = Math.Floor(Amount)
                Amt = Amt - Amount
                Math.Round(Amt, 2)
                Amt = MyOwnRound(RoundedAt, Amt * 100)
                Amount = Amount + (Amt * 0.01)
                Return Amount
            Else
                Return Amount
            End If


        Catch ex As Exception
            Return Amount
        End Try
    End Function

    Private Function MyOwnRound(ByVal NumberToRound As Integer, _
                              ByVal ValueToRound As Integer) As Integer
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

    Public Function L40CashierDetails(ByVal dtCurrentDate As Date, Optional ByVal strInvoiceNumber As String = "", Optional ByVal strCashierId As String = "") As String
        Try
            Dim sbCashierDetails As New StringBuilder()

            Dim strCashMemo, strDate, strCashierName As String

            If (Not String.IsNullOrEmpty(strInvoiceNumber)) Then
                strCashMemo = String.Format(getValueByKey("CLSPS4008"), strInvoiceNumber)  'CASH MEMO:
                sbCashierDetails.Append(SplitString(strCashMemo, strLineL40.Length).ToString().TrimEnd() + vbCrLf)
            End If

            strDate = "Payment Date"  'DATE:getValueByKey("CLSPS4009")
            strDate = strDate & "  : " & dtCurrentDate.ToString
            sbCashierDetails.Append(SplitString(strDate, strLineL40.Length).ToString().TrimEnd() + vbCrLf)
            strCashierName = String.Format(getValueByKey("MDCMFN008") + strCashierId)  'Cashier Name
            sbCashierDetails.Append(SplitString(strCashierName).ToString().TrimEnd())

            'strVendorName = getValueByKey("MDCMFN009").PadRight(15) + String.Empty  'Vendor Name
            'sbCashierDetails.Append(SplitString(strVendorName, 39).ToString() + vbCrLf)

            Return sbCashierDetails.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function A4CashierDetails(ByVal dtCurrentDate As Date, Optional ByVal strInvoiceNumber As String = "", Optional ByVal strCashierId As String = "", Optional ByVal doctype As String = "", Optional ByVal strEvntDt As String = "", Optional ByVal strEvntName As String = "") As String
        Try
            Dim sbCashierDetails As New StringBuilder()
            'sbCashierDetails.Append("Creation Date:")
            sbCashierDetails.Append(getValueByKey("MDCMFN010"))

            If doctype = "BirthList" Then
                sbCashierDetails.Append(dtCurrentDate.ToString().PadRight(strLineL40.Length))
                'sbCashierDetails.Append("Event :")
                sbCashierDetails.Append(getValueByKey("MDCMFN011"))
                sbCashierDetails.Append(strEvntName + vbCrLf)
            Else
                sbCashierDetails.Append(dtCurrentDate.ToString() + vbCrLf)
            End If

            'Rohit
            'sbCashierDetails.Append("Cashier Name :" + strCashierId.PadRight(40))
            sbCashierDetails.Append(getValueByKey("MDCMFN008") + strCashierId.PadRight(strLineL40.Length))
            If doctype = "BirthList" Then
                'sbCashierDetails.Append("Event Date :")
                sbCashierDetails.Append(getValueByKey("MDCMFN012"))

                Dim eventdate As String = String.Empty
                If (strEvntDt.Contains(Space(1))) Then
                    eventdate = strEvntDt.Split(Space(1))(0)
                Else
                    eventdate = strEvntDt
                End If

                sbCashierDetails.Append(eventdate)
            End If

            Return sbCashierDetails.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function L40Messages() As String
        Try
            Dim sbMessages As New StringBuilder
            'sbMessages.Append("TOP OF RECEIPT MESSAGE ")
            sbMessages.Append(getValueByKey("MDCMFN014") & " ")

            Return sbMessages.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function PrintFormatCurrency(ByVal objAmount As Object, ByVal objCurrency As Object, Optional ByVal iNumberDigitAfterPoint As Integer = 2) As String
        Try
            objAmount = FormatNumber(objAmount, DecimalDigits, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)

            Return CStr(objAmount).Trim() & " " & CStr(objCurrency).Trim()

        Catch ex As Exception

        End Try
    End Function

#Region "Private Method's & Function's"
    ''' <summary>
    ''' Printing the Doucment
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
      ByVal e As PrintPageEventArgs) Handles printDocument1.PrintPage

        Try

           
            Dim MyFont As New System.Drawing.Font(BillFont, 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim charactersOnPage As Integer = 0
            Dim linesPerPage As Integer = 0


            'Added by Rakesh Gautam 31 Oct 2009- using for print barcode in Gift Voucher and Credit Note 
            If Not VarBarcode Is Nothing AndAlso VarBarcode.Text <> String.Empty AndAlso IsPrintVoucher = True Then
                ' e.Graphics.DrawImage(VarBarcode.Image, 50, 250)
                If (BarcodeMarginTop > 0) Then
                    e.Graphics.DrawImage(VarBarcode.Image, 0, BarcodeMarginTop, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                Else
                    e.Graphics.DrawImage(VarBarcode.Image, 0, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Height, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                End If
            ElseIf Not VarBarcode Is Nothing AndAlso VarBarcode.Text <> String.Empty AndAlso IsEvassChanges = True Then
                ' e.Graphics.DrawImage(VarBarcode.Image, 2, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Height + 50, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                '  IsEvassChanges = False
                'e.Graphics.DrawImage(VarBarcode.Image, 2, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Height + 50, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                'IsEvassChanges = False
                'vipin code added to generate barcode For Spectrum as Mettler
                If SpectrumAsMettlerBarcode = True Then
                    e.Graphics.DrawImage(VarBarcode.Image, 0, 0, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                Else
                    e.Graphics.DrawImage(VarBarcode.Image, 2, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Height + 50, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                End If
                IsEvassChanges = False
                SpectrumAsMettlerBarcode = False
            ElseIf Not VarBarcode Is Nothing AndAlso VarBarcode.Text <> String.Empty AndAlso IsBookingPrint = False Then
                e.Graphics.DrawImage(VarBarcode.Image, 0, 0, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
            ElseIf Not VarBarcode Is Nothing AndAlso VarBarcode.Text <> String.Empty AndAlso IsBookingPrint = True Then
                '' barcode scan issue changes made by ketan
                'e.Graphics.DrawImage(VarBarcode.Image, 10, 145, e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage).Width, VarBarcode.Image.Height)
                e.Graphics.DrawImage(VarBarcode.Image, 45, 145, VarBarcode.Image.Width - 15, VarBarcode.Image.Height)
            End If

            ' Sets the value of charactersOnPage to the number of characters 
            ' of stringToPrint that will fit within the bounds of the page.

            e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, _
             StringFormat.GenericTypographic, charactersOnPage, linesPerPage)
            ' Draws the string within the bounds of the page.



            ' e.Graphics.DrawString(stringToPrint, MyFont, Brushes.Black, _
            '     e.MarginBounds, StringFormat.GenericTypographic)

            GenerateCustomPrint(linesPerPage, e)
            'GeneratePrint(linesPerPage, e)



            'If PrintFormatNo = 3 Then

            '----- Get Split Data 
            Dim SplitedLines() = stringToPrint.Split(vbCrLf)
            Dim TempStringToPring As String = String.Empty
            For index = (linesPerPage) To SplitedLines.Count - 1
                TempStringToPring &= SplitedLines(index).Replace(vbLf, vbCrLf)
            Next

            If String.IsNullOrEmpty(TempStringToPring.Trim) Then
                stringToPrint = String.Empty
            Else
                stringToPrint = TempStringToPring
            End If
            'Else
            '    'Remove the portion of the string that has been printed.
            '    If stringToPrint.Length > 0 Then
            '        stringToPrint = stringToPrint.Substring(charactersOnPage)
            '    End If
            ' End If
            ' Check to see if more pages are to be printed.
            e.HasMorePages = stringToPrint.Length > 0

            ' If there are no more pages, reset the string to be printed.
            If Not e.HasMorePages Then
                stringToPrint = documentContents
            End If

            ' If there are no more pages, reset the string to be printed.
            If Not e.HasMorePages Then
                stringToPrint = documentContents
                stringToPrint = stringPrintForPreview
            End If

        Catch ex As Exception

        End Try


    End Sub

    'Private Function GetFont(ByVal HtmlTag As String) As System.Drawing.Font
    '    Try
    '        Dim MyFont As New System.Drawing.Font("BillFont", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '        Dim MyFontItalic As New System.Drawing.Font("BillFont", 10.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '        Dim MyFontBold As New System.Drawing.Font("BillFont", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '        Dim MyColorFont As New System.Drawing.Font("BillFont", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '        Dim dictionary As New Dictionary(Of String, System.Drawing.Font)
    '        dictionary.Add("<B>", MyFontBold)
    '        dictionary.Add("<I>", MyFontItalic)
    '        dictionary.Add("<C color", MyFontItalic)
    '        If dictionary.ContainsKey(HtmlTag) Then
    '            If HtmlTag = "<B>" Then
    '                Return MyFontBold
    '            ElseIf HtmlTag = "<I>" Then
    '                Return MyFontItalic
    '            Else
    '                Return MyFont
    '            End If
    '        ElseIf InStr(HtmlTag, "<C color") > 0 Then
    '            Return MyColorFont
    '        Else
    '            Return MyFont
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function
    'Private Sub GenerateCustomPrint(ByVal linesPerPage As Integer, ByVal e As PrintPageEventArgs)
    '    Try
    '        Dim MyFont As New System.Drawing.Font("BillFont", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    '        Dim curLineCount As Integer = 1
    '        Dim yPos As Single = 0
    '        Dim topMargin As Single = e.MarginBounds.Top
    '        Dim leftMargin As Single = e.MarginBounds.Left
    '        Dim strReader As New StringReader(stringToPrint)
    '        Dim currentPrintLine As String = ""
    '        Dim startFlag = True
    '        While curLineCount <= linesPerPage
    '            leftMargin = 0.0
    '            currentPrintLine = ""
    '            currentPrintLine = strReader.ReadLine()
    '            If currentPrintLine Is Nothing Then
    '                Return
    '            End If
    '            If currentPrintLine <> "" Then


    '                yPos = topMargin + curLineCount * MyFont.GetHeight(e.Graphics)
    '                Dim printStyle As System.Drawing.Font
    '                For printIndex As Integer = 0 To currentPrintLine.Length - 1
    '                    Dim sttemp As String = currentPrintLine(printIndex)
    '                    Dim HtmlTag As String = ""
    '                    If startFlag = True Then

    '                        If currentPrintLine(printIndex) = "<" Then
    '                            HtmlTag &= currentPrintLine(printIndex)
    '                            printIndex = printIndex + 1
    '                            For skipIndex As Integer = printIndex To currentPrintLine.Length
    '                                If currentPrintLine(skipIndex) = ">" Then
    '                                    HtmlTag &= currentPrintLine(skipIndex)
    '                                    printStyle = GetFont(HtmlTag)
    '                                    startFlag = False
    '                                    Exit For
    '                                Else
    '                                    HtmlTag &= currentPrintLine(skipIndex)
    '                                End If
    '                                printIndex = printIndex + 1
    '                            Next
    '                        Else
    '                            e.Graphics.DrawString(currentPrintLine(printIndex), MyFont, Brushes.Black, leftMargin, yPos, StringFormat.GenericTypographic)
    '                            leftMargin = leftMargin + 7
    '                        End If
    '                    ElseIf startFlag = False Then
    '                        If currentPrintLine(printIndex) = "<" Then
    '                            HtmlTag &= currentPrintLine(printIndex)
    '                            printIndex = printIndex + 1
    '                            For skipIndex As Integer = printIndex To currentPrintLine.Length
    '                                If currentPrintLine(skipIndex) = ">" Then
    '                                    HtmlTag &= currentPrintLine(skipIndex)
    '                                    startFlag = True
    '                                    Exit For
    '                                Else
    '                                    HtmlTag &= currentPrintLine(skipIndex)
    '                                End If
    '                                printIndex = printIndex + 1
    '                            Next
    '                        Else
    '                            e.Graphics.DrawString(currentPrintLine(printIndex), printStyle, Brushes.Black, leftMargin, yPos, StringFormat.GenericTypographic)
    '                            leftMargin = leftMargin + 7
    '                        End If
    '                    End If

    '                Next

    '                curLineCount += 1
    '            End If
    '        End While
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Function GetFont(ByVal HtmlTag As String) As System.Drawing.Font
        Try
            Dim MyFont As New System.Drawing.Font(BillFont, 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontItalic As New System.Drawing.Font(BillFont, 10.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontBold As New System.Drawing.Font(BillFont, 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontBold1 As New System.Drawing.Font(BillFont, 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'Dim MyFontBold As New System.Drawing.Font(BillFont, 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyColorFont As New System.Drawing.Font(BillFont, 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontLarge As New System.Drawing.Font(BillFont, 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontItemdescBold As New System.Drawing.Font(BillFont, 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '''''''Font For New Bill Format-"Calibri"----------
            Dim MyFontPCBillDesc As New System.Drawing.Font(BillFont, 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '''''''Font For New Bill Format-----------
            'Bill Paid
            Dim MyFontPCKotBillPaid As New System.Drawing.Font(BillFont, 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'Footer
            Dim MyFontPCKotBillDtl As New System.Drawing.Font(BillFont, 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ''header
            Dim MyFontPCKotHeader As New System.Drawing.Font(BillFont, 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ''Header Address
            Dim MyFontPCKotHeaderAddress As New System.Drawing.Font(BillFont, 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ''KOT article
            Dim MyFontPCKotArticle As New System.Drawing.Font(BillFont, 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ''ArticleDetails
            Dim MyFontPCKotArticleDetails As New System.Drawing.Font(BillFont, 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '''''''''''''''''''''''
            '''MyFont Bold With Same Size
            Dim MyFontPCBold As New System.Drawing.Font(BillFont, 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '''Column Header for Bill Format
            Dim MyFontPCBillHeader As New System.Drawing.Font(BillFont, 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ''''Footer Font for Bill Format
            Dim MyFontPCBillFooter As New System.Drawing.Font(BillFont, 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyCustomFontHeader As New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'added by khusrao adil on 29-08-2017 for natural
            'KOT Token No bold
            Dim MyFontKOTTokenNoBold As New System.Drawing.Font(BillFont, 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim dictionary As New Dictionary(Of String, System.Drawing.Font)
            dictionary.Add("<B>", MyFontBold)
            dictionary.Add("<B1>", MyFontBold1)
            dictionary.Add("<I>", MyFontItalic)
            dictionary.Add("<C color", MyFontItalic)
            dictionary.Add("<L>", MyFontLarge)
            dictionary.Add("<FontPCKotBillPaid>", MyFontPCKotBillPaid)
            dictionary.Add("<FontPCKotBillDtl>", MyFontPCKotBillDtl)
            dictionary.Add("<FontPCKotHeader>", MyFontPCKotHeader)
            dictionary.Add("<FontPCKotHeaderAddress>", MyFontPCKotHeaderAddress)
            dictionary.Add("<FontPCKOTArticle>", MyFontPCKotArticle)
            dictionary.Add("<FontPCKotArticleDetails>", MyFontPCKotArticleDetails)
            dictionary.Add("<FontPCBold>", MyFontPCBold)
            dictionary.Add("<FontPCBillHeader>", MyFontPCBillHeader)
            dictionary.Add("<FontPCBillFooter>", MyFontPCBillFooter)
            dictionary.Add("<FontPCItemDesc>", MyFontItemdescBold)
            dictionary.Add("<FontPCBillItemDesc>", MyFontPCBillDesc)
            dictionary.Add("<CustomFontHeader>", MyCustomFontHeader)
            dictionary.Add("<BT>", MyFontKOTTokenNoBold)
            If dictionary.ContainsKey(HtmlTag) Then
                If HtmlTag = "<B>" Then
                    Return MyFontBold
                ElseIf HtmlTag = "<I>" Then
                    Return MyFontItalic
                ElseIf HtmlTag = "<L>" Then
                    Return MyFontLarge
                ElseIf HtmlTag = "<FontPCKotBillPaid>" Then
                    Return MyFontPCKotBillPaid
                ElseIf HtmlTag = "<FontPCKotBillDtl>" Then
                    Return MyFontPCKotBillDtl
                ElseIf HtmlTag = "<FontPCKotHeader>" Then
                    Return MyFontPCKotHeader
                ElseIf HtmlTag = "<FontPCKotHeaderAddress>" Then
                    Return MyFontPCKotHeaderAddress
                ElseIf HtmlTag = "<FontPCKOTArticle>" Then
                    Return MyFontPCKotArticle
                ElseIf HtmlTag = "<FontPCKotArticleDetails>" Then
                    Return MyFontPCKotArticleDetails
                ElseIf HtmlTag = "<FontPCBold>" Then
                    Return MyFontPCBold
                ElseIf HtmlTag = "<FontPCBillHeader>" Then
                    Return MyFontPCBillHeader
                ElseIf HtmlTag = "<FontPCBillFooter>" Then
                    Return MyFontPCBillFooter
                ElseIf HtmlTag = "<FontPCItemDesc>" Then
                    Return MyFontItemdescBold
                ElseIf HtmlTag = "<FontPCBillItemDesc>" Then
                    Return MyFontPCBillDesc
                ElseIf HtmlTag = "<CustomFontHeader>" Then
                    Return MyCustomFontHeader
                ElseIf HtmlTag = "<BT>" Then
                    Return MyFontKOTTokenNoBold
                ElseIf HtmlTag = "<B1>" Then
                    Return MyFontBold1
                Else
                    Return MyFont
                End If
            ElseIf InStr(HtmlTag, "<C color") > 0 Then
                Return MyColorFont
            Else
                Return MyFont
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub GenerateCustomPrint(ByVal linesPerPage As Integer, ByVal e As PrintPageEventArgs)
        Try
            Dim MyFont As New System.Drawing.Font(BillFont, 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim MyFontLarge As New System.Drawing.Font(BillFont, 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Dim curLineCount As Integer = 1
            Dim yPos As Single = 0
            Dim topMargin As Single = e.MarginBounds.Top
            Dim leftMargin As Single = e.MarginBounds.Left
            '---------Added for Font Changes as Requested By Client
            If lastAppliedFont <> String.Empty Then
                stringToPrint = lastAppliedFont & stringToPrint
            End If
            Dim strReader As New StringReader(stringToPrint)
            Dim currentPrintLine As String = ""
            Dim startFlag = True
            While curLineCount <= linesPerPage
                leftMargin = 0.0
                currentPrintLine = ""
                currentPrintLine = strReader.ReadLine()
                If currentPrintLine Is Nothing Or String.IsNullOrEmpty(currentPrintLine) Then
                    currentPrintLine &= "" & vbCrLf
                End If
                If currentPrintLine <> "" Then
                    yPos = topMargin + curLineCount * MyFont.GetHeight(e.Graphics)
                    Dim printStyle As System.Drawing.Font
                    For printIndex As Integer = 0 To currentPrintLine.Length - 1
                        Dim sttemp As String = currentPrintLine(printIndex)
                        Dim HtmlTag As String = ""
                        If startFlag = True Then
                            If currentPrintLine(printIndex) = "<" Then
                                HtmlTag &= currentPrintLine(printIndex)
                                printIndex = printIndex + 1
                                For skipIndex As Integer = printIndex To currentPrintLine.Length
                                    If currentPrintLine(skipIndex) = ">" Then
                                        HtmlTag &= currentPrintLine(skipIndex)
                                        printStyle = GetFont(HtmlTag)
                                        startFlag = False
                                        '---------Added for Font Changes as Requested By Client
                                        lastAppliedFont = HtmlTag
                                        If HtmlTag.Contains("/") Then
                                            lastAppliedFont = String.Empty
                                        End If
                                        '---------------
                                        Exit For
                                    Else
                                        HtmlTag &= currentPrintLine(skipIndex)
                                    End If
                                    printIndex = printIndex + 1
                                Next
                            Else
                                e.Graphics.DrawString(currentPrintLine(printIndex), MyFont, Brushes.Black, leftMargin, yPos, StringFormat.GenericTypographic)
                                If MyFont.Size = "20.0" Then
                                    leftMargin = leftMargin + 15
                                Else
                                    leftMargin = leftMargin + 7
                                End If
                            End If
                        ElseIf startFlag = False Then
                            If currentPrintLine(printIndex) = "<" Then
                                HtmlTag &= currentPrintLine(printIndex)
                                printIndex = printIndex + 1
                                For skipIndex As Integer = printIndex To currentPrintLine.Length
                                    If currentPrintLine(skipIndex) = ">" Then
                                        HtmlTag &= currentPrintLine(skipIndex)
                                        startFlag = True
                                        '---------Added for Font Changes as Requested By Client
                                        lastAppliedFont = HtmlTag
                                        If HtmlTag.Contains("/") Then
                                            lastAppliedFont = String.Empty
                                        End If
                                        '---------------
                                        Exit For
                                    Else
                                        HtmlTag &= currentPrintLine(skipIndex)
                                    End If
                                    printIndex = printIndex + 1
                                Next
                            Else
                                e.Graphics.DrawString(currentPrintLine(printIndex), printStyle, Brushes.Black, leftMargin, yPos, StringFormat.GenericTypographic)
                                If printStyle.Size > "8.0" AndAlso printStyle.Size <= "12.0" Then
                                    leftMargin = leftMargin + 10
                                ElseIf printStyle.Size > "12.0" AndAlso printStyle.Size <= "15.0" Then
                                    leftMargin = leftMargin + 11
                                ElseIf printStyle.Size > "15.0" AndAlso printStyle.Size <= "20.0" Then
                                    leftMargin = leftMargin + 15
                                Else
                                    leftMargin = leftMargin + 7
                                End If
                            End If
                        End If
                    Next
                Else
                    e.Graphics.DrawString("", Nothing, Brushes.Black, leftMargin, yPos, StringFormat.GenericTypographic)
                End If
                curLineCount += 1
            End While
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Public Function GetBarcode(ByVal BarcodeValue As String) As C1BarCode
        'constant value
        'CodeTypeEnum.Code39 = 0  --- 108
        'CodeTypeEnum.Code93 = 1  ---  109
        'CodeTypeEnum.Code128 = 2  -- 110
        'CodeTypeEnum.CodeI2of5 = 3 ---
        'CodeTypeEnum.Codabar = 4  -- 107
        'CodeTypeEnum.PostNet = 5  --
        'CodeTypeEnum.Ean13 = 6  --- 104
        'CodeTypeEnum.Ean8 = 7   --  103
        'CodeTypeEnum.UpcA = 8   --- 101
        'CodeTypeEnum.UpcE = 9   -- 102

        Dim BarCode As New C1BarCode()
        If PBarCodeType = "0" Or PBarCodeType = "108" Then
            BarCode.CodeType = CodeTypeEnum.Code39
        ElseIf PBarCodeType = "1" Or PBarCodeType = "109" Then
            BarCode.CodeType = CodeTypeEnum.Code93
        ElseIf PBarCodeType = "2" Or PBarCodeType = "110" Then
            BarCode.CodeType = CodeTypeEnum.Code128
            'ElseIf PBarCodeType = "3" Or PBarCodeType = "108" Then
            '    BarCode.CodeType = CodeTypeEnum.Codabar
        ElseIf PBarCodeType = "4" Or PBarCodeType = "107" Then
            BarCode.CodeType = CodeTypeEnum.Codabar
            'ElseIf PBarCodeType = "5" Or PBarCodeType = "108" Then
            '    BarCode.CodeType = CodeTypeEnum.Codabar
        ElseIf PBarCodeType = "6" Or PBarCodeType = "104" Then
            BarCode.CodeType = CodeTypeEnum.Ean13
        ElseIf PBarCodeType = "7" Or PBarCodeType = "103" Then
            BarCode.CodeType = CodeTypeEnum.Ean8
        ElseIf PBarCodeType = "8" Or PBarCodeType = "101" Then
            BarCode.CodeType = CodeTypeEnum.UpcA
        ElseIf PBarCodeType = "9" Or PBarCodeType = "102" Then
            BarCode.CodeType = CodeTypeEnum.UpcE
        Else
            BarCode.CodeType = CodeTypeEnum.Code128
        End If

0:      BarCode.Text = BarcodeValue
        Return BarCode

    End Function

    Public Function SetPrinterName(ByVal dtPrinter As DataTable, ByVal pDocType As String, ByVal pTransType As String) As String
        Dim strDocType As String = Nothing
        Try
            PrinterName = Nothing

            If pDocType = "SalesOrder" Then
                strDocType = "Sales Order"
                If pTransType = "SOStatus" AndAlso dtPrinter.Select("PrinterDocument='SOStatus'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='SOStatus'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='SOStatus'")(0)("PrinterType").ToString
                End If
                If pTransType = "SOInvc" AndAlso dtPrinter.Select("PrinterDocument='SOInvc'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='SOInvc'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='SOInvc'")(0)("PrinterType").ToString
                End If
                If pTransType = "SOOB" AndAlso dtPrinter.Select("PrinterDocument='SOOB'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='SOOB'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='SOOB'")(0)("PrinterType").ToString
                End If

                'Status, Invoice,
            ElseIf pDocType = "BirthList" Then
                strDocType = "Birth-List"
                If pTransType = "BLStatus" AndAlso dtPrinter.Select("PrinterDocument='BLStatus'").Length > 0 Then
                    '--Commented by rama on 25-Oct-2010 for wrong condtion matching 
                    'If pTransType = clsBirthList.PrintBLTransactionSet.BirthListStatus.ToString AndAlso dtPrinter.Select("PrinterDocument='BLStatus'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='BLStatus'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='BLStatus'")(0)("PrinterType").ToString
                End If
                If pTransType = "BLInvc" AndAlso dtPrinter.Select("PrinterDocument='BLInvc'").Length > 0 Then
                    '--Commented by rama on 25-Oct-2010 for wrong condtion matching 
                    'If pTransType = clsBirthList.PrintBLTransactionSet.SaleBirthListItem.ToString AndAlso dtPrinter.Select("PrinterDocument='BLInvc'").Length > 0 AndAlso Transation = "BLInvc" Then
                    PrinterName = dtPrinter.Select("PrinterDocument='BLInvc'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='BLInvc'")(0)("PrinterType").ToString
                End If
                If pTransType = "BLOB" AndAlso dtPrinter.Select("PrinterDocument='BLOB'").Length > 0 Then
                    '--Commented by rama on 25-Oct-2010 for wrong condtion matching 
                    'If pTransType = clsBirthList.PrintBLTransactionSet.SaleBirthListItem.ToString AndAlso dtPrinter.Select("PrinterDocument='BLOB'").Length > 0 AndAlso Transation = "BLOB" Then
                    PrinterName = dtPrinter.Select("PrinterDocument='BLOB'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='BLOB'")(0)("PrinterType").ToString
                End If

            ElseIf pDocType = "CashMemo" Then
                strDocType = "Cash Memo"
                If pTransType = "Billing" AndAlso dtPrinter.Select("PrinterDocument='CMInvc'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='CMInvc'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='CMInvc'")(0)("PrinterType").ToString
                End If
                If pTransType = "Hold" AndAlso dtPrinter.Select("PrinterDocument='HoldCM'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='HoldCM'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='HoldCM'")(0)("PrinterType").ToString
                End If
            ElseIf pDocType = "TillCloseFinReport" Then
                If dtPrinter.Select("PrinterDocument='TillCloseFinReport'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='TillCloseFinReport'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='TillCloseFinReport'")(0)("PrinterType").ToString
                End If

            ElseIf pDocType = "Voucher" Then
                If dtPrinter.Select("PrinterDocument='Voucher'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='Voucher'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='Voucher'")(0)("PrinterType").ToString
                End If
            ElseIf pDocType = "PettyCash" Then
                If pTransType = "PCV" AndAlso dtPrinter.Select("PrinterDocument='PettyCash'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='PettyCash'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='PettyCash'")(0)("PrinterType").ToString
                End If
            ElseIf pDocType = "CreditSettleMentNote" Then
                If pTransType = "CreditSettlement" AndAlso dtPrinter.Select("PrinterDocument='CreditSettleMentNote'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='CreditSettlementNote'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='CreditSettlementNote'")(0)("PrinterType").ToString
                End If
            ElseIf pDocType = "SalesOrderBooking" Then
                If pTransType = "" AndAlso dtPrinter.Select("PrinterDocument='SalesOrderBooking'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='SalesOrderBooking'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='SalesOrderBooking'")(0)("PrinterType").ToString
                End If
            ElseIf pDocType = "LblPrint" Then
                If pTransType = "LblPrint" AndAlso dtPrinter.Select("PrinterDocument='LblPrint'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='LblPrint'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='LblPrint'")(0)("PrinterType").ToString
                End If
            End If


            If PrinterName = Nothing Then
                If dtPrinter.Select("PrinterDocument='ALLDocs'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='ALLDocs'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='ALLDocs'")(0)("PrinterType").ToString
                End If
                If dtPrinter.Select("PrinterDocument='X-Read'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='X-Read'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='X-Read'")(0)("PrinterType").ToString
                End If
                If dtPrinter.Select("PrinterDocument='XReadCategorywise'").Length > 0 Then
                    PrinterName = dtPrinter.Select("PrinterDocument='XReadCategorywise'")(0)("PrinterName").ToString
                    PrinterType = dtPrinter.Select("PrinterDocument='XReadCategorywise'")(0)("PrinterType").ToString
                End If
            End If

            Return PrinterName
        Catch ex As Exception
            MessageBox.Show(String.Format(getValueByKey("CLMCF09"), strDocType), getValueByKey("CLAE05"))
            'MsgBox.Show("Printer not assigned for document type {0}", strDocType, "Error")
        End Try
    End Function

    Public Function ValidateDataRow(ByVal objCheck As Object, ByRef objReturnvalue As Object) As Boolean
        Try
            If Not objCheck Is Nothing AndAlso Not objCheck Is DBNull.Value Then
                objReturnvalue = objCheck
            Else

                If (TypeOf (objReturnvalue) Is Integer) Then
                    objReturnvalue = 0
                ElseIf (TypeOf (objReturnvalue) Is Decimal) Then
                    objReturnvalue = 0.0
                ElseIf (objReturnvalue Is Nothing) Then
                    objReturnvalue = New Object()
                    objReturnvalue = ""
                Else
                    objReturnvalue = ""
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetTaxName(ByVal strTaxCode As String, ByRef strTaxName As String) As Boolean
        Dim drTaxName As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCommand As New SqlCommand("select TaxName from dbo.MstTax where TaxCode='" & strTaxCode & "'")
            sqlCommand.Connection = SpectrumCon
            drTaxName = sqlCommand.ExecuteReader()
            If Not drTaxName Is Nothing Then
                If drTaxName.Read() Then
                    strTaxName = drTaxName.GetString(0)
                Else
                    strTaxName = "Unknown"
                End If
            End If
            Return True
        Catch ex As Exception
            strTaxName = "Unknown"
            Return False
        Finally
            If Not drTaxName Is Nothing AndAlso Not drTaxName.IsClosed() Then
                drTaxName.Close()
            End If
            CloseConnection()
        End Try
    End Function

    ''' <summary>
    ''' Aded this function to support document to be printed on a specific format
    ''' </summary>
    ''' <param name="pTerminal"></param>
    ''' <param name="pDoc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPrintFormat(ByVal pTerminal As String, ByVal pDoc As String) As DataTable
        Try
            Dim daPrinter As SqlDataAdapter
            Dim qryPrinter As String = "Select PrintFormat " + _
                                       "from PrinterTillMap Where TerminalID='" & pTerminal & "' and PrinterDocument= '" & pDoc & "'"
            daPrinter = New SqlClient.SqlDataAdapter(qryPrinter, SpectrumCon)
            Dim dtPrinterTp As New DataTable
            daPrinter.Fill(dtPrinterTp)
            If dtPrinterTp.Rows.Count = 0 Then
                qryPrinter = "Select PrintFormat " + _
                                       "from PrinterTillMap Where TerminalID='" & pTerminal & "' and PrinterDocument= 'ALLDocs'"
                daPrinter = Nothing
                daPrinter = New SqlClient.SqlDataAdapter(qryPrinter, SpectrumCon)
                daPrinter.Fill(dtPrinterTp)
                If dtPrinterTp.Rows.Count > 0 Then
                    If IsDBNull(dtPrinterTp.Rows(0)("PrintFormat")) Then
                        dtPrinterTp.Rows(0)(0) = "A4"
                    End If
                End If
            End If
            If dtPrinterTp.Rows.Count = 0 Then
                If Not dtPrinterTp.Columns.Contains("PrintFormat") Then
                    dtPrinterTp.Columns.Add("PrintFormat")
                End If

                Dim dr As DataRow = dtPrinterTp.NewRow
                dr("PrintFormat") = "A4"
                dtPrinterTp.Rows.Add(dr)
            End If
            Return dtPrinterTp
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '' Functions Added By Mahesh for Encryption Secryption 
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

    'added on 12 may - ashma - for Innoviti
    Public Function PaymentToCallEDC(ByVal invoiceNumber As String, ByVal amount As String) As Dictionary(Of String, String)
        Try
            Dim resonseInnoviti As New Dictionary(Of String, String)
            Dim baseAmount = FormatNumber(((amount) * 100), 0).Replace(",", "")
            Dim transactionTime = Date.Now.ToString("hhmmssddMM")
            Dim posReferenceNumber = "000000" 'as a hardcode number

            Dim responseFromInnov As New Dictionary(Of String, String)
            If True Then
                responseFromInnov = modCommanFunction.PaymentThroughEDC(invoiceNumber, baseAmount, posReferenceNumber, transactionTime)
                InnovitiResponseError = ""
                Dim ErrorMgs As New System.Text.StringBuilder
                For Each kvp As KeyValuePair(Of String, String) In responseFromInnov
                    If kvp.Key = "StatusMessage" Then
                        ErrorMgs.Append("StatusMessage: " & kvp.Value & vbCrLf)
                    End If
                Next

                For Each kvp As KeyValuePair(Of String, String) In responseFromInnov
                    If kvp.Key = "StatusCode" Then
                        If kvp.Value.ToUpper = "00" Then
                        Else
                            ErrorMgs.Append("StatusCode: " & kvp.Value & vbCrLf)
                            InnovitiResponseError = ErrorMgs.ToString()
                            Exit Function
                        End If
                    End If
                Next
            End If

            resonseInnoviti = responseFromInnov

            Return resonseInnoviti
        Catch ex As Exception
            'LogException(ex)
        End Try
    End Function

    'added on 12 may - ashma - for Innoviti
    Public Function PaymentThroughEDC(ByVal invoiceNumber As String, ByVal amount As String, ByVal posReferenceNumber As String, ByVal transactionTime As String) As Dictionary(Of String, String)
        Try
            Dim responseFromInnovitii As New Dictionary(Of String, String)
            If True Then

                responseFromInnovitii = RawDataPrint.RawPrinterHelper.PaymentThroughEDC(invoiceNumber, amount, posReferenceNumber, transactionTime)
                'responseFromInnovitii.Add("StatusCode", "00")
                'responseFromInnovitii.Add("StatusMessage", "Valid")
                'responseFromInnovitii.Add("TransactionTime", "1111111")
                'responseFromInnovitii.Add("RetrievalReferenceNumber", "8793")
                'responseFromInnovitii.Add("CardNumber", "3000")
            End If
            Return responseFromInnovitii
        Catch ex As Exception
            ' LogException(ex)
        End Try

    End Function

End Module
