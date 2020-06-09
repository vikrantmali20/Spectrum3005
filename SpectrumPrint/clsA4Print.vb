Imports System.ComponentModel
Imports System.Management

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
Imports SpectrumBL


Public Class clsA4Print
    Private stringToPrint, documentContents As String
    'WithEvents printPreviewDialog1 As New PrintPreviewDialog()
    Private WithEvents printDocument1 As New PrintDocument()
    Private WithEvents explorer As New PosExplorer
    'WithEvents explorer As PosExplorer
    WithEvents gOposScanner As Scanner
    WithEvents gOposMSR As Msr
    Public gOposPolDisplay As LineDisplay
    WithEvents gOposPrinter As PosPrinter
    WithEvents gOposCashDrawer As CashDrawer
    WithEvents gOposFiscalPrinter As FiscalPrinter


#Region "Property for  Printing format for A4 size "

    ''' <summary>
    ''' Printing document Types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DocumentTypeList
        SalesOrder
        BirthListStatus
        PettyCashVoucher
    End Enum
    ''' <summary>
    '''  Assgin for Document Type 
    ''' </summary>
    ''' <remarks></remarks>
    Private _DocumentType As DocumentTypeList
    Public Property DocumentType() As DocumentTypeList
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As DocumentTypeList)
            _DocumentType = value
        End Set
    End Property
    ''' <summary>
    ''' Title for Document.
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary> 
    ''' <remarks>String</remarks>

    Private _strA4Title As String
    Public Property A4Title() As String
        Get
            Return _strA4Title
        End Get
        Set(ByVal value As String)
            _strA4Title = value
        End Set
    End Property
    ''' <summary>
    ''' Header for document
    ''' </summary>
    ''' <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' <remarks>String</remarks>
    Private _strA4Header As String
    Public Property A4Header() As String
        Get
            Return _strA4Header
        End Get
        Set(ByVal value As String)
            _strA4Header = value
        End Set
    End Property
    ''' <summary>
    ''' Terms And  Condition for document
    '''    <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4TermsNConitions As String
    Public Property A4TermsNConditions() As String
        Get
            Return _strA4TermsNConitions
        End Get
        Set(ByVal value As String)
            _strA4TermsNConitions = value
        End Set
    End Property
    ''' <summary>
    ''' Cashier information for display on document
    '''    <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strCashierDetails As String
    Public Property A4CashierDetails() As String
        Get
            Return _strCashierDetails
        End Get
        Set(ByVal value As String)
            _strCashierDetails = value
        End Set
    End Property

    ''' <summary>
    ''' Customer information is only for document .
    ''' <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4CustomerDetails As String
    Public Property A4CustomerDetails() As String
        Get
            Return _strA4CustomerDetails
        End Get
        Set(ByVal value As String)
            _strA4CustomerDetails = value
        End Set
    End Property
    ''' <summary>
    ''' Delivery Address for document
    ''' </summary>
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' <remarks></remarks>
    Private _strA4DeliveryAddress As String
    Public Property A4DeliveryAddress() As String
        Get
            Return _strA4DeliveryAddress
        End Get
        Set(ByVal value As String)
            _strA4DeliveryAddress = value
        End Set
    End Property
    ''' <summary>
    '''  Purchased Items Header 
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4ItemHeader As String
    Public Property A4ItemHeader() As String
        Get
            Return _strA4ItemHeader
        End Get
        Set(ByVal value As String)
            _strA4ItemHeader = value
        End Set
    End Property
    ''' <summary>
    ''' All Purchased items details 
    ''' </summary>
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' <remarks></remarks>
    Private _strA4ItemDetails As String
    Public Property A4ItemDetails() As String
        Get
            Return _strA4ItemDetails
        End Get
        Set(ByVal value As String)
            _strA4ItemDetails = value
        End Set
    End Property
    ''' <summary>
    ''' Payment details agianst Bill
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4PaymentDetails As String
    Public Property A4PaymentDetails() As String
        Get
            Return _strA4PaymentDetails
        End Get
        Set(ByVal value As String)
            _strA4PaymentDetails = value
        End Set
    End Property

    ''' <summary>
    ''' Footer for document .mainly this contails customer sign etc. details 
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4Footer As String
    Public Property A4Footer() As String
        Get
            Return _strA4Footer
        End Get
        Set(ByVal value As String)
            _strA4Footer = value
        End Set
    End Property
    ''' <summary>
    ''' Remark   display on printing document .
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4Remark As String
    Public Property A4Remark() As String
        Get
            Return _strA4Remark
        End Get
        Set(ByVal value As String)
            _strA4Remark = value
        End Set
    End Property

    ''' <summary>
    ''' Message for giftvocuher details for only BirthList Status document.
    '''  <UsedBy>clsBirthList.cs,clsPrintSalesOrder.cs</UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4GiftVoucher As String
    Public Property A4GiftVoucher() As String
        Get
            Return _strA4GiftVoucher
        End Get
        Set(ByVal value As String)
            _strA4GiftVoucher = value
        End Set
    End Property
    ''' <summary>
    '''  Open Amount for only BirthList Status document
    '''  <UsedBy>clsBirthList.cs </UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strA4OpenAmount As String
    Public Property A4OpenAmount() As String
        Get
            Return _strA4OpenAmount
        End Get
        Set(ByVal value As String)
            _strA4OpenAmount = value
        End Set
    End Property
    ''' <summary>
    ''' Message for GiftVoucher print document .
    ''' </summary>
    ''' <remarks></remarks> 
    Private _GiftReceiptMessage As String
    Public Property GiftReceiptMessage() As String
        Get
            Return _GiftReceiptMessage
        End Get
        Set(ByVal value As String)
            _GiftReceiptMessage = value
        End Set
    End Property

    ''' <summary>
    '''  Total Discount Amount for only Birhtlist Status Document .
    ''' <UsedBy>clsBirthList.cs </UsedBy>
    ''' </summary>
    ''' <remarks></remarks>

    Private _decFinalDiscount As String
    Public Property FinalDiscount() As String
        Get
            Return _decFinalDiscount
        End Get
        Set(ByVal value As String)
            _decFinalDiscount = value
        End Set
    End Property
    ''' <summary>
    '''  Total Discount Amount for only Birhtlist Status Document .
    ''' <UsedBy>clsBirthList.cs </UsedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Private _strTotalDiscount As String
    Public Property A4Total() As String
        Get
            Return _strTotalDiscount
        End Get
        Set(ByVal value As String)
            _strTotalDiscount = value
        End Set
    End Property
    ''' <summary>
    ''' Welcome message for printing document
    ''' </summary>
    ''' <remarks></remarks>
    Private _strWelcomeMsg As String
    Public Property A4WelcomeMessage() As String
        Get
            Return _strWelcomeMsg
        End Get
        Set(ByVal value As String)
            _strWelcomeMsg = value
        End Set
    End Property
    ''' <summary>
    ''' Tax information on printing document 
    ''' </summary>
    ''' <remarks></remarks>
    Private _strTaxInformation As String
    Public Property A4TaxInformation() As String
        Get
            Return _strTaxInformation
        End Get
        Set(ByVal value As String)
            _strTaxInformation = value
        End Set
    End Property

    ''' <summary>
    ''' Tax details against current invoice   
    ''' </summary>
    ''' <remarks></remarks>
    Private _strTaxDetailsInvoice As String
    Public Property A4TaxDetailsInvoice() As String
        Get
            Return _strTaxDetailsInvoice
        End Get
        Set(ByVal value As String)
            _strTaxDetailsInvoice = value
        End Set
    End Property

    ''' <summary>
    ''' promotion messages for printing document 
    ''' </summary>
    ''' <remarks></remarks>
    Private _strPromotionMsg As String
    Public Property A4PromotionInformation() As String
        Get
            Return _strPromotionMsg
        End Get
        Set(ByVal value As String)
            _strPromotionMsg = value
        End Set
    End Property

    Private _CashDrawerWithoutDriver As String
    Public Property CashDrawerWithoutDriver() As String
        Get
            Return _CashDrawerWithoutDriver
        End Get
        Set(ByVal value As String)
            _CashDrawerWithoutDriver = value
        End Set
    End Property
