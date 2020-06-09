using System;
namespace Spectrum.Models
{
    public class CommonModel
    {
        public static string ConnectionString { get; set; }

        public static string SiteCode { get; set; }
        public static string UserID { get; set; }
        public static DateTime CurrentDate { get; set; }
    }

    public class MasterTypeModel
    {
        public string CodeType { get; set; }
        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool? Status { get; set; }
        public int IsSysParameter { get; set; }

    }

    public class ItemHierarchy
    {
        public string Nodecode { get; set; }
        public string NodeName { get; set; }
        public string ParentNodecode { get; set; }
        public string ParentNodeName { get; set; }
        public string TreeCode { get; set; }
        public string TreeName { get; set; }
        public bool? ISThisLastNode { get; set; }
        public string LevelCode { get; set; }
        public string LevelName { get; set; }
        public int NodeType { get; set; }
    }


    public class ArticleTypeModel
    {
        public string ArticalTypeCode { get; set; }
        public string ArticalTypeName { get; set; }
    }

    public class UoMModel
    {
        public string UOMCode { get; set; }
        public string UOM { get; set; }
    }

    public class ManufacturerModel
    {
        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
     }
    public class MstDocumentTypeModel
    {
        public string DocumentType { get; set; }
        public string DocumentTypeDescription { get; set; }
    }

    public class GeneralCodeMstModel
    {
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
    //public class stockoutreasons
    //{
    //    public string reasonname { get; set; }
    //    public string reasonvalue { get; set; }
    //}
    //public class fromlocation
    //{
    //    public string fromloctext { get; set; }
    //    public string fromlocvalue { get; set; }
    //}

    public class BrandModel
    {
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string ManufacturerCode { get; set; }
    }
    public class MstTenderTypeModel
    {
        public string TenderType { get; set; }
        public string Description { get; set; }
        
    }
    public class MstCLPProgramModel
    {
        public string CLPProgramId { get; set; }
        public string CLPProgramName { get; set; }

    }
    public class MstVoucherModel
    {
        public string VoucherCode { get; set; }
        public string VoucherDesc { get; set; }

    }
    public class MstRoleModel
    {
        public string RoleID { get; set; }
        public string Description { get; set; }

    }
    public class MstSiteModel
    {
        public string SiteCode { get; set; }
        public string SiteShortName { get; set; }

    }
    public class MstBusinessModel
    {
        public string BusinessCode { get; set; }
        public string BusinessName { get; set; }

    }
    public class MstLanguageModel
    {
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

    }
    public class MstEANModel
    {
        public string EAN { get; set; }
        public string Discription { get; set; }

    }
    public class MstSupplierModel
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }

    }
    public class MstCurrencyModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }

    }
    public class CharacteristicsModel
    {
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string CharCode { get; set; }
        public string CharName { get; set; }
        public string CharValue { get; set; }
    }

    public class InvoiceModel : BaseModel
    {
        public string InvoiceCode { get; set; }
        public string SiteCode { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceType { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string PayTo { get; set; }
        public string SupplierCode { get; set; }
        public string PONumber { get; set; }
        public string GRNNumber { get; set; }
        public Nullable<decimal> InvoiceDiscount { get; set; }
        public Nullable<bool> PhyInvReceived { get; set; }
    }
    public class OrderDtlModel:BaseModel
    {
        public string SiteCode { get; set; }
        public string FinYear { get; set; }
        public string DocumentNumber { get; set; }
        public string ArticleCode { get; set; }
        public string EAN { get; set; }
        public decimal LineNumber { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string UnitofMeasure { get; set; }
        public Nullable<decimal> OpenQty { get; set; }
        public Nullable<decimal> DeliveredQty { get; set; }
        public Nullable<bool> DeliveryCompleted { get; set; }
        public Nullable<decimal> PurchaseGroupCode { get; set; }
        public string RefDocument { get; set; }
        public string RefDocumentNo { get; set; }
        public string PONo { get; set; }
        public string PoSiteCode { get; set; }
        public Nullable<decimal> PoLineNbr { get; set; }
        public string BirthListId { get; set; }
        public string SaleOrderNumber { get; set; }
        public string AllocationRule { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> NetCostPrice { get; set; }
        public Nullable<decimal> ExciseDutyAmt { get; set; }
        public Nullable<decimal> ExciseDutyRate { get; set; }
        public Nullable<decimal> PurchaseTaxAmt { get; set; }
        public Nullable<decimal> PurchaseTaxRate { get; set; }
        public Nullable<decimal> AdditionalChargeAmt { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> AdditionalChargeRate { get; set; }
        public Nullable<decimal> DocValue { get; set; }
        public Nullable<decimal> DiscrepancyQty { get; set; }
        public Nullable<System.DateTime> dayOpenDate { get; set; }
        public string RequestedArticle { get; set; }
        public Nullable<decimal> RequestedQty { get; set; }
        public string BaseUom { get; set; }
        public Nullable<decimal> BaseUomQty { get; set; }
        public Nullable<decimal> LocalCostPrice { get; set; }
        public Nullable<decimal> LocalDiscount { get; set; }
        public Nullable<decimal> LocalTaxAmount { get; set; }
        public Nullable<decimal> LocalNetCostPrice { get; set; }
        public Nullable<decimal> ItemTaxAmt { get; set; }
        public Nullable<decimal> LandingCostPrice { get; set; }
        public string BarCode { get; set; }
        public decimal AdjustmentQty { get; set; }
        public string Reason { get; set; }
    }
    public class OrderHdrModel:BaseModel 
    {
        public string SiteCode { get; set; }
        public string FinYear { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public System.DateTime DocDate { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<decimal> PurchaseGroupCode { get; set; }
        public string DeliverySiteCode { get; set; }
        public System.DateTime ExpectedDeliveryDt { get; set; }
        public Nullable<decimal> PaymentAfterDelDays { get; set; }
        public string AdditionalTermsNConditions { get; set; }
        public string AdditionalPaymentTerms { get; set; }
        public Nullable<decimal> DocNetValue { get; set; }
        public Nullable<decimal> DocGrossValue { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<bool> IsFreezed { get; set; }
        public string ReturnReasonCode { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedLevel { get; set; }
        public string ApprovalLevel { get; set; }
        public string NextApprovalAt { get; set; }
        public Nullable<decimal> AmmendmentNo { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string Transporter { get; set; }
        public string DocApprovalID { get; set; }
        public string ParentOrderNo { get; set; }
        public string WayBillNo { get; set; }
        public Nullable<System.DateTime> WayBillDate { get; set; }
        public string VehicleDtls { get; set; }
        public string SalesOrderRef { get; set; }
        public string YourRef { get; set; }
        public string StrategyCode { get; set; }
        public string NextApproverId { get; set; }
        public Nullable<decimal> ProcessInstanceID { get; set; }
        public Nullable<decimal> TaskInstanceID { get; set; }
        public Nullable<decimal> ProcessInstanceIDCCE { get; set; }
        public Nullable<decimal> TaskInstanceIDCCE { get; set; }
        public string StrategyStep { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Quarter { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<decimal> WarehouseCharges { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public Nullable<decimal> PakingCharges { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<decimal> LocalNetValue { get; set; }
        public string driverDtls { get; set; }
        public string pickpackNumber { get; set; }
        public string ClosingRemarks { get; set; }
        public string DeliveryChallanNumber { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<System.DateTime> ValidityDate { get; set; }
       
    }
    public class ItemExclude
    {
        public string ArticleCode { get; set; }
    }
}
