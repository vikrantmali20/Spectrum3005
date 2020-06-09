using System.Linq;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Spectrum.BL
{
    public class TenderManager : GenericManager<TenderModel>, ITenderManager
    {
        public TenderManager()
        {
            this.tenderRepository =new TenderRepository();
            Mapper.CreateMap<TenderModel, MstTender>();
            Mapper.CreateMap<MstTender, TenderModel>();
            Mapper.CreateMap<TenderModelList, MstTender>();
            Mapper.CreateMap<MstTender, TenderModelList>();
        }
        private ITenderRepository tenderRepository;

        public bool SaveTender(TenderModel tenderModel)
        {
            tenderModel.ToAddOrModifyEntity(true);
            var tender = Mapper.Map(tenderModel, new MstTender());            

            return this.tenderRepository.SaveTender(tender);
        }
        public bool UpdateTender(TenderModel tenderModel)
        {
            MstTender tender = this.tenderRepository.GetTenderByID(tenderModel.TenderHeadCode);

            return this.tenderRepository.UpdateTender(tender);
        }

        public bool DeleteByID(string tenderID, string sitecode)
        {
            return this.tenderRepository.DeleteByID(tenderID, sitecode);
        }

        public IQueryable<TenderModelList> GetTenderList()
        {
            var tenderList = this.tenderRepository.GetTenderList();
            var tenderModelList = new List<TenderModelList>();

            for (int i = 0; i < tenderList.Count() ; i++)
            {
                tenderModelList.Add(Mapper.Map(tenderList.ToList()[i], new TenderModelList()));
            }

            return tenderModelList.AsQueryable();
        }
        public TenderModel GetTenderByID(string tenderID)
        {
            var tender = this.tenderRepository.GetTenderByID(tenderID);

            return (tender != null) ? Mapper.Map(tender, new TenderModel()) : null;
        }

    }
}
