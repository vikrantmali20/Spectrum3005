using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System.Data.Entity.Validation;
namespace Spectrum.DAL
{
    public class SiteRepository : GenericRepository<MstSite>, ISiteRepository
    {
        public bool SaveSite(MstSite site , MstFinYear finYear,MstAreaCode areaCode,MstCurrency currency,MstSiteCurrancyMap siteCurrency)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var IsCountryExist = Context.MstAreaCode.Where(x => x.AreaCode == areaCode.AreaCode).FirstOrDefault();
                    if (IsCountryExist == null)
                        Context.MstAreaCode.Add(areaCode);
                    var IsCurrencyExist = Context.MstCurrency.Where(x => x.CurrencyCode == currency.CurrencyCode).FirstOrDefault();
                    if (IsCurrencyExist == null)
                        Context.MstCurrency.Add(currency);                    
                    Context.MstSite.Add(site);
                    Context.MstFinYear.Add(finYear);  
                    Context.MstSiteCurrancyMap.Add(siteCurrency);
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
                    foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                    {
                        foreach (DbValidationError error in entityErr.ValidationErrors)
                        {
                            Logging.Logger.Log("Error Property Name " + error.PropertyName + " : Error Message: " + error.ErrorMessage  ,Logging.Logger.LogingLevel.Error);
                            Logging.Logger.Log(dbEx, Logging.Logger.LogingLevel.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ex;
                }
            }
            return tranResult;
        }
        public bool UpdateSite(MstSite site,MstFinYear finYear)
        {
              bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var existSite = GetSiteByID(site.SiteCode);
                    existSite.SiteOfficialName = site.SiteOfficialName;
                    existSite.SiteShortName = site.SiteShortName;
                    existSite.SiteAddressLn1 = site.SiteAddressLn1;
                    existSite.SiteAddressLn2 = site.SiteAddressLn2;
                    existSite.SiteAddressLn3 = site.SiteAddressLn3;
                    existSite.ContactPerson = site.ContactPerson;
                    existSite.CentralSalesTaxNo = site.CentralSalesTaxNo;
                    existSite.EmailId = site.EmailId;
                    existSite.SiteTelephone1 = site.SiteTelephone1;
                    existSite.LocalSalesTaxNo = site.LocalSalesTaxNo;
                    existSite.FaxNo = site.FaxNo;
                    existSite.CountryCode = site.CountryCode;
                    existSite.LocalCurrancyCode = site.LocalCurrancyCode;
                    existSite.UPDATEDON = site.UPDATEDON;
                    existSite.UPDATEDAT = site.UPDATEDAT;
                    existSite.UPDATEDBY = site.UPDATEDBY;
                    Context.Entry(existSite).State = EntityState.Modified;

                    var IsFinyearExist = GetFinYearDetailsBySiteID(site.SiteCode);
                    if (IsFinyearExist == null)
                        Context.MstFinYear.Add(finYear);
                     
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                catch (DbEntityValidationException dbEx)
                {
                    dbContextTransaction.Rollback();
                    foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                    {
                        foreach (DbValidationError error in entityErr.ValidationErrors)
                        {
                            Logging.Logger.Log("Error Property Name " + error.PropertyName + " : Error Message: " + error.ErrorMessage, Logging.Logger.LogingLevel.Error);
                            Logging.Logger.Log(dbEx, Logging.Logger.LogingLevel.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ex;
                }
            }
            return tranResult;
        }
        public void DeleteSite(MstSite site)
        {
            try
            {
                Context.MstSite.Remove(site);
                Context.Entry<MstSite>(site).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteByID(string siteID)
        {
            try
            {
                var site = Context.MstSite.Where(u => u.SiteCode == siteID).FirstOrDefault();

                Context.MstSite.Remove(site);
                Context.Entry<MstSite>(site).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstSite> GetSiteList()
        {
            try
            {
                return Context.MstSite.Where(a => a.BusinessCode == "Store").OrderBy(a => a.SiteCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<SiteModelEditList> GetSiteEditList()
        {
            try
            {
                var siteDetails=(from s in Context.MstSite 
                                 select new SiteModelEditList
                                 {
                                     SiteCode = s.SiteCode ,
                                     SiteName = s.SiteShortName,
                                     EmailId  = s.EmailId,
                                     ContactPerson = s.ContactPerson 

                                 }).Distinct().AsQueryable();
                return siteDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstSite GetSiteByID(string siteID)
        {
            try
            {
                return Context.MstSite.Where(u => u.SiteCode == siteID ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstFinYear GetFinYearDetailsBySiteID(string siteID)
        {
            try
            {
                return Context.MstFinYear.Where(u => u.SiteCode == siteID && u.STATUS==true && u.FinStatus==true ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstCurrency GetCurrencySymbol(string currencyCode)
        {
            try
            {
                return Context.MstCurrency.Where(u => u.CurrencyCode == currencyCode && u.STATUS == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
