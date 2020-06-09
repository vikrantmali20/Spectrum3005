using System;
using System.ComponentModel;

namespace Spectrum.Models
{
    public class SupplierModel : BaseModel
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string CityCode { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string TelexNo { get; set; }
        public string Pincode { get; set; }
        public string LocalSalesTaxNo { get; set; }
        public string CentralSalesTaxNo { get; set; }
        public Nullable<System.DateTime> LocalSalesTaxDate { get; set; }
        public Nullable<System.DateTime> CentralSalesTaxDate { get; set; }
        public Nullable<decimal> MinMargin { get; set; }
        public string PaymentMethod { get; set; }
        public string ShipmentMethod { get; set; }
        public string EmailId { get; set; }
        public string ContactPerson { get; set; }
        public string CurrencyCode { get; set; }
        public string LanguageCode { get; set; }
        public int DeliveryDays { get; set; }
        public Nullable<int> PaymentDays { get; set; }
        public string TINNo { get; set; }
        public string PANNo { get; set; }
        public string ServiceTaxRegNo { get; set; }
        public string VendorPostingGroup { get; set; }
        public string GenBusPostingGroup { get; set; }
        public Nullable<bool> isActive { get; set; }
        public bool IsAutoNumber { get; set; }
    }

    public class Supplier
    {
        //[DisplayName("Select")]
        //public bool Select { get; set; }

        [DisplayName("Supplier Code")]
        public string Code { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("E-mail Address")]
        public string EmailID { get; set; }

        [DisplayName("Is User Active?")]
        public bool IsActive { get; set; }

        //[DisplayName("Filter")]
        //public string Filter { get; set; }

    }

}
