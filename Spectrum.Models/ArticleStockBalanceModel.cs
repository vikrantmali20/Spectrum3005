using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
    public class ArticleStockBalanceModel : BaseModel
    {
        public string SiteCode { get; set; }
        public string ArticleCode { get; set; }
        public string EAN { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> PhysicalQty { get; set; }
        public Nullable<decimal> ReservedQty { get; set; }
        public Nullable<decimal> DamagedQty { get; set; }
        public Nullable<decimal> OnOrderQty { get; set; }
        public Nullable<decimal> InTrasnsitQty { get; set; }
        public Nullable<decimal> NonSaleableQty { get; set; }
        public Nullable<decimal> TotalPhysicalSaleableQty { get; set; }
        public Nullable<decimal> TotalPhysicalNonSaleableQty { get; set; }
        public Nullable<decimal> TotalVirtualNonSaleableQty { get; set; }
        public Nullable<decimal> TotalSaleableQty { get; set; }
        public Nullable<decimal> TotalARSQty { get; set; }
        public string StockStatus { get; set; }
        public Nullable<System.DateTime> NextAvailableDate { get; set; }
    }
}
