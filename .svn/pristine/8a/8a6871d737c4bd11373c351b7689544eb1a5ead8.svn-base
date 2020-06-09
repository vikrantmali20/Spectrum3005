using System.Linq;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    public interface ISiteManager : IGenericManager<SiteModel>
    {
        bool SaveSite(SiteDALModel siteDALModel );
        bool UpdateSite(SiteModel siteModel, MstFinYearModel finYear);
        bool DeleteByID(string siteID);

        IQueryable<SiteModel> GetSiteList();
        IQueryable<SiteModelEditList> GetSiteEditList();
        SiteModel GetSiteByID(string siteID);
        MstFinYearModel GetFinYearDetailsBySiteID(string siteID);
        MstCurrencySiteModel GetCurrencySymbol(string currencyCode);
         
    }
}
