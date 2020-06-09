using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Spectrum.Models;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using AutoMapper;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL.CustomEntities;
using C1.C1Excel;
using System.Text.RegularExpressions;
using Spectrum.Logging;   // added by vipin on 03-4-2017
namespace Spectrum.BL
{
    public class ArticleManager : GenericManager<ArticleModel>, IArticleManager
    {
        const int xlscolumnRow = 0;

        public ArticleManager()
        {
            this.articleRepository = new ArticleRepository();
            this.commonRepository = new CommonRepository();
            this.articleRepository.defaultProfileName = defaultProfileName;

        }

        private IArticleRepository articleRepository;
        private ICommonRepository commonRepository;

        public string defaultProfileName { get; set; }

        /// <summary>
        /// Add article to the Database : if article code does not exist , create a 15 digit article code 
        /// </summary>
        /// <param name="articleModel"></param>
        /// <returns>Sucuss/Fail</returns>
        public bool SaveArticle(ArticleModel articleModel, List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel, ArticleImageModel articleImageModel,
                                   List<EANModel> eanModel, List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel,
                                   MasterArticleMapModel masterArticleMapModel, List<DropDownModel> rightSupplierList, List<DropDownModel> leftSupplierList, ref string autoArticleCode, ref string LastSupllierAddRemoved)    //code changed by irfan for spectrum lite issus on 09/11/2017
                                                                      