#End Region


  

    ''' <summary>
    ''' Before callling this function you need to assign property values.
    ''' </summary>
    ''' <param name="strPrintOrPreview"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Dim arryList As New Dictionary(Of String, System.Drawing.Font)
    Dim arryListTemp As New Dictionary(Of String, System.Drawing.Font)
    Dim flg As Boolean = False
    Public Function fnPrint(Optional ByVal strPrintOrPreview As String = "") As String
        ' strPrintOrPreview = 'PRN' that means Print
        ' strPrintOrPreview = 'PRV' that means Preview 
        SpectrumPrintFont.DocumentType = DocumentType
        If Not A4Header Is Nothing AndAlso Not A4Header = String.Empty Then
            arryList.Add(A4Header, SpectrumPrintFont.Header)
        End If 
        If Not A4WelcomeMessage Is Nothing AndAlso Not A4WelcomeMessage = String.Empty Then
            arryList.Add("LineBreak" + vbCrLf + A4WelcomeMessage, SpectrumPrintFont.ItemDetail)
        End If 
        arryList.Add(A4Title, SpectrumPrintFont.Title) 
        If Not A4TaxInformation Is Nothing AndAlso Not A4TaxInformation = String.Empty Then
            arryList.Add(A4TaxInformation + vbCrLf + "LineBreak", SpectrumPrintFont.ItemDetail)
        End If
        If Not A4CashierDetails Is Nothing AndAlso Not A4CashierDetails = String.Empty Then
            arryList.Add(A4CashierDetails + vbCrLf + "LineBreak", SpectrumPrintFont.CustomerInfo)
        End If
        If Not A4CustomerDetails Is Nothing AndAlso Not A4CustomerDetails = String.Empty Then
            arryList.Add(A4CustomerDetails, SpectrumPrintFont.CustomerInfo)
        End If

        If Not A4DeliveryAddress Is Nothing AndAlso Not A4DeliveryAddress = String.Empty Then
            arryList.Add(A4DeliveryAddress, SpectrumPrintFont.DeliveryAddress)
        End If
        If Not A4Remark Is Nothing AndAlso Not A4Remark = String.Empty Then
            arryList.Add(A4Remark, SpectrumPrintFont.Remark)
        End If
        If Not A4ItemHeader Is Nothing AndAlso Not A4ItemHeader = String.Empty Then
            arryList.Add(A4ItemHeader, SpectrumPrintFont.ItemHeader)
        End If
        If Not A4ItemDetails Is Nothing AndAlso Not A4ItemDetails = String.Empty Then
            arryList.Add(A4ItemDetails, SpectrumPrintFont.ItemDetail)
        End If
        If DocumentType = DocumentTypeList.BirthListStatus Then 
            'arryList.Add(String.Format("{0} ", "GIFT VOUCHERS ".PadRight(90) + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemHeader)
            arryList.Add(String.Format("{0} ", (getValueByKey("CLSA4P001") & " ").PadRight(90) + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemHeader)
            If Not A4GiftVoucher Is Nothing Then
                arryList.Add(A4GiftVoucher + vbCrLf + "LineBreak", SpectrumPrintFont.ItemDetail)
            End If
            'arryList.Add(String.Format("{0} ", "OPEN AMOUNT ".PadRight(90)), SpectrumPrintFont.ItemHeader)
            arryList.Add(String.Format("{0} ", (getValueByKey("CLSA4P002") & " ").PadRight(90)), SpectrumPrintFont.ItemHeader)
            If Not A4OpenAmount Is Nothing Then
                'arryList.Add(String.Format("{0} {1} ", "Open amount ".PadRight(90), A4OpenAmount + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemDetail)
                arryList.Add(String.Format("{0} {1} ", (getValueByKey("CLSA4P003") & " ").PadRight(90), A4OpenAmount + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemDetail)
            End If

            'Dim strFinalDiscount As String = String.Format("{0} {1}", "TOTAL FINAL DISCOUNT".PadRight(90), FinalDiscount)
            Dim strFinalDiscount As String = String.Format("{0} {1}", (getValueByKey("CLSA4P004") & " ").PadRight(90), FinalDiscount)
            arryList.Add(strFinalDiscount + vbCrLf, SpectrumPrintFont.ItemDetail)
        End If
        
        If Not A4PaymentDetails Is Nothing AndAlso Not A4PaymentDetails = String.Empty Then

            arryList.Add(A4PaymentDetails + vbCrLf + "LineBreak" + vbCrLf, SpectrumPrintFont.ItemDetail)
        End If

        If DocumentType = DocumentTypeList.SalesOrder AndAlso Not A4TaxDetailsInvoice Is Nothing Then
            If A4TaxDetailsInvoice <> String.Empty Then
                'arryList.Add(String.Format("{0} ", "Tax Details ".PadRight(90) + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemHeader)
                arryList.Add(String.Format("{0} ", (getValueByKey("CLSA4P005") & " ").PadRight(90) + vbCrLf + "LineBreak"), SpectrumPrintFont.ItemHeader)

                arryList.Add(A4TaxDetailsInvoice + vbCrLf + "LineBreak", SpectrumPrintFont.ItemDetail)
            End If
        End If
        If Not A4TermsNConditions Is Nothing AndAlso Not A4TermsNConditions = String.Empty Then
            arryList.Add(A4TermsNConditions, SpectrumPrintFont.ItemDetail)
        End If
        If Not A4Footer Is Nothing AndAlso Not A4Footer = String.Empty Then
            arryList.Add(A4Footer + vbCrLf + "LineBreak" + vbCrLf, SpectrumPrintFont.ItemDetail)
        End If
        If Not A4PromotionInformation Is Nothing AndAlso Not A4PromotionInformation = String.Empty Then
            arryList.Add(A4PromotionInformation + vbCrLf + "LineBreak", SpectrumPrintFont.ItemDetail)
        End If
        If Not GiftReceiptMessage Is Nothing AndAlso Not GiftReceiptMessage = String.Empty Then
            arryList.Add(GiftReceiptMessage + vbCrLf + "LineBreak" + vbCrLf, SpectrumPrintFont.ItemDetail) 
        End If

        arryListTemp = CreateDictionaryClone(arryList)

        Try

            printDocument1.OriginAtMargins = True
            printDocument1.DefaultPageSettings.Margins.Left = 30
            printDocument1.DefaultPageSettings.Margins.Right = 70
            printDocument1.DefaultPageSettings.Margins.Bottom = 120
            printDocument1.PrinterSettings.PrinterName = PrinterName
            If strPrintOrPreview = "PRN" Then
                printDocument1.PrinterSettings.PrintToFile = True
                printDocument1.Print()
            Else
                printPreviewDialog1.Document = printDocument1
                printPreviewDialog1.Select()
                printPreviewDialog1.ShowDialog()

            End If
            fnPrint = "Success"
            'fnPrint = getValueByKey("CLSA4P006")

        Catch ex As Exception
            If printDocument1.PrinterSettings.IsValid = False Then
                MessageBox.Show(String.Format(getValueByKey("CLBL03"), PrinterName), "CLBL03 - " & getValueByKey("CLAE04"))
            End If
            fnPrint = "Failed"
            'fnPrint = getValueByKey("CLSA4P007")
        End Try
    End Function


    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
      ByVal e As PrintPageEventArgs) Handles printDocument1.PrintPage
        'Dim g As Graphics = e.Graphics
        'g.PageUnit = GraphicsUnit.Inch

        Dim MyFont As New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Dim charactersOnPage As Integer = 0
        Dim linesPerPage As Integer = 0

        'If VarBarcode.Text <> String.Empty Then
        '    e.Graphics.DrawImage(VarBarcode.Image, 0, 0)
        'End If
        ' Sets the value of charactersOnPage to the number of characters 
        ' of stringToPrint that will fit within the bounds of the page.

        ' e.Graphics.MeasureString(stringToPrint, MyFont, e.MarginBounds.Size, _
        'StringFormat.GenericTypographic, charactersOnPage, linesPerPage)
        ' ' Draws the string within the bounds of the page.
        ' e.Graphics.DrawString(stringToPrint, MyFont, Brushes.Black, _
        '     e.MarginBounds, StringFormat.GenericTypographic)
        ' ' Remove the portion of the string that has been printed.
        ' stringToPrint = stringToPrint.Substring(charactersOnPage)
        ' ' Check to see if more pages are to be printed.

        ' e.HasMorePages = stringToPrint.Length > 0
        If flg Then
            'Do nothing
        Else
            arryList = CreateDictionaryClone(arryListTemp)
        End If
        A4DocumentWrite(e)
        ' If there are no more pages, reset the string to be printed.

        If Not e.HasMorePages Then
            stringToPrint = documentContents
        End If

    End Sub
    Dim currentPos As Integer

    Dim sizeRt As SizeF

    Public Function A4DocumentWrite(ByRef e As PrintPageEventArgs) As Boolean
        Try

            currentPos = e.PageBounds.Y
            Dim localArryList As Dictionary(Of String, System.Drawing.Font)
            localArryList = CreateDictionaryClone(arryList)
            Dim array As Array = arryList.ToArray()
            If Not array Is Nothing Then
                For index As Integer = 0 To array.Length - 1
                    If Not array(index) Is Nothing Then
                        Dim strLineMain As System.Collections.Generic.KeyValuePair(Of String, System.Drawing.Font) = array(index)
                        Dim strLine() As String = strLineMain.Key.Split(vbLf)
                        Dim strLinFont As System.Drawing.Font = strLineMain.Value
                        If strLine.Length > 0 Then
                            If Not (LinePrint(e, strLine, strLinFont)) Then
                                Dim Keys(arryList.Count - 1) As String
                                Dim newStrLine(strLine.Length - 1) As String
                                Dim strb As String = ""
                                arryList.Keys.CopyTo(Keys, 0)

                                For i As Integer = 0 To index
                                    arryList.Remove(Keys.GetValue(i))
                                Next i

                                If currentIndex < strLine.Length Then

                                    'For i As Integer = index To array.Length - 1
                                    For index1 As Integer = 0 To currentIndex - 1
                                        strLine(index1) = strLine(index1).Remove(0)
                                    Next index1
                                    Dim cnt As Integer = -1
                                    For j As Integer = 0 To strLine.Length - 1
                                        If Not String.IsNullOrEmpty(strLine(j)) Then
                                            cnt = cnt + 1
                                        End If
                                    Next j
                                    ReDim newStrLine(cnt)
                                    cnt = 0
                                    For j As Integer = 0 To strLine.Length - 1
                                        If Not String.IsNullOrEmpty(strLine(j)) Then
                                            newStrLine(cnt) = strLine(j) + vbLf
                                            strb = strb + strLine(j) + vbLf
                                            cnt = cnt + 1
                                        End If
                                    Next j
                                    arryList.Add(strb.ToString(), SpectrumPrintFont.ItemDetail)

                                End If

                                'For index1 As Integer = 0 To index
                                ' localArryList.Remove(strLineMain.Key)
                                'Next
                                currentIndex = 0
                                flg = True
                                localArryList.Remove(strLineMain.Key)
                                Return False
                            Else
                                'arryList.Remove(strLineMain.Key)
                                localArryList.Remove(strLineMain.Key)
                            End If
                        End If
                    End If

                Next
                localArryList.Clear()
                flg = False
            End If


        Catch ex As Exception

        End Try
    End Function


    Function CreateDictionaryClone(ByVal oldDict As Dictionary(Of String, System.Drawing.Font)) As Dictionary(Of String, System.Drawing.Font)
        Dim newDict As New Dictionary(Of String, System.Drawing.Font)
        For Each pair As KeyValuePair(Of String, System.Drawing.Font) In oldDict
            newDict.Add(pair.Key, pair.Value)
        Next
        Return newDict
    End Function


    ''' <summary>
    '''  Line by line printing 
    ''' </summary>
    ''' <remarks></remarks>
    Dim currentIndex As Integer = 0
    Private Function LinePrint(ByVal e As PrintPageEventArgs, ByVal strToPrint() As String, ByVal strFont As System.Drawing.Font) As Boolean
        Try
            If Not currentIndex = 0 Then

                For index As Integer = currentIndex To strToPrint.Length - 1

                    If strToPrint(index).Contains("LineBreak") Then
                        e.Graphics.DrawString(strLineA4, strFont, Brushes.Black, e.MarginBounds.Left, currentPos, StringFormat.GenericTypographic)
                        sizeRt = e.Graphics.MeasureString(strToPrint(index).ToString(), strFont)
                        currentPos = currentPos + sizeRt.Height
                        If currentPos > e.MarginBounds.Bottom Then
                            e.HasMorePages = True
                            currentIndex = index + 1
                            currentPos = 0
                            Return False
                        End If
                    Else
                        e.Graphics.DrawString(strToPrint(index).ToString(), strFont, Brushes.Black, e.MarginBounds.Left, currentPos, StringFormat.GenericTypographic)
                        sizeRt = e.Graphics.MeasureString(strToPrint(index).ToString(), strFont)
                        currentPos = currentPos + sizeRt.Height
                        If currentPos > e.MarginBounds.Bottom Then
                            e.HasMorePages = True
                            currentPos = 0
                            currentIndex = index + 1
                            Return False
                        End If
                    End If
                Next
                currentIndex = 0
            Else
                For index As Integer = currentIndex To strToPrint.Length - 1
                    If strToPrint(index).Contains("LineBreak") Then
                        e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, currentPos, e.MarginBounds.Right, currentPos)

                        'e.Graphics.DrawString(strLineA4, strFont, Brushes.Black, BoundsSpecified.X, currentPos, StringFormat.GenericTypographic)
                        'sizeRt = e.Graphics.MeasureString(strToPrint(index).ToString(), strFont)

                        currentPos = currentPos + 2
                        If currentPos > e.MarginBounds.Bottom Then
                            e.HasMorePages = True
                            currentIndex = index + 1
                            currentPos = 0
                            Return False
                        End If
                    Else

                        e.Graphics.DrawString(strToPrint(index).ToString(), strFont, Brushes.Black, e.MarginBounds.Left, currentPos, StringFormat.GenericTypographic)
                        sizeRt = e.Graphics.MeasureString(strToPrint(index).ToString(), strFont)
                        currentPos = currentPos + sizeRt.Height

                        If currentPos > e.MarginBounds.Bottom Then
                            e.HasMorePages = True
                            currentPos = 0
                            currentIndex = index + 1
                            Return False
                        End If
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Sub DrawerReady(ByRef gOposCashDrawer As CashDrawer)
        gOposCashDrawer.Open()
        gOposCashDrawer.Claim(100)
        gOposCashDrawer.DeviceEnabled = True
    End Sub

    Sub Draweropen(ByRef gOposCashDrawer As CashDrawer)
        gOposCashDrawer.OpenDrawer()
    End Sub
    Sub DrawerRealease(ByRef gOposCashDrawer As CashDrawer)
        gOposCashDrawer.DeviceEnabled = False
        gOposCashDrawer.Release()
        gOposCashDrawer.Close()
    End Sub

    'Rohit Start
    Public Sub OperateDevice(ByVal strDeviceType As String, Optional ByVal strToPrint As String = Nothing, Optional CDflag As Integer = 0)
        Dim explorer As New PosExplorer
        Dim DevInfo As DeviceInfo
        Dim goposprinter As PosPrinter
        'Dim gOposCashDrawer As CashDrawer
        If strDeviceType = DeviceType.CashDrawer Then
            'DevInfo = explorer.GetDevice(DeviceType.CashDrawer, My.Settings.Drawer)
            If String.IsNullOrEmpty(CashDrawerWithoutDriver) Then
                DevInfo = explorer.GetDevice(DeviceType.CashDrawer, SpectrumBL.clsLogin.ReadSpectrumParamFile("Drawer"))
            Else
                For Each Device As Object In explorer.GetDevices()
                    Dim POSSoName = Device.SoName.ToString
                    If CashDrawerWithoutDriver.Equals(POSSoName) Then
                        DevInfo = Device
                        Exit For
                    End If
                Next
            End If


            If Not DevInfo Is Nothing Then
                Try
                    gOposCashDrawer = explorer.CreateInstance(DevInfo)

                    If CDflag = 1 Then
                        DrawerReady(gOposCashDrawer)
                        Draweropen(gOposCashDrawer)
                    Else
                        DrawerReady(gOposCashDrawer)
                        Draweropen(gOposCashDrawer)
                        DrawerRealease(gOposCashDrawer)
                    End If

                Catch ex As Exception
                    LogException(ex.InnerException)
                    If Not CDflag = 1 Then
                        DrawerRealease(gOposCashDrawer)
                    End If
                End Try
            End If

        ElseIf strDeviceType = DeviceType.PosPrinter Then
            DevInfo = explorer.GetDevice(DeviceType.PosPrinter, PrinterName)
            If Not DevInfo Is Nothing Then
                Try
                    goposprinter = explorer.CreateInstance(DevInfo)
                    goposprinter.Open()
                    goposprinter.Claim(1000)
                    goposprinter.DeviceEnabled = True
                    goposprinter.PrintNormal(PrinterStation.Receipt, strToPrint)
                    Try
                        If goposprinter.CapRecBarCode = True Then
                            If VarBarcode IsNot Nothing Then
                                goposprinter.PrintBarCode(PrinterStation.Receipt, VarBarcode.Text, PBarCodeType, 50, 100, 0, BarCodeTextPosition.Below)
                            End If
                        Else
                            '----- barcode print from image file
                            If VarBarcode IsNot Nothing Then
                                Dim ESC As String
                                ESC = Chr(&H1B)
                                Dim ImgBarcode As Bitmap
                                Dim strFilePath As String = ""
                                strFilePath = Application.StartupPath
                                ImgBarcode = VarBarcode.GetImage(Imaging.ImageFormat.Bmp)
                                ImgBarcode.Save(strFilePath & "\PrintBarCode.bmp", Imaging.ImageFormat.Bmp)
                                strFilePath = strFilePath & "\PrintBarCode.bmp"


                                'Output by the high quality mode for barcode
                                goposprinter.RecLetterQuality = True
                                ''//ESC|bC = Bold
                                ''//ESC|uC = Underline
                                ''//ESC|2C = Wide charcter


                                Dim iRetryCount As Integer


                                If goposprinter.CapRecBitmap Then
                                    Dim bSetBitmapSuccess As Boolean
                                    For iRetryCount = 0 To 5
                                        Try
                                            goposprinter.SetBitmap(1, PrinterStation.Receipt, strFilePath, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter)
                                            bSetBitmapSuccess = True
                                            'goposprinter.PrintNormal(PrinterStation.Receipt, ESC + "|bC") '_
                                            '+ "Tax excluded. $200.00" + ESC + "|N" + vbCrLf)
                                            goposprinter.PrintNormal(PrinterStation.Receipt, ESC + "|N" + vbCrLf)
                                            goposprinter.PrintImmediate(PrinterStation.Receipt, ESC + "|1B")
                                            goposprinter.PrintNormal(PrinterStation.Receipt, ESC + "|N" + vbCrLf)
                                            Exit For
                                        Catch pce As PosControlException
                                            If pce.ErrorCode = ErrorCode.Failure And pce.ErrorCodeExtended = 0 And pce.Message = "It is not initialized." Then
                                                System.Threading.Thread.Sleep(1000)
                                            End If
                                        End Try
                                    Next
                                    'If Not bSetBitmapSuccess Then
                                    ' MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep4" _
                                    ' , MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    'End If
                                End If
                                '-----
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.PrintNormal(PrinterStation.Receipt, vbCrLf)
                    goposprinter.CutPaper(90)
                    goposprinter.DeviceEnabled = False
                    goposprinter.Release()
                    goposprinter.Close()
                Catch ex As Exception
                    goposprinter.DeviceEnabled = False
                    goposprinter.Release()
                    goposprinter.Close()
                End Try
            End If
        ElseIf strDeviceType = DeviceType.FiscalPrinter Then
            Dim strFiscalprintername As String
            strFiscalprintername = clsFiscalPrinting.ReadSpectrumParamFile("FISCALPRINTERNAME")
            DevInfo = explorer.GetDevice(DeviceType.FiscalPrinter, strFiscalprintername)
            If Not DevInfo Is Nothing Then
                Try
                    gOposFiscalPrinter = explorer.CreateInstance(DevInfo)
                    gOposFiscalPrinter.Open()
                    gOposFiscalPrinter.Claim(1000)
                    gOposFiscalPrinter.DeviceEnabled = True
                    gOposFiscalPrinter.PrintNormal(FiscalPrinterStations.Receipt, strToPrint)
                Catch ex As Exception
                End Try
            End If
        End If


    End Sub
    'Rohit End



    Private Sub gOposCashDrawer_StatusUpdateEvent(ByVal sender As Object, ByVal e As Microsoft.PointOfService.StatusUpdateEventArgs) Handles gOposCashDrawer.StatusUpdateEvent
        'MsgBox(e.EventId)
    End Sub

