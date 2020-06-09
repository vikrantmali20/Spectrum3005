using System.Linq;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface ITenderRepository : IGenericRepository<MstTender>
    {
        bool SaveTender(MstTender Tender);
        bool UpdateTender(MstTender Tender);
        void DeleteTender(MstTender Tender);
        bool DeleteByID(string TenderID, string sitecode);

        IQueryable<MstTender> GetTenderList();
        MstTender GetTenderByID(string TenderID);

    }
}
