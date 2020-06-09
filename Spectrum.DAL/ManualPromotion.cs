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
    
    public partial class ManualPromotion
    {
        public string PromotionId { get; set; }
        public string PromotionName { get; set; }
        public Nullable<decimal> PromotionValue { get; set; }
        public Nullable<bool> DiscPer { get; set; }
        public Nullable<bool> FixedPriceOff { get; set; }
        public Nullable<bool> FixedSelling { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> OfferActive { get; set; }
        public string CREATEDAT { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string UPDATEDAT { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDON { get; set; }
        public Nullable<bool> STATUS { get; set; }
    }
}