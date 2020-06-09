using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Logging;
using Spectrum.Models;

namespace Spectrum.BL
{
    public class ArticleStockBalancesManager : GenericManager<ArticleStockBalanceModel>, IArticleStockBalancesManager
    {
        public ArticleStockBalancesManager()
        {
            this.articleStockBalanceRepository = new ArticleStockBalancesRepository();

            Mapper.CreateMap<ArticleStockBalanceModel, ArticleStockBalances>();
            Mapper.CreateMap<ArticleStockBalances, ArticleStockBalanceModel>();

            Mapper.CreateMap<InvoiceModel, Invoice>();
            Mapper.CreateMap<Invoice, InvoiceModel>();

            Mapper.CreateMap<OrderDtlModel, OrderDtl>();
            Mapper.CreateMap<OrderDtl, OrderDtlModel>();

            Mapper.CreateMap<OrderHdrModel, OrderHdr>();
            Mapper.CreateMap<OrderHdr, OrderHdrModel>();
        }
        private IArticleStockBalancesRepository articleStockBalanceRepository;

        public bool SaveArticleStockInData(ArticleStockInModel articleStockInModel)
        {
            try
            {
                var invoice = new Invoice();

                var orderHdr = new OrderHdr();
                orderHdr.OrderDtl = new List<OrderDtl>();

                var articleStockBalances = new List<ArticleStockBalances>();

                //Get article codes for updating article stock
                var articleCodes = (from a in articleStockInModel.ArticleStockBalanceModels
                                    select a.ArticleCode).ToList();

                var articleStockBalanceList = this.articleStockBalanceRepository.GetArticleStockBalanceByArticleCodes(articleCodes);

                Mapper.Map(articleStockInModel.InvoiceModel, invoice);
                Mapper.Map(articleStockInModel.OrderHdrModel, orderHdr);

                for (int rowIndex = 0; rowIndex < articleStockInModel.OrderDtlModels.Count; rowIndex++)
                    orderHdr.OrderDtl.Add(Mapper.Map(articleStockInModel.OrderDtlModels[rowIndex], new OrderDtl()));

                for (int rowIndex = 0; rowIndex < articleStockInModel.ArticleStockBalanceModels.Count; rowIndex++)
                {
                    var articleStockBalance = articleStockBalanceList.Where(a => a.ArticleCode == articleStockInModel.ArticleStockBalanceModels[rowIndex].ArticleCode && a.SiteCode==CommonModel.SiteCode ).FirstOrDefault();
                    if (articleStockBalance != null)
                    {
                        articleStockBalance.PhysicalQty += articleStockInModel.ArticleStockBalanceModels[rowIndex].PhysicalQty;
                        articleStockBalance.TotalPhysicalSaleableQty += articleStockInModel.ArticleStockBalanceModels[rowIndex].TotalPhysicalSaleableQty;
                        articleStockBalance.TotalSaleableQty += articleStockInModel.ArticleStockBalanceModels[rowIndex].TotalSaleableQty;
                        articleStockBalance.UPDATEDAT = CommonModel.SiteCode;
                        articleStockBalance.UPDATEDBY = CommonModel.UserID;
                        articleStockBalance.UPDATEDON = DateTime.Now;
                    }
                }

                return this.articleStockBalanceRepository.SaveArticleStockInData(invoice, orderHdr, articleStockBalanceList.ToList());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        public bool SaveArticleStockOutData(ArticleStockOutModel articleStockOutModel)
        {
            try
            {
                var orderHdr = new OrderHdr();
                orderHdr.OrderDtl = new List<OrderDtl>();

                //Get article codes for updating article stock
                var articleCodes = (from a in articleStockOutModel.OrderDtlModels
                                    select a.ArticleCode).ToList();

                var articleStockBalanceList = this.articleStockBalanceRepository.GetArticleStockBalanceByArticleCodes(articleCodes);

                for (int rowIndex = 0; rowIndex < articleStockOutModel.OrderDtlModels.Count(); rowIndex++)
                {
                    var articleStockBalance = articleStockBalanceList.Where(a => a.ArticleCode == articleStockOutModel.OrderDtlModels[rowIndex].ArticleCode).FirstOrDefault();
                    if (articleStockBalance != null)
                    {
                        decimal quantity = articleStockOutModel.OrderDtlModels[rowIndex].AdjustmentQty;

                        if (articleStockOutModel.StockOutReason == StockOutReason.ReturnToSupplier || articleStockOutModel.StockOutReason == StockOutReason.WriteOff)
                        {
                            articleStockBalance.PhysicalQty -= quantity;

                            switch (articleStockOutModel.StockFromLocation)
                            {
                                case StockFromLocation.Damaged:
                                    articleStockBalance.TotalPhysicalNonSaleableQty -= quantity;
                                    articleStockBalance.DamagedQty -= quantity;
                                    break;
                                case StockFromLocation.NonSaleable:
                                    articleStockBalance.TotalPhysicalNonSaleableQty -= quantity;
                                    articleStockBalance.NonSaleableQty -= quantity;
                                    break;
                                case StockFromLocation.Saleable:
                                    articleStockBalance.TotalPhysicalSaleableQty -= quantity;
                                    articleStockBalance.TotalSaleableQty -= quantity;
                                    break;
                            }
                        }
                        else
                        {
                            articleStockBalance.TotalPhysicalNonSaleableQty += quantity;
                            articleStockBalance.TotalPhysicalSaleableQty -= quantity;
                            articleStockBalance.TotalSaleableQty -= quantity;

                            if (articleStockOutModel.StockOutReason == StockOutReason.Damaged)
                                articleStockBalance.DamagedQty += quantity;
                            else if (articleStockOutModel.StockOutReason == StockOutReason.NonSaleable)
                                articleStockBalance.NonSaleableQty -= quantity;
                        }
                        articleStockBalance.UPDATEDON = DateTime.Now;
                        articleStockBalance.UPDATEDBY = CommonModel.UserID;
                        articleStockBalance.UPDATEDAT = CommonModel.SiteCode;
                    }
                }

                var maxTranNo = this.articleStockBalanceRepository.GetMaxTranNoInStockAdjustment();
                maxTranNo = maxTranNo + 1;
                var stockAdjustmentList = new List<StockAdjustment>();

                for (int i = 0; i < articleStockOutModel.OrderDtlModels.Count; i++)
                {
                    stockAdjustmentList.Add(new StockAdjustment
                    {
                        SiteCode = CommonModel.SiteCode,
                        TrnNo = maxTranNo,
                        FinYear = CommonModel.CurrentDate.ToString("yyyy"),
                        TrnDate = CommonModel.CurrentDate,
                        ArticleCode = articleStockOutModel.OrderDtlModels[i].ArticleCode,
                        EAN = articleStockOutModel.OrderDtlModels[i].EAN,
                        UOM = articleStockOutModel.OrderDtlModels[i].UnitofMeasure,
                        Qty = articleStockOutModel.OrderDtlModels[i].AdjustmentQty,
                        Reason = (!string.IsNullOrEmpty(articleStockOutModel.OrderDtlModels[i].Reason)) ? articleStockOutModel.OrderDtlModels[i].Reason : articleStockOutModel.Reason,
                        CREATEDAT = CommonModel.SiteCode,
                        CREATEDBY = CommonModel.UserID,
                        CREATEDON = CommonModel.CurrentDate,
                        UPDATEDAT = CommonModel.SiteCode,
                        UPDATEDBY = CommonModel.UserID,
                        UPDATEDON = CommonModel.CurrentDate,
                        STATUS = true
                    });

                    maxTranNo += 1;
                }

                return this.articleStockBalanceRepository.UpdateArticleStockOutData(articleStockBalanceList.ToList(), stockAdjustmentList);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        public InvoiceModel CheckInvoiceID(string InvoiceNumberID)
        {
            var invoiceList = this.articleStockBalanceRepository.CheckInvoiceID(InvoiceNumberID);
            var invoiceModel = Mapper.Map(invoiceList, new InvoiceModel());
            return invoiceModel;
        }
         
    }
}
