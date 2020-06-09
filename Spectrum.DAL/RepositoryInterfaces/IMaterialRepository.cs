using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;
using C1.C1Excel;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IMaterialRepository : IGenericRepository<MstMaterial>
    {
        string defaultProfileName { get; set; }

        //bool SaveImportExcel(ref List<MstMaterial> materialList);

        //ashm 3 may 2018 - merging excel sheets
        bool SaveImportExcel(ref XLSheet material, ref XLSheet materialArticleMap);

        bool ImportExcelValidations(ref XLSheet material, ref XLSheet materialArticleMap, ref string ErrorMsg);

        bool ExportToExcel(ref List<MstMaterial> objMaterial, ref List<MstMaterialArticleMap> objMaterialArticleMap, ref string path);
        //ashma 22 may 2018 - failed material excel export 
        bool Save(ref XLSheet material, ref XLSheet materialArticleMap, ref C1XLBook failedMaterialbook);
    }
}

