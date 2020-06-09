using System;
using System.Linq;
using Spectrum.DAL.RepositoryInterfaces;
using System.Data.Entity;
using Spectrum.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;

namespace Spectrum.DAL
{
    public class CharacteristicRepository : GenericRepository<CharacteristicsValue>, ICharacteristicRepository
    {
        public bool SaveCharacteristic(CharacteristicsValue characteristic)
        {
            try
            {
                Context.CharacteristicsValue.Add(characteristic);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateCharacteristic(CharacteristicsValue characteristic,string oldCharValue)
        {
           using (var context = ContextFactory.CreateContext())
          {
              using (var dbTran = context.Database.BeginTransaction())
              {
                  try
                  {              
                    Context.CharacteristicsValue.Remove(characteristic);
                    Context.Entry<CharacteristicsValue>(characteristic).State = EntityState.Deleted;                  
                    Context.SaveChanges();
                    characteristic.CharValue = oldCharValue ;
                    Context.CharacteristicsValue.Add(characteristic);
                    Context.SaveChanges();                 
                    dbTran.Commit();
                    return true;
                  }
                  catch (Exception ex)
                  {
                      dbTran.Rollback();
                      throw ex;
                  }
                  finally
                  {
                      context.Dispose();
                  }
              }
          }

        }

        public void DeleteCharacteristic(CharacteristicsValue characteristic)
        {
            try
            {
                Context.CharacteristicsValue.Remove(characteristic);
                Context.Entry<CharacteristicsValue>(characteristic).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteByID(string charCode)
        {
            try
            {
                var characteristic = Context.CharacteristicsValue.Where(u => u.CharCode == charCode).FirstOrDefault();
                Context.CharacteristicsValue.Remove(characteristic);
                Context.Entry<CharacteristicsValue>(characteristic).State = EntityState.Deleted;
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<CharacteristicsValue> GetCharacteristicList()
        {
            try
            {
                //var articleExportData = GetExportArticlesData("ANC000000000013");
                //Console.Write(articleExportData.ArticleDetails.Count());

                return Context.CharacteristicsValue.OrderBy(a => a.CharValue).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<CharacteristicModel> GetCharacteristicListNew()
        {
            try
            {
                var a = (from s in Context.CharacteristicsValue
                         select new CharacteristicModel
                         {
                             CharCode=s.CharCode ,
                             CharValue=s.CharValue,
                             PreValue=s.CharValue 
                         }).AsQueryable();
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CharacteristicsValue GetCharacteristicByID(string charCode)
        {
            try
            {
                return Context.CharacteristicsValue.Where(u => u.CharCode == charCode && u.STATUS == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<DropDownModel> GetCharacteristicTypes()
        {
            try
            {
                return (from c in Context.MstCharacteristics
                        select new DropDownModel
                        {
                            Code = c.CharCode,
                            Description = c.CharName
                        }).OrderBy(t => t.Description).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool  IsProductAssociate(string charvalue)
        {
            try
            {
                 var IsExist= (from c in Context.ArticleMatrix join 
                             e in Context.MstEAN on c.EanCode equals e.EAN //join 
                             //cv in Context.CharacteristicsValue on cv.ch
                             where c.CharValue==charvalue 
                             select e.ArticleCode 
                         ).FirstOrDefault();
                 if (IsExist != null)
                     return true;
                 else
                     return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string  IsProductDeletAssociate(string charvalue)
        {
            try
            {
                var IsExist = (from c in Context.ArticleMatrix
                               join
                                   e in Context.MstEAN on c.EanCode equals e.EAN
                               where c.CharValue == charvalue
                               select c.CharValue
                        ).FirstOrDefault();
                 
                    return IsExist;
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ArticleDataExportModel GetExportArticlesData(string nodeCode)
        {
            var articleDataExportModel = new ArticleDataExportModel();
            //var articlePara = new List<SqlParameter>();

            //articlePara.Add(new SqlParameter { ParameterName = "@nodeCode", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = nodeCode });
            //DbCommand cmd = EntityDataExtensions.CreateStoreCommand(SpectrumContext, "dbo.ExportArticleData", System.Data.CommandType.StoredProcedure, articlePara.ToArray());

            //// Use a connection scope to manage the lifetime of the connection
            //using (var connectionScope = cmd.Connection.CreateConnectionScope())
            //{
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        // Materialize the recipe.
            //        var articleDetails = reader.Materialize<ArticleDetails>().Bind<ArticleDetails>(SpectrumContext);

            //        // Move on to the categories resultset and attach it to the recipe.
            //        // Also bind it to the datacontext, so the object tracking works correctly.
            //        reader.NextResult();
            //        articleDataExportModel.TaxDetails = reader.Materialize<TaxDetails>().Bind<TaxDetails>(SpectrumContext);

            //        // Materialize the recipe ingredient information and attach it to the recipe.
            //        // Also bind it to the datacontext, so the object tracking works correctly.
            //        reader.NextResult();
            //        var charDetails = reader.Materialize<CharDetails>().Bind<CharDetails>(SpectrumContext);

            //        // Materialize the ingredients and attach them to the datacontext to enable object tracking.
            //        reader.NextResult();
            //        var salesDetails = reader.Materialize<SalesDetails>().Bind<SalesDetails>(SpectrumContext);

            //        // Iterate over the ingredient information and attach the ingredients to the records
            //        foreach (var item in articleDataExportModel.SalesDetails)
            //        {
            //            // Attach the related ingredient to the ingredient reference property
            //            //item.IngredientReference.Attach(ingredients.FirstOrDefault(
            //            //ingredient => ingredient.IngredientId == item.IngredientId));
            //        }

            //        reader.NextResult();
            //        var purchaseDetails = reader.Materialize<PurchaseDetails>().Bind<PurchaseDetails>(SpectrumContext);

            //        // Iterate over the ingredients in the recipe and attach the related unit information
            //        foreach (var ingredient in articleDataExportModel.PurchaseDetails.Select(item => item.ArticleCode))
            //        {
            //            // Attach the related unit to the unit reference property
            //            //ingredient.UnitReference.Attach(units.FirstOrDefault(unit => unit.UnitId == ingredient.UnitId));
            //        }
            //    }
            //}
            return articleDataExportModel;
        }
    }
}
