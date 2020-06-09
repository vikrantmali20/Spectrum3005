using Spectrum.Models;
using System;
using System.Collections.Generic;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.BL.BusinessInterface;

namespace Spectrum.BL
{
   public class ShiftManagementManager : GenericManager<ShiftManagementModel>, IShiftManagementManager
   {
       public ShiftManagementManager()
       {
           this.shiftManagementRepository = new ShiftManagementRepository();
       }
       private IShiftManagementRepository shiftManagementRepository;

       public bool LoadData(ref List<MstShift> objShift)
       {
           bool result = false;
           try
           {
               result = this.shiftManagementRepository.LoadData(ref objShift);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return result;
       }
       public bool SaveData(ref List<MstShift> objShift)
       {
           bool result = false;
           try
           {
               result = this.shiftManagementRepository.SaveData(ref objShift);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return result;
       }
       public bool GetShift(ref string SiteCode, ref int ShiftId)
       {
           bool result = false;
           try
           {
               result = this.shiftManagementRepository.GetShift(ref SiteCode, ref ShiftId);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return result;
       }
    }
}
