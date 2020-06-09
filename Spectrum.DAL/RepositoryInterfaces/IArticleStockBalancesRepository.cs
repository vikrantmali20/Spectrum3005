using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IArticleStockBalancesRepository
    {
        ArticleStockBalances GetArticleStockBalanceByID(string articleStockBalanceID);

        IQueryable<ArticleStockBalances> GetArticleStockBalanceByArticleCodes(IList<string> articleCodes);
        
        bool SaveArticleStockInData(Invoice invoice, OrderHdr orderHdr, IList<ArticleStockBalances> articleStockBalances);

        bool UpdateArticleStockOutData(IList<ArticleStockBalances> articleStockBalances, IList<StockAdjustment> stockAdjustmentList);

        decimal GetMaxTranNoInStockAdjustment();

        Invoice CheckInvoiceID(string InvoiceNumberID);
    }
}
