using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
    public class StockAdjustmentModel : BaseModel
    {
        public string SiteCode { get; set; }
        public decimal TrnNo { get; set; }
        public string FinYear { get; set; }
        public System.DateTime TrnDate { get; set; }
        public string ArticleCode { get; set; }
        public string EAN { get; set; }
        public string UOM { get; set; }
        public string FromLOc { get; set; }
        public string ToLoc { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public string BaseUOM { get; set; }
        public Nullable<decimal> BaseQty { get; set; }
        public Nullable<decimal> OldQty { get; set; }
        public string Reason { get; set; }
    }
}
