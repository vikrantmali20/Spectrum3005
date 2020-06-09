Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections
Imports SpectrumPrint
Public Class frmPCBulkOrderCombo
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
    Dim dtPackagingPrintBox As New DataTable
    Dim dtPackagingcopiedCombo As New DataTable
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim objPCSO As New clsSalesOrderPC
    Dim obCombo As New clsArticleCombo
    'Dim vTerminalID As String = clsAdmin.TerminalID
    'Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    'Dim vCurrencyCode As String = clsAdmin.CurrencyCode
    Dim vUserName As String = clsAdmin.UserName
    Dim dvRemarks As DataView
    Dim dvEditDeleteItems As DataView
    Dim obj1 As New frmPCSalesOrderCreation
    Dim Obj2 As New frmPCNSalesOrderUpdate
    Private _SoBulkComboHdr As DataTable
    Public Property DtSoBulkComboHdr() As DataTable
        Get
            Return _SoBulkComboHdr
        End Get
        Set(ByVal value As DataTable)
            _SoBulkComboHdr = value
        End Set
    End Property

    Private _DtSoBulkRemarks As DataTable
    Public Property DtSoBulkRemarks() As DataTable
        Get
            Return _DtSoBulkRemarks
        End Get
        Set(ByVal value As DataTable)
            _DtSoBulkRemarks = value
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
    '' added by nikhil
    Private _ArticleComboDtl As DataTable
    Public Property ArticleComboDtl() As DataTable
        Get
            Return _ArticleComboDtl
        End Get
        Set(ByVal value As DataTable)
            _ArticleComboDtl = value
        End Set
    End Property
    Private _IsChildComboEdit As Boolean = False    '' added by nikhil
    Public Property IsChildComboEdit() As Boolean
        Get
            Return _IsChildComboEdit
        End Get
        Set(ByVal value As Boolean)
            _IsChildComboEdit = value
        End Set
    End Property
    Dim dtLable As New DataTable


    Private _BulkComboMstId As Int64
    Public Property BulkComboMstId() As Int64
        Get
            Return _BulkComboMstId
        End Get
        Set(ByVal value As Int64)
            _BulkComboMstId = value
        End Set
    End Property
    Public _GrossAmt As Decimal = 0
    Public Property GrossAmt() As Decimal
        Get
            Return _GrossAmt
        End Get
        Set(value As Decimal)
            _GrossAmt = value
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
    Private _editedSrNo As Integer
    Public Property EditedSrNo() As Integer
        Get
            Return _editedSrNo
        End Get
        Set(ByVal value As Integer)
            _editedSrNo = value
        End Set
    End Property
    Private _displaySrNo As Integer
    Public Property displaySrNo() As Integer
        Get
            Return _displaySrNo
        End Get
        Set(ByVal value As Integer)
            _displaySrNo = value
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
    Private _IsEdit As Boolean
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property
    Private _ArticleCode As String
    Public Property ArticleCode() As String
        Get
            Return _ArticleCode
        End Get
        Set(ByVal value As String)
            _ArticleCode = value
        End Set
    End Property
    Private _remark As String
    Public Property remark() As String
        Get
            Return _remark
        End Get
        Set(ByVal value As String)
            _remark = value
        End Set
    End Property
    Private _SalesOrderNo As String
    Public Property SalesOrderNo() As String
        Get
            Return _SalesOrderNo
        End Get
        Set(ByVal value As String)
            _SalesOrderNo = value
        End Set
    End Property
    Private _IsCombo As Boolean
    Public Property IsCombo() As Boolean
        Get
            Return _IsCombo
        End Get
        Set(ByVal value As Boolean)
            _IsCombo = value
        End Set
    End Property
    Private _SingleArticleCodeo As String
    Public Property SingleArticleCode() As String
        Get
            Return _SingleArticleCodeo
        End Get
        Set(ByVal value As String)
            _SingleArticleCodeo = value
        End Set
    End Property
    Private _dtPackagingcopiedfrom As DataTable
    Public Property dtPackagingcopiedfrom() As DataTable
        Get
            Return _dtPackagingcopiedfrom
        End Get
        Set(ByVal value As DataTable)
            _dtPackagingcopiedfrom = value
        End Set
    End Property
    Private _CardNo As String
    Public Property CardNo() As String
        Get
            Return _CardNo
        End Get
        Set(ByVal value As String)
            _CardNo = value
        End Set
    End Property

    Enum DgBulkOrder
        Selects = 0
        Delete
        SrNo
        ArticleCode
        ArticleDescription
        'EAN
        'Packageduom
        Packageduom
        Qty
        Weight
        EAN
        BaseUOM
        IsSysQty
        Price
        Discount
        NetAmt 'vipin
        Tax
        TaxAmount
        GrossAmt 'vipin
    End Enum

    Public Function PopulateComboBox1(ByVal dtCombo As DataTable, ByRef ObjComboBox As ctrlCombo)

        ObjComboBox.DataSource = dtCombo
        ObjComboBox.ValueMember = dtCombo.Columns(0).ColumnName
        ObjComboBox.DisplayMember = dtCombo.Columns(1).ColumnName
        If dtCombo.Rows.Count > 0 Then
            ObjComboBox.SelectedIndex = 0
        End If


        Return ""

    End Function

    Private Sub frmBulkOrderCombo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '' added by nikhil
            'CtrlGross.Visible = False
            'ctrlNetValue.Visible = False
            'CtrlDiscount.Visible = False
            'ctrlNetValue.Visible = False
            'CtrlTaxAmt.Visible = False
            'txtTaxAmount.Visible = False
            rbnSingle.Enabled = True
            rbnCombo.Enabled = True
            '  txtRemarks.Multiline = True '##
            '  txtRemarks.MaxLength = 1000 '##
            txtRemarks.ScrollBars = ScrollBars.Both
            If DtSoBulkRemarks Is Nothing Then
                DtSoBulkRemarks = objPCSO.RmearksStructure()
                If Not (DtSoBulkRemarks Is Nothing) Then
                    DtSoBulkRemarks.Clear()
                End If
            End If
            If Not IsEdit Then
                rbnCombo.Checked = False
                rbnSingle.Checked = False
                Panel2.Visible = False
                Panel3.Visible = False
            End If
            '    dtPackagingPrintBox = objClsCommon.GetPackagingBox(clsAdmin.SiteCode, 2) '2=packaging box print  as
            If Not DtSoBulkComboHdr Is Nothing Then
                Dim DrComboHdr As DataRow()
                DrComboHdr = DtSoBulkComboHdr.Select("ComboSrNo='" + EditedSrNo.ToString() + "'")
                Dim dtPackagingPrintBox As DataTable
                If DrComboHdr.Length > 0 Then
                    Dim IFixedPrice As Integer = 0
                    If DrComboHdr(0)("IsFixedPrice") Then
                        IFixedPrice = 1
                    Else
                        IFixedPrice = 0
                    End If
                    dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, IFixedPrice) '2=packaging box print name
                Else
                    dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, 0)
                End If
                PopulateComboBox1(dtPackagingPrintBox, cboPrintName)
                pC1ComboSetDisplayMember(cboPrintName)
                cboPrintName.SelectedIndex = -1
            End If
            AddHandler txtAddArticle.KeyDown, AddressOf txtSearch_KeyDown

            Call getBinding()
            If DtSoBulkComboHdr IsNot Nothing Then


                If DtSoBulkComboHdr.Rows.Count > 0 Then

                    'dtPackagingcopiedfrom = objPCSO.CopyFromStructure()
                    'If dtPackagingcopiedfrom IsNot Nothing Then
                    '    dtPackagingcopiedfrom.Clear()

                    'End If
                    'For Each dr As DataRow In DtSoBulkComboHdr.Rows
                    '    Dim result As DataRow() = DtSoBulkComboDtl.Select("ComboSrNo='" + dr("ComboSrNo").ToString() + "' ")
                    '    Dim rowDeliveryAddr = dtPackagingcopiedfrom.NewRow()
                    '    rowDeliveryAddr("ComboSrNo") = dr("ComboSrNo").ToString()

                    '    Dim pkgboxprintname = DtSoBulkRemarks.Select("SrNo='" & dr("ComboSrNo").ToString() & "'")
                    '    If pkgboxprintname.Length > 0 Then
                    '        rowDeliveryAddr("PackagingBoxPrintName") = pkgboxprintname(0)("ArticleName")
                    '    Else
                    '        rowDeliveryAddr("PackagingBoxPrintName") = dr("ComboSrNo").ToString() & "-" & dr("PackagingBoxPrintName").ToString() & " (" & result.Length & ")"
                    '    End If

                    '    dtPackagingcopiedfrom.Rows.Add(rowDeliveryAddr)
                    'Next

                    'cboCopyFrom.DataSource = dtPackagingcopiedfrom
                    'cboCopyFrom.ValueMember = dtPackagingcopiedfrom.Columns("ComboSrNo").ColumnName
                    'cboCopyFrom.DisplayMember = dtPackagingcopiedfrom.Columns("PackagingBoxPrintName").ColumnName

                    cboCopyFrom.ClearItems()
                    Dim drPackagingcopiedfromTemp As DataRow() = dtPackagingcopiedfrom.Select("PackagingBoxPrintName like '%Snacks%'")
                    Dim dtPackagingcopiedfromTemp As DataTable
                    If drPackagingcopiedfromTemp.Length > 0 Then
                        dtPackagingcopiedfromTemp = drPackagingcopiedfromTemp.CopyToDataTable
                        cboCopyFrom.DataSource = dtPackagingcopiedfromTemp
                        cboCopyFrom.ValueMember = dtPackagingcopiedfromTemp.Columns("ComboSrNo").ColumnName
                        cboCopyFrom.DisplayMember = dtPackagingcopiedfromTemp.Columns("PackagingBoxPrintName").ColumnName
                    Else
                        cboCopyFrom.DataSource = Nothing
                        pC1ComboSetDisplayMember(cboCopyFrom)
                    End If

                    If DtSoBulkComboHdr.Rows.Count > 0 Then
                        ' cboCopyFrom.SelectedIndex = 1
                    End If

                    pC1ComboSetDisplayMember(cboCopyFrom)
                End If
            End If
            If IsEdit Then
                If IsCombo = False Then
                    rbnSingle.Checked = True
                    txtAddArticle.Text = ArticleCode
                    txtAddArticle.ReadOnly = True
                    txtRemarks.Text = remark
                    rbnCombo.Enabled = False
                Else
                    rbnCombo.Checked = True
                    cboCopyFrom.Enabled = False
                    LoadArticleData(False)
                    rbnSingle.Enabled = False
                End If


            End If

            '-----bind data to androidsearchtextbox 
            Dim dtBind = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, "")

            If dtBind.Rows.Count > 1 Then
                'Dim listSource As List(Of [String]) = (From row In dtBind
                '                        Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("ArticleName"))).Distinct().ToList()
                'txtAddArticle.lstNames = listSource
                Call SetWildSearchTextBox(dtBind, txtAddArticle, key:="ArticleCode", Value:="ArticleName", searchData:="ArticleCodeDesc")
                txtAddArticle.IsMovingControl = True
            End If
            If clsDefaultConfiguration.IsSavoy Then
                Me.Text = "Add Article"
                rbnSingle.Visible = False
                rbnCombo.Visible = False
                rbnSingle.Checked = True
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

            'If IsEdit = True Then
            '    DgBulkComboGrid.Cols("WtPerPiece(KG)").AllowEditing = False
            'Else
            '    DgBulkComboGrid.Cols("WtPerPiece(KG)").AllowEditing = True
            'End If
            DgBulkComboGrid.Cols("WtPerPiece(KG)").AllowEditing = True 'vipin

            If IsCombo = True Then
                Dim result As DataRow()
                result = DtSoBulkComboHdr.Select("ComboSrNo='" + EditedSrNo.ToString() + "'")
                If result.Length > 0 Then
                    CboFixed.Checked = result(0)("IsFixedPrice")
                End If

                If CboFixed.Checked = True Then
                    '  Panel4.Visible = True
                    TxtFixedPriceEnter.Visible = True
                Else
                    'Panel4.Visible = False
                    TxtFixedPriceEnter.Visible = False
                End If
            End If
            If rbnCombo.Checked = False And rbnSingle.Checked = False Then
                TxtFixedPriceEnter.Visible = False
                Panel4.Visible = False
            End If
            If IsEdit Then
                Panel4.Enabled = False
            Else
                Panel4.Enabled = True
            End If
            objCM_2.IsnewSalesOrder = True
            'vipin
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub LoadArticleData(ByVal isCopied As Boolean)
        Try
            Dim result As DataRow()
            DtSoBulkComboDtl = DtSoBulkComboDtl.Select("", "CreatedOn").CopyToDataTable

            'For i = 0 To DtSoBulkComboDtl.Rows.Count - 1
            'DtSoBulkComboDtl.Rows(i)("Discount") = 0
            '  Next

            If isCopied Then
                result = DtSoBulkComboHdr.Select("ComboSrNo='" + cboCopyFrom.SelectedValue.ToString + "'")
                dvEditDeleteItems = New DataView(DtSoBulkComboDtl, "ComboSrNo='" & cboCopyFrom.SelectedValue.ToString & "' AND Status=True", "", DataViewRowState.CurrentRows)
            Else
                result = DtSoBulkComboHdr.Select("ComboSrNo='" + EditedSrNo.ToString() + "'")
                dvEditDeleteItems = New DataView(DtSoBulkComboDtl, "ComboSrNo='" & EditedSrNo.ToString() & "' AND Status=True", "", DataViewRowState.CurrentRows)
            End If

            cboPrintName.SelectedValue = result(0)("PackagingBoxPrintName")
            txtRemarks.Text = result(0)("AdditionComments")



            Dim indexP As Integer = 1
            If dvEditDeleteItems.Count > 0 Then
                'GrdFixedCombo.Rows.RemoveRange(1, GrdFixedCombo.Rows.Count - 1)
                CboFixed.Checked = result(0)("IsFixedPrice")
                If CboFixed.Checked = True Then
                    GrdFixedCombo.Rows.RemoveRange(1, GrdFixedCombo.Rows.Count - 1)
                    For Each dr As DataRowView In dvEditDeleteItems
                        GrdFixedCombo.Rows.Add()

                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.SrNo) = indexP
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.ArticleCode) = dr("ArticleCode")
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.ArticleDescription) = dr("ArticleDescription")
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.EAN) = dr("EAN")
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.BaseUOM) = dr("BaseUOM")
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.Packageduom) = dr("Packageduom")
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.Qty) = dr("QTY")
                        'If dr("Packageduom") = "KGS" Then
                        '    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Weight) = Nothing

                        'Else
                        '    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Weight) = dr("Weight")
                        'End If
                        GrdFixedCombo.Rows(indexP)(DgBulkOrder.Weight) = dr("Weight")

                        If GrdFixedCombo.Rows(indexP)(DgBulkOrder.BaseUOM) = "KGS" Then
                            Dim dtsys = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, dr("ArticleCode"), clsAdmin.LangCode)
                            If dtsys.Rows.Count > 0 Then
                                Dim wtperpiece = dtsys.Rows(0)("Weight")
                                If wtperpiece > 0 Then
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.IsSysQty) = "True"
                                Else
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.IsSysQty) = "False"
                                End If

                            End If
                            '  
                        Else
                            GrdFixedCombo.Rows(indexP)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        ' added by nikhil
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            CtrlGross.Visible = True
                            ctrlNetValue.Visible = True 'vipn
                            CtrlDiscount.Visible = True
                            lblDiscount.Visible = True
                            ctrlNetValue.Visible = True
                            CtrlTaxAmt.Visible = True
                            txtTaxAmount.Visible = True
                            lblNetValue.Visible = True
                            Dim TotGross As Double
                            If DtSoBulkComboDtl.Columns.Contains("GrossAmt") Then
                                TotGross = If(DtSoBulkComboDtl.Compute("Sum(GrossAmt)", "") Is DBNull.Value, 0, DtSoBulkComboDtl.Compute("Sum(GrossAmt)", ""))
                                If Not ArticleComboDtl Is Nothing AndAlso ArticleComboDtl.Rows.Count > 0 Then   '' $$ added by nik
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = (ArticleComboDtl.Rows(0)("Discount") / ArticleComboDtl.Rows(0)("Quantity")) / DtSoBulkComboDtl.Rows.Count
                                    DtSoBulkComboDtl.Rows(indexP - 1)("Discount") = GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount)
                                Else
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = "0"

                                End If
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.Price) = dr("Price")
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.Tax) = dr("Tax")
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.TaxAmount) = dr("TaxAmount")
                                DtSoBulkComboDtl.Rows(indexP - 1)("TaxAmount") = GrdFixedCombo.Rows(indexP)(DgBulkOrder.TaxAmount)
                                If Not ArticleComboDtl Is Nothing AndAlso ArticleComboDtl.Rows.Count > 0 Then
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = (ArticleComboDtl.Rows(0)("Discount") / ArticleComboDtl.Rows(0)("Quantity")) / DtSoBulkComboDtl.Rows.Count
                                Else
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = 0
                                    '   GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = (ArticleComboDtl.Rows(0)("Discount") / ArticleComboDtl.Rows(0)("Quantity")) / DtSoBulkComboDtl.Rows.Count
                                End If
                            Else
                                If Not ArticleComboDtl Is Nothing AndAlso ArticleComboDtl.Rows.Count > 0 Then
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = (ArticleComboDtl.Rows(0)("Discount") / ArticleComboDtl.Rows(0)("Quantity")) / DtSoBulkComboDtl.Rows.Count
                                Else
                                    GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = 0
                                End If

                                ' GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount) = (ArticleComboDtl.Rows(0)("Discount") / ArticleComboDtl.Rows(0)("Quantity")) / DtSoBulkComboDtl.Rows.Count
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.Price) = dr("Price")
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.Tax) = dr("Tax")
                                GrdFixedCombo.Rows(indexP)(DgBulkOrder.TaxAmount) = dr("TaxAmount")
                            End If

                            GrdFixedCombo.Rows(indexP)(DgBulkOrder.NetAmt) = Math.Round((1 * dr("Price")) - GrdFixedCombo.Rows(indexP)(DgBulkOrder.Discount), 2) 'vipin
                            GrdFixedCombo.Rows(indexP)(DgBulkOrder.GrossAmt) = GrdFixedCombo.Rows(indexP)(DgBulkOrder.NetAmt) + GrdFixedCombo.Rows(indexP)(DgBulkOrder.TaxAmount)
                        End If
                        indexP = indexP + 1
                    Next
                Else
                    DgBulkComboGrid.Rows.RemoveRange(1, DgBulkComboGrid.Rows.Count - 1)
                    For Each dr As DataRowView In dvEditDeleteItems
                        DgBulkComboGrid.Rows.Add()

                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.SrNo) = indexP
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.ArticleCode) = dr("ArticleCode")
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.ArticleDescription) = dr("ArticleDescription")
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.EAN) = dr("EAN")
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.BaseUOM) = dr("BaseUOM")
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Packageduom) = dr("Packageduom")
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Qty) = dr("Qty")
                        'If dr("Packageduom") = "KGS" Then
                        '    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Weight) = Nothing

                        'Else
                        '    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Weight) = dr("Weight")
                        'End If
                        DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Weight) = dr("Weight")

                        If DgBulkComboGrid.Rows(indexP)(DgBulkOrder.BaseUOM) = "KGS" Then
                            Dim dtsys = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, dr("ArticleCode"), clsAdmin.LangCode)
                            If dtsys.Rows.Count > 0 Then
                                Dim wtperpiece = dtsys.Rows(0)("Weight")
                                If wtperpiece > 0 Then
                                    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.IsSysQty) = "True"
                                Else
                                    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.IsSysQty) = "False"
                                End If

                            End If
                            '  
                        Else
                            DgBulkComboGrid.Rows(indexP)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        ' added by nikhil
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            CtrlGross.Visible = True
                            ctrlNetValue.Visible = True 'vipn
                            CtrlDiscount.Visible = True
                            lblDiscount.Visible = True
                            ctrlNetValue.Visible = True
                            CtrlTaxAmt.Visible = True
                            txtTaxAmount.Visible = True
                            lblNetValue.Visible = True
                            Dim TotGross As Double
                            If DtSoBulkComboDtl.Columns.Contains("GrossAmt") Then
                                TotGross = If(DtSoBulkComboDtl.Compute("Sum(GrossAmt)", "") Is DBNull.Value, 0, DtSoBulkComboDtl.Compute("Sum(GrossAmt)", ""))
                                If Not ArticleComboDtl Is Nothing AndAlso ArticleComboDtl.Rows.Count > 0 Then   '' $$ added by nik
                                    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount) = ((dr("Qty") * dr("Price") / TotGross) * ArticleComboDtl.Rows(0)("Discount")) / ArticleComboDtl.Rows(0)("Quantity")   '  {(Qty per box* Price Per Piece)/ Net Value of combo}*Discount amount 
                                    DtSoBulkComboDtl.Rows(indexP - 1)("Discount") = DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount)   'vipin
                                Else
                                    DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount) = "0"

                                End If
                                ' DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount) = ((dr("Qty") * dr("Price") / TotGross) * ArticleComboDtl.Rows(0)("Discount")) / ArticleComboDtl.Rows(0)("Quantity")   '  {(Qty per box* Price Per Piece)/ Net Value of combo}*Discount amount 
                                'DtSoBulkComboDtl.Rows(indexP - 1)("Discount") = DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount)   'vipin
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Price) = dr("Price")
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Tax) = dr("Tax")
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.TaxAmount) = Math.Round((CDbl(dr("Price") * dr("Qty")) - (DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount))) * CDbl(dr("Tax")) / 100, 2)       ''''dr("TaxAmount")
                                DtSoBulkComboDtl.Rows(indexP - 1)("TaxAmount") = DgBulkComboGrid.Rows(indexP)(DgBulkOrder.TaxAmount)
                            Else
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount) = dr("Discount")
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Price) = dr("Price")
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Tax) = dr("Tax")
                                DgBulkComboGrid.Rows(indexP)(DgBulkOrder.TaxAmount) = dr("TaxAmount")
                            End If

                            DgBulkComboGrid.Rows(indexP)(DgBulkOrder.NetAmt) = Math.Round((dr("Qty") * dr("Price")) - DgBulkComboGrid.Rows(indexP)(DgBulkOrder.Discount), 2) 'vipin
                            DgBulkComboGrid.Rows(indexP)(DgBulkOrder.GrossAmt) = DgBulkComboGrid.Rows(indexP)(DgBulkOrder.NetAmt) + DgBulkComboGrid.Rows(indexP)(DgBulkOrder.TaxAmount)
                        End If
                        indexP = indexP + 1
                    Next
                End If
            End If
            '' added by nikhil 
            If clsDefaultConfiguration.IsNewSalesOrder Then
                If CboFixed.Checked = True Then
                    showValueFixed()
                Else
                    showValue()
                End If

                If isCopied Then
                    lblDiscount.Text = 0
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub showValue()

        'Dim Value1 = ArticleComboDtl.Compute("Sum(SellingPrice)", "").ToString
        'Dim value2 = ArticleComboDtl.Compute("Sum(Quantity)", "").ToString
        'Dim GrossAmt = (Value1 * value2).ToString
        'lblGross.Text = GrossAmt
        ' lblDiscount.Text = Math.Round(ArticleComboDtl.Compute("Sum(Discount)", ""), 2)
        'lblNetValue.Text = (GrossAmt - lblDiscount.Text).ToString

        'lblNetValue.Text = ArticleComboDtl.Rows(0)("NetAmount")   '' added on 20/07/17


        Dim grossAmt As Decimal = 0
        Dim NetAmt As Decimal = 0
        Dim Discount As Decimal = 0
        Dim TotTax As Decimal = 0
        Dim MaxTax As Decimal = 0
        For Each dr1 In DtSoBulkComboDtl.Rows
            '   NetAmt = NetAmt + (dr1("Price") * dr1("Qty")) - dr1("Discount")
            grossAmt = grossAmt + (dr1("Price") * dr1("Qty"))
            Discount = Discount + dr1("Discount")
            '    TotTax = TotTax + dr1("TaxAmount")
        Next

        'If DgBulkComboGrid.Rows.Count > 1 Then
        '    For i = 1 To DgBulkComboGrid.Rows.Count - 1
        '        grossAmt = grossAmt + Math.Round(DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty), 2)
        '        Discount = Discount + DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount)
        '    Next
        'End If

        'For i = 1 To DgBulkComboGrid.Rows.Count - 1   'aaded by vipin for max tax calculation
        '    If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
        '        TotTax = CDbl(TotTax)
        '        '  MaxTax = MaxTax
        '    Else
        '        MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
        '        TotTax = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
        '    End If
        'Next

        Dim preTaxPer As Decimal = 0
        Dim IsAllTaxClash As Boolean = True
        For i = 1 To DgBulkComboGrid.Rows.Count - 1
            If i <> 1 Then
                If preTaxPer <> DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                    IsAllTaxClash = False
                End If
            End If
            preTaxPer = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
        Next

        'If IsAllTaxClash = True Then

        '    Dim TaxAmt As Double = 0
        '    For i = 1 To DgBulkComboGrid.Rows.Count - 1
        '        If TaxAmt > DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) Then
        '            TaxAmt = TaxAmt
        '        Else
        '            TaxAmt = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
        '        End If
        '    Next
        '    txtTaxAmount.Text = Math.Round(TaxAmt, 2)
        'Else

        For i = 1 To DgBulkComboGrid.Rows.Count - 1
            If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                TotTax = CDbl(TotTax)
                '  MaxTax = MaxTax
            Else
                MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                TotTax = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
            End If
        Next
        '     End If
        'txtTaxAmount.Text = Math.Round(TotTax, 2)  ' Commented by vipin
        lblGross.Text = Math.Round(grossAmt, 2)
        lblDiscount.Text = Math.Round(Discount, 2)
        '  txtTaxAmount.Text = Math.Round(TotTax, 3)
        '    lblNetValue.Text = Math.Round(grossAmt - Discount + TotTax)
        ' lblNetValue.Text = Math.Round(grossAmt - Discount + Math.Round(CDbl(txtTaxAmount.Text), 2))
        txtTaxAmount.Text = Math.Round(((CDbl(grossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
        lblNetValue.Text = Math.Round(grossAmt + CDbl(txtTaxAmount.Text) - CDbl(lblDiscount.Text), 2) 'vipin
        CtrlTaxAmt.Text = "Tax Amt: (" + Math.Round(MaxTax).ToString + " %)"
        TxtFixedPriceEnter.Text = lblGross.Text
    End Sub
    Private Sub showValueFixed()


        Dim grossAmt As Decimal = 0
        Dim NetAmt As Decimal = 0
        Dim Discount As Decimal = 0
        Dim TotTax As Decimal = 0
        Dim MaxTax As Decimal = 0
        'For Each dr1 In GrdFixedCombo.Rows
        '    grossAmt = grossAmt + (dr1("Price") * 1)
        '    Discount = Discount + dr1("Discount")
        'Next
        If GrdFixedCombo.Rows.Count > 1 Then
            For i = 1 To GrdFixedCombo.Rows.Count - 1
                grossAmt = grossAmt + GrdFixedCombo.Rows(i)(DgBulkOrder.Price)
                Discount = Discount + GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)
            Next
        End If

        Dim preTaxPer As Decimal = 0
        Dim IsAllTaxClash As Boolean = True
        For i = 1 To GrdFixedCombo.Rows.Count - 1
            If i <> 1 Then
                If preTaxPer <> GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                    IsAllTaxClash = False
                End If
            End If
            preTaxPer = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
        Next


        For i = 1 To GrdFixedCombo.Rows.Count - 1
            If MaxTax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                TotTax = CDbl(TotTax)
                '  MaxTax = MaxTax
            Else
                MaxTax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                TotTax = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
            End If
        Next
        lblGross.Text = Math.Round(grossAmt)
        lblDiscount.Text = Math.Round(Discount, 2)
        txtTaxAmount.Text = Math.Round(((CDbl(Math.Round(grossAmt)) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
        lblNetValue.Text = Math.Round(Math.Round(grossAmt) + CDbl(txtTaxAmount.Text) - CDbl(lblDiscount.Text), 2) 'vipin
        CtrlTaxAmt.Text = "Tax Amt: (" + Math.Round(MaxTax).ToString + " %)"
        TxtFixedPriceEnter.Text = Math.Round(CDbl(lblGross.Text))
    End Sub

    Private Sub ClaculateFixedCombo()
        If TxtFixedPriceEnter.Text = "" Then
            TxtFixedPriceEnter.Text = 0
        End If
        'For Each dtDecsZero In DtSoBulkComboDtl.Rows  '## added for Discount zero Qty change
        '    If DtSoBulkComboDtl.Columns.Contains("Discount") Then '##
        '        dtDecsZero("Discount") = 0
        '    End If
        'Next
        If lblDiscount.Text <> "" AndAlso lblDiscount.Text > 0 Then
            If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then
            End If
        End If
        If GrdFixedCombo.Rows.Count > 1 Then
            For i = 1 To GrdFixedCombo.Rows.Count - 1
                GrdFixedCombo.Rows(i)(DgBulkOrder.Price) = Math.Round(CDbl(TxtFixedPriceEnter.Text) / (GrdFixedCombo.Rows.Count - 1), 2)
                GrdFixedCombo.Rows(i)("Discount") = "0"
                GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) = "0"
                Dim TaxAmt As Double = 0
                TaxAmt = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) / 100)
                GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount) = TaxAmt
                GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) = GrdFixedCombo.Rows(i)(DgBulkOrder.Price) - GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)
                GrdFixedCombo.Rows(i)(DgBulkOrder.GrossAmt) = GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) + GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount)
            Next
            showValueFixed()

            If CDbl(TxtFixedPriceEnter.Text) = 0 Then
                TxtFixedPriceEnter.Text = ""
            End If
        End If
        obj1._QtyChange = True
        Obj2._QtyChange = True

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

            If CboFixed.Checked = True Then
                If e.KeyCode = Keys.Delete AndAlso GrdFixedCombo.Rows.Count > 1 AndAlso txtAddArticle.Text.Length = 0 Then
                    GrdFixedCombo.Rows.Remove(GrdFixedCombo.Row)
                    If (GrdFixedCombo.Rows.Count > 1) Then
                        GrdFixedCombo.Select(1, 2)
                    End If
                    sender.Select()
                    sender.Focus()
                    Exit Sub
                End If
            Else
                If e.KeyCode = Keys.Delete AndAlso DgBulkComboGrid.Rows.Count > 1 AndAlso txtAddArticle.Text.Length = 0 Then
                    DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
                    If (DgBulkComboGrid.Rows.Count > 1) Then
                        DgBulkComboGrid.Select(1, 2)
                    End If
                    sender.Select()
                    sender.Focus()
                    Exit Sub
                End If
            End If


            If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then

                If rbnSingle.Checked Then
                    'txtSearch.Text = sender.Text.Trim
                    txtRemarks.Focus()
                    Exit Sub
                End If

                Dim dt As New DataTable
                dt = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, sender.Text.Trim, clsAdmin.LangCode, False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                If CboFixed.Checked = True Then
                    If GrdFixedCombo.Rows.Count > 1 Then
                        For index = 1 To GrdFixedCombo.Rows.Count - 1

                            If dt.Rows(0)("ArticleCode").ToString() = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleCode) Then
                                ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                                txtAddArticle.Text = ""
                                txtAddArticle.Select()
                                Exit Sub
                            End If

                        Next
                    End If
                Else
                    If DgBulkComboGrid.Rows.Count > 1 Then
                        For index = 1 To DgBulkComboGrid.Rows.Count - 1

                            If dt.Rows(0)("ArticleCode").ToString() = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode) Then
                                ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                                txtAddArticle.Text = ""
                                txtAddArticle.Select()
                                Exit Sub
                            End If

                        Next
                    End If
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
                    '---------Delete item then immedialty add item
                    Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + dt.Rows(0)("ArticleCode").ToString() + "' and ComboSrNo='" + EditedSrNo.ToString() + "' and status=false")
                    If result.Length > 0 Then
                        result(0)("Status") = True
                    End If
                    If CboVariable.Checked = True Then
                        DgBulkComboGrid.Rows.Add()

                        'If btnAddBulkCombo.Enabled Then
                        '    IsStrGenerateApplicable = True
                        'End If

                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Selects) = ""
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.SrNo) = DgBulkComboGrid.Rows.Count - 1
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()
                        If clsDefaultConfiguration.PrintItemFullName Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleName")
                        Else
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                        End If



                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("EAN").ToString()

                        'DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Packageduom) = dt.Rows(0)("BaseUnitofMeasure").ToString()

                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Qty) = Val(dt.Rows(0)("Qty"))
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BaseUOM) = dt.Rows(0)("BaseUnitofMeasure")
                        'If dt.Rows(0)("BaseUnitofMeasure").ToString().ToUpper() = "KGS" Then
                        '    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Nothing
                        'Else
                        '    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))
                        'End If
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))


                        If Val(dt.Rows(0)("Weight")) > 0 Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "True"
                        Else
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("Ean").ToString()
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Packageduom) = "" 'dt.Rows(0)("BaseUnitofMeasure")
                        'DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ItemQtyBaseUOM) = Val(dt.Rows(0)("Qty"))

                        '' added by nkhil
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            If Val(dt.Rows(0)("Weight")) > 0 Then 'vipin
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Price) = Val(dt.Rows(0)("Price"))
                            Else
                                If dt.Rows(0)("BaseUnitofMeasure") = "NOS" Then
                                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Price) = Val(dt.Rows(0)("Price"))
                                Else
                                    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Price) = Val(0)
                                End If
                            End If
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Discount) = Val(dt.Rows(0)("Discount"))
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.NetAmt) = (Val(dt.Rows(0)("Qty")) * Val(dt.Rows(0)("Price"))) - Val(dt.Rows(0)("Discount")) 'vipin
                            Dim dtTax As DataTable = obCombo.getTaxAmount(dt.Rows(0)("ArticleCode").ToString(), clsAdmin.SiteCode, "SO201")  ''Dim Tax As Double 
                            '     Dim Tax As Double = CDbl(dtTax.Compute("Sum(TaxValue)", ""))
                            Dim Tax As Double = CDbl(IIf(dtTax.Compute("Sum(TaxValue)", "") Is DBNull.Value, 0, dtTax.Compute("Sum(TaxValue)", ""))) 'vipin
                            If Convert.ToString(Tax) <> "" Then
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Tax) = Tax
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.TaxAmount) = (dt.Rows(0)("Price") * Val(dt.Rows(0)("Qty"))) - dt.Rows(0)("Discount")
                            End If
                        End If
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.GrossAmt) = DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.NetAmt) + DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.TaxAmount)  'vipin
                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.STRQty) = Val(dt.Rows(0)("STR_QTY"))
                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.StrExcludeCheck) = False
                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BulkComboDetId) = DgBulkComboGrid.Rows.Count - 1
                        sender.Text = String.Empty
                        sender.Focus()
                        sender.Select()

                        If (DgBulkComboGrid.Rows.Count > 1) Then
                            DgBulkComboGrid.Select(DgBulkComboGrid.Rows.Count - 1, 2)
                        End If
                    Else

                        GrdFixedCombo.Rows.Add()

                        'If btnAddBulkCombo.Enabled Then
                        '    IsStrGenerateApplicable = True
                        'End If

                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Selects) = ""
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.SrNo) = GrdFixedCombo.Rows.Count - 1
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()
                        If clsDefaultConfiguration.PrintItemFullName Then
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleName")
                        Else
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                        End If



                        ' GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("EAN").ToString()

                        'GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Packageduom) = dt.Rows(0)("BaseUnitofMeasure").ToString()

                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Qty) = Val(dt.Rows(0)("Qty"))
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.BaseUOM) = dt.Rows(0)("BaseUnitofMeasure")
                        'If dt.Rows(0)("BaseUnitofMeasure").ToString().ToUpper() = "KGS" Then
                        '    GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Weight) = Nothing
                        'Else
                        '    GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))
                        'End If
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Weight) = 0


                        If Val(dt.Rows(0)("Weight")) > 0 Then
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "True"
                        Else
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("Ean").ToString()
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Packageduom) = "" 'dt.Rows(0)("BaseUnitofMeasure")
                        'GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ItemQtyBaseUOM) = Val(dt.Rows(0)("Qty"))

                        '' added by nkhil
                        If TxtFixedPriceEnter.Text = "" Then
                            TxtFixedPriceEnter.Text = 0
                        End If
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            dt.Rows(0)("Price") = 0
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Price) = dt.Rows(0)("Price")
                            'If Val(dt.Rows(0)("Weight")) > 0 Then 'vipin
                            '    GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Price) = Val(dt.Rows(0)("Price"))
                            'Else
                            '    GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Price) = Val(0)
                            'End If
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Discount) = Val(dt.Rows(0)("Discount"))
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.NetAmt) = (Val(dt.Rows(0)("Qty")) * Val(dt.Rows(0)("Price"))) - Val(dt.Rows(0)("Discount")) 'vipin
                            Dim dtTax As DataTable = obCombo.getTaxAmount(dt.Rows(0)("ArticleCode").ToString(), clsAdmin.SiteCode, "SO201")  ''Dim Tax As Double 
                            '     Dim Tax As Double = CDbl(dtTax.Compute("Sum(TaxValue)", ""))
                            Dim Tax As Double = CDbl(IIf(dtTax.Compute("Sum(TaxValue)", "") Is DBNull.Value, 0, dtTax.Compute("Sum(TaxValue)", ""))) 'vipin
                            If Convert.ToString(Tax) <> "" Then
                                GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Tax) = Tax
                                GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.TaxAmount) = (dt.Rows(0)("Price") * Val(dt.Rows(0)("Qty"))) - dt.Rows(0)("Discount")
                            End If
                        End If
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.GrossAmt) = GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.NetAmt) + GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.TaxAmount)  'vipin
                        sender.Text = String.Empty
                        sender.Focus()
                        sender.Select()

                        If (GrdFixedCombo.Rows.Count > 1) Then
                            GrdFixedCombo.Select(GrdFixedCombo.Rows.Count - 1, 2)
                        End If
                        TxtFixedPriceEnter.Enabled = True
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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
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

            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0

            'If e.KeyCode = Keys.Delete AndAlso DgBulkComboGrid.Rows.Count > 1 AndAlso txtAddArticle.Text.Length = 0 Then
            '    DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
            '    If (DgBulkComboGrid.Rows.Count > 1) Then
            '        DgBulkComboGrid.Select(1, 2)
            '    End If
            '    sender.Select()
            '    sender.Focus()
            '    Exit Sub
            'End If

            ' If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
            If (txtAddArticle.Text.Trim() <> String.Empty) Then
                Dim dt As New DataTable
                txtAddArticle.Text = txtAddArticle.Text.ToString().Split(" ")(0)
                dt = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, txtAddArticle.Text.Trim(), clsAdmin.LangCode, False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                If dt.Rows.Count = 0 Then
                    ShowMessage("Article does not exist", "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If CboFixed.Checked = True Then
                    If GrdFixedCombo.Rows.Count > 1 Then
                        For index = 1 To GrdFixedCombo.Rows.Count - 1

                            If dt.Rows(0)("ArticleCode").ToString() = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleCode) Then
                                ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                                Exit Sub
                            End If
                        Next
                    End If
                Else
                    If DgBulkComboGrid.Rows.Count > 1 Then
                        For index = 1 To DgBulkComboGrid.Rows.Count - 1

                            If dt.Rows(0)("ArticleCode").ToString() = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode) Then
                                ShowMessage(getValueByKey("CLIST06"), "CLIST06 - " & getValueByKey("CLIST06"))
                                Exit Sub
                            End If

                        Next
                    End If
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

                    '---------Delete item then immedialty add item
                    Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + dt.Rows(0)("ArticleCode").ToString() + "' and ComboSrNo='" + EditedSrNo.ToString() + "' and status=false")
                    If result.Length > 0 Then
                        result(0)("Status") = True
                    End If

                    If CboFixed.Checked = True Then
                        GrdFixedCombo.Rows.Add()

                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Selects) = ""
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.SrNo) = GrdFixedCombo.Rows.Count - 1
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()
                        If clsDefaultConfiguration.PrintItemFullName Then
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleName")
                        Else
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                        End If

                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Qty) = Val(dt.Rows(0)("Qty"))
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.BaseUOM) = dt.Rows(0)("BaseUnitofMeasure")

                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))
                        If Val(dt.Rows(0)("Weight")) > 0 Then
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "True"
                        Else
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("Ean").ToString()
                        GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Packageduom) = "" 'dt.Rows(0)("BaseUnitofMeasure")
                        '' added by nikhil
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Price) = dt.Rows(0)("Price")
                            GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Discount) = dt.Rows(0)("Discount")

                            Dim dtTax As DataTable = obCombo.getTaxAmount(dt.Rows(0)("ArticleCode").ToString(), clsAdmin.SiteCode, "SO201")  ''Dim Tax As Double 
                            Dim Tax As Double = IIf((dtTax.Compute("Sum(TaxValue)", "") Is DBNull.Value), 0, dtTax.Compute("Sum(TaxValue)", ""))
                            If Convert.ToString(Tax) <> "" Then
                                GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.Tax) = Tax
                                GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.TaxAmount) = (dt.Rows(0)("Price") * Val(dt.Rows(0)("Qty"))) - dt.Rows(0)("Discount")
                            End If

                        End If

                        If (GrdFixedCombo.Rows.Count > 1) Then
                            GrdFixedCombo.Select(GrdFixedCombo.Rows.Count - 1, 2)
                        End If
                        TxtFixedPriceEnter.Enabled = True
                    Else
                        DgBulkComboGrid.Rows.Add()

                        'If btnAddBulkCombo.Enabled Then
                        '    IsStrGenerateApplicable = True
                        'End If

                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Selects) = ""
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.SrNo) = DgBulkComboGrid.Rows.Count - 1
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()
                        If clsDefaultConfiguration.PrintItemFullName Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleName")
                        Else
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                        End If



                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("EAN").ToString()

                        'DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Packageduom) = dt.Rows(0)("BaseUnitofMeasure").ToString()

                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Qty) = Val(dt.Rows(0)("Qty"))
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BaseUOM) = dt.Rows(0)("BaseUnitofMeasure")

                        'If dt.Rows(0)("BaseUnitofMeasure").ToString().ToUpper() = "KGS" Then
                        '    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Nothing
                        'Else
                        '    DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))
                        'End If
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Weight) = Val(dt.Rows(0)("Weight"))
                        If Val(dt.Rows(0)("Weight")) > 0 Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "True"
                        Else
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                        End If
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.EAN) = dt.Rows(0)("Ean").ToString()
                        DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Packageduom) = "" 'dt.Rows(0)("BaseUnitofMeasure")
                        '' added by nikhil
                        If clsDefaultConfiguration.IsNewSalesOrder Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Price) = dt.Rows(0)("Price")
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Discount) = dt.Rows(0)("Discount")

                            Dim dtTax As DataTable = obCombo.getTaxAmount(dt.Rows(0)("ArticleCode").ToString(), clsAdmin.SiteCode, "SO201")  ''Dim Tax As Double 
                            Dim Tax As Double = IIf((dtTax.Compute("Sum(TaxValue)", "") Is DBNull.Value), 0, dtTax.Compute("Sum(TaxValue)", ""))
                            If Convert.ToString(Tax) <> "" Then
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.Tax) = Tax
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.TaxAmount) = (dt.Rows(0)("Price") * Val(dt.Rows(0)("Qty"))) - dt.Rows(0)("Discount")
                            End If

                        End If
                        'DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.ItemQtyBaseUOM) = Val(dt.Rows(0)("Qty"))



                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.STRQty) = Val(dt.Rows(0)("STR_QTY"))
                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.StrExcludeCheck) = False
                        ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.BulkComboDetId) = DgBulkComboGrid.Rows.Count - 1
                        'sender.Text = String.Empty
                        'sender.Focus()
                        'sender.Select()

                        If (DgBulkComboGrid.Rows.Count > 1) Then
                            DgBulkComboGrid.Select(DgBulkComboGrid.Rows.Count - 1, 2)
                        End If
                    End If
                End If
            End If
            If CboFixed.Checked = True Then
                If GrdFixedCombo.Rows.Count = 1 AndAlso rbnCombo.Checked Then
                    ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Else
                If DgBulkComboGrid.Rows.Count = 1 AndAlso rbnCombo.Checked Then
                    'MsgBox("Please add atleast one item ")
                    ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If
            txtAddArticle.Text = ""
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
            ' Me.Dispose() '' commented by nikhil for crashing window
            IsEdit = False
            '' added by ketan 
            obj1._QtyChange = False
            Obj2._QtyChange = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#Region "Function"

    Private Sub FillData()
        Try
            ''BulkComboMstId = DtSoBulkComboHdr.Rows(0)("BulkComboMstId")
            'Dim drHdr() = DtSoBulkComboHdr.Select("ComboSrNo=" & ComboSrNo)
            'If drHdr.Count > 0 Then
            '    CboPakagingBox.SelectedValue = drHdr(0)("PackagingBoxCode")
            '    txtAdditionalComments.Text = drHdr(0)("AdditionComments")
            'End If

            'Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId)
            'If drDtl.Count > 0 Then
            '    For index = 0 To drDtl.Count - 1
            '        DgBulkComboGrid.Rows.Add()
            '        Dim dtRow = DgBulkComboGrid.Rows.Count - 1
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.BulkComboDetId) = drDtl(index)("BulkComboDetId")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ArticleCode) = drDtl(index)("ArticleCode")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ArticleDescription) = drDtl(index)("ArticleDescription")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.EAN) = drDtl(index)("EAN")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Packageduom) = drDtl(index)("PackagedUOM")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Qty) = drDtl(index)("Qty")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.Weight) = drDtl(index)("Weight")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.STRQty) = drDtl(index)("STRQty")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.StrExcludeCheck) = drDtl(index)("StrExcludeCheck")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.BaseUOM) = drDtl(index)("BaseUOM")
            '        DgBulkComboGrid.Rows(dtRow)(DgBulkOrder.ItemQtyBaseUOM) = drDtl(index)("ItemQtyBaseUOM")
            '    Next
            'End If
        Catch ex As Exception
            LogException(ex)
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
            Call GridSettingsCombo(UpdateFlag) 'vipin
            DgBulkComboGrid.AllowEditing = True
            GrdFixedCombo.AllowEditing = True
        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Set the Grid Based on Flag
    ''' </summary>
    ''' <param name="Update">Update flag</param>admin
    ''' <remarks></remarks>
    ''' 

    Private Sub GridSettings(ByVal Update As Boolean)
        Try
            DgBulkComboGrid.Rows.MinSize = 28

            DgBulkComboGrid.Cols(DgBulkOrder.Selects).Caption = " "
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).Width = 1
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.Selects).Caption = " "

            '  DgBulkComboGrid.Cols(DgBulkOrder.Delete).Width = 20
            DgBulkComboGrid.Cols(DgBulkOrder.Delete).ComboList = "..."
            DgBulkComboGrid.Cols(DgBulkOrder.Delete).AllowEditing = True

            '   DgBulkComboGrid.Cols(DgBulkOrder.SrNo).Width = 50
            DgBulkComboGrid.Cols(DgBulkOrder.SrNo).Caption = "Sr.No."
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.DgBulkComboGrid.articlecode"))
            DgBulkComboGrid.Cols(DgBulkOrder.SrNo).AllowEditing = False

            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).Width = 70
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).Caption = "Code"
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.DgBulkComboGrid.articlecode"))
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleCode).AllowEditing = False

            '   DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).Width = 320
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).Caption = "Article Description"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            DgBulkComboGrid.Cols(DgBulkOrder.ArticleDescription).AllowEditing = False



            Dim DtUOM = objClsCommon.GetActiveUOMs()
            Dim UomList As String
            For index = 0 To DtUOM.Rows.Count - 1
                UomList = UomList & DtUOM(index)(0) & "|"
            Next index
            If UomList.Length > 0 Then
                UomList = UomList.Substring(0, UomList.Length - 1)
            End If



            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Width = 100
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Caption = "Qty.Per Box"
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).DataType = Type.GetType("System.Decimal")
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).Format = "0.000"
            DgBulkComboGrid.Cols(DgBulkOrder.Qty).AllowEditing = True
            ''IIf(resourceMgr Is Nothing, "Qty", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).Width = 60
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).Caption = "UOM"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.DgBulkComboGrid.quantity"))
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.Packageduom).ComboList = UomList

            '  DgBulkComboGrid.Cols(DgBulkOrder.Weight).Width = 80
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).Caption = "Wt.Per Piece(KG)" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).AllowEditing = True
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).DataType = Type.GetType("System.Decimal")
            DgBulkComboGrid.Cols(DgBulkOrder.Weight).Format = "0.000"

            '  DgBulkComboGrid.Cols(DgBulkOrder.EAN).Width = 20
            DgBulkComboGrid.Cols(DgBulkOrder.EAN).Caption = "EAN"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            DgBulkComboGrid.Cols(DgBulkOrder.EAN).AllowEditing = False
            DgBulkComboGrid.Cols(DgBulkOrder.EAN).Visible = False

            DgBulkComboGrid.Cols(DgBulkOrder.BaseUOM).Visible = False

            '    DgBulkComboGrid.Cols(DgBulkOrder.IsSysQty).Width = 20
            DgBulkComboGrid.Cols(DgBulkOrder.IsSysQty).Caption = "IsSys"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            DgBulkComboGrid.Cols(DgBulkOrder.IsSysQty).AllowEditing = False
            DgBulkComboGrid.Cols(DgBulkOrder.IsSysQty).Visible = False



            '' added by nikhil
            If clsDefaultConfiguration.IsNewSalesOrder Then


                DgBulkComboGrid.Cols(DgBulkOrder.Price).Width = 150
                DgBulkComboGrid.Cols(DgBulkOrder.Price).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.Price).TextAlign = TextAlignEnum.LeftCenter
                DgBulkComboGrid.Cols(DgBulkOrder.Price).Caption = "Price Per Article(Rs.)" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
                If IsChildComboEdit = True Then   '' $$ added by nikhil
                    DgBulkComboGrid.Cols(DgBulkOrder.Price).AllowEditing = True
                Else
                    DgBulkComboGrid.Cols(DgBulkOrder.Price).AllowEditing = True
                End If
                DgBulkComboGrid.Cols(DgBulkOrder.Price).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.Price).Format = "0.00"


                '    DgBulkComboGrid.Cols(DgBulkOrder.Discount).Width = 10
                DgBulkComboGrid.Cols(DgBulkOrder.Discount).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.Discount).Caption = "Discount" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
                DgBulkComboGrid.Cols(DgBulkOrder.Discount).AllowEditing = False
                DgBulkComboGrid.Cols(DgBulkOrder.Discount).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.Discount).Format = "0.00"

                '   ---------------------------------------'vipin --------------------------
                DgBulkComboGrid.Cols(DgBulkOrder.NetAmt).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.NetAmt).Caption = "Net Amt"
                DgBulkComboGrid.Cols(DgBulkOrder.NetAmt).AllowEditing = False
                DgBulkComboGrid.Cols(DgBulkOrder.NetAmt).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.NetAmt).Format = "0.00"

                DgBulkComboGrid.Cols(DgBulkOrder.GrossAmt).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.GrossAmt).Caption = "Gross Amt"
                DgBulkComboGrid.Cols(DgBulkOrder.GrossAmt).AllowEditing = False
                DgBulkComboGrid.Cols(DgBulkOrder.GrossAmt).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.GrossAmt).Format = "0.00"
                '   ---------------------------------------------------------------------------

                'DgBulkComboGrid.Cols(DgBulkOrder.Tax).Width = 10ss
                DgBulkComboGrid.Cols(DgBulkOrder.Tax).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.Tax).Caption = "Tax %"
                DgBulkComboGrid.Cols(DgBulkOrder.Tax).AllowEditing = False
                DgBulkComboGrid.Cols(DgBulkOrder.Tax).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.Tax).Format = "0.00"

                'DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).Width = 10
                DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).Visible = True
                DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).Caption = "Tax Amount"
                DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).AllowEditing = False
                DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).DataType = Type.GetType("System.Decimal")
                DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).Format = "0.00"

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Detail Screen not intiallized properly ", "Error")
        End Try
    End Sub

    Private Sub GridSettingsCombo(ByVal Update As Boolean)
        Try
            GrdFixedCombo.Rows.MinSize = 28

            GrdFixedCombo.Cols(DgBulkOrder.Selects).Caption = " "
            GrdFixedCombo.Cols(DgBulkOrder.Selects).Width = 1
            GrdFixedCombo.Cols(DgBulkOrder.Selects).AllowEditing = True
            GrdFixedCombo.Cols(DgBulkOrder.Selects).Caption = " "

            '  GrdFixedCombo.Cols(DgBulkOrder.Delete).Width = 20
            GrdFixedCombo.Cols(DgBulkOrder.Delete).ComboList = "..."
            GrdFixedCombo.Cols(DgBulkOrder.Delete).AllowEditing = True

            '   GrdFixedCombo.Cols(DgBulkOrder.SrNo).Width = 50
            GrdFixedCombo.Cols(DgBulkOrder.SrNo).Caption = "Sr.No."
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.GrdFixedCombo.articlecode"))
            GrdFixedCombo.Cols(DgBulkOrder.SrNo).AllowEditing = False

            GrdFixedCombo.Cols(DgBulkOrder.ArticleCode).Width = 70
            GrdFixedCombo.Cols(DgBulkOrder.ArticleCode).Caption = "Code"
            ''IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.GrdFixedCombo.articlecode"))
            GrdFixedCombo.Cols(DgBulkOrder.ArticleCode).AllowEditing = False

            '   GrdFixedCombo.Cols(DgBulkOrder.ArticleDescription).Width = 320
            GrdFixedCombo.Cols(DgBulkOrder.ArticleDescription).Caption = "Article Description"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.GrdFixedCombo.discription"))
            GrdFixedCombo.Cols(DgBulkOrder.ArticleDescription).AllowEditing = False



            Dim DtUOM = objClsCommon.GetActiveUOMs()
            Dim UomList As String
            For index = 0 To DtUOM.Rows.Count - 1
                UomList = UomList & DtUOM(index)(0) & "|"
            Next index
            If UomList.Length > 0 Then
                UomList = UomList.Substring(0, UomList.Length - 1)
            End If



            GrdFixedCombo.Cols(DgBulkOrder.Qty).Width = 100
            GrdFixedCombo.Cols(DgBulkOrder.Qty).Caption = "Qty.Per Box"
            GrdFixedCombo.Cols(DgBulkOrder.Qty).DataType = Type.GetType("System.Decimal")
            GrdFixedCombo.Cols(DgBulkOrder.Qty).Format = "0.000"
            GrdFixedCombo.Cols(DgBulkOrder.Qty).AllowEditing = True
            ''IIf(resourceMgr Is Nothing, "Qty", getValueByKey("frmcashmemo.GrdFixedCombo.quantity"))
            GrdFixedCombo.Cols(DgBulkOrder.Packageduom).Width = 60
            GrdFixedCombo.Cols(DgBulkOrder.Packageduom).Caption = "UOM"
            '// IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.GrdFixedCombo.quantity"))
            GrdFixedCombo.Cols(DgBulkOrder.Packageduom).AllowEditing = True
            GrdFixedCombo.Cols(DgBulkOrder.Packageduom).ComboList = UomList

            '  GrdFixedCombo.Cols(DgBulkOrder.Weight).Width = 80
            GrdFixedCombo.Cols(DgBulkOrder.Weight).Caption = "Wt.Per Piece(KG)" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.GrdFixedCombo.sellingprice"))
            GrdFixedCombo.Cols(DgBulkOrder.Weight).AllowEditing = False
            GrdFixedCombo.Cols(DgBulkOrder.Weight).DataType = Type.GetType("System.Decimal")
            GrdFixedCombo.Cols(DgBulkOrder.Weight).Format = "0.000"

            '  DgBulkComboGrid.Cols(DgBulkOrder.EAN).Width = 20
            GrdFixedCombo.Cols(DgBulkOrder.EAN).Caption = "EAN"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            GrdFixedCombo.Cols(DgBulkOrder.EAN).AllowEditing = False
            GrdFixedCombo.Cols(DgBulkOrder.EAN).Visible = False

            GrdFixedCombo.Cols(DgBulkOrder.BaseUOM).Visible = False

            '    DgBulkComboGrid.Cols(DgBulkOrder.IsSysQty).Width = 20
            GrdFixedCombo.Cols(DgBulkOrder.IsSysQty).Caption = "IsSys"
            'IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.DgBulkComboGrid.discription"))
            GrdFixedCombo.Cols(DgBulkOrder.IsSysQty).AllowEditing = False
            GrdFixedCombo.Cols(DgBulkOrder.IsSysQty).Visible = False



            '' added by nikhil
            If clsDefaultConfiguration.IsNewSalesOrder Then


                GrdFixedCombo.Cols(DgBulkOrder.Price).Width = 150
                GrdFixedCombo.Cols(DgBulkOrder.Price).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.Price).TextAlign = TextAlignEnum.LeftCenter
                GrdFixedCombo.Cols(DgBulkOrder.Price).Caption = "Price Per Article(Rs.)" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
                If IsChildComboEdit = True Then   '' $$ added by nikhil
                    GrdFixedCombo.Cols(DgBulkOrder.Price).AllowEditing = False
                Else
                    GrdFixedCombo.Cols(DgBulkOrder.Price).AllowEditing = False
                End If
                GrdFixedCombo.Cols(DgBulkOrder.Price).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.Price).Format = "0.00"


                '    DgBulkComboGrid.Cols(DgBulkOrder.Discount).Width = 10
                GrdFixedCombo.Cols(DgBulkOrder.Discount).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.Discount).Caption = "Discount" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.DgBulkComboGrid.sellingprice"))
                GrdFixedCombo.Cols(DgBulkOrder.Discount).AllowEditing = False
                GrdFixedCombo.Cols(DgBulkOrder.Discount).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.Discount).Format = "0.00"

                '   ---------------------------------------'vipin --------------------------
                GrdFixedCombo.Cols(DgBulkOrder.NetAmt).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.NetAmt).Caption = "Net Amt"
                GrdFixedCombo.Cols(DgBulkOrder.NetAmt).AllowEditing = False
                GrdFixedCombo.Cols(DgBulkOrder.NetAmt).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.NetAmt).Format = "0.00"

                GrdFixedCombo.Cols(DgBulkOrder.GrossAmt).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.GrossAmt).Caption = "Gross Amt"
                GrdFixedCombo.Cols(DgBulkOrder.GrossAmt).AllowEditing = False
                GrdFixedCombo.Cols(DgBulkOrder.GrossAmt).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.GrossAmt).Format = "0.00"
                '   ---------------------------------------------------------------------------

                'DgBulkComboGrid.Cols(DgBulkOrder.Tax).Width = 10ss
                GrdFixedCombo.Cols(DgBulkOrder.Tax).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.Tax).Caption = "Tax %"
                GrdFixedCombo.Cols(DgBulkOrder.Tax).AllowEditing = False
                GrdFixedCombo.Cols(DgBulkOrder.Tax).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.Tax).Format = "0.00"

                'DgBulkComboGrid.Cols(DgBulkOrder.TaxAmount).Width = 10
                GrdFixedCombo.Cols(DgBulkOrder.TaxAmount).Visible = True
                GrdFixedCombo.Cols(DgBulkOrder.TaxAmount).Caption = "Tax Amount"
                GrdFixedCombo.Cols(DgBulkOrder.TaxAmount).AllowEditing = False
                GrdFixedCombo.Cols(DgBulkOrder.TaxAmount).DataType = Type.GetType("System.Decimal")
                GrdFixedCombo.Cols(DgBulkOrder.TaxAmount).Format = "0.00"

            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Detail Screen not intiallized properly ", "Error")
        End Try
    End Sub

#End Region

    Private Function ValidateBulkOrderCombo() As Boolean
        ValidateBulkOrderCombo = False
        Try
            If rbnCombo.Checked Then
                If cboPrintName.SelectedValue Is Nothing Then
                    'MsgBox("Please add atleast one item ")
                    ShowMessage("Please select Print Name", "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Function
                End If
            End If

            If CboFixed.Checked = True Then
                If GrdFixedCombo.Rows.Count = 1 AndAlso rbnCombo.Checked Then
                    'MsgBox("Please add atleast one item ")
                    ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Function
                End If
            Else
                If DgBulkComboGrid.Rows.Count = 1 AndAlso rbnCombo.Checked Then
                    'MsgBox("Please add atleast one item ")
                    ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Function
                End If
            End If
            If rbnSingle.Checked Then
                'MsgBox("Please add atleast one item ")
                If txtAddArticle.Text.Trim = String.Empty Then
                    ShowMessage(getValueByKey("BOC001"), "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Function
                End If

                Dim dt As New DataTable
                dt = objCM_2.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, txtAddArticle.Text.Trim, clsAdmin.LangCode, False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                If dt.Rows.Count = 0 Then
                    ShowMessage("Article does not exist", "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Function
                Else
                    SingleArticleCode = dt.Rows(0)("ArticleCode").ToString()
                    updateRemarks(dt.Rows(0)("ArticleCode").ToString(), dt.Rows(0)("ArticleShortName").ToString())
                    IsCombo = False
                End If
            Else
                If CboFixed.Checked = True Then
                    For index = 1 To GrdFixedCombo.Rows.Count - 1
                        Dim qty As Decimal = GrdFixedCombo.Rows(index)(DgBulkOrder.Qty)
                        If GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) = "NOS" Then
                            Dim OrderQty = qty.ToString.Split(".")
                            If OrderQty.Length = 2 Then
                                If OrderQty(1) > 0 Then
                                    ShowMessage("Article Qty should not be in decimal", "BOC001 - " & getValueByKey("CLAE04"))
                                    Exit Function
                                End If
                            End If
                        End If
                        If qty <= 0 Then
                            ShowMessage("Article Qty Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                            Exit Function
                        End If
                        ' 

                        Dim weight As Decimal = GrdFixedCombo.Rows(index)(DgBulkOrder.Weight)
                        'If GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) <> "NOS" AndAlso GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) <> "KGS" Then
                        '    If weight <= 0 Then 'AndAlso DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) <> "KGS" AndAlso DgBulkComboGrid.Rows(index)(DgBulkOrder.IsSysQty) = "False"
                        '        ShowMessage("Weight Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                        '        Exit Function
                        '    End If

                        'Else
                        '    If GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) <> GrdFixedCombo.Rows(index)(DgBulkOrder.BaseUOM) Then
                        '        If GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) = "NOS" Then
                        '            If GrdFixedCombo.Rows(index)(DgBulkOrder.IsSysQty) = False Then
                        '                If weight = 0 Then
                        '                    If clsDefaultConfiguration.ClientForMail = "PC" Then
                        '                    Else
                        '                        ShowMessage("Weight Can Not be less than zero", "BOC001 - " & getValueByKey("CLAE04"))
                        '                        Exit Function
                        '                    End If


                        '                End If
                        '            End If
                        '        End If

                        '    ElseIf GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom) = "KGS" Then

                        '        Dim weightperbox = weight.ToString.Split(".")
                        '        If weightperbox.Length = 2 Then
                        '            If weightperbox(1) < 0 Then
                        '                ShowMessage("Weight Can Not be less than zero", "BOC001 - " & getValueByKey("CLAE04"))
                        '                Exit Function
                        '            End If
                        '        End If
                        '        If weight = 0 Then
                        '            If qty <= 0 Then
                        '                ShowMessage("Article Qty Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                        '                Exit Function
                        '            End If
                        '        End If

                        '    End If
                        'End If
                        '' added by ketan Validation if net amount is zero as per reqiurment from Manish and Varified by DilipKumar
                        Dim Netamount As Decimal = GrdFixedCombo.Rows(index)(DgBulkOrder.NetAmt)
                        If Netamount = 0 Then
                            ShowMessage("Net Amount Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                            Exit Function
                        End If
                        Dim BaseUOM As String = GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom)
                        If BaseUOM = "" Then
                            ShowMessage("Select article UOM first", "BOC001 - " & getValueByKey("CLAE04"))
                            GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Qty) = 0
                            Exit Function
                        End If
                    Next
                Else
                    For index = 1 To DgBulkComboGrid.Rows.Count - 1
                        Dim qty As Decimal = DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)
                        If DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) = "NOS" Then

                            Dim OrderQty = qty.ToString.Split(".")
                            If OrderQty.Length = 2 Then
                                If OrderQty(1) > 0 Then
                                    ShowMessage("Article Qty should not be in decimal", "BOC001 - " & getValueByKey("CLAE04"))
                                    Exit Function
                                End If

                            End If
                            If qty <= 0 Then
                                ShowMessage("Article Qty Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                                Exit Function
                            End If
                        End If

                        Dim weight As Decimal = DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)
                        'If DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) = "KGS" AndAlso weight <> 0 Then
                        '    If qty >= 0 Then
                        '        ShowMessage("Please select UOM other than KGS", "BOC001 - " & getValueByKey("CLAE04"))
                        '        Exit Function
                        '    End If
                        'End If
                        If DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) <> "NOS" AndAlso DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) <> "KGS" Then
                            If weight <= 0 Then 'AndAlso DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) <> "KGS" AndAlso DgBulkComboGrid.Rows(index)(DgBulkOrder.IsSysQty) = "False"
                                ShowMessage("Weight Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                                Exit Function
                            End If

                        Else
                            If DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) <> DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM) Then
                                If DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) = "NOS" Then
                                    If DgBulkComboGrid.Rows(index)(DgBulkOrder.IsSysQty) = False Then
                                        If weight = 0 Then
                                            If clsDefaultConfiguration.IsNewSalesOrder Then
                                            Else
                                                ShowMessage("Weight Can Not be less than zero", "BOC001 - " & getValueByKey("CLAE04"))
                                                Exit Function
                                            End If


                                        End If
                                    End If
                                End If

                            ElseIf DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) = "KGS" Then

                                Dim weightperbox = weight.ToString.Split(".")
                                If weightperbox.Length = 2 Then
                                    If weightperbox(1) < 0 Then
                                        ShowMessage("Weight Can Not be less than zero", "BOC001 - " & getValueByKey("CLAE04"))
                                        Exit Function
                                    End If
                                End If
                                If weight = 0 Then
                                    If qty <= 0 Then
                                        ShowMessage("Article Qty Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                                        Exit Function
                                    End If
                                End If

                            End If


                        End If
                        '' added by ketan Validation if net amount is zero as per reqiurment from Manish and Varified by DilipKumar
                        Dim Netamount As Decimal = DgBulkComboGrid.Rows(index)(DgBulkOrder.NetAmt)
                        If Netamount = 0 Then
                            ShowMessage("Net Amount Can Not be zero", "BOC001 - " & getValueByKey("CLAE04"))
                            Exit Function
                        End If
                        Dim BaseUOM As String = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                        If BaseUOM = "" Then
                            ShowMessage("Select article UOM first", "BOC001 - " & getValueByKey("CLAE04"))
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) = 0
                            Exit Function
                        End If
                    Next
                End If

                updateRemarks(cboPrintName.SelectedValue.ToString(), cboPrintName.SelectedValue.ToString())
                IsCombo = True
            End If

            'If CboPakagingBox.Text.ToString.Trim.Length = 0 Or CboPakagingBox.SelectedIndex = -1 Then
            '    'MsgBox("Please Select Packaging Box ")
            '    ShowMessage(getValueByKey("BOC002"), "BOC002 - " & getValueByKey("CLAE04"))
            '    Exit Function
            'End If
            'Dim dtPackagingBox = objBulkCombo.GetPackagingBoxDataSet(clsDefaultConfiguration.PackagingBoxLastNodeCode)
            'Dim pkgBoxName As String = CboPakagingBox.Text
            'If dtPackagingBox.Rows.Count > 0 Then
            '    Dim drHdr() = dtPackagingBox.Select(CboPakagingBox.DisplayMember & "='" & pkgBoxName & "'")
            '    If drHdr.Count = 0 Then
            '        'MsgBox("Please Select Packaging Box ")
            '        ShowMessage(getValueByKey("BOC002"), "BOC002 - " & getValueByKey("CLAE04"))
            '        Exit Function
            '    End If
            'End If
            'For index = 1 To DgBulkComboGrid.Rows.Count - 1
            '    If DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM) = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom) Then
            '        If Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)) <= 0 And _
            '           Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)) <= 0 Then
            '            'MsgBox("Please enter either Quantity or Weight at Row :" & index)
            '            ShowMessage(getValueByKey("BOC003") & index, "BOC003 - " & getValueByKey("CLAE04"))
            '            Exit Function
            '        End If
            '    Else
            '        If Val(DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)) <= 0 Then
            '            'MsgBox("Please enter both Quantity and Weight at Row :" & index)
            '            ShowMessage(getValueByKey("BOC004") & index, "BOC004 - " & getValueByKey("CLAE04"))
            '            Exit Function
            '        End If
            '    End If
            'Next index
            ValidateBulkOrderCombo = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Private Sub DgBulkComboGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles DgBulkComboGrid.AfterEdit
        Try

            If (e.Col = DgBulkOrder.Packageduom) Then
                Dim Dt As DataTable = objClsCommon.GetArticleDetail(DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode))
                Dim DtComboDtl1 As DataTable = objCM_2.GetItemDetailsForBulkOrderEdit(clsAdmin.SiteCode, DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode).ToString.Trim, "EN", False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                If Dt.Rows.Count > 0 And Not Dt Is Nothing Then
                    If Dt.Rows(0)("BaseUnitofMeasure") = "NOS" Then
                        If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) <> Dt.Rows(0)("BaseUnitofMeasure") Then
                            MessageBox.Show("This Article cannot be sold in any UOM other than NOS")
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) = ""
                            Exit Sub
                        End If
                    End If

                    If Dt.Rows(0)("BaseUnitofMeasure") = "KGS" Then
                        If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) = Dt.Rows(0)("BaseUnitofMeasure") Then
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0
                            DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Price) = DtComboDtl1.Rows(0)("Price")
                        End If
                        If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) = "NOS" Then
                            If DtComboDtl1.Rows.Count > 0 And Not Dt Is Nothing Then
                                DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = DtComboDtl1.Rows(0)("Weight")
                            End If
                            DgBulkComboGrid.Rows(e.Row)(DgBulkOrder.Price) = CDbl(IIf(DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0, 1, DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)) * DtComboDtl1.Rows(0)("Price"))
                        End If
                    End If
                End If
            End If
            'If (e.Col = DgBulkOrder.Qty) Or (e.Col = DgBulkOrder.Weight) Or (e.Col = DgBulkOrder.Packageduom) Or (e.Col = DgBulkOrder.StrExcludeCheck) Then
            '    If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom) Then
            '        If e.Col = DgBulkOrder.Qty Then
            '            If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) > 0 Then
            '                DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0
            '                DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty)
            '                If btnAddBulkCombo.Enabled Then
            '                    IsStrGenerateApplicable = True
            '                End If
            '            End If
            '        Else
            '            If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) > 0 Then
            '                DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) = 0
            '                DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)
            '                If btnAddBulkCombo.Enabled Then
            '                    IsStrGenerateApplicable = True
            '                End If
            '            End If
            '        End If
            '    Else
            '        ' If DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM).ToString.ToUpper = "GM" Or DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.BaseUOM).ToString.ToUpper = "KG" Then
            '        DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)
            '        If btnAddBulkCombo.Enabled Then
            '            IsStrGenerateApplicable = True
            '        End If


            '        'Else
            '        '    DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ItemQtyBaseUOM) = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty)
            '        'End If
            '    End If
            'End If

            'If e.Col = DgBulkOrder.StrExcludeCheck Then
            '    IsStrGenerateApplicable = True
            'End If

            Try
                If (e.Col = DgBulkOrder.Weight) Then
                    Dim DtComboDtl As DataTable = objCM_2.GetItemDetailsForBulkOrderEdit(clsAdmin.SiteCode, DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode).ToString.Trim, "EN", False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                    DgBulkComboGrid.Rows(e.Row)(DgBulkOrder.Price) = CDbl(IIf(DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0, 1, DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)) * DtComboDtl.Rows(0)("Price"))
                End If

                obj1._QtyChange = True
                Obj2._QtyChange = True

                Dim BaseUOM As String = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom)
                If BaseUOM = "" Then
                    ShowMessage("Select article UOM first", "BOC001 - " & getValueByKey("CLAE04"))
                    DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty) = 0
                    Exit Sub
                End If

                '' added by ketan display Message after add Qty In combi if dis allready applyed
                If lblDiscount.Text <> "" AndAlso lblDiscount.Text > 0 Then
                    If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then
                    End If
                End If

                Dim CurrentCell As Integer = e.Col
                Dim CurrentRow As Integer = DgBulkComboGrid.Row '-- e.Row
                Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode) + "' and ComboSrNo='" + EditedSrNo.ToString() + "' ")
                If result.Count > 0 Then
                    If (e.Col = DgBulkOrder.Qty) Then
                        result(0)("Qty") = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Qty)
                    End If
                    If (e.Col = DgBulkOrder.Weight) Then
                        result(0)("Weight") = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight)
                    End If
                    If (e.Col = DgBulkOrder.Packageduom) Then
                        result(0)("Packageduom") = DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Packageduom)
                    End If
                    'If DgBulkComboGrid.Cols(e.Col).Name = "UOM" Then
                    '    If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Packageduom) = "NOS" Then
                    '        DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.Weight) = 0
                    '        result(0)("Weight") = 0
                    '    End If
                    'End If


                    DtSoBulkComboDtl.AcceptChanges()
                End If
                If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Packageduom) <> "KGS" Then
                    ' DgBulkComboGrid.Rows(DgBulkComboGrid.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                End If
                ' added on 07/06/17
                '' added by nikhil

                If clsDefaultConfiguration.IsNewSalesOrder Then
                    For Each dtDecsZero In DtSoBulkComboDtl.Rows  '## added for Discount zero Qty change
                        If DtSoBulkComboDtl.Columns.Contains("Discount") Then '##
                            dtDecsZero("Discount") = 0
                        End If

                    Next

                    If DgBulkComboGrid.Rows.Count > 1 Then
                        For i = 1 To DgBulkComboGrid.Rows.Count - 1
                            DgBulkComboGrid.Rows(i)("Discount") = "0"
                            DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount) = "0"
                            Dim TaxAmt As Double = 0
                            TaxAmt = CDbl(((DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty)) - DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount)) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) / 100)
                            DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) = TaxAmt
                            DgBulkComboGrid.Rows(i)(DgBulkOrder.NetAmt) = (DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Price)) - DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount)
                            DgBulkComboGrid.Rows(i)(DgBulkOrder.GrossAmt) = DgBulkComboGrid.Rows(i)(DgBulkOrder.NetAmt) + DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount)
                        Next
                    End If


                    CtrlGross.Visible = True
                    ctrlNetValue.Visible = True 'vipin
                    CtrlDiscount.Visible = True
                    ctrlNetValue.Visible = True
                    CtrlTaxAmt.Visible = True
                    txtTaxAmount.Visible = True
                    lblGross.Visible = True
                    lblNetValue.Visible = True
                    lblGross.Text = "0"
                    lblDiscount.Text = "0"
                    lblNetValue.Text = "0"
                    txtTaxAmount.Text = "0"
                    CtrlTaxAmt.Text = "Tax Amt: (" + "0" + " %)"
                    GrossAmt = 0
                    For i = 1 To DgBulkComboGrid.Rows.Count - 1
                        GrossAmt = CDbl(GrossAmt + CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty)))
                    Next
                    lblGross.Text = Math.Round(GrossAmt, 2)

                    Dim MaxTax As Double = 0
                    Dim txtAmount As Double = 0
                    If DgBulkComboGrid.Rows.Count > 1 Then


                        Dim preTaxPer As Decimal = 0
                        Dim IsAllTaxClash As Boolean = True
                        For i = 1 To DgBulkComboGrid.Rows.Count - 1
                            If i <> 1 Then
                                If preTaxPer <> DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                                    IsAllTaxClash = False
                                End If
                            End If
                            preTaxPer = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                        Next

                        'If IsAllTaxClash = True Then

                        '    Dim TaxAmt As Double = 0
                        '    For i = 1 To DgBulkComboGrid.Rows.Count - 1
                        '        If TaxAmt > DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) Then
                        '            TaxAmt = TaxAmt
                        '        Else
                        '            TaxAmt = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                        '        End If
                        '    Next
                        '    txtTaxAmount.Text = Math.Round(TaxAmt, 2)
                        'Else

                        For i = 1 To DgBulkComboGrid.Rows.Count - 1
                            If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                                txtAmount = CDbl(txtAmount)
                                '  MaxTax = MaxTax
                            Else
                                MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                                txtAmount = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next
                        '  txtTaxAmount.Text = Math.Round(txtAmount, 2)
                    End If

                    Dim netPrice As Double = 0
                    netPrice = CDbl((GrossAmt - lblDiscount.Text))
                    txtTaxAmount.Text = Math.Round(((CDbl(GrossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
                    lblNetValue.Text = Math.Round(netPrice, 2) + txtTaxAmount.Text
                    CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                End If


            Catch ex As Exception
                LogException(ex)
            End Try
        Catch ex As Exception
            ' ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub GrdFixedCombo_AfterEdit(sender As Object, e As RowColEventArgs) Handles GrdFixedCombo.AfterEdit
        Try
            Try
                If (e.Col = DgBulkOrder.Packageduom) Then
                    Dim Dt As DataTable = objClsCommon.GetArticleDetail(GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.ArticleCode))
                    Dim DtComboDtl1 As DataTable = objCM_2.GetItemDetailsForBulkOrderEdit(clsAdmin.SiteCode, GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.ArticleCode).ToString.Trim, "EN", False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                    If Dt.Rows.Count > 0 And Not Dt Is Nothing Then
                        If Dt.Rows(0)("BaseUnitofMeasure") = "NOS" Then
                            If GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Packageduom) <> Dt.Rows(0)("BaseUnitofMeasure") Then
                                MessageBox.Show("This Article cannot be sold in any UOM other than NOS")
                                GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Packageduom) = ""
                                Exit Sub
                            End If
                        End If
                    End If
                End If

                If (e.Col = DgBulkOrder.Weight) Then
                    Dim DtComboDtl As DataTable = objCM_2.GetItemDetailsForBulkOrderEdit(clsAdmin.SiteCode, GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.ArticleCode).ToString.Trim, "EN", False, clsDefaultConfiguration.customerwisepricemanagement, CardNo)
                    GrdFixedCombo.Rows(e.Row)(DgBulkOrder.Price) = CDbl(GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Weight) * DtComboDtl.Rows(0)("Price"))
                    'GrdFixedCombo.Rows(e.Row)(DgBulkOrder.Price) = CDbl(IIf(GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Weight) = 0, 1, GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Weight)) * DtComboDtl.Rows(0)("Price"))
                End If

                'obj1._QtyChange = True
                'Obj2._QtyChange = True

                Dim BaseUOM As String = GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Packageduom)
                If BaseUOM = "" Then
                    ShowMessage("Select article UOM first", "BOC001 - " & getValueByKey("CLAE04"))
                    GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Qty) = 0
                    Exit Sub
                End If

                '' added by ketan display Message after add Qty In combi if dis allready applyed
                'If lblDiscount.Text <> "" AndAlso lblDiscount.Text > 0 Then
                '    If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then
                '    End If
                'End If

                Dim CurrentCell As Integer = e.Col
                Dim CurrentRow As Integer = GrdFixedCombo.Row '-- e.Row
                Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.ArticleCode) + "' and ComboSrNo='" + EditedSrNo.ToString() + "' ")
                If result.Count > 0 Then
                    If (e.Col = DgBulkOrder.Qty) Then
                        result(0)("Qty") = GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Qty)
                    End If
                    If (e.Col = DgBulkOrder.Weight) Then
                        result(0)("Weight") = GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Weight)
                    End If
                    If (e.Col = DgBulkOrder.Packageduom) Then
                        result(0)("Packageduom") = GrdFixedCombo.Rows(GrdFixedCombo.Row)(DgBulkOrder.Packageduom)
                    End If
                    DtSoBulkComboDtl.AcceptChanges()
                End If
                If GrdFixedCombo.Item(CurrentRow, DgBulkOrder.Packageduom) <> "KGS" Then
                    ' GrdFixedCombo.Rows(GrdFixedCombo.Rows.Count - 1)(DgBulkOrder.IsSysQty) = "False"
                End If
                ' added on 07/06/17
                '' added by nikhil

                If clsDefaultConfiguration.IsNewSalesOrder Then
                    'For Each dtDecsZero In DtSoBulkComboDtl.Rows  '## added for Discount zero Qty change
                    '    If DtSoBulkComboDtl.Columns.Contains("Discount") Then '##
                    '        dtDecsZero("Discount") = 0
                    '    End If

                    'Next
                    If TxtFixedPriceEnter.Text = "" Then
                        TxtFixedPriceEnter.Text = 0
                    End If
                    If GrdFixedCombo.Rows.Count > 1 Then
                        For i = 1 To GrdFixedCombo.Rows.Count - 1
                            'GrdFixedCombo.Rows(i)("Discount") = "0"
                            'GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) = "0"
                            GrdFixedCombo.Rows(i)(DgBulkOrder.Price) = CDbl(TxtFixedPriceEnter.Text) / (GrdFixedCombo.Rows.Count - 1)
                            GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) = Math.Round(CDbl(lblDiscount.Text) / (GrdFixedCombo.Rows.Count - 1), 2)
                            Dim TaxAmt As Double = 0
                            TaxAmt = CDbl(((GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * 1) - GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)) * GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) / 100)
                            GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount) = TaxAmt
                            GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) = (1 * GrdFixedCombo.Rows(i)(DgBulkOrder.Price)) - GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)
                            GrdFixedCombo.Rows(i)(DgBulkOrder.GrossAmt) = GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) + GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount)
                        Next
                    End If


                    CtrlGross.Visible = True
                    ctrlNetValue.Visible = True 'vipin
                    CtrlDiscount.Visible = True
                    ctrlNetValue.Visible = True
                    CtrlTaxAmt.Visible = True
                    txtTaxAmount.Visible = True
                    lblGross.Visible = True
                    lblNetValue.Visible = True
                    TxtFixedPriceEnter.Visible = True
                    'lblGross.Text = "0"
                    'lblDiscount.Text = "0"
                    'lblNetValue.Text = "0"
                    'txtTaxAmount.Text = "0"
                    'CtrlTaxAmt.Text = "Tax Amt: (" + "0" + " %)"
                    'GrossAmt = 0
                    'For i = 1 To GrdFixedCombo.Rows.Count - 1
                    '    GrossAmt = CDbl(GrossAmt + CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * 1))
                    '    lblDiscount.Text = CDbl(lblDiscount.Text) + CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) * 1)
                    'Next
                    'lblGross.Text = Math.Round(GrossAmt, 2)
                    lblGross.Visible = False
                    ' TxtFixedPriceEnter.Text = Math.Round(GrossAmt)
                    Dim MaxTax As Double = 0
                    Dim txtAmount As Double = 0
                    If GrdFixedCombo.Rows.Count > 1 Then


                        Dim preTaxPer As Decimal = 0
                        Dim IsAllTaxClash As Boolean = True
                        For i = 1 To GrdFixedCombo.Rows.Count - 1
                            If i <> 1 Then
                                If preTaxPer <> GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                                    IsAllTaxClash = False
                                End If
                            End If
                            preTaxPer = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                        Next

                        For i = 1 To GrdFixedCombo.Rows.Count - 1
                            If MaxTax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                                txtAmount = CDbl(txtAmount)
                                '  MaxTax = MaxTax
                            Else
                                MaxTax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                                txtAmount = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next
                        txtTaxAmount.Text = Math.Round(((CDbl(TxtFixedPriceEnter.Text) - CDbl(lblDiscount.Text)) * MaxTax) / 100, 2)
                        lblNetValue.Text = CDbl(txtTaxAmount.Text) + CDbl(TxtFixedPriceEnter.Text)
                    End If

                    Dim netPrice As Double = 0
                    'lblDiscount.Text = Math.Round(CDbl(lblDiscount.Text), 2)
                    'netPrice = CDbl((GrossAmt - lblDiscount.Text))
                    'txtTaxAmount.Text = Math.Round(((CDbl(GrossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
                    'lblNetValue.Text = Math.Round(netPrice, 2) + txtTaxAmount.Text
                    CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                End If


            Catch ex As Exception
                LogException(ex)
            End Try
        Catch ex As Exception
            ' ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnSingle_CheckedChanged(sender As Object, e As EventArgs) Handles rbnSingle.CheckedChanged

        txtAddArticle.ResetListBox()
        txtAddArticle._SearchValue = [String].Empty
        Panel2.Visible = False     'vipin ##
        DgBulkComboGrid.Visible = False
        GrdFixedCombo.Visible = False
        btnAddBulkCombo.Visible = False
        Panel3.Visible = True
        'lblCopyFrom.Visible = False 'vipin ##
        'cboCopyFrom.Visible = False 'vipin
        'cboPrintName.Visible = False 'vipin
        'lblPrintName.Visible = False  'vipin
        IsCombo = False
        clearform()
        '  txtRemarks.Size = New Size(455, 120) 'vipin ##
        btnSave.Location = New System.Drawing.Point(412, 170)
        btnCancel.Location = New System.Drawing.Point(519, 170)
        btnClear.Location = New System.Drawing.Point(626, 170)
        If rbnSingle.Checked = True Then 'vipin
            Label1.Text = " "
        End If
        Panel4.Visible = False 'vipin.Visible = False
        GrdFixedCombo.Visible = False
        TxtFixedPriceEnter.Visible = False
        lblGross.Visible = False
        lblDiscount.Visible = False
        lblNetValue.Visible = False
        CtrlTaxAmt.Visible = False
        CtrlDiscount.Visible = False
        ctrlNetValue.Visible = False
        CtrlGross.Visible = False
        txtTaxAmount.Visible = False
        ctrlNetValue.Visible = False 'vipin
        '   RadioButton2_CheckedChanged(sender, e)

        Panel2.Visible = True     'vipin $$$
        cboCopyFrom.Visible = False
        lblCopyFrom.Visible = False
        lblPrintName.Visible = False
        cboPrintName.Visible = False
        btnAddBulkCombo.Visible = True
    End Sub

    Private Sub rbnCombo_CheckedChanged(sender As Object, e As EventArgs) Handles rbnCombo.CheckedChanged
        Panel2.Visible = True
        Panel3.Visible = True
        DgBulkComboGrid.Visible = True
        GrdFixedCombo.Visible = True
        btnAddBulkCombo.Visible = True

        'lblCopyFrom.Visible = True 'vipin ##
        'cboCopyFrom.Visible = True 'vipin
        'cboPrintName.Visible = True 'vipin
        'lblPrintName.Visible = True  'vipin

        IsCombo = True
        clearform()
        '  txtRemarks.Size = New Size(455, 36) 'vipin ##
        btnSave.Location = New System.Drawing.Point(456, 291)
        btnCancel.Location = New System.Drawing.Point(563, 291)
        btnClear.Location = New System.Drawing.Point(670, 291)
        If rbnCombo.Checked = True Then
            Label1.Text = "Single Combo Price Details"
        End If
        Panel4.Visible = True 'vipin
        lblGross.Visible = True
        lblDiscount.Visible = True
        lblNetValue.Visible = True
        CtrlTaxAmt.Visible = True
        CtrlDiscount.Visible = True
        ctrlNetValue.Visible = True
        CtrlGross.Visible = True
        txtTaxAmount.Visible = True
        ctrlNetValue.Visible = True 'vipin
        RadioButton2_CheckedChanged(sender, e)

        Panel2.Visible = True     'vipin $$$
        cboCopyFrom.Visible = True
        lblCopyFrom.Visible = True
        lblPrintName.Visible = True
        cboPrintName.Visible = True
        btnAddBulkCombo.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            TxtFixedPriceEnter_Leave(sender, e)

            If ValidateBulkOrderCombo() Then
                Dim CurrentDateTime As DateTime = objClsCommon.GetCurrentDate()
                If rbnSingle.Checked = False Then
                    If clsDefaultConfiguration.IsNewSalesOrder Then
                        If DtSoBulkComboDtl.Columns.Contains("Price") = True Then
                        Else
                            DtSoBulkComboDtl.Columns.Add("Price", GetType(Decimal))
                        End If

                        If DtSoBulkComboDtl.Columns.Contains("Discount") = True Then
                        Else
                            DtSoBulkComboDtl.Columns.Add("Discount", GetType(Decimal))
                        End If

                        If DtSoBulkComboDtl.Columns.Contains("Tax") = True Then
                        Else
                            DtSoBulkComboDtl.Columns.Add("Tax", GetType(Decimal))

                        End If

                        If DtSoBulkComboDtl.Columns.Contains("TaxAmount") = True Then
                        Else
                            DtSoBulkComboDtl.Columns.Add("TaxAmount", GetType(Decimal))
                        End If
                        If dtLable.Columns.Contains("GrossAmount") = True And dtLable.Columns.Contains("NetAmount") = True Then
                        Else
                            dtLable.Columns.Add("GrossAmount", GetType(Decimal))
                            dtLable.Columns.Add("NetAmount", GetType(Decimal))
                            dtLable.Columns.Add("Tax", GetType(Decimal))
                            dtLable.Columns.Add("TaxAmount", GetType(Decimal))
                            dtLable.Columns.Add("Discount", GetType(Decimal))

                        End If

                    End If

                    If IsEdit Then
                        Dim editIndex As Integer = DtSoBulkComboDtl.Rows.Count

                        Dim resultheader As DataRow() = DtSoBulkComboHdr.Select("ComboSrNo='" + EditedSrNo.ToString() + "'")
                        If resultheader.Length > 0 Then
                            resultheader(0)("AdditionComments") = txtRemarks.Text
                            resultheader(0)("PackagingBoxPrintname") = cboPrintName.SelectedValue.ToString()

                        End If

                        If CboFixed.Checked = True Then
                            For index = 1 To GrdFixedCombo.Rows.Count - 1
                                Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleCode).ToString() + "' and ComboSrNo='" + EditedSrNo.ToString() + "'")
                                If result.Length = 0 Then
                                    DtSoBulkComboDtl.Rows.Add()

                                    DtSoBulkComboDtl.Rows(editIndex)("SaleOrderNumber") = SalesOrderNo
                                    DtSoBulkComboDtl.Rows(editIndex)("FinYear") = clsAdmin.Financialyear
                                    DtSoBulkComboDtl.Rows(editIndex)("ComboSrNo") = EditedSrNo
                                    DtSoBulkComboDtl.Rows(editIndex)("SiteCode") = clsAdmin.SiteCode
                                    DtSoBulkComboDtl.Rows(editIndex)("ArticleCode") = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleCode)
                                    DtSoBulkComboDtl.Rows(editIndex)("ArticleDescription") = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleDescription)
                                    DtSoBulkComboDtl.Rows(editIndex)("EAN") = GrdFixedCombo.Rows(index)(DgBulkOrder.EAN)
                                    DtSoBulkComboDtl.Rows(editIndex)("Qty") = GrdFixedCombo.Rows(index)(DgBulkOrder.Qty)
                                    DtSoBulkComboDtl.Rows(editIndex)("Weight") = GrdFixedCombo.Rows(index)(DgBulkOrder.Weight)
                                    If clsDefaultConfiguration.IsNewSalesOrder Then
                                        DtSoBulkComboDtl.Rows(editIndex)("Price") = GrdFixedCombo.Rows(index)(DgBulkOrder.Price)
                                        DtSoBulkComboDtl.Rows(editIndex)("Discount") = GrdFixedCombo.Rows(index)(DgBulkOrder.Discount)
                                        DtSoBulkComboDtl.Rows(editIndex)("Tax") = GrdFixedCombo.Rows(index)(DgBulkOrder.Tax)
                                        DtSoBulkComboDtl.Rows(editIndex)("TaxAmount") = GrdFixedCombo.Rows(index)(DgBulkOrder.TaxAmount)
                                    End If
                                    DtSoBulkComboDtl.Rows(editIndex)("BaseUOM") = GrdFixedCombo.Rows(index)(DgBulkOrder.BaseUOM)
                                    DtSoBulkComboDtl.Rows(editIndex)("Packageduom") = GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom)
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDAT") = vSiteCode
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDBY") = vUserName
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDON") = CurrentDateTime
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDAT") = vSiteCode
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDBY") = vUserName
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDON") = CurrentDateTime
                                    DtSoBulkComboDtl.Rows(editIndex)("STATUS") = True
                                    editIndex = editIndex + 1
                                Else
                                    If clsDefaultConfiguration.IsNewSalesOrder Then
                                        result(0)("Price") = GrdFixedCombo.Rows(index)(DgBulkOrder.Price).ToString
                                        result(0)("Discount") = GrdFixedCombo.Rows(index)(DgBulkOrder.Discount)
                                        result(0)("Tax") = GrdFixedCombo.Rows(index)(DgBulkOrder.Tax)
                                        result(0)("TaxAmount") = GrdFixedCombo.Rows(index)(DgBulkOrder.TaxAmount)
                                    End If
                                End If
                            Next
                        Else
                            For index = 1 To DgBulkComboGrid.Rows.Count - 1
                                Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode).ToString() + "' and ComboSrNo='" + EditedSrNo.ToString() + "'")
                                If result.Length = 0 Then
                                    DtSoBulkComboDtl.Rows.Add()

                                    DtSoBulkComboDtl.Rows(editIndex)("SaleOrderNumber") = SalesOrderNo
                                    DtSoBulkComboDtl.Rows(editIndex)("FinYear") = clsAdmin.Financialyear
                                    DtSoBulkComboDtl.Rows(editIndex)("ComboSrNo") = EditedSrNo
                                    DtSoBulkComboDtl.Rows(editIndex)("SiteCode") = clsAdmin.SiteCode
                                    'DtSoBulkComboDtl.Rows(dtRow)("BulkComboMstId") = BulkComboMstId
                                    DtSoBulkComboDtl.Rows(editIndex)("ArticleCode") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode)
                                    DtSoBulkComboDtl.Rows(editIndex)("ArticleDescription") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleDescription)
                                    DtSoBulkComboDtl.Rows(editIndex)("EAN") = DgBulkComboGrid.Rows(index)(DgBulkOrder.EAN)
                                    'DtSoBulkComboDtl.Rows(dtRow)("PackagedUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                                    DtSoBulkComboDtl.Rows(editIndex)("Qty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)
                                    'If DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM) = "KGS" Then
                                    '    DtSoBulkComboDtl.Rows(editIndex)("Weight") = 0
                                    'Else
                                    '    DtSoBulkComboDtl.Rows(editIndex)("Weight") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)
                                    'End If
                                    DtSoBulkComboDtl.Rows(editIndex)("Weight") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)
                                    If clsDefaultConfiguration.IsNewSalesOrder Then
                                        DtSoBulkComboDtl.Rows(editIndex)("Price") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Price)
                                        DtSoBulkComboDtl.Rows(editIndex)("Discount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Discount)
                                        DtSoBulkComboDtl.Rows(editIndex)("Tax") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Tax)
                                        DtSoBulkComboDtl.Rows(editIndex)("TaxAmount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.TaxAmount)
                                    End If
                                    ' DtSoBulkComboDtl.Rows(dtRow)("STRQty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.STRQty)
                                    'DtSoBulkComboDtl.Rows(dtRow)("StrExcludeCheck") = DgBulkComboGrid.Rows(index)(DgBulkOrder.StrExcludeCheck)
                                    DtSoBulkComboDtl.Rows(editIndex)("BaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM)
                                    DtSoBulkComboDtl.Rows(editIndex)("Packageduom") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                                    'DtSoBulkComboDtl.Rows(dtRow)("ItemQtyBaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ItemQtyBaseUOM)
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDAT") = vSiteCode
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDBY") = vUserName
                                    DtSoBulkComboDtl.Rows(editIndex)("CREATEDON") = CurrentDateTime
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDAT") = vSiteCode
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDBY") = vUserName
                                    DtSoBulkComboDtl.Rows(editIndex)("UPDATEDON") = CurrentDateTime
                                    DtSoBulkComboDtl.Rows(editIndex)("STATUS") = True
                                    editIndex = editIndex + 1
                                Else
                                    If clsDefaultConfiguration.IsNewSalesOrder Then
                                        result(0)("Price") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Price).ToString
                                        result(0)("Discount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Discount)
                                        result(0)("Tax") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Tax)
                                        result(0)("TaxAmount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.TaxAmount)
                                    End If
                                End If
                            Next
                        End If


                    Else
                        IsCombo = True
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

                        DtSoBulkComboHdr.Rows(hdrRow)("SaleOrderNumber") = SalesOrderNo
                        DtSoBulkComboHdr.Rows(hdrRow)("sitecode") = clsAdmin.SiteCode
                        DtSoBulkComboHdr.Rows(hdrRow)("ComboSrNo") = ComboSrNo
                        DtSoBulkComboHdr.Rows(hdrRow)("FinYear") = clsAdmin.Financialyear
                        DtSoBulkComboHdr.Rows(hdrRow)("AdditionComments") = txtRemarks.Text.Trim
                        DtSoBulkComboHdr.Rows(hdrRow)("PackagingBoxPrintName") = cboPrintName.SelectedValue
                        DtSoBulkComboHdr.Rows(hdrRow)("PackagingBoxCode") = "11111111"

                        DtSoBulkComboHdr.Rows(hdrRow)("CREATEDAT") = vSiteCode
                        DtSoBulkComboHdr.Rows(hdrRow)("CREATEDBY") = vUserName
                        DtSoBulkComboHdr.Rows(hdrRow)("CREATEDON") = CurrentDateTime
                        DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDAT") = vSiteCode
                        DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDBY") = vUserName
                        DtSoBulkComboHdr.Rows(hdrRow)("UPDATEDON") = CurrentDateTime
                        DtSoBulkComboHdr.Rows(hdrRow)("STATUS") = True

                        If CboFixed.Checked = True Then
                            DtSoBulkComboHdr.Rows(hdrRow)("IsFixedPrice") = True
                        Else
                            DtSoBulkComboHdr.Rows(hdrRow)("IsFixedPrice") = False
                        End If

                        ''----Delete all details record 
                        'Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & BulkComboMstId & " AND BulkComboDetId < 500 ")
                        'If drDtl.Count > 0 Then
                        '    For Each row As DataRow In drDtl
                        '        DtSoBulkComboDtl.Rows.Remove(row)
                        '    Next
                        'End If
                        If CboFixed.Checked = True Then
                            For index = 1 To GrdFixedCombo.Rows.Count - 1
                                Dim dtRow As Int32 = -1
                                'If DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) > 500 Then
                                '    For index2 = 0 To DtSoBulkComboDtl.Rows.Count - 1
                                '        If DtSoBulkComboDtl.Rows(index2)("BulkComboDetId") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) Then
                                '            dtRow = index2
                                '            Exit For
                                '        End If
                                '    Next index2
                                'Else
                                '    DtSoBulkComboDtl.Rows.Add()
                                '    dtRow = DtSoBulkComboDtl.Rows.Count - 1
                                'End If
                                DtSoBulkComboDtl.Rows.Add()
                                dtRow = DtSoBulkComboDtl.Rows.Count - 1
                                DtSoBulkComboDtl.Rows(dtRow)("SaleOrderNumber") = SalesOrderNo
                                DtSoBulkComboDtl.Rows(dtRow)("FinYear") = clsAdmin.Financialyear
                                DtSoBulkComboDtl.Rows(dtRow)("ComboSrNo") = ComboSrNo
                                DtSoBulkComboDtl.Rows(dtRow)("SiteCode") = clsAdmin.SiteCode
                                DtSoBulkComboDtl.Rows(dtRow)("ArticleCode") = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleCode)
                                DtSoBulkComboDtl.Rows(dtRow)("ArticleDescription") = GrdFixedCombo.Rows(index)(DgBulkOrder.ArticleDescription)
                                DtSoBulkComboDtl.Rows(dtRow)("EAN") = GrdFixedCombo.Rows(index)(DgBulkOrder.EAN)
                                DtSoBulkComboDtl.Rows(dtRow)("Qty") = GrdFixedCombo.Rows(index)(DgBulkOrder.Qty)
                                If GrdFixedCombo.Rows(index)(DgBulkOrder.Weight) Is Nothing Then
                                    DtSoBulkComboDtl.Rows(dtRow)("Weight") = 0
                                Else
                                    DtSoBulkComboDtl.Rows(dtRow)("Weight") = GrdFixedCombo.Rows(index)(DgBulkOrder.Weight)
                                End If


                                If clsDefaultConfiguration.IsNewSalesOrder Then
                                    DtSoBulkComboDtl.Rows(dtRow)("Price") = GrdFixedCombo.Rows(index)(DgBulkOrder.Price)
                                    DtSoBulkComboDtl.Rows(dtRow)("Discount") = GrdFixedCombo.Rows(index)(DgBulkOrder.Discount)
                                    DtSoBulkComboDtl.Rows(dtRow)("Tax") = GrdFixedCombo.Rows(index)(DgBulkOrder.Tax)
                                    DtSoBulkComboDtl.Rows(dtRow)("TaxAmount") = GrdFixedCombo.Rows(index)(DgBulkOrder.TaxAmount)
                                End If
                                DtSoBulkComboDtl.Rows(dtRow)("BaseUOM") = GrdFixedCombo.Rows(index)(DgBulkOrder.BaseUOM)
                                DtSoBulkComboDtl.Rows(dtRow)("Packageduom") = GrdFixedCombo.Rows(index)(DgBulkOrder.Packageduom)
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDAT") = vSiteCode
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDBY") = vUserName
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDON") = CurrentDateTime
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDAT") = vSiteCode
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDBY") = vUserName
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDON") = CurrentDateTime
                                DtSoBulkComboDtl.Rows(dtRow)("STATUS") = True
                            Next index
                        Else
                            For index = 1 To DgBulkComboGrid.Rows.Count - 1
                                Dim dtRow As Int32 = -1
                                'If DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) > 500 Then
                                '    For index2 = 0 To DtSoBulkComboDtl.Rows.Count - 1
                                '        If DtSoBulkComboDtl.Rows(index2)("BulkComboDetId") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BulkComboDetId) Then
                                '            dtRow = index2
                                '            Exit For
                                '        End If
                                '    Next index2
                                'Else
                                '    DtSoBulkComboDtl.Rows.Add()
                                '    dtRow = DtSoBulkComboDtl.Rows.Count - 1
                                'End If
                                DtSoBulkComboDtl.Rows.Add()
                                dtRow = DtSoBulkComboDtl.Rows.Count - 1
                                DtSoBulkComboDtl.Rows(dtRow)("SaleOrderNumber") = SalesOrderNo
                                DtSoBulkComboDtl.Rows(dtRow)("FinYear") = clsAdmin.Financialyear
                                DtSoBulkComboDtl.Rows(dtRow)("ComboSrNo") = ComboSrNo
                                DtSoBulkComboDtl.Rows(dtRow)("SiteCode") = clsAdmin.SiteCode
                                'DtSoBulkComboDtl.Rows(dtRow)("BulkComboMstId") = BulkComboMstId
                                DtSoBulkComboDtl.Rows(dtRow)("ArticleCode") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleCode)
                                DtSoBulkComboDtl.Rows(dtRow)("ArticleDescription") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ArticleDescription)
                                DtSoBulkComboDtl.Rows(dtRow)("EAN") = DgBulkComboGrid.Rows(index)(DgBulkOrder.EAN)
                                'DtSoBulkComboDtl.Rows(dtRow)("PackagedUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                                DtSoBulkComboDtl.Rows(dtRow)("Qty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Qty)
                                If DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight) Is Nothing Then
                                    DtSoBulkComboDtl.Rows(dtRow)("Weight") = 0
                                Else
                                    DtSoBulkComboDtl.Rows(dtRow)("Weight") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Weight)
                                End If


                                If clsDefaultConfiguration.IsNewSalesOrder Then
                                    DtSoBulkComboDtl.Rows(dtRow)("Price") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Price)
                                    DtSoBulkComboDtl.Rows(dtRow)("Discount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Discount)
                                    DtSoBulkComboDtl.Rows(dtRow)("Tax") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Tax)
                                    DtSoBulkComboDtl.Rows(dtRow)("TaxAmount") = DgBulkComboGrid.Rows(index)(DgBulkOrder.TaxAmount)
                                End If
                                ' DtSoBulkComboDtl.Rows(dtRow)("STRQty") = DgBulkComboGrid.Rows(index)(DgBulkOrder.STRQty)
                                'DtSoBulkComboDtl.Rows(dtRow)("StrExcludeCheck") = DgBulkComboGrid.Rows(index)(DgBulkOrder.StrExcludeCheck)
                                DtSoBulkComboDtl.Rows(dtRow)("BaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.BaseUOM)
                                DtSoBulkComboDtl.Rows(dtRow)("Packageduom") = DgBulkComboGrid.Rows(index)(DgBulkOrder.Packageduom)
                                'DtSoBulkComboDtl.Rows(dtRow)("ItemQtyBaseUOM") = DgBulkComboGrid.Rows(index)(DgBulkOrder.ItemQtyBaseUOM)
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDAT") = vSiteCode
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDBY") = vUserName
                                DtSoBulkComboDtl.Rows(dtRow)("CREATEDON") = CurrentDateTime
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDAT") = vSiteCode
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDBY") = vUserName
                                DtSoBulkComboDtl.Rows(dtRow)("UPDATEDON") = CurrentDateTime
                                DtSoBulkComboDtl.Rows(dtRow)("STATUS") = True
                            Next index
                        End If

                    End If

                    If clsDefaultConfiguration.IsNewSalesOrder Then
                        Dim drRow As DataRow
                        Dim Tax As Double = 0
                        Dim TaxAmount As Double = 0
                        Dim MaxTax As Double = 0
                        Dim txtAmount As Double = 0
                        dtLable.Clear()

                        If CboFixed.Checked = True Then
                            If GrdFixedCombo.Rows.Count > 1 Then

                                For i = 1 To GrdFixedCombo.Rows.Count - 1
                                    If Tax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                                        Tax = Tax
                                    Else
                                        Tax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                                    End If
                                Next

                                For i = 1 To GrdFixedCombo.Rows.Count - 1
                                    If TaxAmount > GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount) Then
                                        TaxAmount = TaxAmount
                                    Else
                                        TaxAmount = GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount)
                                    End If
                                Next

                                For i = 1 To GrdFixedCombo.Rows.Count - 1
                                    If MaxTax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                                        txtAmount = CDbl(txtAmount)
                                        '  MaxTax = MaxTax
                                    Else
                                        MaxTax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                                        txtAmount = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
                                    End If
                                Next
                                txtTaxAmount.Text = Math.Round(txtAmount, 3)   ''txtAmount.ToString ''

                            End If
                        Else
                            If DgBulkComboGrid.Rows.Count > 1 Then

                                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                                    If Tax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                                        Tax = Tax
                                    Else
                                        Tax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                                    End If
                                Next

                                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                                    If TaxAmount > DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) Then
                                        TaxAmount = TaxAmount
                                    Else
                                        TaxAmount = DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount)
                                    End If
                                Next

                                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                                    If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                                        txtAmount = CDbl(txtAmount)
                                        '  MaxTax = MaxTax
                                    Else
                                        MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                                        txtAmount = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                                    End If
                                Next
                                txtTaxAmount.Text = Math.Round(txtAmount, 3)   ''txtAmount.ToString ''

                            End If
                        End If

                        txtTaxAmount.Text = Math.Round(((CDbl(GrossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
                        CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                        Dim Order As New frmPCSalesOrderCreation
                        Dim Order1 As New frmPCNSalesOrderUpdate
                        drRow = dtLable.NewRow
                        If Not DtSoBulkComboDtl Is Nothing Then  '##
                            If DtSoBulkComboDtl.Rows.Count > 0 Then
                                If IIf(DtSoBulkComboDtl.Rows(0)("Discount").ToString.Trim = "", 0, DtSoBulkComboDtl.Rows(0)("Discount").ToString.Trim) = "0" Then
                                    drRow("GrossAmount") = Math.Round(CDbl(lblGross.Text), 2)
                                    drRow("NetAmount") = Math.Round(CDbl(lblNetValue.Text), 2)
                                    drRow("Tax") = Tax.ToString
                                    drRow("TaxAmount") = txtAmount.ToString
                                    drRow("Discount") = lblDiscount.Text
                                Else
                                    drRow("GrossAmount") = Math.Round(CDbl(lblGross.Text), 2)
                                    drRow("NetAmount") = Math.Round(CDbl(lblNetValue.Text), 2)
                                    drRow("Discount") = lblDiscount.Text
                                    drRow("Tax") = Tax.ToString
                                    drRow("TaxAmount") = txtAmount.ToString
                                End If
                            End If
                        End If
                        dtLable.Rows.Add(drRow)
                        Order.lblArticleCombo = dtLable
                        Order1.lblArticleCombo1 = dtLable
                    End If
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    IsEdit = False
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    IsEdit = False
                End If

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If clsDefaultConfiguration.IsNewSalesOrder Then

            lblGross.Visible = False
            lblDiscount.Visible = False
            lblNetValue.Visible = False
            CtrlTaxAmt.Visible = False
            CtrlDiscount.Visible = False
            ctrlNetValue.Visible = False
            CtrlGross.Visible = False
            txtTaxAmount.Visible = False
            ctrlNetValue.Value = False 'vipin
            TxtFixedPriceEnter.Text = 0
            TxtFixedPriceEnter.Visible = False
        End If
        If CboFixed.Checked = True Then
            clearformFixed()
        Else
            clearform()
        End If
        IsEdit = False
    End Sub

    Private Sub clearform()
        txtRemarks.Text = ""
        txtAddArticle.ReadOnly = False

        txtAddArticle.Text = ""
        If dtPackagingPrintBox.Rows.Count > 0 Then
            cboPrintName.SelectedIndex = -1
        End If
        If dtPackagingcopiedCombo.Rows.Count > 0 Then
            cboCopyFrom.SelectedIndex = 1
        End If
        DgBulkComboGrid.Rows.RemoveRange(1, DgBulkComboGrid.Rows.Count - 1)
    End Sub

    Private Sub clearformFixed()
        txtRemarks.Text = ""
        txtAddArticle.ReadOnly = False

        txtAddArticle.Text = ""
        If dtPackagingPrintBox.Rows.Count > 0 Then
            cboPrintName.SelectedIndex = -1
        End If
        If dtPackagingcopiedCombo.Rows.Count > 0 Then
            cboCopyFrom.SelectedIndex = 1
        End If
        GrdFixedCombo.Rows.RemoveRange(1, GrdFixedCombo.Rows.Count - 1)
    End Sub

    Private Function updateRemarks(Optional ByVal ArticleCode As String = "", Optional ByVal Desc As String = "") As DataTable
        Try
            ' If rbnSingle.Checked Then
            If Not IsEdit Then
                If txtRemarks.Text.Trim.Length > 0 Then
                    Dim rowDeliveryAddr = DtSoBulkRemarks.NewRow()
                    rowDeliveryAddr("SrNo") = ComboSrNo
                    rowDeliveryAddr("ArticleCode") = ArticleCode
                    If rbnSingle.Checked Then
                        rowDeliveryAddr("ArticleName") = Desc
                        rowDeliveryAddr("ArticleType") = "Single"
                    Else
                        rowDeliveryAddr("ArticleName") = displaySrNo & "-" & Desc & " (" & DgBulkComboGrid.Rows.Count - 1 & ")"
                        rowDeliveryAddr("ArticleType") = "Combo"
                    End If

                    rowDeliveryAddr("Remark") = txtRemarks.Text.Trim
                    DtSoBulkRemarks.Rows.Add(rowDeliveryAddr)
                End If
            Else
                Dim result As DataRow() = DtSoBulkRemarks.Select("ArticleCode='" + ArticleCode.ToString() + "' and SrNo='" + EditedSrNo.ToString + "'")
                If result.Length > 0 Then
                    If txtRemarks.Text.Trim.Length > 0 Then
                        result(0)("Remark") = txtRemarks.Text.Trim   'Edit
                        If result(0)("ArticleType") = "Combo" Then
                            result(0)("ArticleName") = displaySrNo & "-" & Desc & " (" & DgBulkComboGrid.Rows.Count - 1 & ")"
                        End If
                    Else
                        'For i = 0 To DtSoBulkRemarks.Rows.Count - 1
                        '    If DtSoBulkRemarks.Rows(i)("ArticleCode") = ArticleCode Then
                        '        DtSoBulkRemarks.Rows.RemoveAt(i)
                        '    End If
                        'Next
                        Dim dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & EditedSrNo.ToString() & "'", "", DataViewRowState.CurrentRows)
                        If dvRemarks.Count > 0 Then
                            For Each drView As DataRowView In dvRemarks
                                drView.Delete()
                            Next
                        End If
                    End If
                Else
                    If txtRemarks.Text.Trim.Length > 0 Then
                        Dim rowDeliveryAddr = DtSoBulkRemarks.NewRow()
                        rowDeliveryAddr("SrNo") = EditedSrNo
                        rowDeliveryAddr("ArticleCode") = ArticleCode
                        If rbnSingle.Checked Then
                            rowDeliveryAddr("ArticleName") = Desc
                            rowDeliveryAddr("ArticleType") = "Single"
                        Else
                            rowDeliveryAddr("ArticleName") = displaySrNo & "-" & Desc & " (" & DgBulkComboGrid.Rows.Count - 1 & ")"
                            rowDeliveryAddr("ArticleType") = "Combo"
                        End If

                        rowDeliveryAddr("Remark") = txtRemarks.Text.Trim
                        DtSoBulkRemarks.Rows.Add(rowDeliveryAddr)
                        'dvRemarks = New DataView(DtSoBulkRemarks, "SrNo='" & ComboSrNo & "' ", "", DataViewRowState.CurrentRows) 'Delete 
                        'If dvRemarks.Count > 0 Then
                        '    For Each drView2 As DataRowView In dvRemarks
                        '        drView2.Delete()
                        '    Next
                        '    ' LoadRemarks(DtSoBulkRemarks, True)
                        'End If
                    End If
                End If
            End If
            DtSoBulkRemarks.AcceptChanges()
            ' End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub cboCopyFrom_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCopyFrom.SelectedValueChanged
        Try
            If cboCopyFrom.SelectedValue > 0 Then
                IsEdit = False
                LoadArticleData(True)
                obj1.IsCopied = True

                If CboFixed.Checked = True Then
                    GrossAmt = 0 '$$$
                    lblDiscount.Text = 0
                    For i = 1 To GrdFixedCombo.Rows.Count - 1               ''$$ added by nikhil
                        GrossAmt = CDbl(GrossAmt + CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * GrdFixedCombo.Rows(i)(DgBulkOrder.Qty)))
                        lblDiscount.Text = Math.Round(CDbl(CDbl(lblDiscount.Text) + CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Discount))), 2)
                    Next
                    lblGross.Text = Math.Round(GrossAmt, 2)

                    Dim MaxTax As Double = 0
                    Dim txtAmount As Double = 0
                    If GrdFixedCombo.Rows.Count > 1 Then
                        Dim TaxAmt As Double = 0
                        For i = 1 To GrdFixedCombo.Rows.Count - 1
                            If TaxAmt > GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount) Then
                                TaxAmt = TaxAmt
                            Else
                                TaxAmt = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next

                        For i = 1 To GrdFixedCombo.Rows.Count - 1
                            If MaxTax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                                txtAmount = CDbl(txtAmount)
                                '  MaxTax = MaxTax
                            Else
                                MaxTax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                                txtAmount = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next
                        txtTaxAmount.Text = Math.Round(txtAmount, 2)
                    End If
                    Dim netPrice As Double = 0
                    netPrice = CDbl((GrossAmt - lblDiscount.Text))
                    txtTaxAmount.Text = Math.Round(((CDbl(GrossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
                    CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                    lblNetValue.Text = GrossAmt - CDbl(lblDiscount.Text) + CDbl(txtTaxAmount.Text)
                    TxtFixedPriceEnter.Enabled = True
                Else
                    GrossAmt = 0 '$$$
                    lblDiscount.Text = 0
                    For i = 1 To DgBulkComboGrid.Rows.Count - 1               ''$$ added by nikhil
                        GrossAmt = CDbl(GrossAmt + CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty)))
                        lblDiscount.Text = Math.Round(CDbl(CDbl(lblDiscount.Text) + CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount))), 2)
                    Next
                    lblGross.Text = Math.Round(GrossAmt, 2)

                    Dim MaxTax As Double = 0
                    Dim txtAmount As Double = 0
                    If DgBulkComboGrid.Rows.Count > 1 Then
                        Dim TaxAmt As Double = 0
                        For i = 1 To DgBulkComboGrid.Rows.Count - 1
                            If TaxAmt > DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) Then
                                TaxAmt = TaxAmt
                            Else
                                TaxAmt = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next

                        For i = 1 To DgBulkComboGrid.Rows.Count - 1
                            If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                                txtAmount = CDbl(txtAmount)
                                '  MaxTax = MaxTax
                            Else
                                MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                                txtAmount = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                            End If
                        Next
                        txtTaxAmount.Text = Math.Round(txtAmount, 2)
                    End If
                    Dim netPrice As Double = 0
                    netPrice = CDbl((GrossAmt - lblDiscount.Text))
                    txtTaxAmount.Text = Math.Round(((CDbl(GrossAmt) - CDbl(lblDiscount.Text)) * CDbl(MaxTax)) / 100, 2)
                    CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                    lblNetValue.Text = GrossAmt - CDbl(lblDiscount.Text) + CDbl(txtTaxAmount.Text)
                End If


            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub DgBulkComboGrid_CellButtonClick(sender As Object, e As RowColEventArgs) Handles DgBulkComboGrid.CellButtonClick
        Try
            Dim ComboSrNo = EditedSrNo
            obj1._QtyChange = True
            Obj2._QtyChange = True
            Dim ArticleCode = DgBulkComboGrid.Item(DgBulkComboGrid.Row, "Code")
            DeleteBulkCombo(ComboSrNo, ArticleCode)
            '' added by nikhil
            For i = 1 To DgBulkComboGrid.Rows.Count - 1
                DgBulkComboGrid.Rows(i)("Discount") = "0"
                DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount) = "0"
                DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) = CDbl(((DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty))) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) / 100)
                DgBulkComboGrid.Rows(i)(DgBulkOrder.NetAmt) = (DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Price))
                DgBulkComboGrid.Rows(i)(DgBulkOrder.GrossAmt) = DgBulkComboGrid.Rows(i)(DgBulkOrder.NetAmt) + DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount)
            Next
            If clsDefaultConfiguration.IsNewSalesOrder Then  'nilhil
                lblGross.Text = "0"
                lblDiscount.Text = "0"
                lblNetValue.Text = "0"
                txtTaxAmount.Text = "0"   '## added by vipin
                CtrlTaxAmt.Text = "Tax Amt: (" + "0" + " %)"
                Dim GrossAmt As Decimal = 0
                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                    GrossAmt = GrossAmt + CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.Price) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty))
                Next
                lblGross.Text = Math.Round(GrossAmt, 2)
                'lblNetValue.Text = (CDbl(txtTaxAmount.Text) + CDbl(GrossAmt - lblDiscount.Text)).ToString
                Dim MaxTax As Double = 0
                Dim txtAmount As Double = 0
                'For i = 1 To DgBulkComboGrid.Rows.Count - 1
                '    If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                '        txtAmount = CDbl(txtAmount)
                '        '  MaxTax = MaxTax
                '    Else
                '        MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                '        DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) = CDbl(((DgBulkComboGrid.Rows(i)(DgBulkOrder.Price)) * DgBulkComboGrid.Rows(i)(DgBulkOrder.Qty)) - DgBulkComboGrid.Rows(i)(DgBulkOrder.Discount)) * CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) / 100)
                '        txtAmount = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                '    End If
                'Next
                'txtTaxAmount.Text = Math.Round(txtAmount, 2)
                Dim TotTax As Decimal = 0
                Dim preTaxPer As Decimal = 0
                Dim IsAllTaxClash As Boolean = True
                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                    If i <> 1 Then
                        If preTaxPer <> DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                            IsAllTaxClash = False
                        End If
                    End If
                    preTaxPer = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                Next

                'If IsAllTaxClash = True Then

                '    Dim TaxAmt As Double = 0
                '    For i = 1 To DgBulkComboGrid.Rows.Count - 1
                '        If TaxAmt > DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount) Then
                '            TaxAmt = TaxAmt
                '        Else
                '            TaxAmt = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                '        End If
                '    Next
                '    txtTaxAmount.Text = Math.Round(TaxAmt, 2)
                'Else

                For i = 1 To DgBulkComboGrid.Rows.Count - 1
                    If MaxTax > DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax) Then
                        TotTax = CDbl(TotTax)
                        '  MaxTax = MaxTax
                    Else
                        MaxTax = DgBulkComboGrid.Rows(i)(DgBulkOrder.Tax)
                        TotTax = CDbl(DgBulkComboGrid.Rows(i)(DgBulkOrder.TaxAmount))
                    End If
                Next

                txtTaxAmount.Text = Math.Round((CDbl(GrossAmt) * CDbl(MaxTax)) / 100, 2)
                ' End If
                lblNetValue.Text = (CDbl(txtTaxAmount.Text) + CDbl(GrossAmt)) '## Commnted and added by vipin 
                CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                If DgBulkComboGrid.Rows.Count = 1 Then
                    'lblGross.Visible = False
                    'lblDiscount.Visible = False
                    'lblNetValue.Visible = False
                    'CtrlTaxAmt.Visible = False
                    'CtrlDiscount.Visible = False
                    'ctrlNetValue.Visible = False
                    'CtrlGross.Visible = False
                    'txtTaxAmount.Visible = False
                    'ctrlNetValue.Visible = False 'vipin
                    lblGross.Text = 0
                    lblDiscount.Text = 0
                    lblNetValue.Text = 0
                    txtTaxAmount.Text = 0
                    TxtFixedPriceEnter.Enabled = False
                    TxtFixedPriceEnter.Visible = False
                    TxtFixedPriceEnter.Text = 0.0
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GrdFixedCombo_CellButtonClick(sender As Object, e As RowColEventArgs) Handles GrdFixedCombo.CellButtonClick
        Try
            Dim ComboSrNo = EditedSrNo
            'obj1._QtyChange = True
            'Obj2._QtyChange = True
            Dim ArticleCode = GrdFixedCombo.Item(GrdFixedCombo.Row, "Code")
            DeleteBulkComboFixed(ComboSrNo, ArticleCode)
            '' added by nikhil
            If TxtFixedPriceEnter.Text = "" Then
                TxtFixedPriceEnter.Text = "0"
            End If
            For i = 1 To GrdFixedCombo.Rows.Count - 1
                'GrdFixedCombo.Rows(i)("Discount") = "0"
                'GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) = "0"
                GrdFixedCombo.Rows(i)(DgBulkOrder.Price) = Math.Round(CDbl(TxtFixedPriceEnter.Text) / (GrdFixedCombo.Rows.Count - 1), 2)
                GrdFixedCombo.Rows(i)(DgBulkOrder.Discount) = Math.Round(CDbl(lblDiscount.Text) / (GrdFixedCombo.Rows.Count - 1), 2)
                GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount) = CDbl(((GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * 1)) * GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) / 100)
                GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) = (1 * GrdFixedCombo.Rows(i)(DgBulkOrder.Price)) - GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)
                GrdFixedCombo.Rows(i)(DgBulkOrder.GrossAmt) = GrdFixedCombo.Rows(i)(DgBulkOrder.NetAmt) + GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount)
            Next
            If clsDefaultConfiguration.IsNewSalesOrder Then  'nilhil
                'lblGross.Text = "0"
                'lblDiscount.Text = "0"
                'lblNetValue.Text = "0"
                'txtTaxAmount.Text = "0"   '## added by vipin
                'CtrlTaxAmt.Text = "Tax Amt: (" + "0" + " %)"
                'Dim GrossAmt As Decimal = 0
                'Dim TotalDiscount As Decimal = 0
                'For i = 1 To GrdFixedCombo.Rows.Count - 1
                '    GrossAmt = GrossAmt + CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.Price) * 1)
                '    TotalDiscount = TotalDiscount + GrdFixedCombo.Rows(i)(DgBulkOrder.Discount)
                'Next
                'lblGross.Text = Math.Round(GrossAmt, 2)

                Dim MaxTax As Double = 0
                Dim txtAmount As Double = 0
                Dim TotTax As Decimal = 0
                Dim preTaxPer As Decimal = 0
                Dim IsAllTaxClash As Boolean = True
                For i = 1 To GrdFixedCombo.Rows.Count - 1
                    If i <> 1 Then
                        If preTaxPer <> GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                            IsAllTaxClash = False
                        End If
                    End If
                    preTaxPer = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                Next

                For i = 1 To GrdFixedCombo.Rows.Count - 1
                    If MaxTax > GrdFixedCombo.Rows(i)(DgBulkOrder.Tax) Then
                        TotTax = CDbl(TotTax)
                        '  MaxTax = MaxTax
                    Else
                        MaxTax = GrdFixedCombo.Rows(i)(DgBulkOrder.Tax)
                        TotTax = CDbl(GrdFixedCombo.Rows(i)(DgBulkOrder.TaxAmount))
                    End If
                Next

                'txtTaxAmount.Text = Math.Round((CDbl((GrossAmt) - (TotalDiscount)) * CDbl(MaxTax)) / 100, 2)
                '' End If
                'lblNetValue.Text = (CDbl(txtTaxAmount.Text) + CDbl(GrossAmt) - (TotalDiscount)) '## Commnted and added by vipin 
                'CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)"
                TxtFixedPriceEnter.Text = Math.Round(CDbl(lblGross.Text))

                txtTaxAmount.Text = Math.Round(((CDbl(TxtFixedPriceEnter.Text) - CDbl(lblDiscount.Text)) * MaxTax) / 100, 2)
                lblNetValue.Text = CDbl(txtTaxAmount.Text) + CDbl(TxtFixedPriceEnter.Text) - CDbl(lblDiscount.Text)
                CtrlTaxAmt.Text = "Tax Amt: (" + MaxTax.ToString + " %)" 'vipin
                If GrdFixedCombo.Rows.Count = 1 Then
                    'lblGross.Visible = False
                    'lblDiscount.Visible = False
                    'lblNetValue.Visible = False
                    'CtrlTaxAmt.Visible = False
                    'CtrlDiscount.Visible = False
                    'ctrlNetValue.Visible = False
                    'CtrlGross.Visible = False
                    'txtTaxAmount.Visible = False
                    'ctrlNetValue.Visible = False 'vipin
                    'TxtFixedPriceEnter.Visible = False

                    lblGross.Text = 0
                    lblDiscount.Text = 0
                    lblNetValue.Text = 0
                    txtTaxAmount.Text = 0
                    TxtFixedPriceEnter.Enabled = False
                    TxtFixedPriceEnter.Visible = True
                    TxtFixedPriceEnter.Text = 0
                    'CtrlDiscount.Visible = False
                    'ctrlNetValue.Visible = False
                    'CtrlGross.Visible = False
                    'txtTaxAmount.Visible = False
                    'ctrlNetValue.Visible = False 'vipin

                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function DeleteBulkCombo(combosrNo As Integer, ByVal articleCode As String) As Boolean

        Try

            If DtSoBulkComboHdr.Rows.Count > 0 Then

                Dim drDtl() = DtSoBulkComboDtl.Select("ComboSrNo=" & combosrNo & " and articlecode='" & articleCode & "'")
                If drDtl.Count > 0 Then

                    If Not IsEdit Then
                        For Each row As DataRow In drDtl
                            DtSoBulkComboDtl.Rows.Remove(row)
                        Next
                    Else
                        If drDtl(0)("Status") = True Then
                            drDtl(0)("Status") = False
                        End If
                        For Each row As DataRow In drDtl
                            DtSoBulkComboDtl.Rows.Remove(row)
                        Next

                    End If
                    DtSoBulkComboDtl.AcceptChanges()
                End If
                DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
                If (DgBulkComboGrid.Rows.Count > 1) Then
                    DgBulkComboGrid.Select(DgBulkComboGrid.Rows.Count - 1, 2)

                End If
            Else
                DgBulkComboGrid.Rows.Remove(DgBulkComboGrid.Row)
            End If
            For index = 1 To DgBulkComboGrid.Rows.Count - 1
                DgBulkComboGrid.Rows(index)(DgBulkOrder.SrNo) = index
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Private Function DeleteBulkComboFixed(combosrNo As Integer, ByVal articleCode As String) As Boolean

        Try

            If DtSoBulkComboHdr.Rows.Count > 0 Then

                Dim drDtl() = DtSoBulkComboDtl.Select("ComboSrNo=" & combosrNo & " and articlecode='" & articleCode & "'")
                If drDtl.Count > 0 Then

                    If Not IsEdit Then
                        For Each row As DataRow In drDtl
                            DtSoBulkComboDtl.Rows.Remove(row)
                        Next
                    Else
                        If drDtl(0)("Status") = True Then
                            drDtl(0)("Status") = False
                        End If
                        For Each row As DataRow In drDtl
                            DtSoBulkComboDtl.Rows.Remove(row)
                        Next

                    End If
                    DtSoBulkComboDtl.AcceptChanges()
                End If
                GrdFixedCombo.Rows.Remove(GrdFixedCombo.Row)
                If (GrdFixedCombo.Rows.Count > 1) Then
                    GrdFixedCombo.Select(GrdFixedCombo.Rows.Count - 1, 2)

                End If
            Else
                GrdFixedCombo.Rows.Remove(GrdFixedCombo.Row)
            End If
            For index = 1 To GrdFixedCombo.Rows.Count - 1
                GrdFixedCombo.Rows(index)(DgBulkOrder.SrNo) = index
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Sub DgBulkComboGrid_BeforeEdit(sender As Object, e As RowColEventArgs) Handles DgBulkComboGrid.BeforeEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            If (DgBulkComboGrid.Cols(CurrentCell).Name = "Wt. Per Piece(KG)") Then
                If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Packageduom) = "KGS" Then
                    e.Cancel = True
                End If
                'If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Weight) = "-" Then
                '    e.Cancel = True
                'End If
                If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Packageduom) = "NOS" Then
                    If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.BaseUOM) = "KGS" And DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Weight) > 0 Then
                        If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.IsSysQty) Then
                            e.Cancel = True
                        End If
                    End If
                    If DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.Packageduom) = DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.BaseUOM) Then
                        e.Cancel = True
                    End If
                End If
                Dim isHeader As String = IIf(DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.IsSysQty) Is DBNull.Value, 0, DgBulkComboGrid.Item(CurrentRow, DgBulkOrder.IsSysQty))
                If isHeader = "True" Then
                    e.Cancel = True
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtRemarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRemarks.KeyDown
        'If e.KeyCode = Keys.Space Then
        '    Dim str = txtRemarks.Text
        '    Dim data As String = str
        '    Dim word = data.Split(" ")
        '    Dim temp = word(word.Length - 1)
        '    If Not temp = String.Empty Then
        '        temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToLower()
        '        word(word.Length - 1) = temp
        '        str = String.Join(" ", word)
        '        txtRemarks.Text = str
        '        txtRemarks.SelectionStart = txtRemarks.Text.Length
        '    End If

        'End If
        If e.KeyCode = Keys.Enter Then
            txtRemarks.Focus()
            txtRemarks.SelectionStart = txtRemarks.Text.Length
        End If
    End Sub
    Private Sub txtRemarks_leave(sender As Object, e As EventArgs) Handles txtRemarks.Leave
        Try
            'sender.Text = CapitalValidation(sender.Text)
            sender.Text = objClsCommon.CapitalValidationStatement(sender.Text)
            sender.SelectionStart = sender.Text.Length
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        btnAddBulkCombo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAddBulkCombo.BackColor = Color.Transparent
        btnAddBulkCombo.BackColor = Color.FromArgb(0, 107, 163)
        btnAddBulkCombo.ForeColor = Color.FromArgb(255, 255, 255)
        btnAddBulkCombo.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnAddBulkCombo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAddBulkCombo.FlatStyle = FlatStyle.Flat
        btnAddBulkCombo.FlatAppearance.BorderSize = 0
        btnAddBulkCombo.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnAddBulkCombo.TextAlign = ContentAlignment.MiddleCenter

        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnCancel.TextAlign = ContentAlignment.MiddleCenter

        btnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnClear.BackColor = Color.Transparent
        btnClear.BackColor = Color.FromArgb(0, 107, 163)
        btnClear.ForeColor = Color.FromArgb(255, 255, 255)
        btnClear.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.FlatAppearance.BorderSize = 0
        btnClear.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnClear.TextAlign = ContentAlignment.MiddleCenter

        btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnSave.TextAlign = ContentAlignment.MiddleCenter

        rbnCombo.ForeColor = Color.White
        rbnCombo.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        rbnSingle.ForeColor = Color.White
        rbnSingle.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        lblCopyFrom.ForeColor = Color.Black
        lblCopyFrom.AutoSize = False
        lblCopyFrom.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblCopyFrom.BorderStyle = BorderStyle.None
        lblCopyFrom.BackColor = Color.FromArgb(212, 212, 212)
        lblCopyFrom.SendToBack()
        lblCopyFrom.Size = New Size(115, 23)

        lblPrintName.ForeColor = Color.Black
        lblPrintName.AutoSize = False
        lblPrintName.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblPrintName.BorderStyle = BorderStyle.None
        lblPrintName.BackColor = Color.FromArgb(212, 212, 212)
        lblPrintName.SendToBack()
        lblPrintName.Size = New Size(115, 23)

        CtrlLabel1.ForeColor = Color.Black
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.SendToBack()
        CtrlLabel1.Size = New Size(115, 22)
        ' CtrlLabel1.Location = New Point(29, 3)

        CtrlLabel2.ForeColor = Color.Black
        CtrlLabel2.AutoSize = False
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel2.BorderStyle = BorderStyle.None
        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.SendToBack()
        CtrlLabel2.Size = New Size(115, 22)
        CtrlLabel2.Location = New Point(29, 39)

        'DgBulkComboGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        'DgBulkComboGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        'DgBulkComboGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        'DgBulkComboGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        'DgBulkComboGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'DgBulkComboGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'DgBulkComboGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'DgBulkComboGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'DgBulkComboGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        CtrlGross.BackColor = Color.Transparent



        CtrlDiscount.BackColor = Color.Transparent
        CtrlTaxAmt.BackColor = Color.Transparent
        ctrlNetValue.BackColor = Color.Transparent


        lblGross.BackColor = Color.Transparent
        lblDiscount.BackColor = Color.Transparent
        txtTaxAmount.BackColor = Color.Transparent
        lblNetValue.BackColor = Color.Transparent
        '  TxtFixedPriceEnter.BackColor = Color.Transparent




    End Function

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles CboVariable.CheckedChanged

        If CboVariable.Checked = True Then 'vipin
            TxtFixedPriceEnter.Visible = False
            GrdFixedCombo.Visible = False
            DgBulkComboGrid.Visible = True
            TxtFixedPriceEnter.Visible = False
            lblGross.Visible = True
        Else
            TxtFixedPriceEnter.Visible = True
            GrdFixedCombo.Visible = True
            DgBulkComboGrid.Visible = False
            TxtFixedPriceEnter.Visible = True
            lblGross.Visible = False
        End If
    End Sub
    Private Sub TxtFixedPriceEnter_Leave(sender As Object, e As EventArgs) Handles TxtFixedPriceEnter.Leave
        If TxtFixedPriceEnter.Text.Trim = "" Then
            TxtFixedPriceEnter.Text = "0"
        End If
    End Sub
    Private Sub CboVariable_MouseClick(sender As Object, e As MouseEventArgs) Handles CboVariable.MouseClick

        If CboVariable.Checked = True Then 'vipin
            If GrdFixedCombo.Rows.Count > 1 Then
                Dim result As Integer = MessageBox.Show("If you change the combo type,all the data entered will be lost.Do you want to continue?", "Combo", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    GrdFixedCombo.Visible = False
                    DgBulkComboGrid.Visible = True
                    TxtFixedPriceEnter.Visible = False
                    lblGross.Visible = True

                    lblGross.Text = 0
                    lblDiscount.Text = 0
                    txtTaxAmount.Text = 0
                    lblNetValue.Text = 0
                    TxtFixedPriceEnter.Text = 0


                    clearformFixed()
                    dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, 0) '2=packaging box print name
                    PopulateComboBox1(dtPackagingPrintBox, cboPrintName)
                    pC1ComboSetDisplayMember(cboPrintName)
                    cboPrintName.SelectedIndex = -1

                    cboCopyFrom.Text = ""
                    Dim drPackagingcopiedfromTemp As DataRow() = dtPackagingcopiedfrom.Select("PackagingBoxPrintName like '%Snacks%'")
                    Dim dtPackagingcopiedfromTemp As DataTable
                    If drPackagingcopiedfromTemp.Length > 0 Then
                        dtPackagingcopiedfromTemp = drPackagingcopiedfromTemp.CopyToDataTable
                        cboCopyFrom.DataSource = dtPackagingcopiedfromTemp
                        cboCopyFrom.ValueMember = dtPackagingcopiedfromTemp.Columns("ComboSrNo").ColumnName
                        cboCopyFrom.DisplayMember = dtPackagingcopiedfromTemp.Columns("PackagingBoxPrintName").ColumnName
                        pC1ComboSetDisplayMember(cboCopyFrom)
                    Else
                        cboCopyFrom.DataSource = Nothing
                        ' pC1ComboSetDisplayMember(cboCopyFrom)
                    End If
                Else
                    CboFixed.Checked = True
                End If
            Else
                GrdFixedCombo.Visible = False
                DgBulkComboGrid.Visible = True
                TxtFixedPriceEnter.Visible = False
                lblGross.Visible = True

                lblGross.Text = 0
                lblDiscount.Text = 0
                txtTaxAmount.Text = 0
                lblNetValue.Text = 0
                TxtFixedPriceEnter.Text = 0

                dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, 0) '2=packaging box print name
                PopulateComboBox1(dtPackagingPrintBox, cboPrintName)
                pC1ComboSetDisplayMember(cboPrintName)
                cboPrintName.SelectedIndex = -1

                cboCopyFrom.Text = ""
                Dim drPackagingcopiedfromTemp As DataRow() = dtPackagingcopiedfrom.Select("PackagingBoxPrintName like '%Snacks%'")
                Dim dtPackagingcopiedfromTemp As DataTable
                If drPackagingcopiedfromTemp.Length > 0 Then
                    dtPackagingcopiedfromTemp = drPackagingcopiedfromTemp.CopyToDataTable
                    cboCopyFrom.DataSource = dtPackagingcopiedfromTemp
                    cboCopyFrom.ValueMember = dtPackagingcopiedfromTemp.Columns("ComboSrNo").ColumnName
                    cboCopyFrom.DisplayMember = dtPackagingcopiedfromTemp.Columns("PackagingBoxPrintName").ColumnName
                    pC1ComboSetDisplayMember(cboCopyFrom)
                Else
                    cboCopyFrom.DataSource = Nothing
                    ' pC1ComboSetDisplayMember(cboCopyFrom)
                End If
            End If
        End If
    End Sub

    Private Sub CboFixed_MouseClick(sender As Object, e As MouseEventArgs) Handles CboFixed.MouseClick
        If DgBulkComboGrid.Rows.Count > 1 Then
            Dim result As Integer = MessageBox.Show("If you change the combo type,all the data entered will be lost.Do you want to continue?", "Combo", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                GrdFixedCombo.Visible = True
                DgBulkComboGrid.Visible = False
                TxtFixedPriceEnter.Visible = True
                lblGross.Visible = False

                lblGross.Text = 0
                lblDiscount.Text = 0
                txtTaxAmount.Text = 0
                lblNetValue.Text = 0
                TxtFixedPriceEnter.Text = 0
                clearform()

                dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, 1) '2=packaging box print name
                PopulateComboBox1(dtPackagingPrintBox, cboPrintName)
                pC1ComboSetDisplayMember(cboPrintName)
                cboPrintName.SelectedIndex = -1
                TxtFixedPriceEnter.Enabled = False

                cboCopyFrom.Text = ""
                Dim drPackagingcopiedfromTemp As DataRow() = dtPackagingcopiedfrom.Select("PackagingBoxPrintName like '%Assorted%'")
                Dim dtPackagingcopiedfromTemp As DataTable
                If drPackagingcopiedfromTemp.Length > 0 Then
                    dtPackagingcopiedfromTemp = drPackagingcopiedfromTemp.CopyToDataTable
                    cboCopyFrom.DataSource = dtPackagingcopiedfromTemp
                    cboCopyFrom.ValueMember = dtPackagingcopiedfromTemp.Columns("ComboSrNo").ColumnName
                    cboCopyFrom.DisplayMember = dtPackagingcopiedfromTemp.Columns("PackagingBoxPrintName").ColumnName
                    pC1ComboSetDisplayMember(cboCopyFrom)
                Else
                    cboCopyFrom.DataSource = Nothing
                    '   pC1ComboSetDisplayMember(cboCopyFrom)
                End If
            Else
                CboVariable.Checked = True
            End If
        Else
            GrdFixedCombo.Visible = True
            DgBulkComboGrid.Visible = False
            TxtFixedPriceEnter.Visible = True
            lblGross.Visible = False

            lblGross.Text = 0
            lblDiscount.Text = 0
            txtTaxAmount.Text = 0
            lblNetValue.Text = 0
            TxtFixedPriceEnter.Text = 0

            dtPackagingPrintBox = objClsCommon.GetPackagingBoxSelection(clsAdmin.SiteCode, 2, 1) '2=packaging box print name
            PopulateComboBox1(dtPackagingPrintBox, cboPrintName)
            pC1ComboSetDisplayMember(cboPrintName)
            cboPrintName.SelectedIndex = -1
            TxtFixedPriceEnter.Enabled = False

            cboCopyFrom.Text = ""
            Dim drPackagingcopiedfromTemp As DataRow() = dtPackagingcopiedfrom.Select("PackagingBoxPrintName like '%Assorted%'")
            Dim dtPackagingcopiedfromTemp As DataTable
            If drPackagingcopiedfromTemp.Length > 0 Then
                dtPackagingcopiedfromTemp = drPackagingcopiedfromTemp.CopyToDataTable
                cboCopyFrom.DataSource = dtPackagingcopiedfromTemp
                cboCopyFrom.ValueMember = dtPackagingcopiedfromTemp.Columns("ComboSrNo").ColumnName
                cboCopyFrom.DisplayMember = dtPackagingcopiedfromTemp.Columns("PackagingBoxPrintName").ColumnName
                pC1ComboSetDisplayMember(cboCopyFrom)
            Else
                cboCopyFrom.DataSource = Nothing
                '    pC1ComboSetDisplayMember(cboCopyFrom)
            End If

        End If
    End Sub

    Private Sub TxtFixedPriceEnter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFixedPriceEnter.KeyPress
        If (Not Char.IsDigit(e.KeyChar)) AndAlso Not (e.KeyChar = Convert.ToChar(8)) Then
            e.Handled = True
        End If

    End Sub
    Private Sub TxtFixedPriceEnter_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtFixedPriceEnter.KeyUp
        If e.KeyCode <> Keys.Enter Then
            Call ClaculateFixedCombo()
        End If
    End Sub

    Private Sub CboFixed_CheckedChanged(sender As Object, e As EventArgs) Handles CboFixed.CheckedChanged

    End Sub

    Private Sub txtRemarks_TextChanged(sender As Object, e As EventArgs) Handles txtRemarks.TextChanged

    End Sub

    Private Sub lblPrintName_Click(sender As Object, e As EventArgs) Handles lblPrintName.Click

    End Sub
End Class