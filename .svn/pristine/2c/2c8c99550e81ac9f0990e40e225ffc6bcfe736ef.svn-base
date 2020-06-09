using System.Linq;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface ITaxRepository : IGenericRepository<MstTax>
    {
        bool SaveTax(MstTax Tax, TaxSiteMapping taxSiteMap,TaxSiteDocMap taxsitedocmap);
        bool UpdateTax(MstTax Tax, TaxSiteDocMap taxsitedocmap);
        bool UpdateTaxSiteDoc(MstTax Tax, TaxSiteDocMap taxsitedocmap);
        void DeleteTax(MstTax Tax);
        bool DeleteByID(string TaxID,string DocID);
        bool DuplicateRecords(string taxName, string docID, string taxCode);

        IQueryable<MstTax> GetTaxList();
        IQueryable<TaxSiteDocMap> GetTaxDoc(); 
        MstTax GetTaxByID(string TaxID);
        TaxSiteDocMap GetTaxByDoc(string TaxID, string docID);

    }
}
