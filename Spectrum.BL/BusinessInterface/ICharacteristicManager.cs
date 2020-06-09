using System.Collections.Generic;
using System.Linq;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    public interface ICharacteristicManager : IGenericManager<CharacteristicModel>
    {
        bool SaveCharacteristic(IList<CharacteristicModel> characteristicsModel);
         
        CharacteristicModel GetCharacteristicByID(string characteristicID);

        IList<CharacteristicModel> GetCharacteristicList();

        IList<DropDownModel> GetCharacteristicType();
        bool IsProductAssociate(string charvalue);
        string IsProductDeletAssociate(string charvalue);
        IQueryable<CharacteristicModel> GetCharacteristicListNew();
    }
}
