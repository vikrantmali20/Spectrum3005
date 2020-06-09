using System.Collections.Generic;
using Spectrum.Models;

namespace Spectrum.DAL.CustomEntities
{
    public class ArticleDALModel
    {
        public MstArticle MstArticle { get; set; }
        public MstArticleImage MstArticleImage { get; set; } 
        public List<ArticleMatrix> ArticleMatrixList { get; set; }
        public ArticleUOM ArticleUOM { get; set; }
        public List<SalesInfoRecord> SalesInfoRecordList { get; set; }
        public List<PurchaseInfoRecord> PurchaseInfoRecordList { get; set; }
        public List<MasterArticleMapModel> SiteTaxList { get; set; }   // added by vipin 0n 12-04-2017
        public List<MstEAN> MstEanList { get; set; }  // added by vipin 0n 14-04-2017
        public List<ArticleStockBalances> ArticleStockBalances{ get; set; }
        public MasterArticleMap MasterArticleMap { get; set; }
        public ArticleReplenishment ArticleReplenishment { get; set; }
        public ArticleDescInDiffLang ArticleDescInDiffLang { get; set; }
        public string SiteCode { get; set;}
        public bool IsAutoNumber { get; set;} 
        public string SourcePath{ get; set; }
        public string TargetPath { get; set; }
        public int EANAutoCount { get; set; }

    }

    public class ArticleExcelDALModel
    {
        public ArticleModel articleModel { get; set; }
        public List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel { get; set; }
        public ArticleImageModel articleImageModel { get; set; }
        public List<EANModel> eanModel { get; set; }
        public List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel { get; set; }
        public List<DropDownModel> rightSupplierList { get; set; }
        public string autoArticleCode { get; set; } 
    }

    public class ArticleDataModel
    {
        public string autoArticleCode { get; set; } 
        public ArticleModel articleModel { get; set; }
        public List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel { get; set; }
        public SalesInfoRecordModel salesInfoRecordModel { get; set; }
        public List<PurchaseInfoRecordModel> purchaseInfoRecordModelList { get; set; }
        public List<SiteArticleTaxMappingModel> siteArticleTaxMappingModelList { get; set; }
        public ArticleImageModel articleImageModel { get; set; }
        public List<EANModel> eanModel { get; set; }
        public List<DropDownModel> rightSupplierList { get; set;}
    }


}
