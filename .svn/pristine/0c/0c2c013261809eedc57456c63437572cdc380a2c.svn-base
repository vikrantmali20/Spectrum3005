using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface ICommonRepository : IGenericRepository<MstAreaCode>
    {
        IQueryable<MstAreaCode> GetAreaCodeList();
        IQueryable<GeneralCodeMst> GetMasterTypeList();
        IQueryable<MstArticleType> GetArticleTypeList();
        IQueryable<MstUoM> GetUoMList();
        IQueryable<Brand> GetBrandList();
        IQueryable<MstTenderType> GetTenderTypeList();
        IQueryable<MstCLPProgram> GetClPbyTenderType();
        IQueryable<MstCLPProgramModel> getclpprogram(string sitecode);
        IQueryable<MstVoucher> GetVouchers(string vouchertype);
        IQueryable<MstRole> GetRoles();
        IQueryable<MstBusiness> GetBusinessDetails();
        IQueryable<MstLanguage> GetLanguageList();
        IQueryable<MstEAN> GetDefaultEAN();
        IQueryable<MstEAN> GetDefaultEANbyArticle(string articleCode);
        IQueryable<MstCurrency> GetCurrency();
        IQueryable<Manufacturer> GetManufacturerList();
        IQueryable<CharacteristicsModel> GetCharacteristicsList();
        IList<DropDownModel> GetStockOutReasons();
        IList<DropDownModel> GetFromLocation();
        /// <summary>
        /// Get Next new number
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="objectID"></param>
        /// <returns></returns>
        int GetNextID(string siteCode, string objectID);
        void UpdateNextID(string siteCode, string objectID);
        IQueryable<MstSite> GetSiteList();
        IQueryable<MstSupplierModel> GetSupplierBySite(string SiteCode);
        IQueryable<MstDocumentTypeModel> GetDocumentType(string SiteCode);
        IQueryable<GeneralCodeMstModel> GetAppliedOn(string SiteCode);

        //ashma - 15 may 2018 for ShiftManagement
        string GetSiteName();

        //bool Savestocksinothers(Invoice invoicesModel, OrderDtl orddetmodel, OrderHdr ordhdrmodel);
        //bool Savestocksoutothers( OrderDtl orddetmodel, OrderHdr ordhdrmodel);

    }

}
