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

namespace Spectrum.BL
{
    public class BarcodeManager : GenericManager<BarcodeModel>, IBarcodeManager
    {
        public BarcodeManager()
        {
            this.barcodeRepository = new BarcodeRepository();
            this.commonRepository = new CommonRepository();
             
        }

        private IBarcodeRepository barcodeRepository;
        private ICommonRepository commonRepository;

        public IList<DocumentModel> GetDocumentList(string DocType, string DocNumber)
        {
            return this.barcodeRepository.GetDocumentList(DocType, DocNumber);
        }
    }
}
