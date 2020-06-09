using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Spectrum.DAL.CustomEntities;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System.Data.Entity.Core.Objects;
using EntityState = System.Data.Entity.EntityState;
using C1.C1Excel;
using System.Text.RegularExpressions;
using System.Text;
using System.Data.Entity.Validation;


namespace Spectrum.DAL
{

    public class ArticleRepository : GenericRepository<MstArticle>, IArticleRepository
    {
        /// <summary>
        ///  add article model to database ..if Autonumber is generated then update next no to GLNoRangeObjects for this obectid and site
        /// </summary>
        /// <param name="article"></param>
        /// <param name="siteCode"> for updating auto Number </param>
        /// <param name="isAutoNumber"> Whether code is manual or auto </param>
        /// <returns></returns>

        //public bool SaveArticle(MstArticle article, string siteCode, bool isAutoNumber,string fileName , string sourcePath , string targetPath )
        //public bool SaveArticle(MstArticle article, ArticleImageModel articleImage, List<MstEAN> mstEANlist, List<ArticleMatrix> articleMatrixlist,
        //ArticleNodeMap articleNodeMap ,ArticleUOM articleUOM ,SalesInfoRecord salesInfoRecord  ,PurchaseInfoRecord purchaseInfoRecord  , List<DropDownModel> rightSupplierList, string siteCode, bool isAutoNumber )

        const int xlscolumnRow = 0;
        public string defaultProfileName { get; set; }
        string ErrorMsg;
        //public bool SaveArticle(ArticleDALModel articleDALModel)
        //{
        //    bool tranResult = false;
        //    using (var dbContextTransaction = Context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // MstArticle
        //            Context.MstArticle.Add(articleDALModel.MstArticle);
        //            //MstArticleImage ... [Included in article]
        //            //Context.MstArticleImage.Add(articleDALModel.MstArticleImage);
        //            copyFile(articleDALModel.MstArticleImage.ArticleImage, articleDALModel.SourcePath,
        //                articleDALModel.TargetPath);
        //            // EAN [Included in article]
        //            //------- Context.MstEAN.AddRange(articleDALModel.MstEANList);

        //            // SalesInfoRecord
        //            Context.SalesInfoRecord.AddRange(articleDALModel.SalesInfoRecordList);
        //            ////  PurchaseInfoRecor
        //            Context.PurchaseInfoRecord.AddRange(articleDALModel.PurchaseInfoRecordList);

        //            // // ArticleMatrix
        //            Context.ArticleMatrix.AddRange(articleDALModel.ArticleMatrixList);

        //            // //ArticleUOM
        //            Context.ArticleUOM.Add(articleDALModel.ArticleUOM);

        //            // // ArticleStockBalances 
        //            if (articleDALModel.ArticleStockBalances != null)
        //            {
        //                Context.ArticleStockBalances.AddRange(articleDALModel.ArticleStockBalances);
        //            }

        //            //SiteArticleTaxMapping. [Included in article]
        //            // Context.SiteArticleTaxMapping.AddRange(articleDALModel.SiteArticleTaxMappinglist);

        //            //ArticleNodeMap. [Included in article]
        //            //Context.ArticleNodeMap.Add(articleDALModel.ArticleNodeMap);

        //            Context.SaveChanges();

        //            if (articleDALModel.IsAutoNumber)
        //                this.UpdateNextID(articleDALModel.SiteCode, "AC");

        //            for (int EANRow = 0; EANRow < articleDALModel.EANAutoCount; EANRow++)
        //            {
        //                this.UpdateNextID(articleDALModel.SiteCode, "EC");
        //            }

        //            dbContextTransaction.Commit();

        //            tranResult = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            dbContextTransaction.Rollback();
        //            Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
        //            throw ex;
        //        }
        //    }
        //    return tranResult;
        //}
        string ErrorMsgThrow;
        public bool SaveArticle(ArticleDALModel articleDALModel)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    /// decide Insert or Update 
                    string mode = "INSERT";


                    if (!string.IsNullOrEmpty(articleDALModel.MstArticle.ArticleCode))
                    {
                        var temp = GetArticleByID(articleDALModel.MstArticle.ArticleCode);
                        if (temp != null) mode = "UPDATE";
                    }

