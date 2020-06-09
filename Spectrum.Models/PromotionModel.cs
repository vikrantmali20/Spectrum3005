using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
   public class PromotionModel
    {
       public ManualPromotionModel ManualPromotionModel { get; set; }
       public PromotionSiteMapModel PromotionSiteMapModel { get; set; }
    }
   public class ManualPromotionModel:BaseModel 
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
        
   }
   public class ManualPromotionModelEdit
   {
       //[DisplayName("")]
       //public bool Select { get; set; }
       [DisplayName("Promotion Id")]
       public string PromotionId { get; set; }
       [DisplayName("Promotion Name")] 
       public string PromotionName { get; set; }
       [DisplayName("Promotion Value")]
       public Nullable<decimal> PromotionValue { get; set; }

   }
   public class PromotionSiteMapModel : BaseModel 
   {
       public string OfferNo { get; set; }
       public string SiteCode { get; set; }      
       public Nullable<bool> Monday { get; set; }
       public Nullable<bool> Tuesday { get; set; }
       public Nullable<bool> Wednesday { get; set; }
       public Nullable<bool> Thursday { get; set; }
       public Nullable<bool> Friday { get; set; }
       public Nullable<bool> Saturday { get; set; }
       public Nullable<bool> Sunday { get; set; }
   }

}
