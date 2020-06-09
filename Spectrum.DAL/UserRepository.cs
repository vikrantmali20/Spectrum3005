using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;

namespace Spectrum.DAL
{
    public class UserRepository : GenericRepository<AuthUsers>, IUserRepository 
    {
      public AuthUsers IsUserExist(string UserId, string SiteCode)
      {
           
          try
          {
              return  Context.AuthUsers.Where(u => u.UserID == UserId  ).FirstOrDefault();
          }
          catch (Exception ex)
          {
              throw ex;
          }
           
      }
      public AuthUsers IsIdCardExist(string IdCard)
      {

          try
          {
              return Context.AuthUsers.Where(u => u.IDNumber == IdCard ).FirstOrDefault();
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public AuthUsers IsIdCardExistForOtherUser(string IdCard,string UserId)
      {

          try
          {
              return Context.AuthUsers.Where(u => u.IDNumber == IdCard && u.UserID!=UserId  ).FirstOrDefault();
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public IList<AuthUsers> GetUserListEdit()
      {
          try
          {
              return Context.AuthUsers.OrderBy(a => a.UserID).ToList();
              //return Context.AuthUsers.Where(a=> a.SiteCode==CommonModel.SiteCode ).OrderBy(a => a.UserID ).ToList();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public AuthUsers GetUserById(string userId)
      {
          try
          {
              return Context.AuthUsers.Where(x => x.UserID == userId).OrderBy(a => a.UserID).FirstOrDefault();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public MstSalesPerson GetSalesPersonById(string userId)
      {
          try
          {
              return Context.MstSalesPerson.Where(x => x.Empcode == userId ).FirstOrDefault();
           
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public AuthUserSiteRoleMap GetRolesDataById(string userId)
      {
          try
          {
              return Context.AuthUserSiteRoleMap.Where(x => x.UserID  == userId ).FirstOrDefault();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public bool UpdateUser(AuthUsers user, AuthUserSiteRoleMap userRole, MstSalesPerson salesPerson, bool flagRoleAddEdit,bool flagSalesAddEdit)
      {

          using (var context = ContextFactory.CreateContext())
          {
              using (var dbTran = context.Database.BeginTransaction())
              {
                  try
                  {
                      var existUser = GetUserById(user.UserID);

                      existUser.UserName = user.UserName;
                      existUser.Designation = user.Designation;
                      existUser.IDNumber = user.IDNumber;
                      existUser.EmailId = user.EmailId;
                     // existUser.SiteCode = user.SiteCode;
                      existUser.Active = user.Active;
                      existUser.UPDATEDON = DateTime.Now;
                      existUser.issalesperson = user.issalesperson;
                      existUser.UPDATEDBY = CommonModel.UserID;
                      existUser.UPDATEDAT = CommonModel.SiteCode;
                      Context.Entry(existUser).State = EntityState.Modified;
                      if (userRole != null && flagRoleAddEdit == true)
                      {
                          var existRole = GetRolesDataById(user.UserID);
                          existRole.GRoleid = userRole.GRoleid;
                         // existRole.SiteCode = userRole.SiteCode;
                          existRole.UPDATEDON = DateTime.Now;
                          existRole.UPDATEDBY = CommonModel.UserID;
                          existRole.UPDATEDAT = CommonModel.SiteCode;
                          Context.Entry(existRole).State = EntityState.Modified;
                      }
                      else if (userRole != null)
                          Context.AuthUserSiteRoleMap.Add(userRole);


                      //if (existUser != null)
                      //{
                      //    AuthUsers ContextAuthUser;
                      //    ContextAuthUser = Context.AuthUsers.Where(x => x.UserID == user.UserID).FirstOrDefault();
                      //    Context.Entry(ContextAuthUser).State = System.Data.Entity.EntityState.Deleted;

                      //    Context.SaveChanges();
                      //    Context.AuthUsers.Add(user);
                      //    Context.SaveChanges();
                      //}
                      //else
                      //{
                      //    Context.AuthUsers.Add(user);
                      //    Context.SaveChanges();
                      //}


                      var existUserSales = GetRolesDataById(user.UserID); //vipin on 07-04-2017

                      if (userRole != null)
                      {
                          if (existUserSales != null)
                          {
                              Context.Entry(existUserSales).State = System.Data.Entity.EntityState.Deleted;

                              Context.SaveChanges();
                              Context.AuthUserSiteRoleMap.Add(userRole);
                              Context.SaveChanges();
                          }
                          else
                          {
                              Context.AuthUserSiteRoleMap.Add(userRole);
                              Context.SaveChanges();
                          }
                      }

                      var existUserRole = GetSalesPersonById(user.UserID);

                      //code is commented by irfan on 16-7-2018
                      //if (existUserRole != null)
                      //{
                      //    Context.Entry(existUserRole).State = System.Data.Entity.EntityState.Deleted;

                      //    Context.SaveChanges();
                      //    Context.MstSalesPerson.Add(salesPerson);
                      //    Context.SaveChanges();
                      //}
                      //else
                      //{
                      //    if (context.MstSalesPerson.Any(x => x.Empcode == user.UserID))
                      //    {
                      //        Context.MstSalesPerson.Add(salesPerson);
                      //        Context.SaveChanges();
                      //    }
                      //}

                      if (salesPerson != null)
                      {
                          if (existUserRole != null)
                          {
                              //if (context.MstSalesPerson.Any(x => x.Empcode == user.UserID))
                              //{
                              //    Context.Entry(existUserRole).State = System.Data.Entity.EntityState.Deleted;
                              //    context.SaveChanges();
                              //    Context.MstSalesPerson.Add(salesPerson);
                              //    Context.SaveChanges();
                              //    existUserRole.UPDATEDON = DateTime.Now;
                              //    existUserRole.UPDATEDBY = CommonModel.UserID;
                              //    existUserRole.UPDATEDAT = CommonModel.SiteCode;
                              //    context.SaveChanges();
                              //}
                              existUserRole.SalesPersonName = salesPerson.SalesPersonName;
                              existUserRole.SalesPersonFullName = salesPerson.SalesPersonName;
                              existUserRole.SalesSection = salesPerson.SalesSection;
                              existUserRole.SalesArea = salesPerson.SalesArea;
                              existUserRole.UPDATEDON = DateTime.Now;
                              existUserRole.UPDATEDBY = CommonModel.UserID;
                              existUserRole.UPDATEDAT = CommonModel.SiteCode;
                              Context.Entry(existUserRole).State = EntityState.Modified;
                              Context.SaveChanges();
                          }
                          else
                          {
                              Context.MstSalesPerson.Add(salesPerson);
                              Context.SaveChanges();
                          }
                      }


        

                      //if (salesPerson != null && flagSalesAddEdit == true)
                      //{
                      //    var existSales = GetSalesPersonById(user.UserID);  //vipin on 07-04-2017
                      //    if (existSales != null)
                      //    {
                      //        existSales.SalesPersonName = salesPerson.SalesPersonName;
                      //        existSales.SalesPersonFullName = salesPerson.SalesPersonName;
                      //        existSales.SalesSection = salesPerson.SalesSection;
                      //        existSales.SalesArea = salesPerson.SalesArea;
                      //        existSales.SiteCode = existSales.SiteCode;
                      //        existSales.UPDATEDON = DateTime.Now;
                      //        existSales.UPDATEDBY = CommonModel.UserID;
                      //        existSales.UPDATEDAT = CommonModel.SiteCode;
                      //        Context.Entry(existSales).State = EntityState.Modified;
                      //    }
                      //    else
                      //    {
                      //        Context.MstSalesPerson.Add(salesPerson);
                      //    }

                      //}
                      //else if (salesPerson != null)
                      //    Context.MstSalesPerson.Add(salesPerson);

                      //Context.SaveChanges();
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
      public bool AddUser(AuthUsers user, AuthUserSiteRoleMap userRole, MstSalesPerson salesPerson)
      {

          using (var context = ContextFactory.CreateContext())
          {
              using (var dbTran = context.Database.BeginTransaction())
              {
                  try
                  {
                      Context.AuthUsers.Add(user);
                      if (userRole != null)
                          Context.AuthUserSiteRoleMap.Add(userRole);
                      if (salesPerson != null)
                          Context.MstSalesPerson.Add(salesPerson);
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
    }
}
