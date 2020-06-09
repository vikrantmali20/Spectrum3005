using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    
    public interface ICommonManager : IGenericManager<AreaCodeModel>
    {
         IQueryable<AreaCodeModel> GetAreaCodeList();
         IQueryable<MasterTypeModel> GetMasterTypeList();
      
         Int64 GetTranNextNo(string tranObjId);
         IQueryable<ArticleTypeModel> GetArticleTypeList();
         IQueryable<ManufacturerModel> GetManufacturerList();
         IQueryable<BrandModel> GetBrandList();
         IQueryable<MstTenderTypeModel> GetTenderTypeList();
         IQueryable<MstCLPProgramModel> GetClPbyTenderType();
         IQueryable<MstCLPProgramModel> getclpprogram(string sitecode);
         IQueryable<MstVoucherModel> GetVouchers(string vouchertype);
         IQueryable<MstRoleModel> GetRoles();
         IQueryable<MstBusinessModel> GetBusinessDetails();
         IQueryable<MstLanguageModel> GetLanguageList();
         IQueryable<MstEANModel> GetDefaultEAN();
         IQueryable<MstEANModel> GetDefaultEANbyArticle(string articleCode);
         IQueryable<MstCurrencyModel> GetCurrency();
         IQueryable<UoMModel> GetUoMList();
         IQueryable<CharacteristicsModel> GetCharacteristicsList();
         int GetNextID(string siteCode, string objectID);
         IList<DropDownModel> GetStockOutReasons();
         IList<DropDownModel> GetFromLocation();
         void UpdateNextID(string siteCode, string objectID);
         IQueryable<MstSiteModel> GetSiteList();
         IQueryable<MstSupplierModel> GetSupplierBySite(string SiteCode);
         IQueryable<MstDocumentTypeModel> GetDocumentType(string SiteCode);
         IQueryable<GeneralCodeMstModel> GetAppliedOn(string SiteCode);

         //ashma - 15 may 2018 for ShiftManagement
         string GetSiteName();

         //bool Savestocksinothers(InvoiceModel invoicesModel, OrderDtlModel orddetmodel, OrderHdrModel ordhdrmodel);
         //bool Savestocksoutothers(OrderDtlModel orddetmodel, OrderHdrModel ordhdrmodel);
    }

}
