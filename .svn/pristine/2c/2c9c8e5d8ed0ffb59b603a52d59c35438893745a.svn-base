using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;

namespace Spectrum.DAL
{
    public class SupplierRepository : GenericRepository<MstSupplier>, ISupplierRepository
    {
        /// <summary>
        ///  add supplier model to database ..if Autonumber is generated then update next no to GLNoRangeObjects for this obectid and site
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="siteCode"> for updating auto Number </param>
        /// <param name="isAutoNumber"> Whether code is manual or auto </param>
        /// <returns></returns>
        public bool SaveSupplier(MstSupplier supplier, SiteSupplierMap siteSupplierMap, string siteCode, bool isAutoNumber)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.MstSupplier.Add(supplier);
                    Context.SiteSupplierMap.Add(siteSupplierMap);
                    Context.SaveChanges();

                    //if (isAutoNumber)
                    //    this.UpdateNextID(siteCode, "SU");

                    Context.SaveChanges();
                    dbContextTransaction.Commit();

                    tranResult = true;
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

        public bool UpdateSupplier(MstSupplier supplier)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Entry<MstSupplier>(supplier).State = EntityState.Modified;
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;
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
        public Boolean DeleteSupplier(MstSupplier supplier)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.MstSupplier.Remove(supplier);
                    Context.Entry<MstSupplier>(supplier).State = EntityState.Deleted;
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;
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

        public bool DeleteByID(string supplierID)
        {
            try
            {
                var supplier = Context.MstSupplier.Where(u => u.SupplierCode == supplierID).FirstOrDefault();

                Context.MstSupplier.Remove(supplier);
                Context.Entry<MstSupplier>(supplier).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<MstSupplier> GetSupplierList()
        {
            try
            {
                return Context.MstSupplier.OrderBy(a => a.SupplierName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstSupplier> GetSupplierListbySite()
        {
            try
            {
                return Context.MstSupplier.OrderBy(a => a.SupplierName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get list of Supplier 
        /// </summary>
        /// <returns>IList<Supplier> </returns>
        public IQueryable<Supplier> GetSuppliers()
        {
            try
            {
                var supplierList = (from s in Context.MstSupplier
                                    where s.STATUS == true
                                    orderby s.SupplierName
                                    select new Supplier
                                    {
                                        //Select = false ,
                                        Code = s.SupplierCode,
                                        Name = s.SupplierName,
                                        EmailID = s.EmailId,
                                        IsActive = s.isActive ?? false
                                        //,Filter = (s.SupplierCode ?? "") + (s.SupplierName ?? "") + (s.EmailId ?? "")
                                    }).AsQueryable();

                return supplierList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MstSupplier GetSupplierByID(string supplierID)
        {
            try
            {
                return Context.MstSupplier.Where(u => u.SupplierCode == supplierID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
