using System.Linq;
using Spectrum.Models;
using Spectrum.DAL;
using Spectrum.BL.BusinessInterface;
using System;
using System.Linq.Expressions;
using AutoMapper;
using Spectrum.BL.Mappers;
using Spectrum.DAL.RepositoryInterfaces;
using System.Collections.Generic;
using Spectrum.DAL;

namespace Spectrum.BL
{
    public class CharacteristicManager : GenericManager<CharacteristicModel>, ICharacteristicManager
    {
        public CharacteristicManager()
        {
            this.commonRepository = new CommonRepository();
            this.characteristicRepository = new CharacteristicRepository();

            this.predicate = PredicateBuilder.False<CharacteristicsValue>();
            Mapper.CreateMap<CharacteristicsValue, CharacteristicModel>();
            Mapper.CreateMap<CharacteristicModel, CharacteristicsValue>();
        }
        Expression<Func<CharacteristicsValue, bool>> predicate;
        ICommonRepository commonRepository;
        ICharacteristicRepository characteristicRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characteristicModel"></param>
        /// <returns></returns>
        public bool SaveCharacteristic(IList<CharacteristicModel> characteristicsModel)
        {
            CharacteristicsValue characteristic;
            var characteristics = new List<CharacteristicsValue>();

            try
            {
                var characteristicList = this.characteristicRepository.GetCharacteristicList().ToList();

                for (int rowIndex = 0; rowIndex < characteristicsModel.Count(); rowIndex++)
                {
                    characteristic = characteristicList.Where(c => c.CharValue == characteristicsModel[rowIndex].CharValue).FirstOrDefault();

                    if (characteristicsModel[rowIndex].CharStatus != null)
                    {
                        if (characteristicsModel[rowIndex].CharStatus.Equals("Deleted"))
                        {
                            this.characteristicRepository.DeleteCharacteristic(characteristic);
                        }
                        else if (characteristicsModel[rowIndex].CharStatus.Equals("Added"))
                        {
                            characteristic = new CharacteristicsValue();
                            Mapper.Map(characteristicsModel[rowIndex], characteristic);
                            characteristic.ToAddOrModifyEntity(true);
                            characteristicList.Add(characteristic);
                            this.characteristicRepository.SaveCharacteristic(characteristic);
                        }
                        else if (characteristicsModel[rowIndex].CharStatus.Equals("Modified"))
                        {
                            characteristic = characteristicList.Where(c => c.CharValue == characteristicsModel[rowIndex].OriginalCharValue).FirstOrDefault();

                            //if (characteristic.CharValue.Equals(characteristicsModel[rowIndex].OriginalCharValue))
                            //{
                             string oldCharValue = characteristic.CharValue;
                            //    characteristic.CharValue = characteristicsModel[rowIndex].CharValue;
                            //    characteristic.ToAddOrModifyEntity(true);
                             this.characteristicRepository.UpdateCharacteristic(characteristic, characteristicsModel[rowIndex].CharValue);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characteristicID"></param>
        /// <returns></returns>
        public CharacteristicModel GetCharacteristicByID(string characteristicID)
        {
            this.predicate = this.predicate.And(c => c.CharCode == characteristicID);

            var characteristic = this.characteristicRepository.GetByID(predicate);

            return (characteristic != null) ? Mapper.Map(characteristic, new CharacteristicModel()) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<CharacteristicModel> GetCharacteristicList()
        {
            var characteristicList = this.characteristicRepository.GetCharacteristicList();

            var characteristicModelList = new List<CharacteristicModel>();

            for (int i = 0; i < characteristicList.Count(); i++)
                characteristicModelList.Add(Mapper.Map(characteristicList.ToList()[i], new CharacteristicModel()));

            return characteristicModelList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<DropDownModel> GetCharacteristicType()
        {
            return this.characteristicRepository.GetCharacteristicTypes();
        }
        public bool IsProductAssociate(string charvalue)
        {
            return this.characteristicRepository.IsProductAssociate(charvalue);
        }
        public string IsProductDeletAssociate(string charvalue)
        {
            return this.characteristicRepository.IsProductDeletAssociate(charvalue);
        }

        public IQueryable<CharacteristicModel> GetCharacteristicListNew()
        {
            return this.characteristicRepository.GetCharacteristicListNew();
        }
    }
}
