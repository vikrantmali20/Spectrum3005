using System.Collections.Generic;

namespace Spectrum.Models
{
    public class ArticleStockInModel
    {
        public InvoiceModel InvoiceModel { get; set; }
        public IList<ArticleStockBalanceModel> ArticleStockBalanceModels { get; set; }
        public OrderHdrModel OrderHdrModel { get; set; }
        public IList<OrderDtlModel> OrderDtlModels { get; set; }
    }

    public class ArticleStockOutModel
    {
        public StockOutReason StockOutReason { get; set; }
        public StockFromLocation StockFromLocation { get; set; }
        public string Reason { get; set; }
        public IList<OrderDtlModel> OrderDtlModels { get; set; }
    }
}
