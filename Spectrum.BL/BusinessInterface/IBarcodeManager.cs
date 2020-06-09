using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;

namespace Spectrum.BL.BusinessInterface
{
   public interface IBarcodeManager
    {
        IList<DocumentModel> GetDocumentList(string DocType, string DocNumber);
    }
}
