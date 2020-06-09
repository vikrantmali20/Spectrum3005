using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Spectrum.DAL.CustomEntities;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System.Data.Entity.Core.Objects;
using EntityState = System.Data.Entity.EntityState;
using C1.C1Excel;
using System.Text.RegularExpressions;
using System.Text;
using System.Data.Entity.Validation;




namespace Spectrum.DAL
{

    public class MaterialRepository : GenericRepository<MstMaterial>, IMaterialRepository
    {
        /// <summary>
        ///  add article model to database ..if Autonumber is generated then update next no to GLNoRangeObjects for this obectid and site
        /// </summary>
        /// <param name="article"></param>
        /// <param name="siteCode"> for updating auto Number </param>
        /// <param name="isAutoNumber"> Whether code is manual or auto </param>
        /// <returns></returns>

      const int xlscolumnRow = 0;
        public string defaultProfileName { get; set; }
        //string ErrorMsg;
        
        string ErrorMsgThrow;

        private bool ArticleLblPrintValidations(ref XLSheet articleLblPrint, ref bool result, ref  string ErrorMsg)
        {
            string siteCode = string.Empty;
            for (int rowCntr = 1; rowCntr < articleLblPrint.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < articleLblPrint.Columns.Count; colIndex++)
                {
                    switch (articleLblPrint.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "SITE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(articleLblPrint[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Site Code in 'ARTICLE PRINT' sheet at row no " + rowCntr;
                            }
                            break;
                        case "ARTICLE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(articleLblPrint[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Article Code in 'ARTICLE PRINT' sheet at row no " + rowCntr;
                            }
                            break;
                        case "EXPIRY":
                            if (string.IsNullOrEmpty(Convert.ToString(articleLblPrint[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Expiry in 'ARTICLE PRINT' sheet at row no " + rowCntr;
                            }
                            break;
                        case "STATUS":
                            if (string.IsNullOrEmpty(Convert.ToString(articleLblPrint[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Status in 'ARTICLE PRINT' sheet at row no " + rowCntr;
                            }
                            break;
                        default:
                            break;
                    }


                }
            }
            return result;
        }

        private bool MaterialValidations(ref XLSheet material, ref bool result, ref  string ErrorMsg)
        {
            for (int rowCntr = 1; rowCntr < material.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < material.Columns.Count; colIndex++)
                {
                    switch (material.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "SITE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Site Code in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;

                        case "MATERIAL TYPE":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Type in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;

                        case "MATERIAL ID":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Id in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;

                        case "MATERIAL NAME":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Name in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;
                        case "STATUS":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Status in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        private bool MaterialArticleMapValidations(ref XLSheet materialArticleMap, ref bool result, ref  string ErrorMsg)
        {

            for (int rowCntr = 1; rowCntr < materialArticleMap.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < materialArticleMap.Columns.Count; colIndex++)
                {
                    switch (materialArticleMap.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "SITE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Site Code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "ARTICLE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Article Code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "MATERIAL TYPE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Type in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;

                        case "MATERIAL ID":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Id in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;

                        case "MATERIAL VALUE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Value in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "STATUS":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Status in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        //ashm 3 may 2018 - merging excel sheets
        private bool ImportExportMaterialValidations(ref XLSheet material, ref XLSheet materialArticleMap, ref bool result, ref  string ErrorMsg)
        {
            #region Material
            for (int rowCntr = 1; rowCntr < material.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < material.Columns.Count; colIndex++)
                {
                    switch (material.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "SITE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Site Code in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;
                        case "MATERIAL TYPE":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Type in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            else
                            {
                                string materialType = material[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                string[] existingMaterialType = new string[] { "I", "N" };
                                if (!existingMaterialType.Contains(materialType))
                                {
                                    result = false;
                                    ErrorMsg = "Material Type is invalid in 'MATERIAL' sheet at row no " + rowCntr;
                                }
                            }
                            break;
                        case "MATERIAL ID":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material ID in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            else
                            {
                                string materialCode = (material[rowCntr, colIndex].Value.ToString().ToUpper().Trim());
                                int count = 0;
                                for (int i = 1; i < material.Rows.Count; i++)
                                {
                                    if (Convert.ToString(material[i, 2].Value).ToUpper().Trim() == materialCode && count == 0)
                                    {
                                        result = true;
                                        count = 1;
                                    }
                                    else if (Convert.ToString(material[i, 2].Value) == materialCode && count == 1)
                                    {
                                        result = false;
                                        ErrorMsg = "Material ID should be unique in 'MATERIAL' sheet at row no " + rowCntr;
                                    }
                                }
                            }
                            break;
                        case "MATERIAL NAME":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Name in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;
                        case "STATUS":
                            if (string.IsNullOrEmpty(Convert.ToString(material[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Status in 'MATERIAL' sheet at row no " + rowCntr;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion
            #region Mateial Article Map
            for (int rowCntr = 1; rowCntr < materialArticleMap.Rows.Count; rowCntr++)
            {
                for (int colIndex = 0; colIndex < materialArticleMap.Columns.Count; colIndex++)
                {
                    switch (materialArticleMap.GetCell(xlscolumnRow, colIndex).Value.ToString().ToUpper().Trim())
                    {
                        case "SITE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Site Code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "ARTICLE CODE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Article Code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "MATERIAL TYPE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Type in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            else
                            {
                                string materialType = materialArticleMap[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                string[] existingMaterialType = new string[] { "I", "N" };
                                if (!existingMaterialType.Contains(materialType))
                                {
                                    result = false;
                                    ErrorMsg = "Material Type is invalid in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, 3].Value)))
                                {
                                    string materialCode = materialArticleMap[rowCntr, 3].Value.ToString().ToUpper().Trim();
                                    for (int i = 1; i < material.Rows.Count; i++)
                                    {
                                        if (Convert.ToString(material[i, 2].Value) == materialCode)
                                        {
                                            if (Convert.ToString(material[i, 1].Value) != materialType)
                                            {
                                                result = false;
                                                ErrorMsg = "Material Type is invalid in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "MATERIAL ID":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Id in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            else
                            {
                                bool flag = false;
                                int count = 0;
                                string materialCode = materialArticleMap[rowCntr, colIndex].Value.ToString().ToUpper().Trim();
                                string articleCode = materialArticleMap[rowCntr, 1].Value.ToString().ToUpper().Trim();
                                for (int i = 1; i < material.Rows.Count; i++)
                                {
                                    if (Convert.ToString(material[i, 2].Value).ToUpper().Trim() == materialCode)
                                    {
                                        result = true;
                                        flag = true;
                                    }
                                }
                                if (!flag)
                                {
                                    result = false;
                                    ErrorMsg = "Material ID is invalid in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                                    break;
                                }
                                for (int i = 1; i < materialArticleMap.Rows.Count; i++)
                                {
                                    if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode && Convert.ToString(materialArticleMap[i, 3].Value) == materialCode && count == 0)
                                    {
                                        result = true;
                                        count = 1;
                                    }
                                    else if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode && Convert.ToString(materialArticleMap[i, 3].Value) == materialCode && count == 1)
                                    {
                                        result = false;
                                        ErrorMsg = "Material ID is already assigned to Article Code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                                        break;
                                    }
                                }
                            }
                            break;
                        case "MATERIAL VALUE":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Material Value in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        case "EXPIRY":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "please provide Expiry in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            else
                            {
                                string articleCode = materialArticleMap[rowCntr, 1].Value.ToString().ToUpper().Trim();
                                string expiry = Convert.ToString(materialArticleMap[rowCntr, colIndex].Value);

                                for (int i = 1; i < materialArticleMap.Rows.Count; i++)
                                {
                                    if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode)
                                    {
                                        if (Convert.ToString(materialArticleMap[i, 5].Value) != expiry)
                                        {
                                            result = false;
                                            ErrorMsg = "Expiry days are invalid for Article code in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "STATUS":
                            if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, colIndex].Value)))
                            {
                                result = false;
                                ErrorMsg = "Please provide Status in 'MATERIAL ARTICLE MAP' sheet at row no " + rowCntr;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            return result;

        }

        //public bool ImportExcelValidations(ref XLSheet articleLblPrint, ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg)
        public bool ImportExcelValidations(ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg)
        {
            bool result = false;
            try
            {
                #region
                //if (articleLblPrint != null)
                //{
                //    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                //    {
                //        result = ArticleLblPrintValidations(ref articleLblPrint, ref result, ref ErrorMsgThrow);
                //    }
                //}

                //if (material != null)
                //{
                //    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                //    {
                //        result = MaterialValidations(ref material, ref result, ref ErrorMsgThrow);
                //    }
                //}

                //if (materialArticleMap != null)
                //{
                //    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                //    {
                //        result = MaterialArticleMapValidations(ref materialArticleMap, ref result, ref ErrorMsgThrow);
                //    }
                //}
                #endregion

                //3 may 2018 merging excel sheets
                if (material != null && materialArticleMap != null)
                {
                    if (ErrorMsgThrow == "" || string.IsNullOrEmpty(ErrorMsgThrow))
                    {
                        result = ImportExportMaterialValidations(ref material, ref materialArticleMap, ref result, ref ErrorMsgThrow);
                    }
                }
                ErrorMsg = ErrorMsgThrow;
                ErrorMsgThrow = "";
            }
            catch (Exception ex)
            {
                //  throw ex;
            }
            return result;
        }

        private void Update<T>(T entity, Func<T, bool> keycondition) where T : class
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = Context.Set<T>();
                T attachedEntity = set.Local.SingleOrDefault(keycondition);
                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }

            }
        }

        //public bool SaveImportExcel(ref XLSheet articleLblPrint, ref XLSheet material, ref XLSheet materialArticleMap)
        public bool SaveImportExcel(ref XLSheet material, ref XLSheet materialArticleMap)
        {
            bool tranResult = false;
            try
            {
                using (var dbContextTransaction = Context.Database.BeginTransaction())
                {
                    #region Commented
                    //for (int rowCntr = 1; rowCntr < articleLblPrint.Rows.Count; rowCntr++)
                    //{
                    //    ArticlelblPrintDtl objArticle = new ArticlelblPrintDtl();
                    //    objArticle.SiteCode = articleLblPrint.GetCell(rowCntr, 0).Value.ToString();
                    //    objArticle.ArticleCode = articleLblPrint.GetCell(rowCntr, 1).Value.ToString();
                    //    objArticle.ExpiryInDays = Convert.ToInt32(articleLblPrint.GetCell(rowCntr, 2).Value);
                    //    objArticle.Status = Convert.ToBoolean(articleLblPrint.GetCell(rowCntr, 3).Value);
                    //    //var _objArticle = (from a in Context.ArticlelblPrintDtl
                    //    //                   select a);
                    //    //var _objArticle = from a in Context.ArticlelblPrintDtl
                    //    //                  where (a.SiteCode == objArticle.SiteCode && a.ArticleCode == objArticle.ArticleCode)
                    //    //                  select a;
                    //    var _objArticle = Context.ArticlelblPrintDtl.Where(a => a.ArticleCode == objArticle.ArticleCode && a.SiteCode == objArticle.SiteCode).Count();
                    //    if (_objArticle != 0)
                    //    {
                    //        //update
                    //        objArticle.UpdatedAt = CommonModel.SiteCode;
                    //        objArticle.UpdatedOn = DateTime.Now;
                    //        objArticle.UpdatedBy = CommonModel.UserID;
                    //        Context.Entry(objArticle).State = EntityState.Modified;
                    //        Context.SaveChanges();
                    //    }
                    //    else
                    //    {
                    //        //insert
                    //        objArticle.CreatedAt = CommonModel.SiteCode;
                    //        objArticle.CreatedOn = DateTime.Now;
                    //        objArticle.CreatedBy = CommonModel.UserID;
                    //        objArticle.UpdatedAt = CommonModel.SiteCode;
                    //        objArticle.UpdatedOn = DateTime.Now;
                    //        objArticle.UpdatedBy = CommonModel.UserID;
                    //        Context.ArticlelblPrintDtl.Add(objArticle);
                    //    }
                    //}
                    //for (int rowCntr = 1; rowCntr < material.Rows.Count; rowCntr++)
                    //{
                    //    MstMaterial objMaterial = new MstMaterial();
                    //    objMaterial.SiteCode = material.GetCell(rowCntr, 0).Value.ToString();
                    //    objMaterial.MaterialType = material.GetCell(rowCntr, 1).Value.ToString();
                    //    objMaterial.MaterialId = material.GetCell(rowCntr, 2).Value.ToString();
                    //    objMaterial.MaterialName = material.GetCell(rowCntr, 3).Value.ToString();
                    //    objMaterial.Status = Convert.ToBoolean(material.GetCell(rowCntr, 4).Value);
                    //    var _objMaterial = Context.MstMaterial.Where(a => a.SiteCode == objMaterial.SiteCode && a.MaterialId == objMaterial.MaterialId).Count();
                    //    if (_objMaterial != 0)
                    //    {
                    //        objMaterial.UpdatedAt = CommonModel.SiteCode;
                    //        objMaterial.UpdatedOn = DateTime.Now;
                    //        objMaterial.UpdatedBy = CommonModel.UserID;
                    //        Context.Entry(objMaterial).State = EntityState.Modified;
                    //        Context.SaveChanges();
                    //    }
                    //    else
                    //    {
                    //        objMaterial.CreatedAt = CommonModel.SiteCode;
                    //        objMaterial.CreatedOn = DateTime.Now;
                    //        objMaterial.CreatedBy = CommonModel.UserID;
                    //        objMaterial.UpdatedAt = CommonModel.SiteCode;
                    //        objMaterial.UpdatedOn = DateTime.Now;
                    //        objMaterial.UpdatedBy = CommonModel.UserID;
                    //        Context.MstMaterial.Add(objMaterial);
                    //    }

                    //}
                    //for (int rowCntr = 1; rowCntr < materialArticleMap.Rows.Count; rowCntr++)
                    //{
                    //    MstMaterialArticleMap objMaterialArticleMap = new MstMaterialArticleMap();
                    //    objMaterialArticleMap.SiteCode = materialArticleMap.GetCell(rowCntr, 0).Value.ToString();
                    //    objMaterialArticleMap.ArticleCode = materialArticleMap.GetCell(rowCntr, 1).Value.ToString();
                    //    objMaterialArticleMap.MaterialType = materialArticleMap.GetCell(rowCntr, 2).Value.ToString();
                    //    objMaterialArticleMap.MaterialId = materialArticleMap.GetCell(rowCntr, 3).Value.ToString();
                    //    objMaterialArticleMap.MaterialValue = materialArticleMap.GetCell(rowCntr, 4).Value.ToString();
                    //    objMaterialArticleMap.Status = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 5).Value);
                    //    var _objMaterialArticle = Context.MstMaterialArticleMap.Where(a => a.SiteCode == objMaterialArticleMap.SiteCode && a.ArticleCode == objMaterialArticleMap.ArticleCode && a.MaterialId == objMaterialArticleMap.MaterialId).Count();
                    //    if (_objMaterialArticle != 0)
                    //    {
                    //        objMaterialArticleMap.UpdatedAt = CommonModel.SiteCode;
                    //        objMaterialArticleMap.UpdatedOn = DateTime.Now;
                    //        objMaterialArticleMap.UpdatedBy = CommonModel.UserID;
                    //        Context.Entry(objMaterialArticleMap).State = EntityState.Modified;
                    //        Context.SaveChanges();
                    //    }
                    //    else
                    //    {
                    //        objMaterialArticleMap.CreatedAt = CommonModel.SiteCode;
                    //        objMaterialArticleMap.CreatedOn = DateTime.Now;
                    //        objMaterialArticleMap.CreatedBy = CommonModel.UserID;
                    //        objMaterialArticleMap.UpdatedAt = CommonModel.SiteCode;
                    //        objMaterialArticleMap.UpdatedOn = DateTime.Now;
                    //        objMaterialArticleMap.UpdatedBy = CommonModel.UserID;
                    //        Context.MstMaterialArticleMap.Add(objMaterialArticleMap);
                    //    }

                    //}
                    //Context.SaveChanges();
                    //dbContextTransaction.Commit();
                    //tranResult = true;
                    #endregion

                    //ashm 3 may 2018 - merging excel sheets
                    #region Material insert/update
                    for (int rowCntr = 1; rowCntr < material.Rows.Count; rowCntr++)
                    {

                        MstMaterial objMaterial = new MstMaterial();
                        objMaterial.SiteCode = material.GetCell(rowCntr, 0).Value.ToString();
                        objMaterial.MaterialType = material.GetCell(rowCntr, 1).Value.ToString();
                        objMaterial.MaterialId = material.GetCell(rowCntr, 2).Value.ToString();
                        objMaterial.MaterialName = material.GetCell(rowCntr, 3).Value.ToString();
                        objMaterial.Status = Convert.ToBoolean(material.GetCell(rowCntr, 4).Value);
                        objMaterial.UpdatedAt = CommonModel.SiteCode;
                        objMaterial.UpdatedOn = DateTime.Now;
                        objMaterial.UpdatedBy = CommonModel.UserID;
                        var _objMaterial = Context.MstMaterial.Where(a => a.SiteCode == objMaterial.SiteCode && a.MaterialId == objMaterial.MaterialId).FirstOrDefault();
                        if (_objMaterial != null)
                        {
                            //mantis id 3275 - 4 may 2018 ashma
                            objMaterial.CreatedAt = _objMaterial.CreatedAt;
                            objMaterial.CreatedOn = _objMaterial.CreatedOn;
                            objMaterial.CreatedBy = _objMaterial.CreatedBy;
                            Update(objMaterial, key => key.SiteCode == objMaterial.SiteCode && key.MaterialId == objMaterial.MaterialId);
                            Context.SaveChanges();
                        }
                        else
                        {
                            objMaterial.CreatedAt = CommonModel.SiteCode;
                            objMaterial.CreatedOn = DateTime.Now;
                            objMaterial.CreatedBy = CommonModel.UserID;
                            Context.MstMaterial.Add(objMaterial);
                            Context.SaveChanges();
                        }
                    }
                    #endregion

                    for (int rowCntr = 1; rowCntr < materialArticleMap.Rows.Count; rowCntr++)
                    {
                        #region MaterialArticle insert/update
                        MstMaterialArticleMap objMaterialArticleMap = new MstMaterialArticleMap();
                        objMaterialArticleMap.SiteCode = materialArticleMap.GetCell(rowCntr, 0).Value.ToString();
                        objMaterialArticleMap.ArticleCode = materialArticleMap.GetCell(rowCntr, 1).Value.ToString();
                        objMaterialArticleMap.MaterialType = materialArticleMap.GetCell(rowCntr, 2).Value.ToString();
                        objMaterialArticleMap.MaterialId = materialArticleMap.GetCell(rowCntr, 3).Value.ToString();
                        objMaterialArticleMap.MaterialValue = materialArticleMap.GetCell(rowCntr, 4).Value.ToString();
                        objMaterialArticleMap.Status = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 6).Value);
                        objMaterialArticleMap.UpdatedAt = CommonModel.SiteCode;
                        objMaterialArticleMap.UpdatedOn = DateTime.Now;
                        objMaterialArticleMap.UpdatedBy = CommonModel.UserID;
                        var _objMaterialArticle = Context.MstMaterialArticleMap.Where(a => a.SiteCode == objMaterialArticleMap.SiteCode
                            && a.ArticleCode == objMaterialArticleMap.ArticleCode && a.MaterialId == objMaterialArticleMap.MaterialId).FirstOrDefault();
                        if (_objMaterialArticle != null)
                        {
                            //mantis id 3275 - 4 may 2018 ashma
                            objMaterialArticleMap.CreatedAt = _objMaterialArticle.CreatedAt;
                            objMaterialArticleMap.CreatedOn = _objMaterialArticle.CreatedOn;
                            objMaterialArticleMap.CreatedBy = _objMaterialArticle.CreatedBy;
                            Update(objMaterialArticleMap, key => key.SiteCode == objMaterialArticleMap.SiteCode && key.ArticleCode == objMaterialArticleMap.ArticleCode
                                && key.MaterialId == objMaterialArticleMap.MaterialId);
                            Context.SaveChanges();
                        }
                        else
                        {
                            objMaterialArticleMap.CreatedAt = CommonModel.SiteCode;
                            objMaterialArticleMap.CreatedOn = DateTime.Now;
                            objMaterialArticleMap.CreatedBy = CommonModel.UserID;
                            Context.MstMaterialArticleMap.Add(objMaterialArticleMap);
                            Context.SaveChanges();
                        }
                        #endregion

                        #region ArticlelblPrintDtl for Expiry
                        ArticlelblPrintDtl objArticle = new ArticlelblPrintDtl();
                        objArticle.SiteCode = materialArticleMap.GetCell(rowCntr, 0).Value.ToString();
                        objArticle.ArticleCode = materialArticleMap.GetCell(rowCntr, 1).Value.ToString();
                        objArticle.ExpiryInDays = Convert.ToInt32(materialArticleMap.GetCell(rowCntr, 5).Value);
                        objArticle.Status = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 6).Value);
                        objArticle.UpdatedAt = CommonModel.SiteCode;
                        objArticle.UpdatedOn = DateTime.Now;
                        objArticle.UpdatedBy = CommonModel.UserID;

                        var _objArticle = Context.ArticlelblPrintDtl.Where(ap => ap.SiteCode == objArticle.SiteCode && ap.ArticleCode == objArticle.ArticleCode).FirstOrDefault();
                        if (_objArticle != null)
                        {
                            //update
                            //mantis id 3275 - 4 may 2018 ashma
                            objArticle.CreatedAt = _objArticle.CreatedAt;
                            objArticle.CreatedOn = _objArticle.CreatedOn;
                            objArticle.CreatedBy = _objArticle.CreatedBy;
                            Update(objArticle, key => key.SiteCode == objArticle.SiteCode && key.ArticleCode == objArticle.ArticleCode);
                            Context.SaveChanges();
                        }
                        else
                        {   //insert
                            objArticle.CreatedAt = CommonModel.SiteCode;
                            objArticle.CreatedOn = DateTime.Now;
                            objArticle.CreatedBy = CommonModel.UserID;
                            objArticle.UpdatedAt = CommonModel.SiteCode;
                            objArticle.UpdatedOn = DateTime.Now;
                            objArticle.UpdatedBy = CommonModel.UserID;
                            Context.ArticlelblPrintDtl.Add(objArticle);
                            Context.SaveChanges();
                        }
                        #endregion
                    }
                    dbContextTransaction.Commit();
                    tranResult = true;
                }
            }
            catch (Exception ex)
            {
                //  throw ex;
            }
            return tranResult;

        }

        //public bool ExportToExcel(ref List<ArticlelblPrintDtl> objArticleLblPrint, ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path)
        public bool ExportToExcel(ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path)
        {
            bool tranResult = false;
            //objArticleLblPrint = (from ap in Context.ArticlelblPrintDtl
            //                      select ap).ToList();
            objMaterial = (from m in Context.MstMaterial
                           select m).ToList();
            objMaterialArticleMap = (from ma in Context.MstMaterialArticleMap
                                     select ma).ToList();
            int _row = 1;
            C1XLBook excelWorkBook = new C1XLBook();
            //excelWorkBook.Sheets[0].Name = "Article Print";
            //excelWorkBook.Sheets[1].Name = "Material";
            //excelWorkBook.Sheets[2].Name = "Material Article Map";

            // 1st Sheet
            //XLSheet excelSheet1 = excelWorkBook.Sheets[0];
            //excelSheet1.Name = "Article Print";
            //excelSheet1[0, 0].Value = "Site Code";
            //excelSheet1[0, 1].Value = "Article Code";
            //excelSheet1[0, 2].Value = "Expiry";
            //excelSheet1[0, 3].Value = "Status";
            //foreach (var obj in objArticleLblPrint.ToList())
            //{
            //    excelSheet1[_row, 0].Value = obj.SiteCode;
            //    excelSheet1[_row, 1].Value = obj.ArticleCode;
            //    excelSheet1[_row, 2].Value = obj.ExpiryInDays;
            //    excelSheet1[_row, 3].Value = obj.Status;
            //    _row += 1;
            //}
            // 2nd Sheet
            _row = 1;
            //ashm 3 may 2018 - merging excel sheets
            //XLSheet excelSheet2 = excelWorkBook.Sheets.Add();
            XLSheet excelSheet2 = excelWorkBook.Sheets[0];
            excelSheet2.Name = "Material";
            excelSheet2[0, 0].Value = "Site Code";
            excelSheet2[0, 1].Value = "Material Type";
            excelSheet2[0, 2].Value = "Material Id";
            excelSheet2[0, 3].Value = "Material Name";
            excelSheet2[0, 4].Value = "Status";
            foreach (var obj in objMaterial.ToList())
            {
                excelSheet2[_row, 0].Value = obj.SiteCode;
                excelSheet2[_row, 1].Value = obj.MaterialType;
                excelSheet2[_row, 2].Value = obj.MaterialId;
                excelSheet2[_row, 3].Value = obj.MaterialName;
                excelSheet2[_row, 4].Value = obj.Status;
                _row += 1;
            }
            // 3rd Sheet
            _row = 1;
            XLSheet excelSheet3 = excelWorkBook.Sheets.Add();
            excelSheet3.Name = "Material Article Map";
            excelSheet3[0, 0].Value = "Site Code";
            excelSheet3[0, 1].Value = "Article Code";
            excelSheet3[0, 2].Value = "Material Type";
            excelSheet3[0, 3].Value = "Material Id";
            excelSheet3[0, 4].Value = "Material Value";
            excelSheet3[0, 5].Value = "Expiry"; // added on 27-4-18
            excelSheet3[0, 6].Value = "Status";
            foreach (var obj in objMaterialArticleMap.ToList())
            {
                var expiryDays = Context.ArticlelblPrintDtl.Single(x => x.ArticleCode == obj.ArticleCode).ExpiryInDays;
                excelSheet3[_row, 0].Value = obj.SiteCode;
                excelSheet3[_row, 1].Value = obj.ArticleCode;
                excelSheet3[_row, 2].Value = obj.MaterialType;
                excelSheet3[_row, 3].Value = obj.MaterialId;
                excelSheet3[_row, 4].Value = obj.MaterialValue;
                excelSheet3[_row, 5].Value = expiryDays; //added on 27-4-18
                excelSheet3[_row, 6].Value = obj.Status;
                _row += 1;
            }
            excelWorkBook.Save(path);
            tranResult = true;

            //Application excel = new Microsoft.Office.Interop.Excel.Application();
            //Workbook excelWorkbook = excel.Workbooks.Add(Type.Missing);
            //Worksheet excelWorkSheet = excelWorkbook.ActiveSheet;
            //excelWorkSheet.Name = "Test work sheet";


            //workbook.Load("sheetName");
            //var ws = workbook.Worksheet("sheetName");

            //int row = 1;
            //foreach (object item in itemList)
            //{
            //    ws.Cell("A" + row.ToString()).Value = item.ToString();
            //    row++;
            //}

            //workbook.SaveAs("yourExcel.xlsx");

            return tranResult;
        }

        //ashma 22 may 2018 - failed material excel export 
        public bool Save(ref XLSheet material, ref XLSheet materialArticleMap, ref C1XLBook failedMaterialbook)
        {
            bool tranResult = false;
            try
            {
                XLSheet excelSheet1 = failedMaterialbook.Sheets[0];
                excelSheet1.Name = "Material";
                excelSheet1[0, 0].Value = "Site Code";
                excelSheet1[0, 1].Value = "Material Type";
                excelSheet1[0, 2].Value = "Material Id";
                excelSheet1[0, 3].Value = "Material Name";
                excelSheet1[0, 4].Value = "Status";

                XLSheet excelSheet2 = failedMaterialbook.Sheets.Add();
                excelSheet2.Name = "Material Article Map";
                excelSheet2[0, 0].Value = "Site Code";
                excelSheet2[0, 1].Value = "Article Code";
                excelSheet2[0, 2].Value = "Material Type";
                excelSheet2[0, 3].Value = "Material Id";
                excelSheet2[0, 4].Value = "Material Value";
                excelSheet2[0, 5].Value = "Expiry"; // added on 27-4-18
                excelSheet2[0, 6].Value = "Status";
                using (var dbContextTransaction = Context.Database.BeginTransaction())
                {
                    //ashm 3 may 2018 - merging excel sheets
                    int excelRow = 1;
                    #region Material insert/update
                    for (int rowCntr = 1; rowCntr < material.Rows.Count; rowCntr++)
                    {
                        bool result = false;
                        MstMaterial objMaterial = new MstMaterial();
                        if (!string.IsNullOrEmpty(Convert.ToString(material.GetCell(rowCntr, 0).Value)))
                        {
                            objMaterial.SiteCode = Convert.ToString(material.GetCell(rowCntr, 0).Value);
                        }
                        else
                        {
                            excelSheet1[excelRow, 0].Value = " (Invalid)";
                            result = true;
                        }
                        string materialType = material[rowCntr, 1].Value.ToString().ToUpper().Trim();
                        string[] existingMaterialType = new string[] { "I", "N" };
                        if (!string.IsNullOrEmpty(Convert.ToString(material.GetCell(rowCntr, 1).Value)) && existingMaterialType.Contains(materialType))
                        {
                            objMaterial.MaterialType = Convert.ToString(material.GetCell(rowCntr, 1).Value);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty((Convert.ToString(material.GetCell(rowCntr, 1).Value))))
                            {
                                excelSheet1[excelRow, 1].Value = " (Invalid)";
                                result = true;
                            }
                            else if (!existingMaterialType.Contains(materialType))
                            {
                                excelSheet1[excelRow, 1].Value = Convert.ToString(material.GetCell(rowCntr, 1).Value) + " (Invalid)";
                                result = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(material.GetCell(rowCntr, 2).Value)))
                        {
                            string materialCode = (material[rowCntr, 2].Value.ToString().ToUpper().Trim());
                            int count = 0;
                            for (int i = 1; i < material.Rows.Count; i++)
                            {
                                if (Convert.ToString(material[i, 2].Value).ToUpper().Trim() == materialCode && count == 0)
                                {
                                    objMaterial.MaterialId = Convert.ToString(material.GetCell(rowCntr, 2).Value);
                                    count = 1;
                                }
                                else if (Convert.ToString(material[i, 2].Value) == materialCode && count == 1)
                                {
                                    if (i == rowCntr)
                                    {
                                        excelSheet1[excelRow, 2].Value = Convert.ToString(material.GetCell(rowCntr, 2).Value) + " (Invalid)";
                                        result = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            excelSheet1[excelRow, 2].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(material.GetCell(rowCntr, 3).Value)))
                        {
                            objMaterial.MaterialName = Convert.ToString(material.GetCell(rowCntr, 3).Value);
                        }
                        else
                        {
                            excelSheet1[excelRow, 3].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(material.GetCell(rowCntr, 4).Value)))
                        {
                            objMaterial.Status = Convert.ToBoolean(material.GetCell(rowCntr, 4).Value);
                        }
                        else
                        {
                            excelSheet1[excelRow, 4].Value = " (Invalid)";
                            result = true;
                        }
                        if (!result)
                        {
                            objMaterial.UpdatedAt = CommonModel.SiteCode;
                            objMaterial.UpdatedOn = DateTime.Now;
                            objMaterial.UpdatedBy = CommonModel.UserID;
                            var _objMaterial = Context.MstMaterial.Where(a => a.SiteCode == objMaterial.SiteCode && a.MaterialId == objMaterial.MaterialId).FirstOrDefault();
                            if (_objMaterial != null)
                            {
                                //mantis id 3275 - 4 may 2018 ashma
                                objMaterial.CreatedAt = _objMaterial.CreatedAt;
                                objMaterial.CreatedOn = _objMaterial.CreatedOn;
                                objMaterial.CreatedBy = _objMaterial.CreatedBy;
                                Update(objMaterial, key => key.SiteCode == objMaterial.SiteCode && key.MaterialId == objMaterial.MaterialId);
                                Context.SaveChanges();
                            }
                            else
                            {
                                objMaterial.CreatedAt = CommonModel.SiteCode;
                                objMaterial.CreatedOn = DateTime.Now;
                                objMaterial.CreatedBy = CommonModel.UserID;
                                Context.MstMaterial.Add(objMaterial);
                                Context.SaveChanges();
                            }

                        }
                        else
                        {
                            //add row to export excel file
                            if (!Convert.ToString(excelSheet1[excelRow, 0].Value).Contains(" (Invalid)"))
                            {
                                excelSheet1[excelRow, 0].Value = Convert.ToString(material.GetCell(rowCntr, 0).Value);
                            }
                            if (!Convert.ToString(excelSheet1[excelRow, 1].Value).Contains(" (Invalid)"))
                            {
                                excelSheet1[excelRow, 1].Value = Convert.ToString(material.GetCell(rowCntr, 1).Value);
                            }
                            if (!Convert.ToString(excelSheet1[excelRow, 2].Value).Contains(" (Invalid)"))
                            {
                                excelSheet1[excelRow, 2].Value = Convert.ToString(material.GetCell(rowCntr, 2).Value);
                            }
                            if (!Convert.ToString(excelSheet1[excelRow, 3].Value).Contains(" (Invalid)"))
                            {
                                excelSheet1[excelRow, 3].Value = Convert.ToString(material.GetCell(rowCntr, 3).Value);
                            }
                            if (!Convert.ToString(excelSheet1[excelRow, 4].Value).Contains(" (Invalid)"))
                            {
                                excelSheet1[excelRow, 4].Value = Convert.ToBoolean(material.GetCell(rowCntr, 4).Value);
                            }
                            excelRow++;
                        }
                    }
                    #endregion

                    #region MaterialArticle insert/update
                    int excelrow = 1;
                    for (int rowCntr = 1; rowCntr < materialArticleMap.Rows.Count; rowCntr++)
                    {
                        bool result = false;
                        MstMaterialArticleMap objMaterialArticleMap = new MstMaterialArticleMap();
                        ArticlelblPrintDtl objArticle = new ArticlelblPrintDtl();
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap.GetCell(rowCntr, 0).Value)))
                        {
                            objMaterialArticleMap.SiteCode = Convert.ToString(materialArticleMap.GetCell(rowCntr, 0).Value);
                            objArticle.SiteCode = Convert.ToString(materialArticleMap.GetCell(rowCntr, 0).Value);
                        }
                        else
                        {
                            excelSheet2[excelrow, 0].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap.GetCell(rowCntr, 1).Value)))
                        {
                            objMaterialArticleMap.ArticleCode = Convert.ToString(materialArticleMap.GetCell(rowCntr, 1).Value);
                            objArticle.ArticleCode = Convert.ToString(materialArticleMap.GetCell(rowCntr, 1).Value);
                        }
                        else
                        {
                            excelSheet2[excelrow, 1].Value = " (Invalid)";
                            result = true;
                        }

                        string materialType = materialArticleMap[rowCntr, 2].Value.ToString().ToUpper().Trim();
                        string[] existingMaterialType = new string[] { "I", "N" };
                        if (string.IsNullOrEmpty(Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value)))
                        {
                            excelSheet2[excelrow, 2].Value = " (Invalid)";
                            result = true;
                        }
                        if (!existingMaterialType.Contains(materialType))
                        {
                            excelSheet2[excelrow, 2].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value) + " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value)))
                        {
                            string materialCode = (materialArticleMap[rowCntr, 3].Value.ToString().ToUpper().Trim());
                            string articleCode = materialArticleMap[rowCntr, 1].Value.ToString().ToUpper().Trim();
                            int count = 0, count_ = 0;
                            for (int i = 1; i < material.Rows.Count; i++)
                            {
                                if (Convert.ToString(material[i, 2].Value).ToUpper().Trim() == materialCode && count == 0)
                                {
                                    if (Convert.ToString(material[i, 1].Value) == materialType)
                                    {
                                        count = 1;
                                        objMaterialArticleMap.MaterialType = Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value);
                                        objMaterialArticleMap.MaterialId = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value);
                                    }
                                }
                            }
                            if (count == 0)
                            {
                                excelSheet2[excelrow, 2].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value) + " (Invalid)";
                                excelSheet2[excelrow, 3].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value) + " (Invalid)";
                                result = true;
                            }
                            else
                            {
                                objMaterialArticleMap.MaterialType = Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value);
                                objMaterialArticleMap.MaterialId = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 3].Value).Contains(" (Invalid"))
                            {
                                for (int i = 1; i < materialArticleMap.Rows.Count; i++)
                                {
                                    if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode && Convert.ToString(materialArticleMap[i, 3].Value) == materialCode && count_ == 0)
                                    {
                                        objMaterialArticleMap.MaterialId = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value);
                                        count_ = 1;
                                    }
                                    else if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode && Convert.ToString(materialArticleMap[i, 3].Value) == materialCode && count_ == 1)
                                    {
                                        excelSheet2[excelrow, 3].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value) + " (Invalid)";
                                        result = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            excelSheet2[excelrow, 3].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, 4].Value)))
                        {
                            objMaterialArticleMap.MaterialValue = Convert.ToString(materialArticleMap.GetCell(rowCntr, 4).Value);
                        }
                        else
                        {
                            excelSheet2[excelrow, 4].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, 5].Value)))
                        {
                            string articleCode = materialArticleMap[rowCntr, 1].Value.ToString().ToUpper().Trim();
                            string expiry = Convert.ToString(materialArticleMap[rowCntr, 5].Value);

                            for (int i = 1; i < materialArticleMap.Rows.Count; i++)
                            {
                                if (Convert.ToString(materialArticleMap[i, 1].Value) == articleCode)
                                {
                                    if (Convert.ToString(materialArticleMap[i, 5].Value) == expiry)
                                    {
                                        objArticle.ExpiryInDays = Convert.ToInt32(materialArticleMap.GetCell(rowCntr, 5).Value);
                                    }
                                    else
                                    {
                                        excelSheet2[excelrow, 5].Value = Convert.ToInt32(materialArticleMap.GetCell(rowCntr, 5).Value) + " (Invalid)";
                                        result = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            excelSheet2[excelrow, 5].Value = " (Invalid)";
                            result = true;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(materialArticleMap[rowCntr, 6].Value)))
                        {
                            objMaterialArticleMap.Status = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 6).Value);
                            objArticle.Status = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 6).Value);
                        }
                        else
                        {
                            excelSheet2[excelrow, 6].Value = " (Invalid)";
                            result = true;
                        }
                        if (!result)
                        {
                            objMaterialArticleMap.UpdatedAt = CommonModel.SiteCode;
                            objMaterialArticleMap.UpdatedOn = DateTime.Now;
                            objMaterialArticleMap.UpdatedBy = CommonModel.UserID;

                            objArticle.UpdatedAt = CommonModel.SiteCode;
                            objArticle.UpdatedOn = DateTime.Now;
                            objArticle.UpdatedBy = CommonModel.UserID;

                            var _objMaterialArticle = Context.MstMaterialArticleMap.Where(a => a.SiteCode == objMaterialArticleMap.SiteCode
                                && a.ArticleCode == objMaterialArticleMap.ArticleCode && a.MaterialId == objMaterialArticleMap.MaterialId).FirstOrDefault();
                            if (_objMaterialArticle != null)
                            {
                                //mantis id 3275 - 4 may 2018 ashma
                                objMaterialArticleMap.CreatedAt = _objMaterialArticle.CreatedAt;
                                objMaterialArticleMap.CreatedOn = _objMaterialArticle.CreatedOn;
                                objMaterialArticleMap.CreatedBy = _objMaterialArticle.CreatedBy;
                                Update(objMaterialArticleMap, key => key.SiteCode == objMaterialArticleMap.SiteCode && key.ArticleCode == objMaterialArticleMap.ArticleCode
                                    && key.MaterialId == objMaterialArticleMap.MaterialId);
                                Context.SaveChanges();
                            }
                            else
                            {
                                objMaterialArticleMap.CreatedAt = CommonModel.SiteCode;
                                objMaterialArticleMap.CreatedOn = DateTime.Now;
                                objMaterialArticleMap.CreatedBy = CommonModel.UserID;
                                Context.MstMaterialArticleMap.Add(objMaterialArticleMap);
                                Context.SaveChanges();
                            }
                            var _objArticle = Context.ArticlelblPrintDtl.Where(ap => ap.SiteCode == objArticle.SiteCode && ap.ArticleCode == objArticle.ArticleCode).FirstOrDefault();
                            if (_objArticle != null)
                            {
                                //update
                                //mantis id 3275 - 4 may 2018 ashma
                                objArticle.CreatedAt = _objArticle.CreatedAt;
                                objArticle.CreatedOn = _objArticle.CreatedOn;
                                objArticle.CreatedBy = _objArticle.CreatedBy;
                                Update(objArticle, key => key.SiteCode == objArticle.SiteCode && key.ArticleCode == objArticle.ArticleCode);
                                Context.SaveChanges();
                            }
                            else
                            {   //insert
                                objArticle.CreatedAt = CommonModel.SiteCode;
                                objArticle.CreatedOn = DateTime.Now;
                                objArticle.CreatedBy = CommonModel.UserID;
                                objArticle.UpdatedAt = CommonModel.SiteCode;
                                objArticle.UpdatedOn = DateTime.Now;
                                objArticle.UpdatedBy = CommonModel.UserID;
                                Context.ArticlelblPrintDtl.Add(objArticle);
                                Context.SaveChanges();
                            }
                        }
                        else
                        {
                            //add row to export excel file
                            if (!Convert.ToString(excelSheet2[excelrow, 0].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 0].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 0).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 1].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 1].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 1).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 2].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 2].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 2).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 3].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 3].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 3).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 4].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 4].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 4).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 5].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 5].Value = Convert.ToString(materialArticleMap.GetCell(rowCntr, 5).Value);
                            }
                            if (!Convert.ToString(excelSheet2[excelrow, 6].Value).Contains(" (Invalid)"))
                            {
                                excelSheet2[excelrow, 6].Value = Convert.ToBoolean(materialArticleMap.GetCell(rowCntr, 6).Value);
                            }
                            excelrow++;
                        }
                    }

                    #endregion
                                       
                    dbContextTransaction.Commit();
                    tranResult = true;
                }
            }
            catch (Exception ex)
            {
                //  throw ex;
            }
            return tranResult;
        }
    }
}





