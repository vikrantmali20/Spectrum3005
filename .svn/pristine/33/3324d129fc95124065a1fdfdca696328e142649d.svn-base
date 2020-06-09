using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System.Data.Entity.Validation;
namespace Spectrum.DAL
{
    public class CommonRepository : GenericRepository<MstAreaCode>, ICommonRepository
    {
         /// <summary>
         /// Get Arealist( country , State , City ) 
         /// </summary>
         /// <returns></returns>
        public IQueryable<MstAreaCode> GetAreaCodeList()
        {
            try
            {
                return Context.MstAreaCode.OrderBy(a => a.AreaType).ThenBy(a => a.Description).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<GeneralCodeMst> GetMasterTypeList()
        {
            try
            {
                return Context.GeneralCodeMst.Where(a => a.CodeType.Contains("MSTEanType")).AsQueryable();
                //return Context.GeneralCodeMst.OrderBy(a => a.CodeType).ThenBy(a => a.ShortDesc).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<ItemHierarchy> GetItemHierarchyList()
        {
            try
            {
                return (from atnm in Context.ArticleTreeNodeMap join man in Context.MstArticleNode on atnm.Nodecode equals man.Nodecode
                                join pman in Context.MstArticleNode on atnm.ParentNodecode equals pman.Nodecode
                      into t
                        from rt in t.DefaultIfEmpty()
                        select new ItemHierarchy
                        {
                            Nodecode = atnm.Nodecode,
                            NodeName = man.NodeName,
                            ParentNodecode = atnm.ParentNodecode,
                            ParentNodeName = rt.NodeName,
                            TreeCode = atnm.Treecode,
                            ISThisLastNode = man.ISThisLastNode,
                            LevelCode = man.LevelCode
                        }).AsQueryable();
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<CharacteristicsModel> GetCharacteristicsList()
        {
            try
            {
                return (from CV in Context.CharacteristicsValue
                        join C in Context.MstCharacteristics on CV.CharCode equals C.CharCode
                        join CP in Context.CharacteristicsProfile on C.CharCode equals CP.CharCode
                        join P in Context.MstCharacteristicsProfile on CP.CharProfileCode equals P.ProfileCode
                 into t
                        from rt in t.DefaultIfEmpty()
                        select new CharacteristicsModel
                       {
                           ProfileCode = rt.ProfileCode,
                           ProfileName = rt.ProfileName,
                           CharCode = C.CharCode,
                           CharName = C.CharName,
                           CharValue = CV.CharValue
                       }).AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<MstArticleType> GetArticleTypeList()
        {
            try
            {
                return Context.MstArticleType.OrderByDescending(a => a.ArticalTypeName).AsQueryable();    // added  by vipin on 19-04-2017
              ///  return Context.MstArticleType.OrderBy(a => a.ArticalTypeName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<MstUoM> GetUoMList()
        {
            try
            {
                return Context.MstUoM.OrderBy(a => a.UOM).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<Manufacturer> GetManufacturerList()
        {
            try
            {
                return Context.Manufacturer.OrderBy(a => a.ManufacturerName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<Brand> GetBrandList()
        {
            try
            {
                return Context.Brand.OrderBy(a => a.BrandName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTenderType > GetTenderTypeList()
        {
            try
            {
                return Context.MstTenderType.OrderBy(a => a.TenderType).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstCLPProgram > GetClPbyTenderType()
        {
            try
            {
                return Context.MstCLPProgram.OrderBy(a => a.STATUS == true).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstCLPProgramModel> getclpprogram(string sitecode)
        {
            var clplist = (from atnm in Context.MstCLPProgram
                               join man in Context.CLPProgramSiteMap on atnm.CLPProgramId equals man.ClpProgramId 
                                 
                               where man.Sitecode == sitecode && man.STATUS==true 
                               select new MstCLPProgramModel
                               {
                                   CLPProgramId = man.ClpProgramId ,
                                   CLPProgramName  = atnm.CLPProgramName 
                                   
                               }).AsQueryable();

            return clplist;

 
        }
        public IQueryable<MstVoucher> GetVouchers(string vouchertype)
        {
            try
            {
                return Context.MstVoucher.Where(a=>a.VourcherType==vouchertype && a.STATUS==true ).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstRole> GetRoles()
        {
            try
            {
                return Context.MstRole.Where(x => x.RoleID == "Admin" || x.RoleID == "Cashier" || x.RoleID == "PUR_MGR" || x.RoleID == "STORE_MGR").OrderBy(a => a.Description).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstBusiness> GetBusinessDetails()
        {
            try
            {
                return Context.MstBusiness.OrderBy(a => a.BusinessName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstLanguage> GetLanguageList()
        {
            try
            {
                return Context.MstLanguage.OrderBy(a => a.LanguageName).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstEAN> GetDefaultEAN()
        {
            try
            {
                return Context.MstEAN.Where(a=>a.DefaultEAN ==false ).OrderBy(a => a.EAN).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstEAN> GetDefaultEANbyArticle(string articleCode)
        {
            try
            {
                return Context.MstEAN.Where(a =>  a.DefaultEAN == true && a.ArticleCode==articleCode ).OrderBy(a => a.EAN).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstCurrency> GetCurrency()
        {
            try
            {
                return Context.MstCurrency.OrderBy(a => a.CurrencyCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<DropDownModel> GetStockOutReasons()
 
        {
            try
            {
                var stockOutReasons = new List<DropDownModel>();

                stockOutReasons.Add(new DropDownModel { Code = "SupplierReturn", Description = "Return to Supplier" });
                stockOutReasons.Add(new DropDownModel { Code = "NonSaleable", Description = "Non Saleable" });
                stockOutReasons.Add(new DropDownModel { Code = "Damaged", Description = "Damaged" });
                stockOutReasons.Add(new DropDownModel { Code = "WriteOff", Description = "Write-off" });
               
                return stockOutReasons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<DropDownModel> GetFromLocation()
        {
            try
            {
                var fromLocations = new List<DropDownModel>();

                fromLocations.Add(new DropDownModel { Code = "Select", Description = "-- Select --" });
                fromLocations.Add(new DropDownModel { Code = "Damaged", Description = "Damaged" });
                fromLocations.Add(new DropDownModel { Code = "NonSaleable", Description = "Non Saleable" });
                fromLocations.Add(new DropDownModel { Code = "Saleable", Description = "Saleable" });

                return fromLocations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNextID(string siteCode, string objectID)
        {
            try
            {
                int nextID = 0;
                using (var context = ContextFactory.CreateContext())
                {
                    var glObjects = context.GLNoRangeObjects.Where(g => g.SiteCode == siteCode && g.ObjectID == objectID).FirstOrDefault();
                    nextID = (glObjects != null) ? (int)glObjects.CurrentNo : 1;
                }
                return nextID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateNextID(string siteCode, string objectID)
        {
            try
            {
                var glObject = Context.GLNoRangeObjects.Where(g => g.SiteCode == siteCode && g.ObjectID == objectID).FirstOrDefault();
                glObject.CurrentNo += 1;
                glObject.UPDATEDON = DateTime.Now;
                Context.Entry<GLNoRangeObjects>(glObject).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstSite> GetSiteList()
        {
            try
            {
                return Context.MstSite.Where(a => a.BusinessCode == "Store" && a.SiteCode == CommonModel.SiteCode).OrderBy(a => a.SiteCode).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstSupplierModel> GetSupplierBySite(string sitecode)
        {



            try
            {
                var supplierList = (from atnm in Context.MstSupplier
                                    join man in Context.SiteSupplierMap on atnm.SupplierCode equals man.SupplierCode

                               where man.SiteCode  == sitecode && man.STATUS == true
                                    select new MstSupplierModel 
                               {
                                    SupplierCode = atnm.SupplierCode,
                                    SupplierName = atnm.SupplierName

                               }).AsQueryable();

                return supplierList;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<MstDocumentTypeModel> GetDocumentType(string sitecode)
        {
            try
            {
                var documentList = (from md in Context.MstDocumentTypes
                                    //join man in Context.SiteSupplierMap on atnm.SupplierCode equals man.SupplierCode

                                    where md.STATUS == true
                                    select new MstDocumentTypeModel
                                    {
                                        DocumentType = md.DocumentType,
                                        DocumentTypeDescription = md.DocumentTypeDesc

                                    }).AsQueryable();

                return documentList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<GeneralCodeMstModel> GetAppliedOn(string sitecode)
        {



            try
            {
                var appliedon = (from md in Context.GeneralCodeMst
                                 //join man in Context.SiteSupplierMap on atnm.SupplierCode equals man.SupplierCode

                                 where md.CodeType == "AppliedonTax" && md.Status == true
                                 select new GeneralCodeMstModel
                                 {
                                     ShortDesc = md.ShortDesc,
                                     LongDesc = md.LongDesc

                                 }).AsQueryable();

                return appliedon;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ashma - 15 may 2018 for ShiftManagement
        public string GetSiteName()
        {
            try
            {
                var siteName = (from ms in Context.MstSite
                                where ms.SiteCode == CommonModel.SiteCode
                                select ms.SiteShortName).SingleOrDefault();
                return siteName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public bool UpdateNextID(string siteCode, string objectID)
        //{
        //    try
        //    {
        //        var glObject = Context.GLNoRangeObjects.Where(g => g.SiteCode == siteCode && g.ObjectID == objectID).FirstOrDefault();
        //        glObject.CurrentNo += 1;
        //       // Context.Entry<GLNoRangeObjects>(glObject).State = EntityState.Modified;
        //        Context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
