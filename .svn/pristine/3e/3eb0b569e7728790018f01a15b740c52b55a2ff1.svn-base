using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    public interface IArticleStockBalancesManager : IGenericManager<ArticleStockBalanceModel>
    {
        bool SaveArticleStockInData(ArticleStockInModel articleStockInModel);

        bool SaveArticleStockOutData(ArticleStockOutModel articleStockOutModel);

        InvoiceModel CheckInvoiceID(string InvoiceNumberID);
    }
}
