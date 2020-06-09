using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;

namespace Spectrum.DAL
{
    public class TaxRepository : GenericRepository<MstTax>, ITaxRepository
    {
        public bool SaveTax(MstTax tax,TaxSiteMapping taxSiteMap,TaxSiteDocMap taxsitedocmap)
        {
            using (var context = ContextFactory.CreateContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {

                        Context.MstTax.Add(tax);
                        Context.TaxSiteMapping.Add(taxSiteMap);
                        Context.TaxSiteDocMap.Add(taxsitedocmap);
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

        public bool UpdateTax(MstTax tax, TaxSiteDocMap taxsitedocmap)
        {
            try
            {
                taxsitedocmap.SiteCode = CommonModel.SiteCode;
                taxsitedocmap.TaxName = tax.TaxName;
                taxsitedocmap.IsDocumentLevelTax = false;


                Context.TaxSiteDocMap.Add(taxsitedocmap);
                Context.SaveChanges();

                MstTax existTax = Context.MstTax .Where(a => a.TaxCode  == tax.TaxCode && a.STATUS==true ).FirstOrDefault();
                existTax.TaxName = tax.TaxName;
                existTax.Value = tax.Value;
                existTax.Inclusive = tax.Inclusive;
                existTax.Type = tax.Type;
                existTax.UPDATEDON = DateTime.Now;
                existTax.UPDATEDAT = tax.UPDATEDAT;
                existTax.UPDATEDBY = tax.UPDATEDBY;
                existTax.InterStateTax = tax.InterStateTax;
                Context.Entry<MstTax>(existTax).State = EntityState.Modified;
                Context.SaveChanges();
               // Context.Entry<MstTax>(tax).State = EntityState.Modified;
             

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateTaxSiteDoc(MstTax tax, TaxSiteDocMap taxsitedocmap)
        {
            try
            {

                TaxSiteDocMap existDoc = Context.TaxSiteDocMap.Where(a => a.TaxCode == taxsitedocmap.TaxCode && a.STATUS == true && a.SiteCode == CommonModel.SiteCode && a.DocumentType == taxsitedocmap.DocumentType).FirstOrDefault();
                existDoc.TaxName = taxsitedocmap.TaxName;
                existDoc.TaxValue = taxsitedocmap.TaxValue;
                existDoc.Inclusive = taxsitedocmap.Inclusive;
                existDoc.TaxCode = taxsitedocmap.TaxCode;
                existDoc.TaxType = taxsitedocmap.TaxType;
                existDoc.IsDocumentLevelTax = taxsitedocmap.IsDocumentLevelTax;
                existDoc.IsPercentageValue = taxsitedocmap.IsPercentageValue;
                existDoc.Appliedon = taxsitedocmap.Appliedon;
                existDoc.DocumentType = taxsitedocmap.DocumentType;
                existDoc.UPDATEDON = DateTime.Now;
                existDoc.UPDATEDAT = taxsitedocmap.UPDATEDAT;
                existDoc.UPDATEDBY = taxsitedocmap.UPDATEDBY;

                taxsitedocmap.SiteCode = CommonModel.SiteCode;
                // taxsitedocmap.TaxName = tax.TaxName;
                //taxsitedocmap.IsDocumentLevelTax = false;

                Context.Entry<TaxSiteDocMap>(existDoc).State = EntityState.Modified;
                //Context.TaxSiteDocMap.Add(taxsitedocmap);
                Context.SaveChanges();

                MstTax existTax = Context.MstTax.Where(a => a.TaxCode == tax.TaxCode && a.STATUS == true).FirstOrDefault();
                if (existTax.TaxCode == tax.TaxCode)
                {
                    existTax.TaxName = tax.TaxName;
                    existTax.Value = tax.Value;
                    existTax.Inclusive = tax.Inclusive;
                    existTax.Type = tax.Type;
                    existTax.UPDATEDON = DateTime.Now;
                    existTax.UPDATEDAT = tax.UPDATEDAT;
                    existTax.UPDATEDBY = tax.UPDATEDBY;
                    existTax.InterStateTax = tax.InterStateTax;
                    Context.Entry<MstTax>(existTax).State = EntityState.Modified;
                    Context.SaveChanges();
                }



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteTax(MstTax tax)
        {
            try
            {
                Context.MstTax.Remove(tax);
                Context.Entry<MstTax>(tax).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteByID(string taxID,string docID)
        {
            try
            {
                var tax = Context.MstTax.Where(u => u.TaxCode == taxID).FirstOrDefault();
                var taxdoc = Context.TaxSiteDocMap.Where(u => u.TaxCode == taxID && u.DocumentType == docID).FirstOrDefault();
                taxdoc.STATUS = false;
                var taxcount = Context.TaxSiteDocMap.Where(u => u.TaxCode == taxID && u.STATUS == true).ToList();
                var DocCount = taxcount.Count;
                if (DocCount == 1)
                {
                    tax.STATUS = false;
                }
                //Context.MstTax.Remove(tax);
                //Context.Entry<MstTax>(tax).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DuplicateRecords(string taxName, string docID, string taxCode)
        {
            try
            {

                var taxcount = Context.TaxSiteDocMap.Where(u => u.TaxName == taxName && u.STATUS == true && u.DocumentType == docID && u.TaxCode==taxCode ).ToList();
                var DocCount = taxcount.Count;
                if (DocCount >= 1)
                {
                    Context.SaveChanges();
                    return true;
                }
                else

                    Context.SaveChanges();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTax> GetTaxList()
        {
            try
            {
                return Context.MstTax.Where(x=>x.STATUS==true).OrderBy(a => a.TaxCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TaxSiteDocMap> GetTaxDoc()
        {
            try
            {
                return Context.TaxSiteDocMap.Where(x => x.STATUS == true).OrderBy(a => a.TaxCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MstTax GetTaxByID(string taxID)
        {
            try
            {
                return Context.MstTax.Where(u => u.TaxCode == taxID && u.STATUS == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaxSiteDocMap GetTaxByDoc(string taxID, string docID)
        {
            try
            {

                return Context.TaxSiteDocMap.Where(u => u.TaxCode == taxID && u.STATUS == true && u.DocumentType == docID).FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
