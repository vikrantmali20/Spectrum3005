Imports System
Imports System.Collections.Generic

Partial Public Class CashMemoDtl
    Inherits BasePrintModel

    Public Property SiteCode As String
    Public Property FinYear As String
    Public Property BillNo As String
    Public Property BillLineNo As String
    Public Property EAN As String
    Public Property ArticleCode As String
    Public Property SalesStaffID As String
    Public Property SellingPrice As String
    Public Property NetSellingPrice As String
    Public Property CostPrice As String
    Public Property GrossAmt As String
    Public Property NetAmount As String
    Public Property Quantity As String
    Public Property TransactionStatus As String
    Public Property BillDate As String
    Public Property BillTime As String
    Public Property PromotionId As String
    Public Property Section As String
    Public Property Shelf As String
    Public Property TransactionCode As String
    Public Property ScaleItem As String
    Public Property ReturnNoSale As String
    Public Property SerialNbr As String
    Public Property LineDiscount As String
    Public Property PostingStatus As String
    Public Property CLPRequire As String
    Public Property CLPPoints As String
    Public Property CLPDiscount As String
    Public Property UnitofMeasure As String
    Public Property TotalDiscount As String
    Public Property TotalDiscPercentage As String
    Public Property ExclusiveTax As String
    Public Property TotalTaxAmount As String
    Public Property AuthUserId As String
    Public Property AuthUserRemarks As String
    Public Property IFBNo As String
    Public Property Btype As String
    Public Property ReturnDocumentType As String
    Public Property SalesReturnReason As String
    Public Property Returncmno As String
    Public Property Returncmdate As String
    Public Property ReturnQty As String
    Public Property Quarter As String
    Public Property Month As String
    Public Property Day As String
    Public Property FIRSTLEVELDISC As String
    Public Property TOPLEVELDISC As String
    Public Property FIRSTLEVEL As String
    Public Property TOPLEVEL As String
    Public Property BatchBarcode As String

    Public Property ArticleName As String
    Public Property IsComboArticle As String
    Public Property mrp As String

    Public Overridable Property CashMemoComboDetails As IList(Of CashMemoComboDtl)

End Class
