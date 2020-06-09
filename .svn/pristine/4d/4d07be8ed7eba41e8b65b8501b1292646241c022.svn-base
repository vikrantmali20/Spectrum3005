using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrum.DAL.RepositoryInterfaces
{
   public interface IPromotionRepository:IGenericRepository<ManualPromotion>
    {
        
        IList<ManualPromotion> GetManualPromotions();
        bool UpdateManualPromotions(ManualPromotion manualPromotion, PromotionSiteMap promoSiteMap);
        bool AddManualPromotions(ManualPromotion manualPromotion, PromotionSiteMap promoSiteMap);
        ManualPromotion GetPromotionById(string PromotionId);
    }
}
