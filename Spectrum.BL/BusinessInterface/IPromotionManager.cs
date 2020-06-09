using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
    public interface IPromotionManager : IGenericManager<PromotionModel>
    {
        IList<ManualPromotionModelEdit> GetManualPromotions();
        bool UpdateManualPromotions(PromotionModel promotionModel);
        bool AddManualPromotions(PromotionModel promotionModel);
        ManualPromotionModel GetPromotionById(string PromotionId);
    }
}
