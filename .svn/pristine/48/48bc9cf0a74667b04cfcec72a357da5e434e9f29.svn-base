Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.Text
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode
Public Class clsSalesOrderPC

    Dim objComn As New clsCommon
    Dim objCM As New clsCashMemo

    Dim SqlTrans As SqlTransaction

    Dim Sqlda, daScan As New SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlcmd As SqlCommand
    Dim Sqlds, dsScan As New DataSet
    Dim Sqldr As DataRow
    Dim dv As DataView
    Dim vStmtQry As New System.Text.StringBuilder
    Dim SalesInvcNo As String = ""
    Dim stmtSalesStatus As String = "('Cancel','Closed','Return')"
    Dim _SOCreation As String
    Public Property SOCreation() As String
        Get
            Return _SOCreation
        End Get
        Set(ByVal value As String)
            _SOCreation = value
        End Set
    End Property

    Public Shared _SelectedCurrencyIndex As String
    Public Property SelectedCurrencyIndex() As String
        Get
            SelectedCurrencyIndex = _SelectedCurrencyIndex
        End Get
        Set(value As String)
            _SelectedCurrencyIndex = value
        End Set
    End Property


    Public Shared _iCurrencyCode As String
    Public Property CurrencyCode() As String
        Get
            CurrencyCode = _iCurrencyCode
        End Get
        Set(ByVal value As String)
            _iCurrencyCode = value
        End Set

    End Property

    Dim _SOReturn As String
    Public Property SOReturn() As String
        Get
            Return _SOReturn
        End Get
        Set(ByVal value As String)
            _SOReturn = value
        End Set
    End Property

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

    Private _IsStrGenerate As Boolean
    Public Property IsStrGenerate() As String
        Get
            Return _IsStrGenerate
        End Get
        Set(ByVal value As String)
            _IsStrGenerate = value
        End Set
    End Property

    Private _IsMannualClose As Boolean
    Public Property IsMannualClose() As Boolean
        Get
            Return _IsMannualClose
        End Get
        Set(ByVal value As Boolean)
            _IsMannualClose = value
        End Set
    End Property

    ''' <summary>
    ''' Set Table Structure for Item Scanning
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetCollectionOfItems() As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select '' as DEL,  " & vbCrLf)
            vStmtQry.Append(" '' as PLUS,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as UOM, " & vbCrLf)

            vStmtQry.Append(" Convert(numeric(18,2),0) as Quantity, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxPer, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as NetAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PckgSize, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PckgQty, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as PackagingMaterial, " & vbCrLf)

            vStmtQry.Append(" Convert(numeric(18,2),0) as PickUpQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ArticleLevelTax, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)

            vStmtQry.Append(" Convert(bit,'True') as Status, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalDiscPercentage, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxInPer, " & vbCrLf) '' $$ added by ketan
            vStmtQry.Append(" Convert(numeric(18,2),0) as ExclTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxInPer, " & vbCrLf) '' $$ added by nikhil
            vStmtQry.Append(" getdate() as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Stock, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCLP, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as ReservedQty, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as Reserved, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as RowIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as DeliveryIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeOB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSR, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)

            vStmtQry.Append(" Convert(numeric(18,2),0) as GrossAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as MinPayAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as IncTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as IsStatus, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPPoints, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as AuthUserId, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as AuthUserRemarks, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(250),'') as ReturnReasonCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as PromotionId, " & vbCrLf) 'PromotionId, OfferId 
            vStmtQry.Append(" Convert(numeric(18,2),0) as LineDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as FirstLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as TopLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as LastNodeCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as FirstLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TopLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as SalesStaffID, " & vbCrLf)
            'change to track the pick Amt by ram 
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalPickUpAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as BillLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as BatchBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as DeliverySiteCode, " & vbCrLf)
            'change to track the pick Amt
            vStmtQry.Append(" Convert(Varchar(35),'') as EditBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(85),'') as ArticleDiscription," & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CostAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsCombo, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsImageReq, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsDelivery, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsSTR, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsHeader, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsNew" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "ItemScanDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCollectionOfPackagingMaterial() As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(15),'') as SiteCode,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as SaleOrderNumber, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as FinYear, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as UOM, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as PkgLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Quantity, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ArticleLevelTax, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxPer, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as NetAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PckgSize, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PckgQty, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as PackagingMaterial, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingBoxCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PickUpQty, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as ArticleLevelTax, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)


            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalDiscPercentage, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxInPer, " & vbCrLf) '' added by ketan $$ FOR SO GST CHANGES 
            vStmtQry.Append(" Convert(numeric(18,2),0) as ExclTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TaxInPer, " & vbCrLf) '' $$ added by nikhilss
            vStmtQry.Append(" getdate() as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Stock, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCLP, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as ReservedQty, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as Reserved, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as RowIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as DeliveryIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeOB, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as FreezeSR, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)

            vStmtQry.Append(" Convert(numeric(18,2),0) as GrossAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as MinPayAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as IncTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as IsStatus, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPPoints, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CLPDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as AuthUserId, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as AuthUserRemarks, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(250),'') as ReturnReasonCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as PromotionId, " & vbCrLf) 'PromotionId, OfferId 
            vStmtQry.Append(" Convert(numeric(18,2),0) as LineDiscount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(40),'') as FirstLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as TopLevel, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as LastNodeCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as FirstLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TopLevelDISC, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(20),'') as SalesStaffID, " & vbCrLf)
            'change to track the pick Amt by ram 
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalPickUpAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as BillLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as BatchBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as DeliverySiteCode, " & vbCrLf)
            'change to track the pick Amt
            vStmtQry.Append(" Convert(Varchar(35),'') as EditBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(85),'') as ArticleDiscription," & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CostAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsCombo, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsImageReq, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsDelivery ," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsSTR, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),'') as PackageBaseUOM," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(26),'') as PackagedEAN," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsHeader, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsNew" & vbCrLf)







            'vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)

            'vStmtQry.Append(" Convert(numeric(18,2),0) as PckgSize, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as PckgQty, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(50),'') as PackagingType, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(200),'') as PackagingMaterial, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as ReservedQty " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "PackagingMaterial")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCollectionOfPackagingDelivery() As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Length = 0
            vStmtQry.Append(" Select '' as DEL,  " & vbCrLf)
            vStmtQry.Append(" '' as PLUS,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as SiteCode,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as SaleOrderNumber, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as FinYear, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as UOM, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as PkgLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,0),0) as DeliveryLineNo, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as Quantity, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DispSrNo, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as NetAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as PckgSize, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as PckgQty, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as PackagingMaterial, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingBoxCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PickUpQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ArticleLevelTax, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as PendingQty, " & vbCrLf)
            vStmtQry.Append(" '' as STR,  " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalDiscPercentage, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as ExclTaxAmt, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as TotalTaxAmt, " & vbCrLf)
            vStmtQry.Append(" getdate() as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Stock, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCLP, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsHeader, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as IsCombo, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as ReservedQty, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'True') as Reserved, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as RowIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PackagingIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as DeliveryIndex, " & vbCrLf)
            vStmtQry.Append(" getdate() as LastPickDate, " & vbCrLf)
            vStmtQry.Append(" getdate() as DeliveryDate, " & vbCrLf)
            vStmtQry.Append(" getdate() as DeliveryTime, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DeliveryAddress, " & vbCrLf)


            'change to track the pick Amt by ram 

            vStmtQry.Append(" Convert(numeric(18,0),0) as BillLineNo, " & vbCrLf)

            vStmtQry.Append(" Convert(Varchar(15),'') as DeliverySiteCode, " & vbCrLf)
            'change to track the pick Amt
            vStmtQry.Append(" Convert(Varchar(35),'') as EditBarcode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(85),'') as ArticleDiscription," & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as CostAmount, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsCombo, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsImageReq, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsDelivery, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),'') as IsCustAddress, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsSTR, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsNew" & vbCrLf)





            'vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)

            'vStmtQry.Append(" Convert(numeric(18,2),0) as PckgSize, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as PckgQty, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(50),'') as PackagingType, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(200),'') as PackagingMaterial, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as DeliveredQty, " & vbCrLf)
            'vStmtQry.Append(" Convert(numeric(18,2),0) as ReservedQty " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "PackagingDelivery")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintArticleWisePaymentTableStruc() As DataTable
        'vipin GST 31.07.2017 SO Tax changes
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Numeric(18,0),0) as BillLineNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleDescription," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,2),0) as Quantity," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Price," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as NetAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as OtherCharges," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Discount," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as DiscountPer," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as TaxableAmount," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as CGST," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as CGSTValue," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as SGST," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as SGSTValue," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as GrossAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as HSNCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as IsCombo" & vbCrLf)    'vipin to hide HSN in combo

            Dim daArticleWisePayDetails As New SqlDataAdapter
            daArticleWisePayDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtArticlewisePayDetails As New DataTable
            daArticleWisePayDetails.Fill(dtArticlewisePayDetails)

            Return dtArticlewisePayDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSODiscounPer(ByVal vsaleNo As String, ByVal siteCode As String, ArticleCode As String) As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(DiscountAmount),0) as DiscountAmount,SUm (isnull(DiscountPer,0))AS DiscountPer  from SoPackagingDiscDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and Articlecode='" & ArticleCode & "'  and STATUS=1" & vbCrLf)
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function RmearksStructure() As DataTable
        Try
            vStmtQry.Length = 0

            vStmtQry.Length = 0

            vStmtQry.Append(" Select Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as ArticleName,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(1000),'') as Remark  " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dsScan As New DataTable
            daScan.Fill(dsScan)

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CopyFromStructure() As DataTable
        Try
            vStmtQry.Length = 0

            vStmtQry.Length = 0

            vStmtQry.Append(" Select Convert(Varchar(50),'') as ComboSrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(150),'') as PackagingBoxPrintName  " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dsScan As New DataTable
            daScan.Fill(dsScan)

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCollectionOfSTR() As DataTable
        Try

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(15),'') as SiteCode,  " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as SaleOrderNumber, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as FinYear, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)

            vStmtQry.Append(" Convert(Varchar(50),'') as QtyPerBox, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(200),'') as WtPerPiece, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as WtPerBox, " & vbCrLf)

            vStmtQry.Append(" Convert(Varchar(15),'') as STRUOM, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as STRQty, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as STRDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as STRTime, " & vbCrLf)

            vStmtQry.Append(" Convert(Varchar(15),'') as IsImageReq, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as DeliveryIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as StrIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as SortIndex, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsCombo, " & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as Status " & vbCrLf)


            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dsScan As New DataTable
            daScan.Fill(dsScan)

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Sales Order Table Structure Information
    ''' </summary>
    ''' <param name="vSiteCode">SiteCode</param>
    ''' <param name="vSalesNo">SalesNo</param>
    ''' <param name="vStatus">Insertion/Updation/Cancelation</param>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSOTableStruct(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "", Optional ByVal IsQuotation As Boolean = False, Optional ByVal QuotationNumber As String = "") As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From SalesOrderHDR Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And SOStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select A.*,B.FreezeSB, B.FreezeSR,B.FreezeOB,0.0 as Reserved_Qty,0.0 as Delivered_Qty From SalesOrderDTL A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 AND A.SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And ArticleStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesOrderHDRAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderDTLAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & " ' And DocumentNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderTaxDtls Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderOtherCharges Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From OrderHdr Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)
            vStmtQry.Append(" Select * From OrderDtl Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)

            vStmtQry.Append(" Select * from CLPTransaction Where SiteCode='0' And BillNo='0'; " & vbCrLf)
            vStmtQry.Append(" Select * from CLPTransactionsDetails Where SiteCode='0' And BillNo='0' And BillLineNo='0'; " & vbCrLf)

            If IsQuotation AndAlso QuotationNumber <> "" Then
                vStmtQry.Append(" select * from Quotationhdr where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
                vStmtQry.Append(" select *, b.ArticleName as Discription from Quotationdtl a inner join MSTArticle b on  a.ArticleCode= b.ArticleCode where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
            End If


            vStmtQry.Append(" Select * From SalesOrderBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            'vStmtQry.Append(" Select BCD.* From SalesOrderBulkComboHdr as BCH Inner Join SalesOrderBulkComboDtl as BCD on  BCH.SaleOrderNumber = BCD.SaleOrderNumber  Where BCH.SiteCode='" & vSiteCode & " ' And BCH.SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderBulkComboDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'order by CREATEDON; " & vbCrLf)
            vStmtQry.Append(" Select * From SOItemPackagingBoxDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingBoxDeliveryDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingDiscDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderStrDetails Where Status=1 and SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From PkgOrderDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingBoxDeliveryTempDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderSTRFactoryRemark Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' ;" & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingTaxDtl Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            'added by khusrao adil for savoy
            vStmtQry.Append("select * from SaleOrderTermNConditions Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "SalesOrderHDR"
            Sqlds.Tables(1).TableName = "SalesOrderDTL"
            Sqlds.Tables(2).TableName = "SalesOrderHDRAudit"
            Sqlds.Tables(3).TableName = "SalesOrderDTLAudit"
            Sqlds.Tables(4).TableName = "SalesInvoice"
            Sqlds.Tables(5).TableName = "SalesOrderTaxDtls"
            Sqlds.Tables(6).TableName = "SalesOrderOtherCharges"
            Sqlds.Tables(7).TableName = "OrderHdr"
            Sqlds.Tables(8).TableName = "OrderDtl"
            Sqlds.Tables(9).TableName = "CLPTransaction"
            Sqlds.Tables(10).TableName = "CLPTransactionsDetails"
            If IsQuotation AndAlso QuotationNumber <> "" Then
                Sqlds.Tables(11).TableName = "Quotationhdr"
                Sqlds.Tables(12).TableName = "Quotationdtl"
                Sqlds.Tables(13).TableName = "SalesOrderBulkComboHdr"
                Sqlds.Tables(14).TableName = "SalesOrderBulkComboDtl"
                Sqlds.Tables(15).TableName = "SOItemPackagingBoxDtl"
                Sqlds.Tables(16).TableName = "SOPackagingBoxDeliveryDtl"
                Sqlds.Tables(17).TableName = "SOPackagingDiscDtl"
                Sqlds.Tables(18).TableName = "SalesOrderStrDetails"
                Sqlds.Tables(19).TableName = "PkgOrderDtl"
                Sqlds.Tables(20).TableName = "SOPackagingBoxDeliveryTempDtl"
                Sqlds.Tables(21).TableName = "SalesOrderSTRFactoryRemark"
                Sqlds.Tables(22).TableName = "SOPackagingTaxDtl"
                Sqlds.Tables(23).TableName = "SaleOrderTermNConditions"
            Else
                Sqlds.Tables(11).TableName = "SalesOrderBulkComboHdr"
                Sqlds.Tables(12).TableName = "SalesOrderBulkComboDtl"
                Sqlds.Tables(13).TableName = "SOItemPackagingBoxDtl"
                Sqlds.Tables(14).TableName = "SOPackagingBoxDeliveryDtl"
                Sqlds.Tables(15).TableName = "SOPackagingDiscDtl"
                Sqlds.Tables(16).TableName = "SalesOrderStrDetails"
                Sqlds.Tables(17).TableName = "PkgOrderDtl"
                Sqlds.Tables(18).TableName = "SOPackagingBoxDeliveryTempDtl"
                Sqlds.Tables(19).TableName = "SalesOrderSTRFactoryRemark"
                Sqlds.Tables(20).TableName = "SOPackagingTaxDtl"
                Sqlds.Tables(21).TableName = "SaleOrderTermNConditions"
            End If

            Dim KeySOhdr(2) As DataColumn
            KeySOhdr(0) = Sqlds.Tables("SalesOrderHDR").Columns("SiteCode")
            KeySOhdr(1) = Sqlds.Tables("SalesOrderHDR").Columns("FinYear")
            KeySOhdr(2) = Sqlds.Tables("SalesOrderHDR").Columns("SaleOrderNumber")
            Sqlds.Tables("SalesOrderHDR").PrimaryKey = KeySOhdr

            Dim KeySOdtl(4) As DataColumn
            KeySOdtl(0) = Sqlds.Tables("SalesOrderDTL").Columns("SiteCode")
            KeySOdtl(1) = Sqlds.Tables("SalesOrderDTL").Columns("FinYear")
            KeySOdtl(2) = Sqlds.Tables("SalesOrderDTL").Columns("SaleOrderNumber")
            KeySOdtl(3) = Sqlds.Tables("SalesOrderDTL").Columns("EAN")
            KeySOdtl(4) = Sqlds.Tables("SalesOrderDTL").Columns("BillLineNo")
            Sqlds.Tables("SalesOrderDTL").PrimaryKey = KeySOdtl

            Dim KeySOhdrAudit(3) As DataColumn
            KeySOhdrAudit(0) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SiteCode")
            KeySOhdrAudit(1) = Sqlds.Tables("SalesOrderHDRAudit").Columns("FinYear")
            KeySOhdrAudit(2) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SaleOrderNumber")
            KeySOhdrAudit(3) = Sqlds.Tables("SalesOrderHDRAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderHDRAudit").PrimaryKey = KeySOhdrAudit

            Dim KeySOdtlAudit(4) As DataColumn
            KeySOdtlAudit(0) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SiteCode")
            KeySOdtlAudit(1) = Sqlds.Tables("SalesOrderDTLAudit").Columns("FinYear")
            KeySOdtlAudit(2) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SaleOrderNumber")
            KeySOdtlAudit(3) = Sqlds.Tables("SalesOrderDTLAudit").Columns("EAN")
            KeySOdtlAudit(4) = Sqlds.Tables("SalesOrderDTLAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderDTLAudit").PrimaryKey = KeySOdtlAudit

            Dim KeySOInvc(4) As DataColumn
            KeySOInvc(0) = Sqlds.Tables("SalesInvoice").Columns("SiteCode")
            KeySOInvc(1) = Sqlds.Tables("SalesInvoice").Columns("FinYear")
            KeySOInvc(2) = Sqlds.Tables("SalesInvoice").Columns("DocumentNumber")
            KeySOInvc(3) = Sqlds.Tables("SalesInvoice").Columns("SaleInvNumber")
            KeySOInvc(4) = Sqlds.Tables("SalesInvoice").Columns("SaleInvLineNumber")
            Sqlds.Tables("SalesInvoice").PrimaryKey = KeySOInvc

            Dim KeySOTax(4) As DataColumn
            KeySOTax(0) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SiteCode")
            KeySOTax(1) = Sqlds.Tables("SalesOrderTaxDtls").Columns("FinYear")
            KeySOTax(2) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SaleOrderNumber")
            KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("EAN")
            'KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLabel")
            KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLineNo")
            Sqlds.Tables("SalesOrderTaxDtls").PrimaryKey = KeySOTax

            Dim KeySOCharges(3) As DataColumn
            KeySOCharges(0) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SiteCode")
            KeySOCharges(1) = Sqlds.Tables("SalesOrderOtherCharges").Columns("FinYear")
            KeySOCharges(2) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SaleOrderNumber")
            KeySOCharges(3) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SerailNo")
            Sqlds.Tables("SalesOrderOtherCharges").PrimaryKey = KeySOCharges

            Dim KeyOrderHdr(2) As DataColumn
            KeyOrderHdr(0) = Sqlds.Tables("OrderHdr").Columns("SiteCode")
            KeyOrderHdr(1) = Sqlds.Tables("OrderHdr").Columns("FinYear")
            KeyOrderHdr(2) = Sqlds.Tables("OrderHdr").Columns("DocumentNumber")
            Sqlds.Tables("OrderHdr").PrimaryKey = KeyOrderHdr

            Dim KeyOrderDtl(4) As DataColumn
            KeyOrderDtl(0) = Sqlds.Tables("OrderDtl").Columns("SiteCode")
            KeyOrderDtl(1) = Sqlds.Tables("OrderDtl").Columns("FinYear")
            KeyOrderDtl(2) = Sqlds.Tables("OrderDtl").Columns("DocumentNumber")
            KeyOrderDtl(3) = Sqlds.Tables("OrderDtl").Columns("EAN")
            KeyOrderDtl(4) = Sqlds.Tables("OrderDtl").Columns("LineNumber")
            Sqlds.Tables("OrderDtl").PrimaryKey = KeyOrderDtl

            Dim KeyCLPHdr(1) As DataColumn
            KeyCLPHdr(0) = Sqlds.Tables("CLPTransaction").Columns("SiteCode")
            KeyCLPHdr(1) = Sqlds.Tables("CLPTransaction").Columns("BillNo")
            Sqlds.Tables("CLPTransaction").PrimaryKey = KeyCLPHdr

            Dim KeyCLPDtl(2) As DataColumn
            KeyCLPDtl(0) = Sqlds.Tables("CLPTransactionsDetails").Columns("SiteCode")
            KeyCLPDtl(1) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillNo")
            KeyCLPDtl(2) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillLineNo")
            Sqlds.Tables("CLPTransactionsDetails").PrimaryKey = KeyCLPDtl

            If IsQuotation AndAlso QuotationNumber <> "" Then
                Dim KeyQuhdr(2) As DataColumn
                KeyQuhdr(0) = Sqlds.Tables("Quotationhdr").Columns("SiteCode")
                KeyQuhdr(1) = Sqlds.Tables("Quotationhdr").Columns("FinYear")
                KeyQuhdr(2) = Sqlds.Tables("Quotationhdr").Columns("SaleOrderNumber")
                Sqlds.Tables("Quotationhdr").PrimaryKey = KeyQuhdr

            End If
            Dim KeySOPackVar(5) As DataColumn
            KeySOPackVar(0) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("SiteCode")
            KeySOPackVar(1) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("FinYear")
            KeySOPackVar(2) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("SaleOrderNumber")
            KeySOPackVar(3) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("EAN")
            KeySOPackVar(4) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("BillLineNo")
            KeySOPackVar(5) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("PkgLineNo")
            Sqlds.Tables("SOItemPackagingBoxDtl").PrimaryKey = KeySOPackVar

            Dim KeySOPackVarDelivery(6) As DataColumn
            KeySOPackVarDelivery(0) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("SiteCode")
            KeySOPackVarDelivery(1) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("FinYear")
            KeySOPackVarDelivery(2) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("SaleOrderNumber")
            KeySOPackVarDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("EAN")
            KeySOPackVarDelivery(4) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("BillLineNo")
            KeySOPackVarDelivery(5) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("PkgLineNo")
            KeySOPackVarDelivery(6) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("DeliveryLineNo")
            Sqlds.Tables("SOPackagingBoxDeliveryDtl").PrimaryKey = KeySOPackVarDelivery

            Dim KeySOComboHdr(3) As DataColumn
            KeySOComboHdr(0) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("SaleOrderNumber")
            KeySOComboHdr(1) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("Sitecode")
            KeySOComboHdr(2) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("FinYear")
            KeySOComboHdr(3) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("ComboSrNo")

            Sqlds.Tables("SalesOrderBulkComboHdr").PrimaryKey = KeySOComboHdr

            Dim KeySOComboDtl(4) As DataColumn
            KeySOComboDtl(0) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("SaleOrderNumber")
            KeySOComboDtl(1) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("Sitecode")
            KeySOComboDtl(2) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("FinYear")
            KeySOComboDtl(3) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("ComboSrNo")
            KeySOComboDtl(4) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("EAN")

            Sqlds.Tables("SalesOrderBulkComboDtl").PrimaryKey = KeySOComboDtl

            Dim KeySODiscDtl(6) As DataColumn

            KeySODiscDtl(0) = Sqlds.Tables("SOPackagingDiscDtl").Columns("Sitecode")
            KeySODiscDtl(1) = Sqlds.Tables("SOPackagingDiscDtl").Columns("FinYear")
            KeySODiscDtl(2) = Sqlds.Tables("SOPackagingDiscDtl").Columns("SaleOrderNumber")
            KeySODiscDtl(3) = Sqlds.Tables("SOPackagingDiscDtl").Columns("EAN")
            KeySODiscDtl(4) = Sqlds.Tables("SOPackagingDiscDtl").Columns("BillLineNo")
            KeySODiscDtl(5) = Sqlds.Tables("SOPackagingDiscDtl").Columns("PkgLineNo")
            KeySODiscDtl(6) = Sqlds.Tables("SOPackagingDiscDtl").Columns("SrNo")

            Sqlds.Tables("SOPackagingDiscDtl").PrimaryKey = KeySODiscDtl

            Dim KeySOSTRDtl(3) As DataColumn

            KeySOSTRDtl(0) = Sqlds.Tables("SalesOrderStrDetails").Columns("Sitecode")
            KeySOSTRDtl(1) = Sqlds.Tables("SalesOrderStrDetails").Columns("FinYear")
            KeySOSTRDtl(2) = Sqlds.Tables("SalesOrderStrDetails").Columns("SaleOrderNumber")
            KeySOSTRDtl(3) = Sqlds.Tables("SalesOrderStrDetails").Columns("STRIndex")
            Sqlds.Tables("SalesOrderStrDetails").PrimaryKey = KeySOSTRDtl

            Dim KeyOrderDelivery(7) As DataColumn
            KeyOrderDelivery(0) = Sqlds.Tables("PkgOrderDtl").Columns("SiteCode")
            KeyOrderDelivery(1) = Sqlds.Tables("PkgOrderDtl").Columns("FinYear")
            KeyOrderDelivery(2) = Sqlds.Tables("PkgOrderDtl").Columns("SaleOrderNumber")
            KeyOrderDelivery(3) = Sqlds.Tables("PkgOrderDtl").Columns("EAN")
            KeyOrderDelivery(4) = Sqlds.Tables("PkgOrderDtl").Columns("BillLineNo")
            KeyOrderDelivery(5) = Sqlds.Tables("PkgOrderDtl").Columns("PkgLineNo")
            KeyOrderDelivery(6) = Sqlds.Tables("PkgOrderDtl").Columns("DeliveryLineNo")
            KeyOrderDelivery(7) = Sqlds.Tables("PkgOrderDtl").Columns("DocumentNo")
            Sqlds.Tables("PkgOrderDtl").PrimaryKey = KeyOrderDelivery


            Dim KeySOPackVarTempDelivery(6) As DataColumn
            KeySOPackVarTempDelivery(0) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("SiteCode")
            KeySOPackVarTempDelivery(1) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("FinYear")
            KeySOPackVarTempDelivery(2) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("SaleOrderNumber")
            KeySOPackVarTempDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("EAN")
            KeySOPackVarTempDelivery(4) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("BillLineNo")
            KeySOPackVarTempDelivery(5) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("PkgLineNo")
            KeySOPackVarTempDelivery(6) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("DeliveryLineNo")
            Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").PrimaryKey = KeySOPackVarTempDelivery

            Dim KeySTRFactory(3) As DataColumn

            KeySTRFactory(0) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("Sitecode")
            KeySTRFactory(1) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("FinYear")
            KeySTRFactory(2) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("SaleOrderNumber")
            KeySTRFactory(3) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("FactorySiteCode")
            Sqlds.Tables("SalesOrderSTRFactoryRemark").PrimaryKey = KeySTRFactory


            Dim KeySOTaxDtl(6) As DataColumn

            KeySOTaxDtl(0) = Sqlds.Tables("SOPackagingTaxDtl").Columns("Sitecode")
            KeySOTaxDtl(1) = Sqlds.Tables("SOPackagingTaxDtl").Columns("FinYear")
            KeySOTaxDtl(2) = Sqlds.Tables("SOPackagingTaxDtl").Columns("SaleOrderNumber")
            KeySOTaxDtl(3) = Sqlds.Tables("SOPackagingTaxDtl").Columns("EAN")
            KeySOTaxDtl(4) = Sqlds.Tables("SOPackagingTaxDtl").Columns("BillLineNo")
            KeySOTaxDtl(5) = Sqlds.Tables("SOPackagingTaxDtl").Columns("PkgLineNo")
            KeySOTaxDtl(6) = Sqlds.Tables("SOPackagingTaxDtl").Columns("SrNo")

            Sqlds.Tables("SOPackagingTaxDtl").PrimaryKey = KeySOTaxDtl

            Dim KeySaleOrderTermNConditions(4) As DataColumn
            KeySaleOrderTermNConditions(0) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SiteCode")
            KeySaleOrderTermNConditions(1) = Sqlds.Tables("SaleOrderTermNConditions").Columns("FinYear")
            KeySaleOrderTermNConditions(2) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SaleOrderNumber")
            KeySaleOrderTermNConditions(3) = Sqlds.Tables("SaleOrderTermNConditions").Columns("TnCcode")
            KeySaleOrderTermNConditions(4) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SrNo")
            Sqlds.Tables("SaleOrderTermNConditions").PrimaryKey = KeySaleOrderTermNConditions

            Return Sqlds
        Catch SqlEx As SqlException
            LogException(SqlEx)
            If SqlEx.Number = -2 Then
                Throw New ApplicationException("Server is not Responding")
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOTableStructEdit(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "", Optional ByVal IsQuotation As Boolean = False, Optional ByVal QuotationNumber As String = "") As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From SalesOrderHDR Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And SOStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select A.*,B.FreezeSB, B.FreezeSR,B.FreezeOB,0.0 as Reserved_Qty,0.0 as Delivered_Qty From SalesOrderDTL A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 AND A.SiteCode='" & vSiteCode & "' " & vbCrLf)
            'If vStatus = "Cancel" Then
            '    vStmtQry.Append(" And ArticleStatus Not In " & stmtSalesStatus & vbCrLf)
            'End If
            vStmtQry.Append(" And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesOrderHDRAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderDTLAudit Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & " ' And DocumentNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderTaxDtls Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderOtherCharges Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select * From OrderHdr Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)
            vStmtQry.Append(" Select * From OrderDtl Where SiteCode='" & vSiteCode & " ' And DocumentNumber='0'; " & vbCrLf)

            vStmtQry.Append(" Select * from CLPTransaction Where SiteCode='0' And BillNo='0'; " & vbCrLf)
            vStmtQry.Append(" Select * from CLPTransactionsDetails Where SiteCode='0' And BillNo='0' And BillLineNo='0'; " & vbCrLf)

            If IsQuotation AndAlso QuotationNumber <> "" Then
                vStmtQry.Append(" select * from Quotationhdr where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
                vStmtQry.Append(" select *, b.ArticleName as Discription from Quotationdtl a inner join MSTArticle b on  a.ArticleCode= b.ArticleCode where SaleOrderNumber='" & QuotationNumber & "'; " & vbCrLf)
            End If


            vStmtQry.Append(" Select * From SalesOrderBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            'vStmtQry.Append(" Select BCD.* From SalesOrderBulkComboHdr as BCH Inner Join SalesOrderBulkComboDtl as BCD on  BCH.SaleOrderNumber = BCD.SaleOrderNumber  Where BCH.SiteCode='" & vSiteCode & " ' And BCH.SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderBulkComboDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'order by CREATEDON; " & vbCrLf)
            vStmtQry.Append(" Select * From SOItemPackagingBoxDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingBoxDeliveryDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingDiscDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderStrDetails Where Status=1 and SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From PkgOrderDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingBoxDeliveryTempDtl Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From SalesOrderSTRFactoryRemark Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' ;" & vbCrLf)
            vStmtQry.Append(" Select * From SOPackagingTaxDtl Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            'added by khusrao adil for savoy
            vStmtQry.Append("select * from SaleOrderTermNConditions Where SiteCode='" & vSiteCode & "' And SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "SalesOrderHDR"
            Sqlds.Tables(1).TableName = "SalesOrderDTL"
            Sqlds.Tables(2).TableName = "SalesOrderHDRAudit"
            Sqlds.Tables(3).TableName = "SalesOrderDTLAudit"
            Sqlds.Tables(4).TableName = "SalesInvoice"
            Sqlds.Tables(5).TableName = "SalesOrderTaxDtls"
            Sqlds.Tables(6).TableName = "SalesOrderOtherCharges"
            Sqlds.Tables(7).TableName = "OrderHdr"
            Sqlds.Tables(8).TableName = "OrderDtl"
            Sqlds.Tables(9).TableName = "CLPTransaction"
            Sqlds.Tables(10).TableName = "CLPTransactionsDetails"
            If IsQuotation AndAlso QuotationNumber <> "" Then
                Sqlds.Tables(11).TableName = "Quotationhdr"
                Sqlds.Tables(12).TableName = "Quotationdtl"
                Sqlds.Tables(13).TableName = "SalesOrderBulkComboHdr"
                Sqlds.Tables(14).TableName = "SalesOrderBulkComboDtl"
                Sqlds.Tables(15).TableName = "SOItemPackagingBoxDtl"
                Sqlds.Tables(16).TableName = "SOPackagingBoxDeliveryDtl"
                Sqlds.Tables(17).TableName = "SOPackagingDiscDtl"
                Sqlds.Tables(18).TableName = "SalesOrderStrDetails"
                Sqlds.Tables(19).TableName = "PkgOrderDtl"
                Sqlds.Tables(20).TableName = "SOPackagingBoxDeliveryTempDtl"
                Sqlds.Tables(21).TableName = "SalesOrderSTRFactoryRemark"
                Sqlds.Tables(22).TableName = "SOPackagingTaxDtl"
                Sqlds.Tables(23).TableName = "SaleOrderTermNConditions"
            Else
                Sqlds.Tables(11).TableName = "SalesOrderBulkComboHdr"
                Sqlds.Tables(12).TableName = "SalesOrderBulkComboDtl"
                Sqlds.Tables(13).TableName = "SOItemPackagingBoxDtl"
                Sqlds.Tables(14).TableName = "SOPackagingBoxDeliveryDtl"
                Sqlds.Tables(15).TableName = "SOPackagingDiscDtl"
                Sqlds.Tables(16).TableName = "SalesOrderStrDetails"
                Sqlds.Tables(17).TableName = "PkgOrderDtl"
                Sqlds.Tables(18).TableName = "SOPackagingBoxDeliveryTempDtl"
                Sqlds.Tables(19).TableName = "SalesOrderSTRFactoryRemark"
                Sqlds.Tables(20).TableName = "SOPackagingTaxDtl"
                Sqlds.Tables(21).TableName = "SaleOrderTermNConditions"
            End If

            Dim KeySOhdr(2) As DataColumn
            KeySOhdr(0) = Sqlds.Tables("SalesOrderHDR").Columns("SiteCode")
            KeySOhdr(1) = Sqlds.Tables("SalesOrderHDR").Columns("FinYear")
            KeySOhdr(2) = Sqlds.Tables("SalesOrderHDR").Columns("SaleOrderNumber")
            Sqlds.Tables("SalesOrderHDR").PrimaryKey = KeySOhdr

            Dim KeySOdtl(3) As DataColumn
            KeySOdtl(0) = Sqlds.Tables("SalesOrderDTL").Columns("SiteCode")
            KeySOdtl(1) = Sqlds.Tables("SalesOrderDTL").Columns("FinYear")
            KeySOdtl(2) = Sqlds.Tables("SalesOrderDTL").Columns("SaleOrderNumber")
            ' KeySOdtl(3) = Sqlds.Tables("SalesOrderDTL").Columns("EAN")
            KeySOdtl(3) = Sqlds.Tables("SalesOrderDTL").Columns("BillLineNo")
            Sqlds.Tables("SalesOrderDTL").PrimaryKey = KeySOdtl

            Dim KeySOhdrAudit(3) As DataColumn
            KeySOhdrAudit(0) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SiteCode")
            KeySOhdrAudit(1) = Sqlds.Tables("SalesOrderHDRAudit").Columns("FinYear")
            KeySOhdrAudit(2) = Sqlds.Tables("SalesOrderHDRAudit").Columns("SaleOrderNumber")
            KeySOhdrAudit(3) = Sqlds.Tables("SalesOrderHDRAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderHDRAudit").PrimaryKey = KeySOhdrAudit

            Dim KeySOdtlAudit(3) As DataColumn
            KeySOdtlAudit(0) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SiteCode")
            KeySOdtlAudit(1) = Sqlds.Tables("SalesOrderDTLAudit").Columns("FinYear")
            KeySOdtlAudit(2) = Sqlds.Tables("SalesOrderDTLAudit").Columns("SaleOrderNumber")
            ' KeySOdtlAudit(3) = Sqlds.Tables("SalesOrderDTLAudit").Columns("EAN")
            KeySOdtlAudit(3) = Sqlds.Tables("SalesOrderDTLAudit").Columns("AmendedNo")
            Sqlds.Tables("SalesOrderDTLAudit").PrimaryKey = KeySOdtlAudit

            Dim KeySOInvc(4) As DataColumn
            KeySOInvc(0) = Sqlds.Tables("SalesInvoice").Columns("SiteCode")
            KeySOInvc(1) = Sqlds.Tables("SalesInvoice").Columns("FinYear")
            KeySOInvc(2) = Sqlds.Tables("SalesInvoice").Columns("DocumentNumber")
            KeySOInvc(3) = Sqlds.Tables("SalesInvoice").Columns("SaleInvNumber")
            KeySOInvc(4) = Sqlds.Tables("SalesInvoice").Columns("SaleInvLineNumber")
            Sqlds.Tables("SalesInvoice").PrimaryKey = KeySOInvc

            'Dim KeySOTax(3) As DataColumn
            'KeySOTax(0) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SiteCode")
            'KeySOTax(1) = Sqlds.Tables("SalesOrderTaxDtls").Columns("FinYear")
            'KeySOTax(2) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SaleOrderNumber")
            ' ''' KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("EAN")
            ''KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLabel")
            'KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLineNo")
            'Sqlds.Tables("SalesOrderTaxDtls").PrimaryKey = KeySOTax

            'vipin PC SO Merge 03-05-2018
            Dim KeySOTax(4) As DataColumn 'vipin PC SO Merge 03-05-2018
            KeySOTax(0) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SiteCode")
            KeySOTax(1) = Sqlds.Tables("SalesOrderTaxDtls").Columns("FinYear")
            KeySOTax(2) = Sqlds.Tables("SalesOrderTaxDtls").Columns("SaleOrderNumber")
            KeySOTax(3) = Sqlds.Tables("SalesOrderTaxDtls").Columns("EAN")   '' added by nikhil 
            KeySOTax(4) = Sqlds.Tables("SalesOrderTaxDtls").Columns("TaxLineNo")  '' added by nikhil 
            Sqlds.Tables("SalesOrderTaxDtls").PrimaryKey = KeySOTax

            Dim KeySOCharges(3) As DataColumn
            KeySOCharges(0) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SiteCode")
            KeySOCharges(1) = Sqlds.Tables("SalesOrderOtherCharges").Columns("FinYear")
            KeySOCharges(2) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SaleOrderNumber")
            KeySOCharges(3) = Sqlds.Tables("SalesOrderOtherCharges").Columns("SerailNo")
            Sqlds.Tables("SalesOrderOtherCharges").PrimaryKey = KeySOCharges

            Dim KeyOrderHdr(2) As DataColumn
            KeyOrderHdr(0) = Sqlds.Tables("OrderHdr").Columns("SiteCode")
            KeyOrderHdr(1) = Sqlds.Tables("OrderHdr").Columns("FinYear")
            KeyOrderHdr(2) = Sqlds.Tables("OrderHdr").Columns("DocumentNumber")
            Sqlds.Tables("OrderHdr").PrimaryKey = KeyOrderHdr

            Dim KeyOrderDtl(3) As DataColumn
            KeyOrderDtl(0) = Sqlds.Tables("OrderDtl").Columns("SiteCode")
            KeyOrderDtl(1) = Sqlds.Tables("OrderDtl").Columns("FinYear")
            KeyOrderDtl(2) = Sqlds.Tables("OrderDtl").Columns("DocumentNumber")
            'KeyOrderDtl(3) = Sqlds.Tables("OrderDtl").Columns("EAN")
            KeyOrderDtl(3) = Sqlds.Tables("OrderDtl").Columns("LineNumber")
            Sqlds.Tables("OrderDtl").PrimaryKey = KeyOrderDtl

            Dim KeyCLPHdr(1) As DataColumn
            KeyCLPHdr(0) = Sqlds.Tables("CLPTransaction").Columns("SiteCode")
            KeyCLPHdr(1) = Sqlds.Tables("CLPTransaction").Columns("BillNo")
            Sqlds.Tables("CLPTransaction").PrimaryKey = KeyCLPHdr

            Dim KeyCLPDtl(2) As DataColumn
            KeyCLPDtl(0) = Sqlds.Tables("CLPTransactionsDetails").Columns("SiteCode")
            KeyCLPDtl(1) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillNo")
            KeyCLPDtl(2) = Sqlds.Tables("CLPTransactionsDetails").Columns("BillLineNo")
            Sqlds.Tables("CLPTransactionsDetails").PrimaryKey = KeyCLPDtl

            If IsQuotation AndAlso QuotationNumber <> "" Then
                Dim KeyQuhdr(2) As DataColumn
                KeyQuhdr(0) = Sqlds.Tables("Quotationhdr").Columns("SiteCode")
                KeyQuhdr(1) = Sqlds.Tables("Quotationhdr").Columns("FinYear")
                KeyQuhdr(2) = Sqlds.Tables("Quotationhdr").Columns("SaleOrderNumber")
                Sqlds.Tables("Quotationhdr").PrimaryKey = KeyQuhdr

            End If
            Dim KeySOPackVar(4) As DataColumn
            KeySOPackVar(0) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("SiteCode")
            KeySOPackVar(1) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("FinYear")
            KeySOPackVar(2) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("SaleOrderNumber")
            ' KeySOPackVar(3) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("EAN")
            KeySOPackVar(3) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("BillLineNo")
            KeySOPackVar(4) = Sqlds.Tables("SOItemPackagingBoxDtl").Columns("PkgLineNo")
            Sqlds.Tables("SOItemPackagingBoxDtl").PrimaryKey = KeySOPackVar

            Dim KeySOPackVarDelivery(5) As DataColumn
            KeySOPackVarDelivery(0) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("SiteCode")
            KeySOPackVarDelivery(1) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("FinYear")
            KeySOPackVarDelivery(2) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("SaleOrderNumber")
            ' KeySOPackVarDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("EAN")
            KeySOPackVarDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("BillLineNo")
            KeySOPackVarDelivery(4) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("PkgLineNo")
            KeySOPackVarDelivery(5) = Sqlds.Tables("SOPackagingBoxDeliveryDtl").Columns("DeliveryLineNo")
            Sqlds.Tables("SOPackagingBoxDeliveryDtl").PrimaryKey = KeySOPackVarDelivery

            Dim KeySOComboHdr(3) As DataColumn
            KeySOComboHdr(0) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("SaleOrderNumber")
            KeySOComboHdr(1) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("Sitecode")
            KeySOComboHdr(2) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("FinYear")
            KeySOComboHdr(3) = Sqlds.Tables("SalesOrderBulkComboHdr").Columns("ComboSrNo")

            Sqlds.Tables("SalesOrderBulkComboHdr").PrimaryKey = KeySOComboHdr

            Dim KeySOComboDtl(4) As DataColumn
            KeySOComboDtl(0) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("SaleOrderNumber")
            KeySOComboDtl(1) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("Sitecode")
            KeySOComboDtl(2) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("FinYear")
            KeySOComboDtl(3) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("ComboSrNo")
            KeySOComboDtl(4) = Sqlds.Tables("SalesOrderBulkComboDtl").Columns("EAN")

            Sqlds.Tables("SalesOrderBulkComboDtl").PrimaryKey = KeySOComboDtl

            Dim KeySODiscDtl(5) As DataColumn

            KeySODiscDtl(0) = Sqlds.Tables("SOPackagingDiscDtl").Columns("Sitecode")
            KeySODiscDtl(1) = Sqlds.Tables("SOPackagingDiscDtl").Columns("FinYear")
            KeySODiscDtl(2) = Sqlds.Tables("SOPackagingDiscDtl").Columns("SaleOrderNumber")
            ' KeySODiscDtl(3) = Sqlds.Tables("SOPackagingDiscDtl").Columns("EAN")
            KeySODiscDtl(3) = Sqlds.Tables("SOPackagingDiscDtl").Columns("BillLineNo")
            KeySODiscDtl(4) = Sqlds.Tables("SOPackagingDiscDtl").Columns("PkgLineNo")
            KeySODiscDtl(5) = Sqlds.Tables("SOPackagingDiscDtl").Columns("SrNo")

            Sqlds.Tables("SOPackagingDiscDtl").PrimaryKey = KeySODiscDtl

            Dim KeySOSTRDtl(3) As DataColumn

            KeySOSTRDtl(0) = Sqlds.Tables("SalesOrderStrDetails").Columns("Sitecode")
            KeySOSTRDtl(1) = Sqlds.Tables("SalesOrderStrDetails").Columns("FinYear")
            KeySOSTRDtl(2) = Sqlds.Tables("SalesOrderStrDetails").Columns("SaleOrderNumber")
            KeySOSTRDtl(3) = Sqlds.Tables("SalesOrderStrDetails").Columns("STRIndex")
            Sqlds.Tables("SalesOrderStrDetails").PrimaryKey = KeySOSTRDtl

            Dim KeyOrderDelivery(6) As DataColumn
            KeyOrderDelivery(0) = Sqlds.Tables("PkgOrderDtl").Columns("SiteCode")
            KeyOrderDelivery(1) = Sqlds.Tables("PkgOrderDtl").Columns("FinYear")
            KeyOrderDelivery(2) = Sqlds.Tables("PkgOrderDtl").Columns("SaleOrderNumber")
            '  KeyOrderDelivery(3) = Sqlds.Tables("PkgOrderDtl").Columns("EAN")
            KeyOrderDelivery(3) = Sqlds.Tables("PkgOrderDtl").Columns("BillLineNo")
            KeyOrderDelivery(4) = Sqlds.Tables("PkgOrderDtl").Columns("PkgLineNo")
            KeyOrderDelivery(5) = Sqlds.Tables("PkgOrderDtl").Columns("DeliveryLineNo")
            KeyOrderDelivery(6) = Sqlds.Tables("PkgOrderDtl").Columns("DocumentNo")
            Sqlds.Tables("PkgOrderDtl").PrimaryKey = KeyOrderDelivery


            Dim KeySOPackVarTempDelivery(5) As DataColumn
            KeySOPackVarTempDelivery(0) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("SiteCode")
            KeySOPackVarTempDelivery(1) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("FinYear")
            KeySOPackVarTempDelivery(2) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("SaleOrderNumber")
            ' KeySOPackVarTempDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("EAN")
            KeySOPackVarTempDelivery(3) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("BillLineNo")
            KeySOPackVarTempDelivery(4) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("PkgLineNo")
            KeySOPackVarTempDelivery(5) = Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").Columns("DeliveryLineNo")
            Sqlds.Tables("SOPackagingBoxDeliveryTempDtl").PrimaryKey = KeySOPackVarTempDelivery

            Dim KeySTRFactory(3) As DataColumn

            KeySTRFactory(0) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("Sitecode")
            KeySTRFactory(1) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("FinYear")
            KeySTRFactory(2) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("SaleOrderNumber")
            KeySTRFactory(3) = Sqlds.Tables("SalesOrderSTRFactoryRemark").Columns("FactorySiteCode")
            Sqlds.Tables("SalesOrderSTRFactoryRemark").PrimaryKey = KeySTRFactory


            Dim KeySOTaxDtl(5) As DataColumn

            KeySOTaxDtl(0) = Sqlds.Tables("SOPackagingTaxDtl").Columns("Sitecode")
            KeySOTaxDtl(1) = Sqlds.Tables("SOPackagingTaxDtl").Columns("FinYear")
            KeySOTaxDtl(2) = Sqlds.Tables("SOPackagingTaxDtl").Columns("SaleOrderNumber")
            ' KeySOTaxDtl(3) = Sqlds.Tables("SOPackagingTaxDtl").Columns("EAN")
            KeySOTaxDtl(3) = Sqlds.Tables("SOPackagingTaxDtl").Columns("BillLineNo")
            KeySOTaxDtl(4) = Sqlds.Tables("SOPackagingTaxDtl").Columns("PkgLineNo")
            KeySOTaxDtl(5) = Sqlds.Tables("SOPackagingTaxDtl").Columns("SrNo")

            Sqlds.Tables("SOPackagingTaxDtl").PrimaryKey = KeySOTaxDtl

            Dim KeySaleOrderTermNConditions(4) As DataColumn
            KeySaleOrderTermNConditions(0) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SiteCode")
            KeySaleOrderTermNConditions(1) = Sqlds.Tables("SaleOrderTermNConditions").Columns("FinYear")
            KeySaleOrderTermNConditions(2) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SaleOrderNumber")
            KeySaleOrderTermNConditions(3) = Sqlds.Tables("SaleOrderTermNConditions").Columns("TnCcode")
            KeySaleOrderTermNConditions(4) = Sqlds.Tables("SaleOrderTermNConditions").Columns("SrNo")
            Sqlds.Tables("SaleOrderTermNConditions").PrimaryKey = KeySaleOrderTermNConditions

            Return Sqlds
        Catch SqlEx As SqlException
            LogException(SqlEx)
            If SqlEx.Number = -2 Then
                Throw New ApplicationException("Server is not Responding")
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetFactoryNodeCodes() As DataSet
        Try
            Dim query As String = " SELECT  *  FROM   dbo.nodecode('EN')  ;"
            query &= " select distinct ArticleCode,LastNodeCode   from MSTArticle;"
            Dim ds As New DataSet
            Dim daCM As SqlDataAdapter
            daCM = New SqlDataAdapter(query, SpectrumCon)
            daCM.Fill(ds)

            Return ds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllDocumentInfoCustmWise(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocumentType As String, ByVal SelectedCustomerCode As String) As DataTable
        Try
            Dim daInvoice As New SqlDataAdapter
            Dim dtInvoice As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            If pDocumentType = "SalesOrder" Then

                SqlQuery.Append("Select ActualDeliveryDate as BillDate, SaleOrderNumber as SalesOrderNo, LineItems as TotalItems, NetAmt as TotalAmount,AdvanceAmt as AmountPaid, " & vbCrLf)
                SqlQuery.Append(" SalesOrderHdr.AdvanceAmt - ISNULL(CreditSaleRecords.CreditAmt, 0) + ISNULL(CrdSaleAdjust.CreditAdjustmentAmt, 0) AS AmountReceived," & vbCrLf)
                SqlQuery.Append(" SalesOrderHdr.NetAmt - (SalesOrderHdr.AdvanceAmt - ISNULL(CreditSaleRecords.CreditAmt, 0) + ISNULL(CrdSaleAdjust.CreditAdjustmentAmt, 0)) AS AmountDue " & vbCrLf)
                SqlQuery.Append("FROM SalesOrderHdr LEFT OUTER JOIN (SELECT DocumentNumber, SUM(AmountTendered) AS CreditAmt FROM SalesInvoice AS SInv" & vbCrLf)
                SqlQuery.Append(" WHERE  SInv.status =1 and (TenderTypeCode = 'Credit') GROUP BY DocumentNumber) AS CreditSaleRecords ON SalesOrderHdr.SaleOrderNumber = CreditSaleRecords.DocumentNumber" & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (SELECT     RefBillNo AS DocumentNumber, SUM(ISNULL(AmountTendered, 0)) AS CreditAdjustmentAmt" & vbCrLf)
                SqlQuery.Append(" FROM CreditReceipt AS Cr GROUP BY RefBillNo) AS CrdSaleAdjust ON SalesOrderHdr.SaleOrderNumber = CrdSaleAdjust.DocumentNumber" & vbCrLf)
                SqlQuery.Append(" Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' and SOStatus not in('Cancel','Return') " & vbCrLf)
                SqlQuery.Append(" And CustomerNo='" & SelectedCustomerCode & "'  Order By UpdatedOn Desc" & vbCrLf)

            End If

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllComboItemsBySalesOrder(ByVal pSiteCode As String, ByVal pSalesOrderNo As String) As DataTable
        Try
            Dim daInvoice As New SqlDataAdapter
            Dim dtInvoice As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0









            SqlQuery.Append(" select  sbdtl.ArticleCode ,sbdtl.ArticleDescription ,sbdtl.Qty ,dtl.ItemQtyBaseUOM ,dtl.SellingPrice , dtl.NetAmount,dtl.DiscountPercentage  ,art.ArticleShortName  from " & vbCrLf)
            SqlQuery.Append(" inner join SoBulkComboDtl sbdtl on sbhdr.BulkComboMstId=sbdtl.BulkComboMstId inner join MSTArticle art on art.ArticleCode=dtl.ArticleCode " & vbCrLf)

            SqlQuery.Append(" where sbhdr.SaleOrderNumber ='" & pSalesOrderNo & "' and dtl.SiteCode='" & pSiteCode & "' " & vbCrLf)
            SqlQuery.Append(" Order By dtl.UpdatedOn Desc" & vbCrLf)




            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMaxSTRNo(ByVal vsaleNo As String, ByVal siteCode As String) As Integer
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select Isnull(MAX(StrIndex ),0) as maxSRNo from SalesOrderStrDetails where  SaleOrderNumber ='" & vsaleNo & "' and sitecode= '" & siteCode & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            Else
                Return 1
            End If
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetStruc(ByVal CashMemo As String, ByVal SiteCode As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim daCM As SqlDataAdapter
            daCM = New SqlDataAdapter(" EXEC GetSalesOrderBillStru '" & CashMemo & "','" & SiteCode & "'", SpectrumCon)
            daCM.Fill(ds)
            ds.Tables(0).TableName = "SALESORDERHDR"
            ds.Tables(1).TableName = "SALESORDERDTL"
            ds.Tables(2).TableName = "SALESORDERRECEIPT"
            ds.Tables(3).TableName = "SOBULKCOMBODTL"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateSoDeliverQty(ByVal siteCode As String, ByVal SaleOrderNo As String) As Boolean

        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strString As String = ""

            strString = "UPDATE SOPackagingBoxDeliveryTempDtl SET PickupQty=0 where SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "';"
            'strString &= "UPDATE SOItemPackagingBoxDtl SET DeliveredQty=0 where SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "';"
            'strString &= "UPDATE SOPackagingBoxDeliveryDtl SET DeliveredQty=0 where SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "';"
            'strString &= "UPDATE OrderDtl SET DeliveredQty=0 where DocumentNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "';"
            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)
            If cmd.ExecuteNonQuery() > 0 Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            Return False

        Catch ex As Exception

            CloseConnection()
            LogException(ex)
            Return False
        End Try




    End Function

    Public Function GetSaleOrderCreationTime(ByVal siteCode As String, ByVal SaleOrderNo As String) As Boolean

        Try
            Dim dtPayment As New DataTable
            Dim strStringInvoice As String = "select * from SOPackagingBoxDeliveryTempDtl where SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "'"
            Dim daDefaultPayment As New SqlDataAdapter(strStringInvoice, ConString)
            daDefaultPayment.Fill(dtPayment)
            If dtPayment.Rows.Count > 0 Then
                Dim strString As String = "select UPDATEDON from SalesOrderHdr where SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "'"
                Dim dt As New DataTable
                Dim daDefault As New SqlDataAdapter(strString, ConString)
                daDefault.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Dim updatedon As DateTime = dt(0)(0)
                    updatedon = updatedon.AddMinutes(60)
                    If updatedon < DateTime.Now Then
                        UpdateSoDeliverQty(siteCode, SaleOrderNo)
                        Return True
                    Else
                        Return False
                    End If
                End If
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOInvoiceTableStruc(ByVal vSiteCode As String, ByVal vSalesNo As String) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From SalesInvoice Where status =1 and SiteCode='" & vSiteCode & " ' And DocumentNumber='" & vSalesNo & "'; " & vbCrLf)
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)
            Sqlds.Tables(0).TableName = "SalesInvoice"
            Dim KeySOInvc(4) As DataColumn
            KeySOInvc(0) = Sqlds.Tables("SalesInvoice").Columns("SiteCode")
            KeySOInvc(1) = Sqlds.Tables("SalesInvoice").Columns("FinYear")
            KeySOInvc(2) = Sqlds.Tables("SalesInvoice").Columns("DocumentNumber")
            KeySOInvc(3) = Sqlds.Tables("SalesInvoice").Columns("SaleInvNumber")
            KeySOInvc(4) = Sqlds.Tables("SalesInvoice").Columns("SaleInvLineNumber")
            Sqlds.Tables("SalesInvoice").PrimaryKey = KeySOInvc
            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Other Charges Table Structure Information
    ''' </summary>
    ''' <param name="sitecode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetDtOtherCharge(ByVal sitecode As String, Optional ByVal SaleOrderNumber As String = "") As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select SiteCode,finyear, SaleOrderNumber, SerailNo, ChargeName, ChargeAmount, TaxName, TaxAmt from SalesOrderOtherCharges Where SiteCode='" & sitecode & "' ")
            If Not String.IsNullOrEmpty(SaleOrderNumber) Then
                vStmtQry.Append(" AND SaleOrderNumber ='" & SaleOrderNumber & "'")
            End If
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Prepare Data For Save
    ''' </summary>
    ''' <param name="dsSOMain">Sales Order DataSet</param>
    ''' <returns>True/False</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function PrepareSaveData(ByVal currentSalesinvoice As String, ByVal DayOpendate As DateTime, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal IsNextSalesNo As Boolean, ByVal IsNextInvoiceNo As Boolean, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal Storage As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, ByVal IsOBCreated As Boolean, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0, Optional ByRef isPromoApplied As Boolean = False, Optional ByVal DtDeletedData As DataTable = Nothing, Optional ByVal DeliveryLocInfo As List(Of SODeliveryLocationInfo) = Nothing, Optional ByVal IsbatchMgt As Boolean = False, Optional ByVal BatchBarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo) = Nothing, Optional ByVal IsUpdate As Boolean = False, Optional ByVal StrNumList As String = "", Optional ByRef dtStrResult As DataSet = Nothing) As Boolean
        Try
            Dim objComm As New clsCommon
            Dim disc As Double
            If isPromoApplied = True Then

                Dim strSql As String = " DELETE FROM SALESDISCDTL WHERE SiteCode = '" & vSiteCode & "' AND FinYear = '" & FinYear & "' AND BillNo = '" & vSalesNo & "'"
                OpenConnection()
                Dim cmd As New SqlCommand(strSql, SpectrumCon)
                cmd.ExecuteNonQuery()

                disc = IIf(dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", ""))
                If disc > 0 Then
                    Dim dtDtl As DataTable = dsSOMain.Tables("SalesOrderDTL").Copy()

                    Dim dtDisc As DataTable = objComm.CreateDiscSummary(DayOpendate, dtDtl, "", "SO201", vSiteCode, FinYear, vSalesNo, UserId, ServerDate, "FIRSTLEVEL", "TOPLEVEL")
                    If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
                        Dim PromotionId = dtDisc(0)("PromotionId").ToString()
                        If dsSOMain.Tables.Contains("SoPackagingDiscDtl") Then
                            If dsSOMain.Tables("SoPackagingDiscDtl").Rows.Count > 0 Then
                                For Each drsdisc As DataRow In dsSOMain.Tables("SoPackagingDiscDtl").Rows
                                    If drsdisc("DiscountAmount") > 0 Then
                                        drsdisc("PromotionId") = PromotionId
                                    Else
                                        drsdisc("PromotionId") = ""
                                    End If

                                Next

                            End If
                        End If
                        If dsSOMain.Tables.Contains("salesdiscdtl") Then
                            dsSOMain.Tables.Remove("salesdiscdtl")
                            dsSOMain.Tables.Add(dtDisc)
                        Else
                            dsSOMain.Tables.Add(dtDisc)
                        End If
                        'dsSOMain.AcceptChanges()
                    End If
                End If
            Else
                If IsUpdate Then
                    If dsSOMain.Tables.Contains("SoPackagingDiscDtl") Then
                        If dsSOMain.Tables("SoPackagingDiscDtl").Rows.Count > 0 Then
                            For Each drsdisc As DataRow In dsSOMain.Tables("SoPackagingDiscDtl").Rows
                                Dim result As DataRow() = dsSOMain.Tables("SalesOrderDTL").Select("billlineno='" + drsdisc("billlineno").ToString() + "'")
                                If drsdisc("DiscountAmount") > 0 Then
                                    If result.Length > 0 Then
                                        Dim PromotionId = result(0)("OfferNo").ToString()
                                        Dim PromotionIdArray As Array = PromotionId.Split(",")
                                        If PromotionIdArray.Length > 0 Then
                                            For i = 0 To PromotionIdArray.Length - 1
                                                If PromotionIdArray(i).ToString().Contains("PR") Then
                                                    drsdisc("PromotionId") = PromotionIdArray(i).ToString()
                                                End If
                                            Next
                                        Else
                                            drsdisc("PromotionId") = ""
                                        End If
                                    Else
                                        drsdisc("PromotionId") = ""
                                    End If
                                Else
                                    drsdisc("PromotionId") = ""
                                End If
                            Next

                        End If
                    End If
                End If

            End If
            Dim dtTempReserved As DataSet = dsSOMain.Copy()
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Reserved_Qty") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Reserved_Qty")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Delivered_Qty") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Delivered_Qty")
            End If
            'B.FreezeSB, B.FreezeSR,B.FreezeOB
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSB") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSB")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSR") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSR")
            End If
            If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeOB") Then
                dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeOB")
            End If

            '----- SET CREDIT Sale Update 
            Dim dtCreditSaleData As New DataTable
            Dim DvSalesOrderClosed As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Closed'", "", DataViewRowState.CurrentRows)
            If DvSalesOrderClosed.ToTable.Rows.Count > 0 Then
                Dim objclsReturn As New clsCashMemoReturn
                dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" & dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber") & "'")
                dtCreditSaleData.Columns.Add("NetCreditSaleAmount")
                dtCreditSaleData.Columns.Add("AdjustedCredit")
                objCM.SetCloseBillWiseCreditSaleAmt(dtCreditSaleData, dsSOMain)
            End If

            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()
            'If Not SaveDiscount(dsSOMain.Tables("SalesOrderDTL"), vSalesNo, SqlTrans) Then
            '    SqlTrans.Rollback()
            '    CloseConnection()
            '    Return False
            'End If
            If Not IsUpdate Then
                'If dtTempReserved.Tables.Contains("SalesOrderTaxDtls") Then
                '    If dtTempReserved.Tables("SalesOrderTaxDtls").Rows.Count > 0 Then
                '        For Each row As DataRow In dtTempReserved.Tables("SalesOrderTaxDtls").Rows
                '            If row.RowState <> DataRowState.Deleted Then

                '                row("CreatedOn") = ServerDate
                '                row("Status") = 1
                '                row("CreatedBy") = UserId
                '                row("createdAt") = vSiteCode


                '                row("Updatedon") = ServerDate
                '                row("Updatedby") = UserId
                '                row("UpdatedAt") = vSiteCode

                '                row("DocumentType") = "SalesOrder"
                '            End If
                '        Next
                '    End If
                'End If
            End If


            If objComm.SaveData(dtTempReserved, SpectrumCon, SqlTrans) = True Then


                For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                    If dr.RowState <> DataRowState.Deleted Then
                        Dim reserveQty As Decimal = IIf(dr("Reserved_Qty") Is DBNull.Value, 0, dr("Reserved_Qty"))


                        '----- Now Handling Bulk Combo Case (code change By Mahesh )
                        If dr("IsCombo") Then
                            If Not DtSoBulkComboHdr Is Nothing Then
                                If DtSoBulkComboHdr.Rows.Count > 0 Then
                                    Dim test() = DtSoBulkComboHdr.Select("PackagingBoxCode ='" & dr("ArticleCode").ToString() & "' AND ComboSrNo=" & dr("BillLineNo"))
                                End If
                            End If
                            Dim DeliveredQty = IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString())
                            If dsSOMain.Tables("SOItemPackagingBoxDtl").Rows.Count > 0 Then

                                'Dim dv As New DataView
                                'dv = New DataView(dsSOMain.Tables("SOItemPackagingBoxDtl"), "BillLineNo='" & dr("BillLineNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                                'For Each drView As DataRowView In dv
                                '    If drView("PckgMaterial").ToString() <> "" Then
                                '        DeliveredQty = DeliveredQty + IIf(IsDBNull(drView("DeliveredQty")), 0, drView("DeliveredQty").ToString())
                                '        If objComm.UpdateStock(drView("SiteCode").ToString, drView("ArticleCode").ToString(), drView("PackagedEAN").ToString(), drView("PackageBaseUOM").ToString(), IIf(IsDBNull(drView("PckgQty")), 0, drView("PckgQty").ToString()), drView("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                '            SqlTrans.Rollback()
                                '            CloseConnection()
                                '            Return False
                                '        End If
                                '    End If
                                'Next

                                Dim dvDtl As New DataView
                                dvDtl = New DataView(dsSOMain.Tables("SalesOrderBulkComboDtl"), "ComboSrNo='" & dr("BillLineNo").ToString() & "'", "", DataViewRowState.CurrentRows)

                                For Each drView As DataRowView In dvDtl
                                    'If drView("PckgMaterial").ToString() <> "" Then
                                    Dim inDivQty As Double = 1
                                    If drView("PackagedUOM") = drView("BaseUOM") Then
                                        If drView("Qty") = 0 Then
                                            drView("Qty") = 1
                                        End If
                                        inDivQty = DeliveredQty * drView("Qty")
                                    Else
                                        If drView("Weight") = 0 Then
                                            drView("Weight") = 1
                                        End If
                                        inDivQty = DeliveredQty * (drView("Qty") * drView("Weight"))
                                    End If
                                    If objComm.UpdateStock(drView("SiteCode").ToString, drView("ArticleCode").ToString(), drView("EAN").ToString(), drView("BaseUOM").ToString(), inDivQty, drView("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                        SqlTrans.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                    'End If
                                Next





                                '-----Run for Each Article of Bulk combo
                                'Dim comboItemDeliveryQty As Double = 0
                                'Dim comboItemReserveQty As Double = 0
                                'Dim drDtl() = DtSoBulkComboDtl.Select("BulkComboMstId=" & test(0)("BulkComboMstId"))
                                'If drDtl.Count > 0 Then
                                '    With DtSoBulkComboDtl
                                '        For index = 0 To drDtl.Count - 1
                                '            If .Rows(index)("STRExcludeCheck") Then
                                '                If (DeliveryQty + DeliveredQty) = Qty Then
                                '                    comboItemDeliveryQty = drDtl(index)("ItemQtyBaseUOM").ToString()
                                '                End If
                                '            Else
                                '                comboItemDeliveryQty = DeliveryQty * drDtl(index)("ItemQtyBaseUOM").ToString()
                                '                comboItemReserveQty = reserveQty * drDtl(index)("ItemQtyBaseUOM").ToString()
                                '            End If

                                '            'Dim otherSiteDeliveryQty As Decimal = SoDeliveryInfo.Sum(Function(x) IIf(x.ArticleCode = .Rows(index)("ArticleCode").ToString(), x.Quantity, 0))
                                '            If objComm.UpdateStock(dr("SiteCode").ToString, drDtl(index)("ArticleCode").ToString(), drDtl(index)("EAN").ToString(), drDtl(index)("BaseUOM").ToString(), comboItemDeliveryQty, drDtl(index)("CreatedAt"), SpectrumCon, SqlTrans, Storage, comboItemReserveQty) = False Then
                                '                SqlTrans.Rollback()
                                '                CloseConnection()
                                '                Return False
                                '            End If
                                '        Next index
                                '    End With
                                'End If 
                            Else
                                If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                    SqlTrans.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                                'Dim dv As New DataView
                                'dv = New DataView(dsSOMain.Tables("SOItemPackagingBoxDtl"), "BillLineNo='" & dr("BillLineNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                                'For Each drView As DataRowView In dv
                                '    If drView("PckgMaterial").ToString() <> "" Then
                                '        If objComm.UpdateStock(drView("SiteCode").ToString, drView("PackagingBoxCode").ToString(), drView("PackagedEAN").ToString(), drView("PackageBaseUOM").ToString(), IIf(IsDBNull(drView("PckgQty")), 0, drView("PckgQty").ToString()), drView("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                '            SqlTrans.Rollback()
                                '            CloseConnection()
                                '            Return False
                                '        End If
                                '    End If
                                'Next
                            End If
                        Else


                            'dv = New DataView(dsSOMain.Tables("SOItemPackagingBoxDtl"), "BillLineNo='" & dr("BillLineNo").ToString() & "'", "", DataViewRowState.CurrentRows)

                            If objComm.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), dr("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                            'Dim dv As New DataView
                            'dv = New DataView(dsSOMain.Tables("SOItemPackagingBoxDtl"), "BillLineNo='" & dr("BillLineNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                            'For Each drView As DataRowView In dv
                            '    If drView("PckgMaterial").ToString() <> "" Then
                            '        If objComm.UpdateStock(drView("SiteCode").ToString, drView("PackagingBoxCode").ToString(), drView("PackagedEAN").ToString(), drView("PackageBaseUOM").ToString(), IIf(IsDBNull(drView("PckgQty")), 0, drView("PckgQty").ToString()), drView("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                            '            SqlTrans.Rollback()
                            '            CloseConnection()
                            '            Return False
                            '        End If
                            '    End If
                            'Next
                        End If


                    End If
                Next
                ''Packaging for single article if exist

                'For Each drs As DataRow In dsSOMain.Tables("SOItemPackagingBoxDtl").Rows
                '    If drs("IsCombo") = False Then
                '        Dim reserveQty As Decimal = 0
                '        If objComm.UpdateStock(drs("SiteCode").ToString, drs("ArticleCode").ToString(), drs("EAN").ToString(), drs("BaseUnitofMeasure").ToString(), IIf(IsDBNull(drs("DeliveredQty")), 0, drs("DeliveredQty").ToString()), drs("CreatedAt"), SpectrumCon, SqlTrans, Storage, reserveQty) = False Then
                '            SqlTrans.Rollback()
                '            CloseConnection()
                '            Return False
                '        End If
                '    End If
                'Next

                '---- Update Credit sale data in case of return ....
                Dim terminalid As String = "", BalanceAmount As Double = 0, AdavanceAmount As Double = 0
                Dim Retbillno As String = "", SalesOrderNumber As String = String.Empty

                '---- Fetch Sales Order Current Advance & Balance Amount , later will update these value 
                Dim DvSalesOrder As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus <> 'Return'", "", DataViewRowState.CurrentRows)
                SalesOrderNumber = DvSalesOrder.ToTable.Rows(0)("SaleOrderNumber")
                terminalid = DvSalesOrder.ToTable.Rows(0)("terminalid")
                BalanceAmount = DvSalesOrder.ToTable.Rows(0)("BalanceAmount")
                AdavanceAmount = DvSalesOrder.ToTable.Rows(0)("AdvanceAmt")

                '--- Fetch Return Bill No & caculate Credit sale
                Dim DvSalesOrderRet As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Return'", "", DataViewRowState.CurrentRows)
                If DvSalesOrderRet.ToTable.Rows.Count > 0 Then
                    Retbillno = DvSalesOrderRet.ToTable.Rows(0)("SaleOrderNumber")

                    Dim creditsale As Double = 0, crdsaleadjustamount As Double = 0

                    Dim NetReturnAmount As Double = 0
                    Dim salesorderdtl As New DataView(dsSOMain.Tables("SalesOrderDtl"), "ArticleStatus = 'Return'", "", DataViewRowState.CurrentRows)
                    Dim dt As DataTable = salesorderdtl.ToTable(True, "SaleOrderNumber", "NetAmount")

                    If dt.Rows.Count > 0 AndAlso Not dt Is Nothing Then
                        For Each x In dt.Rows
                            NetReturnAmount += x("NetAmount") * -1
                            'dtBillReturnDetails.Rows.Add(SalesOrderNumber, creditsale, NetReturnAmount, crdsaleadjustamount)
                        Next
                    End If


                    Dim DvSalesInvoice As New DataView(dsSOMain.Tables("SalesInvoice"), "TenderHeadCode = 'Credit Sales'", "", DataViewRowState.CurrentRows)
                    Dim dtInvoiceNoWiseCreditSale As DataTable = DvSalesInvoice.ToTable(True, "SaleInvNumber", "AmountTendered", "DocumentNumber")

                    Dim dtSalesInvoiceCreditDetails As New DataTable
                    dtSalesInvoiceCreditDetails.TableName = "dtSalesInvoiceCreditDetails"
                    dtSalesInvoiceCreditDetails.Columns.Add("SalesInvoiceNumber", GetType(String))
                    dtSalesInvoiceCreditDetails.Columns.Add("CreditSale", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("NetReturnAmount", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("crdsaleadjustamount", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("UpdateValue", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("CreditUsed", GetType(Double))
                    dtSalesInvoiceCreditDetails.Columns.Add("BalanceCredit", GetType(Double))
                    Dim FinalValue As Double = 0, CreditUsed As Double = 0, balancecredit As Double = 0
                    Dim SalesOrderInvNumber As String = ""
                    If dtInvoiceNoWiseCreditSale.Rows.Count > 0 Then
                        For Each x In dtInvoiceNoWiseCreditSale.Rows
                            SalesOrderInvNumber = x("SaleInvNumber")
                            creditsale = x("AmountTendered")
                            crdsaleadjustamount = objCM.GetInvoiceWiseCreditAdjusted(SalesOrderInvNumber, SpectrumCon, SqlTrans)
                            Dim maxlimit As Double = 0
                            maxlimit = creditsale - crdsaleadjustamount


                            If maxlimit < NetReturnAmount Then
                                FinalValue = creditsale - maxlimit
                                balancecredit = maxlimit
                            Else
                                FinalValue = creditsale - NetReturnAmount
                                balancecredit = NetReturnAmount
                            End If

                            If NetReturnAmount < creditsale Then
                                CreditUsed = NetReturnAmount
                            Else
                                CreditUsed = creditsale
                            End If

                            dtSalesInvoiceCreditDetails.Rows.Add(SalesOrderInvNumber, creditsale, NetReturnAmount, crdsaleadjustamount, FinalValue, CreditUsed, balancecredit)
                            NetReturnAmount = NetReturnAmount - balancecredit
                        Next

                        Dim NetCrdSale As Double = 0

                        If dtSalesInvoiceCreditDetails.Rows.Count > 0 Then
                            For Each x In dtSalesInvoiceCreditDetails.Rows
                                NetCrdSale = x("creditsale") - x("crdsaleadjustamount")
                                CreditUsed = x("balancecredit")

                                If NetCrdSale > 0 Then

                                    If objCM.UpdateCSThroughSalesInvoiceNo(x("SalesInvoiceNumber"), x("UpdateValue"), SpectrumCon, SqlTrans, terminalid, DayOpendate, Retbillno, x("balancecredit"), vSiteCode) Then
                                        BalanceAmount = BalanceAmount + CreditUsed
                                        AdavanceAmount = AdavanceAmount - CreditUsed

                                        If CreditUsed > 0 Then
                                            If Not objCM.UpdateBalanceAndAdvanceAmt(SalesOrderNumber, BalanceAmount, AdavanceAmount, SpectrumCon, SqlTrans) Then
                                                SqlTrans.Rollback()
                                                CloseConnection()
                                                Return False
                                            End If
                                        End If
                                    Else
                                        SqlTrans.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                End If

                            Next



                        End If

                    End If


                End If

                If dtCreditSaleData.Rows.Count > 0 AndAlso IsMannualClose = True Then

                    If objCM.UpdateCreditSalesOnReturnItems(dtCreditSaleData, _SelectedCurrencyIndex, _iCurrencyCode, SpectrumCon, SqlTrans, terminalid, DayOpendate, dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber"), vSiteCode) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                ''updating credit sale value in case of return


                '---
                If IsbatchMgt Then
                    If BatchBarcodeList IsNot Nothing Then

                        For Each dr As SpectrumCommon.BtachbarcodeInfo In BatchBarcodeList
                            'Change by Sameer for Issue 6915 4/4/13
                            If objComn.UpdateBatchDtlQtyAllocated(vSiteCode, dr.BatchBarcode.ToString(), dr.Qty, SqlTrans) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Next
                    End If

                    'For Each dr As DataRow In dsSOMain.Tables("SalesOrderDTL").Rows
                    '    If dr.RowState <> DataRowState.Deleted Then
                    '        If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                    '            'Change by Sameer for Issue 6915 4/4/13
                    '            If objComn.UpdateBatchDtlQtyAllocated(dr("SiteCode").ToString, dr("BatchBarcode").ToString(), IIf(IsDBNull(dr("Delivered_Qty")), 0, dr("Delivered_Qty").ToString()), SqlTrans) = False Then
                    '                SqlTrans.Rollback()
                    '                CloseConnection()
                    '                Return False
                    '            End If
                    '        End If
                    '    End If
                    'Next
                End If
                '-----deleted data code for reversing reserve Qty---------
                If Not DtDeletedData Is Nothing AndAlso DtDeletedData.Rows.Count > 0 Then
                    For Each dr As DataRow In DtDeletedData.Rows
                        If dr.RowState <> DataRowState.Deleted Then
                            If objComm.UpdateStock(vSiteCode, dr("ArticleCode").ToString(), dr("EAN").ToString(), "", 0, UserId, SpectrumCon, SqlTrans, Storage, IIf(dr("ReservedQty") = False, 0, dr("Quantity") * -1)) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                    DtDeletedData = Nothing
                End If
                '---------end here for reserve-----------
                If IsOBCreated = True Then
                    If objComn.UpdateDocumentNo("OutBound", SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                ' Add for insert for CV by ram  in ver 1.0.10.7 . dataviewrowstate.add is add because in edit sales order it is pickup old records.
                'current Sales Invoice Number
                Dim strCurrSaleInvNbr As String = ""
                If dtTempReserved.Tables("SalesInvoice").Rows.Count > 0 Then
                    strCurrSaleInvNbr = dtTempReserved.Tables("SalesInvoice").Compute("MAX(SaleInvNumber)", "")
                End If



                Dim dvCreditVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SaleInvNumber='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)
                Dim RedimCVExpDay As Integer = 0
                If dvCreditVoucher.Count > 0 Then
                    For Each drView As DataRowView In dvCreditVoucher

                        '' Issue CV against partial redeemation should have expirydate same as orignal CV
                        'If dvCreditVoucher.Count > 1 Then
                        '    Dim dvRedimCV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'CreditVouc(R)'", "", DataViewRowState.CurrentRows)
                        '    If dvRedimCV.Count > 0 Then
                        '        If Not IsDBNull(dvRedimCV(0).Item("RefDate")) Then
                        '            RedimCVExpDay = DateDiff(DateInterval.Day, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dvRedimCV(0).Item("RefDate"))
                        '        End If
                        '    End If
                        'End If
                        '' Issue CV against partial redeemation should have expirydate same as orignal CV

                        If drView("TenderTypeCode") = "CreditVouc(I)" Then
                            ' this is to get the old expiry date of partial redeem CV
                            'If dvCreditVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem CV

                            If objCM.UpdateCreditVoucher(CVProgram, DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "CreditVouc(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString()) = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                Dim dvGiftVoucher As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode Like 'GiftVouch%'", "", DataViewRowState.CurrentRows)
                Dim RedimGVExpDay As Integer = 0
                If dvGiftVoucher.Count > 0 Then
                    For Each drView As DataRowView In dvGiftVoucher
                        ' Issue GV against partial redeemation should have expirydate same as orignal GV
                        'If dvGiftVoucher.Count > 1 Then
                        '    Dim dvRedimGV As New DataView(dtTempReserved.Tables("SalesInvoice"), " SALEINVNUMBER='" & strCurrSaleInvNbr & "' AND TenderTypeCode = 'GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                        '    If dvRedimGV.Count > 0 Then
                        '        If Not IsDBNull(dvRedimGV(0).Item("RefDate")) Then
                        '            RedimGVExpDay = DateDiff(DateInterval.Day, Now, dvRedimGV(0).Item("RefDate"))
                        '        End If
                        '    End If
                        'End If
                        '' Issue GV against partial redeemation should have expirydate same as orignal GV

                        If drView("TenderTypeCode") = "GiftVoucher(I)" Then
                            ' this is to get the old expiry date of partial redeem GV
                            'If dvGiftVoucher.Count > 1 Then
                            If Not IsDBNull(drView("RefDate")) Then
                                VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                                VoucherDays = VoucherDays
                            End If
                            'End If
                            ' this is to get the old expiry date of partial redeem GV

                            If objCM.UpdateCreditVoucher(drView("RefNo_2"), DocType, True, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, drView("AmountTendered"), Voucherno, VoucherDays, "GV") = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        ElseIf drView("TenderTypeCode") = "GiftVoucher(R)" Then
                            If objCM.UpdateCreditVoucher(CVProgram, DocType, False, drView("DocumentNumber").ToString(), dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), drView("SOInvDate"), dsSOMain.Tables("SalesOrderHdr").Rows(0)("CreatedBy"), SqlTrans, SpectrumCon, 0, drView("RefNO_2").ToString(), 0, "GV") = False Then
                                SqlTrans.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                    Next
                End If

                ' Add for insert for GV by ram  in ver 1.0.9.2 
                Dim dvCLP As New DataView(dtTempReserved.Tables("SalesInvoice"), "TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
                If dvCLP.Count > 0 Then
                    For Each CLpRow As DataRowView In dvCreditVoucher
                        Dim TotalPoints As Integer = CLpRow("AmountTendered")
                        'CLPRedemptionPoints = TotalPoints
                        'TotalPoints = TotalPoints * -1
                        If objComm.UpdateClpPoints(False, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, SqlTrans, dsSOMain.Tables("SalesOrderHdr").Rows(0)("SiteCode"), UserId, vSalesNo, ServerDate, False) = False Then
                            SqlTrans.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    Next
                End If

                '--------Code Added By Mahesh to Generate STR 
                If StrNumList <> "" Then
                    If objComn.UpdateStrData(vSalesNo, vSiteCode, StrNumList, SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
                If IsStrGenerate Then
                    Dim dsStr As New DataSet
                    dsStr = objComn.GenerateSaleOrderSTRNew(vSalesNo, vSiteCode, SpectrumCon, SqlTrans)
                    If dsStr Is Nothing Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    dtStrResult = dsStr
                Else
                    '' added by ketan vaidya Update STR Factory Remark
                    If objComn.UpdateSTRRemark(vSalesNo, vSiteCode, SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
                If IsNextSalesNo = True Then
                    Dim objType = "FO_DOC"
                    If objComn.UpdateDocumentNo("SalesOrder", SpectrumCon, SqlTrans, , objType) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                If IsNextInvoiceNo = True Then
                    If objComn.UpdateDocumentNo("CM", SpectrumCon, SqlTrans) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If




                SqlTrans.Commit()
                CloseConnection()

                'dsSOMain.Clear()
                SqlTrans.Dispose()
                isPromoApplied = False
                Return True
            Else
                SqlTrans.Rollback()
                CloseConnection()
                Return False
            End If

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    'Public Function PrepareSaveDataCCE(ByVal currentSalesinvoice As String, ByVal DayOpendate As DateTime, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef dsSOMain As DataSet, ByVal IsNextSalesNo As Boolean, ByVal IsNextInvoiceNo As Boolean, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal Storage As String, ByVal CVProgram As String, ByVal DocType As String, ByVal FinYear As String, ByVal UserId As String, ByVal ServerDate As DateTime, ByVal IsOBCreated As Boolean, Optional ByRef Voucherno As String = "", Optional ByRef VoucherDays As Int32 = 0, Optional ByRef isPromoApplied As Boolean = False, Optional ByVal DtDeletedData As DataTable = Nothing, Optional ByVal DeliveryLocInfo As List(Of SODeliveryLocationInfo) = Nothing, Optional ByVal IsbatchMgt As Boolean = False, Optional ByVal BatchBarcodeList As List(Of SpectrumCommon.BtachbarcodeInfo) = Nothing, Optional ByVal IsUpdate As Boolean = False, Optional ByVal StrNumList As String = "", Optional ByRef dtStrResult As DataSet = Nothing) As Boolean
    '    Try
    '        Dim objComm As New clsCommon
    '        Dim disc As Double
    '        If isPromoApplied = True Then

    '            Dim strSql As String = " DELETE FROM SALESDISCDTL WHERE SiteCode = '" & vSiteCode & "' AND FinYear = '" & FinYear & "' AND BillNo = '" & vSalesNo & "'"
    '            OpenConnection()
    '            Dim cmd As New SqlCommand(strSql, SpectrumCon)
    '            cmd.ExecuteNonQuery()

    '            disc = IIf(dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dsSOMain.Tables("SalesOrderDTL").Compute("Sum(DiscountAmount)", ""))
    '            If disc > 0 Then
    '                Dim dtDtl As DataTable = dsSOMain.Tables("SalesOrderDTL").Copy()

    '                Dim dtDisc As DataTable = objComm.CreateDiscSummary(DayOpendate, dtDtl, "", "SO201", vSiteCode, FinYear, vSalesNo, UserId, ServerDate, "FIRSTLEVEL", "TOPLEVEL")
    '                If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
    '                    Dim PromotionId = dtDisc(0)("PromotionId").ToString()
    '                    If dsSOMain.Tables.Contains("SoPackagingDiscDtl") Then
    '                        If dsSOMain.Tables("SoPackagingDiscDtl").Rows.Count > 0 Then
    '                            For Each drsdisc As DataRow In dsSOMain.Tables("SoPackagingDiscDtl").Rows
    '                                If drsdisc("DiscountAmount") > 0 Then
    '                                    drsdisc("PromotionId") = PromotionId
    '                                Else
    '                                    drsdisc("PromotionId") = ""
    '                                End If

    '                            Next

    '                        End If
    '                    End If
    '                    If dsSOMain.Tables.Contains("salesdiscdtl") Then
    '                        dsSOMain.Tables.Remove("salesdiscdtl")
    '                        dsSOMain.Tables.Add(dtDisc)
    '                    Else
    '                        dsSOMain.Tables.Add(dtDisc)
    '                    End If
    '                    'dsSOMain.AcceptChanges()
    '                End If
    '            End If
    '        Else
    '            If IsUpdate Then
    '                If dsSOMain.Tables.Contains("SoPackagingDiscDtl") Then
    '                    If dsSOMain.Tables("SoPackagingDiscDtl").Rows.Count > 0 Then
    '                        For Each drsdisc As DataRow In dsSOMain.Tables("SoPackagingDiscDtl").Rows
    '                            Dim result As DataRow() = dsSOMain.Tables("SalesOrderDTL").Select("billlineno='" + drsdisc("billlineno").ToString() + "'")
    '                            If drsdisc("DiscountAmount") > 0 Then
    '                                If result.Length > 0 Then
    '                                    Dim PromotionId = result(0)("OfferNo").ToString()
    '                                    Dim PromotionIdArray As Array = PromotionId.Split(",")
    '                                    If PromotionIdArray.Length > 0 Then
    '                                        For i = 0 To PromotionIdArray.Length - 1
    '                                            If PromotionIdArray(i).ToString().Contains("PR") Then
    '                                                drsdisc("PromotionId") = PromotionIdArray(i).ToString()
    '                                            End If
    '                                        Next
    '                                    Else
    '                                        drsdisc("PromotionId") = ""
    '                                    End If
    '                                Else
    '                                    drsdisc("PromotionId") = ""
    '                                End If
    '                            Else
    '                                drsdisc("PromotionId") = ""
    '                            End If
    '                        Next

    '                    End If
    '                End If
    '            End If

    '        End If
    '        Dim dtTempReserved As DataSet = dsSOMain.Copy()
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Reserved_Qty") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Reserved_Qty")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("Delivered_Qty") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("Delivered_Qty")
    '        End If
    '        'B.FreezeSB, B.FreezeSR,B.FreezeOB
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSB") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSB")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeSR") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeSR")
    '        End If
    '        If dtTempReserved.Tables("SalesOrderDTL").Columns.Contains("FreezeOB") Then
    '            dtTempReserved.Tables("SalesOrderDTL").Columns.Remove("FreezeOB")
    '        End If

    '        '----- SET CREDIT Sale Update 
    '        Dim dtCreditSaleData As New DataTable
    '        Dim DvSalesOrderClosed As New DataView(dsSOMain.Tables("SalesOrderHdr"), "SoStatus = 'Closed'", "", DataViewRowState.CurrentRows)
    '        If DvSalesOrderClosed.ToTable.Rows.Count > 0 Then
    '            Dim objclsReturn As New clsCashMemoReturn
    '            dtCreditSaleData = objclsReturn.getCreditSaleBillData("'" & dsSOMain.Tables("SalesOrderHdr")(0)("SaleOrderNumber") & "'")
    '            dtCreditSaleData.Columns.Add("NetCreditSaleAmount")
    '            dtCreditSaleData.Columns.Add("AdjustedCredit")
    '            objCM.SetCloseBillWiseCreditSaleAmt(dtCreditSaleData, dsSOMain)
    '        End If

    '        OpenConnectionCCE()
    '        SqlTransCCE = SpectrumConCCE.BeginTransaction()
    '        If objComm.SaveData(dtTempReserved, SpectrumConCCE, SqlTransCCE) = True Then
    '            SqlTransCCE.Commit()
    '            CloseConnectionCCE()

    '            dsSOMain.Clear()
    '            SqlTrans.Dispose()
    '            isPromoApplied = False
    '            Return True
    '        Else
    '            SqlTransCCE.Rollback()
    '            CloseConnection()
    '            CloseConnectionCCE()
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        SqlTransCCE.Rollback()
    '        CloseConnection()
    '        CloseConnectionCCE()
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function
    Public Function GenerateStrData(ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            Dim SqlTranSTR As SqlTransaction = Nothing
            OpenConnection()
            SqlTranSTR = SpectrumCon.BeginTransaction()

            '--------Code Added By Mahesh to Generate STR 

            If objComn.GenerateSaleOrderSTR(vSalesNo, vSiteCode, SpectrumCon, SqlTranSTR) = False Then
                SqlTranSTR.Rollback()
                CloseConnection()
                Return False
            End If
            SqlTranSTR.Commit()
            CloseConnection()
            SqlTranSTR.Dispose()
            Return True
        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GenerateStrDataNew(ByVal vSiteCode As String, ByVal vSalesNo As String) As DataSet
        Try
            Dim dtstrResult As New DataSet
            Dim SqlTranSTR As SqlTransaction = Nothing
            OpenConnection()
            SqlTranSTR = SpectrumCon.BeginTransaction()

            '--------Code Added By Mahesh to Generate STR 

            dtstrResult = objComn.GenerateSaleOrderSTRNew(vSalesNo, vSiteCode, SpectrumCon, SqlTranSTR)
            If dtstrResult Is Nothing Then
                SqlTranSTR.Rollback()
                CloseConnection()
                Return Nothing
            Else
                SqlTranSTR.Commit()
                CloseConnection()
                SqlTranSTR.Dispose()
            End If

            Return dtstrResult
        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function PrepareSaveClpData(ByRef dsCLOMain As DataSet, ByVal CLPProgId As String, ByVal CLPCustmId As String, ByVal CLPPoints As Double, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            Dim SqlTranClp As SqlTransaction = Nothing
            OpenConnection()

            SqlTranClp = SpectrumCon.BeginTransaction()
            If SaveClpData(dsCLOMain, SpectrumCon, SqlTranClp, vSiteCode, vSalesNo) = True Then
                If objComn.UpdateClpPoints(True, CLPProgId, CLPCustmId, CLPPoints, SpectrumCon, SqlTranClp) = True Then
                    SqlTranClp.Commit()
                    CloseConnection()
                    Return True
                End If
            End If
            SqlTranClp.Rollback()
            CloseConnection()
            Return False

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveClpData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and BillNo  = '" & vSalesNo & "' " & vbCrLf)

                Sqlda = New SqlDataAdapter(vStmtQry.ToString, Sqlconn)
                Sqlda.SelectCommand.Transaction = SqlTran

                Sqlcmdb = New SqlCommandBuilder(Sqlda)
                Sqlda.TableMappings.Add(tableName, tableName)
                Sqlda = Sqlcmdb.DataAdapter
                Sqlda.Update(dsSO, tableName)
            Next

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Save/Update Sales Order Details
    ''' </summary>
    ''' <param name="dsSO">DataSet</param>
    ''' <param name="Sqlconn">Sql Connection</param>
    ''' <param name="SqlTran">Sql Transaction</param>
    ''' <returns>True/False</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SaveData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                If tableName = "SalesInvoice" Or tableName = "OrderHdr" Or tableName = "OrderDtl" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and DocumentNumber= '" & vSalesNo & "' " & vbCrLf)
                Else
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and SaleOrderNumber  = '" & vSalesNo & "' " & vbCrLf)
                End If

                Sqlda = New SqlDataAdapter(vStmtQry.ToString, Sqlconn)
                Sqlda.SelectCommand.Transaction = SqlTran

                Sqlcmdb = New SqlCommandBuilder(Sqlda)
                Sqlda.TableMappings.Add(tableName, tableName)
                Sqlda = Sqlcmdb.DataAdapter
                Sqlda.Update(dsSO, tableName)
            Next

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Get Sales Order Default Config Information
    ''' </summary>
    ''' <param name="vSiteCode">Site Code</param>
    ''' <returns>Object Values</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancelation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSODefaultConfig(ByVal vSiteCode As String) As String

        Try
            _SOCreation = "SO201"
            _SOReturn = "SO202"
            'vStmtQry.Length = 0
            ''vStmtQry.Append("Select FldLabel, FldValue from DefaultConfig Where DocumentType='SalesOrder' And SiteCode='" & vSiteCode & "' ; " & vbCrLf)
            'vStmtQry.Append("Select DocumentType, DocumentTypeDesc from MstDocumentTypes Where DocumentTypeDesc like 'Sales %'; " & vbCrLf)

            'Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            'Sqlcmdb = New SqlCommandBuilder(Sqlda)
            'Sqlds = New DataSet
            'Sqlda.Fill(Sqlds)

            ''Sqlds.Tables(0).TableName = "DefaultConfig"
            'Sqlds.Tables(0).TableName = "MstDocumentTypes"

            'For Each drDocType As DataRow In Sqlds.Tables("MstDocumentTypes").Rows
            '    If drDocType("DocumentTypeDesc") = "Sales Order" Then
            '        _SOCreation = drDocType("DocumentType")
            '    End If
            '    If drDocType("DocumentTypeDesc") = "Sales Order Return" Then
            '        _SOReturn = drDocType("DocumentType")
            '    End If
            'Next

            Return ""

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Information of Printing format, Layout and Type, etc.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPrintingDetail() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select * from PrintingDetail Where DocumentType='SO201'")
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' so disc dtl
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPickUpDiscDetail(ByVal soNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select   ean ,Case isnull(sum(PickUpQuantity),0) when 0 then 0 else (isnull(sum(DiscountAmount),0)/isnull(sum(PickUpQuantity),0)) End as ReturnAmt from SoPackagingDiscDtl where SaleOrderNumber='" & soNo & "' and sitecode='" & siteCode & "' group by ean")
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' so Tax dtl
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPickUpTaxDetail(ByVal soNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select   ean ,Case isnull(sum(PickUpQuantity),0) when 0 then 0 else (isnull(sum(TaxAmount),0)/isnull(sum(PickUpQuantity),0)) End as ReturnAmt from sopackagingtaxdtl where SaleOrderNumber='" & soNo & "' and sitecode='" & siteCode & "' group by ean")
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Get Next Sales Invoice Number
    ''' </summary>
    ''' <param name="vSiteCode">SiteCode</param>
    ''' <param name="DocType">Document Type</param>
    ''' <returns>InvoiceNo</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetNextSOInvcNo(ByVal vSiteCode As String, Optional ByVal DocType As String = "") As String

        Try
            OpenConnection()
            vStmtQry.Length = 0

            vStmtQry.Append("Select IsNull(Max(SaleInvNumber) ,'1200000001')+1 as InvoiceNo From SalesInvoice " & vbCrLf)
            vStmtQry.Append(" Where SiteCode='" & vSiteCode & "' " & vbCrLf)
            If Not (DocType = String.Empty) Then
                vStmtQry.Append(" And DocumentType='" & DocType & "' " & vbCrLf)
            End If

            Sqlcmd = New SqlClient.SqlCommand(vStmtQry.ToString, SpectrumCon)
            SalesInvcNo = Sqlcmd.ExecuteScalar

            Sqlcmd.Dispose()
            CloseConnection()

            Return SalesInvcNo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Customer Information for Order
    ''' </summary>
    ''' <param name="vCustmCode">Customer Code</param>
    ''' <returns>Customer Table</returns>
    ''' <UsedBy>frmSalesOrderCreation.vb, frmSalesOrderUpdation.vb, frmSalesOrderCancel.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSOCustmerInfo(ByVal vCustmCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select cd.CustomerType, cd.CustomerNo, ad.AddressType, dbo.FnGetDesc('Address',ad.AddressType,'000') as AddressTypeName, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetDesc('TITLE',cd.TitleCode,'000') + ' ' +  cd.FirstName + ' ' + cd.LastName as CustomerName, " & vbCrLf)
            vStmtQry.Append(" cd.ResidencePhone, cd.MobilePhone, cd.OfficePhone, cd.EmailId, " & vbCrLf)
            vStmtQry.Append(" ad.AddressLn1 + ', ' + ad.AddressLn2 + ', ' + ad.AddressLn3 + ', ' + ad.AddressLn4 as AddressLn1 , " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CityCode) + ', ' + dbo.FnGetLocnDesc(ad.StateCode) + ', ' + " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CountryCode) + ', ' + ad.PinCode as AddressLn2 " & vbCrLf)
            vStmtQry.Append(" From CustomerSaleOrder cd, CustomerAddress ad Where cd.CustomerNo=ad.CustomerNo and " & vbCrLf)
            vStmtQry.Append(" ad.AddressType in (1,2) and ad.Status='1' and cd.CustomerNo='" & vCustmCode & "' " & vbCrLf)
            vStmtQry.Append(" Union " & vbCrLf)

            vStmtQry.Append(" Select 2 as CustomerType, cd.AccountNo as CustomerNo, ad.AddressType, dbo.FnGetDesc('Address',ad.AddressType,'000') as AddressTypeName, " & vbCrLf)
            vStmtQry.Append(" cd.TITLE + ' ' +  cd.FirstName + ' ' + cd.SurName as CustomerName, " & vbCrLf)
            vStmtQry.Append(" cd.Res_Phone as ResidencePhone, cd.MobileNo as MobilePhone,'' as OfficePhone,cd.EmailId, " & vbCrLf)
            vStmtQry.Append(" ad.AddressLn1 + ', ' + ad.AddressLn2 + ', ' + ad.AddressLn3 + ', ' + ad.AddressLn4 as AddressLn1, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CityCode) + ', ' + dbo.FnGetLocnDesc(ad.StateCode) + ', ' + " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ad.CountryCode) + ', ' + ad.PinCode as AddressLn2 " & vbCrLf)
            vStmtQry.Append(" From CLPCustomers cd, CLPCustomerAddress ad Where cd.CardNo=ad.CardNo and cd.ClpProgramId=ad.ClpProgramId " & vbCrLf)
            vStmtQry.Append(" And ad.AddressType in (1,2) and cd.Status='1' and cd.CardNo='" & vCustmCode & "' " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Sales Order Information
    ''' </summary>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSearchSO.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetSearchSalesOrder(ByVal siteCode As String, Optional ByVal RequiredStatus As Boolean = False, Optional ByVal IsOtherLocationDelivery As Boolean = False, Optional ByVal IsNewSalesOrder As Boolean = False) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Distinct sHdr.SaleOrderNumber as SalesNo,")
            '--changed by rama for bug 811,1155
            If RequiredStatus = True Then
                vStmtQry.Append(" sHdr.SOStatus AS Status,")
            End If
            '--
            If IsNewSalesOrder = True Then
                vStmtQry.Append("  CASE when shdr.IsDelivery=1 Then 'Delivery' Else 'Store Pickup' END as DeliveryType," & vbCrLf)
                vStmtQry.Append(" isnull(SHDR.OrderPreparationSite,'') 'OrderPreparationSite'," & vbCrLf) 'vipin
            End If
            'vStmtQry.Append(" dbo.FnGetDesc('Terminal',sHdr.TerminalID,sHdr.Sitecode) as TerminalID, " & vbCrLf)
            If IsNewSalesOrder = True Then
                vStmtQry.Append(" Case When sHdr.CustomerType='CLP' Then clpInfo.FirstName +' '+ clpInfo.SurName " & vbCrLf)
            Else
                vStmtQry.Append(" Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & vbCrLf)
            End If
            vStmtQry.Append(" When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & vbCrLf)
            If IsNewSalesOrder = True Then
                vStmtQry.Append(" ISNULL(clpInfo.CompanyName,'') AS CompanyName,ISNULL(CLPADD.Department,'') AS Department,sHdr.CreatedOn as SalesDate, " & vbCrLf)
            Else
                vStmtQry.Append(" sHdr.CustomerNo, sHdr.CreatedOn as SalesDate,   " & vbCrLf)
            End If
            If IsNewSalesOrder = True Then
                vStmtQry.Append("  sdel.DeliveryTime as DeliveryDate,  " & vbCrLf)
            Else
                vStmtQry.Append("  sHdr.PromisedDeliveryDate as DeliveryDate,  " & vbCrLf)
            End If

            vStmtQry.Append(" sHdr.CreatedBy as CashierName, sp.SalesPersonFullName as SalesPerson," & vbCrLf)
            If IsNewSalesOrder = True Then
                vStmtQry.Append("  ROUND(cast (sHdr.NetAmt as varchar),0) AS Amount, sHdr.CustomerNo " & vbCrLf)
            Else
                vStmtQry.Append("  sHdr.NetAmt AS Amount   " & vbCrLf)
            End If
            vStmtQry.Append("  from SalesOrderHDR sHdr " & vbCrLf)
            vStmtQry.Append(" inner join SalesOrderDtl sdtl on sHdr .SiteCode = sdtl.SiteCode and sHdr.SaleOrderNumber = sdtl.SaleOrderNumber " & vbCrLf)
            If IsNewSalesOrder = True Then
                vStmtQry.Append(" inner Join SOPackagingBoxDeliveryDtl sdel on sHdr.SaleOrderNumber=sdel.SaleOrderNumber and sdel.status=1 and sdel.Quantity <> sdel.DeliveredQty " & vbCrLf) ' and sdel.DeliveredQty<>sdel.Quantity
            End If

            vStmtQry.Append(" Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.DocumentNumber and sInvc.status=1 " & vbCrLf)
            vStmtQry.Append(" Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & vbCrLf)
            vStmtQry.Append(" Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & vbCrLf)
            If IsNewSalesOrder = True Then
                vStmtQry.Append(" Left Join CLPCustomerAddress CLPADD on shdr.CustomerNo= CLPADD.CardNo  and CLPADD .DefaultAddress = 1 and CLPADD .STATUS =1" & vbCrLf)
            End If
            vStmtQry.Append(" Left Join MstSalesPerson sp on sHdr.SalesExecutiveCode=sp.EmpCode " & vbCrLf)
            vStmtQry.Append(" Where  sHdr.SOStatus Not In " & stmtSalesStatus & vbCrLf)
            If IsOtherLocationDelivery Then
                vStmtQry.Append(" and sdtl.DeliverySiteCode='" & siteCode & "' and sdtl .SiteCode<>'" & siteCode & "' " & vbCrLf)
            Else
                vStmtQry.Append(" and sdtl.SiteCode='" & siteCode & "' " & vbCrLf)
            End If
            If IsNewSalesOrder = True Then
                vStmtQry.Append(" AND shdr.SaleOrderNumber like '%-%'" & vbCrLf)
            Else
                vStmtQry.Append(" AND shdr.SaleOrderNumber NOT like '%-%'" & vbCrLf)
            End If
            vStmtQry.Append(" Order By SalesDate Desc, SalesNo Desc " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "SalesOrderSearch")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function SearchSalesOrderWithPendingDelivery(ByVal siteCode As String) As DataSet
        Try
            Dim vStmtQry As String = "Select Distinct sHdr.SaleOrderNumber as SalesNo, Case When sHdr.CustomerType='CLP' Then clpInfo.SurName +' '+ clpInfo.FirstName " & _
                                     "When sHdr.CustomerType='SO' Then cInfo.CustomerName End as CustomerName, " & _
                                     "sHdr.CustomerNo, sHdr.CreatedOn as SalesDate, sHdr.PromisedDeliveryDate as DeliveryDate,  " & _
                                     "sHdr.CreatedBy as CashierName, sp.SalesPersonFullName as SalesPerson,sHdr.NetAmt AS Amount " & _
                                     "from SalesOrderHDR sHdr " & _
                                     "Inner join dbo.SalesOrderDeliveryLocInfo sodl on sHdr.SaleOrderNumber = sodl.SalesOrderNumber " & _
                                     "Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.DocumentNumber and sInvc.status =1" & _
                                     "Left Join CustomerSaleOrder cInfo on sHdr.CustomerNo=cInfo.CustomerNo " & _
                                     "Left Join CLPCustomers clpInfo on sHdr.CustomerNo=clpInfo.CardNo " & _
                                     "Left Join MstSalesPerson sp on sHdr.SalesExecutiveCode=sp.EmpCode " & _
                                     "Where  sHdr.SOStatus Not In ('Cancel','Closed','Return') and sodl.DeliverySiteCode = '" & siteCode & "' Order By SalesNo Desc "

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "SalesOrderSearch")
            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Search a Sales Order Information
    ''' </summary>
    ''' <param name="vSiteNo"> Site Code</param>
    ''' <param name="vSalesNo"> Sales Order Number</param>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSalesOrderCancel.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SetSalesOrderInSOCancel(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select A.ArticleCode ,A.DeliverySiteCode,A.BillLineNo,A.BatchBarcode , dbo.FnGetEANDesc(A.ArticleCode) as Discription, A.SellingPrice, A.Quantity, 0 as PickupQty, " & vbCrLf)
            vStmtQry.Append(" A.DeliveredQty,0 AS TaxInPer,0 AS TaxPer,  A.DiscountPercentage,A.DiscountAmount as Discount, A.NetAmount,A.CostAmount, A.ActualDeliveryDate as ExpDelDate, " & vbCrLf)
            vStmtQry.Append(" A.ReservedQty, A.LineDiscount, 50 as Stock, A.EAN , " & vbCrLf)
            vStmtQry.Append(" A.SellingPrice*A.Quantity as GrossAmt, A.ExclTaxAmt as ExclTaxAmt,A.TotalTaxAmount, A.Status, " & vbCrLf)
            vStmtQry.Append(" A.UnitOfMeasure, A.OfferNo, A.SaleOrderNumber,A.SalesStaffID, A.CreatedOn, A.IsCLPApplicable, A.ClpPoints, A.ClpDiscount,isnull(B.FreezeSB,0)as FreezeSB,isnull(B.FreezeSR,0)as FreezeSR,isnull(B.FreezeOB,0)as FreezeOB,A.remarks,A.IsCombo" & vbCrLf)
            vStmtQry.Append(" from SalesOrderDTL  A Inner join SalesInfoRecord B on A.SiteCode=B.SiteCode AND A.EAN=B.EAN AND A.ArticleCode=B.ArticleCode AND B.Srno=1 Where  A.SiteCode='" & vSiteNo & "' and A.SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            '-- added by rama ranjan for bug no 0000566
            vStmtQry.Append(" AND isnull(A.ArticleStatus,'')<>'Deleted' AND isnull(A.Status,0)<>0 " & vbCrLf)
            '--
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "ItemScanDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function SetSalesOrderPackVariationInSO(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select mart.lastnodecode, *,A.GrossAmount as GrossAmt, dbo.FnGetEANDesc(A.ArticleCode) as Discription,'' as ArticleType,'' as PromotionId,Cast( 0 as Decimal)  as TotalDiscPercentage,Cast( 0 as Decimal)  as TaxInPer,Cast( 0 as Decimal)  as TaxPer,'' as FirstLevel,'' as FirstLevelDisc,'' as TopLevel,0 as IsImageReq,0 as stock,0 as IsCLP,'' as LastNodeCode,Cast( 0 as Decimal) IncTaxAmt,0 as DeliveryIndex,Cast( 0 as Decimal)  as Discount,Cast( 0 as Decimal) as  PickupQty,0 as FreezeSB,0 as MinPayAmt,Cast( 0 as Decimal) as TotalPickUpAmt,0 as RowIndex,Cast( 0 as Decimal) as SrNo, 0 as FreezeOB,case when A.Status = 0 then 'Deleted' else 'Inserted' end as IsStatus,'False' as IsNew from SOItemPackagingBoxDtl A inner join MSTArticle mart on A.articlecode=mart.ArticleCode  where  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "' and A.Status=1" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "PackagingMaterial")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Dim dtReturnOrderComboDtl As DataTable
    Public Function SOPrintReturnOrderComboDetails() As DataTable
        Try 'vipin PC SO Merge 03-05-2018
            dtReturnOrderComboDtl = GetSOPrintOrderDetailsTableStruc()
            dtReturnOrderComboDtl.Rows.Clear()
            Return dtReturnOrderComboDtl
        Catch ex As Exception
        End Try
    End Function
    Public Function GetSalesOrderPickupHistory(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            'Upated by ketan Pickup History Changes
            vStmtQry.Length = 0
            'vStmtQry.Append("SELECT POD.PkgLineNo, POD.BillLineNo,POD.ArticleCode AS Orders,POD.Quantity AS Orderqty ,POD.PckgSize, POD.PckgQty, POD.PckgType,POD.CREATEDON AS DeliveryDate," & vbCrLf)
            'vStmtQry.Append("POD.DeliveredQty AS pickupQty,POD.Quantity - (SELECT ISNULL(SUM(ISNULL(delOD.DeliveredQty,0)),0) FROM pkgOrderDtl as delOD " & vbCrLf)
            'vStmtQry.Append("WHERE delOD.SaleOrderNumber =POD.SaleOrderNumber AND delOD.deliveryLineNo = POD.deliveryLineNo AND  delOD.EAN = POD.EAN AND" & vbCrLf)
            'vStmtQry.Append("delOD.CREATEDON <= POD.CREATEDON) AS PendingQty  FROM  pkgOrderDtl AS POD " & vbCrLf)
            'vStmtQry.Append("WHERE POD.SaleOrderNumber = '" & vSalesNo & "' and POD.SiteCode = '" & vSiteNo & "' ORDER BY POD.CREATEDON " & vbCrLf)
            '  Changes In PckgQty AS Discussed with Akshay and Jayan 
            vStmtQry.Append("SELECT POD.PkgLineNo, POD.BillLineNo,POD.ArticleCode AS Orders, POD.Quantity AS Orderqty, POD.PckgSize," & vbCrLf)
            vStmtQry.Append("Case WHEN POD.PckgSize =0 THEN 0 ELSE  POD.Quantity/POD.PckgSize END AS PckgQty," & vbCrLf)
            vStmtQry.Append("POD.PckgType,POD.CREATEDON AS DeliveryDate," & vbCrLf)
            vStmtQry.Append("POD.DeliveredQty AS pickupQty,POD.Quantity - (SELECT ISNULL(SUM(ISNULL(delOD.DeliveredQty,0)),0) FROM pkgOrderDtl as delOD " & vbCrLf)
            vStmtQry.Append("WHERE delOD.SaleOrderNumber =POD.SaleOrderNumber AND delOD.deliveryLineNo = POD.deliveryLineNo AND  delOD.EAN = POD.EAN AND" & vbCrLf)
            vStmtQry.Append("delOD.CREATEDON <= POD.CREATEDON) AS PendingQty  FROM  pkgOrderDtl AS POD " & vbCrLf)
            vStmtQry.Append("WHERE POD.SaleOrderNumber = '" & vSalesNo & "' and POD.SiteCode = '" & vSiteNo & "' ORDER BY POD.CREATEDON " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "PickupHistory")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function SetSalesOrderRemarks(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable

        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append(" select BillLineNo as SrNo, case when IsCombo = 'True' then 'Combo' else 'Single' end as ArticleType,case when Iscombo='True' then (select PackagingBoxName from SalesOrderBulkComboHdr where  ComboSrNo=BillLineNo and SaleOrderNumber='" & vSalesNo & "') else A.articlecode End As ArticleCode  , case when IsCombo = 'True' then CAST(BillLineNo As Varchar) + '-' + (select PackagingBoxName from SalesOrderBulkComboHdr where  ComboSrNo=BillLineNo and SaleOrderNumber='" & vSalesNo & "') + ' (' + cast((select COUNT(*) from SalesOrderBulkComboDtl where STATUS=1 and SaleOrderNumber='" & vSalesNo & "' and ComboSrNo=BillLineNo) as varchar) + ')'  else  dbo.FnGetEANDesc(A.ArticleCode) end  as ArticleName ,Remarks as Remark from  SalesOrderDtl a where Remarks<>'' and  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "'" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSOPickUpDiscount(ByVal vsaleNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(DiscountAmount),0) as DiscountAmount,ArticleCode from SoPackagingDiscDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and STATUS=1 group by ArticleCode" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSOPickUpTax(ByVal vsaleNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(TaxAmount),0) as TaxAmount,ArticleCode from SoPackagingTaxDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and STATUS=1 group by ArticleCode" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSOPickUpDiscountBillLineWise(ByVal vsaleNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(DiscountAmount),0) as DiscountAmount,PkgLineNo,BillLineNo from SoPackagingDiscDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and STATUS=1 group by PkgLineNo,BillLineNo" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSOPickUpTaxOnBillLineNo(ByVal vsaleNo As String, ByVal siteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(Round (TaxAmount,3)),0) as TaxAmount,PkgLineNo,BillLineNo from SoPackagingTaxDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and STATUS=1 group by PkgLineNo,BillLineNo" & vbCrLf)
            ' vStmtQry.Append("select isnull(sum(TaxAmount),0) as TaxAmount,ArticleCode from SoPackagingTaxDtl where SaleOrderNumber='" & vsaleNo & "' and SiteCode='" & siteCode & "' and STATUS=1 group by ArticleCode" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    '$$ added be ketan PC GST Changes  
    Public Function GetSalesOrderPickUpTax(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable
        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("select Sitecode ,SaleOrderNumber ,sum(TaxAmount) AS TaxAmount,BillLineNo,PkgLineNo from SOPackagingTaxDtl WHERE SaleOrderNumber  ='" & vSalesNo & "' and sitecode='" & vSiteNo & "' and Status=1 group By Sitecode,SaleOrderNumber,BillLineNo,PkgLineNo" & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '$$ By ketan SO GST CHANGES 
    Public Function GetSOComboTaxPer(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable
        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("SELECT Max (ValueInPer) AS ValueInPer ,ComboSrNo,SaleOrderNumber FROM ( " & vbCrLf)
            vStmtQry.Append("SELECT SaleOrderNumber,ComboSrNo ,CD.Articlecode,SUm (Value) ValueInPer from SalesOrderBulkComboDtl CD " & vbCrLf)
            vStmtQry.Append("LEFT Join SiteArticleTaxMapping tM ON CD.articleCode= tM.articleCode and CD.SiteCode =TM.SiteCode and CD.STATUS=1 " & vbCrLf)
            vStmtQry.Append(" LEFT JOIN MstTax MT ON MT.TaxCode =TM.TaxCode and MT.STATUS=1 WHERE SaleOrderNumber ='" & vSalesNo & "' and CD.SiteCode='" & vSiteNo & "' and TM.STATUS =1" & vbCrLf)
            vStmtQry.Append("Group By ComboSrNo,SaleOrderNumber,CD.Articlecode,Value ) AS A Group by ComboSrNo,SaleOrderNumber" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSalesOrderSTR(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable

        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            'vStmtQry.Append("select ROW_NUMBER() OVER(ORDER BY a.CREATEDON ) AS SrNO,a.CREATEDON as GeneratedOn  ,DocumentNumber as STRNo, case when a.SiteCode='0005' then 'Factory Sweets' when a.SiteCode='0007' then 'Warehouse - DF Namkeen'  else 'Factory Snacks' end as Requested , ExpectedDeliveryDt as StrDate, strtime as STRTime, a.CREATEDBY as GeneratedBy  from OrderHdr a inner join salesorderstrdetails b on  a.ReferenceNo=b.SaleOrderNumber and a.DocumentNumber=b.STRNumber where  OrderStatus<>'Closed' and   ReferenceNo='" & vSalesNo & "'" & vbCrLf)

            vStmtQry.Append("select ROW_NUMBER() OVER(ORDER BY a.CREATEDON ) AS SrNO,a.CREATEDON as GeneratedOn  ,DocumentNumber as STRNo," & vbCrLf)
            vStmtQry.Append("case when a.SiteCode='0005' then 'Factory Sweets' when a.SiteCode='0007' then 'Warehouse - DF Namkeen'  else 'Factory Snacks' end as Requested ," & vbCrLf)
            vStmtQry.Append(" ExpectedDeliveryDt as StrDate, strtime as STRTime, a.CREATEDBY as GeneratedBy  from OrderHdr a inner join salesorderstrdetails b " & vbCrLf)
            vStmtQry.Append(" on  a.ReferenceNo=b.SaleOrderNumber and a.DocumentNumber=b.STRNumber where  OrderStatus<>'Closed' and   ReferenceNo='" & vSalesNo & "'" & vbCrLf)
            '---Changed for Showing Seperate STR For Same Date and Time
            vStmtQry.Append("Group By DocumentNumber,a.CREATEDON,a.SiteCode,ExpectedDeliveryDt,strtime,a.CREATEDBY" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSalesOrderPickUpDisc(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable

        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("select  sum(DiscountAmount) as DiscountAmount ,BillLineNo,PkgLineNo  from SoPackagingDiscDtl where SaleOrderNumber ='" & vSalesNo & "' and sitecode='" & vSiteNo & "' group by BillLineNo,PkgLineNo " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSalesOrderSTRPrint(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable
        Try
            Dim dtSTR As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("select SOH.IsSTRRaised, ROW_NUMBER() OVER(ORDER BY OH.CREATEDON ) AS SrNO,OH.CREATEDON as GeneratedOn  ,OH.DocumentNumber as STRNo,case when OH.SiteCode='0005'" & vbCrLf)
            vStmtQry.Append("then 'Factory Sweets' when OH.SiteCode='0007' then 'Warehouse - DF Namkeen'  else 'Factory Snacks' end as Requested ," & vbCrLf)
            vStmtQry.Append("ExpectedDeliveryDt as StrDate,ExpectedDeliveryDt as   STRTime,OH.CREATEDBY as GeneratedBy  from OrderHdr OH JOIN SalesOrderHdr  SOH ON OH.ReferenceNo = SOH.SaleOrderNumber where OH.OrderStatus<>'Closed' and  ReferenceNo='" & vSalesNo & "'" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtSTR)

            Return dtSTR
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetClpContactDetails(ByVal vSiteNo As String, ByVal vCardNo As String) As String
        Try
            Dim dtContactNo As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("DECLARE @ContactValue AS  VARCHAR (MAX)" & vbCrLf)
            vStmtQry.Append("SELECT TOP 4 @ContactValue =COALESCE (@ContactValue+', ' , '')+ContactValue from CLPCustomerContacts where Status=1 AND  CardNo='" & vCardNo & "' And" & vbCrLf)
            vStmtQry.Append("ContactType Not in('Mobile Number','Email Address')and ClpProgramId=(SELECT ClpProgramId  FROM CLPProgramSiteMap  WHERE Status=1 and SiteCode ='" & vSiteNo & "')" & vbCrLf)
            vStmtQry.Append("SELECT ISNULL ( @ContactValue,'-') AS ContactNo" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtContactNo)

            Return dtContactNo.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetSalesOrderDocTaxes(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable

        Try
            Dim dtremark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append(" select stm.TaxName,st.TaxLabel as TaxCode,'Saved' as Status,st.TaxValue as TaxAmt,stm.TaxValue as TaxPercent, 'Exclusive'  as [Type]  from SalesOrderTaxDtls st inner join TaxSiteDocMap stm on st.TaxLabel=stm.TaxCode where   stm.DocumentType='SO201' and stm.IsDocumentLevelTax=1 and st.SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "'  and stm.STATUS=1 and st.STATUS=1" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)

            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtremark)

            Return dtremark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function SetSalesOrderDeliveryInSO(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet

        Try
            vStmtQry.Length = 0
            '-Added By Prasad AS Not Showing Order Details Tab Data
            ' vStmtQry.Append(" Select *, dbo.FnGetEANDesc(A.ArticleCode) as Discription,Cast( 0 as Decimal) as PickupQty,Cast( 0 as Decimal) as PendingQty,Cast( 0 as Decimal) as DispSrNo,'' as ArticleType,'False' as IsImageReq from SOPackagingBoxDeliveryDtl A where  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("Select SiteCode, FinYear, SaleOrderNumber, EAN, BillLineNo, PkgLineNo, DeliveryLineNo, ArticleCode, DeliveryDate, DeliveryTime, IsCustAddress, DeliveryAddress, ISNULL(DeliveredQty,0) As DeliveredQty, ReservedQty, LastPickUpDateTime, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, IsCombo, IsHeader, Quantity, UnitofMeasure, SrNo, dbo.FnGetEANDesc(A.ArticleCode) as Discription,Cast( 0 as Decimal) as PickupQty,Cast( 0 as Decimal) as PendingQty,Cast( 0 as Decimal) as DispSrNo,'' as ArticleType,'False' as IsImageReq, Convert(numeric(18,2),0) as SellingPrice,Convert(Varchar(200),'') as PckgSize,Convert(Varchar(200),'') as PckgQty, Convert(Varchar(50),'') as PackagingType, Convert(Varchar(200),'') as PackagingMaterial, Convert(Varchar(50),'') as PackagingBoxCode, Convert(numeric(18,2),0) as ArticleLevelTax,'' as STR,  " & vbCrLf)
            vStmtQry.Append("Convert(numeric(18,2),0) as TotalDiscPercentage,Convert(numeric(18,2),0) as ExclTaxAmt,Convert(numeric(18,2),0) as TotalTaxAmt,IsHeader,IsCombo, getdate() as ExpDelDate,Convert(Varchar(50),'') as Stock,Convert(bit,'True') as IsCLP,Convert(bit,'True') as Reserved, Convert(Varchar(50),'') as RowIndex,Convert(Varchar(50),'') as PackagingIndex," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as DeliveryIndex,Convert(Varchar(15),'') as DeliverySiteCode,Convert(Varchar(35),'') as EditBarcode,Convert(numeric(18,2),0) as CostAmount,Convert(bit,'False') as IsDelivery,Convert(bit,'False') as IsSTR ,Convert(bit,'False') as IsNew " & vbCrLf)
            vStmtQry.Append(" from SOPackagingBoxDeliveryDtl A where  A.status=1 and  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "' ")

            '--
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "PackagingDelivery")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function SetSalesOrderReturnSO(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataTable

        Try
            Dim vStmtQry As String
            'vStmtQry = " Select  dbo.FnGetEANDesc(A.ArticleCode) as Discription,*,Cast( 0 as Decimal) as PickupQty,Cast( 0 as Decimal) as PendingQty,Cast( 0 as Decimal) as DispSrNo,'' as ArticleType from SOPackagingBoxDeliveryDtl A where  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "' " & vbCrLf
            vStmtQry = " select SrNo, IsCombo As ArticleType,ArticleCode, dbo.FnGetEANDesc(ArticleCode) as Discription,UnitofMeasure, Cast( 0 as Decimal) as PickupQty, DeliveredQty,Quantity as OrderQty,DeliveryDate,DeliveryTime,DeliveryAddress,SiteCode, FinYear, SaleOrderNumber, EAN, BillLineNo, PkgLineNo, DeliveryLineNo,IsCustAddress, ReservedQty,LastPickUpDateTime from SOPackagingBoxDeliveryDtl  where DeliveredQty > 0 and  SiteCode='" & vSiteNo & "' and SaleOrderNumber='" & vSalesNo & "' " & vbCrLf

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(vStmtQry, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetAllTaxesAppliedToSiteArticleLevel(ByVal sitecode As String, ByVal docType As String) As DataTable
        Try
            Dim query As String = "select TaxCode,TaxName,Inclusive   from TaxSiteDocMap where IsDocumentLevelTax=0 and  STATUS = 1 and SiteCode = '" & sitecode & "' and DocumentType = '" & docType & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetFilledTable(ByVal strQuery As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Search a Sales Invoice Information
    ''' </summary>
    ''' <param name="vSiteNo"> Site Code</param>
    ''' <param name="vSalesNo"> Sales Order Number</param>
    ''' <returns>Sales DataSet</returns>
    ''' <UsedBy>frmSalesOrderCancel.vb, frmSalesOrderUpdation.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function SetInvoiceInSOCancel(ByVal vSiteNo As String, ByVal vSalesNo As String) As DataSet
        Try
            vStmtQry.Length = 0

            'vStmtQry.Append(" Select DocumentNumber as SalesNo, SaleInvNumber as InvoiceNo, DocumentType, " & vbCrLf)
            'vStmtQry.Append(" dbo.FnGetDesc('Terminal',TerminalID,SiteCode) as TerminalID, " & vbCrLf)
            'vStmtQry.Append(" TenderTypeCode as TenderType, " & vbCrLf)
            'vStmtQry.Append(" AmountTendered as InvoiceAmt, UserName, SOInvTime as InvoiceDate " & vbCrLf)
            'vStmtQry.Append(" From SalesInvoice Where status =1 and SiteCode='" & vSiteNo & "' And DocumentNumber='" & vSalesNo & "' " & vbCrLf)
            ''added By ketan pc Changes
            vStmtQry.Append(" SELECT * from ( " & vbCrLf)
            vStmtQry.Append(" SELECT  A.DocumentNumber as SalesNo, A.SaleInvNumber as InvoiceNo, A.DocumentType," & vbCrLf)
            vStmtQry.Append("dbo.FnGetDesc('Terminal',A.TerminalID,A.SiteCode) as TerminalID, A.TenderTypeCode as TenderType," & vbCrLf)
            vStmtQry.Append("CASE WHEN  A.TenderTypeCode = 'Credit' THEN  A.AmountTendered -Isnull (B.PaidAmount,0)" & vbCrLf)
            vStmtQry.Append("ELSE A.AmountTendered END AS InvoiceAmt,A.UserName, A.SOInvTime as InvoiceDate FROM	SALESINVOICE A " & vbCrLf)
            '------Changed by Prasad as Discussed With Akshay refbllno and documentno.
            vStmtQry.Append("LEFT JOIN  (SELECT CR.RefBillInvNumber ,CR.RefBillNo ,SUM (CR.AmountTendered ) AS PaidAmount,MAX(CR.CREATEDON ) " & vbCrLf)
            vStmtQry.Append("as CREATEDON ,CR.SiteCode FROM CreditReceipt CR WHERE  CR.TYPECODE ='SO' GROUP BY SiteCode,CR.RefBillNo,CR.RefBillInvNumber ) B ON A.DocumentNumber =B.RefBillNo AND A.SITECODE =B.SITECODE AND B.RefBillInvNumber=A.SaleInvNumber   " & vbCrLf)
            vStmtQry.Append("LEFT JOIN CreditSaleWriteOff WOFF ON  A.SiteCode=WOFF.SITECODE AND A.FINYEAR=WOFF.FINYEAR AND  A.DocumentNumber = WOFF.RefBillNo AND WOFF.RefBillInvNumber=A.SaleInvNumber AND A.TenderTypeCode = 'Credit'  " & vbCrLf)
            vStmtQry.Append(" Where A.status =1 AND  WOFF.AmountTendered IS null and A.SiteCode='" & vSiteNo & "' And A.DocumentNumber='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append(" ) AS A WHERE A.InvoiceAmt<> 0" & vbCrLf)


            vStmtQry.Append("UNION ALL SELECT	CR.RefBillNo , CR.BILLNO AS InvoiceNo ,CR.TypeCode AS DocumentType,dbo.FnGetDesc" & vbCrLf)
            vStmtQry.Append("('Terminal',CR.TerminalID,CR.SiteCode)AS TerminalID ,CR.TenderTypeCode AS TenderType , CR.AmountTendered" & vbCrLf)
            vStmtQry.Append("as InvoiceAmt, AU.UserName AS UserName,CR.CREATEDON as InvoiceDate" & vbCrLf)
            vStmtQry.Append("FROM CreditReceipt CR LEFT JOIN AuthUsers AU On AU.USERID = CR.CREATEDBY  WHERE CR.status =1 and CR.SiteCode='" & vSiteNo & "' And CR.RefBillNo='" & vSalesNo & "' " & vbCrLf)
            '' Added By ketan Write-off Amount disply in Payment History
            vStmtQry.Append("UNION ALL SELECT SI.DocumentNumber AS SalesNo, WOFF.WriteOffNumber AS InvoiceNo , WOFF.TYPECODE AS DocumentType," & vbCrLf)
            vStmtQry.Append("dbo.FnGetDesc('Terminal',WOFF.TerminalID,WOFF.SiteCode) as TerminalID,'Write-Off' as TenderType, WOFF.AmountTendered AS InvoiceAmt,AU.UserName, WOFF.CREATEDON as InvoiceDate" & vbCrLf)
            vStmtQry.Append("FROM SALESINVOICE SI INNER JOIN CreditSaleWriteOff WOFF ON  SI.SiteCode=WOFF.SITECODE AND SI.FINYEAR=WOFF.FINYEAR AND  SI.DocumentNumber = WOFF.RefBillNo AND SI.TenderTypeCode = 'Credit'" & vbCrLf)
            vStmtQry.Append("AND WOFF.RefBillInvNumber=SI.SaleInvNumber LEFT JOIN AUTHUSERs AU ON AU.UserId =WOFF.UpdatedBy WHERE WOFF.STATUS=1 AND SI.SiteCode='" & vSiteNo & "' And SI.DocumentNumber='" & vSalesNo & "' " & vbCrLf)


            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan, "InvoiceDetails")

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' m
    ''' </summary>
    ''' <param name="vSalesNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateSalesOrderCancel(ByVal vSalesNo As String) As DataRow
        vStmtQry.Length = 0
        vStmtQry.Append(" Select Distinct sHdr.SaleOrderNumber, sHdr.AdvanceAmt, sHdr.BalanceAmount, sHdr.DiscountAmt, " & vbCrLf)
        vStmtQry.Append(" sHdr.NetAmt, Sum(sDtl.NetSellingPrice) as GrossAmount, sHdr.SOStatus " & vbCrLf)
        vStmtQry.Append(" from SalesOrderHDR sHdr " & vbCrLf)
        vStmtQry.Append(" Inner Join SalesOrderDTL sDtl on sHdr.SaleOrderNumber=sDtl.SaleOrderNumber " & vbCrLf)
        vStmtQry.Append(" And sHdr.SOStatus=sDtl.ArticleStatus " & vbCrLf)
        vStmtQry.Append(" Left Join SalesInvoice sInvc on sHdr.SaleOrderNumber=sInvc.SaleOrderNumber and sInvc.status =1 " & vbCrLf)
        vStmtQry.Append(" Where  sHdr.SaleOrderNumber ='" & vSalesNo & "' " & vbCrLf)
        vStmtQry.Append(" Group By sHdr.SaleOrderNumber, sHdr.SOStatus,sHdr.NetAmt, " & vbCrLf)
        vStmtQry.Append(" sHdr.AdvanceAmt, sHdr.BalanceAmount, sHdr.DiscountAmt " & vbCrLf)

        Sqlda = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
        Sqlds = New DataSet
        Sqlda.Fill(Sqlds)

        Sqldr = Nothing
        If Sqlds.Tables(0).Rows.Count > 0 Then
            Sqldr = Sqlds.Tables(0).Rows(0)
        End If

        Return Sqldr
    End Function

    ''' <summary>
    ''' n
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReason(ByVal DocType As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daReturn As New SqlDataAdapter("SELECT REASONCODE,REASONNAME FROM REASONS where Doctype='" & DocType & "'", ConString)
            daReturn.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function SaveDiscount(ByVal DayOpendate As DateTime, ByVal dtSalesDetails As DataTable, ByVal strSalesInvoiceNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            If Not dtSalesDetails Is Nothing Then
                Dim strSiteCode As String
                Dim strCREATEDBY As String
                Dim strFinYear As String
                Dim strPromotionID As String
                Dim strPromotionValue As Decimal
                Dim dateCreated As Date
                Dim decTotalPromotionvalue As Decimal
                If dtSalesDetails.Rows.Count > 0 Then
                    If Not dtSalesDetails.Rows(0)("SiteCode") Is DBNull.Value Then
                        strSiteCode = dtSalesDetails.Rows(0)("SiteCode")
                    End If
                    If Not dtSalesDetails.Rows(0)("FinYear") Is DBNull.Value Then
                        strFinYear = dtSalesDetails.Rows(0)("SiteCode")
                    End If
                    If Not dtSalesDetails.Rows(0)("CREATEDBY") Is DBNull.Value Then
                        strCREATEDBY = dtSalesDetails.Rows(0)("CREATEDBY")
                    End If
                    If Not dtSalesDetails.Rows(0)("CreatedOn") Is DBNull.Value Then
                        dateCreated = dtSalesDetails.Rows(0)("CreatedOn")
                    End If

                    Dim objclsCommon As New clsCommon

                    Dim disc As Double
                    disc = IIf(dtSalesDetails.Compute("Sum(DiscountAmount)", "") Is DBNull.Value, 0, dtSalesDetails.Compute("Sum(DiscountAmount)", ""))
                    'If disc > 0 Then
                    Dim dtDtl As DataTable = dtSalesDetails.Copy()
                    Dim dtDisc As DataTable = objclsCommon.CreateDiscSummary(DayOpendate, dtDtl, "", "SO", strSiteCode, strFinYear, strCREATEDBY, strCREATEDBY, dateCreated, "FIRSTLEVEL", "TOPLEVEL")
                    If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
                        dsScan.Tables.Add(dtDisc)
                        Return True
                    End If
                    'End If

                    'If (objclsBirthListGlobal.InsertQuery("Insert into dbo.SalesDiscDtl(SiteCode,FinYear,BillNo,documentType,PromotionID,PromotionValue,CREATEDBY,CREATEDAT,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS ) values ('" + strSiteCode + "','" + strFinYear + "','" + strSalesInvoiceNo + "','SO','" + strPromotionID + "'," & strPromotionValue & ",'" + strCREATEDBY + "','" + strSiteCode + "','" + dateCreated + "',' ','','','1')", tran)) Then
                    '    Return True
                    'Else
                    '    Return False
                    'End If
                End If

            Else
                Return False
            End If



        Catch ex As Exception
            LogException(ex)
            Return False

        End Try
    End Function

    Public Function GetAllOrderInfo(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocNo As String, ByVal pDocType As String) As DataSet
        Try
            vStmtQry.Length = 0
            If pDocType = "SalesOrder" Then
                vStmtQry.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                vStmtQry.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, so.CustomerNo, so.CustomerType " & vbCrLf)
                vStmtQry.Append("from SalesInvoice si Inner join SalesOrderHdr so on si.DocumentNumber =so.SaleOrderNumber " & vbCrLf)
                vStmtQry.Append("Where si.status =1 and si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And si.DocumentNumber='" & pDocNo & "' And so.SOStatus='Open' ; " & vbCrLf & vbCrLf)


                vStmtQry.Append("Select sd.ArticleCode, am.ArticleName, sd.Quantity as OrderQty, sd.DeliveredQty, " & vbCrLf)
                vStmtQry.Append("0 as PickupQty, sd.SellingPrice As Rate, sd.NetAmount, 0.0 as PickupAmt, sd.GrossAmount, sd.EAN, sd.UnitofMeasure, sd.LineDiscount, sd.ExclTaxAmt " & vbCrLf)
                vStmtQry.Append("from SalesOrderDtl sd Inner join SalesOrderHdr sh on sd.SaleOrderNumber =sh.SaleOrderNumber " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on sd.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append("Where sd.SiteCode='" & pSiteCode & "' And sd.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And sd.SaleOrderNumber='" & pDocNo & "' And sh.SOStatus='Open' ; " & vbCrLf)

            ElseIf pDocType = "BirthList" Then

                'vStmtQry.Append("Select si.DocumentNumber, si.SaleInvNumber, si.TerminalID, si.TenderTypeCode, si.AmountTendered, si.ExchangeRate, " & vbCrLf)
                'vStmtQry.Append("si.CurrencyCode, Convert(Varchar(10),si.SOInvDate,105) as SOInvDate, si.UserName, bl.CustomerId As CustomerNo, bl.CustomerType " & vbCrLf)
                'vStmtQry.Append("from SalesInvoice si Inner join BirthList bl on si.DocumentNumber =bl.BirthListId " & vbCrLf)
                'vStmtQry.Append("Where si.SiteCode='" & pSiteCode & "' And si.FinYear='" & pFinYear & "' " & vbCrLf)
                'vStmtQry.Append("And si.DocumentNumber='" & pDocNo & "' And bl.BirthListStatus<>'Closed'; " & vbCrLf & vbCrLf)

                'vStmtQry.Append("Select bh.ArticleCode, am.ArticleName, bh.BookedQty as OrderQty, bh.DeliveredQty, 0 as PickupQty, " & vbCrLf)
                'vStmtQry.Append("Rate, bh.NetAmt as NetAmount, 0 as PickupAmt, bh.NetAmt as GrossAmount, bh.EAN, bh.SaleInvNumber, " & vbCrLf)
                'vStmtQry.Append("am.SaleUnitofMeasure as UnitofMeasure, IsNull(bh.TotalDiscountAmt,0) as LineDiscount, bh.TaxAmount as ExclTaxAmt, bh.CustomerId as CustomerNo, bh.CustomerType " & vbCrLf)
                'vStmtQry.Append("from BirthListSalesDtl bh Inner Join MstArticle am on bh.ArticleCode=am.ArticleCode " & vbCrLf)
                'vStmtQry.Append("Inner join BirthList bl on bh.BirthListId =bl.BirthListId " & vbCrLf)
                'vStmtQry.Append("Where bh.SiteCode='" & pSiteCode & "' And bh.FinYear='" & pFinYear & "' " & vbCrLf)
                'vStmtQry.Append("And bh.BirthListId='" & pDocNo & "' And bl.BirthListStatus<>'Closed' " & vbCrLf)

                vStmtQry.Append("Select blr.ArticleCode, am.ArticleName, blr.RequstedQty as OrderQty, blr.BookedQty, " & vbCrLf)
                vStmtQry.Append("blr.DeliveredQty, 0 as PickupQty from BirthListRequestedItems blr " & vbCrLf)
                vStmtQry.Append("Inner Join MstArticle am on blr.ArticleCode=am.ArticleCode " & vbCrLf)
                vStmtQry.Append("Where blr.SiteCode='" & pSiteCode & "' And blr.FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And blr.BirthListId='" & pDocNo & "' And blr.BookedQty>0 ;" & vbCrLf)

                vStmtQry.Append("Select * from BirthList Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & pDocNo & "' ;" & vbCrLf)

            End If

            Sqlda = New SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetStructOutboundDelivery(ByVal vSiteCode As String, ByVal vFinYear As String, ByVal vSalesNo As String, ByVal vDocType As String) As DataSet
        Try
            vStmtQry.Length = 0

            vStmtQry.Append("Select * From OrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)
            vStmtQry.Append("Select * From OrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vSalesNo & "' " & vbCrLf)
            vStmtQry.Append("And DocumentNumber='0'; " & vbCrLf & vbCrLf)

            If vDocType = "SalesOrder" Then
                vStmtQry.Append("Select * From SalesOrderHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From SalesOrderDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

            ElseIf vDocType = "BirthList" Then
                vStmtQry.Append("Select * From BirthListRequestedItems Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesHdr Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)

                vStmtQry.Append("Select * From BirthListSalesDtl Where SiteCode='" & vSiteCode & "' And FinYear='" & vFinYear & "' " & vbCrLf)
                vStmtQry.Append("And BirthListId='" & vSalesNo & "'; " & vbCrLf & vbCrLf)
            End If

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "OrderHdr"
            Sqlds.Tables(1).TableName = "OrderDtl"

            If vDocType = "SalesOrder" Then
                Sqlds.Tables(2).TableName = "SalesOrderHdr"
                Sqlds.Tables(3).TableName = "SalesOrderDtl"
            ElseIf vDocType = "BirthList" Then
                Sqlds.Tables(2).TableName = "BirthListRequestedItems"
                Sqlds.Tables(3).TableName = "BirthListSalesDtl"
                Sqlds.Tables(4).TableName = "BirthListSalesDtl"
            End If

            Return Sqlds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function PrepareSaveOutboundData(ByRef dsODMain As DataSet, ByVal vSiteCode As String, ByVal FinYear As String, ByVal vSalesNo As String, ByVal vOutbNo As String, ByVal vLocation As String) As Boolean
        Dim SqlTranOD As SqlTransaction = Nothing
        Try
            OpenConnection()
            SqlTranOD = SpectrumCon.BeginTransaction()

            If SaveOutboundData(dsODMain, SpectrumCon, SqlTranOD, vSiteCode, vSalesNo, vOutbNo) = True Then
                ' If objComn.UpdateDocumentNo("OutboundDelivery", SpectrumCon, SqlTranOD) = False Then
                If objComn.UpdateDocumentNo("OutBound", SpectrumCon, SqlTranOD) = False Then
                    SqlTrans.Rollback()
                    CloseConnection()
                    Return False
                End If

                For Each dr As DataRow In dsODMain.Tables(1).Select("DeliveredQty>0")
                    If objComn.UpdateStock(dr("SiteCode").ToString, dr("ArticleCode").ToString(), dr("EAN").ToString(), dr("UnitofMeasure").ToString(), dr("DeliveredQty").ToString(), dr("CreatedAt"), SpectrumCon, SqlTranOD, vLocation, 0) = False Then
                        SqlTrans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                Next
            Else
                SqlTrans.Rollback()
                CloseConnection()
                Return False
            End If
            SqlTranOD.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            SqlTranOD.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveOutboundData(ByRef dsSO As DataSet, ByRef Sqlconn As SqlConnection, ByRef SqlTran As SqlTransaction, ByVal vSiteCode As String, ByVal vSalesNo As String, ByVal vOutbNo As String) As Boolean
        Try
            For TbCnt = 0 To dsSO.Tables.Count - 1

                Dim tableName As String = dsSO.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSO.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSO.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                If tableName = "SalesOrderHdr" Or tableName = "SalesOrderDtl" Then
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and SaleOrderNumber  = '" & vSalesNo & "' " & vbCrLf)
                Else
                    vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)
                    vStmtQry.Append(" Where SiteCode = '" & vSiteCode & "' and DocumentNumber  = '" & vOutbNo & "' " & vbCrLf)
                End If

                Sqlda = New SqlDataAdapter(vStmtQry.ToString, Sqlconn)
                Sqlda.SelectCommand.Transaction = SqlTran

                Sqlcmdb = New SqlCommandBuilder(Sqlda)
                Sqlda.TableMappings.Add(tableName, tableName)
                Sqlda = Sqlcmdb.DataAdapter
                Sqlda.Update(dsSO, tableName)
            Next

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function

    Public Function GetOutboundData(ByVal SiteCode As String, ByVal SONumber As String) As DataTable
        Sqlda = New SqlClient.SqlDataAdapter("select BarCode,sum(DeliveredQty)DeliveredQty from OrderDtl where SaleOrderNumber='" & SONumber & "' AND SiteCode='" & SiteCode & "'  group by BarCode", SpectrumCon)
        Sqlcmdb = New SqlCommandBuilder(Sqlda)
        Dim sqldt As New DataTable
        Sqlda.Fill(sqldt)
        Return sqldt
    End Function

    Public Sub GetSOBulkComboTablestructure(ByRef DtSoBulkComboHdr As DataTable, ByRef DtSoBulkComboDtl As DataTable)
        Try
            '------  DtSoBulkComboHdr()
            ' DtSoBulkComboHdr.Columns.Add("BulkComboMstId", Type.GetType("System.Int64"))
            DtSoBulkComboHdr.Columns.Add("SaleOrderNumber", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("sitecode", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("ComboSrNo", Type.GetType("System.Int16"))
            DtSoBulkComboHdr.Columns.Add("FinYear", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("PackagingBoxPrintName", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("PackagingBoxCode", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("PackagingBox", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("OrderQty", Type.GetType("System.Decimal"))
            DtSoBulkComboHdr.Columns.Add("AdditionComments", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDAT", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDBY", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
            DtSoBulkComboHdr.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboHdr.Columns.Add("STATUS", Type.GetType("System.Int16"))
            DtSoBulkComboHdr.Columns.Add("IsFixedPrice", Type.GetType("System.Int16")) ''vipin PC SO Merge 03-05-2018
            ''-------- DtSoBulkComboDtl
            'DtSoBulkComboDtl.Columns.Add("BulkComboDetId", Type.GetType("System.Int64"))
            'DtSoBulkComboDtl.Columns.Add("BulkComboMstId", Type.GetType("System.Int64"))
            DtSoBulkComboDtl.Columns.Add("SaleOrderNumber", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("FinYear", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ArticleCode", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ArticleDescription", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("EAN", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("sitecode", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ComboSrNo", Type.GetType("System.Int16"))
            DtSoBulkComboDtl.Columns.Add("PackagedUOM", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("Qty", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("Weight", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("STRQty", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("STRExcludeCheck", Type.GetType("System.Boolean"))
            DtSoBulkComboDtl.Columns.Add("BaseUOM", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("ItemQtyBaseUOM", Type.GetType("System.Decimal"))
            DtSoBulkComboDtl.Columns.Add("CREATEDAT", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("CREATEDBY", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
            DtSoBulkComboDtl.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
            DtSoBulkComboDtl.Columns.Add("STATUS", Type.GetType("System.Int16"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSOBulkComboTableStruct(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "") As DataSet
        Try
            vStmtQry.Length = 0

            'vStmtQry.Append(" Select * From SoBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            'vStmtQry.Append(" Select BCD.* From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" SELECT  SBCH.BulkComboMstId, SBCH.SaleOrderNumber, SBCH.siteCode, SBCH.ComboSrNo,  ")
            vStmtQry.Append(" 		SBCH.PackagingBoxCode, MA.ArticleShortName AS PackagingBox ,SOD.Quantity AS OrderQty,   ")
            vStmtQry.Append("         SBCH.AdditionComments, SBCH.CREATEDAT, SBCH.CREATEDBY, SBCH.CREATEDON, SBCH.UPDATEDAT,   ")
            vStmtQry.Append("         SBCH.UPDATEDBY, SBCH.UPDATEDON, SBCH.STATUS  ")
            vStmtQry.Append(" FROM    SoBulkComboHdr AS SBCH INNER JOIN  ")
            vStmtQry.Append("                       SalesOrderDtl AS SOD ON SBCH.siteCode = SOD.SiteCode   ")
            vStmtQry.Append("                       AND SBCH.SaleOrderNumber = SOD.SaleOrderNumber   ")
            vStmtQry.Append("                       AND SBCH.PackagingBoxCode = SOD.ArticleCode AND   ")
            vStmtQry.Append("                       SBCH.ComboSrNo = SOD.BillLineNo   ")
            vStmtQry.Append("         INNER JOIN MstArticle AS MA ON SBCH.PackagingBoxCode = MA.ArticleCode  ")
            vStmtQry.Append(" WHERE  (SBCH.siteCode = '" & vSiteCode & "') AND (SBCH.SaleOrderNumber = '" & vSalesNo & "')")

            vStmtQry.Append(" Select BCD.* From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "SoBulkComboHdr"
            Sqlds.Tables(1).TableName = "SoBulkComboDtl"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOBulkComboTableStructForPC(ByVal vSiteCode As String, ByVal vSalesNo As String, Optional ByVal vStatus As String = "") As DataSet
        Try
            vStmtQry.Length = 0

            'vStmtQry.Append(" Select * From SoBulkComboHdr Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)
            'vStmtQry.Append(" Select BCD.* From SoBulkComboHdr as BCH Inner Join SoBulkComboDtl as BCD on  BCH.BulkComboMstId = BCD.BulkComboMstId  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select BCH.*,BCH.PackagingBoxName As PackagingBoxPrintName From SalesOrderBulkComboHdr as BCH   Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "'; " & vbCrLf)

            vStmtQry.Append(" Select BCD.* From SalesOrderBulkComboDtl as BCD  Where SiteCode='" & vSiteCode & " ' And SaleOrderNumber='" & vSalesNo & "' and Status='True'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "DtSoBulkComboHdr"
            Sqlds.Tables(1).TableName = "DtSoBulkComboDtl"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSOPrintHeaderTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as CompanyName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SiteName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Address," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as City," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),'') as PinCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'')as Contact," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as CustomerName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as CustomerCompanyName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as CustomerCellNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as CustomerDept," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as OtherContacts," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EmailId," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SalesOrderNo," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as OrderDate," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),'') as DeliveryRequired," & vbCrLf)
            vStmtQry.Append(" Convert(VarChar(20),'') as DeliveryDate," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as FooterMassage," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as GeneratedDate," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as TillNo," & vbCrLf)
            ' vStmtQry.Append(" Convert(Varchar(50),'') as BookedBy" & vbCrLf) 'PC SO Merge vipin 02-05-2018
            vStmtQry.Append(" Convert(Varchar(50),'') as BookedBy," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as CHSN," & vbCrLf)                     'vipin  27.07.2017 GST
            vStmtQry.Append(" Convert(Varchar(50),'') as LocalSalesTaxNo" & vbCrLf)
            Dim daSOPrintHeader As New SqlDataAdapter
            daSOPrintHeader = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintHeader As New DataTable
            daSOPrintHeader.Fill(dtSOPrintHeader)

            Return dtSOPrintHeader
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintOrderDetailsTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Numeric(18,0),0) as BillLineNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleDescription," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,2),0) as Quantity," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),'') as UnitofMeasure," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Price," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0)as Total," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as PckgSize," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as PckgQty," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as PckgType," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as PickupQuantity," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False') as IsCombo," & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as header," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as PackagingMaterial," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as HSN" & vbCrLf)   '''vipin PC SO Merge 03-05-2018

            Dim daSOPrintOrderDetails As New SqlDataAdapter
            daSOPrintOrderDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintOrderDetails As New DataTable
            daSOPrintOrderDetails.Fill(dtSOPrintOrderDetails)

            Return dtSOPrintOrderDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintPickupHistoryTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as Orders," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as Orderqty," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Pckgsize," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as PckgQty," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as PckgType," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'')as DeliveryAddress," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as DeliveryDate," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as DeliveryTime," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0)as pickupQty," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0)as PendingQty" & vbCrLf)
            Dim daSOPickupHistory As New SqlDataAdapter
            daSOPickupHistory = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSoPickupHistory As New DataTable
            daSOPickupHistory.Fill(dtSoPickupHistory)

            Return dtSoPickupHistory
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSOPrintPaymenytDetails1TableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Numeric(18,3),0) as GrossAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Discount," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as Tax," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric (18,3),0) as OtherCharges," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as NetAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0)as paidAmt," & vbCrLf)
            '   vStmtQry.Append(" Convert(Numeric(18,3),0) as BalAmt" & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as BalAmt," & vbCrLf) 'PC SO Merge vipin 02-05-2018
            vStmtQry.Append(" Convert(Numeric(18,3),0) as ReturnAmt" & vbCrLf)
            Dim daSOPrintPaymentDetails As New SqlDataAdapter
            daSOPrintPaymentDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintPaymentDetails1 As New DataTable
            daSOPrintPaymentDetails.Fill(dtSOPrintPaymentDetails1)

            Return dtSOPrintPaymentDetails1
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintPaymenytDetailsTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as Shift," & vbCrLf) 'vipin
            vStmtQry.Append(" Convert(Varchar(50),'') as Till," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CashierName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as InvoiceNo," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as PymentDateAndTime," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'') as Tender," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0)as Amt" & vbCrLf)
            ''PC SO Merge vipin 02-05-2018

            Dim daSOPrintPaymentDetails As New SqlDataAdapter
            daSOPrintPaymentDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintPaymentDetails As New DataTable
            daSOPrintPaymentDetails.Fill(dtSOPrintPaymentDetails)

            Return dtSOPrintPaymentDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintDeliveryDetailsTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as Orders," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,0),0) as BillLineNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as Orderqty," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Pckgsize," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as PckgQty," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as PckgType," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'')as DeliveryAddress," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as DeliveryDate," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as DeliveryTime," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0)as pickupQty," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0)as PendingQty" & vbCrLf)

            Dim daSOPrintDetailsDetails As New SqlDataAdapter
            daSOPrintDetailsDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintDetailsDetails As New DataTable
            daSOPrintDetailsDetails.Fill(dtSOPrintDetailsDetails)

            Return dtSOPrintDetailsDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintRemarksTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            'vStmtQry.Length = 0
            'vStmtQry.Append(" Select Convert(Varchar(100),'') as ArticleShortName," & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(100),'') as Remarks" & vbCrLf)
            vStmtQry.Length = 0 'PC SO Merge vipin 02-05-2018
            vStmtQry.Append(" Select Convert(Numeric(18,0),0) as BillLineNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleShortName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Remarks" & vbCrLf)
            Dim daSOPrintRemarks As New SqlDataAdapter
            daSOPrintRemarks = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintRemarks As New DataTable
            daSOPrintRemarks.Fill(dtSOPrintRemarks)

            Return dtSOPrintRemarks
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOPrintAddressTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(200),'') as Address" & vbCrLf)
            Dim daSOPrintAddress As New SqlDataAdapter
            daSOPrintAddress = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintAddress As New DataTable
            daSOPrintAddress.Fill(dtSOPrintAddress)

            Return dtSOPrintAddress
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetStrDetailsTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as STRRaised," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as STRRequestedfromSite," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as STRNo," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as STRDeliveryDate," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as STRDeliveryTime," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as CREATEDON," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(15),'')as CREATEDBY" & vbCrLf)
            Dim daSOPrintDetailsDetails As New SqlDataAdapter
            daSOPrintDetailsDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintDetailsDetails As New DataTable
            daSOPrintDetailsDetails.Fill(dtSOPrintDetailsDetails)

            Return dtSOPrintDetailsDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#Region "Table Struct for Delivery Invoice Challan"
    Public Function GetSOPrintInvoiceDeliveryChallanHeaderTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as ClientName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ClientAddress," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ClientPhoneNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as InvoiceNo," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as InvoiceDate," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ModeofPayment," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Consignee," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ConsigneeAddress," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ConsigneePhoneNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CurrencySymbol," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as AmountinWords," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DiscountName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Discount," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TaxName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Tax," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as TotalSum" & vbCrLf)
            Dim daSOPrintChallanHdrDetails As New SqlDataAdapter
            daSOPrintChallanHdrDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintChallanHdrDetails As New DataTable
            daSOPrintChallanHdrDetails.Fill(dtSOPrintChallanHdrDetails)

            Return dtSOPrintChallanHdrDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetSOPrintInvoiceDeliveryChallanTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as ArticleDescription," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as Quantity," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as UnitofMeasure," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,3),0) as Price," & vbCrLf)
            vStmtQry.Append(" Convert(Numeric(18,2),0) as Total" & vbCrLf)
            Dim daSOPrintChallanDetails As New SqlDataAdapter
            daSOPrintChallanDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOPrintChallanDetails As New DataTable
            daSOPrintChallanDetails.Fill(dtSOPrintChallanDetails)

            Return dtSOPrintChallanDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSOPrintTaxDiscDtls(ByVal OfferNo As String, ByVal Taxid As String) As DataSet
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select OfferName from Promotions where  OfferNo='" & OfferNo & "' and STATUS=1;" & vbCrLf)
            vStmtQry.Append("select TaxName from msttax where TaxCode='" & Taxid & "' AND STATUS=1" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)
            Sqlds.Tables(0).TableName = "Promotions"
            Sqlds.Tables(1).TableName = "MSTTAX"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


#End Region
    Public Function GetWriteOffAmt(ByVal SONumber As String, ByVal Sitecode As String, ByVal Fyear As String) As Double
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            ''CHANGE bY KETAN 
            vStmtQry.Append("select SUM (Isnull (AmountTendered,0)) AS AmountTendered  from CreditSaleWriteOff " & vbCrLf)
            vStmtQry.Append("where RefBillNo='" & SONumber & "' AND SiteCode='" & Sitecode & "' AND  FinYear='" & Fyear & "'" & vbCrLf)

            Dim daSOWriteoff As New SqlDataAdapter
            daSOWriteoff = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtSOWriteoff As New DataTable
            daSOWriteoff.Fill(dtSOWriteoff)
            If dtSOWriteoff.Rows.Count > 0 Then
                Return dtSOWriteoff.Rows(0)(0)
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetAmtPaidOtherMode(ByVal SONumber As String, ByVal Sitecode As String, ByVal Fyear As String) As Double
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("select isnull(sum(isnull(AmountTendered,0)),0) from SalesInvoice" & vbCrLf)
            ' vStmtQry.Append("where DocumentNumber='" & SONumber & "' and TenderTypeCode<>'Credit' and STATUS=1 AND Sitecode='" & Sitecode & "' AND FinYear='" & Fyear & "'" & vbCrLf)
            vStmtQry.Append("where DocumentNumber='" & SONumber & "' and TenderTypeCode<>'Credit' and STATUS=1 AND Sitecode='" & Sitecode & "'" & vbCrLf) 'AND FinYear='" & Fyear & "'
            Dim daAmt As New SqlDataAdapter
            daAmt = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtAmt As New DataTable
            daAmt.Fill(dtAmt)

            Return dtAmt.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDiscUsedData(ByVal SONumber As String, ByVal Sitecode As String, Optional ByVal ArticleCode As String = Nothing) As DataTable
        Try
            Dim StmtQry As New StringBuilder
            StmtQry.Length = 0
            StmtQry.Append("SELECT * from  FnGetDiscountPersentage ('" & SONumber & "','" & ArticleCode & "','" & Sitecode & "',DEFAULT)" & vbCrLf)
            Dim daDisc As New SqlDataAdapter
            daDisc = New SqlClient.SqlDataAdapter(StmtQry.ToString, SpectrumCon)
            Dim dtDisc As New DataTable
            daDisc.Fill(dtDisc)

            Return dtDisc
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetWriteOffAmt(ByVal SONumber As String, ByVal Sitecode As String) As DataTable
        Try
            Dim StmtQry As New StringBuilder
            StmtQry.Length = 0
            StmtQry.Append("SELECT RefBillNo , SiteCode ,SUM (Isnull (AmountTendered,0)) WriteOffAmountTender FROM CreditSaleWriteOff " & vbCrLf)
            StmtQry.Append(" Where SiteCode='" & Sitecode & "' And RefBillNo='" & SONumber & "' and Status=1 " & vbCrLf)
            StmtQry.Append("Group By RefBillNo , SiteCode" & vbCrLf)
            Dim daWriteOff As New SqlDataAdapter
            daWriteOff = New SqlClient.SqlDataAdapter(StmtQry.ToString, SpectrumCon)
            Dim dtWriteOff As New DataTable
            daWriteOff.Fill(dtWriteOff)
            Return dtWriteOff
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CreatePathForPrint(ByVal dtHeaderDetails As DataTable, ByVal NEWdtDeliveryDetails As DataTable, ByVal SOPath As String, Optional ByVal PrintName As String = "", Optional ByVal consolidated As Boolean = False, Optional ByVal PrintFormOrderPre As Boolean = True) As String
        Try
            Dim PathSaved As String
            Dim SalesOrderNo As String
            Dim CustName As String
            Dim CustomerCompanyName As String
            Dim DeliveryTime As String
            If dtHeaderDetails.Rows.Count > 0 Then
                SalesOrderNo = dtHeaderDetails.Rows(0)("SalesOrderNo")
                CustName = dtHeaderDetails.Rows(0)("CustomerName")
                CustomerCompanyName = dtHeaderDetails.Rows(0)("CustomerCompanyName")
                CustomerCompanyName = CustomerCompanyName.Replace(".", "")
            End If
            If Not NEWdtDeliveryDetails Is Nothing AndAlso NEWdtDeliveryDetails.Rows.Count > 0 Then
                DeliveryTime = Convert.ToDateTime(NEWdtDeliveryDetails(0)("DeliveryTime")).ToString("dd-MM-yyyy_HH-mm")
            End If

            SOPath = SOPath & "\Sales Order\" & SalesOrderNo & ""
            ' SOPath = "D" + SOPath.Substring(1)
            If Not Directory.Exists(SOPath) Then
                Directory.CreateDirectory(SOPath)
            End If

            If consolidated = True Then
                PathSaved = SOPath & "\" & CustName & "-" & CustomerCompanyName & "-" & SalesOrderNo.Substring(SalesOrderNo.LastIndexOf("-"), (SalesOrderNo.Length - SalesOrderNo.LastIndexOf("-"))) & "-" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            Else
                If PrintFormOrderPre = True Then
                    PathSaved = SOPath & "\" & CustName & "-" & CustomerCompanyName & "-" & SalesOrderNo.Substring(SalesOrderNo.LastIndexOf("-"), (SalesOrderNo.Length - SalesOrderNo.LastIndexOf("-"))) & "-DD-" & DeliveryTime & "-" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                Else
                    PathSaved = SOPath & "\" & PrintName & "-" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
                End If
            End If
            Return PathSaved
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function SOPrintComboVariationDetails(ByVal dtOrderComboDetails As DataTable, ByVal NEWdtOrderComboDetails As DataTable) As DataTable
        Try
            If Not NEWdtOrderComboDetails Is Nothing AndAlso NEWdtOrderComboDetails.Rows.Count > 0 Then
                Dim SrNo As String = ""
                Dim Iscombo As Boolean = False
                Dim header As Integer = 0
                Dim SrNo1 As String = ""
                Dim datatable_Combovariation As DataTable
                datatable_Combovariation = NEWdtOrderComboDetails.Copy
                datatable_Combovariation.Rows.Clear()
                For Each drNew As DataRow In NEWdtOrderComboDetails.Rows
                    SrNo = drNew("SrNo")
                    Iscombo = drNew("IsCombo")
                    header = drNew("header")
                    SrNo1 = SrNo.Substring(0, 1)

                    If SrNo.Contains(".") Then
                        If Iscombo = True AndAlso header = 1 Then
                            Dim DVdtOrderComboDetails_ComboVariation As New DataView(dtOrderComboDetails)
                            DVdtOrderComboDetails_ComboVariation.RowFilter = "IsCombo =1 AND header= 0 AND SrNo= '" & SrNo1 & "'"
                            For Each drPrintCombo As DataRow In DVdtOrderComboDetails_ComboVariation.ToTable.Rows
                                datatable_Combovariation.ImportRow(drPrintCombo)
                            Next

                        End If
                    End If
                Next
                If Not datatable_Combovariation Is Nothing AndAlso datatable_Combovariation.Rows.Count > 0 Then
                    ' Mantis 2981 Vipin
                    Dim TobeDistinct As String() = {"BillLineNo", "SrNo", "ArticleDescription", "Quantity", "UnitOfMeasure", "Price", "Total", "PckgSize", "PckgQty", "PckgType", "PickUpQuantity", "IsCombo", "Header", "PackagingMaterial", "HSN"}
                    datatable_Combovariation = datatable_Combovariation.DefaultView.ToTable(True, TobeDistinct)
                    For Each drPrintCombo As DataRow In datatable_Combovariation.Rows
                        SrNo = drPrintCombo("SrNo")
                        SrNo1 = SrNo.Substring(0, 1)
                        Dim foundRow() As DataRow
                        Dim ComboRow() As DataRow
                        foundRow = NEWdtOrderComboDetails.Select("IsCombo =1 AND header= 0 AND SrNo= '" & SrNo1 & "'")
                        ComboRow = datatable_Combovariation.Select("IsCombo =1 AND header= 0 AND SrNo= '" & SrNo1 & "'")
                        If foundRow.Count <> ComboRow.Count Then
                            NEWdtOrderComboDetails.ImportRow(drPrintCombo)
                        End If

                    Next
                End If

            End If
            ' Return NEWdtOrderComboDetails
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
  
#Region "SO History"

    ''' <summary>
    ''' Get SO Document Information Customer Wise
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="FinYear"></param>
    ''' <param name="DocumentType"></param>
    ''' <param name="SelectedCustomerCode"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetDocumentInfoCustmWise(ByVal SiteCode As String, ByVal FinYear As String, ByVal DocumentType As String, ByVal SelectedCustomerCode As String, Optional ByVal IsNewSalesOrder As Boolean = False) As DataTable
        Try
            Dim daDocInfo As New SqlDataAdapter
            Dim dtDocInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            If DocumentType = "SalesOrder" Then

                SqlQuery.Append("Select ROW_NUMBER() over (Order By UpdatedOn Desc ) as SrNo,CREATEDON as BookingDate,CreatedOn as BookingTime,MstSite.SiteShortName,SaleOrderNumber as SalesOrderNo, " & vbCrLf)
                SqlQuery.Append("  SalesExecutiveCode as SalesPerson,LineItems as TotalItems,ROUND( NetAmt,0) as TotalAmount,ROUND((AdvanceAmt-isnull(CreditSaleRecords.CreditAmt,0)+isnull(CreditAdjustmentAmt,0)),0) as AmountPaid, " & vbCrLf)
                SqlQuery.Append("ROUND((SalesOrderHdr.NetAmt- ((AdvanceAmt-isnull(CreditSaleRecords.CreditAmt,0)+isnull(CreditAdjustmentAmt,0)))-isnull(CreditSaleWriteOff.WriteOffAmt,0)),0) AS AmountDue " & vbCrLf)
                SqlQuery.Append("FROM SalesOrderHdr LEFT OUTER JOIN (SELECT DocumentNumber, SUM(AmountTendered) AS CreditAmt FROM SalesInvoice AS SInv" & vbCrLf)
                SqlQuery.Append(" WHERE  SInv.status =1 and (TenderTypeCode = 'Credit') GROUP BY DocumentNumber) AS CreditSaleRecords ON SalesOrderHdr.SaleOrderNumber = CreditSaleRecords.DocumentNumber" & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (SELECT     RefBillNo AS DocumentNumber, SUM(ISNULL(AmountTendered, 0)) AS CreditAdjustmentAmt" & vbCrLf)
                SqlQuery.Append("FROM CreditReceipt AS Cr GROUP BY RefBillNo) AS CrdSaleAdjust ON SalesOrderHdr.SaleOrderNumber = CrdSaleAdjust.DocumentNumber" & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (SELECT     RefBillNo, SUM(ISNULL(AmountTendered, 0)) AS WriteOffAmt " & vbCrLf)
                SqlQuery.Append(" FROM CreditSaleWriteOff  where STATUS='1' GROUP BY RefBillNo) AS CreditSaleWriteOff  ON SalesOrderHdr.SaleOrderNumber = CreditSaleWriteOff.RefBillNo" & vbCrLf)
                SqlQuery.Append(" LEFT OUTER JOIN (SELECT SiteShortName,SiteCode FROM MSTSite)AS MstSite ON salesOrderhdr.SiteCode=mstsite.SiteCode " & vbCrLf)
                SqlQuery.Append(" Where mstsite.SiteCode='" & SiteCode & "' and SOStatus not in('Cancel','Return')" & vbCrLf)
                SqlQuery.Append(" And CustomerNo='" & SelectedCustomerCode & "'")
                '' Added By Ketan Remove FinYear Condition As Discussed With Sagar and Akshay For the PC ISSE  :- And FinYear='" & FinYear & "'
                If IsNewSalesOrder = True Then
                    SqlQuery.Append(" AND SalesOrderHdr.SaleOrderNumber like '%-%'" & vbCrLf)
                Else
                    SqlQuery.Append(" AND SalesOrderHdr.SaleOrderNumber NOT like '%-%'" & vbCrLf)
                End If
                SqlQuery.Append(" Order By UpdatedOn Desc" & vbCrLf)

            End If

            daDocInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtDocInfo = New DataTable
            daDocInfo.Fill(dtDocInfo)

            Return dtDocInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Item Information of SO
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetItemInfoSO(ByVal SiteCode As String, ByVal SalesNo As String) As DataTable
        Try
            Dim daItemInfo As New SqlDataAdapter
            Dim dtItemInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append(" select '' as SrNo,CASE WHEN IsCombo=1 THEN 'Combo' eLSE 'Single' end as ArticleType,  " & vbCrLf) 'ROW_NUMBER() over (Order BY SOIP.createdon)
            SqlQuery.Append(" SOIP.ArticleCode,CASE WHEN SOIP.ISCOMBO=1 THEN  SOBC.PackagingBoxName else dbo.FnGetEANDesc(SOIP.ArticleCode)END  as Discription,SOIP.UnitOfMeasure," & vbCrLf)
            SqlQuery.Append(" SOIP.Quantity,SOIP.SellingPrice,DiscountAmount,SOIP.NetAmount,PckgSize,PckgQty,SOIP.PckgType," & vbCrLf)
            SqlQuery.Append("   PckgMaterial,ISNULL(SOIP.DeliveredQty,0)+ISNULL(cmdtl.Quantity,0)  as  PickupQty, SOIP.TotalTaxAmount,SOIP.IsHeader from SOItemPackagingBoxDtl SOIP " & vbCrLf)
            SqlQuery.Append(" Left  Join CashMemoDtl cmdtl" & vbCrLf)
            SqlQuery.Append("   ON SOIP.ArticleCode=cmdtl.ArticleCode AND SOIP.SiteCode=cmdtl.SiteCode AND SOIP.SaleOrderNumber=cmdtl.Returncmno  " & vbCrLf)
            SqlQuery.Append("  left join SalesOrderBulkComboHdr SOBC" & vbCrLf)
            SqlQuery.Append("  on SOIP.SiteCode=SOBC.siteCode and SOIP.BillLineNo=SOBC.ComboSrNo and SOIP.SaleOrderNumber=SOBC.SaleOrderNumber" & vbCrLf)
            SqlQuery.Append(" where  SOIP.Status=1 and SOIP.SiteCode='" & SiteCode & "' and SOIP.SaleOrderNumber='" & SalesNo & "' Order BY SOIP.BillLineNo")

            daItemInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtItemInfo = New DataTable
            daItemInfo.Fill(dtItemInfo)

            Return dtItemInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Order Information Delivery Details
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetOrderInfoDelivery(ByVal SiteCode As String, ByVal SalesNo As String, Optional ByVal PkgFieldAllowed As Boolean = False) As DataTable
        Try
            Dim daDeliveryInfo As New SqlDataAdapter
            Dim dtDeliveryInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append("select SOPD.SrNo as SrNo,IsNUll(ComboSrNo,0) as ComboSrNo, " & vbCrLf) 'ROW_NUMBER() over (Order BY SOPD.createdon)
            SqlQuery.Append(" CASE WHEN SOPD.IsCombo=1 THEN 'Combo' eLSE 'Single' end as ArticleType, " & vbCrLf)
            SqlQuery.Append("CASE WHEN SOPD.ISCOMBO=1 THEN  soh.PackagingBoxName else dbo.FnGetEANDesc(SOPD.ArticleCode)END " & vbCrLf)
            SqlQuery.Append("as Discription,SOPD.Quantity,SOPD.UnitofMeasure,SOI.PckgMaterial," & vbCrLf)
            If PkgFieldAllowed = True Then
                SqlQuery.Append("SOI.PckgSize,CASE WHEN SOPD.UnitofMeasure='KGS' AND SOI.PckgSize<>0 THEN  CAST(CAST((SOPD.Quantity-SOPD.DeliveredQty)/SOI.PckgSize AS Numeric(18,2)) AS VARCHAR) + ' ' + SOI.PckgType " & vbCrLf)
                SqlQuery.Append("WHEN SOI.PckgSize=0 THEN  CAST( '-' as varchar) else CAST( '-' as varchar) END AS PckgQty," & vbCrLf)
            End If
            SqlQuery.Append("SOPD.DeliveryDate,SOPD.DeliveryTime,DeliveryAddress,isnull(SOPD.DeliveredQty,0) + isnull (SOPD.ReturnQty,0) as PickupQty,SOPD.ReturnQty,SOPD.LastPickUpDateTime," & vbCrLf)
            SqlQuery.Append("(SOPD.Quantity-SOPD.DeliveredQty-isnull (SOPD.ReturnQty,0)) as PendingQty,SOPD.BillLineNo,SOPD.EAN,SOPD.IsHeader from SOPackagingBoxDeliveryDtl SOPD JOIN SOItemPackagingBoxDtl SOI " & vbCrLf)

            SqlQuery.Append("  ON SOPD.SaleOrderNumber=SOI.SaleOrderNumber AND SOPD.SiteCode=SOI.SiteCode AND SOI.PkgLineNo=SOPD.PkgLineNo" & vbCrLf)
            SqlQuery.Append("left join(select PackagingBoxName,SiteCode,SaleOrderNumber,ComboSrNo from SalesOrderBulkComboHdr) soh" & vbCrLf)
            SqlQuery.Append("   on sopd.SiteCode=soh.siteCode and sopd.BillLineNo=soh.ComboSrNo and sopd.SaleOrderNumber=soh.SaleOrderNumber" & vbCrLf)
            SqlQuery.Append("  where SOPD.Status=1 and SOI.Status=1 and SOPD.SiteCode='" & SiteCode & "' and SOPD.SaleOrderNumber='" & SalesNo & "' order by SOPD.BillLineNo" & vbCrLf)
            'If IsPickup = True Then
            '    SqlQuery.Append("and  SOPD.DeliveredQty>0 Group By dbo.FnGetEANDesc(SOPD.ArticleCode),soh.ComboSrNo,sopd.CREATEDON,SOPD.IsCombo,soh.PackagingBoxName,")
            '    SqlQuery.Append("sopd.Quantity,sopd.UnitofMeasure,SOI.PckgMaterial,SOPD.DeliveryDate, SOPD.DeliveryTime,DeliveryAddress,")
            '    SqlQuery.Append("SOPD.DeliveredQty,SOPD.LastPickUpDateTime" & vbCrLf)
            'End If
            daDeliveryInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtDeliveryInfo = New DataTable
            daDeliveryInfo.Fill(dtDeliveryInfo)

            Return dtDeliveryInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Pickup Information Delivery Details
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
     Public Function GetPickupInfo(ByVal SiteCode As String, ByVal SalesNo As String, Optional ByVal PkgFieldAllowed As Boolean = False) As DataTable
        Try
            Dim daPickupInfo As New SqlDataAdapter
            Dim dtPickupInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append("select ROW_NUMBER()  over (Order By  PkgOD.CreatedOn) as SrNo,IsNUll(ComboSrNo,0) as ComboSrNo, " & vbCrLf)
            SqlQuery.Append("  CASE WHEN PkgOD.IsCombo=1 THEN 'Combo' ELSE 'Single' end as ArticleType, " & vbCrLf)
            SqlQuery.Append("CASE WHEN SOPD.ISCOMBO=1 THEN  SOBH.PackagingBoxName else dbo.FnGetEANDesc(SOPD.ArticleCode)END as Discription ," & vbCrLf)
            SqlQuery.Append("PkgOD.Quantity,PkgOD.UnitofMeasure,SOI.PckgMaterial," & vbCrLf)

            If PkgFieldAllowed = True Then
                SqlQuery.Append("PkgOD.PckgSize,CASE WHEN  PkgOD.UnitofMeasure='KGS' THEN  CAST(CAST(PkgOD.DeliveredQty*PkgOD.PckgSize AS Numeric(18,2)) AS VARCHAR) + ' ' + PkgOD.PckgType  " & vbCrLf)
                SqlQuery.Append("else CAST( '-' as varchar) END AS PckgQty," & vbCrLf)
            End If
            SqlQuery.Append("SOPD.DeliveryDate,SOPD.DeliveryTime,SOPD.DeliveryAddress," & vbCrLf)
            SqlQuery.Append(" PkgOD.DeliveredQty as PickupQty,SOPD.ReturnQty,PkgOD.CreatedOn as PickupDate," & vbCrLf)
            SqlQuery.Append(" (SOPD.Quantity-SOPD.DeliveredQty -isnull (SOPD.ReturnQty,0)) AS PendingQty from pkgorderdtl as PkgOD Inner Join" & vbCrLf)
            SqlQuery.Append("  SOPackagingBoxDeliveryDtl as SOPD ON PkgOD.SiteCode=SOPD.SiteCode AND PkgOD.SaleOrderNumber=SOPD.SaleOrderNumber" & vbCrLf)
            SqlQuery.Append("   AND SOPD.deliveryLineNo = PkgOD.deliveryLineNo AND  SOPD.EAN = PkgOD.EAN" & vbCrLf)
            SqlQuery.Append(" LEFT JOIN SOItemPackagingBoxDtl SOI ON PkgOD.SiteCode=SOI.SiteCode AND PkgOD.SaleOrderNumber=SOI.SaleOrderNumber" & vbCrLf)
            SqlQuery.Append(" AND  SOI.EAN = PkgOD.EAN AND SOI.BillLineNo=PkgOD.BillLineNo AND SOI.PkgLineNo=PkgOD.PkgLineNo" & vbCrLf)
            SqlQuery.Append(" LEFT JOIN SalesOrderBulkComboHdr SOBH ON PkgOD.SiteCode=SOBH.siteCode AND SOBH.SaleOrderNumber=PkgOD.SaleOrderNumber" & vbCrLf)
            SqlQuery.Append("  AND pkgoD.BillLineNo=sobh.ComboSrNo  where PkgOD.Status=1 and SOPD.Status=1 and PkgOD.SiteCode='" & SiteCode & "' and PkgOD.SaleOrderNumber='" & SalesNo & "'" & vbCrLf)
            SqlQuery.Append(" and  PkgOD.DeliveredQty>0 " & vbCrLf)

            daPickupInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtPickupInfo = New DataTable

            daPickupInfo.Fill(dtPickupInfo)
            Return dtPickupInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Schema for Order Info SO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOrderInfoSchema(Optional ByVal PkgFieldAllowed As Boolean = False) As DataTable
        Try
            Dim daDeliveryInfo As New SqlDataAdapter
            Dim dtDeliveryInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append("select Convert(Varchar(50),'') as SrNo,Convert(Varchar(50),'')  as ComboSrNo, " & vbCrLf)
            SqlQuery.Append(" Convert(Varchar(50),'') as ArticleType, " & vbCrLf)
            SqlQuery.Append(" Convert(Varchar(50),'') as Discription,Convert(numeric(18,2),0) AS Quantity, Convert(Varchar(50),'')  AS UnitofMeasure, Convert(Varchar(50),'')  AS PckgMaterial, " & vbCrLf)

            If PkgFieldAllowed = True Then
                SqlQuery.Append("  Convert(numeric(18,3),0) AS PckgSize,Convert(Varchar(50),'')  AS PckgQty," & vbCrLf)
            End If
            SqlQuery.Append(" Convert(DateTime,'')  AS DeliveryDate,Convert(DateTime,'')  AS DeliveryTime, Convert(Varchar(100),'')  AS DeliveryAddress,Convert(numeric(18,2),0) AS PickupQty,Convert(numeric(18,2),0) AS ReturnQty,Convert(DateTime,'') AS LastPickUpDateTime," & vbCrLf)
            SqlQuery.Append("Cast( 0 as Decimal) as PendingQty,Convert(numeric(18,0),0) AS BillLineNo ")


            daDeliveryInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtDeliveryInfo = New DataTable
            daDeliveryInfo.Fill(dtDeliveryInfo)

            Return dtDeliveryInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Payment Information of SO
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>

    Public Function GetPaymentInfo(ByVal SiteCode As String, ByVal SalesNo As String) As DataTable
        Try
            Dim daPayInfo As New SqlDataAdapter
            Dim dtPayInfo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            ''last chane By Ketan Vaidya Date :-13Sep2016
            'SqlQuery.Append(" Select SaleInvNumber as InvoiceNo, SOInvTime as InvoiceDate , " & vbCrLf)
            'SqlQuery.Append(" dbo.FnGetDesc('Terminal',TerminalID,SiteCode) as TerminalID, " & vbCrLf)
            'SqlQuery.Append(" TenderTypeCode as TenderType, " & vbCrLf)
            'SqlQuery.Append(" AmountTendered as InvoiceAmt, UserName" & vbCrLf)
            'SqlQuery.Append(" From SalesInvoice Where status =1 and SiteCode='" & SiteCode & "' And DocumentNumber='" & SalesNo & "' " & vbCrLf)
            SqlQuery.Append(" SELECT * from ( " & vbCrLf)
            SqlQuery.Append(" SELECT  A.DocumentNumber as SalesNo, A.SaleInvNumber as InvoiceNo, A.DocumentType," & vbCrLf)
            SqlQuery.Append("dbo.FnGetDesc('Terminal',A.TerminalID,A.SiteCode) as TerminalID, A.TenderTypeCode as TenderType," & vbCrLf)
            SqlQuery.Append("CASE WHEN  A.TenderTypeCode = 'Credit' THEN  A.AmountTendered -Isnull (B.PaidAmount,0)" & vbCrLf)
            SqlQuery.Append("ELSE A.AmountTendered END AS InvoiceAmt,A.UserName, A.SOInvTime as InvoiceDate FROM	SALESINVOICE A " & vbCrLf)
            '------Changed by Prasad as Discussed With Akshay refbllno and documentno.
            SqlQuery.Append("LEFT JOIN  (SELECT CR.RefBillInvNumber ,CR.RefBillNo ,SUM (CR.AmountTendered ) AS PaidAmount,MAX(CR.CREATEDON ) " & vbCrLf)
            SqlQuery.Append("as CREATEDON ,CR.SiteCode FROM CreditReceipt CR WHERE  CR.TYPECODE ='SO' GROUP BY SiteCode,CR.RefBillNo,CR.RefBillInvNumber ) B ON A.DocumentNumber =B.RefBillNo AND A.SITECODE =B.SITECODE AND B.RefBillInvNumber=A.SaleInvNumber   " & vbCrLf)
            SqlQuery.Append("LEFT JOIN CreditSaleWriteOff WOFF ON  A.SiteCode=WOFF.SITECODE AND A.FINYEAR=WOFF.FINYEAR AND  A.DocumentNumber = WOFF.RefBillNo AND WOFF.RefBillInvNumber=A.SaleInvNumber AND A.TenderTypeCode = 'Credit'  " & vbCrLf)
            SqlQuery.Append(" Where A.status =1 AND  WOFF.AmountTendered IS null and A.SiteCode='" & SiteCode & "' And A.DocumentNumber='" & SalesNo & "' " & vbCrLf)
            SqlQuery.Append(" ) AS A WHERE A.InvoiceAmt<> 0" & vbCrLf)



            SqlQuery.Append("UNION ALL SELECT	CR.RefBillNo , CR.BILLNO AS InvoiceNo ,CR.TypeCode AS DocumentType,dbo.FnGetDesc" & vbCrLf)
            SqlQuery.Append("('Terminal',CR.TerminalID,CR.SiteCode)AS TerminalID ,CR.TenderTypeCode AS TenderType , CR.AmountTendered" & vbCrLf)
            SqlQuery.Append("as InvoiceAmt, AU.UserName AS UserName,CR.CREATEDON as InvoiceDate" & vbCrLf)
            SqlQuery.Append("FROM CreditReceipt CR LEFT JOIN AuthUsers AU On AU.USERID = CR.CREATEDBY  WHERE CR.status =1 and CR.SiteCode='" & SiteCode & "' And CR.RefBillNo='" & SalesNo & "' " & vbCrLf)
            '' Added By ketan Write-off Amount disply in Payment History
            SqlQuery.Append("UNION ALL SELECT SI.DocumentNumber AS SalesNo, WOFF.WriteOffNumber AS InvoiceNo , WOFF.TYPECODE AS DocumentType," & vbCrLf)
            SqlQuery.Append("dbo.FnGetDesc('Terminal',WOFF.TerminalID,WOFF.SiteCode) as TerminalID,'Write-Off' as TenderType, WOFF.AmountTendered AS InvoiceAmt,AU.UserName, WOFF.CREATEDON as InvoiceDate" & vbCrLf)
            SqlQuery.Append("FROM SALESINVOICE SI INNER JOIN CreditSaleWriteOff WOFF ON  SI.SiteCode=WOFF.SITECODE AND SI.FINYEAR=WOFF.FINYEAR AND  SI.DocumentNumber = WOFF.RefBillNo AND SI.TenderTypeCode = 'Credit'" & vbCrLf)
            SqlQuery.Append("AND WOFF.RefBillInvNumber=SI.SaleInvNumber LEFT JOIN AUTHUSERs AU ON AU.UserId =WOFF.UpdatedBy WHERE WOFF.STATUS=1 AND SI.SiteCode='" & SiteCode & "' And SI.DocumentNumber='" & SalesNo & "' " & vbCrLf)


            daPayInfo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtPayInfo = New DataTable
            daPayInfo.Fill(dtPayInfo)

            Return dtPayInfo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Combo Article Details Information of SO
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetComboDetails(ByVal SiteCode As String, ByVal SalesNo As String) As DataTable
        Try
            Dim daCombo As New SqlDataAdapter
            Dim dtCombo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append("  select ComboSrNo,ArticleDescription as Discription,Qty as Quantity,PackagedUOM as UnitofMeasure" & vbCrLf)
            SqlQuery.Append("   from SalesOrderBulkComboDtl where SaleOrderNumber='" & SalesNo & "'" & vbCrLf)
            SqlQuery.Append(" and SiteCode='" & SiteCode & "'" & vbCrLf)
            daCombo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtCombo = New DataTable
            daCombo.Fill(dtCombo)

            Return dtCombo

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Schema of SO History Header
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="SalesNo"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSchemaSOHistoryHeader() As DataTable
        Try
            Dim daSOHistory As New SqlDataAdapter
            Dim dtSOHistory As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0

            SqlQuery.Append("  select CONVERT(Varchar(50),'') as ClientName,CONVERT(Varchar(50),'') as SiteName,'Sales Order History' as HeaderText," & vbCrLf)
            SqlQuery.Append(" CONVERT(Varchar(50),'') as CustomerName,CONVERT(Varchar(50),'') as CompanyName," & vbCrLf)
            SqlQuery.Append(" CONVERT(Varchar(50),'') as Department,CONVERT(Varchar(15),'') as MobileNo,GETDATE() as generatedDate" & vbCrLf)
            daSOHistory = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtSOHistory = New DataTable
            daSOHistory.Fill(dtSOHistory)

            Return dtSOHistory

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get CLP Program ID
    ''' </summary>
    ''' <param name="vSiteCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCLPProgramID(ByVal vSiteCode As String) As String
        Try
            Dim daCLP As New SqlDataAdapter
            Dim dtCLP As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("select ClpProgramId  from CLPProgramSiteMap  where Status=1 and SiteCode ='" & vSiteCode & "'" & vbCrLf)
            daCLP = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtCLP = New DataTable
            daCLP.Fill(dtCLP)
            Return dtCLP.Rows(0)(0)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetFactoryRemarks(ByVal SONumber As String) As DataTable
        Try
            Dim dtFactoryRemark As New DataTable
            vStmtQry.Length = 0
            vStmtQry.Append("SELECT MS.siteCode  AS FactoryCode , MS.SiteShortName as FactoryName ,FACTR.Remark FROM mstsite MS LEFT JOIN SalesOrderSTRFactoryRemark FACTR ON FACTR.FactorySiteCode =Ms.sitecode AND FACTR.SaleOrderNumber= '" & SONumber & "' WHERE MS.BusinessCode ='WH' AND MS.Status =1 " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim da As New SqlDataAdapter(vStmtQry.ToString(), ConString)
            da.Fill(dtFactoryRemark)
            Return dtFactoryRemark
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

#Region "SO Print"

    Dim drSOPrintHeader As DataRow
    Dim dtHeaderDetails As DataTable
    Dim dtOrderDetails As DataTable
    Dim dtOrderComboDetails As DataTable
    Dim dtPaymentDetails1 As DataTable
    Dim dtPaymentDetails As DataTable
    Dim dtDeliveryDetails As DataTable
    Dim dtStrDetails As DataTable
    Dim dtAddress As DataTable
    Dim dtRemark As DataTable
    Dim dtPickupHistory As DataTable
    Dim dtArticleWisePaymentDetails As DataTable


    Public Function SOPrintHeader(ByRef dtSiteInfo As DataTable, ByRef dtCustDetail As DataTable, DocumentNo As String, dsSOInvoice As DataSet, ClientName As String, TerminalID As String, UserName As String) As DataTable
        Try
            Dim deliverydate As String
            dtHeaderDetails = GetSOPrintHeaderTableStruc()
            If dtSiteInfo.Rows.Count > 0 And dtCustDetail.Rows.Count > 0 Then
                dtHeaderDetails.Rows.Clear()
                drSOPrintHeader = dtHeaderDetails.NewRow()
                drSOPrintHeader("CompanyName") = ClientName
                drSOPrintHeader("SiteName") = dtSiteInfo.Rows(0)("SiteOfficialName")
                drSOPrintHeader("Address") = dtSiteInfo.Rows(0)("SiteAddressLn1") + "," + dtSiteInfo.Rows(0)("SiteAddressLn2") + " " + dtSiteInfo.Rows(0)("SiteAddressLn3")
                drSOPrintHeader("City") = dtSiteInfo.Rows(0)("CityCode")
                drSOPrintHeader("PinCode") = dtSiteInfo.Rows(0)("SitePinCode")
                drSOPrintHeader("Contact") = dtSiteInfo.Rows(0)("SiteTelephone1")
                drSOPrintHeader("CustomerName") = dtCustDetail.Rows(0)("CUSTOMERNAME")
                drSOPrintHeader("CustomerCompanyName") = dtCustDetail.Rows(0)("CompanyName")
                drSOPrintHeader("CustomerCellNo") = dtCustDetail.Rows(0)("MOBILENO")
                drSOPrintHeader("CustomerDept") = dtCustDetail.Rows(0)("DepartMent")
                drSOPrintHeader("OtherContacts") = GetClpContactDetails(dtSiteInfo.Rows(0)("SiteCode"), dtCustDetail.Rows(0)("CUSTOMERNO"))
                drSOPrintHeader("EmailId") = dtCustDetail.Rows(0)("Email Address")
                drSOPrintHeader("SalesOrderNo") = DocumentNo
                drSOPrintHeader("OrderDate") = dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("CreatedOn")
                If dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("IsDelivery").ToString Then
                    drSOPrintHeader("DeliveryRequired") = "Yes"
                Else
                    drSOPrintHeader("DeliveryRequired") = "No"
                End If
                For i = 0 To dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count - 1
                    If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("STATUS") = True Then
                        If Convert.ToDateTime(dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime")) = Convert.ToDateTime(dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(i)("DeliveryTime")) Then
                            deliverydate = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows(0)("DeliveryTime").ToString()
                            deliverydate = Convert.ToDateTime(deliverydate).ToString("dd-MM-yyyy HH:mm ")
                        Else
                            deliverydate = "Multiple"
                            Exit For
                        End If
                    End If
                Next
                drSOPrintHeader("DeliveryDate") = deliverydate
                drSOPrintHeader("FooterMassage") = "This is computer generated invoice"
                drSOPrintHeader("GeneratedDate") = DateTime.Now
                drSOPrintHeader("TillNo") = TerminalID
                drSOPrintHeader("BookedBy") = IIf(dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode") Is DBNull.Value, UserName, dsSOInvoice.Tables("SalesOrderHdr").Rows(0)("SalesExecutiveCode"))
                dtHeaderDetails.Rows.Add(drSOPrintHeader)
            End If
            Return dtHeaderDetails
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Public Function SOPrintOrderDetails(dsSOInvoice As DataSet, dsPackagingDelivery As DataSet, PackageFiedlsAllowed As Boolean, dtPackagingPrintBox As DataTable) As DataTable
        Try
            Dim a = dsPackagingDelivery
            dtOrderDetails = GetSOPrintOrderDetailsTableStruc()
            dtOrderDetails.Rows.Clear()
            Dim i As Integer = 1
            If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("pkgLineNo").ToString + "'")
                    Dim drpickupQty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("DeliverylineNo='" + dr("DeliverylineNo").ToString + "'")
                    drSOPrintHeader = dtOrderDetails.NewRow()
                    drSOPrintHeader("BillLineNo") = dr("BillLineNo")
                    Dim drSrNo As DataRow() = dtOrderDetails.Select("BillLineNo='" + dr("BillLineNo").ToString + "'")
                    If drSrNo.Length = 0 Then
                        drSOPrintHeader("SrNo") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("SrNo") = i & "." & drSrNo.Length
                    End If
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("ArticleDescription") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("ArticleDescription") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    End If
                    drSOPrintHeader("Quantity") = dr("Quantity")
                    drSOPrintHeader("UnitofMeasure") = dr("UnitOfMeasure")
                    drSOPrintHeader("Price") = drResult(0)("SellingPrice")
                    drSOPrintHeader("Total") = drResult(0)("SellingPrice") * dr("Quantity")
                    'drSOPrintHeader("PckgSize") = drResult(0)("PckgSize")
                    If PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    End If
                    If drResult(0)("PckgSize") = 0 Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("Quantity") / drResult(0)("PckgSize")
                        If PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                            drSOPrintHeader("PckgQty") = 0
                        End If
                    End If

                    drSOPrintHeader("PckgType") = drResult(0)("PckgType")
                    drSOPrintHeader("PickupQuantity") = dr("DeliveredQty")
                    drSOPrintHeader("IsCombo") = dr("IsCombo")
                    drSOPrintHeader("header") = 1
                    drSOPrintHeader("PackagingMaterial") = drResult(0)("pckgMaterial")
                    dtOrderDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
            Return dtOrderDetails
        Catch ex As Exception

        End Try

    End Function

    'Public Function SOPrintOrderComboDetails(dsSOInvoice As DataSet) As DataTable
    '    Try

    '        dtOrderComboDetails = GetSOPrintOrderDetailsTableStruc()
    '        dtOrderComboDetails.Rows.Clear()
    '        Dim i As Long = 1
    '        If dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
    '            For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderBulkComboDtl").Select("Status=True", "ComboSrNo")
    '                Dim drResult As DataRow() = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
    '                drSOPrintHeader = dtOrderComboDetails.NewRow()
    '                drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
    '                Dim drSrNo As DataRow() = dtOrderComboDetails.Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
    '                If drSrNo.Length = 0 Then
    '                    i = 1
    '                    drSOPrintHeader("SrNo") = Chr(i + 96)
    '                Else
    '                    'i = i - 1
    '                    drSOPrintHeader("SrNo") = Chr(i + 96)
    '                End If
    '                drSOPrintHeader("ArticleDescription") = dr("ArticleDescription")
    '                drSOPrintHeader("Quantity") = dr("QTy")
    '                drSOPrintHeader("UnitofMeasure") = dr("PackagedUOM")
    '                drSOPrintHeader("Price") = 0
    '                drSOPrintHeader("Total") = 0
    '                drSOPrintHeader("PckgSize") = 0
    '                drSOPrintHeader("PckgQty") = 0
    '                drSOPrintHeader("PckgType") = 0
    '                drSOPrintHeader("PickupQuantity") = drResult(0)("DeliveredQty")
    '                drSOPrintHeader("IsCombo") = 1
    '                drSOPrintHeader("header") = 0
    '                dtOrderComboDetails.Rows.Add(drSOPrintHeader)
    '                i = i + 1
    '            Next
    '        End If
    '        dtOrderComboDetails.Merge(dtOrderDetails)
    '        If dtOrderComboDetails.Rows.Count > 0 Then
    '            dtOrderComboDetails = dtOrderComboDetails.Select("", "BillLineNo").CopyToDataTable()
    '        End If
    '        dtOrderComboDetails.AcceptChanges()
    '        Return dtOrderComboDetails
    '    Catch ex As Exception

    '    End Try
    'End Function
    Public Function SOPrintOrderComboDetails(dsSOInvoice As DataSet, Optional ISnewSO As Boolean = False) As DataTable
        Try
            If ISnewSO Then 'vipin PC SO Merge 03-05-2018
                Dim ObjclsCommon As New clsCommon
                dtOrderComboDetails = GetSOPrintOrderDetailsTableStruc()
                dtOrderComboDetails.Rows.Clear()
                Dim i As Long = 1
                If dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
                    Dim DtComboDtlForSoPrint As DataTable = ObjclsCommon.GetComboDtlForSoPrint(dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows(0)("SaleOrderNumber").ToString())
                    For Each dr As DataRow In DtComboDtlForSoPrint.Rows
                        drSOPrintHeader = dtOrderComboDetails.NewRow()
                        drSOPrintHeader("SrNo") = dr("ComboSrNo")
                        drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
                        drSOPrintHeader("ArticleDescription") = dr("Combo1")
                        drSOPrintHeader("PckgType") = dr("Combo2")
                        drSOPrintHeader("PackagingMaterial") = dr("Combo3")
                        drSOPrintHeader("HSN") = dr("Combo4")

                        drSOPrintHeader("Price") = 0
                        drSOPrintHeader("Total") = 0
                        drSOPrintHeader("PckgSize") = 0
                        drSOPrintHeader("PckgQty") = 0
                        drSOPrintHeader("IsCombo") = 1
                        drSOPrintHeader("header") = 0

                        'drSOPrintHeader("HSN") = objComn.GetHSNbyArticle(dr("ArticleCode"))    'vipin 27/07/2017 GST
                        dtOrderComboDetails.Rows.Add(drSOPrintHeader)
                        i = i + 1
                    Next
                End If
                dtOrderComboDetails.Merge(dtOrderDetails)
                If dtOrderComboDetails.Rows.Count > 0 Then
                    dtOrderComboDetails = dtOrderComboDetails.Select("", "BillLineNo").CopyToDataTable()
                End If
                dtOrderComboDetails.AcceptChanges()
                Return dtOrderComboDetails
            Else
                dtOrderComboDetails = GetSOPrintOrderDetailsTableStruc()
                dtOrderComboDetails.Rows.Clear()
                Dim i As Long = 1
                If dsSOInvoice.Tables("SalesOrderBulkComboDtl").Rows.Count > 0 Then
                    For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderBulkComboDtl").Select("Status=True", "ComboSrNo")
                        Dim drResult As DataRow() = dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                        drSOPrintHeader = dtOrderComboDetails.NewRow()
                        drSOPrintHeader("BillLineNo") = dr("ComboSrNo")
                        Dim drSrNo As DataRow() = dtOrderComboDetails.Select("BillLineNo='" + dr("ComboSrNo").ToString + "'")
                        If drSrNo.Length = 0 Then
                            i = 1
                            drSOPrintHeader("SrNo") = Chr(i + 96)
                        Else
                            'i = i - 1
                            drSOPrintHeader("SrNo") = Chr(i + 96)
                        End If
                        drSOPrintHeader("ArticleDescription") = dr("ArticleDescription")
                        drSOPrintHeader("Quantity") = dr("QTy")
                        drSOPrintHeader("UnitofMeasure") = dr("PackagedUOM")
                        drSOPrintHeader("Price") = 0
                        drSOPrintHeader("Total") = 0
                        drSOPrintHeader("PckgSize") = 0
                        drSOPrintHeader("PckgQty") = 0
                        drSOPrintHeader("PckgType") = 0
                        drSOPrintHeader("PickupQuantity") = drResult(0)("DeliveredQty")
                        drSOPrintHeader("IsCombo") = 1
                        drSOPrintHeader("header") = 0
                        dtOrderComboDetails.Rows.Add(drSOPrintHeader)
                        i = i + 1
                    Next
                End If
                dtOrderComboDetails.Merge(dtOrderDetails)
                If dtOrderComboDetails.Rows.Count > 0 Then
                    dtOrderComboDetails = dtOrderComboDetails.Select("", "BillLineNo").CopyToDataTable()
                End If
                dtOrderComboDetails.AcceptChanges()
                Return dtOrderComboDetails
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Function soprintarticlepaymentwisedetails(dsSOInvoice As DataSet, dsPackagingDelivery As DataSet, _dsPackagingVar As DataSet) As DataTable 'by ketan 
        Try
            Dim a = dsPackagingDelivery
            dtArticleWisePaymentDetails = GetSOPrintArticleWisePaymentTableStruc()
            dtArticleWisePaymentDetails.Rows.Clear()
            Dim otherCharges As Double
            If dsSOInvoice.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsSOInvoice.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            Dim i As Integer = 1
            If dsSOInvoice.Tables("sopackagingboxdeliverydtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("sopackagingboxdeliverydtl").Select("status=true", "billlineno")
                    Dim drresult As DataRow() = dsSOInvoice.Tables("soitempackagingboxdtl").Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim drpickupqty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("deliverylineno='" + dr("deliverylineno").ToString + "'")
                    Dim drdisc As DataRow() = dsSOInvoice.Tables("sopackagingdiscdtl").Select("pkglineno='" + dr("pkglineno").ToString + "' and status=true")
                    Dim drtax As DataRow() = dsSOInvoice.Tables("salesordertaxdtls").Select("packagingindex='" + dr("pkglineno").ToString + "' and status=true")
                    Dim DsPackVar As DataRow() = _dsPackagingVar.Tables(0).Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim dttx As DataTable
                    If drtax.Length > 0 Then
                        Dim taxtlabel As String = String.Empty
                        For index = 0 To drtax.Length - 1
                            taxtlabel = taxtlabel & ",'" & drtax(index)("taxlabel").ToString & "'"

                        Next
                        taxtlabel = taxtlabel.Substring(1, taxtlabel.Length - 1)
                        dttx = clsCommon.GetArticleGSTTax(taxtlabel)
                    End If

                    drSOPrintHeader = dtArticleWisePaymentDetails.NewRow()
                    drSOPrintHeader("billlineno") = dr("billlineno")
                    Dim drsrno As DataRow() = dtArticleWisePaymentDetails.Select("billlineno='" + dr("billlineno").ToString + "'")
                    If drsrno.Length = 0 Then
                        drSOPrintHeader("srno") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("srno") = i & "." & drsrno.Length
                    End If
                    Dim discription = dsSOInvoice.Tables("salesorderbulkcombohdr").Select("combosrno='" & dr("billlineno") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("articledescription") = discription(0)("packagingboxname")
                    Else
                        drSOPrintHeader("articledescription") = clsCommon.GetArticleFullName(dr("articlecode")).ToString()
                    End If
                    drSOPrintHeader("quantity") = dr("quantity")
                    drSOPrintHeader("price") = drresult(0)("sellingprice")
                    drSOPrintHeader("netamt") = drresult(0)("sellingprice") * dr("quantity")
                    drSOPrintHeader("othercharges") = otherCharges
                    '       If drdisc.Length > 0 Then
                    If drresult.Length > 0 Then
                        'drsoprintheader("discount") = drdisc(0)("discountamount")
                        'drsoprintheader("discountper") = drdisc(0)("discountper")
                        'drsoprintheader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drdisc(0)("discountamount"))
                        drSOPrintHeader("discount") = drresult(0)("discountamount")  'vipin
                        drSOPrintHeader("discountper") = (drresult(0)("discountamount") * 100) / (drresult(0)("sellingprice") * dr("quantity"))
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drresult(0)("discountamount"))
                    Else
                        drSOPrintHeader("discount") = 0
                        drSOPrintHeader("discountper") = 0
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    End If
                    '' Chnage By ketan issue in SO Print @ Prodution
                    'drSOPrintHeader("cgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2
                    'drSOPrintHeader("sgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2
                    drSOPrintHeader("cgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmount") / 2
                    drSOPrintHeader("sgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmount") / 2

                    drSOPrintHeader("cgst") = (drSOPrintHeader("cgstvalue") / drSOPrintHeader("taxableamount")) * 100
                    drSOPrintHeader("sgst") = (drSOPrintHeader("sgstvalue") / drSOPrintHeader("taxableamount")) * 100


                    drSOPrintHeader("grossamt") = drSOPrintHeader("taxableamount") + drSOPrintHeader("cgstvalue") + drSOPrintHeader("sgstvalue")
                    ' drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    If dr("IsCombo") = True Then  'vipin to hide HSN in case of combo
                        drSOPrintHeader("hsncode") = ""
                        drSOPrintHeader("IsCombo") = "Y"
                    Else
                        drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                        drSOPrintHeader("IsCombo") = "N"
                    End If
                    'drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    dtArticleWisePaymentDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
            Return dtArticleWisePaymentDetails
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function SOPrintPaymentDetails1(dsSOInvoice As DataSet, DocumentNo As String, SiteCode As String, dsInv As DataSet) As DataTable
        Try
            'dsInv()
            Dim TaxAmount As Double
            Dim dsPackagingVar As DataSet = SetSalesOrderPackVariationInSO(SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            If dsPackagingVar.Tables("PackagingMaterial").Rows.Count > 0 Then
                TaxAmount = FormatNumber(CDbl(dsPackagingVar.Tables("PackagingMaterial").Compute("Sum(TotalTaxAmount)", "")), 0) + Math.Round(IIf(dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(TaxValue)", "IsDocumentLevel=1 and status=1") Is DBNull.Value, 0, dsSOInvoice.Tables("SalesOrderTaxDtls").Compute("SUM(taxValue)", "IsDocumentLevel=1 and status=1")), 2)
            End If

            Dim st As Double
            Dim crdsaleadjustamount As Double = 0
            Dim dtCreditSaleData As New DataTable
            Dim objclsReturn As New clsCashMemoReturn
            dtCreditSaleData = objclsReturn.getCreditSaleBillData(DocumentNo)
            If Not dtCreditSaleData Is Nothing AndAlso dtCreditSaleData.Rows.Count > 0 Then
                For Each y In dtCreditSaleData.Rows
                    st += y("CreditSaleAdjustment")
                Next
            End If
            Dim otherCharges As Double
            If dsSOInvoice.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsSOInvoice.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            dtPaymentDetails1 = GetSOPrintPaymenytDetails1TableStruc()
            dtPaymentDetails1.Rows.Clear()
            If dsSOInvoice.Tables("SalesOrderHdr").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderHdr").Select("Status=True")
                    drSOPrintHeader = dtPaymentDetails1.NewRow()
                    drSOPrintHeader("GrossAmt") = FormatNumber(dr("GrossAmt"), 0)
                    ' drSOPrintHeader("GrossAmt") = FormatNumber(drSOPrintHeader("GrossAmt"), 0)
                    drSOPrintHeader("Discount") = dr("TotalDiscount")
                    drSOPrintHeader("Tax") = FormatNumber(TaxAmount, 0)
                    drSOPrintHeader("OtherCharges") = otherCharges
                    drSOPrintHeader("NetAmt") = FormatNumber(dr("NetAmt"), 0)
                    'drSOPrintHeader("paidAmt") = dr("AdvanceAmt")
                    'drSOPrintHeader("BalAmt") = dr("NetAmt") - dr("AdvanceAmt")
                    drSOPrintHeader("paidAmt") = dr("AdvanceAmt") - IIf(dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'") Is DBNull.Value, 0, dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'")) '+ st
                    drSOPrintHeader("BalAmt") = dr("NetAmt") - dr("AdvanceAmt") + IIf(dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'") Is DBNull.Value, 0, dsInv.Tables(0).Compute("Sum(InvoiceAmt)", "Tendertype='Credit'")) '- st
                    dtPaymentDetails1.Rows.Add(drSOPrintHeader)
                Next
            End If
            Return dtPaymentDetails1
        Catch ex As Exception

        End Try

    End Function
    Public Function SOPrintPaymentDetails(dsInv As DataSet, SiteCode As String, DocumentNo As String) As DataTable
        Try
            dtPaymentDetails = GetSOPrintPaymenytDetailsTableStruc()
            dtPaymentDetails.Rows.Clear()

            dsInv = SetInvoiceInSOCancel(SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
            If dsInv.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dsInv.Tables(0).Rows
                    drSOPrintHeader = dtPaymentDetails.NewRow()
                    drSOPrintHeader("Till") = dr("TerminalID")
                    drSOPrintHeader("CashierName") = dr("UserName")
                    drSOPrintHeader("InvoiceNo") = dr("InvoiceNo")
                    drSOPrintHeader("PymentDateAndTime") = dr("InvoiceDate")
                    drSOPrintHeader("Tender") = dr("TenderType")
                    drSOPrintHeader("Amt") = dr("InvoiceAmt")
                    dtPaymentDetails.Rows.Add(drSOPrintHeader)
                Next
            End If
            Return dtPaymentDetails
        Catch ex As Exception

        End Try

    End Function

    Public Function SOPrintDeliveryDetails(dsSOInvoice As DataSet, PackageFiedlsAllowed As Boolean, dtPackagingPrintBox As DataTable, dtOrderAddresses As DataTable) As DataTable
        Try

            dtDeliveryDetails = GetSOPrintDeliveryDetailsTableStruc()
            dtDeliveryDetails.Rows.Clear()
            If dsSOInvoice.Tables("SOItemPackagingBoxDtl").Rows.Count > 0 Then

                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status =True", "BillLineNo")
                    'Dim drResult As DataRow() = dsMain.Tables("SOPackagingBoxDeliveryDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
                    drSOPrintHeader = dtDeliveryDetails.NewRow()
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("Orders") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("Orders") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString
                    End If
                    drSOPrintHeader("Orderqty") = dr("Quantity")
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOItemPackagingBoxDtl").Select("pkglineNo='" + dr("pkglineNo").ToString + "'")
                    'drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    If PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = drResult(0)("Pckgsize")
                    End If
                    If drResult(0)("PckgSize") = 0 Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("Quantity") / drResult(0)("PckgSize")
                        If PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                            drSOPrintHeader("PckgQty") = 0
                        End If
                    End If
                    drSOPrintHeader("PckgType") = drResult(0)("PckgType")
                    If dtOrderAddresses.Rows.Count > 0 Then
                        Dim drAddress As DataRow() = dtOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
                        If drAddress.Length = 0 Then
                            drSOPrintHeader("DeliveryAddress") = "-"
                        Else
                            drSOPrintHeader("DeliveryAddress") = drAddress(0)("AddressValue")
                        End If
                    End If

                    drSOPrintHeader("DeliveryDate") = dr("DeliveryDate")
                    drSOPrintHeader("DeliveryTime") = dr("DeliveryTime")
                    drSOPrintHeader("pickupQty") = dr("DeliveredQty")
                    drSOPrintHeader("PendingQty") = dr("Quantity") - dr("DeliveredQty")
                    drSOPrintHeader("OrderPreparationSite") = drResult(0)("OrderPreparationSite")
                    dtDeliveryDetails.Rows.Add(drSOPrintHeader)
                Next
            End If
            Return dtDeliveryDetails
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function SOStrDetails(dtStrPrint As DataTable) As DataTable
        Try
            dtStrDetails = GetStrDetailsTableStruc()
            dtStrDetails.Rows.Clear()
            If dtStrPrint.Rows.Count > 0 Then
                For Each dr As DataRow In dtStrPrint.Rows
                    drSOPrintHeader = dtStrDetails.NewRow()
                    Dim STRRaised As String = dr("IsSTRRaised")
                    If STRRaised = True Then
                        drSOPrintHeader("STRRaised") = "Yes"
                    Else
                        drSOPrintHeader("STRRaised") = "No"
                    End If
                    drSOPrintHeader("STRRequestedfromSite") = dr("Requested")
                    drSOPrintHeader("STRNo") = dr("STRNo")
                    drSOPrintHeader("STRDeliveryDate") = dr("StrDate")
                    drSOPrintHeader("STRDeliveryTime") = dr("StrTime").ToString().Substring(0, dr("StrTime").ToString().Length - 1)
                    drSOPrintHeader("CREATEDON") = dr("GeneratedOn")
                    drSOPrintHeader("CREATEDBY") = dr("GeneratedBy")
                    dtStrDetails.Rows.Add(drSOPrintHeader)
                Next
            End If
            Return dtStrDetails
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function GetSOPrintAddress(dsSOInvoice As DataSet, dtOrderAddresses As DataTable) As DataTable
        Try
            dtAddress = GetSOPrintAddressTableStruc()
            dtAddress.Rows.Clear()
            If dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsSOInvoice.Tables("SOPackagingBoxDeliveryDtl").Select("Status=True", "BillLineNo")
                    Dim drAddress As DataRow()
                    ' If dtAddress.Rows.Count > 0 Then
                    drAddress = dtOrderAddresses.Select("AddressKey='" & dr("DeliveryAddress") & "'")
                    drSOPrintHeader = dtAddress.NewRow()
                    If drAddress.Length > 0 Then
                        drSOPrintHeader("Address") = drAddress(0)("AddressValue")
                    End If
                    dtAddress.Rows.Add(drSOPrintHeader)
                    ' End If
                Next
            End If
            Return dtAddress
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Public Function SOPrintRemarks(dsSOInvoice As DataSet) As DataTable
        Try
            If dsSOInvoice.Tables("SalesOrderdtl").Rows.Count > 0 Then
                dtRemark = GetSOPrintRemarksTableStruc()
                dtRemark.Rows.Clear()
                For Each dr As DataRow In dsSOInvoice.Tables("SalesOrderdtl").Select("Status=True", "BillLineNo")
                    drSOPrintHeader = dtRemark.NewRow()
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("ArticleShortName") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("ArticleShortName") = clsCommon.GetArticleFullName(dr("ArticleCode")).ToString()
                    End If
                    drSOPrintHeader("Remarks") = dr("Remarks")
                    If dr("Remarks") <> "" Then
                        dtRemark.Rows.Add(drSOPrintHeader)
                    End If
                Next
            End If
            Return dtRemark
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function SOPrintPickupHistory(dsSOInvoice As DataSet, _pickupHistory As DataSet, dtPackagingPrintBox As DataTable, PackageFiedlsAllowed As Boolean) As DataTable
        Try
            dtPickupHistory = GetSOPrintPickupHistoryTableStruc()
            dtPickupHistory.Rows.Clear()
            If Not _pickupHistory Is Nothing AndAlso _pickupHistory.Tables("PickupHistory").Rows.Count > 0 Then
                For Each dr As DataRow In _pickupHistory.Tables("PickupHistory").Rows
                    drSOPrintHeader = dtPickupHistory.NewRow()
                    Dim drResult As DataRow() = dsSOInvoice.Tables("SOitemPackagingBoxDtl").Select("pkgLineNo='" + dr("PkgLineNo").ToString + "'")
                    Dim discription = dsSOInvoice.Tables("SalesOrderBulkComboHdr").Select("ComboSrNo='" & dr("BillLineNo") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("Orders") = discription(0)("PackagingBoxName")
                    Else
                        drSOPrintHeader("Orders") = clsCommon.GetArticleFullName(dr("Orders")).ToString
                    End If
                    drSOPrintHeader("Orderqty") = dr("Orderqty")
                    ' drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                    If PackageFiedlsAllowed = True AndAlso discription.Length > 0 Then
                        Dim resultCombo As DataRow() = dtPackagingPrintBox.Select("KeyValue='" + discription(0)("PackagingBoxName") + "' and isPackagingMandatory=1")
                        If resultCombo.Length > 0 Then
                            drSOPrintHeader("Pckgsize") = 0
                        Else
                            drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                        End If
                    Else
                        drSOPrintHeader("Pckgsize") = dr("Pckgsize")
                    End If
                    If PackageFiedlsAllowed = True AndAlso drResult(0)("UnitOfMeasure") = "NOS" AndAlso drResult(0)("IsCombo") = True Then
                        drSOPrintHeader("PckgQty") = 0
                    Else
                        drSOPrintHeader("PckgQty") = dr("PckgQty")
                    End If
                    drSOPrintHeader("PckgType") = dr("PckgType")
                    drSOPrintHeader("DeliveryDate") = dr("DeliveryDate")
                    drSOPrintHeader("DeliveryTime") = DateTime.Now
                    drSOPrintHeader("pickupQty") = dr("pickupQty")
                    drSOPrintHeader("PendingQty") = dr("PendingQty")
                    dtPickupHistory.Rows.Add(drSOPrintHeader)
                Next
            End If
            Return dtPickupHistory
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#End Region
    Public Function GetPaymentTermName(ByVal TncCode As String) As String
        Try
            Dim daPyt As New SqlDataAdapter
            Dim dtPyt As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("select Description from MstTermsNConditon where TnCcode='" & TncCode & "' and STATUS=1" & vbCrLf)
            daPyt = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtPyt = New DataTable
            daPyt.Fill(dtPyt)
            Return dtPyt.Rows(0)(0)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

#End Region

End Class
