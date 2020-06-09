using System.Linq;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    public interface ITenderManager : IGenericManager<TenderModel>
    {
        bool SaveTender(TenderModel tenderModel);
        bool UpdateTender(TenderModel tenderModel);
        bool DeleteByID(string tenderID, string sitecode);

        IQueryable<TenderModelList> GetTenderList();
        TenderModel GetTenderByID(string tenderID);
    }
}
