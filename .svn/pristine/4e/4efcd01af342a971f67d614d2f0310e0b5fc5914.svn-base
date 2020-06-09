using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;
using C1.C1Excel;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IArticleRepository : IGenericRepository<MstArticle>
    {
        string defaultProfileName { get; set; }

        bool SaveArticle(ArticleDALModel articleDALModel);

        bool UpdateArticle(MstArticle article);

        void DeleteArticle(MstArticle article);

        bool DeleteByID(string articleID);

        IQueryable<ArticleSearchModel> GetArticleSearchList();

        IQueryable<MstArticle> GetArticles();

        MstArticle GetArticleByID(string articleID);

        ArticleModel GetArticleModelByID(string articleID);

        bool copyFile(string fileName, string sourcePath, string targetPath);

        IList<ArticlePurchaseModel> GetArticlePurchaseList(string supplierCode, List<string> articleCodes);

        IList<ArticlePurchaseStockoutModel> GetArticlePurchaseList(string supplierCode);

        ArticleDataExportModel GetArticleExportData(string nodeCode, string siteCode);

        ArticleDataExportModel GetArticlesForBarcodeExportData(string nodeCode, string siteCode);
        
        IQueryable<MstEAN> GetArticleEAN();
        
        void getTreeCode(string nodeName, ref string NodeCode, ref string TreeCode);

        bool SaveImportExcel(ref List<ArticleDALModel> articleExcelDALModel);

        bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails, ref string ErrorMsg);
        
        bool IsBarCodeExist(string eanCode);

        IList<MstEAN> GetArticleEAN(string articleID);
        IList<ArticleMatrix> GetArticleCharacteristicMatrixModel(string articleCode);
        SalesInfoRecord GetArticleSalesInfoRecord(string articleCode);
        IList<PurchaseInfoRecordModel> GetArticlePurchaseInfoRecordList(string articleCode);
        IList<SiteArticleTaxMappingModel> GetsiteArticleTaxMappingList(string articleCode);
        
        IList<PurchaseInfoRecord> ArticleById(string autoArticleCode);
        bool UpdateMst(List<PurchaseInfoRecord> purinfor);
    }
}

