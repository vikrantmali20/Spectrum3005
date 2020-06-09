using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;

namespace Spectrum.DAL
{
    public class TenderRepository : GenericRepository<MstTender>, ITenderRepository
    {
        public bool SaveTender(MstTender tender)
        {

            try
            {
                var inActiveTender = GetDeactiveTenderByID(tender.TenderHeadCode);
                if (inActiveTender != null)
                {
                    var tenderUpdate = Context.MstTender.Where(u => u.TenderHeadCode == tender.TenderHeadCode && u.SiteCode == tender.SiteCode  ).FirstOrDefault();
                    tenderUpdate.STATUS = true;
                    tenderUpdate.TenderHeadName = tender.TenderHeadName;
                    tenderUpdate.TenderType = tender.TenderType;
                    tenderUpdate.Positive_Negative = tender.Positive_Negative;
                    tenderUpdate.ProgramId = tender.ProgramId;
                    tenderUpdate.CREATEDAT = tender.CREATEDAT;
                    tenderUpdate.CREATEDBY = tender.CREATEDBY;
                    tenderUpdate.CREATEDON = tender.CREATEDON;
                    tenderUpdate.UPDATEDAT = tender.UPDATEDAT;
                    tenderUpdate.UPDATEDBY = tender.UPDATEDBY;
                    tenderUpdate.UPDATEDON = tender.UPDATEDON;
                }
                else
                {
                    Context.MstTender.Add(tender);                     
                }
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateTender(MstTender tender)
        {
            try
            {
                Context.Entry<MstTender>(tender).State = EntityState.Modified;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTender(MstTender tender)
        {
            try
            {
                Context.MstTender.Remove(tender);
                Context.Entry<MstTender>(tender).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteByID(string tenderID,string sitecode)
        {
            try
            {
                var tender = Context.MstTender.Where(u => u.TenderHeadCode == tenderID && u.SiteCode==sitecode && u.STATUS==true ).FirstOrDefault();
                tender.STATUS = false;
                //Context.MstTender.Remove(tender);
                //Context.Entry<MstTender>(tender).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTender> GetTenderList()
        {
            try
            {
                return Context.MstTender.Where(x => x.SiteCode==CommonModel.SiteCode && x.STATUS==true).OrderBy(a => a.TenderHeadCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstTender GetTenderByID(string tenderID)
        {
            try
            {
                return Context.MstTender.Where(u => u.TenderHeadCode == tenderID && u.STATUS == true && u.SiteCode==CommonModel.SiteCode ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstTender GetDeactiveTenderByID(string tenderID)
        {
            try
            {
                return Context.MstTender.Where(u => u.TenderHeadCode == tenderID  && u.SiteCode == CommonModel.SiteCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
