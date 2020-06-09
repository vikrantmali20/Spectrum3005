using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;

namespace Spectrum.DAL
{
    public class CharacteristicsRepository : GenericRepository<MstCharacteristics>, ICharacteristicsRepository
    {
        public bool SaveCharacteristics(MstCharacteristics Characteristics)
        {
            try
            {
                Context.MstCharacteristics.Add(Characteristics);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateCharacteristics(MstCharacteristics Characteristics)
        {
            try
            {
                Context.Entry<MstCharacteristics>(Characteristics).State = EntityState.Modified;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCharacteristics(MstCharacteristics Characteristics)
        {
            try
            {
                Context.MstCharacteristics.Remove(Characteristics);
                Context.Entry<MstCharacteristics>(Characteristics).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteByID(string CharacteristicsID)
        {
            try
            {
                var Characteristics = Context.MstCharacteristics.Where(u => u.CharCode == CharacteristicsID).FirstOrDefault();

                Context.MstCharacteristics.Remove(Characteristics);
                Context.Entry<MstCharacteristics>(Characteristics).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstCharacteristics> GetCharacteristicsList()
        {
            try
            {
                return Context.MstCharacteristics.OrderBy(a => a.CharName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstCharacteristics GetCharacteristicsByID(string CharacteristicsID)
        {
            try
            {
                return Context.MstCharacteristics.Where(u => u.CharCode== CharacteristicsID ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
