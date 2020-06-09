using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityState = System.Data.Entity.EntityState;

namespace Spectrum.DAL
{
    public class ShiftManagementRepository : GenericRepository<MstShift>, IShiftManagementRepository
    {
        public bool LoadData(ref List<MstShift> objShift)
        {
            bool result = false;
            try
            {
                objShift = Context.MstShift.Where(a => a.SiteCode == CommonModel.SiteCode).ToList();
                result = true;
            }
            catch (Exception ex) { }
            return result;
        }

        public bool GetShift(ref string SiteCode, ref int ShiftId)
        {
            bool result = false;
            try
            {
                string sitecode = SiteCode;
                int shiftid = ShiftId;
                var objShift = Context.MstShift.Where(a => a.SiteCode == sitecode && a.ShiftId == shiftid).Count();
                if (objShift != 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex) { }
            return result;
        }

        public bool SaveData(ref List<MstShift> objShift)
        {
            bool tranResult = false;
            try
            {
                using (var dbContextTransaction = Context.Database.BeginTransaction())
                {
                    for (int rowCntr = 0; rowCntr < objShift.Count; rowCntr++)
                    {
                        string SiteCode = objShift[rowCntr].SiteCode;
                        int ShiftId = objShift[rowCntr].ShiftId;
                       
                        var entityobjShift = Context.MstShift.Where(a => a.SiteCode == SiteCode && a.ShiftId == ShiftId).FirstOrDefault();
                        if (entityobjShift != null)
                        {
                            objShift[rowCntr].CREATEDAT = entityobjShift.CREATEDAT;
                            objShift[rowCntr].CREATEDON = entityobjShift.CREATEDON;
                            objShift[rowCntr].CREATEDBY = entityobjShift.CREATEDBY;
                            objShift[rowCntr].UPDATEDAT = CommonModel.SiteCode;
                            objShift[rowCntr].UPDATEDON = DateTime.Now;
                            objShift[rowCntr].UPDATEDBY = CommonModel.UserID;
                            Update(objShift[rowCntr], key => key.SiteCode == SiteCode && key.ShiftId == ShiftId);
                            Context.SaveChanges();
                        }
                        else
                        {
                            objShift[rowCntr].CREATEDAT = CommonModel.SiteCode;
                            objShift[rowCntr].CREATEDON = DateTime.Now;
                            objShift[rowCntr].CREATEDBY = CommonModel.UserID;
                            objShift[rowCntr].UPDATEDAT = CommonModel.SiteCode;
                            objShift[rowCntr].UPDATEDON = DateTime.Now;
                            objShift[rowCntr].UPDATEDBY = CommonModel.UserID;
                            Context.MstShift.Add(objShift[rowCntr]);
                            Context.SaveChanges();
                        }
                    }
                    dbContextTransaction.Commit();
                    tranResult = true;
                }

            }
            catch (Exception ex) { }
            return tranResult;
        }

        private void Update<T>(T entity, Func<T, bool> keycondition) where T : class
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = Context.Set<T>();
                T attachedEntity = set.Local.SingleOrDefault(keycondition);
                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }

            }
        }


    }
}
