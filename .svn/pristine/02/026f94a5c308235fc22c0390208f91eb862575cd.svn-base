using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
using C1.C1Excel;
using Spectrum.DAL.CustomEntities;
using Spectrum.DAL;
namespace Spectrum.BL.BusinessInterface
{

    public interface IMaterialManager
    {
        string defaultProfileName { get; set; }

        //ashm 3 may 2018 - merging excel sheets
        bool SaveImportExcel(ref XLSheet material, ref XLSheet materialArticleMap);

        bool ImportExcelValidations(ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg);

        bool ExportToExcel(ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path);
        //ashma 22 may 2018 - failed material excel export 
        bool Save(ref XLSheet material, ref XLSheet materialArticleMap, ref C1XLBook failedMaterialbook);
    }
}
