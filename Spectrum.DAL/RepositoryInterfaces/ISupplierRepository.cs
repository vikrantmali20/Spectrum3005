using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;

namespace Spectrum.DAL.RepositoryInterfaces
{
     public interface ISupplierRepository : IGenericRepository<MstSupplier>
    {
         bool SaveSupplier(MstSupplier supplier, SiteSupplierMap siteSupplierMap, string siteCode, bool isAutoNumber);
        bool UpdateSupplier(MstSupplier supplier);
        Boolean DeleteSupplier(MstSupplier supplier);
        bool DeleteByID(string supplierID);

        IQueryable<MstSupplier> GetSupplierList();
        MstSupplier GetSupplierByID(string supplierID);
        IQueryable<Supplier> GetSuppliers();
    }
}
