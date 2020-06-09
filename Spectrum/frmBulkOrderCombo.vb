Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections
Imports SpectrumPrint
Public Class frmBulkOrderCombo
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Dim objBulkCombo As New clsBulkComboAdd()
    Dim dsBulkCombo As New DataSet
    Dim DvComboItemDetail As DataView
    Dim UpdateFlag As Boolean = False
    Dim objItemSch As New clsIteamSearch
    Dim objCM_2 As New clsCashMemo
    Dim objClsCommon As New clsCommon

    Dim vSiteCode As String = clsAdmin.SiteCode
    'Dim vTerminalID As String = clsAdmin.TerminalID
    'Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    'Dim vCurrencyCode As String = clsAdmin.CurrencyCode
    Dim vUserName As String = clsAdmin.UserName

    Private _SoBulkComboHdr As DataTable
    Public Property DtSoBulkComboHdr() As DataTable
        Get
            Return _SoBulkComboHdr
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboHdr = value
        End Set
    End Property

    Private _SoBulkComboDtl As DataTable
    Public Property DtSoBulkComboDtl() As DataTable
        Get
            Return _SoBulkComboDtl
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboDtl = value
        End Set
    End Property

    Private _BulkComboMstId As Int64
    Public Property BulkComboMstId() As Int64
        Get
            Return _BulkComboMstId
        End Get
        Set(ByVal value As Int64)
            _BulkComboMstId = value
        End Set
    End Property

    Private _comboSrNo As Integer
    Public Property ComboSrNo() As Integer
        Get
            Return _comboSrNo
        End Get
        Set(ByVal value As Integer)
            _comboSrNo = value
        End Set
    End Property

    Private _IsStrGenerateApplicable As Boolean
    Public Property IsStrGenerateApplicable() As Boolean
        Get
            Return _IsStrGenerateApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsStrGenerateApplicable = value
        End Set
    End Property


    Enum DgBulkOrder
        Selects = 1
        ArticleCode
        ArticleDescription
        EAN
        Packageduom
        Qty
        Weight
        STRQty
        StrExcludeCheck
        BaseUOM
        ItemQtyBaseUOM
        BulkComboDetId
    End Enum




    Private Sub frmBulkOrderCombo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dtPackagingBox = objBulkCombo.GetPackagingBoxDataSet(clsDefaultConfiguration.PackagingBoxLastNodeCode)

            PopulateComboBox(dtPackagingBox, CboPakagingBox)
            If clsDefaultConfiguration.PrintItemFullName Then
                CboPakagingBox.DisplayMember = "ArticleName"
            Else
                CboPakagingBox.DisplayMember = "ArticleShortName"
            End If
            CboPakagingBox.ValueMember = "ArticleCode"
            pC1ComboSetDisplayMember(CboPakagingBox)

            AddHandler txtSearch.KeyDown, AddressOf txtSearch_KeyDown
            Call getBinding()

            For colno = 1 To DgBulkComboGrid.Cols.Count - 1
                If DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "itemdesc".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "itemcode".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "Qty".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "Selects".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "Weight".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "STRQty".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "uom".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "Remove From STR".ToUpper() _
                    AndAlso DgBulkComboGrid.Cols(colno).Name.ToUpper() <> "" Then

                    HideColumns(DgBulkComboGrid, False, DgBulkComboGrid.Cols(colno).Name)

                End If
                'If Not DgBulkComboGrid.Cols(colno).DataType Is Nothing AndAlso DgBulkComboGrid.Cols(colno).DataType.ToString() = "System.Decimal" Then
                '    DgBulkComboGrid.Cols(colno).Format = "0.000"
                'End If

            Next

            DgBulkComboGrid.Rows.Count = 1


            '---- Load In case of Edit Mode 
            If DtSoBulkComboHdr.Rows.Count > 0 Then
                Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
                If drHdr.Count > 0 Then
                    FillData()
                End If
            End If

            Dim dtItemData = objCM_2.GetItemDetails(clsAdmin.SiteCode, txtSearch.Text.Trim, clsAdmin.LangCode)
            PopulateComboBox(dtItemData, txtSearch)
            If clsDefaultConfiguration.PrintItemFullName Then
                txtSearch.DisplayMember = "ArticleName"
            Else
                txtSearch.DisplayMember = "ArticleShortName"
            End If
            txtSearch.ValueMember = "ArticleCode"
            pC1ComboSetDisplayMember(txtSearch)

            CboPakagingBox.Focus()
            CboPakagingBox.Select()

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    'Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0

            If e.KeyCode = Keys.Delete AndAlso DgBulkComboGrid.Rows.Count > 1 AndAlso txtSearch.Text.Length = 0 Then
                DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
                If (DgBulkComboGrid.Rows.Count > 1) Then
                    DgBulkComboGrid.Select(1, 2)
                End If
                sender.Select()
                sender.Focus()
                Exit Sub
            End If

            If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
                Dim dt As New DataTable
                'code commented by vipul
                ' dt = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, sender.Text.Trim, clsAdmin.LangCode)
                'code added by vipul for issue id 2722
                dt = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, txtSearch.SelectedValue.Trim, clsAdmin.LangCode)
                If DgBulkComboGrid.Rows.Count > 1 Then
                    For index = 1 To DgBulkComboGrid.Rows.Count - 1

                        If dt.Rows(0)("ArticleCode").ToString() = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode) Then
                            ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                            Exit Sub
                        End If

                    Next
                End If
                Dim ItemDesc As String = String.Empty
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    Dim Stock As Double = objCM_2.GetStocks(clsAdmin.SiteCode, Ean.Trim, strArticle.Trim, True, clsDefaultConfiguration.IsBatchManagementReq, "")
                    If OnlineConnect = True AndAlso clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                        If CDbl(Stock) <= 0 Or Stock <= dt.Rows(0)("Quantity") Then
                            ShowMessage(getValueByKey("CM017"), "CM017 - " & getValueByKey("CLAE04"))
                            dt = Nothing
                            sender.Text = String.Empty
                            sender.Focus()
                            Exit Sub
                        End If
                    End If

                    DgBulkComboGrid.Rows.Add()

                    If btnAddBulkCombo.Enabled Then
                        IsStrGenerateApplicable = True
                    End If

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Selects) = ""
                    If clsDefaultConfiguration.PrintItemFullName Then
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleName")
                    Else
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                    End If

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("EAN").ToString()

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Packageduom) = dt.Rows(0)("BaseUnitofMeasure").ToString()

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Qty) = Val(dt.Rows(0)("Qty"))

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BaseUOM) = dt.Rows(0)("BaseUnitofMeasure")
                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ItemQtyBaseUOM) = Val(dt.Rows(0)("Qty"))

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))

                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.STRQty) = Val(dt.Rows(0)("STR_QTY"))
                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.StrExcludeCheck) = False
                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BulkComboDetId) = DgBulkComboGrid.Rows.Count - 1
                    sender.Text = String.Empty
                    sender.Focus()
                    sender.Select()

                    If (DgBulkComboGrid.Rows.Count > 1) Then
                        DgBulkComboGrid.Select(DgBulkComboGrid.Rows.Count - 1, 2)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            'MsgBox(ex.InnerException.Message)
            'MsgBox(ex.Message)
            LogException(ex)
            'ShowMessage("Error in Searcing of Item Details", "Error") 
        Finally
            Cursor.Current = Cursors.Default
            'txtSearch.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Focus()
        End Try

    End Sub

    Private Function SearchAvailableBarcodes(ByRef articleCode As String, ByRef IsBarcodeNotAvailable As Boolean) As String
        Dim barCode As String = String.Empty
        Dim objFrmBarcode As New frmSelectBarcode
        objFrmBarcode.ArticleCode = articleCode
        objFrmBarcode.ShowDialog()
        If objFrmBarcode.SelectedRow IsNot Nothing Then
            barCode = objFrmBarcode.SelectedRow.Cells("BatchBarcode").Value
        Else
            If objFrmBarcode.IsBarcodeNotAvailable Then
                IsBarcodeNotAvailable = True
            End If
        End If
        Return barCode
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim objPrint As New clsPrintSalesOrder
            Dim PrintSOPageSetup As String = "L40"
            PrintSOPageSetup = (GetPrintFormat(clsAdmin.TerminalID, "SOStatus")).Rows(0)(0)
            '---- Code Added By Mahesh Send Data of Selected Combo only 
            Dim DvSoBulkComboHdr As DataView = New DataView(DtSoBulkComboHdr, "BulkComboMstId=" & BulkComboMstId, "", DataViewRowState.CurrentRows)
            Dim DvSoBulkComboDtl As DataView = New DataView(DtSoBulkComboDtl, "BulkComboMstId=" & BulkComboMstId, "", DataViewRowState.CurrentRows)

            If PrintSOPageSetup = "L40" Then
                objPrint.SalesComboPrintL40("", clsDefaultConfiguration.SOPrintPreviewAllowed, DvSoBulkComboHdr.ToTable(), DvSoBulkComboDtl.ToTable(), dtPrinterInfo)
            Else
                objPrint.SalesComboPrintL80("", clsDefaultConfiguration.SOPrintPreviewAllowed, DvSoBulkComboHdr.ToTable(), DvSoBulkComboDtl.ToTable(), dtPrinterInfo)
            End If


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAddBulkCombo_Click(sender As Object, e As EventArgs) Handles btnAddBulkCombo.Click
        Try
            If ValidateBulkOrderCombo() Then
                Dim hdrRow As Integer = -1
                If DtSoBulkComboHdr.Rows.Count = 0 Then
                    DtSoBulkComboHdr.Rows.Add()
                    hdrRow = DtSoBulkComboHdr.Rows.Count - 1
                Else
                    For index = 0 To DtSoBulkComboHdr.Rows.Count - 1
                        If DtSoBulkComboHdr.Rows(index)("ComboSrNo") = ComboSrNo Then
                            hdrRow = index
                            Exit For
                        End If
                    Next index
                    If hdrRow = -1 Then
                        DtSoBulkComboHdr.Rows.Add()
                        hdrRow = DtSoBulkComboHdr.Rows.Count - 1
                    End If
                End If

                DtSoBulkComboHdr.Rows(hdrRow)("BulkComboMstId") = BulkComboMstId
                DtSoBulkComboHdr.Rows(hdrRow)("ComboSrNo") = ComboSrNo
                DtSoBulkComboHdr.Rows(hdrRow)("PackagingBoxCode") = CboPakagingBox.SelectedValue
                DtSoBulkComboHdr.Rows(hdrRow)("sitecode") = vSiteCode
                DtSoBulkComboHdr.Rows(hdrRow)("AdditionComments") = txtAdditionalComments.Text

                Dim CurrentDateTime As DateTime = objClsCommon.GetCurrentDate()
                DtSoBulkComboHdr.Rows(hdrRow)("CREATEDAT") = vSiteCode
                DtSoBulkComboHdr.Rows(hdrRow)("CREATEDBY") = vUserName
                DtSoBulkComboHdr.Rows(hdrRow)("CREATEDON") = CurrentDateTime
                DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDAT") = vSiteCode
                DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDBY") = vUserName
                DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDON") = CurrentDateTime
                DtSoBulkComboHdr.Rows(hdrRow)("STATUS") = True

                '----Delete all details record 
                Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId & " AND BulkComboDetId < 500 ")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        DtSoBulkComboDtl.Rows.Remove(row)
                    Next
                End If

                For index = 1 To DgBulkComboGrid.Rows.Count - 1
                    Dim dtRow As Int32 = -1
                    If DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) > 500 Then
                        For index2 = 0 To DtSoBulkComboDtl.Rows.Count - 1
                            If DtSoBulkComboDtl.Rows(index2)("BulkComboDetId") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) Then
                                dtRow = index2
                                Exit For
                            End If
                        Next index2
                    Else
                        DtSoBulkComboDtl.Rows.Add()
                        dtRow = DtSoBulkComboDtl.Rows.Count - 1
                    End If
                    DtSoBulkComboDtl.Rows(dtRow)("BulkComboDetId") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId)
                    DtSoBulkComboDtl.Rows(dtRow)("BulkComboMstId") = BulkComboMstId
                    DtSoBulkComboDtl.Rows(dtRow)("ArticleCode") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode)
                    DtSoBulkComboDtl.Rows(dtRow)("ArticleDescription") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleDescription)
                    DtSoBulkComboDtl.Rows(dtRow)("EAN") = DgBulkComboGrid.Rows(index)(DgBulkOrder.EAN)
                    DtSoBulkComboDtl.Rows(dtRow)("PackagedUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                    DtSoBulkComboDtl.Rows(dtRow)("Qty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)
                    DtSoBulkComboDtl.Rows(dtRow)("Weight") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)
                    DtSoBulkComboDtl.Rows(dtRow)("STRQty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.STRQty)
                    DtSoBulkComboDtl.Rows(dtRow)("StrExcludeCheck") = DgBulkComboGrid.Rows(index)(DgBulkOrder.StrExcludeCheck)
                    DtSoBulkComboDtl.Rows(dtRow)("BaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM)
                    DtSoBulkComboDtl.Rows(dtRow)("ItemQtyBaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ItemQtyBaseUOM)
                    DtSoBulkComboDtl.Rows(dtRow)("CREATEDAT") = vSiteCode
                    DtSoBulkComboDtl.Rows(dtRow)("CREATEDBY") = vUserName
                    DtSoBulkComboDtl.Rows(dtRow)("CREATEDON") = CurrentDateTime
                    DtSoBulkComboDtl.Rows(dtRow)("UPDATEDAT") = vSiteCode
                    DtSoBulkComboDtl.Rows(dtRow)("UPDATEDBY") = vUserName
                    DtSoBulkComboDtl.Rows(dtRow)("UPDATEDON") = CurrentDateTime
                    DtSoBulkComboDtl.Rows(dtRow)("STATUS") = True
                Next index
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#Region "Function"

    Private Sub FillData()
        Try
            'BulkComboMstId = DtSoBulkComboHdr.Rows(0)("BulkComboMstId")
            Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
            If drHdr.Count > 0 Then
                CboPakagingBox.SelectedValue = drHdr(0)("PackagingBoxCode")
                txtAdditionalComments.Text = drHdr(0)("AdditionComments")
            End If

            Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId)
            If drDtl.Count > 0 Then
                For index = 0 To drDtl.Count - 1
                    DgBulkComboGrid.Rows.Add()
                    Dim dtRow = DgBulkComboGrid.Rows.Count - 1
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.BulkComboDetId) = drDtl(index)("BulkComboDetId")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ArticleCode) = drDtl(index)("ArticleCode")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ArticleDescription) = drDtl(index)("ArticleDescription")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.EAN) = drDtl(index)("EAN")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Packageduom) = drDtl(index)("PackagedUOM")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Qty) = drDtl(index)("Qty")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Weight) = drDtl(index)("Weight")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.STRQty) = drDtl(index)("STRQty")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.StrExcludeCheck) = drDtl(index)("StrExcludeCheck")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.BaseUOM) = drDtl(index)("BaseUOM")
                    DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ItemQtyBaseUOM) = drDtl(index)("ItemQtyBaseUOM")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Binding the Controls with the DataSet
    ''' </summary>
    ''' <param name="StrArticle">ArticleCode</param>
    ''' <remarks></remarks>

    Private Sub getBinding()
        Try
            Dim flp As New FlowLayoutPanel
            'dsBulkCombo = objBulkCombo.GetBulkComboStru("0")
            'DvComboItemDetail = New DataView(dsBulkCombo.Tables("SoBulkComboDtl"), "", "BulkComboDetId ASC", DataViewRowState.CurrentRows)
            'DgBulkComboGrid.DataSource = DvComboItemDetail
            Call GridSettings(UpdateFlag)
        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Set the Grid Based on Flag
    ''' </summary>
    ''' <param name="Update">Update flag</param>
    ''' <remarks></remarks>
    ''' 

    Private Sub GridSettings(ByVal Update As Boolean)
        Try
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).Caption = " "
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).Width = 30
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).ComboList = "..."

            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).Width = 130
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).Caption = "Item Code"
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.DgBulkComboGrid.articlecode"))
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).AllowEditing = False

            DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).Width = 230
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).Caption = "Item Description"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).AllowEditing = False

            DgBulkComboGrid.Cols(DgBulkOrder.EAN).Width = 20
            DgBulkComboGrid.Cols(DgBulkOrder.EAN).Caption = "EAN"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            DgBulkComboGrid.Cols(DgBulkOrder.EAN).AllowEditing = False

            Dim DtUOM = objClsCommon.GetActiveUOMs()
            Dim UomList As String
            For index = 0 To DtUOM.Rows.Count - 1
                UomList = UomList & DtUOM(index)(0) & "|"
            Next index
            If UomList.Length > 0 Then
                UomList = UomList.Substring(0, UomList.Length - 1)
            End If

            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).Width = 83
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).Caption = "UOM"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).ComboList = UomList

            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Width = 90
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Caption = "Quantity"
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).DataType = Type.GetType("System.Decimal")
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Format = "0.000"
            ''IIf(resourceMgr Is Nothing, "Qty", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))

            DgBulkComboGrid.Cols(DgBulkOrder.Weight).Width = 90
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).Caption = "Weight" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).DataType = Type.GetType("System.Decimal")
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).Format = "0.000"

            DgBulkComboGrid.Cols(DgBulkOrder.STRQty).Width = 100
            DgBulkComboGrid.Cols(DgBulkOrder.STRQty).Caption = "STR Quantity"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.STRQty).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.STRQty).DataType = Type.GetType("System.Decimal")
            DgBulkComboGrid.Cols(DgBulkOrder.STRQty).Format = "0.000"

            DgBulkComboGrid.Cols(DgBulkOrder.StrExcludeCheck).Width = 120
            DgBulkComboGrid.Cols(DgBulkOrder.StrExcludeCheck).Caption = "Remove From STR"

            DgBulkComboGrid.Cols(DgBulkOrder.BaseUOM).Width = 70
            DgBulkComboGrid.Cols(DgBulkOrder.BaseUOM).Caption = "BaseUOM"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.BaseUOM).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.BaseUOM).ComboList = UomList

            DgBulkComboGrid.Cols(DgBulkOrder.ItemQtyBaseUOM).Width = 100
            DgBulkComboGrid.Cols(DgBulkOrder.ItemQtyBaseUOM).Caption = "ItemQtyBaseUOM"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.ItemQtyBaseUOM).AllowEditing = True


        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Detail Screen not intiallized properly ", "Error")
        End Try
    End Sub

#End Region

    Private Sub DgBulkComboGrid_CellButtonClick(sender As Object, e As RowColEventArgs) Handles DgBulkComboGrid.CellButtonClick
        Try
            Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId & " AND BulkComboDetId = " & DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BulkComboDetId))
            If drDtl.Count > 0 Then
                For Each Row As DataRow In drDtl
                    DtSoBulkComboDtl.Rows.Remove(Row)
                Next
            End If
            DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
            If (DgBulkComboGrid.Rows.Count > 1) Then
                DgBulkComboGrid.Select(DgBulkComboGrid.Rows.Count - 1, 2)
                If btnAddBulkCombo.Enabled Then
                    IsStrGenerateApplicable = True
                End If
            End If
            txtSearch.Focus()
            txtSearch.Select()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Function ValidateBulkOrderCombo() As Boolean
        ValidateBulkOrderCombo = False
        Try
            If DgBulkComboGrid.Rows.Count = 1 Then
                'MsgBox("Please add atleast one item ")
                ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                Exit Function
            End If
            If CboPakagingBox.Text.ToString.Trim.Length = 0 Or CboPakagingBox.SelectedIndex = -1 Then
                'MsgBox("Please Select Packaging Box ")
                ShowMessage(getValueByKey("BOC002"), "BOC002 - " & getValueByKey("CLAE04"))
                Exit Function
            End If
            Dim dtPackagingBox = objBulkCombo.GetPackagingBoxDataSet(clsDefaultConfiguration.PackagingBoxLastNodeCode)
            Dim pkgBoxName As String = CboPakagingBox.Text
            If dtPackagingBox.Rows.Count > 0 Then
                Dim drHdr() = dtPackagingBox.Select(CboPakagingBox.DisplayMember & "='" & pkgBoxName & "'")
                If drHdr.Count = 0 Then
                    'MsgBox("Please Select Packaging Box ")
                    ShowMessage(getValueByKey("BOC002"), "BOC002 - " & getValueByKey("CLAE04"))
                    Exit Function
                End If
            End If
            For index = 1 To DgBulkComboGrid.Rows.Count - 1
                If DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM) = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) Then
                    If Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)) <= 0 And _
                       Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)) <= 0 Then
                        'MsgBox("Please enter either Quantity or Weight at Row :" & index)
                        ShowMessage(getValueByKey("BOC003") & index, "BOC003 - " & getValueByKey("CLAE04"))
                        Exit Function
                    End If
                Else
                    If Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)) <= 0 Then
                        'MsgBox("Please enter both Quantity and Weight at Row :" & index)
                        ShowMessage(getValueByKey("BOC004") & index, "BOC004 - " & getValueByKey("CLAE04"))
                        Exit Function
                    End If
                End If
            Next index
            ValidateBulkOrderCombo = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub DgBulkComboGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles DgBulkComboGrid.AfterEdit
        Try
            If (e.Col = DgBulkOrder.Qty) Or (e.Col = DgBulkOrder.Weight) Or (e.Col = DgBulkOrder.Packageduom) Or (e.Col = DgBulkOrder.StrExcludeCheck) Then
                If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) Then
                    If e.Col = DgBulkOrder.Qty Then
                        If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) > 0 Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty)
                            If btnAddBulkCombo.Enabled Then
                                IsStrGenerateApplicable = True
                            End If
                        End If
                    Else
                        If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) > 0 Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) = 0
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)
                            If btnAddBulkCombo.Enabled Then
                                IsStrGenerateApplicable = True
                            End If
                        End If
                    End If
                Else
                    ' If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM).ToString.ToUpper = "GM" Or DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM).ToString.ToUpper = "KG" Then
                    DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)
                    If btnAddBulkCombo.Enabled Then
                        IsStrGenerateApplicable = True
                    End If


                    'Else
                    '    DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty)
                    'End If
                End If
            End If

            If e.Col = DgBulkOrder.StrExcludeCheck Then
                IsStrGenerateApplicable = True
            End If

        Catch ex As Exception
            ' ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(134, 134, 134)
        DgBulkComboGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        DgBulkComboGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        DgBulkComboGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        DgBulkComboGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        DgBulkComboGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgBulkComboGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgBulkComboGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgBulkComboGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        DgBulkComboGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)


        lblAddItems.ForeColor = Color.Black
        lblAddItems.AutoSize = False
        lblAddItems.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblAddItems.BorderStyle = BorderStyle.None
        lblAddItems.BackColor = Color.FromArgb(212, 212, 212)
        lblAddItems.SendToBack()
        lblAddItems.Size = New Size(162, 23)

        lblAdditionalComments.ForeColor = Color.Black
        lblAdditionalComments.AutoSize = False
        lblAdditionalComments.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblAdditionalComments.BorderStyle = BorderStyle.None
        lblAdditionalComments.BackColor = Color.FromArgb(212, 212, 212)
        lblAdditionalComments.SendToBack()
        lblAdditionalComments.Size = New Size(162, 23)

        lblPackagingBox.ForeColor = Color.Black
        lblPackagingBox.AutoSize = False
        lblPackagingBox.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblPackagingBox.BorderStyle = BorderStyle.None
        lblPackagingBox.BackColor = Color.FromArgb(212, 212, 212)
        lblPackagingBox.SendToBack()
        lblPackagingBox.Size = New Size(162, 22)
        lblPackagingBox.Location = New Point(29, 3)


        Me.btnPrint.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPrint.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnPrint.MaximumSize = New Size(0, 35)
        Me.btnPrint.MinimumSize = New Size(0, 35)
        Me.btnPrint.Size = New System.Drawing.Size(68, 35)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPrint.FlatStyle = FlatStyle.Flat
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnAddBulkCombo
        '
        Me.btnAddBulkCombo.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnAddBulkCombo.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnAddBulkCombo.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.btnAddBulkCombo.MaximumSize = New Size(0, 35)
        Me.btnAddBulkCombo.MinimumSize = New Size(0, 35)
        Me.btnAddBulkCombo.Size = New System.Drawing.Size(68, 35)
        Me.btnAddBulkCombo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddBulkCombo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddBulkCombo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnAddBulkCombo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnAddBulkCombo.FlatStyle = FlatStyle.Flat
        Me.btnAddBulkCombo.FlatAppearance.BorderSize = 0
        Me.btnAddBulkCombo.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnCancel
        '
        Me.btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.btnCancel.MaximumSize = New Size(0, 35)
        Me.btnCancel.MinimumSize = New Size(0, 35)
        Me.btnCancel.Size = New System.Drawing.Size(68, 35)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancel.FlatStyle = FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Return ""
    End Function

End Class