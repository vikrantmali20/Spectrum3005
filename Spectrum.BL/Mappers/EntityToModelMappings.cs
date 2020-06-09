using AutoMapper;
using Spectrum.DAL;

namespace Spectrum.Models.Mappers
{
    public class EntityToModelMappings
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        public static void ConfigureMappers()
        {
            Mapper.CreateMap<SupplierModel, MstSupplier>();
            Mapper.CreateMap<MstSupplier, SupplierModel>();

            Mapper.CreateMap<SiteModel, MstSite>();
            Mapper.CreateMap<MstSite, SiteModel>();

            Mapper.CreateMap<TenderModel , MstTender>();
            Mapper.CreateMap<MstTender, TenderModel>();


            Mapper.CreateMap<ArticleDescInDiffLangModel, ArticleDescInDiffLang>();
            Mapper.CreateMap<ArticleDescInDiffLang, ArticleDescInDiffLangModel>();

            Mapper.CreateMap<MasterArticleMap, MasterArticleMapModel>();
            Mapper.CreateMap<MasterArticleMapModel, MasterArticleMap>();

            Mapper.CreateMap<ArticleModel, MstArticle>();
            Mapper.CreateMap<MstArticle, ArticleModel>();

            Mapper.CreateMap<ArticleImageModel, MstArticleImage>();
            Mapper.CreateMap<MstArticleImage, ArticleImageModel>();

            Mapper.CreateMap<EANModel, MstEAN>();
            Mapper.CreateMap<MstEAN, EANModel>();

            Mapper.CreateMap<SalesInfoRecord, SalesInfoRecordModel>();
            Mapper.CreateMap<SalesInfoRecordModel, SalesInfoRecord>();

            Mapper.CreateMap<PurchaseInfoRecordModel, PurchaseInfoRecord>();
            Mapper.CreateMap<PurchaseInfoRecord, PurchaseInfoRecordModel>();

            Mapper.CreateMap<SiteArticleTaxMapping, SiteArticleTaxMappingModel>();
            Mapper.CreateMap<SiteArticleTaxMappingModel, SiteArticleTaxMapping>();

            Mapper.CreateMap<ArticleModel, MstArticle>();
            Mapper.CreateMap<MstArticle, ArticleModel>();


        }

    }
}
