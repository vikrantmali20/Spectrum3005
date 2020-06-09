using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
using C1.C1Excel;
using Spectrum.DAL.CustomEntities;
namespace Spectrum.BL.BusinessInterface
{

    public interface IArticleManager
    {
        string defaultProfileName { get; set; }

        bool SaveArticle(ArticleModel articleModel, List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel, ArticleImageModel articleImageModel,
                              List<EANModel> eanModel, List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel,
                              MasterArticleMapModel masterArticleMapModel, List<DropDownModel> rightSupplierList, List<DropDownModel> leftSupplierList, ref string autoArticleCode, ref string LastSupllierAddRemoved);

        bool UpdateArticle(ArticleModel articleModel);

        IQueryable<ArticleSearchModel> GetArticleSearchList();

        IList<ArticlePurchaseModel> GetArticlePurchaseList(string supplierCode, List<string> articleCodes);

        IList<ArticlePurchaseStockoutModel> GetArticlePurchaseList(string supplierCode);

        IQueryable<EANModel> GetArticleEAN();

        ArticleDataExportModel GetArticleExportData(string nodeCode, string siteCode);

        ArticleDataExportModel GetArticlesForBarcodeExportData(string nodeCode, string siteCode);

        bool SaveImportExcel(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails);

        bool ImportExcelValidations(ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails,ref string ErrorMsg);

        bool IsBarCodeExist(string eanCode);

        ArticleModel GetArticleById(string editArticleCode);
        
        ArticleDataModel GetArticleData(string editArticleCode);

        bool UpdateMstArticle(string autoArticleCode, string LastSupllierAddRemoved);

    }
}
