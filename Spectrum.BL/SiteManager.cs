using System.Linq;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using AutoMapper;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;

namespace Spectrum.BL
{
    public class SiteManager : GenericManager<SiteModel>, ISiteManager
    {
        public SiteManager()
        {
            this.siteRepository = new SiteRepository();
            Mapper.CreateMap<MstSite, SiteModel>();
            Mapper.CreateMap<SiteModel, MstSite>();
            Mapper.CreateMap<MstSite, SiteModelEditList>();
            Mapper.CreateMap<SiteModelEditList, MstSite>();
            Mapper.CreateMap<MstFinYear, MstFinYearModel>();
            Mapper.CreateMap<MstFinYearModel, MstFinYear>();
            Mapper.CreateMap<MstSiteCurrancyMap, MstSiteCurrancyMapModel>();
            Mapper.CreateMap<MstSiteCurrancyMapModel, MstSiteCurrancyMap>();
            Mapper.CreateMap<MstAreaCode, MstAreaCodeModel>();
            Mapper.CreateMap<MstAreaCodeModel, MstAreaCode>();
            Mapper.CreateMap<MstCurrency, MstCurrencySiteModel>();
            Mapper.CreateMap<MstCurrencySiteModel, MstCurrency>();
        }
        private ISiteRepository siteRepository;

        public bool SaveSite(SiteDALModel siteDALModel)
        {
            var site = Mapper.Map(siteDALModel.SiteModel , new MstSite());
            var finYear = Mapper.Map(siteDALModel.MstFinYearModel, new MstFinYear());
            var siteCurrencyMap = Mapper.Map(siteDALModel.MstSiteCurrancyMapModel, new MstSiteCurrancyMap());
            var areaCodeMap = Mapper.Map(siteDALModel.MstAreaCodeModel , new MstAreaCode ());
            var currencyMap = Mapper.Map(siteDALModel.MstCurrencySiteModel, new MstCurrency ());
            return this.siteRepository.SaveSite(site, finYear, areaCodeMap, currencyMap, siteCurrencyMap);
        }
        public bool UpdateSite(SiteModel siteModel, MstFinYearModel finYearModel)
        {
            var site = Mapper.Map(siteModel, new MstSite());
            var siteFinYear = Mapper.Map(finYearModel, new MstFinYear());

            return this.siteRepository.UpdateSite(site, siteFinYear);
        }

        public bool DeleteByID(string siteID)
        {
            return this.siteRepository.DeleteByID(siteID);
        }

        public IQueryable<SiteModel> GetSiteList()
        {
            var siteList = this.siteRepository.GetSiteList();

            return null;
        } 
        public IQueryable<SiteModelEditList> GetSiteEditList()
        {
            var siteList = this.siteRepository.GetSiteEditList();

            return siteList;
        }
        public SiteModel GetSiteByID(string siteID)
        {
            var site = this.siteRepository.GetSiteByID(siteID);

            var siteModel = Mapper.Map(site, new SiteModel());
            return siteModel;
        }

        public MstFinYearModel GetFinYearDetailsBySiteID(string siteID)
        {
            var finYear  = this.siteRepository.GetFinYearDetailsBySiteID(siteID);

            var finYearModel = Mapper.Map(finYear, new MstFinYearModel());
            return finYearModel;
        }
        public MstCurrencySiteModel GetCurrencySymbol(string currencyCode)
        {
            var siteCurrencysymbol = this.siteRepository.GetCurrencySymbol(currencyCode);

            var siteCurrencyMapModel = Mapper.Map(siteCurrencysymbol, new MstCurrencySiteModel());
            return siteCurrencyMapModel;
        }
    }
}
