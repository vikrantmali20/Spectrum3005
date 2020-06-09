using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;

namespace Spectrum.DAL
{
    public class PromotionRepository : GenericRepository<ManualPromotion>, IPromotionRepository 
    {
        public bool UpdateManualPromotions(ManualPromotion manualPromotion, PromotionSiteMap promoSiteMap)
        {

            using (var context = ContextFactory.CreateContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var existPromotion = GetPromotionById(manualPromotion.PromotionId);
                        existPromotion.PromotionName = manualPromotion.PromotionName;
                        existPromotion.PromotionValue = manualPromotion.PromotionValue;
                        existPromotion.DiscPer = manualPromotion.DiscPer;
                        existPromotion.FixedPriceOff = manualPromotion.FixedPriceOff;
                        existPromotion.FixedSelling = manualPromotion.FixedSelling;
                        existPromotion.UPDATEDON = DateTime.Now;
                        existPromotion.IsApproved = manualPromotion.IsApproved;
                        existPromotion.OfferActive = manualPromotion.OfferActive;
                        Context.Entry(existPromotion).State = EntityState.Modified; 
                        Context.SaveChanges();
                        dbTran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        context.Dispose();
                    }
                }
            }


        }
        public bool AddManualPromotions(ManualPromotion manualPromotion, PromotionSiteMap promoSiteMap)
        {

            using (var context = ContextFactory.CreateContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        Context.ManualPromotion.Add(manualPromotion);
                        Context.PromotionSiteMap.Add(promoSiteMap);
                        Context.SaveChanges();
                        dbTran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        context.Dispose();
                    }
                }
            }


        }
        public ManualPromotion GetPromotionById(string PromotionId)
        {
            try
            {
                return Context.ManualPromotion.Where(x => x.PromotionId == PromotionId).OrderBy(a => a.PromotionName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<ManualPromotion> GetManualPromotions()
        {
            try
            {
                return Context.ManualPromotion.Where(x => x.STATUS == true).OrderBy(a => a.PromotionName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
