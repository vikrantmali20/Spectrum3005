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
    
    public partial class ArticleStockBalances
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
        public string CREATEDAT { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string UPDATEDAT { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDON { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string StockStatus { get; set; }
        public Nullable<System.DateTime> NextAvailableDate { get; set; }
    
        public virtual MstUoM MstUoM { get; set; }
    }
}