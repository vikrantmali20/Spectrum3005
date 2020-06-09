using System.Collections.Generic;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IShiftManagementRepository : IGenericRepository<MstShift>
    {
        bool LoadData(ref List<MstShift> objShift);
        bool SaveData(ref List<MstShift> objShift);
        bool GetShift(ref string SiteCode, ref int ShiftId);
    }
}
