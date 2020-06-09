using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Spectrum.Models;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using AutoMapper;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL.CustomEntities;
using C1.C1Excel;
using System.Text.RegularExpressions;
using Spectrum.Logging;   // added by vipin on 03-4-2017
namespace Spectrum.BL
{
    public class MaterialManager : GenericManager<MaterialModel>, IMaterialManager
    {
        const int xlscolumnRow = 0;

        public MaterialManager()
        {
            this.materialRepository = new MaterialRepository();
            this.commonRepository = new CommonRepository();
            this.materialRepository.defaultProfileName = defaultProfileName;

        }

        private IMaterialRepository materialRepository;
        private ICommonRepository commonRepository;

        public string defaultProfileName { get; set; }

        #region 
        //ashma 3 may 2018 - merging excel sheets
        //public bool ImportExcelValidations(ref XLSheet articleLblPrint, ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg)
        public bool ImportExcelValidations(ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg)    
        {
            bool result = false;
            try
            {
               // result = this.materialRepository.ImportExcelValidations(ref articleLblPrint, ref material, ref materialArticleMap, ref ErrorMsg);
                result = this.materialRepository.ImportExcelValidations(ref material, ref materialArticleMap, ref ErrorMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //public bool SaveImportExcel(ref XLSheet articleLblPrint, ref XLSheet material, ref XLSheet materialArticleMap)
        public bool SaveImportExcel(ref XLSheet material, ref XLSheet materialArticleMap)
        {
            bool result = false;
            try
            {
                //result = this.materialRepository.SaveImportExcel(ref articleLblPrint, ref material, ref materialArticleMap);
                result = this.materialRepository.SaveImportExcel(ref material, ref materialArticleMap);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //public bool ExportToExcel(ref List<ArticlelblPrintDtl> objArticleLblPrint, ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path)
        public bool ExportToExcel(ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path)
        {
        bool result = false;
            try
            {
                //result = this.materialRepository.ExportToExcel(ref objArticleLblPrint, ref objMaterial,ref objMaterialArticleMap , ref path);
                result = this.materialRepository.ExportToExcel(ref objMaterial, ref objMaterialArticleMap, ref path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //ashma 22 may 2018 - failed material excel export 
        public bool Save(ref XLSheet material, ref XLSheet materialArticleMap, ref C1XLBook failedMaterialbook)
        {
            bool result = false;
            try
            {
                //result = this.materialRepository.ExportToExcel(ref objArticleLblPrint, ref objMaterial,ref objMaterialArticleMap , ref path);
                result = this.materialRepository.Save(ref material, ref materialArticleMap, ref failedMaterialbook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }

}
