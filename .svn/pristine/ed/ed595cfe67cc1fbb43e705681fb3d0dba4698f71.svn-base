using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Spectrum.Models;
//using Spectrum.BL.BusinessInterface;
//using Spectrum.DAL;
//using AutoMapper;
//using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using AutoMapper;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL.CustomEntities;
namespace Spectrum.BL
{
    public class CommonManager : GenericManager<AreaCodeModel>, ICommonManager
    {
        public CommonManager()
        {
            this.commonRepository = new CommonRepository();
            this.articleHierarchyRepository = new ArticleHierarchyRepository();

            Mapper.CreateMap<AreaCodeModel, MstAreaCode>();
            Mapper.CreateMap<MstAreaCode, AreaCodeModel>();

            Mapper.CreateMap<MasterTypeModel, GeneralCodeMst>();
            Mapper.CreateMap<GeneralCodeMst, MasterTypeModel>();

            Mapper.CreateMap<ArticleTypeModel, MstArticleType>();
            Mapper.CreateMap<MstArticleType, ArticleTypeModel>();
            
            Mapper.CreateMap<UoMModel, MstUoM>();
            Mapper.CreateMap<MstUoM, UoMModel>();

            Mapper.CreateMap<ManufacturerModel , Manufacturer>();
            Mapper.CreateMap<Manufacturer, ManufacturerModel>();

            Mapper.CreateMap<BrandModel , Brand>();
            Mapper.CreateMap<Brand, BrandModel>();

            Mapper.CreateMap<InvoiceModel , Invoice >();
            Mapper.CreateMap<Invoice, InvoiceModel>();

            Mapper.CreateMap<OrderDtlModel , OrderDtl >();
            Mapper.CreateMap<OrderDtl , OrderDtlModel >();

            Mapper.CreateMap<OrderHdrModel ,OrderHdr >();
            Mapper.CreateMap<OrderHdr , OrderHdrModel >();

            Mapper.CreateMap<MstTenderTypeModel, MstTenderType >();
            Mapper.CreateMap<MstTenderType, MstTenderTypeModel>();

            Mapper.CreateMap<MstCLPProgramModel, MstCLPProgram>();
            Mapper.CreateMap<MstCLPProgram, MstCLPProgramModel>();

            Mapper.CreateMap<MstVoucherModel, MstVoucher>();
            Mapper.CreateMap<MstVoucher, MstVoucherModel>();

            Mapper.CreateMap<MstRoleModel,MstRole>();
            Mapper.CreateMap<MstRole, MstRoleModel>();

            Mapper.CreateMap<MstSiteModel, MstSite>();
            Mapper.CreateMap<MstSite, MstSiteModel>();

            Mapper.CreateMap<MstBusinessModel, MstBusiness>();
            Mapper.CreateMap<MstBusiness, MstBusinessModel>();

            Mapper.CreateMap<MstEANModel, MstEAN>();
            Mapper.CreateMap<MstEAN, MstEANModel>();

            Mapper.CreateMap<MstLanguageModel, MstLanguage>();
            Mapper.CreateMap<MstLanguage, MstLanguageModel>();

            Mapper.CreateMap<MstCurrencyModel, MstCurrency>();
            Mapper.CreateMap<MstCurrency, MstCurrencyModel>();

            Mapper.CreateMap<MstSupplierModel, MstSupplier>();
            Mapper.CreateMap<MstSupplier, MstSupplierModel>();

            Mapper.CreateMap<MstCharacteristics, DropDownModel>();
        }
        
        private ICommonRepository commonRepository;
        private IArticleHierarchyRepository articleHierarchyRepository;


        //public IList<AreaCodeModel> GetAreaCodeList()
        //{
        //    try
        //    {
        //        var areaCodeList = this.commonRepository.GetAreaCodeList();
        //        var areaCodeModelList = new List<AreaCodeModel>();

