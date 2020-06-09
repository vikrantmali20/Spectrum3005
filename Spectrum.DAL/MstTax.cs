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
    
    public partial class MstTax
    {
        public MstTax()
        {
            this.TaxSiteMapping = new HashSet<TaxSiteMapping>();
            this.TaxSiteDocMap = new HashSet<TaxSiteDocMap>();
        }
    
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public string TaxSeqProfile { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Inclusive { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string CREATEDAT { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string UPDATEDAT { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDON { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string TaxType { get; set; }
        public bool InterStateTax { get; set; }
    
        public virtual ICollection<TaxSiteMapping> TaxSiteMapping { get; set; }
        public virtual ICollection<TaxSiteDocMap> TaxSiteDocMap { get; set; }
    }
}
