using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface ICharacteristicRepository : IGenericRepository<CharacteristicsValue>
    {
        bool SaveCharacteristic(CharacteristicsValue characteristic);
        bool UpdateCharacteristic(CharacteristicsValue characteristic,string oldCharValue);
        void DeleteCharacteristic(CharacteristicsValue characteristic);
        bool DeleteByID(string charCode);

        IQueryable<CharacteristicsValue> GetCharacteristicList();
        CharacteristicsValue GetCharacteristicByID(string charCode);
        IList<DropDownModel> GetCharacteristicTypes();
        bool IsProductAssociate(string charvalue);
        string IsProductDeletAssociate(string charvalue);
        IQueryable<CharacteristicModel> GetCharacteristicListNew();
    }
}
