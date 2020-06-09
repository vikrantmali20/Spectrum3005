using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;

namespace Spectrum.BL.BusinessInterface
{
    public interface ISupplierManager : IGenericManager<SupplierModel>
    {
        bool SaveSupplier(SupplierModel supplierModel,ref string autoSupplierCode);
        bool UpdateSupplier(SupplierModel supplierModel);
        bool DeleteByID(string supplierID);

        IQueryable<Supplier> GetSupplierList();
        SupplierModel GetSupplierByID(string supplierID);
    }
}
