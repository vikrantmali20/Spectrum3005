using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System.Data.Common;
using System.Data.Entity.Validation;

namespace Spectrum.DAL
{
    public class ArticleStockBalancesRepository : GenericRepository<ArticleStockBalances>, IArticleStockBalancesRepository
    {
        public ArticleStockBalances GetArticleStockBalanceByID(string articleStockBalanceID)
        {
            try
            {
                return Context.ArticleStockBalances.Where(u => u.ArticleCode == articleStockBalanceID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Invoice  CheckInvoiceID(string InvoiceNumberID)
        {
            try
            {
                return Context.Invoice.Where(u => u.InvoiceNumber == InvoiceNumberID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ArticleStockBalances> GetArticleStockBalanceByArticleCodes(IList<string> articleCodes)
        {
            var articleStockBalances = Context.ArticleStockBalances.Where(a => articleCodes.Contains(a.ArticleCode) && a.SiteCode==CommonModel.SiteCode ).ToList();

            return articleStockBalances.AsQueryable();
        }

        public bool SaveArticleStockInData(Invoice invoice, OrderHdr orderHdr, IList<ArticleStockBalances> articleStockBalances)
        {
            using (var context = ContextFactory.CreateContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {

                    try
                    {
                        if( invoice.InvoiceNumber!="")
                        Context.Invoice.Add(invoice);
                        Context.OrderHdr.Add(orderHdr);

                        for (int rowIndex = 0; rowIndex < articleStockBalances.Count; rowIndex++)
                            Context.Entry<ArticleStockBalances>(articleStockBalances[rowIndex]).State = EntityState.Modified;

                        //var rangeObjects = context.GLNoRangeObjects.Where(g => g.SiteCode == invoice.SiteCode && g.ObjectID == "GR").FirstOrDefault();
                        //rangeObjects.CurrentNo += 1;
                        //Context.Entry<GLNoRangeObjects>(rangeObjects).State = EntityState.Modified;
                        this.UpdateNextID(CommonModel.SiteCode, "GR");    
                        Context.SaveChanges();
                        dbTran.Commit();
                        return true;
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        dbTran.Rollback();
                        foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                        {
                            foreach (DbValidationError error in entityErr.ValidationErrors)
                            {
                                Logging.Logger.Log("Error Property Name " + error.PropertyName + " : Error Message: " + error.ErrorMessage, Logging.Logger.LogingLevel.Error);
                                Logging.Logger.Log(dbEx, Logging.Logger.LogingLevel.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        context.Dispose();
                    }
                    return false;
                }
            }

        }
        public void UpdateNextID(string siteCode, string objectID)
        {
            try
            {
                var glObject = Context.GLNoRangeObjects.Where(g => g.SiteCode == siteCode && g.ObjectID == objectID).FirstOrDefault();
                if (glObject != null) //vipin
                {
                    glObject.CurrentNo += 1;
                    glObject.UPDATEDON = DateTime.Now;
                    Context.Entry<GLNoRangeObjects>(glObject).State = System.Data.Entity.EntityState.Modified;
                }
              //  Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateArticleStockOutData(IList<ArticleStockBalances> articleStockBalances, IList<StockAdjustment> stockAdjustmentList)
        {
            using (var context = ContextFactory.CreateContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int rowIndex = 0; rowIndex < articleStockBalances.Count; rowIndex++)
                            Context.Entry<ArticleStockBalances>(articleStockBalances[rowIndex]).State = EntityState.Modified;

                        for (int rowIndex = 0; rowIndex < stockAdjustmentList.Count; rowIndex++)
                            Context.StockAdjustment.Add(stockAdjustmentList[rowIndex]);

                        Context.SaveChanges();

                        dbTran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        context.Dispose();
                    }
                }
            }

        }

        public decimal GetMaxTranNoInStockAdjustment()
        {
            var a = Context.StockAdjustment.ToList();             
            if (a == null || a.Count==0)
                return 0;
            else
                return Context.StockAdjustment.Max(s => s.TrnNo);
        }
    }
}
