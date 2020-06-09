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

namespace Spectrum.DAL
{
   public class BarcodeRepository: GenericRepository<MstArticle>, IBarcodeRepository
    {
       public IList<DocumentModel> GetDocumentList(string DocType, string DocNumber)
       {
           try
           {
               string sitecode = CommonModel.SiteCode;
               var articlelist = (from asb in Context.OrderHdr 
                                  join atnm in Context.MstSite on asb.DeliverySiteCode equals atnm.SiteCode
                                  join sup in Context.MstSupplier on asb.SupplierCode equals sup.SupplierCode  
                                   
                                  where asb.SiteCode == CommonModel.SiteCode && asb.IsClosed==false && asb.OrderStatus=="Approved" && asb.ApprovalLevel=="1"
                                  && ((string.IsNullOrEmpty(DocType) || (!string.IsNullOrEmpty(DocType) && asb.DocumentType == DocType))
                                  && ((string.IsNullOrEmpty(DocNumber) || (!string.IsNullOrEmpty(DocNumber) && asb.DocumentNumber.Contains(DocNumber)))))
                                  
                                  select new DocumentModel
                                  {
                                      DocumentNumber=asb.DocumentNumber ,
                                      DocDate=asb.DocDate,
                                      SupplierName=sup.SupplierName,
                                      SiteShortName=atnm.SiteShortName ,
                                      OrderStatus=asb.OrderStatus,
                                      DocNetValue=asb.DocNetValue 
                                  }).Distinct().ToList();
               return articlelist;

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
