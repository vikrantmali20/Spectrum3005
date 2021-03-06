//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spectrum.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockAdjustment
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
        public string CREATEDAT { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string UPDATEDAT { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDON { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public string BaseUOM { get; set; }
        public Nullable<decimal> BaseQty { get; set; }
        public Nullable<decimal> OldQty { get; set; }
        public string Reason { get; set; }
    
        public virtual MstEAN MstEAN { get; set; }
        public virtual MstSite MstSite { get; set; }
        public virtual MstArticle MstArticle { get; set; }
    }
}