        {
            try
            {
                bool tranResult = false;
                bool isNewArticle = true;

                if (articleModel.IsAutoNumber)
                {
                    int nextNo = commonRepository.GetNextID(CommonModel.SiteCode, "AC");
                    // ... Format Article code into 15 digit ...
                   
                    var sitecode = CommonModel.SiteCode;

                    sitecode = (sitecode.Length > 3) ? sitecode.Substring(sitecode.Length - 3, 3) : sitecode;

                    articleModel.ArticleCode = string.Format("0{0}", sitecode + nextNo.ToString().PadLeft(7, '0')); 
                    //articleModel.ArticleCode = string.Format("0000{0}", nextNo.ToString().PadLeft(11, '0'));
                    autoArticleCode = articleModel.ArticleCode;
                }
                Mapper.DynamicMap(new ArticleModel(), new MstArticle());
                Mapper.DynamicMap(new MstArticle(), new ArticleModel());
                articleModel.ToAddOrModifyEntity(true);
                var article = Mapper.Map(articleModel, new MstArticle());

                MstArticle articleExist = articleRepository.GetArticleByID(autoArticleCode);
                if (articleExist != null)
                {
                    isNewArticle = false;
                }

                // Image 
                if (articleImageModel != null)
                {
                    articleImageModel.ArticleCode = autoArticleCode;
                    articleImageModel.ToAddOrModifyEntity(true);
                }
                Mapper.DynamicMap(new ArticleImageModel(), new MstArticleImage());
                Mapper.DynamicMap(new MstArticleImage(), new ArticleImageModel());
                var articleImage = Mapper.Map(articleImageModel, new MstArticleImage());
                article.MstArticleImage = articleImage;

                // siteArticleTaxMapping

                for (int rowTax = 0; rowTax < siteArticleTaxMappingModel.Count; rowTax++)
                {
                    siteArticleTaxMappingModel[rowTax].ArticleCode = autoArticleCode;
                    siteArticleTaxMappingModel[rowTax].SiteCode = CommonModel.SiteCode;
                    siteArticleTaxMappingModel[rowTax].ToAddOrModifyEntity(true);
                }
                Mapper.DynamicMap(new SiteArticleTaxMappingModel(), new SiteArticleTaxMapping());
                Mapper.DynamicMap(new SiteArticleTaxMapping(), new SiteArticleTaxMappingModel());
                List<SiteArticleTaxMapping> siteArticleTaxMappinglist = (from p in siteArticleTaxMappingModel select Mapper.Map(p, new SiteArticleTaxMapping())).ToList();
                article.SiteArticleTaxMapping = siteArticleTaxMappinglist;

                // EAN 
                int EanNextNo = 0;
                int EANAutoCount = 0;
                for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                {
                    eanModel[rowEan].ArticleCode = autoArticleCode;
                    if (eanModel[rowEan].IsAutoNumber)
                    {
                        EANAutoCount = EANAutoCount + 1;
                        if (EanNextNo == 0)
                        {
                            EanNextNo = commonRepository.GetNextID(CommonModel.SiteCode, "EC");
                        }
                        else
                        {
                            EanNextNo = EanNextNo + 1;
                        }
                        if (eanModel[rowEan].Discription.ToUpper().Contains("EAN"))
                        {
                            //---    Format EAN into 13 digit ...
                            eanModel[rowEan].EAN = string.Format("0000{0}", EanNextNo.ToString().PadLeft(9, '0'));
                        }
                        else
                        {
                            //---    Format EAN into 12 digit ...
                            eanModel[rowEan].EAN = string.Format("0000{0}", EanNextNo.ToString().PadLeft(8, '0'));
                        }
                    }
                    eanModel[rowEan].UOMCode = article.BaseUnitofMeasure;
                    eanModel[rowEan].ToAddOrModifyEntity(true);
                }
                Mapper.DynamicMap(new EANModel(), new MstEAN());
                Mapper.DynamicMap(new MstEAN(), new EANModel());
                List<MstEAN> mstEANlist = (from p in eanModel select Mapper.Map(p, new MstEAN())).ToList();
                article.MstEAN = mstEANlist;

                //---  SalesInfoRecord ---
                List<SalesInfoRecord> salesInfoRecordList = new List<SalesInfoRecord>();
                for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                {
                    SalesInfoRecord salesInfoRecord = new SalesInfoRecord();
                    salesInfoRecord.ArticleCode = autoArticleCode;
                    salesInfoRecord.SrNo = rowEan + 1;
                    salesInfoRecord.EAN = eanModel[rowEan].EAN.ToString();
                    salesInfoRecord.SellingPrice = articleModel.SellPrice;

                    salesInfoRecord.SiteCode = CommonModel.SiteCode;
                    salesInfoRecord.FromDate = CommonModel.CurrentDate;
                    salesInfoRecord.FreezeSB = false;
                    salesInfoRecord.FreezeSR = false;
                    salesInfoRecord.FreezeIN = false;
                    salesInfoRecord.FreezeOB = false;
                    salesInfoRecord.ToAddOrModifyEntity(true);
                    salesInfoRecordList.Add(salesInfoRecord);
                }

                ////...  PurchaseInfoRecord
                List<PurchaseInfoRecord> purchaseInfoRecordlist = new List<PurchaseInfoRecord>();
                // for single active supplier for each article------------------------------------------------
                ////if (rightSupplierList.Count == 1)
                ////{
                ////    for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                ////    {

                ////        PurchaseInfoRecord purchaseInfoRecord = new PurchaseInfoRecord();
                ////        purchaseInfoRecord.SiteCode = CommonModel.SiteCode;
                ////        purchaseInfoRecord.SRNO = rowEan + 1;
                ////        purchaseInfoRecord.ArticleCode = autoArticleCode;
                ////        purchaseInfoRecord.SupplierCode = rightSupplierList[rowEan].Code;
                ////        purchaseInfoRecord.IsDefaultSupplier = true;
                ////        purchaseInfoRecord.EAN = eanModel[rowEan].EAN.ToString();
                ////        purchaseInfoRecord.CPBaseCurr = articleModel.CostPrice;
                ////        purchaseInfoRecord.CPLocalCurr = articleModel.CostPrice;
                ////        purchaseInfoRecord.MRP = articleModel.MRP;
                ////        purchaseInfoRecord.MAP = articleModel.MRP;
                ////        purchaseInfoRecord.ToAddOrModifyEntity(true);
                ////        purchaseInfoRecordlist.Add(purchaseInfoRecord);
                ////    }
                ////}
                ////else
                ////{
                    for (int ctrSupplier = 0; ctrSupplier < rightSupplierList.Count; ctrSupplier++)
                    {
                        for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                        {
                            PurchaseInfoRecord purchaseInfoRecord = new PurchaseInfoRecord();
                            purchaseInfoRecord.SiteCode = CommonModel.SiteCode;
                            purchaseInfoRecord.SRNO = rowEan + 1;
                            purchaseInfoRecord.ArticleCode = autoArticleCode;
                            purchaseInfoRecord.SupplierCode = rightSupplierList[ctrSupplier].Code;
                            purchaseInfoRecord.IsDefaultSupplier = rightSupplierList[ctrSupplier].Code == LastSupllierAddRemoved ? true : false;
                            //if (rightSupplierList[ctrSupplier].Code == LastSupllierAddRemoved)
                            //    purchaseInfoRecord.IsDefaultSupplier = true;
                            //else
                            //    purchaseInfoRecord.IsDefaultSupplier = false;
                            purchaseInfoRecord.EAN = eanModel[rowEan].EAN.ToString();
                            purchaseInfoRecord.CPBaseCurr = articleModel.CostPrice;
                            purchaseInfoRecord.CPLocalCurr = articleModel.CostPrice;
                            purchaseInfoRecord.MRP = articleModel.MRP;
                            purchaseInfoRecord.MAP = articleModel.MRP;
                            purchaseInfoRecord.ToAddOrModifyEntity(true);
                            purchaseInfoRecordlist.Add(purchaseInfoRecord);
                        }
                    }
                //}

                // code for de activete suplier------------------------------------------------
                //for (int ctrSupplier = 0; ctrSupplier < leftSupplierList.Count; ctrSupplier++)
                //{
                //    for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                //    {
                //        PurchaseInfoRecord purchaseInfoRecord = new PurchaseInfoRecord();
                //        purchaseInfoRecord.SiteCode = CommonModel.SiteCode;
                //        purchaseInfoRecord.SRNO = purchaseInfoRecordlist.Count + 1;
                //        purchaseInfoRecord.ArticleCode = autoArticleCode;
                //        purchaseInfoRecord.SupplierCode = leftSupplierList[ctrSupplier].Code;
                //        purchaseInfoRecord.IsDefaultSupplier = false;
                //        purchaseInfoRecord.EAN = eanModel[rowEan].EAN.ToString();
                //        purchaseInfoRecord.CPBaseCurr = articleModel.CostPrice;
                //        purchaseInfoRecord.CPLocalCurr = articleModel.CostPrice;
                //        purchaseInfoRecord.MRP = articleModel.MRP;
                //        purchaseInfoRecord.MAP = articleModel.MRP;
                //        purchaseInfoRecord.ToAddOrModifyEntity(true);
                //        purchaseInfoRecordlist.Add(purchaseInfoRecord);
                //    }
                //}

                // code for de activete suplier code end----------------------------------------------------------------
                // for single active supplier for each article  code end------------------------------------------------
               
                //// Article Characteristic in ArticleMatrix
                List<ArticleMatrix> articleMatrixlist = new List<ArticleMatrix>();

                ArticleMatrix articleMatrix;
                for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                {
                    for (int rowcharacteristic = 0; rowcharacteristic < articleCharacteristicMatrixModel.Count; rowcharacteristic++)
                    {
                        articleMatrix = new ArticleMatrix();
                        articleMatrix.EanCode = eanModel[rowEan].EAN.ToString();
                        articleMatrix.CharCode = articleCharacteristicMatrixModel[rowcharacteristic].CharCode;
                        articleMatrix.CharValue = articleCharacteristicMatrixModel[rowcharacteristic].CharValue;
                        articleMatrix.CharType = articleCharacteristicMatrixModel[rowcharacteristic].CharType;
                        articleMatrix.ProfileCode = articleCharacteristicMatrixModel[rowcharacteristic].ProfileCode;
                        articleMatrix.RowKey = articleCharacteristicMatrixModel[rowcharacteristic].RowKey;

                        articleMatrix.ToAddOrModifyEntity(true);
                        articleMatrixlist.Add(articleMatrix);
                    }
                }

                // ArticleNodeMap ----
                List<ArticleNodeMap> articleNodeMaplist = new List<ArticleNodeMap>();
                ArticleNodeMap articleNodeMap = new ArticleNodeMap();
                articleNodeMap.ArticleCode = autoArticleCode;
                articleNodeMap.TreeCode = article.TreeID;
                articleNodeMap.LastNodeCode = article.LastNodeCode;
                articleNodeMap.ToAddOrModifyEntity(true);
                articleNodeMaplist.Add(articleNodeMap);
                article.ArticleNodeMap = articleNodeMaplist;


                //// ArticleUOM ----
                ArticleUOM articleUOM = new ArticleUOM();
                articleUOM.ArticleCode = autoArticleCode;
                articleUOM.UOMCode = article.BaseUnitofMeasure;
                articleUOM.defaultUom = false;
                articleUOM.Divisor = (Convert.ToDecimal(article.VolumeUOM));
                articleUOM.IsDivisor = false;
                articleUOM.ToAddOrModifyEntity(true);

                /////==================================================================================
                // --- ARTICLESTOCKBALANCES Table must be add new record if it is Insert Case 
                List<ArticleStockBalances> articleStockBalanceslist = new List<ArticleStockBalances>();
                if (isNewArticle)
                {
                    for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                    {
                        ArticleStockBalances articleStockBalances = new ArticleStockBalances();
                        articleStockBalances.SiteCode = CommonModel.SiteCode;
                        articleStockBalances.ArticleCode = autoArticleCode;
                        articleStockBalances.EAN = eanModel[rowEan].EAN.ToString();
                        articleStockBalances.UOM = article.BaseUnitofMeasure;
                        articleStockBalances.PhysicalQty = 0;
                        articleStockBalances.ReservedQty = 0;
                        articleStockBalances.DamagedQty = 0;
                        articleStockBalances.OnOrderQty = 0;
                        articleStockBalances.InTrasnsitQty = 0;
                        articleStockBalances.NonSaleableQty = 0;
                        articleStockBalances.TotalPhysicalSaleableQty = 0;
                        articleStockBalances.TotalPhysicalNonSaleableQty = 0;
                        articleStockBalances.TotalVirtualNonSaleableQty = 0;
                        articleStockBalances.TotalSaleableQty = 0;
                        articleStockBalances.TotalARSQty = 0;

                        articleStockBalances.ToAddOrModifyEntity(true);
                        articleStockBalanceslist.Add(articleStockBalances);
                    }

                }

                // Article Replenishment
                // Master Article Map 

                List<MasterArticleMap> masterArticleMaplist = new List<MasterArticleMap>();
                if (masterArticleMapModel != null && !string.IsNullOrEmpty(masterArticleMapModel.MasterArticleCode))
                {
                    MasterArticleMap masterArticleMap = new MasterArticleMap();
                    masterArticleMapModel.ToAddOrModifyEntity(true);
                    Mapper.DynamicMap(new MasterArticleMapModel(), new MasterArticleMap());
                    Mapper.DynamicMap(new MasterArticleMap(), new MasterArticleMapModel());
                    masterArticleMap = Mapper.Map(masterArticleMapModel, new MasterArticleMap());
                    masterArticleMaplist.Add(masterArticleMap);
                }

                article.MasterArticleMap = masterArticleMaplist;

                // Article Desc In DiffLang 
                List<ArticleDescInDiffLang> articleDescInDiffLanglist = new List<ArticleDescInDiffLang>();
                ArticleDescInDiffLang articleDescInDiffLang = new ArticleDescInDiffLang();
                articleDescInDiffLang.ArticleCode = autoArticleCode;
                articleDescInDiffLang.ArticleName = article.ArticleName;
                articleDescInDiffLang.ArticleShortName = article.ArticleShortName;
                articleDescInDiffLang.LanguageCode = "EN";
                articleDescInDiffLang.ToAddOrModifyEntity(true);
                articleDescInDiffLanglist.Add(articleDescInDiffLang);

                article.ArticleDescInDiffLang = articleDescInDiffLanglist;

                string SourcePath = string.Empty;
                string TargetPath = string.Empty;
                if (articleImageModel != null)
                {
                    SourcePath = articleImageModel.sourcePath;
                    TargetPath = articleImageModel.targetPath;
                }
                //=======================================================================================

                var articleDALModel = new ArticleDALModel
                {
                    MstArticle = article,
                    MstArticleImage = articleImage,
                    ArticleMatrixList = articleMatrixlist,
                    ArticleUOM = articleUOM,
                    SalesInfoRecordList = salesInfoRecordList,
                    PurchaseInfoRecordList = purchaseInfoRecordlist,
                    ArticleStockBalances = articleStockBalanceslist,
                    SiteCode = CommonModel.SiteCode,
                    IsAutoNumber = articleModel.IsAutoNumber,
                    SourcePath = SourcePath,
                    TargetPath = TargetPath,
                    EANAutoCount = EANAutoCount,
                    ArticleDescInDiffLang = articleDescInDiffLang  //vipin
                };

                tranResult = this.articleRepository.SaveArticle(articleDALModel);
                return tranResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Code is added by irfan for Purchaseinforecord
        public bool UpdateMstArticle(string autoArticleCode, string LastSupllierAddRemoved)
        {
            List<PurchaseInfoRecord> purinfor = this.articleRepository.ArticleById(autoArticleCode).ToList();

            foreach (var item in purinfor)
            {
                if (item.SupplierCode == LastSupllierAddRemoved)
                {
                    item.IsDefaultSupplier = true;
                }
                else
                {
                    item.IsDefaultSupplier = false;
                }
            }
            this.articleRepository.UpdateMst(purinfor);
            return true;
        }



        public bool UpdateArticle(ArticleModel articleModel)
        {
            MstArticle article = this.articleRepository.GetArticleByID(articleModel.ArticleCode);
            bool isStatus = article.STATUS ?? false;
            Mapper.Map(articleModel, article);
            article.STATUS = isStatus;
            return this.articleRepository.UpdateArticle(article);
        }

        public IQueryable<ArticleSearchModel> GetArticleSearchList()
        {
            return this.articleRepository.GetArticleSearchList();
        }

        /// <summary>
        ///  delete article from database of requested article Id ...
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>Sucuss/Fail</returns>

        public bool DeleteByID(string articleID)
        {
            return this.articleRepository.DeleteByID(articleID);
        }

        public ArticleDataModel GetArticleData(string articleCode)
        {
            ArticleDataModel articleDataModel = new ArticleDataModel();
            try
            {
                ////   Article Model
                articleDataModel.articleModel = GetArticleModelByID(articleCode);
                ////   EAN [Included in article]
                articleDataModel.eanModel = GetArticleEAN(articleCode);
                ////   Article Matrix
                articleDataModel.articleCharacteristicMatrixModel = GetArticleCharacteristicMatrixModel(articleCode);
                //////   Purchase Info Record
                articleDataModel.purchaseInfoRecordModelList = GetArticlePurchaseInfoRecordModelList(articleCode);
                ////  Site Article Tax Mapping  ----
                articleDataModel.siteArticleTaxMappingModelList = GetArticleTaxMappingModelList(articleCode);

                return articleDataModel;
            }
            catch (Exception ex)
            {
                Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                throw ex;
            }

        }

        public ArticleModel GetArticleModelByID(string articleID)
        {
            try
            {
                return this.articleRepository.GetArticleModelByID(articleID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private List<SiteArticleTaxMappingModel> GetArticleTaxMappingModelList(string articleCode)
        {
            try
            {
                var siteArticleTaxMappingList = this.articleRepository.GetsiteArticleTaxMappingList(articleCode).ToList();
                return siteArticleTaxMappingList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private List<PurchaseInfoRecordModel> GetArticlePurchaseInfoRecordModelList(string articleCode)
        {
            try
            {
                var purchaseInfoRecordModelList = this.articleRepository.GetArticlePurchaseInfoRecordList(articleCode).ToList();
                return purchaseInfoRecordModelList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private SalesInfoRecordModel GetArticleSalesInfoRecord(string articleCode)
        {
            SalesInfoRecordModel saleRecordInfoModel = new SalesInfoRecordModel();
            try
            {
                //var salesRecordInfo = this.articleRepository.GetArticleSalesInfoRecord(articleCode);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return saleRecordInfoModel; //saleRecordInfoModel ;
        }

        private List<ArticleCharacteristicMatrixModel> GetArticleCharacteristicMatrixModel(string articleCode)
        {
            try
            {
                var articleCharacteristicMatrixList = this.articleRepository.GetArticleCharacteristicMatrixModel(articleCode).ToList();
                var articleCharacteristicMatrixModel = (from r in articleCharacteristicMatrixList select Mapper.Map(r, new ArticleCharacteristicMatrixModel())).ToList();
                return articleCharacteristicMatrixModel.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public ArticleModel GetArticleById(string articleID)
        {
            var article = this.articleRepository.GetArticleByID(articleID);
            return (article != null) ? Mapper.Map(article, new ArticleModel()) : null;
        }

        public IQueryable<EANModel> GetArticleEAN()
        {
            try
            {
                var articleEANList = this.articleRepository.GetArticleEAN().ToList();
                var articleEANModelList = (from r in articleEANList select Mapper.Map(r, new EANModel())).ToList();
                return articleEANModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<EANModel> GetArticleEAN(string articleID)
        {
            try
            {
                var articleEANList = this.articleRepository.GetArticleEAN(articleID).ToList();
                //Mapper.DynamicMap<EANModel, MstEAN>(new EANModel());
                Mapper.DynamicMap(new EANModel(), new MstEAN());
                Mapper.DynamicMap(new MstEAN(), new EANModel());
                var articleEANModelList = (from r in articleEANList select Mapper.Map(r, new EANModel())).ToList();
                return articleEANModelList.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IList<ArticlePurchaseModel> GetArticlePurchaseList(string supplierCode, List<string> articleCodes)
        {
            return this.articleRepository.GetArticlePurchaseList(supplierCode, articleCodes);
        }

        public IList<ArticlePurchaseStockoutModel> GetArticlePurchaseList(string supplierCode)
        {
            return this.articleRepository.GetArticlePurchaseList(supplierCode);
        }

        public ArticleDataExportModel GetArticleExportData(string nodeCode, string siteCode)
        {
            return this.articleRepository.GetArticleExportData(nodeCode, siteCode);
        }
        public ArticleDataExportModel GetArticlesForBarcodeExportData(string nodeCode, string siteCode)
        {
            return this.articleRepository.GetArticlesForBarcodeExportData(nodeCode, siteCode);
        }

        public bool IsBarCodeExist(string eanCode)
        {
            return articleRepository.IsBarCodeExist(eanCode);
        }

        //public bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails)
        //{
        //    bool result = false;
        //    try
        //    {
        //        result = this.articleRepository.ImportExcelValidations(ref  articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
        public bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails, ref string ErrorMsg)  // added by vipin on 03-04-20117
        {
            bool result = false;
            try
            {
                result = this.articleRepository.ImportExcelValidations(ref  articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails, ref ErrorMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        #region "Article Excel Save "

        List<ArticleDALModel> articleExcelDALModel;
        int articleRecordno = 1;
        public bool SaveImportExcel(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails)
        {
            bool tranResult = false;
            int newItemCounter = 0;
            try
            {
                articleExcelDALModel = new List<ArticleDALModel>();
                //result = this.articleRepository.SaveImportExcel(ref articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails);
                string articleCode = string.Empty;
                for (articleRecordno = 1; articleRecordno < articleDetails.Rows.Count; articleRecordno++)
                {
                  //articleCode = 
                    if (articleDetails[articleRecordno, 1].Value != null)
                    {
                        if (articleDetails[articleRecordno, 0].Value != null)
                        {
                            articleCode = articleDetails[articleRecordno, 0].Value.ToString().Trim();
                        }
                        else
                        {
                            articleCode = "";
                        }
                        ArticleModel articleModel = new ArticleModel();
                        List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel = new List<ArticleCharacteristicMatrixModel>();
                        ArticleImageModel articleImageModel = new ArticleImageModel();
                        List<EANModel> eanModel = new List<EANModel>();
                        List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel = new List<SiteArticleTaxMappingModel>();
                        List<DropDownModel> supplierList = new List<DropDownModel>();
                        //     ArticleStockBalanceModel articleStockBalanceModel = new ArticleStockBalanceModel();
                        MasterArticleMapModel masterArticleMapModel = new MasterArticleMapModel();
                        ArticleReplenishmentModel articleReplenishmentModel = new ArticleReplenishmentModel();
                        ArticleDescInDiffLangModel articleDescInDiffLangModel = new ArticleDescInDiffLangModel();

                        //-:-:-:- If It have value then I have to check All Sheet Otherwise Only ItemDetails And Save ...

                        if (string.IsNullOrEmpty(articleCode))
                        {
                            articleModel.IsAutoNumber = true;
                            ArtilceDetailsSave(ref articleDetails, ref articleModel, ref articleImageModel, ref  eanModel,
                                    ref articleReplenishmentModel, ref masterArticleMapModel, ref articleDescInDiffLangModel,
                                    ref supplierList, ref articleRecordno);
                        }
                        else
                        {
                            articleModel.IsAutoNumber = false;

                            ArtilceDetailsSave(ref articleDetails, ref articleModel, ref articleImageModel, ref  eanModel,
                                  ref articleReplenishmentModel, ref masterArticleMapModel, ref articleDescInDiffLangModel,
                                  ref supplierList, ref articleRecordno);

                            ArtilceBarCodeSave(ref barCodeDetails, ref  eanModel, articleCode, ref articleRecordno);
                            ArtilceTaxSave(ref taxDetails, ref siteArticleTaxMappingModel, articleCode, ref articleRecordno);
                            ArtilceCharactersticsSave(ref charDetails, articleCharacteristicMatrixModel, articleCode, ref articleRecordno);

                        }

                        ArticleDALModel articleRowDALModel = GetArticleDALModel(articleModel, articleCharacteristicMatrixModel, articleImageModel, eanModel, siteArticleTaxMappingModel,
                                                            ref articleReplenishmentModel, ref masterArticleMapModel, ref articleDescInDiffLangModel,
                                                            supplierList, ref articleCode, ref newItemCounter);

                        if (articleRowDALModel != null)
                        {
                            articleExcelDALModel.Add(articleRowDALModel);
                        }
                    }
                }
                tranResult = this.articleRepository.SaveImportExcel(ref articleExcelDALModel);
            }
            catch (Exception ex)
            {
               //hrow ex; 
                Logger.Log(ex, Logger.LogingLevel.Error); //ded by vipin on 03-04-20117
            }
            return tranResult;
        }

        public ArticleDALModel GetArticleDALModel(ArticleModel articleModel, List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel,
                           ArticleImageModel articleImageModel, List<EANModel> eanModel, List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel,
                           ref ArticleReplenishmentModel articleReplenishmentModel, ref MasterArticleMapModel masterArticleMapModel, ref ArticleDescInDiffLangModel articleDescInDiffLangModel,
                           List<DropDownModel> rightSupplierList, ref string autoArticleCode, ref int newItemCounter)
        {
            try
            {
                bool isNewArticle = true;
                if (articleModel.IsAutoNumber)
                {
                    int nextNo = commonRepository.GetNextID(CommonModel.SiteCode, "AC") + newItemCounter;
                    // ... Format Article code into 15 digit ...
                    newItemCounter = newItemCounter + 1;
                    articleModel.ArticleCode = string.Format("0000{0}", nextNo.ToString().PadLeft(11, '0'));
                    autoArticleCode = articleModel.ArticleCode;
                    articleModel.ToAddOrModifyEntity(true);
                }
                else
                {
                    articleModel.ToAddOrModifyEntity(false);
                }

                MstArticle articleExist = articleRepository.GetArticleByID(autoArticleCode);
                if (articleExist != null)
                {
                    isNewArticle = false;
                }
                Mapper.DynamicMap(new ArticleModel(), new MstArticle());
                Mapper.DynamicMap(new MstArticle(), new ArticleModel());
                var article = Mapper.Map(articleModel, new MstArticle());

                // Image 
                articleImageModel.ArticleCode = autoArticleCode;
                articleImageModel.ToAddOrModifyEntity(true);
                Mapper.DynamicMap(new ArticleImageModel(), new MstArticleImage());
                Mapper.DynamicMap(new MstArticleImage(), new ArticleImageModel());
                var articleImage = Mapper.Map(articleImageModel, new MstArticleImage());

                article.MstArticleImage = articleImage;

                // siteArticleTaxMapping

                for (int rowTax = 0; rowTax < siteArticleTaxMappingModel.Count; rowTax++)
                {
                    siteArticleTaxMappingModel[rowTax].ArticleCode = autoArticleCode;
                    siteArticleTaxMappingModel[rowTax].SiteCode = CommonModel.SiteCode;
                    siteArticleTaxMappingModel[rowTax].ToAddOrModifyEntity(true);
                }
                Mapper.DynamicMap(new SiteArticleTaxMappingModel(), new SiteArticleTaxMapping());
                Mapper.DynamicMap(new SiteArticleTaxMapping(), new SiteArticleTaxMappingModel());
                List<SiteArticleTaxMapping> siteArticleTaxMappinglist = (from p in siteArticleTaxMappingModel select Mapper.Map(p, new SiteArticleTaxMapping())).ToList();
                article.SiteArticleTaxMapping = siteArticleTaxMappinglist;

                // EAN 
                int EanNextNo = 0;
                int EANAutoCount = 0;
                for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                {
                    eanModel[rowEan].ArticleCode = autoArticleCode;
                    if (eanModel[rowEan].IsAutoNumber)
                    {
                     
                        if (EanNextNo == 0)
                        {
                            EanNextNo = commonRepository.GetNextID(CommonModel.SiteCode, "EC") + (newItemCounter-1);
                        }
                        else
                        {
                            EanNextNo = EanNextNo + 1;
                        }

                        EANAutoCount = EANAutoCount + 1;
                        if (eanModel[rowEan].Discription.ToUpper().Contains("EAN"))
                        {
                            //---    Format EAN into 13 digit ...
                            eanModel[rowEan].EAN = string.Format("0000{0}", EanNextNo.ToString().PadLeft(9, '0'));
                        }
                        else
                        {
                            //---    Format EAN into 12 digit ...
                            eanModel[rowEan].EAN = string.Format("0000{0}", EanNextNo.ToString().PadLeft(8, '0'));
                        }
                        if (articleModel.IsAutoNumber)    // comented by vipin on 19-04-2017
                        {
                            EanNextNo = EanNextNo + newItemCounter;
                        }
                    }
                    else
                    {
                        eanModel[rowEan].EAN = articleModel.BrandCode.ToString().Trim();  // added by vipin on  19=04-2017
                    }
                    eanModel[rowEan].UOMCode = article.BaseUnitofMeasure;
                    eanModel[rowEan].ToAddOrModifyEntity(true);
                }

                Mapper.DynamicMap(new EANModel(), new MstEAN());
                Mapper.DynamicMap(new MstEAN(), new EANModel());
                List<MstEAN> mstEANlist = (from p in eanModel select Mapper.Map(p, new MstEAN())).ToList();
                article.MstEAN = mstEANlist;



                //.  Tax
                //List<MasterArticleMapModel> SiteTaxList = new List<MasterArticleMapModel>();
                ////for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                ////{
                //MasterArticleMapModel SiteTax = new MasterArticleMapModel();
                //SiteTax.ArticleCode = autoArticleCode;
                //SiteTax.TaxName = siteArticleTaxMappingModel[0].TaxName.ToString();
                //SiteTax.Status = Convert.ToBoolean(siteArticleTaxMappingModel[0].Status);
                ////SiteTax.ToAddOrModifyEntity(true);
                //SiteTaxList.Add(SiteTax);
                //   }

                    //.  Ean
                List<MstEAN> MstEANModelList = new List<MstEAN>();   // added by vipin on 14-04-2017
                //for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                //{
                MstEAN MstEAN = new MstEAN();
                MstEAN.ArticleCode = autoArticleCode;
                MstEAN.DefaultEAN = eanModel[0].DefaultEAN;
                MstEAN.Discription = eanModel[0].Discription;
                //MstEAN.EAN = articleModel.BrandCode.ToString().Trim();
                MstEAN.EAN = eanModel[0].EAN.ToString().Trim(); 
                MstEAN.UOMCode = article.BaseUnitofMeasure;
                MstEAN.UOMTypeCode = null;
                MstEAN.STATUS = true;
                MstEAN.ToAddOrModifyEntity(true);
                MstEANModelList.Add(MstEAN);


                //.  SalesInfoRecord
                List<SalesInfoRecord> salesInfoRecordList = new List<SalesInfoRecord>();
                //for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                //{
                    SalesInfoRecord salesInfoRecord = new SalesInfoRecord();
                    salesInfoRecord.ArticleCode = autoArticleCode;
                    salesInfoRecord.SrNo = 1;
                   // salesInfoRecord.EAN = eanModel[0].EAN.ToString();
                    salesInfoRecord.EAN = eanModel[0].EAN.ToString().Trim(); 
                    salesInfoRecord.SellingPrice = articleModel.SellPrice;
                    salesInfoRecord.FromDate = CommonModel.CurrentDate;
                    salesInfoRecord.FreezeSB = false;
                    salesInfoRecord.FreezeSR = false;
                    salesInfoRecord.FreezeIN = false;
                    salesInfoRecord.FreezeOB = false;
                    //salesInfoRecord.SiteCode = CommonModel.SiteCode;
                    salesInfoRecord.SiteCode = articleModel.StoreID;

                    salesInfoRecord.ToAddOrModifyEntity(true);
                    salesInfoRecordList.Add(salesInfoRecord);
             //   }

                ////...  PurchaseInfoRecord
                List<PurchaseInfoRecord> purchaseInfoRecordlist = new List<PurchaseInfoRecord>();
                //for (int ctrSupplier = 0; ctrSupplier < rightSupplierList.Count; ctrSupplier++)
                //{
                //    for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                //    {

                        PurchaseInfoRecord purchaseInfoRecord = new PurchaseInfoRecord();
                       // purchaseInfoRecord.SiteCode = CommonModel.SiteCode;
                        purchaseInfoRecord.SiteCode = articleModel.StoreID;
                        purchaseInfoRecord.SRNO =  1;
                        purchaseInfoRecord.ArticleCode = autoArticleCode;
                       purchaseInfoRecord.SupplierCode = articleModel.ArticleSupplierCode;  //vipin

                        if (articleModel.SupplierRef != null && articleModel.SupplierRef.ToString().ToUpper().Trim() == "YES")
                        {
                            purchaseInfoRecord.IsDefaultSupplier = true;
                        }
                        else
                        {
                            purchaseInfoRecord.IsDefaultSupplier = false;
                        }

                        purchaseInfoRecord.EAN = eanModel[0].EAN.ToString();
                       // purchaseInfoRecord.EAN = articleModel.BrandCode;
                        purchaseInfoRecord.CPBaseCurr = articleModel.CostPrice;
                        purchaseInfoRecord.CPLocalCurr = articleModel.CostPrice;
                        purchaseInfoRecord.MRP = articleModel.MRP;
                        purchaseInfoRecord.MAP = articleModel.MRP;

                        purchaseInfoRecord.ToAddOrModifyEntity(true);
                        purchaseInfoRecordlist.Add(purchaseInfoRecord);

                //    }
                //}

                //// Article Characteristic in ArticleMatrix
                List<ArticleMatrix> articleMatrixlist = new List<ArticleMatrix>();

                ArticleMatrix articleMatrix;
                for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                {
                    for (int rowcharacteristic = 0; rowcharacteristic < articleCharacteristicMatrixModel.Count; rowcharacteristic++)
                    {
                        articleMatrix = new ArticleMatrix();
                        articleMatrix.EanCode = eanModel[rowEan].EAN.ToString();
                        articleMatrix.CharCode = articleCharacteristicMatrixModel[rowcharacteristic].CharCode;
                        articleMatrix.CharValue = articleCharacteristicMatrixModel[rowcharacteristic].CharValue;
                        articleMatrix.CharType = articleCharacteristicMatrixModel[rowcharacteristic].CharType;
                        articleMatrix.ProfileCode = articleCharacteristicMatrixModel[rowcharacteristic].ProfileCode;
                        articleMatrix.RowKey = articleCharacteristicMatrixModel[rowcharacteristic].RowKey;

                        articleMatrix.ToAddOrModifyEntity(true);
                        articleMatrixlist.Add(articleMatrix);
                    }
                }

                // ArticleNodeMap ----
                List<ArticleNodeMap> articleNodeMaplist = new List<ArticleNodeMap>();
                ArticleNodeMap articleNodeMap = new ArticleNodeMap();
                articleNodeMap.ArticleCode = autoArticleCode;
                articleNodeMap.TreeCode = article.TreeID;
                articleNodeMap.LastNodeCode = article.LastNodeCode;
                articleNodeMap.ToAddOrModifyEntity(true);
                articleNodeMaplist.Add(articleNodeMap);
                article.ArticleNodeMap = articleNodeMaplist;


                //// ArticleUOM ----
                ArticleUOM articleUOM = new ArticleUOM();
                articleUOM.ArticleCode = autoArticleCode;
                articleUOM.UOMCode = article.BaseUnitofMeasure;
                articleUOM.defaultUom = false;
                articleUOM.Divisor = (Convert.ToDecimal(article.VolumeUOM));
                articleUOM.IsDivisor = false;
                articleUOM.ToAddOrModifyEntity(true);

                // --- ARTICLESTOCKBALANCES Table must be add new record if it is Insert Case 
                List<ArticleStockBalances> articleStockBalanceslist = new List<ArticleStockBalances>();
                if (isNewArticle)
                {
                    for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                    {
                        ArticleStockBalances articleStockBalances = new ArticleStockBalances();
                       // articleStockBalances.SiteCode = CommonModel.SiteCode;
                        articleStockBalances.SiteCode = articleModel.StoreID;
                        articleStockBalances.ArticleCode = autoArticleCode;
                        articleStockBalances.EAN = eanModel[rowEan].EAN.ToString();
                        articleStockBalances.UOM = article.BaseUnitofMeasure;
                        articleStockBalances.PhysicalQty = 0;
                        articleStockBalances.ReservedQty = 0;
                        articleStockBalances.DamagedQty = 0;
                        articleStockBalances.OnOrderQty = 0;
                        articleStockBalances.InTrasnsitQty = 0;
                        articleStockBalances.NonSaleableQty = 0;
                        articleStockBalances.TotalPhysicalSaleableQty = 0;
                        articleStockBalances.TotalPhysicalNonSaleableQty = 0;
                        articleStockBalances.TotalVirtualNonSaleableQty = 0;
                        articleStockBalances.TotalSaleableQty = 0;
                        articleStockBalances.TotalARSQty = 0;

                        articleStockBalances.ToAddOrModifyEntity(true);
                        articleStockBalanceslist.Add(articleStockBalances);
                    }

                }

                // Article Replenishment
                ArticleReplenishment articleReplenishment = null;
                if (articleReplenishmentModel != null)
                {
                    if ((articleReplenishmentModel.SafetyStockQty != null && articleReplenishmentModel.SafetyStockQty > 0) ||
                          (articleReplenishmentModel.ReOrderPoint != null && articleReplenishmentModel.ReOrderPoint > 0))
                    {
                        for (int rowEan = 0; rowEan < eanModel.Count; rowEan++)
                        {
                            articleReplenishment = new ArticleReplenishment();
                            articleReplenishment.ArticleCode = autoArticleCode;
                            articleReplenishment.EAN = eanModel[rowEan].EAN.ToString();
                            articleReplenishment.SiteCode = CommonModel.SiteCode;
                            articleReplenishment.SafetyStockQty = articleReplenishmentModel.SafetyStockQty;
                            articleReplenishment.ReOrderPoint = articleReplenishmentModel.ReOrderPoint;
                            articleReplenishment.Month = "ALL";
                            articleReplenishment.ToAddOrModifyEntity(true);
                        }
                    }

                }

                // Master Article Map 

                List<MasterArticleMap> masterArticleMaplist = new List<MasterArticleMap>();
                if (masterArticleMapModel != null && !string.IsNullOrEmpty(masterArticleMapModel.MasterArticleCode))
                {
                    MasterArticleMap masterArticleMap = new MasterArticleMap();
                  //  masterArticleMapModel.ToAddOrModifyEntity(true);
                    Mapper.DynamicMap(new MasterArticleMapModel(), new MasterArticleMap());
                    Mapper.DynamicMap(new MasterArticleMap(), new MasterArticleMapModel());
                    masterArticleMap = Mapper.Map(masterArticleMapModel, new MasterArticleMap());
                    masterArticleMaplist.Add(masterArticleMap);
                }

                article.MasterArticleMap = masterArticleMaplist;
                // Article Desc In DiffLang 
                List<ArticleDescInDiffLang> articleDescInDiffLanglist = new List<ArticleDescInDiffLang>();
                if (articleDescInDiffLangModel != null && !string.IsNullOrEmpty(articleDescInDiffLangModel.LanguageCode))
                {
                    ArticleDescInDiffLang articleDescInDiffLang = new ArticleDescInDiffLang();
                    articleDescInDiffLangModel.ArticleName = article.ArticleName;
                    articleDescInDiffLangModel.ArticleShortName = article.ArticleShortName;
                    articleDescInDiffLangModel.ToAddOrModifyEntity(true);
                    Mapper.DynamicMap(new ArticleDescInDiffLangModel(), new ArticleDescInDiffLang());
                    Mapper.DynamicMap(new ArticleDescInDiffLang(), new ArticleDescInDiffLangModel());
                    articleDescInDiffLang = Mapper.Map(articleDescInDiffLangModel, articleDescInDiffLang);
                    articleDescInDiffLanglist.Add(articleDescInDiffLang);
                }
                article.ArticleDescInDiffLang = articleDescInDiffLanglist;



                var articleDALModel = new ArticleDALModel
                {
                    MstArticle = article,
                    MstArticleImage = articleImage,
                    ArticleMatrixList = articleMatrixlist,
                    ArticleUOM = articleUOM,
                    SalesInfoRecordList = salesInfoRecordList,
                  //  SiteTaxList = SiteTaxList,                        //added by vipin on 12-04-2017
                    MstEanList = MstEANModelList,
                    PurchaseInfoRecordList = purchaseInfoRecordlist,
                    ArticleStockBalances = articleStockBalanceslist,
                    ArticleReplenishment = articleReplenishment,
                    SiteCode = CommonModel.SiteCode,
                    IsAutoNumber = articleModel.IsAutoNumber,
                    SourcePath = articleImageModel.sourcePath,
                    TargetPath = articleImageModel.targetPath,
                    EANAutoCount = EANAutoCount
                };

                return articleDALModel;

            }
            catch (Exception ex)
            {
                 

            }
            var articleDALModel1 = new ArticleDALModel();
                  return articleDALModel1;
        }

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


        private void ArtilceDetailsSave(ref XLSheet articleDetails, ref ArticleModel articleModel, ref ArticleImageModel articleImageModel, ref List<EANModel> eanModel,
                                        ref ArticleReplenishmentModel articleReplenishment, ref MasterArticleMapModel rowMasterArticleMapModel,
                                        ref ArticleDescInDiffLangModel articleDescInDiffLang, ref List<DropDownModel> supplierList, ref int rowCntr)
        {
            try
            {
                //for (int rowCntr = 1; rowCntr < articleDetails.Rows.Count; rowCntr++)
                //{
                EANModel rowEANModel = new EANModel();
                DropDownModel rowDropDownModel = new DropDownModel();

                for (int colIndex = 0; colIndex < articleDetails.Columns.Count; colIndex++)
                {
                    if (articleDetails[rowCntr, colIndex].Value == null)
                    {
                        articleDetails[rowCntr, colIndex].Value = string.Empty;
                    }

                    switch (articleDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "ARTICLE CODE":
                            articleModel.ArticleCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;

                        case "ARTICLE NAME":
                            articleModel.ArticleName = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "ARTICLE SHORT NAME":
                            articleModel.ArticleShortName = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "Description":   //vipin
                            articleModel.Descriptions = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "Supplier Status":   //vipin
                            articleModel.LegacyArticleCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "ARTICLE TYPE":
                            articleModel.ArticalTypeCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "MATERIAL TYPE":
                            articleModel.MaterialTypeCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "MASTER ARTICLE":
                            rowMasterArticleMapModel = new MasterArticleMapModel();
                            rowMasterArticleMapModel.MasterArticleCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "PARENT ARTICLE NODE":
                            articleModel.ParentArt = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "LAST NODE":
                            string NodeName = articleDetails[rowCntr, colIndex].Value.ToString();
                            string LastNodeCode = string.Empty;
                            string TreeID = string.Empty;
                            articleRepository.getTreeCode(NodeName, ref LastNodeCode, ref TreeID);
                            articleModel.LastNodeCode = LastNodeCode;
                            articleModel.TreeID = TreeID;
                            //articleModel.LastNodeCode
                            break;
                        case "BARCODE":
                            rowEANModel.ArticleCode = articleModel.ArticleCode;
                            if (string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                articleModel.IsEanAutoGenerate = true;
                                rowEANModel.IsAutoNumber = true;
                              //  articleModel.BrandCode = "ECC" + articleModel.ArticleCode;

                            }
                            else
                            {
                                articleModel.IsEanAutoGenerate = false;
                                rowEANModel.IsAutoNumber = false;
                                articleModel.BrandCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            }
                            break;

                        case "BARCODE TYPE":
                            rowEANModel.Discription = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "DEFAULT BARCODE":
                            if (articleDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() == "YES")
                            {
                                rowEANModel.DefaultEAN = true;
                            }
                            else
                            {
                                rowEANModel.DefaultEAN = false;
                            }
                            break;
                        case "STOREID":
                            articleModel.StoreID = articleDetails[rowCntr, colIndex].Value.ToString();    //vipin
                            break;
                        case "SUPPLIER CODE":
                            rowDropDownModel.Code = articleDetails[rowCntr, colIndex].Value.ToString();
                            rowDropDownModel.Description = articleDetails[rowCntr, colIndex].Value.ToString();
                            articleModel.ArticleSupplierCode = articleDetails[rowCntr, colIndex].Value.ToString();  //vipin
                            break;

                        case "LANGUAGE CODE":
                            articleDescInDiffLang = new ArticleDescInDiffLangModel();
                            articleDescInDiffLang.LanguageCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "IMAGE":
                            string ImgString = articleDetails[rowCntr, colIndex].Value.ToString();
                            if (!string.IsNullOrEmpty(ImgString))
                            {
                                string[] splitresult = ImgString.Split('/');
                                if (splitresult.Length > 1)
                                {
                                    articleImageModel.ArticleImage = splitresult[splitresult.Length - 1];
                                    articleImageModel.sourcePath = ImgString.Substring(0, ImgString.Length - articleImageModel.ArticleImage.ToString().Length);
                                    articleImageModel.targetPath = @"D:/";
                                }
                                else
                                {
                                    articleImageModel.ArticleImage = splitresult[splitresult.Length - 1];
                                    articleImageModel.sourcePath = string.Empty;
                                    articleImageModel.targetPath = string.Empty;
                                }
                            }
                            break;
                        case "BASE UOM":
                            articleModel.BaseUnitofMeasure = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "ORDER UOM":
                            articleModel.OrderUnitofMeasure = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "SALE UOM":
                            articleModel.SaleUnitofMeasure = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "CONSUMPTION UOM":
                            articleModel.ConsumptionUoM = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;

                        case "NET WEIGHT UOM":
                            articleModel.NetWeightUOM = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "GROSS WEIGHT UOM":
                            articleModel.GrossWeightUOM = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "OUOM VALUE":
                            articleModel.VolumeUOM = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "SALE VALUE":
                            articleModel.SalesUomValue = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "COST PRICE":
                            if (!string.IsNullOrEmpty(articleDetails[rowCntr, colIndex].Value.ToString()))
                            {
                                articleModel.CostPrice = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            }

                            break;
                        case "SELLING PRICE":
                            articleModel.SellPrice = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "CONSUMPTION VALUE":
                            //articleModel.ConsumptionUoM = (decimal)articleDetails[rowCntr, colIndex].Value;
                            break;
                        case "MRP":
                            articleModel.MRP = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "MAP":
                            // articleModel.MAP = (decimal)articleDetails[rowCntr, colIndex].Value;
                            break;
                        case "NET WEIGHT":
                            articleModel.NetWeight = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "GROSS WEIGHT":
                            articleModel.GrossWeight = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "OPEN SELLING PRICE":
                            //articleModel.opense = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "REORDER QTY":
                            articleReplenishment.ReOrderPoint = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "SAFETY QTY":
                            articleReplenishment.SafetyStockQty = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "EXPIRY":
                            if (articleDetails[rowCntr, colIndex].Value.ToString().Trim().ToUpper() == "YES")
                            {
                                articleModel.isExpiry = true;
                            }
                            else
                            {
                                articleModel.isExpiry = false;
                            }
                            break;
                        case "SALEABLE":
                            if (articleDetails[rowCntr, colIndex].Value.ToString().Trim().ToUpper() == "YES")  //vipin on 18.04.2017
                            {
                                articleModel.Salable = true; // vipin on 18-4-2017
                            }
                            else
                            {
                                  articleModel.Salable = false;  // vipin on 18-4-2017
                            }

                          
                            break;
                        case "PRINTABLE":
                            if (articleDetails[rowCntr, colIndex].Value.ToString().Trim().ToUpper() == "YES")
                            {
                                articleModel.Printable = true;
                            }
                            else
                            {
                                articleModel.Printable = false;
                            }
                            break;
                        case "DEFAULT SUPPLIER":
                            // Purchase Info Record 
                            articleModel.SupplierRef = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "OPEN QTY":
                            //articleModel.OpenQty = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "DISTRIBUTION UOM":                                                                            //vipin on 20-04-2017
                            articleModel.DistributionUnitofMeasure = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "DISTRIBUTION VALUE":
                            articleModel.DistributionUomValue = decimal.Parse((articleDetails[rowCntr, colIndex].Value.ToString() == string.Empty ? "0" : articleDetails[rowCntr, colIndex].Value.ToString()));
                            break;
                        case "LEGACY CODE":
                            articleModel.LegacyArticleCode = articleDetails[rowCntr, colIndex].Value.ToString();
                            break;
                        case "STATUS":
                          //  if (articleDetails[rowCntr, colIndex].Value.ToString().Trim().ToUpper() == "YES")
                            if (articleDetails[rowCntr, colIndex].Value.ToString().Trim().ToUpper() == "ACTIVE")  //vipin on 18.04.2017
                            {
                                articleModel.Status = true;
                                articleModel.ArticleActive = true;
                            }
                            else
                            {
                                articleModel.Status = false;
                                articleModel.ArticleActive = false;
                            }
                            break;

                        default:
                            break;
                    }
                }

                if (rowEANModel != null)
                {

                    // This Row is Valid only its new item I thinks so ..
                    //if (articleModel.ArticleCode == null || string.IsNullOrEmpty(articleModel.ArticleCode))
                    //{
                        eanModel.Add(rowEANModel);
                   // }

                }

                if (rowDropDownModel != null)  // changed by vipin on 07-04-2017
                {
                    // This Row is Valid only its new item I thinks so ..
                    //if (articleModel.ArticleCode == null || string.IsNullOrEmpty(articleModel.ArticleCode))
                    //{
                        supplierList.Add(rowDropDownModel);
                   // }
                }

                //}
           }
            catch (Exception ex)
            {
               // throw ex;
            }

        }

        private void ArtilceBarCodeSave(ref XLSheet barCodeDetails, ref  List<EANModel> eanModel, string articleCode, ref int rowCntr)
        {
            //for (int rowCntr = 1; rowCntr < barCodeDetails.Rows.Count; rowCntr++)
            //{
            EANModel rowEanModel = null;
            if (barCodeDetails != null) //code added by khusrao adil
            {
                for (int colIndexcheck = 0; colIndexcheck < barCodeDetails.Columns.Count; colIndexcheck++)
                {
                    if (barCodeDetails.GetCell(xlscolumnRow, colIndexcheck).Value.ToString().ToUpper().Trim() == "ARTICLE CODE" && barCodeDetails[rowCntr, colIndexcheck].Value != null
                                  && articleCode == barCodeDetails[rowCntr, colIndexcheck].Value.ToString())
                    {
                        rowEanModel = new EANModel();
                        for (int colIndex = 0; colIndex < barCodeDetails.Columns.Count; colIndex++)
                        {
                            switch (barCodeDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                            {
                                case "STOREID":
                                    break;
                                case "ARTICLE CODE":
                                    rowEanModel.ArticleCode = barCodeDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "BARCODE":
                                    rowEanModel.EAN = barCodeDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "SELLING PRICE":
                                    string spPrice = barCodeDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "STATUS":
                                    if (barCodeDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() == "YES")
                                    {
                                        rowEanModel.Status = true;
                                    }
                                    else
                                    {
                                        rowEanModel.Status = false;
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    // }

                }
            }
            if (rowEanModel != null)
            {
                eanModel.Add(rowEanModel);
            }
        }

        private void ArtilceTaxSave(ref XLSheet taxDetails, ref List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel, string articleCode, ref int rowCntr)
        {
            //for (int rowCntr = 1; rowCntr < taxDetails.Rows.Count; rowCntr++)
            //{
            SiteArticleTaxMappingModel rowSiteArticleTaxMappingModel = null;
            if (taxDetails != null) //code added by khusrao adil
            {
                for (int colIndexcheck = 0; colIndexcheck < taxDetails.Columns.Count; colIndexcheck++)
                {
                    if (taxDetails.GetCell(xlscolumnRow, colIndexcheck).Value.ToString().ToUpper().Trim() == "ARTICLE CODE"
                                && taxDetails[rowCntr, colIndexcheck].Value != null
                                && articleCode == taxDetails[rowCntr, colIndexcheck].Value.ToString())
                    {
                        rowSiteArticleTaxMappingModel = new SiteArticleTaxMappingModel();
                        for (int colIndex = 0; colIndex < taxDetails.Columns.Count; colIndex++)
                        {
                            switch (taxDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                            {
                                case "ARTICLE CODE":
                                    rowSiteArticleTaxMappingModel.ArticleCode = taxDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "ARTICLE NAME":
                                    break;
                                case "STOREID":
                                    rowSiteArticleTaxMappingModel.SiteCode = taxDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "TAX NAME":
                                    rowSiteArticleTaxMappingModel.TaxName = taxDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "TAX CODE":
                                    rowSiteArticleTaxMappingModel.TaxCode = taxDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "TAX STATUS":
                                    if (taxDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES")
                                    {
                                        rowSiteArticleTaxMappingModel.Status = true;
                                    }
                                    else
                                    {
                                        rowSiteArticleTaxMappingModel.Status = false;
                                    }
                                    break;
                                case "SUPPLIER CODE":
                                    rowSiteArticleTaxMappingModel.SupplierCode = taxDetails[rowCntr, colIndex].Value.ToString();
                                    break;

                                default:
                                    break;
                            }

                        }
                        //}
                    }
                }
            }
            if (rowSiteArticleTaxMappingModel != null)
            {
                siteArticleTaxMappingModel.Add(rowSiteArticleTaxMappingModel);
            }
        }

        private void ArtilceCharactersticsSave(ref XLSheet charDetails, List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel, string articleCode, ref int rowCntr)
        {
            //for (int rowCntr = 1; rowCntr < charDetails.Rows.Count; rowCntr++)
            //{
            ArticleCharacteristicMatrixModel rowArticleCharacteristicMatrixModel = null;
            if (charDetails != null)  //code added by khusrao adil
            {
                for (int colIndexcheck = 0; colIndexcheck < charDetails.Columns.Count; colIndexcheck++)
                {
                    if (charDetails.GetCell(xlscolumnRow, colIndexcheck).Value.ToString().ToUpper().Trim() == "ARTICLE CODE" && charDetails[rowCntr, colIndexcheck].Value != null &&
                                articleCode == charDetails[rowCntr, colIndexcheck].Value.ToString())
                    {
                        rowArticleCharacteristicMatrixModel = new ArticleCharacteristicMatrixModel();
                        for (int colIndex = 0; colIndex < charDetails.Columns.Count; colIndex++)
                        {
                            switch (charDetails.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                            {
                                case "ARTICLE CODE":
                                    //rowArticleCharacteristicMatrixModel.EanCode = ""
                                    break;
                                case "ARTICLE NAME":
                                    break;
                                case "BARCODE":
                                    rowArticleCharacteristicMatrixModel.EanCode = charDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "PROFILE":
                                    rowArticleCharacteristicMatrixModel.EanCode = defaultProfileName;
                                    break;
                                case "CHAR ID":
                                    rowArticleCharacteristicMatrixModel.CharValue = charDetails[rowCntr, colIndex].Value.ToString();
                                    break;
                                case "CHAR STATUS":
                                    if (charDetails[rowCntr, colIndex].Value.ToString().ToUpper().Trim() != "YES")
                                    {
                                        rowArticleCharacteristicMatrixModel.Status = true;
                                    }
                                    else
                                    {
                                        rowArticleCharacteristicMatrixModel.Status = false;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    //}
                }
            }
            if (rowArticleCharacteristicMatrixModel != null)
            {
                articleCharacteristicMatrixModel.Add(rowArticleCharacteristicMatrixModel);
            }
        }

        #endregion


    }

}
