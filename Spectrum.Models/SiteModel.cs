using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
    public class SiteDALModel
    {
        public SiteModel SiteModel { get; set; }
        public MstFinYearModel MstFinYearModel { get; set; }
        public MstCurrencySiteModel MstCurrencySiteModel { get; set; }
        public MstAreaCodeModel MstAreaCodeModel { get; set; }
        public MstSiteCurrancyMapModel MstSiteCurrancyMapModel { get; set; }
    }

  public class SiteModel:BaseModel
    {
        public string SiteCode { get; set; }
        public string BusinessCode { get; set; }
        public string SiteShortName { get; set; }
        public string SiteOfficialName { get; set; }
        public string SiteAddressLn1 { get; set; }
        public string SiteAddressLn2 { get; set; }
        public string SiteAddressLn3 { get; set; }
        public string SitePinCode { get; set; }
        public string CityCode { get; set; }
        public string StateCode { get; set; }
        public string SiteStdCode { get; set; }
        public string SiteTelephone1 { get; set; }
        public string SiteTelephone2 { get; set; }
        public string FaxNo { get; set; }
        public string EmailId { get; set; }
        public string WebPageURL { get; set; }
        public string ContactPerson { get; set; }
        public string CountryCode { get; set; }
        public string LanguageCode { get; set; }
        public string LocalSalesTaxNo { get; set; }
        public string CentralSalesTaxNo { get; set; }
        public Nullable<System.DateTime> LocalSalesTaxDate { get; set; }
        public Nullable<System.DateTime> CentralSalesTaxDate { get; set; }
        public Nullable<decimal> SQFTArea { get; set; }
        public Nullable<bool> IsARSApplicable { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsVATApplicable { get; set; }
        public Nullable<bool> IsSalesTaxApplicable { get; set; }
        public string LocalCurrancyCode { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsMAPApplicable { get; set; }
        
        public string defaultean { get; set; }
        public Nullable<bool> IsWarehouseApplicable { get; set; }
        public string WarehouseCode { get; set; }
        public Nullable<bool> SisUploadFlag { get; set; }
        public Nullable<bool> IsBinApplicable { get; set; }
        public Nullable<bool> isBatchApplicable { get; set; }

    }

  public class SiteModelEditList  
  {
      [DisplayName("Site Code")]
      public string SiteCode { get; set; }
      [DisplayName("Site Name")]
      public string SiteName { get; set; }
      [DisplayName("Contact Person")]
      public string ContactPerson { get; set; }
      [DisplayName("Email Address")]
      public string EmailId { get; set; }
      
  }

  public class MstFinYearModel : BaseModel
  {
      public string SiteCode { get; set; }
      public string FinYear { get; set; }
      public System.DateTime ValidFromDt { get; set; }
      public System.DateTime ValidToDt { get; set; }
      public bool FinStatus { get; set; }
       

  }

  public class MstCurrencySiteModel : BaseModel
  {
      public string CurrencyCode { get; set; }
      public string CurrencyDescription { get; set; }
      public string CurrencySymbol { get; set; }
      public Nullable<bool> EMUCurrency { get; set; }
      public Nullable<decimal> RealisedGain { get; set; }
      public Nullable<decimal> RealisedLoss { get; set; }
      public Nullable<decimal> UNRealisedGain { get; set; }
      public Nullable<decimal> UNRealisedLoss { get; set; }


  }

  public class MstAreaCodeModel : BaseModel
  {
      public string AreaCode { get; set; }
      public string Description { get; set; }
      public decimal AreaType { get; set; }
      public string ParentCode { get; set; }


  }
  public class MstSiteCurrancyMapModel : BaseModel
  {
      public string SiteCode { get; set; }
      public string LocalCurrancyCode { get; set; }


  }
}
