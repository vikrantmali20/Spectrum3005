using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Spectrum.Models
{
    public class ArticleModel : BaseModel
    {
        public string ArticleCode { get; set; }
        public string ArticalTypeCode { get; set; }
        public string ArticalCatCode { get; set; }
        public string ArticleShortName { get; set; }
        public string ArticleName { get; set; }
        public string Descriptions { get; set; }       //vipin 
        public string ArticleSupplierCode { get; set; } //vipin 
        public string StoreID { get; set; } //vipin 
        public string BaseUnitofMeasure { get; set; }
        public string DistributionUnitofMeasure { get; set; }
        public string SaleUnitofMeasure { get; set; }
        public string OrderUnitofMeasure { get; set; }
        public Nullable<System.DateTime> CataloguedOn { get; set; }
        public bool ArticleActive { get; set; }
        public Nullable<bool> IssueFreeGift { get; set; }
        public Nullable<bool> ProductImage { get; set; }
        public Nullable<decimal> TotalShelfLife { get; set; }
        public Nullable<decimal> RemainingShelfLife { get; set; }
        public Nullable<bool> DecimalQtyApplicable { get; set; }
        public Nullable<bool> IsMrpOpen { get; set; }
        public Nullable<decimal> SerialNumber { get; set; }
        public Nullable<decimal> WarrantyPeriod { get; set; }
        public string ParentArt { get; set; }
        public string TreeID { get; set; }
        public string LastNodeCode { get; set; }
        public Nullable<decimal> CharProfileCode { get; set; }
        public string SupplierRef { get; set; }
        public string STLocCode { get; set; }
        public Nullable<bool> Salable { get; set; }
        public Nullable<bool> ToleranceValue { get; set; }
        public string LegacyArticleCode { get; set; }
        public string Purchaser { get; set; }
        public string IntraStatCodeEurope { get; set; }
        public string IntraStatCodeUSA { get; set; }
        public Nullable<decimal> NetWeight { get; set; }
        public string NetWeightUOM { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string GrossWeightUOM { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public string VolumeUOM { get; set; }
        public string Style { get; set; }
        public string Season { get; set; }
        public string Theme { get; set; }
        public Nullable<bool> IsPremaman { get; set; }
        public Nullable<bool> NONRETUNABLE { get; set; }
        public string MaterialTypeCode { get; set; }
        public string ConsumptionUoM { get; set; }
        public Nullable<bool> isExpiry { get; set; }
        public Nullable<bool> Printable { get; set; }
        public bool IsEanAutoGenerate { get; set; }
        public bool IsBatchBarcodeAutoGenerate { get; set; }
        public Nullable<decimal> SalesUomValue { get; set; }
        public string ManufacturerCode { get; set; }
        public string BrandCode { get; set; }
        public Nullable<decimal> DistributionUomValue { get; set; }
        //public Nullable<bool> OpenMrp { get; set; }
        //public Nullable<bool> OpenQty { get; set; }
    
        //---- Custom  Fields ......
        public bool IsAutoNumber { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> Margin { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public string ParentArticleName { get; set; }
        public string ArticleImage { get; set; } 

    }
    public class FailedArticleModel  
    {
        public string ArticleCode { get; set; }
        public string Quantity { get; set; }
        
    }
    public class ArticleImageModel : BaseModel
    {
        public string ArticleCode { get; set; }
        public string ArticleImage { get; set; }
        public string GroupID { get; set; }

        // Custom Fields 
        public string sourcePath { get; set; }
        public string targetPath { get; set; }

    }

    public class EANModel : BaseModel
    {
        public string ArticleCode { get; set; }
        public string EAN { get; set; }
        public string Discription { get; set; }
        public string UOMCode { get; set; }
        public string UOMTypeCode { get; set; }
        public Nullable<bool> DefaultEAN { get; set; }
        // Custom 
        public bool IsAutoNumber { get; set; }
    }

    public class ArticlePurchaseModel
    {
        [DisplayName("")]
        public bool Select { get; set; }

        [DisplayName("Item Code")]
        public string ArticleCode { get; set; }

        [DisplayName("Item Discription")]
        public string ArticleName { get; set; }

        [DisplayName("EAN")]
        public string EAN { get; set; }

        [DisplayName("UOM")]
        public string BaseUnitofMeasure { get; set; }

        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Cost Price")]
        public decimal? Cost { get; set; }

        [DisplayName("Tax")]
        public int Tax { get; set; }

      
        [DisplayName("Net Amt")]
        public decimal NetAmount { get; set; }

        [DisplayName("FILTER")]
        public string FILTER { get; set; }

        //public string Discription { get; set; }
        //public string UOMCode { get; set; }
        //public string UOMTypeCode { get; set; }
        //public Nullable<bool> DefaultEAN { get; set; }
        //  public string SupplierCode { get; set; }
        //public bool IsAutoNumber { get; set; }
    }

    public class ArticlePurchaseStockoutModel
    {
         

        [DisplayName("")]
        public bool Select { get; set; }

        [DisplayName("Item Code")]
        public string ArticleCode { get; set; }

        [DisplayName("Item Description")]
        public string ArticleName { get; set; }


        [DisplayName("EAN")]
        public string EAN { get; set; }

        
        

        [DisplayName("UOM")]
        public string BaseUnitofMeasure { get; set; }
        

        [DisplayName("Available Qty")]
        public decimal? AvailableQty { get; set; }

        [DisplayName("Adjustment Qty")]
        public decimal AdjustmentQty { get; set; }

        [DisplayName("Reason")]
        public string Reason { get; set; }

        //[DisplayName("Physical Qty")]
        //public decimal? PhysicalQty { get; set; }

        //[DisplayName("Damaged Qty")]
        //public decimal? DamagedQty { get; set; }

        //[DisplayName("Non Saleable Qty")]
        //public decimal? NonSaleableQty { get; set; }

        //[DisplayName("Total Physical Saleable Qty")]
        //public decimal? TotalPhysicalSaleableQty { get; set; }

        //[DisplayName("Total Physical Non Saleable Qty")]
        //public decimal? TotalPhysicalNonSaleableQty { get; set; }

        //[DisplayName("Total Saleable Qty")]
        //public decimal? TotalSaleableQty { get; set; }

        [DisplayName("FILTER")]
        public string FILTER { get; set; }
    }
 
    public class PurchaseInfoRecordModel
    {
        public string SiteCode { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<bool> IsDefaultSupplier { get; set; }
        public string ArticleCode { get; set; }
        public string EAN { get; set; }
        public Nullable<decimal> CPBaseCurr { get; set; }
        public Nullable<decimal> CPLocalCurr { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> isARSAllowed { get; set; }
        public Nullable<bool> FreezeSB { get; set; }
        public Nullable<bool> FreezeSR { get; set; }
        public Nullable<bool> FreezeIN { get; set; }
        public Nullable<bool> FreezeOB { get; set; }
        public Nullable<decimal> SRNO { get; set; }
        public Nullable<decimal> MAP { get; set; }
    }

    public class SalesInfoRecordModel : BaseModel
    {
        public string SiteCode { get; set; }
        public string ArticleCode { get; set; }
        public int SrNo { get; set; }
        public string EAN { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> FreezeSB { get; set; }
        public Nullable<bool> FreezeSR { get; set; }
        public Nullable<bool> FreezeIN { get; set; }
        public Nullable<bool> FreezeOB { get; set; }
        public Nullable<decimal> SuggestedSalesPrice { get; set; }
    }

    public class SiteArticleTaxMappingModel : BaseModel
    {
        public string SiteCode { get; set; }
        public string ArticleCode { get; set; }
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public string SupplierCode { get; set; }
        // Custom field  Added
        public Nullable<decimal> TaxValue { get; set; }
        public Nullable<Boolean> Inclusive { get; set; }
    }

    public class ArticleCharacteristicMatrixModel : BaseModel
    {
        public string EanCode { get; set; }
        public string CharCode { get; set; }
        public string CharValue { get; set; }
        public Nullable<bool> CharType { get; set; }
        public string ProfileCode { get; set; }
        public Nullable<decimal> RowKey { get; set; }
    }

    public class ArticleTreeModel  : BaseModel
    {
        public string TreeCode { get; set; }
        public string TreeName { get; set; }
    }

    public class ArticleTreeLevelModel : BaseModel
    {
        public string TreeCode { get; set; }
        public string LevelCode { get; set; }
        public string LevelName { get; set; }

    }

    public class ArticleNodeModel : BaseModel
    {
        public string Nodecode { get; set; }
        public string NodeName { get; set; }
        public string Treecode { get; set; }
        public bool ISThisLastNode { get; set; }
        public string LevelCode { get; set; }
    }

    public class ArticleTreeNodeMapModel : BaseModel
    {
        public string Nodecode { get; set; }
        public string ParentNodecode { get; set; }
        public string Treecode { get; set; }
        public Nullable<decimal> ToleranceValue { get; set; } 
    }

    public class ArticleReplenishmentModel : BaseModel
    {
        public string SiteCode { get; set; }
        public string EAN { get; set; }
        public string ArticleCode { get; set; }
        public string ReplenishmentTypeCode { get; set; }
        public Nullable<decimal> StockOnOrder { get; set; }
        public Nullable<decimal> SafetyStockQty { get; set; }
        public Nullable<decimal> ReOrderPoint { get; set; }
        public Nullable<decimal> AverageSaleDuration { get; set; }
        public Nullable<decimal> StockCover { get; set; }
        public Nullable<decimal> EstimatedSale { get; set; }
        public string Month { get; set; }
        public string RequestingSiteCode { get; set; }
    }

    public class MasterArticleMapModel :BaseModel
    {
        public string ArticleCode { get; set; }      // All Added by vipin
        public string MasterArticleCode { get; set; }
        public string TaxName { get; set; }
        public bool Status { get; set; }
    }

    public class ArticleDescInDiffLangModel : BaseModel
    {
        public string ArticleCode { get; set; }
        public string LanguageCode { get; set; }
        public string ArticleShortName { get; set; }
        public string ArticleName { get; set; }
    }

    public class ArticleSearchModel
    {
        [DisplayName("Article Code")]
        public string ArticleCode { get; set; }

        [DisplayName("Name")]
        public string ArticleName { get; set; }

        [DisplayName("Unit")]
        public string BaseUnitofMeasure { get; set; }

        [DisplayName("Status")]
        public Nullable<bool> STATUS { get; set; }
    }

    public enum StockOutReason
    {
        None,
        WriteOff,
        Damaged,
        NonSaleable,
        ReturnToSupplier
    }

    public enum StockFromLocation
    {
        None,
        Damaged,
        Saleable,
        NonSaleable
    }
    
}
