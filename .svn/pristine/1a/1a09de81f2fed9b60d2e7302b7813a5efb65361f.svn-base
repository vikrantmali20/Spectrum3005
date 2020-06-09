using System.ComponentModel;
namespace Spectrum.Models
{
    public class TenderModel : BaseModel
    {
        public string TenderHeadCode { get; set; }
        public string SiteCode { get; set; }
        public string TenderHeadName { get; set; }
        public string TenderType { get; set; }
        public string Positive_Negative { get; set; }
        public decimal? MaxNo { get; set; }
        public decimal? MaxValue { get; set; }
        public decimal? MinBillValue { get; set; }
        public string DefaultValue { get; set; }
        public string ProgramId { get; set; }

        public TenderTypeModel TenderTypeModel { get; set; }
    }
    public class TenderModelList
    {
        [DisplayName("")]
        public bool Select { get; set; }

        
        [DisplayName("Tender Code")]
        public string TenderHeadCode { get; set; }

        [DisplayName("Tender Name")]
        public string TenderHeadName { get; set; }

        [DisplayName("Tender Type")]
        public string TenderType { get; set; }

        [DisplayName("Program Code")]
        public string ProgramId { get; set; }

        [DisplayName("Return Issue")]
        public string ReturnIssue { get; set; }

        [DisplayName("Refund Issue")]
        public string RefundIssue { get; set; }


    }
}
