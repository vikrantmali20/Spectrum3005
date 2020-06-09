using System.ComponentModel;
using System.Collections.Generic;

namespace Spectrum.Models
{
    public class ArticleDataExportModel
    {
        public ArticleDataExportModel()
        {
            this.ArticleDetails = new List<ArticleDetails>();
            this.TaxDetails = new List<TaxDetails>();
            this.CharDetails = new List<CharDetails>();
            this.SalesDetails = new List<SalesDetails>();
            this.PurchaseDetails = new List<PurchaseDetails>();
        }
        public IList<ArticleDetails> ArticleDetails { get; set; }
        public IList<TaxDetails> TaxDetails { get; set; }
        public IList<CharDetails> CharDetails { get; set; }
        public IList<SalesDetails> SalesDetails { get; set; }
        public IList<PurchaseDetails> PurchaseDetails { get; set; }
    }

    public class ArticleDetails
    {
        [Description("Article Code")]
        public string ArticleCode { get; set; }
        [Description("Article Name")]
        public string ArticleName { get; set; }
        [Description("Article Short Name")]
        public string ArticleShortName { get; set; }

        [Description("DESCRIPTION")]                          // added bu vipin
        public string Description { get; set; }


        [Description("Article Type")]
        public string ArticleType { get; set; }
        [Description("Material Type")]
        public string MaterialType { get; set; }
        [Description("Master Article")]
        public string MasterArticle { get; set; }
        [Description("PARENT ARTICLE NODE")]
      //  public string ParentArtilceNode { get; set; }
        public string ParentArticleNode { get; set; }
        [Description("LAST NODE")]
        public string LastNode { get; set; }
        [Description("Article Hierarchy")]
        public string ArticleHierarchy { get; set; }
        [Description("Barcode")]
       // public string EAN { get; set; }
        public string Barcode { get; set; }
        [Description("Barcode Type")]
       // public string DISCRIPTION { get; set; }
        public string BarcodeType { get; set; }
        [Description("Barcode Type")]
        public string DefaultBarCode { get; set; }
        [Description("StoreID")]
        public string StoreID { get; set; }
        [Description("SupplierCode")]
        public string SupplierCode { get; set; }
        [Description("Default Supplier")]
        public string DefaultSupplier { get; set; }
        [Description("COST PRICE")]
        public decimal? CostPrice { get; set; }
        [Description("SELLING PRICE")]
        public decimal? SellingPrice { get; set; }
        [Description("MRP")]
        public decimal? MRP { get; set; }
        [Description("Language Code")]
        public string LanguageCode { get; set; }
        [Description("Image")]
        public string Image { get; set; }
        [Description("Base UoM")]
        public string BaseUoM { get; set; }
        [Description("Order UoM")]
        public string OrderUoM { get; set; }
        [Description("OUOM value")]
        public decimal? OUOMvalue { get; set; }
        [Description("Sale UOM")]
        public string SaleUOM { get; set; }
        [Description("Sale Value")]
        public decimal? SaleValue { get; set; }
        [Description("Distribution UOM")]
        public string DistributionUOM { get; set; }
        [Description("Distribution Value")]
        public decimal DistributionValue { get; set; }
        [Description("Expiry")]
        public string Expiry { get; set; }
        [Description("Saleable")]
        public string Saleable { get; set; }
        [Description("Status")]
        public string Status { get; set; }
        [Description("Printable")]
        public string Printable { get; set; }
        [Description("MAP")]
        public decimal? MAP { get; set; }
        [Description("Net Weight")]
        public decimal? NetWeight { get; set; }
        [Description("Net Weight UOM")]
        public string NetWeightUOM { get; set; }
        [Description("Gross Weight")]
        public decimal? GrossWeight { get; set; }
        [Description("Gross Weight UOM")]
        public string GrossWeightUOM { get; set; }
        [Description("Open Qty")]
        public string OpenQty { get; set; }
        [Description("Open Selling Price")]
        public string OpenSellingPrice { get; set; }
        [Description("Reorder Qty")]
        public decimal? ReorderQty { get; set; }
        [Description("Safety Qty")]
        public decimal? SafetyQty { get; set; }

        [Description("SUPPLIER STATUS")]                          // added bu vipin
        public string SupplierStatus { get; set; }
        [Description("LEGACY CODE")]                          // added bu vipin
        public string LegacyCode { get; set; }
    }


    public class TaxDetails
    {
        public string ArticleCode { get; set; }
        public string ArticleName { get; set; }
        public string StoreID { get; set; }
        public string TaxName { get; set; }
        public string TaxCode { get; set; }
       // public string Tax { get; set; }
        public string Status { get; set; }
        public string SupplierCode { get; set; }
    }

    public class CharDetails
    {
        public string ArticleCode { get; set; }
        public string ArticleName { get; set; }
        public string Barcode { get; set; }
        public string Profile { get; set; }
        public decimal CharID { get; set; }
        public string CharStatus { get; set; }

    }

    public class SalesDetails
    {
        public string StoreID { get; set; }
        public string ArticleCode { get; set; }
        public string Barcode { get; set; }
        public decimal SellingPrice { get; set; }
        public string Status { get; set; }
    }

    public class PurchaseDetails
    {
        public string ArticleCode { get; set; }
        public string ArticleName { get; set; }
        public string OrderUOM { get; set; }
        public decimal OrderValue { get; set; }
        public string Status { get; set; }

    }
}
