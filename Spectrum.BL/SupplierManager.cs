using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.BL.BusinessInterface;
using Spectrum.Models;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using AutoMapper;
using Spectrum.BL.Mappers;

namespace Spectrum.BL
{
    public class SupplierManager : GenericManager<SupplierModel>, ISupplierManager
    {
        public SupplierManager()
        {
            this.supplierRepository = new SupplierRepository();
            this.commonRepository = new CommonRepository();

            Mapper.CreateMap<SupplierModel, MstSupplier>();
            Mapper.CreateMap<MstSupplier, SupplierModel>();
        }

        private ISupplierRepository supplierRepository;
        private ICommonRepository commonRepository;

        /// <summary>
        /// Add supplier to the Database : if supplier code does not exist , create a 15 digit supplier code 
        /// </summary>
        /// <param name="supplierModel"></param>
        /// <returns>Sucuss/Fail</returns>
        public bool SaveSupplier(SupplierModel supplierModel, ref string autoSupplierCode)
        {
            try
            {
                //if (supplierModel.IsAutoNumber)
                //{
                //    int nextNo = commonRepository.GetNextID(CommonModel.SiteCode, "SU");
                //    /// Format Supplier code into 15 digit ...
                //    supplierModel.SupplierCode = string.Format("0000{0}", nextNo.ToString().PadLeft(11, '0'));
                //    autoSupplierCode = supplierModel.SupplierCode;
                //}
                supplierModel.ToAddOrModifyEntity(true);
                var supplier = Mapper.Map(supplierModel, new MstSupplier());

                SiteSupplierMap siteSupplierMap = new SiteSupplierMap();
                siteSupplierMap.SiteCode = CommonModel.SiteCode;
                siteSupplierMap.SupplierCode = supplier.SupplierCode;
                siteSupplierMap.ToAddOrModifyEntity(true);
                return this.supplierRepository.SaveSupplier(supplier, siteSupplierMap, CommonModel.SiteCode, supplierModel.IsAutoNumber);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSupplier(SupplierModel supplierModel)
        {
            MstSupplier supplier = this.supplierRepository.GetSupplierByID(supplierModel.SupplierCode);
            bool isStatus = supplier.STATUS ?? false;
            Mapper.Map(supplierModel, supplier);
            supplier.STATUS = isStatus;
            return this.supplierRepository.UpdateSupplier(supplier);
        }

        /// <summary>
        ///  delete supplier from database of requested supplier Id ...
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>Sucuss/Fail</returns>
        public bool DeleteByID(string supplierID)
        {
            return this.supplierRepository.DeleteByID(supplierID);
        }

        /// <summary>
        /// IList of Supplier from database
        /// </summary>
        /// <returns>IList of Supplier </returns>
        public IQueryable<Supplier> GetSupplierList()
        {
            try
            {
                return this.supplierRepository.GetSuppliers();

                //var supplierModelList = new List<Supplier>();
                //supplierModelList = (List<Supplier>)(supplierList.ToList().ToModels(typeof(Supplier)));
                //return supplierModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///// <summary>
        ///// IList of Supplier from database
        ///// </summary>
        ///// <returns>IList of Supplier </returns>
        //public IQueryable<Supplier> GetSupplierList()
        //{
        //    try
        //    {
        //        var supplierList = this.supplierRepository.GetSupplierList();

        //        var supplierModelList = new List<Supplier>();
        //        supplierModelList = (List<Supplier>)(supplierList.ToList().ToModels(typeof(Supplier)));
        //        return supplierModelList.AsQueryable();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw ex;
        //    }
        //}


        /// <summary>
        ///  Get the supplier details from database of requested supplier Id .
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public SupplierModel GetSupplierByID(string supplierID)
        {
            var supplier = this.supplierRepository.GetSupplierByID(supplierID);
            return (supplier != null) ? Mapper.Map(supplier, new SupplierModel()) : null;
        }

    }
}
