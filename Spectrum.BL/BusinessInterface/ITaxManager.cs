using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;

namespace Spectrum.BL.BusinessInterface
{
    public interface ITaxManager : IGenericManager<TaxModel>
    {
        bool SaveTax(TaxModel taxModel, TaxSiteMappingModel taxSiteMapModel,TaxSiteDocMapModel  taxsitedocmapmodel);
        bool UpdateTax(TaxModel taxModel, TaxSiteDocMapModel taxSiteDocMapModel);
        bool UpdateTaxDoc(TaxModel taxModel, TaxSiteDocMapModel taxSiteDocMapModel);
        bool DeleteByID(string taxID, string docId);
        bool DuplicateRecords(string taxName, string docID, string taxCode);

        IQueryable<TaxModel> GetTaxList();
        IQueryable<TaxSiteDocMapModel> GetTaxDoc();
        TaxModel GetTaxByID(string taxID);
        TaxSiteDocMapModel GetTaxByDoc(string taxID, string docID);
    }
}
