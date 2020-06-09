using System.Linq;
using Spectrum.Models;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IBarcodeRepository : IGenericRepository<MstArticle>
    {
       IList<DocumentModel> GetDocumentList(string DocType, string DocNumber);
    }
}