End Class

''' <summary>
''' Printing Document font styles 
''' </summary>
''' <remarks></remarks>
Public Class SpectrumPrintFont

    Private Shared _DocumentType As clsA4Print.DocumentTypeList
    Public Shared Property DocumentType() As clsA4Print.DocumentTypeList
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As clsA4Print.DocumentTypeList)
            _DocumentType = value
        End Set
    End Property

    ''' <summary>
    '''  Header Font
    ''' </summary>
    ''' <value>system.drawing.Font</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Header() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Title font style  
    ''' </summary>
    ''' <value>system.drawing.Font</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Title() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    '''  Cashier Deatils font style 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property CashierDetails() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Customer information font style
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property CustomerInfo() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Customer delivery address font style
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property DeliveryAddress() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    '''  Purchased items information header style
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ItemHeader() As Font
        Get
            Dim MyFont As System.Drawing.Font
            If DocumentType = clsA4Print.DocumentTypeList.PettyCashVoucher Then
                MyFont = New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Else
                MyFont = New System.Drawing.Font("Courier New", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If

            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Purchased items details font style 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ItemDetail() As Font
        Get
            Dim MyFont As System.Drawing.Font
            If DocumentType = clsA4Print.DocumentTypeList.PettyCashVoucher Then
                MyFont = New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Else
                MyFont = New System.Drawing.Font("Courier New", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If

            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' payment information against bill 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property PaymentDetail() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Footer for document 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Footer() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property
    ''' <summary>
    ''' Remark font style 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Remark() As Font
        Get
            Dim MyFont As New System.Drawing.Font("Courier New", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Return MyFont
        End Get
    End Property


End Class