        //        for (int i = 0; i < areaCodeList.Count() - 1; i++)
        //        {
        //            areaCodeModelList.Add(Mapper.Map(areaCodeList.ToList()[i], new AreaCodeModel()));
        //        }
        //        return areaCodeModelList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// getting the next no for showing on screen ...
        /// </summary>
        /// <param name="tranObjId"> objectId of Transaction</param>
        /// <returns>Next NO</returns>
        public Int64 GetTranNextNo(string tranObjId)
        {
            return commonRepository.GetNextID(CommonModel.SiteCode, tranObjId);
        }



        public IQueryable<AreaCodeModel> GetAreaCodeList()
        {
            try
            {
                var areaCodeList = this.commonRepository.GetAreaCodeList();
                var areaCodeModelList = new List<AreaCodeModel>();

                for (int i = 0; i < areaCodeList.Count() - 1; i++)
                {
                    areaCodeModelList.Add(Mapper.Map(areaCodeList.ToList()[i], new AreaCodeModel()));
                }
                return areaCodeModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<MasterTypeModel> GetMasterTypeList()
        {
            try
            {
                var masterTypeList = this.commonRepository.GetMasterTypeList();
                var masterTypeModelList = new List<MasterTypeModel>();

                for (int i = 0; i < masterTypeList.Count(); i++)
                {
                    masterTypeModelList.Add(Mapper.Map(masterTypeList.ToList()[i], new MasterTypeModel()));
                }
                return masterTypeModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<ArticleTypeModel> GetArticleTypeList()
        {
            try
            {
                var articleTypeList = this.commonRepository.GetArticleTypeList().ToList();
                var articleTypeModelList =( from r in articleTypeList select Mapper.Map(r, new ArticleTypeModel())).ToList();
                return articleTypeModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<UoMModel> GetUoMList()
        {
            try
            {
                var UoMList = this.commonRepository.GetUoMList().ToList();
                var UoMModelList = (from r in UoMList select Mapper.Map(r, new UoMModel())).ToList();
                return UoMModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<ManufacturerModel> GetManufacturerList()
        {
            try
            {
                var manufacturerList = this.commonRepository.GetManufacturerList().ToList();
                var ManufacturerModelList = (from r in manufacturerList select Mapper.Map(r, new ManufacturerModel())).ToList();
                return ManufacturerModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<BrandModel> GetBrandList()
        {
            try
            {
                var BrandList = this.commonRepository.GetBrandList().ToList();
                var BrandModelList = (from r in BrandList select Mapper.Map(r, new BrandModel())).ToList();
                return BrandModelList.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstTenderTypeModel> GetTenderTypeList()
        {
            try
            {
                var tendtypemst = this.commonRepository.GetTenderTypeList().ToList();
                var tendtypemodelmst = (from r in tendtypemst select Mapper.Map(r, new MstTenderTypeModel())).ToList();
                return tendtypemodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstCLPProgramModel> GetClPbyTenderType()
        {
            try
            {
                var clptypemst = this.commonRepository.GetClPbyTenderType().ToList();
                var clptypemodelmst = (from r in clptypemst select Mapper.Map(r, new MstCLPProgramModel())).ToList();
                return clptypemodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstCLPProgramModel> getclpprogram(string sitecode)
        {
            try
            {
                var clptypemst = this.commonRepository.getclpprogram(sitecode).ToList();

                return clptypemst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }    
        public IQueryable<MstVoucherModel> GetVouchers(string vouchertype)
        {
            try
            {
                var vouchers = this.commonRepository.GetVouchers(vouchertype).ToList();
                var vouchermodelmst = (from r in vouchers select Mapper.Map(r, new MstVoucherModel())).ToList();
                return vouchermodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstRoleModel> GetRoles()
        {
            try
            {
                var roles = this.commonRepository.GetRoles().ToList();
                var rolesmodelmst = (from r in roles select Mapper.Map(r, new MstRoleModel())).ToList();
                return rolesmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstSiteModel> GetSiteList()
        {
            try
            {
                var sites = this.commonRepository.GetSiteList().ToList();
                var sitemodelmst = (from r in sites select Mapper.Map(r, new MstSiteModel())).ToList();
                return sitemodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
         
        public IQueryable<MstBusinessModel> GetBusinessDetails()
        {
            try
            {
                var business = this.commonRepository.GetBusinessDetails().ToList();
                var businessmodelmst = (from b in business select Mapper.Map(b, new MstBusinessModel())).ToList();
                return businessmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstLanguageModel> GetLanguageList()
        {
            try
            {
                var languages = this.commonRepository.GetLanguageList().ToList();
                var languagesmodelmst = (from b in languages select Mapper.Map(b, new MstLanguageModel())).ToList();
                return languagesmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstEANModel> GetDefaultEAN()
        {
            try
            {
                var ean = this.commonRepository.GetDefaultEAN().ToList();
                var eanmodelmst = (from b in ean select Mapper.Map(b, new MstEANModel())).ToList();
                return eanmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IQueryable<MstEANModel> GetDefaultEANbyArticle(string articleCode)
        {
            try
            {
                var ean = this.commonRepository.GetDefaultEANbyArticle(articleCode).ToList();
                var eanmodelmst = (from b in ean select Mapper.Map(b, new MstEANModel())).ToList();
                return eanmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
        public IQueryable<MstCurrencyModel> GetCurrency()
        {
            try
            {
                var currency = this.commonRepository.GetCurrency().ToList();
                var currmodelmst = (from c in currency select Mapper.Map(c, new MstCurrencyModel())).ToList();
                return currmodelmst.AsQueryable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
         

        public IQueryable<CharacteristicsModel> GetCharacteristicsList()
        {
            try
            {
                return this.commonRepository.GetCharacteristicsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IList<DropDownModel> GetStockOutReasons()
        {
            try
            {
                return this.commonRepository.GetStockOutReasons();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public IList<DropDownModel> GetFromLocation()
        {
            try
            {
                return this.commonRepository.GetFromLocation();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public int GetNextID(string siteCode, string objectID)
        {
            return this.commonRepository.GetNextID(siteCode, objectID);
        }
        public void UpdateNextID(string siteCode, string objectID)
        {
            try
            {
                  this.commonRepository.UpdateNextID(siteCode, objectID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        public IQueryable<MstSupplierModel> GetSupplierBySite(string SiteCode)
        {
            try
            {

                var supplier = this.commonRepository.GetSupplierBySite(SiteCode).ToList();

                return supplier.AsQueryable();
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<MstDocumentTypeModel> GetDocumentType(string SiteCode)
        {
            try
            {

                var supplier = this.commonRepository.GetDocumentType(SiteCode).ToList();

                return supplier.AsQueryable();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public IQueryable<GeneralCodeMstModel> GetAppliedOn(string SiteCode)
        {
            try
            {

                var supplier = this.commonRepository.GetAppliedOn(SiteCode).ToList();

                return supplier.AsQueryable();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //ashma - 15 may 2018 for ShiftManagement
        public string GetSiteName()
        {
            try
            {
                string siteName = this.commonRepository.GetSiteName();

                return siteName;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //public bool Savestocksinothers(InvoiceModel invoicesModel, OrderDtlModel orddetmodel, OrderHdrModel  ordhdrmodel)
        //{
        //    // bool tranResult = false;
             
        //        try
        //        {
        //            invoicesModel.ToAddOrModifyEntity(true);
        //            orddetmodel.ToAddOrModifyEntity(true);
        //            ordhdrmodel.ToAddOrModifyEntity(true);
        //            var invoiceentity = Mapper.Map(invoicesModel , new Invoice ());
        //            var orderhdrentity = Mapper.Map(ordhdrmodel , new OrderHdr ());
        //            var orderdetentity = Mapper.Map(orddetmodel , new OrderDtl ());
        //            return this.commonRepository.Savestocksinothers(invoiceentity, orderdetentity, orderhdrentity);
                    
        //        }
        //        // return tranResult;
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            throw ex;
        //        }
            
        //}
        //public bool Savestocksoutothers( OrderDtlModel orddetmodel, OrderHdrModel ordhdrmodel)
        //{
        //    // bool tranResult = false;

        //    try
        //    {
        //        return this.Savestocksoutothers( orddetmodel, ordhdrmodel);
        //    }
        //    // return tranResult;
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw ex;
        //    }

        //}

    }
}
