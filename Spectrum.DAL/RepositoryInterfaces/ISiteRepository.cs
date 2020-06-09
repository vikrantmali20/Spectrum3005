using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface ISiteRepository : IGenericRepository<MstSite>
    {
        bool SaveSite(MstSite site, MstFinYear finYear, MstAreaCode areaCode, MstCurrency currency, MstSiteCurrancyMap siteCurrency);
        bool UpdateSite(MstSite site,MstFinYear finYear);
        void DeleteSite(MstSite site);
        bool DeleteByID(string siteID);

        IQueryable<MstSite> GetSiteList();
        IQueryable<SiteModelEditList> GetSiteEditList();
        MstSite GetSiteByID(string siteID);
        MstFinYear GetFinYearDetailsBySiteID(string siteID);
        MstCurrency GetCurrencySymbol(string currencyCode);
    }
}
