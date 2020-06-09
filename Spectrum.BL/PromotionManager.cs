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
    public class PromotionManager : GenericManager<PromotionModel>, IPromotionManager
    {
        public PromotionManager()
        {
            this.promotionRepository = new PromotionRepository();
            Mapper.CreateMap<ManualPromotion, ManualPromotionModel>();
            Mapper.CreateMap<ManualPromotionModelEdit, ManualPromotion>();
            Mapper.CreateMap<ManualPromotion, ManualPromotionModelEdit>();
            Mapper.CreateMap<ManualPromotionModel, ManualPromotion>();
            Mapper.CreateMap<PromotionSiteMap, PromotionSiteMapModel>();
            Mapper.CreateMap<PromotionSiteMapModel, PromotionSiteMap>();

        }

        private IPromotionRepository promotionRepository;

        public ManualPromotionModel GetPromotionById(string PromotionId)
        {
            var promoList = this.promotionRepository.GetPromotionById(PromotionId);
            var promoModel = Mapper.Map(promoList, new ManualPromotionModel());
            return promoModel;
        }

        public IList<ManualPromotionModelEdit> GetManualPromotions()
        {
            var promoList = this.promotionRepository.GetManualPromotions().ToList();

            var promoModelList = (from t in promoList
                                  select Mapper.Map(t, new ManualPromotionModelEdit())).ToList();


            return promoModelList;
        }
        public bool UpdateManualPromotions(PromotionModel promotionModel)
        {

            var promotion = Mapper.Map(promotionModel.ManualPromotionModel, new ManualPromotion());
            var promotionSiteMap = Mapper.Map(promotionModel.PromotionSiteMapModel, new PromotionSiteMap());

            return this.promotionRepository.UpdateManualPromotions(promotion, promotionSiteMap);
        }
        public bool AddManualPromotions(PromotionModel promotionModel)
        {

            var promotion = Mapper.Map(promotionModel.ManualPromotionModel, new ManualPromotion());
            var promotionSiteMap = Mapper.Map(promotionModel.PromotionSiteMapModel, new PromotionSiteMap());
            return this.promotionRepository.AddManualPromotions(promotion, promotionSiteMap);
        }

    }
}
