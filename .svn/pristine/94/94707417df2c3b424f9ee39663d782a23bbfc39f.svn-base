using System.Linq;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using AutoMapper;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;
namespace Spectrum.BL
{
    public class TaxManager : GenericManager<TaxModel>, ITaxManager
    {
        public TaxManager()
        {
            this.taxRepository = new TaxRepository();
            Mapper.CreateMap<MstTax, TaxModel>();
            Mapper.CreateMap<TaxModel, MstTax>();
            Mapper.CreateMap<TaxSiteMapping, TaxSiteMappingModel>();
            Mapper.CreateMap<TaxSiteMappingModel, TaxSiteMapping>();
            Mapper.CreateMap<TaxSiteDocMap, TaxSiteDocMapModel>();
            Mapper.CreateMap<TaxSiteDocMapModel, TaxSiteDocMap>(); 
        }

        private ITaxRepository taxRepository;


        public bool SaveTax(TaxModel taxModel, TaxSiteMappingModel taxSiteMapModel, TaxSiteDocMapModel taxSiteDocMapModel)
        {
            taxModel.ToAddOrModifyEntity(true);
            taxSiteMapModel.ToAddOrModifyEntity(true);
            taxSiteDocMapModel.ToAddOrModifyEntity(true);
            var tax = Mapper.Map(taxModel, new MstTax());
            var taxSiteMap = Mapper.Map(taxSiteMapModel, new TaxSiteMapping());
            var taxSiteDocMap = Mapper.Map(taxSiteDocMapModel, new TaxSiteDocMap());
            return this.taxRepository.SaveTax(tax, taxSiteMap, taxSiteDocMap);
        }
        public bool UpdateTax(TaxModel taxModel, TaxSiteDocMapModel taxsitedocMapModel)
        {
            taxModel.ToAddOrModifyEntity(true);
            var tax = Mapper.Map(taxModel, new MstTax());
            taxsitedocMapModel.ToAddOrModifyEntity(true);
            var taxsitedocMap = Mapper.Map(taxsitedocMapModel, new TaxSiteDocMap());

            return this.taxRepository.UpdateTax(tax, taxsitedocMap);
        }

        public bool UpdateTaxDoc(TaxModel taxModel, TaxSiteDocMapModel taxsitedocMapModel)
        {
            taxModel.ToAddOrModifyEntity(true);
            var tax = Mapper.Map(taxModel, new MstTax());
            taxsitedocMapModel.ToAddOrModifyEntity(true);
            var taxsitedocMap = Mapper.Map(taxsitedocMapModel, new TaxSiteDocMap());
            return this.taxRepository.UpdateTaxSiteDoc(tax, taxsitedocMap);
            //UpdateTaxSiteDoc
        }
        public bool DeleteByID(string taxID, string docID)
        {
            return this.taxRepository.DeleteByID(taxID, docID);
        }

        public IQueryable<TaxModel> GetTaxList()
        {
            var taxList = this.taxRepository.GetTaxList().ToList();

            var taxModelList = (from t in taxList
                                select Mapper.Map(t, new TaxModel())).ToList();
         
            return taxModelList.AsQueryable();
        }

        public IQueryable<TaxSiteDocMapModel> GetTaxDoc()
        {

            var taxList = this.taxRepository.GetTaxDoc().ToList();

            var taxModelList = (from t in taxList

                                select Mapper.Map(t, new TaxSiteDocMapModel())).ToList();

            return taxModelList.AsQueryable();
        }


        public TaxModel GetTaxByID(string taxID)
        {
            var tax = this.taxRepository.GetTaxByID(taxID);
            var taxModel = Mapper.Map(tax, new TaxModel());
            return taxModel;
        }

        public TaxSiteDocMapModel GetTaxByDoc(string taxID, string docID)
        {
            var tax = this.taxRepository.GetTaxByDoc(taxID, docID);
            var taxSiteDocMapModel = Mapper.Map(tax, new TaxSiteDocMapModel());
            return taxSiteDocMapModel;
        }

        public bool DuplicateRecords(string taxName, string docID, string taxCode)
        {
            // return this.taxRepository.DuplicateRecords(taxName, docID);
            return this.taxRepository.DuplicateRecords(taxName, docID, taxCode);
        }
        
    }
}