                    if (mode == "INSERT")
                    {
                        Context.MstArticle.Add(articleDALModel.MstArticle) ;
                        //MstArticleImage ... [Included in article]
                        //Context.MstArticleImage.Add(articleDALModel.MstArticleImage);
                        if (articleDALModel.MstArticleImage!=null)
                        {
                            if (articleDALModel.MstArticleImage.ArticleImage != null)
                            {
                                copyFile(articleDALModel.MstArticleImage.ArticleImage, articleDALModel.SourcePath, articleDALModel.TargetPath);
                            }    
                        }
                        
                        // EAN [Included in article]
                        //------- Context.MstEAN.AddRange(articleDALModel.MstEANList);

                        // SalesInfoRecord
                        if (articleDALModel.SalesInfoRecordList.Count > 0)
                        {
                            Context.SalesInfoRecord.AddRange(articleDALModel.SalesInfoRecordList);
                        }
                        ////  PurchaseInfoRecor
                        if (articleDALModel.PurchaseInfoRecordList.Count > 0)
                        {
                            Context.PurchaseInfoRecord.AddRange(articleDALModel.PurchaseInfoRecordList);
                        }

                        // // ArticleMatrix
                        if (articleDALModel.ArticleMatrixList.Count > 0)
                        {
                            Context.ArticleMatrix.AddRange(articleDALModel.ArticleMatrixList);
                        }

                        // // ArticleStockBalances 
                        if (articleDALModel.ArticleStockBalances != null)
                        {
                            Context.ArticleStockBalances.AddRange(articleDALModel.ArticleStockBalances);
                        }

                        //-- ArticleUOM
                        Context.ArticleUOM.Add(articleDALModel.ArticleUOM);

                        //SiteArticleTaxMapping. [Included in article]
                        // Context.SiteArticleTaxMapping.AddRange(articleDALModel.SiteArticleTaxMappinglist);
                        //ArticleReplenishment 
                        if (articleDALModel.ArticleReplenishment != null)
                        {
                            if ((articleDALModel.ArticleReplenishment.SafetyStockQty != null && articleDALModel.ArticleReplenishment.SafetyStockQty > 0) ||
                                       (articleDALModel.ArticleReplenishment.ReOrderPoint != null && articleDALModel.ArticleReplenishment.ReOrderPoint > 0))
                            {
                                Context.ArticleReplenishment.Add(articleDALModel.ArticleReplenishment);
                            }
                        }

                        //ArticleNodeMap. [Included in article]
                        //Context.ArticleNodeMap.Add(articleDALModel.ArticleNodeMap);

                        if (articleDALModel.IsAutoNumber)
                            this.UpdateNextID(articleDALModel.SiteCode, "AC");

                        for (int EANRow = 0; EANRow < articleDALModel.EANAutoCount; EANRow++)
                        {
                            this.UpdateNextID(articleDALModel.SiteCode, "EC");
                        }
                    }
                    else
                    {
                        Update(articleDALModel.MstArticle, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode);
                        Context.SaveChanges();  // vipin

                        Update(articleDALModel.ArticleDescInDiffLang, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode);
                        Context.SaveChanges();  // vipin


                        if (articleDALModel.MstArticleImage!=null)
                        {
                            if (articleDALModel.MstArticleImage.ArticleImage!=null)
                            {
                                copyFile(articleDALModel.MstArticleImage.ArticleImage, articleDALModel.SourcePath,articleDALModel.TargetPath);        
                            }
                        }
                        

                        // EAN
                        if (articleDALModel.MstArticle.MstEAN.Count > 0)
                        {
                            for (int eanRecordCounter = 0; eanRecordCounter < articleDALModel.MstArticle.MstEAN.Count ; eanRecordCounter++)
                            {
                                MstEAN ean = articleDALModel.MstArticle.MstEAN.ElementAtOrDefault<MstEAN>(eanRecordCounter) ;
                                var test = Context.MstEAN.Where(P => P.EAN == ean.EAN).FirstOrDefault();
                                if (test != null)
                                {
                                    Update(ean, key => key.ArticleCode == ean.ArticleCode && key.EAN == ean.EAN);
                                    Context.SaveChanges();  // vipin
                                }
                                else
                                {
                                    Context.MstEAN.Add(ean);
                                    Context.SaveChanges();  // vipin
                                }
                             }
                        }

                         // Site Tax Mapping
                        //  PK_SiteArticleTaxMapping =SiteCode, ArticleCode, TaxCode, SupplierCode
                        
                        if (articleDALModel.MstArticle.SiteArticleTaxMapping.Count > 0)
                        {
                            for (int taxRecordCounter = 0; taxRecordCounter < articleDALModel.MstArticle.SiteArticleTaxMapping.Count; taxRecordCounter++)
                            {
                                SiteArticleTaxMapping tax = articleDALModel.MstArticle.SiteArticleTaxMapping.ElementAtOrDefault<SiteArticleTaxMapping>(taxRecordCounter);
                                var test = Context.SiteArticleTaxMapping.Where(P => P.SupplierCode == tax.SupplierCode &&  P.SiteCode == tax.SiteCode &&
                                                                                    P.ArticleCode == tax.ArticleCode && P.TaxCode == tax.TaxCode ).FirstOrDefault();
                                if (test != null)
                                {
                                    Update(tax, key => key.SupplierCode == tax.SupplierCode && key.SiteCode == tax.SiteCode &&
                                                                                    key.ArticleCode == tax.ArticleCode && key.TaxCode == tax.TaxCode);
                                    Context.SaveChanges();  // vipin
                                }
                                else
                                {
                                    Context.SiteArticleTaxMapping.Add(tax);
                                    Context.SaveChanges();  // vipin
                                }
                            }
                        }

                        // SalesInfoRecord
                        if (articleDALModel.SalesInfoRecordList.Count > 0)
                        {
                            for (int salesrecordCounter = 0; salesrecordCounter < articleDALModel.SalesInfoRecordList.Count; salesrecordCounter++)
                            {
                                SalesInfoRecord sir = articleDALModel.SalesInfoRecordList[salesrecordCounter];
                                var test = Context.SalesInfoRecord.Where(P => P.SiteCode == sir.SiteCode && P.SrNo == sir.SrNo && P.EAN == sir.EAN).FirstOrDefault();
                                if (test != null)
                                {
                                    Update(sir, key => key.SiteCode == sir.SiteCode && key.SrNo == sir.SrNo && key.EAN == sir.EAN);
                                    Context.SaveChanges();  // vipin
                                }
                                else
                                {
                                    Context.SalesInfoRecord.Add(sir);
                                    Context.SaveChanges();  // vipin
                                }
                                //Context.Entry<SalesInfoRecord>(sir).State = EntityState.Modified;
                            }
                        }

                        ////  PurchaseInfoRecord
                        if (articleDALModel.PurchaseInfoRecordList.Count > 0)
                        {
                            for (int PurchaserecordCounter = 0; PurchaserecordCounter < articleDALModel.PurchaseInfoRecordList.Count; PurchaserecordCounter++)
                            {
                                PurchaseInfoRecord pir = articleDALModel.PurchaseInfoRecordList[PurchaserecordCounter];
                                var test = Context.PurchaseInfoRecord.Where(P => P.SiteCode == pir.SiteCode && P.EAN == pir.EAN && P.SupplierCode == pir.SupplierCode).FirstOrDefault();
                                if (test != null)
                                {
                                    Update(pir, key => key.SiteCode == pir.SiteCode && key.EAN == pir.EAN && key.SupplierCode == pir.SupplierCode);
                                    Context.SaveChanges();  // vipin
                                }
                                else
                                {
                                    Context.PurchaseInfoRecord.Add(pir);
                                    Context.SaveChanges();  // vipin
                                }
                            }
                        }

                        // ArticleMatrix
                        if (articleDALModel.ArticleMatrixList.Count > 0)
                        {
                            //  Context.ArticleMatrix.AddRange(articleDALModel.ArticleMatrixList);
                            for (int ArticleMatrixCounter = 0; ArticleMatrixCounter < articleDALModel.ArticleMatrixList.Count - 1; ArticleMatrixCounter++)
                            {
                                //PurchaseInfoRecord tempPurchaseInfoRecord = Context.ArticleMatrix.Where(x => x.ArticleCode == articleCode && x.EAN == eanCode && x.SiteCode == siteCode).FirstOrDefault();
                                ArticleMatrix artMatrix = articleDALModel.ArticleMatrixList[ArticleMatrixCounter];
                                Context.Entry<ArticleMatrix>(artMatrix).State = EntityState.Modified;
                                Context.SaveChanges();  // vipin
                            }
                        }
                        // //ArticleUOM
                       // Update(articleDALModel.ArticleUOM, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode);
                       // Update(articleDALModel.ArticleUOM, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode && key.UOMCode == articleDALModel.ArticleUOM.UOMCode);

                       // PurchaseInfoRecord Uor = articleDALModel.PurchaseInfoRecordList[PurchaserecordCounter]; //vipin on 11-04-2017
                        var ArtUOM = Context.ArticleUOM.Where(P => P.ArticleCode == articleDALModel.MstArticle.ArticleCode && P.UOMCode == articleDALModel.ArticleUOM.UOMCode).FirstOrDefault();
                        if (ArtUOM != null)
                        {
                            Update(ArtUOM, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode && key.UOMCode == articleDALModel.ArticleUOM.UOMCode);
                            Context.SaveChanges();  // vipin
                        }
                        else
                        {
                            Context.ArticleUOM.Add(articleDALModel.ArticleUOM);
                            Context.SaveChanges();  // vipin
                        }

                    }

                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
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
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                //   throw ex;
                }
            }
            return tranResult;
        }

        public bool SaveImportExcel(ref List<ArticleDALModel> articleExcelDALModel)
        {
            bool tranResult = false;
            string CheckArticle ="";
            int ArticleCount = 0;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                  

                    for (int lstCntr = 0; lstCntr < articleExcelDALModel.Count; lstCntr++)
                    {
                        ArticleDALModel articleDALModel = new ArticleDALModel();
 
                        articleDALModel = articleExcelDALModel[lstCntr];

                        ///decide Insert or Update 
                        string mode = "INSERT";

                      if (!string.IsNullOrEmpty(articleDALModel.MstArticle.ArticleCode))
                        {
                            var temp = GetArticleByID(articleDALModel.MstArticle.ArticleCode);
                            if (temp != null) mode = "UPDATE";
                        }

                        if (mode == "INSERT")
                        {
                            MstArticle ObjMstArticle1;     // added by vipin 19-04-2017
                            ObjMstArticle1 = Context.MstArticle.Where(s => s.ArticleCode == articleDALModel.MstArticle.ArticleCode).FirstOrDefault();


                            articleDALModel.MstArticle.ArticalCatCode = "1";
                            articleDALModel.MstArticle.IsMrpOpen = false;
                            articleDALModel.MstArticle.ToleranceValue =false;
                            articleDALModel.MstArticle.IsPremaman = false;
                       


                            Context.MstArticle.Add(articleDALModel.MstArticle);
                            Context.SaveChanges();

                            //// MStEan                           // vipin on 14-04-2017S
                            //if (articleDALModel.MstEanList.Count > 0)
                            //{
                            //    Context.MstEAN.AddRange(articleDALModel.MstEanList);
                            //    Context.SaveChanges();
                            //}


                            // SalesInfoRecord
                            if (articleDALModel.SalesInfoRecordList.Count > 0)
                            {
                                Context.SalesInfoRecord.AddRange(articleDALModel.SalesInfoRecordList);
                                Context.SaveChanges();
                            }

                            ////  PurchaseInfoRecord
                            if (articleDALModel.PurchaseInfoRecordList.Count > 0)
                            {
                                Context.PurchaseInfoRecord.AddRange(articleDALModel.PurchaseInfoRecordList);
                                Context.SaveChanges();
                            }

                            // ArticleMatrix
                            if (articleDALModel.ArticleMatrixList.Count > 0)
                            {
                                Context.ArticleMatrix.AddRange(articleDALModel.ArticleMatrixList);
                                Context.SaveChanges();
                            }

                            if (articleDALModel.ArticleStockBalances != null)
                            {
                                Context.ArticleStockBalances.AddRange(articleDALModel.ArticleStockBalances);
                                Context.SaveChanges();
                            }

                            // //ArticleUOM
                            Context.ArticleUOM.Add(articleDALModel.ArticleUOM);
                            //ArticleReplenishment 
                            if (articleDALModel.ArticleReplenishment != null)
                            {
                                if ((articleDALModel.ArticleReplenishment.SafetyStockQty != null && articleDALModel.ArticleReplenishment.SafetyStockQty > 0) ||
                                    (articleDALModel.ArticleReplenishment.ReOrderPoint != null && articleDALModel.ArticleReplenishment.ReOrderPoint > 0))
                                {
                                    Context.ArticleReplenishment.Add(articleDALModel.ArticleReplenishment);
                                }

                            }

                            // Updates Auto no 
                            if (articleDALModel.IsAutoNumber)
                                this.UpdateNextID(articleDALModel.SiteCode, "AC");

                            for (int EANRow = 0; EANRow < articleDALModel.EANAutoCount; EANRow++)
                            {
                                this.UpdateNextID(articleDALModel.SiteCode, "EC");
                            }


                        }
                        else
                        {
                            MstArticle ObjMstArticle;     // added by vipin 19-04-2017
                            ObjMstArticle = Context.MstArticle.Where(s => s.ArticleCode == articleDALModel.MstArticle.ArticleCode).FirstOrDefault();

                            if (ObjMstArticle != null)
                            {
                                articleDALModel.MstArticle.ArticalCatCode = ObjMstArticle.ArticalCatCode;
                               // articleDALModel.MstArticle.DistributionUnitofMeasure = ObjMstArticle.DistributionUnitofMeasure;
                                articleDALModel.MstArticle.IsMrpOpen = ObjMstArticle.IsMrpOpen;
                              //  articleDALModel.MstArticle.ParentArt = ObjMstArticle.ParentArt;
                               // articleDALModel.MstArticle.SupplierRef = ObjMstArticle.SupplierRef;
                                articleDALModel.MstArticle.ToleranceValue = ObjMstArticle.ToleranceValue;
                               // articleDALModel.MstArticle.LegacyArticleCode = ObjMstArticle.LegacyArticleCode;
                                articleDALModel.MstArticle.IsPremaman = ObjMstArticle.IsPremaman;
                              //  articleDALModel.MstArticle.DistributionUomValue = ObjMstArticle.DistributionUomValue;
                                articleDALModel.MstArticle.BrandCode = ObjMstArticle.BrandCode;
                            }

                            Update(articleDALModel.MstArticle, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode);


                            copyFile(articleDALModel.MstArticleImage.ArticleImage, articleDALModel.SourcePath,
                                articleDALModel.TargetPath);

                            // SalesInfoRecord
                            if (articleDALModel.SalesInfoRecordList.Count > 0)
                            {

                                for (int salesrecordCounter = 0; salesrecordCounter < articleDALModel.SalesInfoRecordList.Count; salesrecordCounter++)
                                {

                            SalesInfoRecord DeletedSalesInfoRecord;
                            SalesInfoRecord sir = articleDALModel.SalesInfoRecordList[salesrecordCounter];
                            DeletedSalesInfoRecord = Context.SalesInfoRecord.Where(s => s.ArticleCode == sir.ArticleCode && s.EAN == sir.EAN).FirstOrDefault();
                            if (DeletedSalesInfoRecord != null)
                            {
                                DeletedSalesInfoRecord.SellingPrice = sir.SellingPrice;
                                DeletedSalesInfoRecord.FromDate = sir.FromDate;
                                DeletedSalesInfoRecord.CREATEDAT = sir.CREATEDAT;
                                DeletedSalesInfoRecord.CREATEDBY = sir.CREATEDBY;
                                DeletedSalesInfoRecord.CREATEDON = sir.CREATEDON;
                                DeletedSalesInfoRecord.FreezeIN = sir.FreezeIN;

                                DeletedSalesInfoRecord.FreezeOB = sir.FreezeOB;
                                DeletedSalesInfoRecord.FreezeSB = sir.FreezeSB;
                                DeletedSalesInfoRecord.FreezeSR = sir.FreezeSR;
                                DeletedSalesInfoRecord.STATUS = sir.STATUS;
                                DeletedSalesInfoRecord.UPDATEDAT = sir.UPDATEDAT;

                                DeletedSalesInfoRecord.UPDATEDBY = sir.UPDATEDBY;
                                DeletedSalesInfoRecord.UPDATEDON = sir.UPDATEDON;

                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.SellingPrice).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.FromDate).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.CREATEDAT).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.CREATEDBY).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.FreezeIN).IsModified = true;

                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.FreezeOB).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.FreezeSB).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.FreezeSR).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.STATUS).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.UPDATEDAT).IsModified = true;

                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.UPDATEDBY).IsModified = true;
                                Context.Entry(DeletedSalesInfoRecord).Property(p => p.UPDATEDON).IsModified = true;


                                Context.SaveChanges();

                                //Context.Entry(DeletedSalesInfoRecord).State = System.Data.Entity.EntityState.Deleted;
                                //Context.SaveChanges();
                                //Context.SalesInfoRecord.Add(articleDALModel.SalesInfoRecordList[salesrecordCounter]);
                                //Context.SaveChanges();
                            }

                                //  SalesInfoRecord sir = articleDALModel.SalesInfoRecordList[salesrecordCounter];

                                // Update(sir, key => key.ArticleCode == articleDALModel.MstArticle.ArticleCode);
                                //  Update(sir, key => key.ArticleCode == sir.ArticleCode); //vipin
                                // Context.Entry<SalesInfoRecord>(sir).State = EntityState.Modified;  //vipin
                                // Context.Entry(sir).CurrentValues.SetValues(sir);
                                //Context.Entry(sir).Property(p => p.CREATEDBY).IsModified = false;
                                //Context.Entry(sir).Property(p => p.CREATEDON).IsModified = false;
                                //Context.Entry(sir).Property(p => p.CREATEDAT).IsModified = false;
                                //Context.Entry(sir).Property(p => p.STATUS).IsModified = false;
                           
                        
                                }
                            }


                            ////  PurchaseInfoRecord
                            if (articleDALModel.PurchaseInfoRecordList.Count > 0)
                            {
                                for (int PurchaserecordCounter = 0; PurchaserecordCounter < articleDALModel.PurchaseInfoRecordList.Count; PurchaserecordCounter++)
                                {

                                    PurchaseInfoRecord DeletedPurchaseInfoRecord;
                                    PurchaseInfoRecord Pir = articleDALModel.PurchaseInfoRecordList[PurchaserecordCounter];
                                    DeletedPurchaseInfoRecord = Context.PurchaseInfoRecord.Where(s => s.ArticleCode == Pir.ArticleCode && s.EAN == Pir.EAN && Pir.SupplierCode == s.SupplierCode).FirstOrDefault();
                                    if (DeletedPurchaseInfoRecord != null)
                                    {

                                        DeletedPurchaseInfoRecord.CPLocalCurr = Pir.CPBaseCurr;
                                        DeletedPurchaseInfoRecord.CPLocalCurr = Pir.CPLocalCurr;
                                        DeletedPurchaseInfoRecord.MRP = Pir.MRP;
                                        DeletedPurchaseInfoRecord.UPDATEDAT = Pir.UPDATEDAT;
                                        DeletedPurchaseInfoRecord.UPDATEDBY = Pir.UPDATEDBY;
                                        DeletedPurchaseInfoRecord.UPDATEDON = Pir.UPDATEDON;
                                        DeletedPurchaseInfoRecord.ToDate = Pir.ToDate;
                                        //DeletedPurchaseInfoRecord.SiteCode = Pir.SiteCode;

                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.CPBaseCurr).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.CPLocalCurr).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.MRP).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.UPDATEDAT).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.UPDATEDBY).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.UPDATEDON).IsModified = true;
                                        Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.ToDate).IsModified = true;
                                       // Context.Entry(DeletedPurchaseInfoRecord).Property(p => p.SiteCode).IsModified = true;



                                        Context.SaveChanges();
                                        //Context.Entry(DeletedPurchaseInfoRecord).State = System.Data.Entity.EntityState.Deleted;

                                        //Context.SaveChanges();
                                        //Context.PurchaseInfoRecord.Add(articleDALModel.PurchaseInfoRecordList[PurchaserecordCounter]);
                                    }


                                    //PurchaseInfoRecord pir = articleDALModel.PurchaseInfoRecordList[PurchaserecordCounter];
                                  //  Context.Entry<PurchaseInfoRecord>(pir).State = EntityState.Modified;
                                 //   Update(pir, key => key.ArticleCode == pir.ArticleCode); //vipin
                                    //Context.Entry(pir).Property(p => p.CREATEDBY).IsModified = false;
                                    //Context.Entry(pir).Property(p => p.CREATEDON).IsModified = false;
                                    //Context.Entry(pir).Property(p => p.CREATEDAT).IsModified = false;
                                    //Context.Entry(pir).Property(p => p.STATUS).IsModified = false;
                                }

                            }

                            // ArticleMatrix
                            if (articleDALModel.ArticleMatrixList.Count > 0)
                            {
                                //  Context.ArticleMatrix.AddRange(articleDALModel.ArticleMatrixList);
                                for (int ArticleMatrixCounter = 0; ArticleMatrixCounter < articleDALModel.ArticleMatrixList.Count - 1; ArticleMatrixCounter++)
                                {

                                    ArticleMatrix DeletedArticleMatrixRecord;
                                    ArticleMatrix Air = articleDALModel.ArticleMatrixList[ArticleMatrixCounter];
                                    DeletedArticleMatrixRecord = Context.ArticleMatrix.Where(s => s.ArticleCode == Air.ArticleCode).FirstOrDefault();
                                    if (DeletedArticleMatrixRecord != null)
                                    {

                                        DeletedArticleMatrixRecord.CharType = Air.CharType;
                                        DeletedArticleMatrixRecord.CharValue = Air.CharValue;
                                        DeletedArticleMatrixRecord.CREATEDAT = Air.CREATEDAT;
                                        DeletedArticleMatrixRecord.CREATEDBY = Air.CREATEDBY;
                                        DeletedArticleMatrixRecord.CREATEDON = Air.CREATEDON;
                                        DeletedArticleMatrixRecord.UPDATEDAT = Air.UPDATEDAT;
                                        DeletedArticleMatrixRecord.UPDATEDBY = Air.UPDATEDBY;
                                        DeletedArticleMatrixRecord.UPDATEDON = Air.UPDATEDON;
                                        DeletedArticleMatrixRecord.STATUS = Air.STATUS;

                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.CharType).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.CharValue).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.CREATEDAT).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.CREATEDBY).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.CREATEDON).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.UPDATEDAT).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.UPDATEDBY).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.UPDATEDON).IsModified = true;
                                        Context.Entry(DeletedArticleMatrixRecord).Property(p => p.STATUS).IsModified = true;

                                        Context.SaveChanges();
                                        //Context.Entry(DeletedArticleMatrixRecord).State = System.Data.Entity.EntityState.Deleted;

                                    
                                        //Context.ArticleMatrix.Add(articleDALModel.ArticleMatrixList[ArticleMatrixCounter]);
                                    }



                                    //   PurchaseInfoRecord tempPurchaseInfoRecord = Context.ArticleMatrix.Where(x => x.ArticleCode == articleCode && x.EAN == eanCode && x.SiteCode == siteCode).FirstOrDefault();
                                    //ArticleMatrix artMatrix = articleDALModel.ArticleMatrixList[ArticleMatrixCounter];
                                    //Context.Entry<ArticleMatrix>(artMatrix).State = EntityState.Modified;
                                    //Context.Entry(artMatrix).Property(p => p.CREATEDBY).IsModified = false;
                                    //Context.Entry(artMatrix).Property(p => p.CREATEDON).IsModified = false;
                                    //Context.Entry(artMatrix).Property(p => p.CREATEDAT).IsModified = false;
                                    //Context.Entry(artMatrix).Property(p => p.STATUS).IsModified = false;
                                }
                            }

                            // //ArticleUOM
                          //  ArticleUOM ObjArticleUOM = new ArticleUOM();

                         ArticleUOM  DeletedArticleUOM;
                        // DeletedArticleUOM = Context.ArticleUOM.OrderBy(p => p.UOMCode).Where(s => s.ArticleCode == articleDALModel.ArticleUOM.ArticleCode).FirstOrDefault();
                         DeletedArticleUOM = Context.ArticleUOM.Where(s => s.ArticleCode == articleDALModel.ArticleUOM.ArticleCode && s.UOMCode == articleDALModel.ArticleUOM.UOMCode).FirstOrDefault();


                         if (DeletedArticleUOM != null)
                         {


                             DeletedArticleUOM.Divisor = articleDALModel.ArticleUOM.Divisor;
                             DeletedArticleUOM.UpdatedAt = articleDALModel.ArticleUOM.UpdatedAt;
                             DeletedArticleUOM.CreatedBy = articleDALModel.ArticleUOM.UpdatedBy;
                             DeletedArticleUOM.UpdatedOn = articleDALModel.ArticleUOM.UpdatedOn;
                             DeletedArticleUOM.CreatedAt = articleDALModel.ArticleUOM.CreatedAt;
                             DeletedArticleUOM.CreatedBy = articleDALModel.ArticleUOM.CreatedBy;
                             DeletedArticleUOM.CreatedOn = articleDALModel.ArticleUOM.CreatedOn;

                             Context.Entry(DeletedArticleUOM).Property(p => p.Divisor).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.UpdatedAt).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.UpdatedBy).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.UpdatedOn).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.CreatedAt).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.CreatedBy).IsModified = true;
                             Context.Entry(DeletedArticleUOM).Property(p => p.CreatedOn).IsModified = true;

                             Context.SaveChanges();
                             //Context.Entry(DeletedArticleUOM).State = System.Data.Entity.EntityState.Deleted;

                             //Context.SaveChanges();
                             //Context.ArticleUOM.Add(articleDALModel.ArticleUOM);
                         }


                          //  if(ArticleCount !=0)
                          //  {
                          //  CheckArticle =DeletedArticleUOM.ArticleCode.ToString();
                          //  }

                          //if ( CheckArticle ==DeletedArticleUOM.ArticleCode.ToString())
                          //{
                          // ArticleCount = ArticleCount + 1;
                          //}

                          // if (ArticleCount < 1)
                          // {
                             
                          // }

                            //Context.Entry(articleDALModel.ArticleUOM).State = System.Data.Entity.EntityState.Deleted;
                       

                            //ArticleDALModel articleDALModel1 = new ArticleDALModel();
                            //articleDALModel1 = articleExcelDALModel[lstCntr];


                           // Update(articleDALModel.ArticleUOM, key => key.ArticleCode == articleDALModel.ArticleUOM.ArticleCode);
                          
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.ArticleCode).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.UOMCode).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.Divisor).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.CreatedBy).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.CreatedAt).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.CreatedOn).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.UpdatedBy).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.UpdatedAt).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.UpdatedOn).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.Status).IsModified = true;
                            //Context.Entry(articleDALModel.ArticleUOM).Property(p => p.defaultUom).IsModified = true;

                          
                            //Context.Entry(ObjArticleUOM).State = EntityState.Modified;
                        }

                        Context.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                    tranResult = true;
                    CheckArticle = "";
                    ArticleCount = 0;
                }
                catch (Exception ex)
                {
                    CheckArticle = "";
                    ArticleCount = 0;
                    dbContextTransaction.Rollback();
                    dbContextTransaction.Dispose();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                  //  throw ex;
                }
            }
            return tranResult;
        }
      
        private void Update<T>(T entity, Func<T, bool> keycondition) where T : class
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = Context.Set<T>();
                T attachedEntity = set.Local.SingleOrDefault(keycondition);
                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }

            }
        }

        // UpdateMst
        public IList<PurchaseInfoRecord> ArticleById(string autoArticleCode)
        {
            try
            {
                return Context.PurchaseInfoRecord.Where(u => u.ArticleCode == autoArticleCode).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateMst(List<PurchaseInfoRecord> purinfor)
        {
            try
            {
               // Context.Entry<List<PurchaseInfoRecord>>(purinfor).State = EntityState.Modified;
                foreach (PurchaseInfoRecord item in purinfor)
                {
                     Context.Entry(item).State = EntityState.Modified; 
                }
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool UpdateArticle(MstArticle article)
        {
            try
            {
                Context.Entry<MstArticle>(article).State = EntityState.Modified;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void DeleteArticle(MstArticle article)
        {
            try
            {
                Context.MstArticle.Remove(article);
                Context.Entry<MstArticle>(article).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteByID(string articleID)
        {
            try
            {
                var article = Context.MstArticle.Where(u => u.ArticleCode == articleID).FirstOrDefault();

                Context.MstArticle.Remove(article);
                Context.Entry<MstArticle>(article).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ArticleSearchModel> GetArticleSearchList()
        {
            try
            {
                return (from asl in Context.MstArticle
                        select new ArticleSearchModel
                        {
                            ArticleCode = asl.ArticleCode,
                            ArticleName = asl.ArticleName,
                            BaseUnitofMeasure = asl.BaseUnitofMeasure,
                            STATUS = asl.STATUS
                        }
                       ).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get list of Article 
        /// </summary>
        /// <returns>IList<Article> </returns>
        public IQueryable<MstArticle> GetArticles()
        {
            try
            {
                var articleList = (from s in Context.MstArticle
                                   where s.STATUS == true
                                   orderby s.ArticleName
                                   select s).AsQueryable();

                return articleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void getTreeCode(string nodeName, ref string NodeCode, ref string TreeCode)
        {

            var treeCodeq = (from p in Context.ArticleTreeNodeMap
                             join t in Context.MstArticleNode on p.Nodecode equals t.Nodecode
                             where t.NodeName == nodeName
                             select new { p.Treecode, p.Nodecode }).FirstOrDefault();

            if (treeCodeq != null)
            {
                NodeCode = treeCodeq.Nodecode;
                TreeCode = treeCodeq.Treecode;
            }

        }

        public IList<ArticlePurchaseModel> GetArticlePurchaseList(string supplierCode, List<string> articleCodes)
        {
            try
            {
                string sitecode = CommonModel.SiteCode;
                //var articlelist = (from asb in Context.ArticleStockBalances
                //                   join atnm in Context.MstArticle on asb.ArticleCode equals atnm.ArticleCode
                //                   join man in Context.PurchaseInfoRecord on atnm.ArticleCode equals man.ArticleCode into t
                //                   from rt in t.DefaultIfEmpty()
                //                   where asb.SiteCode == CommonModel.SiteCode && rt.SiteCode == CommonModel.SiteCode && ((string.IsNullOrEmpty(supplierCode) || (!string.IsNullOrEmpty(supplierCode) && rt.SupplierCode == supplierCode))
                //                   && (articleCodes.Count == 0) || (articleCodes.Count > 0 && articleCodes.Contains(rt.ArticleCode)))
                //                   select new ArticlePurchaseModel
                //                   {
                //                       Select = false,
                //                       ArticleCode = atnm.ArticleCode,
                //                       ArticleName = atnm.ArticleName,
                //                       BaseUnitofMeasure = atnm.OrderUnitofMeasure,
                //                       Cost = rt.CPBaseCurr,
                //                       EAN = asb.EAN,
                //                       Quantity = 1,
                //                       NetAmount = (rt.CPBaseCurr.HasValue) ? rt.CPBaseCurr.Value : 0,
                //                       FILTER=atnm.ArticleCode + atnm.ArticleName
                //                   }).Distinct().ToList();
                if (! string.IsNullOrEmpty(supplierCode))
                {

                    var articlelist = (from asb in Context.ArticleStockBalances
                                       join atnm in Context.MstArticle
                                       on asb.ArticleCode equals atnm.ArticleCode
                                       join man in Context.PurchaseInfoRecord
                                       on atnm.ArticleCode equals man.ArticleCode

                                       where (asb.SiteCode == CommonModel.SiteCode
                                       && man.IsDefaultSupplier == true && man.SupplierCode == supplierCode
                                       && asb.SiteCode == CommonModel.SiteCode
                                       )
                                       select new ArticlePurchaseModel
                                       {
                                           Select = false,
                                           ArticleCode = atnm.ArticleCode,
                                           ArticleName = atnm.ArticleName,
                                           BaseUnitofMeasure = atnm.OrderUnitofMeasure,
                                           Cost = man.CPBaseCurr,
                                           EAN = asb.EAN,
                                           Quantity = 1,
                                           NetAmount = (man.CPBaseCurr.HasValue) ? man.CPBaseCurr.Value : 0,
                                           FILTER = atnm.ArticleCode + atnm.ArticleName
                                       }).Distinct().ToList();
                    return articlelist;
                }
                else
                {
                    var articlelist = (from asb in Context.ArticleStockBalances
                                       join atnm in Context.MstArticle on asb.ArticleCode equals atnm.ArticleCode
                                       join man in Context.PurchaseInfoRecord on atnm.ArticleCode equals man.ArticleCode into t
                                       from rt in t.DefaultIfEmpty()
                                       where asb.SiteCode == CommonModel.SiteCode && rt.SiteCode == CommonModel.SiteCode && ((string.IsNullOrEmpty(supplierCode) || (!string.IsNullOrEmpty(supplierCode) && rt.SupplierCode == supplierCode))
                                       && (articleCodes.Count == 0) || (articleCodes.Count > 0 && articleCodes.Contains(rt.ArticleCode)))
                                       select new ArticlePurchaseModel
                                       {
                                           Select = false,
                                           ArticleCode = atnm.ArticleCode,
                                           ArticleName = atnm.ArticleName,
                                           BaseUnitofMeasure = atnm.OrderUnitofMeasure,
                                           Cost = rt.CPBaseCurr,
                                           EAN = asb.EAN,
                                           Quantity = 1,
                                           NetAmount = (rt.CPBaseCurr.HasValue) ? rt.CPBaseCurr.Value : 0,
                                           FILTER = atnm.ArticleCode + atnm.ArticleName
                                       }).Distinct().ToList();
                    return articlelist;
                }
                //return articlelist;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<ArticlePurchaseStockoutModel> GetArticlePurchaseList(string supplierCode)
        {
            try
            {
                string sitecode = CommonModel.SiteCode;
                var articlelist = (from asb in Context.ArticleStockBalances
                                   join atnm in Context.MstArticle on asb.ArticleCode equals atnm.ArticleCode
                                   join man in Context.PurchaseInfoRecord on atnm.ArticleCode equals man.ArticleCode into t
                                   from rt in t.DefaultIfEmpty()
                                   where asb.SiteCode == CommonModel.SiteCode && rt.SiteCode == CommonModel.SiteCode && (string.IsNullOrEmpty(supplierCode) || (!string.IsNullOrEmpty(supplierCode) && rt.SupplierCode == supplierCode))
                                   select new ArticlePurchaseStockoutModel
                                   {
                                       Select = false,
                                       ArticleCode = atnm.ArticleCode,
                                       ArticleName = atnm.ArticleName,
                                       BaseUnitofMeasure = atnm.OrderUnitofMeasure,
                                       EAN = asb.EAN,
                                       AvailableQty = asb.PhysicalQty,
                                       Reason="",
                                       FILTER = atnm.ArticleCode + atnm.ArticleName
                                   }).Distinct().ToList();
                return articlelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<MstEAN> GetArticleEAN()
        {
            try
            {
                return Context.MstEAN.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<SiteArticleTaxMappingModel> GetsiteArticleTaxMappingList(string articleCode)
        {
            try
            {
                string siteCode = CommonModel.SiteCode;

                return (from tax in Context.MstTax
                        join
                            taxMap in Context.SiteArticleTaxMapping on tax.TaxCode equals taxMap.TaxCode
                        where taxMap.ArticleCode == articleCode && taxMap.SiteCode == siteCode
                        select new SiteArticleTaxMappingModel
                        {
                            SiteCode = taxMap.SiteCode,
                            ArticleCode = taxMap.ArticleCode,
                            TaxCode = taxMap.TaxCode,
                            TaxName = taxMap.TaxName,
                            SupplierCode = taxMap.SupplierCode,
                            TaxValue = tax.Value,
                            Inclusive = tax.Inclusive
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SalesInfoRecord GetArticleSalesInfoRecord(string articleCode)
        {
            try
            {
                return Context.SalesInfoRecord.Where(p => p.ArticleCode == articleCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<MstEAN> GetArticleEAN(string articleID)
        {
            try
            {
                return Context.MstEAN.Where(p => p.ArticleCode == articleID).ToList();
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public IList<ArticleMatrix> GetArticleCharacteristicMatrixModel(string articleCode)
        {
            try
            {
                var result = (from ean in Context.MstEAN
                              join
                                  articleMatrix in Context.ArticleMatrix on ean.EAN equals articleMatrix.EanCode
                              select articleMatrix
                    ).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<PurchaseInfoRecordModel> GetArticlePurchaseInfoRecordList(string articleCode)
        {
            try
            {
                return (from PIR in Context.PurchaseInfoRecord
                        where PIR.ArticleCode == articleCode
                         //code commented for loading all suppliers of this Article code - ashma on 26-4-18 
                         // && PIR.IsDefaultSupplier == true               //code added by irfan on 09/11/2017
                        select new PurchaseInfoRecordModel
                        {
                            SupplierCode = PIR.SupplierCode,
                            IsDefaultSupplier = PIR.IsDefaultSupplier,
                            ArticleCode = PIR.ArticleCode,
                            EAN = PIR.EAN
                        }).Distinct().OrderByDescending(a => a.IsDefaultSupplier).ToList(); // added order by default supplier - ashma 26-4-18
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MstArticle GetArticleByID(string articleID)
        {
            try
            {
                return Context.MstArticle.Where(u => u.ArticleCode == articleID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArticleModel GetArticleModelByID(string articleID)
        {
            try
            {
                string siteCode = CommonModel.SiteCode;

                var p = (from article in Context.MstArticle
                         join
                             parent in Context.MstArticleNode on article.ParentArt equals parent.Nodecode
                         join ean in Context.MstEAN on article.ArticleCode equals ean.ArticleCode into tempEAN
                         from t1 in tempEAN.DefaultIfEmpty()
                         join PIR in Context.PurchaseInfoRecord on new { ean = t1.EAN, Psitecode = siteCode } equals new { ean = PIR.EAN, Psitecode = PIR.SiteCode } into tempPIR
                         from t2 in tempPIR.DefaultIfEmpty()
                         join SIR in Context.SalesInfoRecord on new { ean = t1.EAN, Psitecode = siteCode } equals new { ean = SIR.EAN, Psitecode = SIR.SiteCode } into tempSIR
                         from t3 in tempSIR.DefaultIfEmpty()
                         join MAI in Context.MstArticleImage on article.ArticleCode equals MAI.ArticleCode into tempMAI
                         from t4 in tempMAI.DefaultIfEmpty()
                         where t1.DefaultEAN == true && article.ArticleCode == articleID
                         select new ArticleModel
                             {
                                 ArticleCode = article.ArticleCode,
                                 ArticalTypeCode = article.ArticalTypeCode,
                                 ArticalCatCode = article.ArticalCatCode,
                                 ArticleShortName = article.ArticleShortName,
                                 ArticleName = article.ArticleName,
                                 BaseUnitofMeasure = article.BaseUnitofMeasure,
                                 DistributionUnitofMeasure = article.DistributionUnitofMeasure,
                                 SaleUnitofMeasure = article.SaleUnitofMeasure,
                                 OrderUnitofMeasure = article.OrderUnitofMeasure,
                                 CataloguedOn = article.CataloguedOn,
                                 ArticleActive = article.ArticleActive,
                                 IssueFreeGift = article.IssueFreeGift,
                                 ProductImage = article.ProductImage,
                                 TotalShelfLife = article.TotalShelfLife,
                                 RemainingShelfLife = article.RemainingShelfLife,
                                 DecimalQtyApplicable = article.DecimalQtyApplicable,
                                 IsMrpOpen = article.IsMrpOpen,
                                 SerialNumber = article.SerialNumber,
                                 WarrantyPeriod = article.WarrantyPeriod,
                                 ParentArt = article.ParentArt,
                                 TreeID = article.TreeID,
                                 LastNodeCode = article.LastNodeCode,
                                 CharProfileCode = article.CharProfileCode,
                                 SupplierRef = article.SupplierRef,
                                 STLocCode = article.STLocCode,
                                 Salable = article.Salable,
                                 ToleranceValue = article.ToleranceValue,
                                 LegacyArticleCode = article.LegacyArticleCode,
                                 Purchaser = article.Purchaser,
                                 IntraStatCodeEurope = article.IntraStatCodeEurope,
                                 IntraStatCodeUSA = article.IntraStatCodeUSA,
                                 NetWeight = article.NetWeight,
                                 NetWeightUOM = article.NetWeightUOM,
                                 GrossWeight = article.GrossWeight,
                                 GrossWeightUOM = article.GrossWeightUOM,
                                 Volume = article.Volume,
                                 VolumeUOM = article.VolumeUOM,
                                 Style = article.Style,
                                 Season = article.Season,
                                 Theme = article.Theme,
                                 IsPremaman = article.IsPremaman,
                                 NONRETUNABLE = article.NONRETUNABLE,
                                 MaterialTypeCode = article.MaterialTypeCode,
                                 ConsumptionUoM = article.ConsumptionUoM,
                                 isExpiry = article.isExpiry,
                                 Printable = article.Printable,//  Descriptions = article.Descriptions ,
                                 IsEanAutoGenerate = article.IsEanAutoGenerate,
                                 IsBatchBarcodeAutoGenerate = article.IsBatchBarcodeAutoGenerate,
                                 SalesUomValue = article.SalesUomValue,
                                 ManufacturerCode = article.ManufacturerCode,
                                 BrandCode = article.BrandCode,
                                 DistributionUomValue = article.DistributionUomValue,
                                 ParentArticleName = parent.NodeName,
                                 CostPrice = t2.CPLocalCurr,
                                 SellPrice = t3.SellingPrice,
                                 Margin = 0,
                                 MRP = t2.MRP,
                                 ArticleImage = t4.ArticleImage
                             }

                     );

                return p.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool copyFile(string fileName = "test.txt", string sourcePath = @"C:\Users\Public\TestFolder",
            string targetPath = @"C:\Users\Public\TestFolder\SubDir")
        {
            try
            {
                if (fileName != null && sourcePath != null)
                {
                    if (fileName.Length > 0 && sourcePath.Length > 0)
                    {
                        // Use Path class to manipulate file and directory paths. 
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(targetPath, fileName);

                        // To copy a folder's contents to a new location: Create a new target folder, if necessary. 
                        if (!System.IO.Directory.Exists(targetPath))
                        {
                            System.IO.Directory.CreateDirectory(targetPath);
                        }
                        // To copy a file to another location and overwrite the destination file if it already exists.
                        System.IO.File.Copy(sourceFile, destFile, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        //public ArticleDataExportModel GetArticleExportData(string nodeCode, string siteCode)
        //{
        //    SqlParameter[] sqlParameters = new SqlParameter[2];

        //    sqlParameters[0] = new SqlParameter
        //    {
        //        ParameterName = "V_NodeCode",
        //        SqlDbType = SqlDbType.VarChar,
        //        SqlValue = nodeCode
        //    };
        //    sqlParameters[1] = new SqlParameter
        //    {
        //        ParameterName = "V_SiteCode",
        //        SqlDbType = SqlDbType.VarChar,
        //        SqlValue = siteCode
        //    };

        //    var articleDataExportModel = new ArticleDataExportModel();
        //    System.Data.Objects.ObjectContext objectContext = new System.Data.Objects.ObjectContext(Context.Database.Connection.ConnectionString);
        //    //System.Data.Objects.ObjectContext objectContext1 = (this.Context).o; 
        //    //( this.Context).ObjectContext;

        //    DbCommand cmd = objectContext.CreateStoreCommand("GetArticleXlsData", sqlParameters);

        //    // Use a connection scope to manage the lifetime of the connection
        //    using (var connectionScope = cmd.Connection.CreateConnectionScope())
        //    {
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            // Materialize the recipe.
        //            //articleDataExportModel = reader.Materialize<ArticleDetails>().Bind(articleDataExportModel.ArticleDetails).FirstOrDefault();
        //            if (articleDataExportModel != null)
        //            {
        //                // Move on to the categories resultset and attach it to the recipe.
        //                // Also bind it to the datacontext, so the object tracking works correctly.
        //                reader.NextResult();
        //                articleDataExportModel.TaxDetails = reader.Materialize<TaxDetails>().Bind(objectContext);

        //                // Materialize the recipe ingredient information and attach it to the recipe.
        //                // Also bind it to the datacontext, so the object tracking works correctly.
        //                reader.NextResult();
        //                var charDetails = reader.Materialize<CharDetails>().Bind(objectContext);

        //                // Materialize the ingredients and attach them to the datacontext to enable object tracking.
        //                reader.NextResult();
        //                var salesDetails = reader.Materialize<SalesDetails>().Bind(objectContext);

        //                // Iterate over the ingredient information and attach the ingredients to the records
        //                foreach (var item in articleDataExportModel.SalesDetails)
        //                {
        //                    // Attach the related ingredient to the ingredient reference property
        //                    //item.IngredientReference.Attach(ingredients.FirstOrDefault(
        //                    //ingredient => ingredient.IngredientId == item.IngredientId));
        //                }

        //                reader.NextResult();
        //                var purchaseDetails = reader.Materialize<PurchaseDetails>().Bind(objectContext);

        //                // Iterate over the ingredients in the recipe and attach the related unit information
        //                foreach (var ingredient in articleDataExportModel.PurchaseDetails.Select(item => item.ArticleCode))
        //                {
        //                    // Attach the related unit to the unit reference property
        //                    //ingredient.UnitReference.Attach(units.FirstOrDefault(unit => unit.UnitId == ingredient.UnitId));
        //                }
        //            }
        //        }
        //    }
        //    return articleDataExportModel;


        //    }

        public bool IsBarCodeExist(string eanCode)
        {
            bool result = false;
            var temp = Context.MstEAN.Where(r => r.EAN == eanCode).FirstOrDefault();

            if (temp != null)
            {
                result = true;
            }
            return result;
        }

        public ArticleDataExportModel GetArticleExportData(string nodeCode, string siteCode)
        {

            var articleDataExportModel = new ArticleDataExportModel();

            for (int resultsetNo = 1; resultsetNo <= 5; resultsetNo++)
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@V_NodeCode",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = nodeCode
                };
                sqlParameters[1] = new SqlParameter
                {
                    ParameterName = "@V_SiteCode",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = siteCode
                };


                sqlParameters[2] = new SqlParameter
                {
                    ParameterName = "@V_ResultSetNo",
                    SqlDbType = SqlDbType.Int,
                    SqlValue = resultsetNo
                };

                GetArticleXlsExportDataM(ref articleDataExportModel, resultsetNo, sqlParameters);
            }




            // Use a connection scope to manage the lifetime of the connection
            //using (var connectionScope = cmd.Connection.CreateConnectionScope())
            //{
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        // Materialize the recipe.
            //        //articleDataExportModel = reader.Materialize<ArticleDetails>().Bind(articleDataExportModel.ArticleDetails).FirstOrDefault();
            //        if (articleDataExportModel != null)
            //        {
            //            // Move on to the categories resultset and attach it to the recipe.
            //            // Also bind it to the datacontext, so the object tracking works correctly.
            //            reader.NextResult();
            //            articleDataExportModel.TaxDetails = reader.Materialize<TaxDetails>().Bind(objectContext);

            //            // Materialize the recipe ingredient information and attach it to the recipe.
            //            // Also bind it to the datacontext, so the object tracking works correctly.
            //            reader.NextResult();
            //            var charDetails = reader.Materialize<CharDetails>().Bind(objectContext);

            //            // Materialize the ingredients and attach them to the datacontext to enable object tracking.
            //            reader.NextResult();
            //            var salesDetails = reader.Materialize<SalesDetails>().Bind(objectContext);

            //            // Iterate over the ingredient information and attach the ingredients to the records
            //            foreach (var item in articleDataExportModel.SalesDetails)
            //            {
            //                // Attach the related ingredient to the ingredient reference property
            //                //item.IngredientReference.Attach(ingredients.FirstOrDefault(
            //                //ingredient => ingredient.IngredientId == item.IngredientId));
            //            }

            //            reader.NextResult();
            //            var purchaseDetails = reader.Materialize<PurchaseDetails>().Bind(objectContext);

            //            // Iterate over the ingredients in the recipe and attach the related unit information
            //            foreach (var ingredient in articleDataExportModel.PurchaseDetails.Select(item => item.ArticleCode))
            //            {
            //                // Attach the related unit to the unit reference property
            //                //ingredient.UnitReference.Attach(units.FirstOrDefault(unit => unit.UnitId == ingredient.UnitId));
            //            }
            //        }
            //    }
            //}
            return articleDataExportModel;


        }
        public ArticleDataExportModel GetArticlesForBarcodeExportData(string nodeCode, string siteCode)
        {

            var articleDataExportModel = new ArticleDataExportModel();


            SqlParameter[] sqlParameters = new SqlParameter[3];

            sqlParameters[0] = new SqlParameter
            {
                ParameterName = "@V_NodeCode",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = nodeCode
            };
            sqlParameters[1] = new SqlParameter
            {
                ParameterName = "@V_SiteCode",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = siteCode
            };


            sqlParameters[2] = new SqlParameter
            {
                ParameterName = "@V_ResultSetNo",
                SqlDbType = SqlDbType.Int,
                SqlValue = 1
            };

            GetArticleXlsExportDataM(ref articleDataExportModel, 1, sqlParameters);



            return articleDataExportModel;


        }
        private void GetArticleXlsExportDataM(ref ArticleDataExportModel articleDataExportModel, int resultsetNo, SqlParameter[] sqlParameters)
        {
            switch (resultsetNo)
            {
                case 1:
                    articleDataExportModel.ArticleDetails = Context.Database.SqlQuery<ArticleDetails>("GetArticleXlsExportData @V_NodeCode,@V_SiteCode,@V_ResultSetNo ", sqlParameters.ToArray()).ToList();
                    break;
                case 2:
                    articleDataExportModel.TaxDetails = Context.Database.SqlQuery<TaxDetails>("GetArticleXlsExportData @V_NodeCode,@V_SiteCode,@V_ResultSetNo ", sqlParameters.ToArray()).ToList();

                    // var t = Context.Database.SqlQuery<ItemCollection>("GetArticleXlsExportData", sqlParameters.ToArray());
                    break;
                case 3:
                    articleDataExportModel.CharDetails = Context.Database.SqlQuery<CharDetails>("GetArticleXlsExportData @V_NodeCode,@V_SiteCode,@V_ResultSetNo ", sqlParameters).ToList();
                    break;
                case 4:
                    articleDataExportModel.SalesDetails = Context.Database.SqlQuery<SalesDetails>("GetArticleXlsExportData @V_NodeCode,@V_SiteCode,@V_ResultSetNo ", sqlParameters).ToList();
                    break;
                case 5:
                    articleDataExportModel.PurchaseDetails = Context.Database.SqlQuery<PurchaseDetails>("GetArticleXlsExportData @V_NodeCode,@V_SiteCode,@V_ResultSetNo ", sqlParameters).ToList();
                    break;
            }
        }


        #region "Article Import From Excel"

        #region "Validations"        

        private static bool IsNumeric(string value)
        {
            Regex rex = new Regex(@"^(([0-9]*)|(([0-9]*).([0-9]*)))$");
            //return value.Trim().length > 0 && value.Trim().Ism(/^[0-9]*(\.[0-9]+)?$/)
            return rex.IsMatch(value);
        }

        private static bool IsAlphaNumeric(string value)
        {
            Regex rex = new Regex(@"[a-zA-Z0-9]*");
            return rex.IsMatch(value);
        }
        private bool ArtilceDetailsValidations(ref XLSheet articleDetails, ref bool result, ref String ErrorMsgOut) //vipin
        {
            string siteCode = string.Empty;
            string articleCode = string.Empty;
            string articleType = string.Empty;
            string materialType = string.Empty;
            MstArticleNode actualLastNode = null;

            string parentArticleNode = string.Empty;
            string lastNode = string.Empty;
            string articleHierarchy = string.Empty;
            bool isOptionalValue = false;

            try
            {
                Boolean ISDynamicDenereted = false;
                for (int rowCntr = 1; rowCntr < articleDetails.Rows.Count; rowCntr++)
                {
                    siteCode = articleDetails[rowCntr, 13].Value.ToString();  //vipin

                    if (articleDetails[rowCntr, 0].Value != null)
                    {
                        articleCode = articleDetails[rowCntr, 0].Value.ToString();
                    }

                    for (int colIndex = 0; colIndex < articleDetails.Columns.Count; colIndex++)
                    {
                       

                        if (articleDetails[rowCntr, colIndex].Value == null)
                        {
                            articleDetails[rowCntr, colIndex].Value = string.Empty;
                        }
                        string switchCaseValue = articleDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim();
                        switch (articleDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                

                        {
                            case "ARTICLE CODE":
                                if (!string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    ISDynamicDenereted = false;
                                    if (articleDetails[rowCntr, colIndex].Value.ToString().Length > 26)
                                    {
                                        result = false;
                                        // throw new ApplicationException("please provide Article Code length is less than 26 char in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg = "please provide Article Code length is less than 26 char in 'ARTICLE Data' sheet at row no " + rowCntr;

                                    }
                                    else
                                    {
                                        Regex rx = new Regex(@"[a-zA-Z0-9]*");
                                        if (!rx.IsMatch(articleDetails[rowCntr, colIndex].Value.ToString()))
                                        {
                                            result = false;
                                            // throw new ApplicationException("please provide Article Code Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr);
                                            ErrorMsg = "please provide Article Code Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr;
                                        }
                                    }

                                }
                                else
                                {
                                    ISDynamicDenereted = true;
                                }
                                break;

                            case "ARTICLE NAME":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    result = false;
                                 //   throw new ApplicationException("Please provide Article Name in 'ARTICLE Data' sheet at rw no " + rowCntr);
                                    ErrorMsg ="Please provide Article Name in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    break;
                                   

                                }
                                else
                                {
                                    if (articleDetails[rowCntr, colIndex].Value.ToString().Length > 60)
                                    {
                                        result = false;
                                        //throw new ApplicationException("please provide Article Code length is less than 60 char in 'ARTICLE Data' sheet at row no " + rowCntr);
                                          ErrorMsg ="please provide Article Code length is less than 60 char in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                    else
                                    {
                                        Regex rx = new Regex(@"[a-zA-Z0-9][a-zA-Z0-9 ]*[a-zA-Z0-9]");
                                        if (!rx.IsMatch(articleDetails[rowCntr, colIndex].Value.ToString()))
                                        {
                                            result = false;
                                         //   throw new ApplicationException("please provide Article Name  Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr);
                                            ErrorMsg ="please provide Article Name  Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr;
                                        }
                                    }
                                }
                                break;

                            case "ARTICLE SHORT NAME":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    result = false;
                                 //   throw new ApplicationException("Please provide 'Article Short Name' in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg="Please provide 'Article Short Name' in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                else
                                {
                                    if (articleDetails[rowCntr, colIndex].Value.ToString().Length > 30)
                                    {
                                        result = false;
                                        //throw new ApplicationException("please provide 'Article Short Name' length is less than 30 char in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg = "Please provide 'Article Short Name' in 'ARTICLE Data' sheet at row no " + rowCntr;

                                    }
                                    else
                                    {
                                        Regex rx = new Regex(@"[a-zA-Z0-9][a-zA-Z0-9 ]*[a-zA-Z0-9]");
                                        if (!rx.IsMatch(articleDetails[rowCntr, colIndex].Value.ToString()))
                                        {
                                            result = false;
                                           // throw new ApplicationException("please provide 'Article Short Name' Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr);
                                            ErrorMsg = "please provide 'Article Short Name' Alphanumeric in 'ARTICLE Data' sheet at row no " + rowCntr;
                                        }
                                    }
                                }
                                break;

                            case "ARTICLE TYPE":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    result = false;
                                  //  throw new ApplicationException("Article type in 'Article Data' sheet at row no " + rowCntr);
                                          ErrorMsg = "Article type in 'Article Data' sheet at row no " + rowCntr;
                                }
                                else
                                {
                                    articleType = articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                    var articleTypeResult = Context.MstArticleType.Where(x => x.ArticalTypeCode == articleType).FirstOrDefault();

                                    if (articleTypeResult == null)
                                    {
                                        result = false;
                                       // throw new ApplicationException("Article Type is invalid  in 'ARTICLE Data' sheet at row no " + rowCntr);
                                          ErrorMsg = "Article Type is invalid  in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                }
                                break;

                            case "MATERIAL TYPE":
                                if (!string.IsNullOrEmpty(articleType))
                                {
                                    if (!string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                    {
                                        materialType = articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                        var meterialTypeResult = Context.MstMaterialType.Where(x => x.MaterialTypeCode == materialType).FirstOrDefault();

                                        if (meterialTypeResult == null)
                                        {
                                            materialType = string.Empty;
                                            result = false;
                                           // throw new ApplicationException("Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr);
                                             ErrorMsg = "Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr;
                                        }
                                        else
                                        {
                                            //if ((articleType == "BULK" && materialType != "MTMT")
                                            //    || ((articleType == "COMBO" || articleType == "KIT") && materialType != "FDPT")
                                            //    || (articleType == "SERVICE"))
                                            //{
                                            //    materialType = string.Empty;
                                            //    result = false;
                                            // //   throw new ApplicationException("Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr);
                                            //       ErrorMsg = "Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr;
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        if ((articleType == "SERVICE"))
                                        {
                                            materialType = string.Empty;
                                            result = false;
                                         //   throw new ApplicationException("Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr);
                                            ErrorMsg = "Meterial Type is invalid 'ARTICLE Data' sheet at row no " + rowCntr;
                                        }
                                    }

                                }
                                break;
                            case "MASTER ARTICLE":
                                articleType = articleType.ToUpper().Trim();
                                materialType = materialType.ToUpper().Trim();
                                string masterArticleCode = articleDetails[rowCntr, colIndex].Value.ToString();
                                string[] existingMaterialType = new string[] { "LOOSE", "REPACKED", "RWMT" };
                                if (!string.IsNullOrEmpty(materialType) && existingMaterialType.Contains(materialType))
                                {
                                    if (!string.IsNullOrEmpty(masterArticleCode))
                                    {
                                        var masterArticleResult = Context.MstArticle.Where(x => x.ArticleCode == masterArticleCode).FirstOrDefault();
                                        if (masterArticleResult == null)
                                        {
                                            result = false;
                                           // throw new ApplicationException(" Master Article is missing 'Article Data' sheet at row no " + rowCntr);
                                            ErrorMsg = " Master Article is not Present In System 'Article Data' sheet at row no " + rowCntr;
                                        }
                                    }
                                    else
                                    {
                                        result = false;
                                        //throw new ApplicationException(" Master Article is missing 'Article Data' sheet at row no " + rowCntr);
                                        ErrorMsg = "Master Article is missing 'Article Data' sheet at row no " + rowCntr;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(materialType) && !string.IsNullOrEmpty(masterArticleCode))
                                    {
                                        result = false;
                                      //  throw new ApplicationException(" Master Article is missing 'Article Data' sheet at row no " + rowCntr);
                                        ErrorMsg = "Master Article is missing 'Article Data' sheet at row no " + rowCntr;

                                    }
                                }
                                break;

                            case "ARTICLE HIERARCHY":
                                String treeCode = string.Empty;
                                articleHierarchy = articleDetails[rowCntr, colIndex].Value.ToString();
                                if (!string.IsNullOrEmpty(articleHierarchy))
                                {
                                    articleHierarchy = articleHierarchy.Replace(".0", "");
                                    actualLastNode = decodeArticleHierarchy(articleHierarchy);
                                    if (actualLastNode == null)
                                    {
                                        result = false;
                                      //  throw new ApplicationException(" Article HIERARCHY is invalid  'Article Data' sheet at row no " + rowCntr);
                                        ErrorMsg = "Article HIERARCHY is invalid  'Article Data' sheet at row no " + rowCntr;
                                    }
                                }
                                break;
                            case "PARENT ARTICLE NODE":
                                parentArticleNode = articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                if (string.IsNullOrEmpty(parentArticleNode))
                                {
                                    result = false;
                                   // throw new ApplicationException(" Parent Article Node in 'Article Data' sheet at row no " + rowCntr);
                                    ErrorMsg = "Parent Article Node in 'Article Data' sheet at row no " + rowCntr;
                                }
                                else
                                {
                                    if (articleHierarchy.Trim().Length > 0 && articleHierarchy.Contains("."))
                                    {
                                        String[] nodeNames = articleHierarchy.Split(new string[] { "\\." }, StringSplitOptions.None);
                                        List<String> nodeList = nodeNames.ToList();

                                        if (nodeList.Count > 2)
                                        {
                                            String parentNode = nodeList[nodeList.Count - 2];
                                            if (!string.IsNullOrEmpty(parentNode) && parentArticleNode != parentNode)
                                            {
                                                result = false;
                                                // throw new ApplicationException(" Parent Article Node in 'Article Data' sheet at row no " + rowCntr);
                                                ErrorMsg = " Parent Article Node in 'Article Data' sheet at row no " + rowCntr;
                                            }
                                        }
                                    }
                                }

                                break;
                            case "LAST NODE":
                                lastNode = articleDetails[rowCntr, colIndex].Value.ToString().Trim();
                                if (string.IsNullOrEmpty(lastNode))
                                {
                                    result = false;
                                 //   throw new ApplicationException(" Last Node in 'Article Data' sheet at row no " + rowCntr);
                                          ErrorMsg = "  Last Node in 'Article Data' sheet at row no " + rowCntr;
                                }
                                else
                                {
                                    if (articleHierarchy.Trim().Length > 0 && articleHierarchy.Contains("."))
                                    {
                                        String[] nodeNames = articleHierarchy.Split(new string[] { "\\." }, StringSplitOptions.None);
                                        List<String> nodeList = nodeNames.ToList();

                                        if (nodeList.Count > 2)
                                        {
                                            String childNode = nodeList[nodeList.Count - 1];
                                            if (!string.IsNullOrEmpty(childNode) && lastNode != childNode)
                                            {
                                                result = false;
                                              //  throw new ApplicationException(" Parent Article Node in 'Article Data' sheet at row no " + rowCntr);
                                                ErrorMsg = "Parent Article Node in 'Article Data' sheet at row no " + rowCntr;
                                            }
                                        }
                                    }
                                }

                                break;

                            //case "BARCODE":
                            //    //if (ISDynamicDenereted != true)
                            //    //{
                            //        string barCode = articleDetails[rowCntr, colIndex].Value.ToString();  //vipin
                            //        if(barCode !="")
                            //        {
                            //        var barCodeResult = Context.SalesInfoRecord.Where(r => r.ArticleCode == articleCode && r.SiteCode == siteCode && r.EAN == barCode).FirstOrDefault();
                            //        if (barCodeResult == null)
                            //        {
                            //            result = false;
                            //            //   throw new ApplicationException("BarCode is Missing in 'Article Data' sheet at row no " + rowCntr);
                            //            ErrorMsg = "BarCode is Invalid in 'Article Data' sheet at row no " + rowCntr;
                            //        }
                            //        }
                            //   // }
                            //    break;

                            case "BARCODE":

                                string barCode = articleDetails[rowCntr, colIndex].Value.ToString();  //vipin on 14-04-2017 as Barcode is not generating Dyanamically
                                if (barCode == "")
                                {
                                
                                        result = false;
                                        ErrorMsg = "BarCode is Invalid in 'Article Data' sheet at row no " + rowCntr;
                                }
                                break;

                            case "BARCODE TYPE":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    result = false;
                                   // throw new ApplicationException("BarCode Type is Missing in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg ="BarCode Type is Missing in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }

                                if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim().Contains("UPC") && articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() == "EAN 13")
                                {
                                    result = false;
                                   // throw new ApplicationException("BarCode Type is not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg ="BarCode Type is not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;

                            case "DEFAULT BARCODE":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    result = false;
                                    // throw new ApplicationException("Default Barcode is Missing in 'ARTICLE Data' sheet at row no " + rowCntr);
                                     ErrorMsg ="Default Barcode is Missing in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }

                                if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES" && articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "NO")
                                {
                                    result = false;
                                  //  throw new ApplicationException("Default Barcode not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                       ErrorMsg ="Default Barcode not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;

                            case "STOREID":
                             //   siteCode = articleDetails[rowCntr, colIndex].Value.ToString();
                                var tempStore = Context.MstSite.Where(x => x.SiteCode == siteCode).FirstOrDefault();
                                if ((tempStore == null))
                                {
                                    result = false;
                                 //   throw new ApplicationException("StoreID already exist in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg ="StoreID already exist in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;


                            case "SUPPLIER CODE":
                                if (!string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    string supplierCode = articleDetails[rowCntr, colIndex].Value.ToString();
                                    var supplier = (from p in Context.SiteSupplierMap where p.SupplierCode == supplierCode && p.SiteCode == siteCode select p).FirstOrDefault();
                                    if (supplier == null)
                                    {
                                        result = false;
                                       // throw new ApplicationException(" Supplier Code not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg ="Supplier Code not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                }
                                break;

                            case "LANGUAGE CODE":
                                string langCode = articleDetails[rowCntr, colIndex].Value.ToString();
                                var templangCodes = Context.MstLanguage.Where(x => x.LanguageCode == langCode).FirstOrDefault();
                                if ((templangCodes == null))
                                {
                                    result = false;
                                   // throw new ApplicationException("LANGUAGE CODE not exist in system exist in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg ="LANGUAGE CODE not exist in system exist in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;
                            case "IMAGE":
                                if (articleDetails[rowCntr, colIndex].Value.ToString().Length > 2)
                                {

                                    string searchFormat = articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim().Split('.')[2]; //vipin
                                    string[] ImageFormat = new string[] { "JPEG", "PNG", "JPG", "GIF" };
                                    if (!ImageFormat.Contains(searchFormat))
                                    {
                                        result = false;
                                      //  throw new ApplicationException("IMAGE is not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg ="IMAGE is not valid in 'ARTICLE Data' sheet at row no " + rowCntr;

                                    }
                                }
                                break;
                            case "BASE UOM":
                            case "ORDER UOM":
                            case "SALE UOM":
                            case "CONSUMPTION UOM":
                            case "NET WEIGHT UOM":
                            case "GROSS WEIGHT UOM":
                                string uom = articleDetails[rowCntr, colIndex].Value.ToString();
                                if (string.IsNullOrEmpty(uom))
                                {
                                    if (new[] { "BASE UOM", "SALE UOM", "CONSUMPTION UOM", "ORDER UOM" }.Any(n => switchCaseValue.Contains(n)))
                                    {
                                        result = false;
                                       // throw new ApplicationException(switchCaseValue + " is missing in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg ="IMAGE is not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                }
                                else
                                {
                                    var uomResult = Context.MstUoM.Where(x => x.UOMCode == uom).FirstOrDefault();
                                    if (uomResult == null)
                                    {
                                        result = false;
                                       // throw new ApplicationException(switchCaseValue + " is not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                        ErrorMsg =switchCaseValue + " is not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                }
                                break;

                            case "OUOM VALUE":
                            case "SALE VALUE":
                            case "COST PRICE":
                            case "SELLING PRICE":
                            case "CONSUMPTION VALUE":
                            case "MRP":
                            case "MAP":
                            case "NET WEIGHT":
                            case "GROSS WEIGHT":
                           // case "OPEN SELLING PRICE":  ##vipin
                            case "REORDER QTY":
                            case "SAFETY QTY":
                                if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                                {
                                    if (!new[] { "NET WEIGHT", "GROSS WEIGHT", "SAFETY QTY", "REORDER QTY" }.Any(n => switchCaseValue.Contains(n)))
                                    {
                                        result = false;
                                      //  throw new ApplicationException(switchCaseValue + " is missing in 'ARTICLE Data' sheet at row no " + rowCntr);
                                         ErrorMsg =switchCaseValue + " is missing in 'ARTICLE Data' sheet at row no " + rowCntr;
                                    }
                                }
                                else
                                {
                                    if (!IsNumeric(articleDetails[rowCntr, colIndex].Value.ToString()))
                                    {
                                       // new ApplicationException(switchCaseValue + " is not in numeric Format: 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                           ErrorMsg =switchCaseValue + " is not in numeric Format: 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;
                                        result = false;
                                    }
                                }
                                break;

                            case "EXPIRY":
                            case "SALEABLE":
                            case "PRINTABLE":
                            case "DEFAULT SUPPLIER":
                            case "OPEN QTY":
                                if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES" && articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "NO")
                                {
                                    result = false;
                                    //throw new ApplicationException(articleDetails.GetCell(xlscolumnRow, colIndex).Value.ToString() + "not valid in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                    ErrorMsg = articleDetails.GetCell(xlscolumnRow, colIndex).Value.ToString() + "not valid in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                                }
                                break;

                            case "STATUS":
                                if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "ACTIVE" && articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "INACTIVE")
                                {
                                    result = false;
                                  //  throw new ApplicationException("Status not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg ="Status not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;

                            case "Supplier Status":    // vipin
                                if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "ACTIVE" && articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "INACTIVE")
                                {
                                    result = false;
                                    //  throw new ApplicationException("Status not valid in 'ARTICLE Data' sheet at row no " + rowCntr);
                                    ErrorMsg = "Supplier Status not valid in 'ARTICLE Data' sheet at row no " + rowCntr;
                                }
                                break;


                            default:
                                break;
                        }
                    }
                    result = true;
                }
                ErrorMsgOut = ErrorMsg;
                ErrorMsg = "";
            }
            catch (Exception ex)
            {
             //   throw ex;
            }
            return result;
        }


        public MstArticleNode decodeArticleHierarchy(string hierarchy)
        {
            MstArticleNode foundNode = null;
            try
            {
                if (hierarchy.Length > 0)
                {
                    if (hierarchy.Contains("."))
                    {
                        String[] nodeNames = hierarchy.Split(new string[] { "." }, StringSplitOptions.None);

                        List<String> nodeList = nodeNames.ToList();

                        if (nodeList.Count > 1)
                        {
                            string nodeList0 = nodeList[0];
                            var tree = Context.MstArticleTree.Where(x => x.TreeName == nodeList0 && x.STATUS == true).FirstOrDefault();

                            if (tree != null)
                            {
                                //var nodes = Context.MstArticleNode.Where(x=>x.NodeName == nodeList[nodeList.Count -1] && x.Treecode == tree.TreeCode && x.STATUS ==  true ).ToList(); 
                                string lastNode = nodeList[nodeList.Count - 1];
                                var nodes = (from x in Context.MstArticleNode
                                             where x.NodeName == lastNode && x.Treecode == tree.TreeCode && x.STATUS == true
                                             select x).ToList();

                                foreach (var node in nodes)
                                {
                                    string nodehierarchy = articleHierarchyString(node.Nodecode, true);
                                    if (nodehierarchy == hierarchy)
                                    {
                                        foundNode = node;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
              //  throw e;
            }
            return foundNode;
        }

        public String articleHierarchyString(string treeNodeMap, bool firstentry)
        {
            StringBuilder hierarchy = new StringBuilder();
            try
            {

                //MstArticleNode node = (MstArticleNode)em.createQuery("from MstArticleNode node where node.nodecode=:nodecode and status=:status")
                //                    .setParameter("nodecode", treeNodeMap)
                //                    .setParameter("status", true).getSingleResult();

                MstArticleNode node = Context.MstArticleNode.Where(x => x.Nodecode == treeNodeMap && x.STATUS == true).FirstOrDefault();

                if (firstentry)
                {
                    //hierarchy.insert(0, node.getNodeName());
                    hierarchy.Insert(0, node.NodeName);
                }
                else
                {
                    //hierarchy.insert(0, node.getNodeName() + ".");
                    hierarchy.Insert(0, node.NodeName + ".");
                }

                //ArticleTreeNodeMap nodeMap = (ArticleTreeNodeMap)em.createQuery("from ArticleTreeNodeMap where nodecode=:nodecode and status=:status")
                //                    .setParameter("nodecode", treeNodeMap).setParameter("status", true).getSingleResult();

                ArticleTreeNodeMap nodeMap = Context.ArticleTreeNodeMap.Where(x => x.Nodecode == treeNodeMap && x.STATUS == true).FirstOrDefault();

                if (nodeMap.ParentNodecode != null && !string.IsNullOrEmpty(nodeMap.ParentNodecode))
                {
                    hierarchy.Insert(0, articleHierarchyString(nodeMap.ParentNodecode, false));
                }
                else
                {
                    hierarchy.Insert(0, node.MstArticleTree.TreeName + ".");
                }
            }
            catch (Exception e)
            {
                hierarchy = new StringBuilder("(invalid)");
                return hierarchy.ToString();
            }
            return hierarchy.ToString();
        }


        private bool ArtilcePurchaseDetailsValidations(ref XLSheet purchaseDetails, ref bool result, ref  string ErrorMsgThrow) //vipin
        {
            string siteCode = string.Empty;
            string articleCode = string.Empty;
            for (int rowCntr = 1; rowCntr < purchaseDetails.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < purchaseDetails.Columns.Count; colIndex++)
                {
                    switch (purchaseDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "ARTICLE CODE":
                            articleCode = purchaseDetails[rowCntr, colIndex].Value.ToString();
                            if (string.IsNullOrEmpty(purchaseDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                                //throw new ApplicationException("please provide Article Code in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article Code in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;

                            }
                            break;

                        case "ARTICLE NAME":
                            if (string.IsNullOrEmpty(purchaseDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                               // throw new ApplicationException("please provide Article Name in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article Name in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;

                            }
                            break;

                        case "ORDER UOM":
                            string uomCode = purchaseDetails[rowCntr, colIndex].Value.ToString();
                            var uomResult = Context.ArticleUOM.Where(x => x.UOMCode == uomCode && x.ArticleCode == articleCode).FirstOrDefault();
                            if ((uomResult == null))
                            {
                                result = false;
                              //  throw new ApplicationException("UOM not exists in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                ErrorMsg = "UOM not exists in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;

                            }
                            break;

                        case "ORDER VALUE":

                            if (!IsNumeric(purchaseDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                             //   throw new ApplicationException("ORDER VALUE in numeric Format: 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                ErrorMsg = "ORDER VALUE in numeric Format: 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;

                            }
                            break;
                        case "STATUS":
                            if (purchaseDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES" && purchaseDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "NO")
                            {
                                result = false;
                               // throw new ApplicationException(" Status not valid in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr);
                                ErrorMsg = "Status not valid in 'ARTICLE Purchase UOM' sheet at row no " + rowCntr;
                            }

                            break;

                        default:


                            break;
                    }


                }

            }
            return result;
        }

        private bool ArtilceBarCodeValidations(ref XLSheet barCodeDetails, ref bool result, ref string  ErrorMsgThrow) //vipin
        {
            string siteCode = string.Empty;
            string articleCode = string.Empty;
            for (int rowCntr = 1; rowCntr < barCodeDetails.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < barCodeDetails.Columns.Count; colIndex++)
                {
                    switch (barCodeDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "STOREID":
                            siteCode = barCodeDetails[rowCntr, colIndex].Value.ToString();
                            var storeIDList = Context.MstSite.Where(t => t.SiteCode == siteCode).FirstOrDefault();
                            if (storeIDList == null)
                            {
                                result = false;
                           //     throw new ApplicationException("StoreID not exist in system : 'Article Bar Code' sheet at row no " + rowCntr);
                                ErrorMsg = "StoreID not exist in system : 'Article Bar Code' sheet at row no " + rowCntr;
                            }
                            break;

                        case "ARTICLE CODE":
                            articleCode = barCodeDetails[rowCntr, colIndex].Value.ToString();
                            if (string.IsNullOrEmpty(barCodeDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                               // throw new ApplicationException("please provide Article Code in 'Article Bar Code' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article Code in 'Article Bar Code' sheet at row no " + rowCntr;
                            }
                            break;

                        case "BARCODE":
                            string barCode = barCodeDetails[rowCntr, colIndex].Value.ToString();

                            var barCodeResult = Context.SalesInfoRecord.Where(r => r.ArticleCode == articleCode && r.SiteCode == siteCode && r.EAN == barCode).FirstOrDefault();

                            if (barCodeResult == null)
                            {
                                result = false;
                              //  throw new ApplicationException("please provide Article BarCode in 'Article BarCode' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article BarCode in 'Article BarCode' sheet at row no " + rowCntr;
                            }
                            break;

                        case "SELLING PRICE":
                            string spPrice = barCodeDetails[rowCntr, colIndex].Value.ToString();
                            if (!IsNumeric(spPrice))
                            {
                                result = false;
                               // throw new ApplicationException("BarCode already exist in 'Article Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "BarCode already exist in 'Article Tax' sheet at row no " + rowCntr;
                            }
                            break;

                        default:
                            break;
                    }


                }


            }
            return result;
        }

        private bool ArtilceTaxValidations(ref XLSheet taxDetails, ref bool result,ref  string ErrorMsgThrow) //vipin
        {
            string siteCode = string.Empty;
            for (int rowCntr = 1; rowCntr < taxDetails.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < taxDetails.Columns.Count; colIndex++)
                {
                    switch (taxDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "ARTICLE CODE":
                            if (string.IsNullOrEmpty(taxDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                                //throw new ApplicationException("Please provide Article Code in 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "Please provide Article Code in 'ARTICLE Tax' sheet at row no " + rowCntr;
                            }
                            break;
                        case "ARTICLE NAME":
                            if (string.IsNullOrEmpty(taxDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                               // throw new ApplicationException("Please provide Article Name in 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "Please provide Article Name in 'ARTICLE Tax' sheet at row no " + rowCntr;
                            }
                            break;
                        case "STOREID":
                            siteCode = taxDetails[rowCntr, colIndex].Value.ToString();
                            var tempStore = Context.MstSite.Where(x => x.SiteCode == siteCode).FirstOrDefault();
                            if ((tempStore == null))
                            {
                                result = false;
                             //   throw new ApplicationException("StoreID already exist in 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "StoreID already exist in 'ARTICLE Tax' sheet at row no " + rowCntr;
                            }
                            break;
                        case "TAX NAME":
                            string taxName = taxDetails[rowCntr, colIndex].Value.ToString();

                            var tempTaxName = (from STM in Context.TaxSiteMapping
                                               join MT in Context.MstTax
                                                   on STM.Taxcode equals MT.TaxCode
                                               where MT.TaxName == taxName && STM.Sitecode == siteCode
                                               select MT).FirstOrDefault();

                            if (tempTaxName == null)
                            {
                                result = false;
                             //   throw new ApplicationException("Tax Name not exist in system : 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "Tax Name not exist in system : 'ARTICLE Tax' sheet at row no " + rowCntr;
                            }
                            break;

                        case "TAX CODE":
                            string taxCode = taxDetails[rowCntr, colIndex].Value.ToString();

                            var tempTaxcode = (from STM in Context.TaxSiteMapping
                                               join MT in Context.MstTax
                                                   on STM.Taxcode equals MT.TaxCode
                                               where MT.TaxCode == taxCode && STM.Sitecode == siteCode
                                               select MT).FirstOrDefault();

                            if (tempTaxcode == null)
                            {
                                result = false;
                              //  throw new ApplicationException("Tax Name not exist in system : 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "Tax Name not exist in system : 'ARTICLE Tax' sheet at row no " + rowCntr;
                            }
                            break;

                        case "TAX STATUS":
                            if (taxDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES" && taxDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "NO")
                            {
                                result = false;
                              //  throw new ApplicationException("Tax Status not valid in 'ARTICLE Tax' sheet at row no " + rowCntr);
                                ErrorMsg = "Tax Status not valid in 'ARTICLE Tax' sheet at row no " + rowCntr;

                            }
                            break;

                        case "SUPPLIER CODE":
                            if (!string.IsNullOrEmpty(taxDetails[rowCntr, colIndex].Value.ToString()) && taxDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "INTERNAL")
                            {
                                string supplierCode = taxDetails[rowCntr, colIndex].Value.ToString();
                                var supplier = (from p in Context.SiteSupplierMap where p.SupplierCode == supplierCode && p.SiteCode == siteCode select p).FirstOrDefault();
                                if (supplier == null)
                                {
                                    result = false;
                                  //  throw new ApplicationException(" Supplier Code not valid in 'ARTICLE Tax' sheet at row no " + rowCntr);
                                    ErrorMsg = "Supplier Code not valid in 'ARTICLE Tax' sheet at row no " + rowCntr;
                                }
                            }
                            break;

                        default:
                            break;
                    }


                }
            }
            return result;
        }

        private bool ArtilceCharactersticsValidations(ref XLSheet charDetails, ref bool result,ref  string ErrorMsgThrow) //vipin
        {
         
            for (int rowCntr = 1; rowCntr < charDetails.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < charDetails.Columns.Count; colIndex++)
                {
                    switch (charDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "ARTICLE CODE":
                            if (string.IsNullOrEmpty(charDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                                //throw new ApplicationException("please provide Article Code in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article Code in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                            }
                            break;

                        case "ARTICLE NAME":
                            if (string.IsNullOrEmpty(charDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                result = false;
                             //   throw new ApplicationException("please provide Article Name in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "please provide Article Name in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                            }
                            break;

                        case "BARCODE":
                            // var temp = listEANModel.Where(x => x.EAN == charDetails[rowCntr, colIndex].Value.ToString()).ToList();
                            string EANValue = charDetails[rowCntr, colIndex].Value.ToString();
                            var temp = Context.MstEAN.Where(x => x.EAN == EANValue).FirstOrDefault();
                            if ((temp != null))
                            {
                                result = false;
                              //  throw new ApplicationException("Barcode already exist in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "Barcode already exist in 'ARTICLE CHAR' sheet at row no " + rowCntr;

                            }
                            break;

                        case "PROFILE":
                            if (defaultProfileName != charDetails[rowCntr, colIndex].Value.ToString())
                            {
                                result = false;
                              //  throw new ApplicationException("Barcode not exist in system : 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "Barcode already exist in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                            }
                            break;
                        case "CHAR ID":
                            string charValue = charDetails[rowCntr, colIndex].Value.ToString();
                            var charList = Context.MstCharacteristics.Where(t => t.CharCode == charValue).FirstOrDefault();
                            if ((charList != null))
                            {
                                result = false;
                              //  throw new ApplicationException("Characterstics already exist in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "Characterstics already exist in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                            }
                            break;

                        case "CHAR STATUS":
                            if (charDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES" && charDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "NO")
                            {
                                result = false;
                               // throw new ApplicationException("Characterstics Status not valid in 'ARTICLE CHAR' sheet at row no " + rowCntr);
                                ErrorMsg = "Characterstics Status not valid in 'ARTICLE CHAR' sheet at row no " + rowCntr;
                            }

                            break;

                        default:
                            break;
                    }
                }
            }
            return result;
        }

        #endregion

        #endregion

        //public bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails, ref string ErrorMsg) //vipin
        //{
           
        //}

        public bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails, ref string  ErrorMsg)
        {
            bool result = false;
            try
            {
                //char - Details
                if (charDetails != null) // code added by khusrao adil
                {
                    ArtilceCharactersticsValidations(ref charDetails, ref result, ref ErrorMsgThrow);
                }
                //taxDetails
                if (taxDetails != null) // code added by khusrao adil
                {
                    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                    {
                        result = ArtilceTaxValidations(ref taxDetails, ref result, ref ErrorMsgThrow);
                    }
                }
                //barCodeDetails 
                if (barCodeDetails != null) // code added by khusrao adil
                {

                    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                    {
                        result = ArtilceBarCodeValidations(ref barCodeDetails, ref result, ref ErrorMsgThrow);
                    }
                }
                //purchaseDetails
                if (purchaseDetails != null)
                    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                    {
                        result = ArtilcePurchaseDetailsValidations(ref purchaseDetails, ref result, ref ErrorMsgThrow);
                    }
                //Article - Details 
                if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                {
                    result = ArtilceDetailsValidations(ref articleDetails, ref result, ref ErrorMsgThrow);
                }
                ErrorMsg = ErrorMsgThrow; //vipin
                ErrorMsgThrow = "";
            }
            catch (Exception ex)
            {
              //  throw ex;
            }
            return result;
            //return ErrorMsg; //vipin
        }
    }
}


#region "LINQ"

//SELECT
//Categories.[Name] AS [CatName],
//Products.[Name] AS [ProductName],
//OrderLines.[Price] AS [Price]
//FROM [Categories] AS Categories
//LEFT OUTER JOIN Products ON Categories.Id = Products.Category_Id
//LEFT OUTER JOIN OrderLines ON Products.Id = OrderLines.Product_Id

//Linq will be -
//var Records = from Cats in Context.Categories
//join Prods in Context.Products on Cats.Id equals Prods.Category_Id into CP
//from CPs in CP.DefaultIfEmpty()
//join Ords in Context.OrderLines on CPs.Id equals Ords.Product_Id into PO
//from POs in PO.DefaultIfEmpty()
//select new { CatName = Cats.Name, ProdName = CPs.Name, OrderAmount = POs.Price };

//Lambda Will be -

//var recs = Context.Categories.GroupJoin(Context.Products,c=>c.Id,p=>p.Category_Id,(c,p) => new {c,p})
//.SelectMany(c=>c.p.DefaultIfEmpty(),(c,p) => new {c.c,p})
//.GroupJoin(Context.OrderLines,cp=>cp.p.Id ,o=>o.Product_Id,(cp,o)=>new {cp,o})
//.SelectMany(cpo=>cpo.o.DefaultIfEmpty(),(cpo,o) =>new { CatName = cpo.cp.c.Name, ProdName = cpo.cp.p.Name, OrderAmount =o.Price})

#endregion



