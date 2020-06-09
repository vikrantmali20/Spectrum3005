using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
   public class UserModel 
    {
        public AuthUserModel AuthUserModel { get; set; }
        public AuthUserSiteRoleMapModel AuthUserSiteRoleMapModel { get; set; }
        public AuthUserSiteTransactionMapModel AuthUserSiteTransactionMapModel { get; set; }
        public MstSalesPersonModel MstSalesPersonModel { get; set; }
    }
   public class AuthUserModel : BaseModel
   {
       public string SiteCode { get; set; }
       public string UserID { get; set; }
       public string UserName { get; set; }
       public string CountryCode { get; set; }
       public string Designation { get; set; }
       public string IDNumber { get; set; }
       public string Password { get; set; }
       public Nullable<System.DateTime> PasswordUpdateDate { get; set; }
       public Nullable<System.DateTime> PasswordChangeNextDate { get; set; }
       public string EmailId { get; set; }
       public Nullable<System.DateTime> PasswordExpiredon { get; set; }
       public string Active { get; set; }
       public Nullable<bool> issalesperson { get; set; }
   }

   public class AuthUserModelEdit 
   {
        
       [DisplayName("Login Name")]
       public string UserID { get; set; }
       [DisplayName("User Name")]
       public string UserName { get; set; }
        
       [DisplayName("Designation")]
       public string Designation { get; set; }
       
       [DisplayName("Email Address")]
       public string EmailId { get; set; }
       
       [DisplayName("Is User Active?")]
       public string Active { get; set; }
        
   }
   public  class AuthUserSiteRoleMapModel:BaseModel
   {
       public string UserID { get; set; }
       public string SiteCode { get; set; }
       public string GRoleid { get; set; }
        

   }
   public class AuthRoleTransactionMapModel:BaseModel 
   {
       public string AuthTransactionCode { get; set; }
       public string RoleID { get; set; }
       

      
   }
   public class AuthUserSiteTransactionMapModel:BaseModel 
   {
       public string UserID { get; set; }
       public string SiteCode { get; set; }
       public string AuthTransactionCode { get; set; }
       public bool Rights { get; set; }
        

   }
   public class MstSalesPersonModel:BaseModel 
   {
       public string SiteCode { get; set; }
       public string Empcode { get; set; }
       public string SalesPersonName { get; set; }
       public string SalesPersonFullName { get; set; }
       public string SalesArea { get; set; }
       public string SalesSection { get; set; }
      
   }
}
