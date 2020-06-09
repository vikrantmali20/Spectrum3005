namespace Spectrum.Models
{
    using System;
    using System.ComponentModel;
    public class TaxModel : BaseModel
    {
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public string TaxSeqProfile { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Inclusive { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string TaxType { get; set; }
        public Nullable<bool> InterStateTax { get; set; }
       
    }
    public class TaxModelList
    {
        [DisplayName("")]
        public bool Select { get; set; }

        [DisplayName("Tax Code")]
        public string TaxCode { get; set; }

        [DisplayName("Tax Name")]
        public string TaxName { get; set; }

        [DisplayName("Tax Value")]
        public Nullable<decimal> Value { get; set; }

        [DisplayName("Tax Value Type")]
        public string TaxType { get; set; }

        
    }
    public class TaxSiteMappingModel:BaseModel 
    {
        public string Taxcode { get; set; }
        public string Sitecode { get; set; }
        public Nullable<bool> Defaultsite { get; set; }
       
    }

    public class TaxSiteDocMapModel : BaseModel
    {

        public string Appliedon { get; set; }
        public string DocumentType { get; set; }
        public bool? Inclusive { get; set; }
        public bool? IsDocumentLevelTax { get; set; }
        public bool? IsPercentageValue { get; set; }
        public decimal? SEQUENCE { get; set; }
        public string SiteCode { get; set; }
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public string TaxType { get; set; }
        public decimal? TaxValue { get; set; }

    }
}
